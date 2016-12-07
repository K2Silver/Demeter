using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;

namespace CreateRemote
{
    #region Enumerations
    // Definitions of iRobot Create OpenInterface Command Numbers
    // See the Create OpenInterface manual for a complete list
    public enum Commands
    {
        // Create Command // Arguments
        Start = 128,
        SafeMode = 131,
        FullMode = 132,
        Drive = 137, // 4: [Vel. Hi][Vel Low][Rad. Hi][Rad. Low]
        DriveDirect = 145, // 4: [Right Hi][Right Low][Left Hi][Left Low]
        Sensors = 142, // 1: Sensor Packet ID
        CoverandDock = 143,// 1: Return to Charger
        Demo = 136, // 2: Run Demo x
        Song = 140, // Download Song Notes - up to 16 notes
        PlaySong = 141, // 2: Play Song number x
        LED = 139, // 3: LED output control
        Stream = 148, // x+1: [# of packets]IDs of packets to stream
        QueryList = 149, // x+1: [# of packets] IDs of requested packets
        StreamPause = 150, // 1: 0 = stop stream, 1 = start stream
    }
    /* iRobot Create Sensor Packet IDs */
    public enum Sensors
    {
        BumpsandDrops = 7,
        Distance = 19,
        Angle = 20,
        RequestedRightVelocity = 41,
        RequestedLeftVelocity = 42,
        LeftEncoderCounts = 43,
        RightEncoderCounts = 44,
    }
    #endregion Enumerations

    public partial class Form1 : Form
    {
        public readonly String PYTHON = @"C:\Python27\python.exe";
        public readonly String PYTHON_SCRIPT_UPDATE_TABLE = ("\"" + Directory.GetCurrentDirectory() + "\\..\\..\\..\\python_scripts\\update_table.py" + "\"");
        public readonly String DELETE_DELIVERED = "DeleteDelivered";
        public readonly String CHECK_NEXT_ORDER = "CheckNextOrder";

        public SerialPort port;
        public Int16 left = 20; // default speed left wheel
        public Int16 right = 20; // default speed right wheel
        private bool ProcessingSensorDataPacket = false;
        public bool raw_sensor_debug = false;

        // Constants
        public volatile short d45 = 92; // turning distance for 45 degrees when both wheels are turning
        public static readonly short d_block = 330; // Block size for map grid in mm

        // variables for control system
        public volatile short new_l, old_l, l, base_l, new_r, old_r, r, base_r;
        public volatile short lr_difference;
        public volatile short angle, new_angle;
        public volatile short x, y;
        public volatile short new_distance;
        public volatile short temp_l, temp_r;
        
        // Flags
        public volatile bool setBase = true;
        public volatile bool move_turn = false;
        public volatile bool move_forward = false;
        public volatile bool executingCommand = false;
        public volatile bool docked = true;

        // List of commands to execute
        public List<DirectionCommand> commandList;
        DirectionCommand currentCommand = DirectionCommand.NONE;

        // Background workers
        private BackgroundWorker bwUpdateValues;
        private BackgroundWorker bwExecuteCommands;
        private BackgroundWorker bwCheckNextOrder;

        // Serial Buffer
        public volatile byte[] buf = new byte[6];

        public Form1()
        {
            InitializeComponent();

            // Update combobox on startup (list of serial ports available)
            String[] comPortList = SerialPort.GetPortNames();
            foreach (String comPort in comPortList)
            {
                comboBoxCOM.Items.Add(comPort);
            }

            // Set to highest COM port by default
            if (comPortList.Length == 0)
            {
                comboBoxCOM.Text = "COM1";
            }
            else
            {
                comboBoxCOM.Text = comPortList[comPortList.Length - 1];
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // Start Button - send start and safe mode, start streaming sensor data
        private void btnStart_Click(object sender, EventArgs e)
        {

            // Serial Port Open Code - used to communicate with iRobot Create robot
            port = new SerialPort(comboBoxCOM.Text, 115200, Parity.None, 8, StopBits.One);
            port.Handshake = Handshake.None;
            port.ReadTimeout = 60000;
            port.WriteTimeout = 60000;
            try // Just in case .NET CF not updated
            {
                port.Open();
            } catch (Exception com_open_except) { }

            port.Write(new byte[] { (byte)Commands.Start, (byte)Commands.SafeMode }, 0, 2);
            Thread.Sleep(100);
            System.Diagnostics.Debug.WriteLine("Requesting data from bot");
            port.Write(new byte[] { (byte)Commands.Stream, 2, (byte)Sensors.LeftEncoderCounts,
            (byte)Sensors.RightEncoderCounts}, 0, 4);
            Thread.Sleep(200);
            
            // Attaching two handlers for better response time
            port.DataReceived += new
            SerialDataReceivedEventHandler(port_DataReceived);
            port.DataReceived += new
            SerialDataReceivedEventHandler(port_DataReceived);

            // Launch background worker to update encoder values and do angle/distance calculation
            bwUpdateValues = new BackgroundWorker();
            bwUpdateValues.DoWork += new DoWorkEventHandler(backgroundWorker_updateValues);
            bwUpdateValues.RunWorkerAsync();

            // Launch background worker to check for new orders
            bwCheckNextOrder = new BackgroundWorker();
            bwCheckNextOrder.DoWork += new DoWorkEventHandler(backgroundWorker_checkNextOrder);
            bwCheckNextOrder.RunWorkerAsync();

            // Initialize, but not launch a background worker to handle direction commands
            bwExecuteCommands = new BackgroundWorker();
            bwExecuteCommands.DoWork += new DoWorkEventHandler(backgroundWorker_move);
            bwExecuteCommands.WorkerSupportsCancellation = true;

            // Disable start
            btnStart.Enabled = false;

            // Initialize list of direction commands
            commandList = new List<DirectionCommand>();
        }
        // Stop Button - turn off drive motors
        private void btnStop_Click(object sender, EventArgs e)
        {
            port.Write(new byte[] { (byte)Commands.DriveDirect, (byte)0, (byte)0, (byte)0, (byte)0 }, 0, 5);
        }
        // Forward Button - turn on drive motors
        private void btnForward_Click(object sender, EventArgs e)
        {
            port.Write(new byte[] { (byte)Commands.DriveDirect,
                (byte)(right >> 8), (byte)right,
                (byte)(left >> 8), (byte)left }, 0, 5);
        }
        // Reverse Button - reverse drive motors
        private void btnReverse_Click(object sender, EventArgs e)
        {
            port.Write(new byte[] { (byte)Commands.DriveDirect,
                (byte)(-right >> 8), (byte)-right,
                (byte)(-left >> 8), (byte)-left }, 0, 5);
        }
        // Left Button - drive motors set to rotate to left
        private void btnLeft_Click(object sender, EventArgs e)
        {
            port.Write(new byte[] { (byte)Commands.DriveDirect,
                (byte)(right >> 8), (byte)right,
                (byte)(-left >> 8), (byte)-left }, 0, 5);
        }
        // Right Button - drive motors set to rotate to right
        private void btnRight_Click(object sender, EventArgs e)
        {
            port.Write(new byte[] { (byte)Commands.DriveDirect,
                (byte)(-right >> 8), (byte)-right,
                (byte)(left >> 8), (byte)left }, 0, 5);
        }
        // Charger Button - return to charger using IR beacons
        private void btnCharger_Click(object sender, EventArgs e)
        {
            port.Write(new byte[] { (byte)Commands.Demo, (byte)1 }, 0, 2);
        }
        // Play Song Button - define and play a song on robot
        private void btnSong_Click(object sender, EventArgs e)
        {// Send out notes & duration to define song and play song
            port.Write(new byte[] { (byte)Commands.Song, 0, 16,
                91, 24, 89, 12, 87, 36, 87, 24, 89, 12,
                91, 24, 91, 12, 91, 12, 89, 12, 87, 12, 89, 12,
                91, 12, 89, 12, 87, 24, 86, 12, 87, 48 }, 0, 35);
            Thread.Sleep(100);
            port.Write(new byte[]{(byte)Commands.PlaySong,
                (byte)0 }, 0, 2);

        }
        // Slider trackbar to set motor speed
        private void trackBarSpeed_Scroll(object sender, EventArgs e)
        {

            left = (Int16)trackBarSpeed.Value;
            right = (Int16)trackBarSpeed.Value;
        }

        // Checks for Proper Checksum at end of Sensor Data Packet
        bool validData(byte[] buf, byte chksum)
        {
            foreach (byte b in buf) chksum += b;
            if (chksum != 0)
            {
                return false;
            }
            else return true;
        }
        // Event activated whenever new serial data is recieved
        // packets are sent every 15 MS
        void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (ProcessingSensorDataPacket) return;
            {
                ProcessingSensorDataPacket = true;
                try
                {
                    // Distance Data Read
                    // align with sensor packet header value and read in data
                    while (port.ReadByte() != 19) { }

                    port.ReadByte(); // skip first byte
                    port.Read(buf, 0, 6);
                    
                }
                catch (TimeoutException)
                {
                    Console.WriteLine("Serial port Read timed out?");
                }
                catch
                {
                }
                ProcessingSensorDataPacket = false;
            }
        }

        // Checkbox to display raw sensor data on console for debug
        // delays response a bit - data backs up in buffers when on
        private void checkBoxRawSensor_CheckStateChanged(object sender,
        EventArgs e)
        {
            raw_sensor_debug = chkboxDisplayRaw.Checked;
        }

        // Seek Dock
        private void btnDock_Click(object sender, EventArgs e)
        {
            port.Write(new byte[] { (byte)Commands.CoverandDock }, 0, 1);
        }


        // Display raw sensor value or not
        private void checkBoxRawSensor_CheckedChanged(object sender, EventArgs e)
        {
            raw_sensor_debug = chkboxDisplayRaw.Checked;
        }

        // Background worker code for checking next order
        private void backgroundWorker_checkNextOrder(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                // Use python script to get next destination
                String result = run_python(PYTHON_SCRIPT_UPDATE_TABLE, CHECK_NEXT_ORDER);

                // If destination is not "NoOrdersReady", find destination and get new command
                if (result != "NoOrdersReady")
                {
                    int[] destination = new int[] { -1, -1 };
                    destination = map.getDestinationByName(result.Substring(0,1).ToUpper());

                    // Solve path based on map and destination
                    List<int> path = PathFinder.solve(map, destination);

                    // Paint path to destinatino
                    map.paintPath(path);

                    // Multiply by 2, necessary since only using orthogonal movements
                    // Original Values
                    // [ ][2][ ]
                    // [3][ ][1]
                    // [ ][0][ ]
                    //
                    // Need values in this fashion
                    // [5][4][3]
                    // [6][ ][2]
                    // [7][0][1]
                    for (int i = 0; i < path.Count; i++)
                    {
                        path[i] = 2 * path[i];
                    }

                    // Set command box to show path
                    SetText(textBoxCommand, String.Join(",", path.ToArray()));

                    // Add path and return path to command list
                    short[] to_trip = Array.ConvertAll(path.ToArray(), s => (short)s);
                    commandList.Clear();
                    for (int i = 0; i < to_trip.Length; i++)
                    {
                        commandList.Add(DirectionCommand.getCommand(to_trip[i]));
                    }
                    for (int i = to_trip.Length - 1; i >= 0; i--)
                    {
                        commandList.Add(DirectionCommand.reverse(DirectionCommand.getCommand(to_trip[i])));
                    }

                    // Move South 3 and North 1 before docking
                    commandList.Add(DirectionCommand.S);
                    commandList.Add(DirectionCommand.S);
                    commandList.Add(DirectionCommand.S);
                    commandList.Add(DirectionCommand.N);

                    // Launch thread
                    bwExecuteCommands.RunWorkerAsync();
                }
                Thread.Sleep(10000); // Wait 10 seconds
            }
        }

        // Background worker to handle moving
        private void backgroundWorker_move(object sender, DoWorkEventArgs e)
        {
            // Iterate through each command
            for (int i = 0; i < commandList.Count; i++)
            {
                // Get next command in list
                DirectionCommand command = commandList[i];
                // Range check
                if ((command.Code > 7) || (command.Code < 0))
                {
                    SetText(textBoxReceived, "Invalid");
                    btnSend.Enabled = true;
                    return;
                }
                // Show current command in square brackets
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < commandList.Count; j++)
                {
                    if (i == j)
                    {
                        sb.Append("[");
                        sb.Append(commandList[j].ToString());
                        sb.Append("]");
                    }
                    else
                    {
                        sb.Append(commandList[j].ToString());
                    }
                    sb.Append(",");
                }
                SetText(textBoxReceived, sb.ToString());

                // Set current command, distance to go, and angle
                currentCommand = command;
                new_distance = (short)(d_block * currentCommand.Multiplier);
                new_angle = (short) currentCommand.Angle;

                // Set executing command to true
                executingCommand = true;
                move_turn = true;
                move_forward = true;

                // Loop until execution of command finished
                while (executingCommand)
                {
                    // Code to move back from dock
                    if (docked)
                    {
                        // Move back 50mm
                        temp_l = l;
                        while (temp_l - 50 < l)
                        {
                            moveBackward();
                        }

                        // Cancel demo mode, since it starts automatically if undocked
                        port.Write(new byte[] { (byte)Commands.Demo, (byte)(255) }, 0, 1);
                        port.Write(new byte[] { (byte)Commands.Start, (byte)Commands.SafeMode }, 0, 2);
                        Thread.Sleep(100);
                        System.Diagnostics.Debug.WriteLine("Requesting data from bot");
                        port.Write(new byte[] { (byte)Commands.Stream, 2, (byte)Sensors.LeftEncoderCounts,
                             (byte)Sensors.RightEncoderCounts}, 0, 4);
                        Thread.Sleep(200);

                        // Set flag to false
                        docked = false;
                    }
                    // Currently turning to correct angle
                    else if (move_turn)
                    {
                        // Calculate value to determine which way to turn
                        short turnval = mod((short)(angle - new_angle - 180), 360);

                        // Display value for debugging
                        SetText(textBoxAngleCalc, turnval.ToString());

                        // If value is between 178 and 182, stop turning
                        if (turnval <= 182 && turnval >= 178) 
                        {
                            move_turn = false;
                            temp_l = l;
                        }
                        // Else, if less than 178, turn right
                        else if (turnval < 178)
                        {
                            turnRight();
                        }
                        // Else, if greater than 182, turn left
                        else if (turnval > 182)
                        {
                            turnLeft();
                        }
                    }
                    // Currently moving forward
                    else if (move_forward)
                    {
                        // Move forward until l variable becomes greater than old l and new distance
                        if ((temp_l + new_distance)  > l)
                        {
                            moveForward();
                        }
                        // Otherwise, stop
                        else if ((temp_l + new_distance) <= l)
                        {
                            stop();
                            move_forward = false;
                            executingCommand = false;
                        }
                        else
                        {
                        }
                    }
                    else // End command execution
                    {
                        executingCommand = false;
                        Thread.Sleep(500);
                    }
                    Thread.Sleep(0); // Pass to different thread
                }
            }

            // Execution finished, delete delivered item from table with Python script
            SetText(textBoxReceived, run_python(PYTHON_SCRIPT_UPDATE_TABLE, DELETE_DELIVERED));

            // Attempt to dock
            dock();
            docked = true;
        }

        // Helper method to set text from background worker
        private void SetText(TextBox textbox, string text)
        {
            if (this.InvokeRequired)
            {
                try
                {
                    Invoke(new MethodInvoker(delegate()
                    {
                        SetText(textbox, text);
                    }));
                }
                catch (ObjectDisposedException e) { }
            }
            else
            {
                textbox.Text = text;
            }
        }

        // Background worker to update values
        private void backgroundWorker_updateValues(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                // Compute the new encoder values
                new_l = (short)((buf[1] << 8) | buf[2]);
                new_r = (short)((buf[4] << 8) | buf[5]);

                // Continuously updating l and r values
                old_l = (short)((new_l) * 72 * Math.PI / 508.8);
                old_r = (short)((new_r) * 72 * Math.PI / 508.8);

                // setting base value for left and right encoders
                if (setBase == true && new_l != 0)
                {
                    base_l = old_l;
                    base_r = old_r;
                    setBase = false;
                }
                // Absolute L and R Wheel travelDistance
                l = (short)(old_l - base_l);
                r = (short)(old_r - base_r);

                // Updating angle turned based on l and r difference
                computeAngle();

                // Print encoder information
                SetText(textBoxBaseL, (base_l).ToString());
                SetText(textBoxBaseR,(base_r).ToString());
                SetText(textBoxCurrentL,(l).ToString());
                SetText(textBoxCurrentR,(r).ToString());
                SetText(textBoxAngle,(angle).ToString());
                SetText(textBoxTurningDistance,(lr_difference).ToString());
                SetText(textBoxTempL,"" + temp_l);

                Thread.Sleep(0); // Pass to another thread
            }

        }

        // Functions for robot movement
        private void turnRight()
        {
            port.Write(new byte[] { (byte)Commands.DriveDirect, (byte)(-right >> 8), (byte)-right, (byte)(left >> 8), (byte)left }, 0, 5);
        }

        private void turnLeft()
        {
            port.Write(new byte[] { (byte)Commands.DriveDirect, (byte)(right >> 8), (byte)right, (byte)(-left >> 8), (byte)-left }, 0, 5);
        }

        private void stop()
        {
            port.Write(new byte[] { (byte)Commands.DriveDirect, (byte)0, (byte)0, (byte)0, (byte)0 }, 0, 5);
        }

        private void dock()
        {
            port.Write(new byte[] { (byte)Commands.CoverandDock }, 0, 1);
        }

        private void moveForward()
        {
            port.Write(new byte[] { (byte)Commands.DriveDirect, (byte)(right >> 8), (byte)right, (byte)(left >> 8), (byte)left }, 0, 5);
        }

        private void moveBackward()
        {
            port.Write(new byte[] { (byte)Commands.DriveDirect, (byte)(-right >> 8), (byte)-right, (byte)(-left >> 8), (byte)-left }, 0, 5);
        }

        // Helper function to compute the angle based on encoder values
        private void computeAngle() {
            lr_difference = (short)(l - r);
            angle = mod((short)(((((lr_difference) / 2.0) / d45) * 45.0)), (short)360);  // this angle relative to left wheel motion

        }

        // Modulus function, since C# modulus does a remainder
        private short mod(short x, short m)
        {
            return (short) ((x % m + m) % m);
        }
    
        // Command Input
        private void textBoxComand_TextChanged(object sender, EventArgs e)
        {

        }

        // Send commands with button click
        private void buttonSend_Click(object sender, EventArgs e)
        {
          
            // Set command list and execute all commands in background worker thread
            short[] to_trip = Array.ConvertAll(textBoxCommand.Text.Split(','), s => short.Parse(s));
            commandList.Clear();
            for(int i = 0; i < to_trip.Length; i++)
            {
                commandList.Add(DirectionCommand.getCommand(to_trip[i]));
            }
            for (int i = to_trip.Length - 1; i >= 0; i--)
            {
                commandList.Add(DirectionCommand.reverse(DirectionCommand.getCommand(to_trip[i])));
            }
            if (bwExecuteCommands != null && bwExecuteCommands.IsBusy)
            {
                bwExecuteCommands.CancelAsync();
                Thread.Sleep(1000); // TODO: wait until cancel
            }
            bwExecuteCommands.RunWorkerAsync();
        }

        // Refresh list of COM Ports 
        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            // Clear combobox
            comboBoxCOM.Items.Clear();

            // Add available com ports to combobox
            String[] comPortList = SerialPort.GetPortNames();
            foreach (String comPort in comPortList)
            {
                comboBoxCOM.Items.Add(comPort);
            }
        }

        // Map object
        Map map;

        // Button dimensions and placement
        static readonly int BUTTON_SIZE = 20;
        static readonly int BUTTON_LEFT_MARGIN = 750;
        static readonly int BUTTON_TOP_MARGIN = 100;

        // Add buttons to form
        private void showMap(Map map)
        {
            // Values used for button positioning
            int horizontal = BUTTON_LEFT_MARGIN;
            int vertical = BUTTON_TOP_MARGIN;

            // Button array
            Button[] buttonArray = map.getButtonArray();

            // For each button array, initialize it and place it on the form, row major
            for (int i = 0; i < buttonArray.Length; i++)
            {
                // Set size and location
                buttonArray[i].Size = new Size(BUTTON_SIZE, BUTTON_SIZE);
                buttonArray[i].Location = new Point(horizontal, vertical);

                // Go to next row if column value reached
                if ((i + 1) % map.getColNum() == 0)
                {
                    horizontal = BUTTON_LEFT_MARGIN;
                    vertical += BUTTON_SIZE;
                }
                else horizontal += BUTTON_SIZE;

                // Add the button to the window
                this.Controls.Add(buttonArray[i]);
            }
        }

        // Generate the button matrix and display it on the screen
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            // Remove map if not null
            if (map != null && map.getButtonArray() != null)
            {
                Button[] buttonArr = map.getButtonArray();
                for (int i = 0; i < buttonArr.Length; i++)
                {
                    // Add the button to the window
                    this.Controls.Remove(buttonArr[i]);
                }
            }

            // Attempt to parse the values from the text input (may switch to dropdown later)
            int rowNum = 0;
            int colNum = 0;
            Int32.TryParse(txtboxRow.Text, out rowNum);
            Int32.TryParse(txtboxColumn.Text, out colNum);

            // Set new map
            map = new Map(rowNum, colNum);

            // Show map
            showMap(map);
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            List<int> path = PathFinder.solve(map);

            map.paintPath(path);
            for (int i = 0; i < path.Count; i++) {
                path[i] = 2 * path[i];
            }
            textBoxCommand.Text = String.Join(",", path.ToArray());
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openMapDialog = new OpenFileDialog();
            if (openMapDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(openMapDialog.FileName);
                String textToInput = sr.ReadToEnd();
                sr.Close();

                // Set new map
                map = new Map(textToInput);

                // Fill in text boxes
                txtboxRow.Text = map.getRowNum().ToString();
                txtboxColumn.Text = map.getColNum().ToString();

                // Show map
                showMap(map);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            // Text to output to the file
            String textToOutput = txtboxRow.Text + "," + txtboxColumn.Text + "," + String.Join(",", map.getIntMap()) + "," + String.Join(";", map.getDestinations().Select(x => x.Key + "=" + x.Value[0] + ":" + x.Value[1]).ToArray());

            SaveFileDialog saveMapDialog = new SaveFileDialog();
            saveMapDialog.Filter = "Text File|*.txt";
            saveMapDialog.Title = "Save Map as Text File";

            // If the file name is not an empty string, open it for saving
            if (saveMapDialog.ShowDialog() == DialogResult.OK && saveMapDialog.FileName != "")
            {
                System.IO.StreamWriter outputFile = new System.IO.StreamWriter(saveMapDialog.FileName);
                outputFile.WriteLine(textToOutput);
                outputFile.Close();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            map.clearPath();
            textBoxCommand.Text = "";
        }

        private string run_python(String script, String args)
        {
            Console.WriteLine("Starting python script");
            ProcessStartInfo mProcessStartInfo = new ProcessStartInfo(PYTHON);

            mProcessStartInfo.UseShellExecute = false;
            mProcessStartInfo.RedirectStandardOutput = true;
            
            mProcessStartInfo.Arguments = string.Format("{0} {1}", script, args);
            Process myProcess = new Process();
            myProcess.StartInfo = mProcessStartInfo;
            Console.WriteLine("Calling python script");
            myProcess.Start();

            StreamReader sr = myProcess.StandardOutput;
            String stringOutput = sr.ReadLine();

            myProcess.WaitForExit();
            myProcess.Close();

            return stringOutput;
        }

    }

    // Map class that has matrix of buttons to represent room
    public class Map
    {
        // Member variables
        private Button[] btnArray;
        private int rowNum;
        private int colNum;
        private Dictionary<String, int[]> destinations;
        private int numDest;

        // Constants
        private static readonly String BUTTON_WALL_NUM = "1";
        private static readonly String BUTTON_START_NUM = "2";
        private static readonly String BUTTON_FINISH_NUM = "3";
        private static readonly String BUTTON_OPEN_NUM = "0";
        private static readonly String BUTTON_WALL = "W";
        private static readonly String BUTTON_START = "S";
        private static readonly String BUTTON_FINISH = "F";
        private static readonly String BUTTON_OPEN = "";
        private static readonly Color COLOR_OPEN_BUTTON = Color.White;
        private static readonly Color COLOR_WALL_BUTTON = Color.Black;
        private static readonly Color COLOR_START_BUTTON = Color.Blue;
        private static readonly Color COLOR_FINISH_BUTTON = Color.Red;
        private static readonly Color COLOR_PATH = Color.Green;

        private static readonly String DEST_KITCHEN = "K";
        private static readonly String DEST_DINING_ROOM = "D";
        private static readonly String DEST_OFFICE = "O";
        private static readonly String DEST_BEDROOM = "B";

        private static readonly String[] DEST_CHAR = new string[] { DEST_KITCHEN, DEST_DINING_ROOM, DEST_OFFICE, DEST_BEDROOM };

        private static int DEST_MAX = 4;


        public int getRowNum()
        {
            return rowNum;
        }

        public int getColNum()
        {
            return colNum;
        }

        public Button[] getButtonArray()
        {
            return btnArray;
        }

        public Map(int rowNum, int colNum)
        {
            this.rowNum = rowNum;
            this.colNum = colNum;
            this.btnArray = new Button[rowNum * colNum];
            this.numDest = 0;

            for (int i = 0; i < btnArray.Length; i++)
            {
                // Initialize tiles
                btnArray[i] = new Button();
                btnArray[i].BackColor = Color.White;
                btnArray[i].Click += new System.EventHandler(toggleButton);
            }

            // Initialize destination dictionary
            this.destinations = new Dictionary<String, int[]>();
        }


        public Map(String mapText)
        {
            char delimiterChar = ',';
            String[] words = mapText.Split(delimiterChar);
            
            // Attempt to parse the values from the text input (may switch to dropdown later)
            Int32.TryParse(words[0], out rowNum);
            Int32.TryParse(words[1], out colNum);

            this.numDest = 0;

            // Allocate memory for button array
            this.btnArray = new Button[rowNum * colNum];

            // For each button array, initialize it and place it on the form, row major
            for (int i = 0; i < btnArray.Length; i++)
            {
                // Initialize tiles
                btnArray[i] = new Button();
                if (words[i + 2] == BUTTON_OPEN_NUM) { btnArray[i].BackColor = COLOR_OPEN_BUTTON; btnArray[i].Text = BUTTON_OPEN; }
                else if (words[i + 2] == BUTTON_WALL_NUM) { btnArray[i].BackColor = COLOR_WALL_BUTTON; btnArray[i].Text = BUTTON_WALL; }
                else if (words[i + 2] == BUTTON_START_NUM) { btnArray[i].BackColor = COLOR_START_BUTTON; btnArray[i].Text = BUTTON_START; }
                else if (words[i + 2] == BUTTON_FINISH_NUM) { btnArray[i].BackColor = COLOR_FINISH_BUTTON; btnArray[i].Text = BUTTON_FINISH; }
                btnArray[i].Click += new System.EventHandler(toggleButton);
            }

            // Initialize destination dictionary
            this.destinations = new Dictionary<String, int[]>();

            // Add destinations to dictionary
            //TODO.
        }

        // Check if a start square exists in the button array
        public bool startExists()
        {
            foreach (Button btn in btnArray)
            {
                if (btn.Text == BUTTON_START) return true;
            }
            return false;
        }

        // Check if an finish square exists in the button array
        public bool finishExists()
        {
            foreach (Button btn in btnArray)
            {
                if (btn.Text == BUTTON_FINISH) return true;
            }
            return false;
        }

        // Get all destinations
        public Dictionary<String,int[]> getDestinations()
        {
            return destinations;
        }

        // Get destination based on destination name
        public int[] getDestinationByName(String destName)
        {
            int[] dest = new int[] { -1, -1 };
            destinations.TryGetValue(destName, out dest);
            return dest;
        }

        // Get location with text as parameter
        private int[] getLoc(String s)
        {
            int row = 0;
            int col = 0;
            int[] loc = { -1, -1 };
            foreach (Button btn in btnArray)
            {
                if (btn.Text.Equals(s))
                {
                    loc[0] = row;
                    loc[1] = col;
                    return loc;
                }
                col++;
                if (col % colNum == 0)
                {
                    row++;
                    col = 0;
                }
            }
            return loc;
        }

        // Get location of start
        public int[] getStartLoc()
        {
            return getLoc(BUTTON_START);
        }

        // Get location of finish
        public int[] getFinishLoc()
        {
            return getLoc(BUTTON_FINISH);
        }

        // Set location of finish
        public void setFinishLoc(int[] destination)
        {
            // Clear any existing finish locations
            int row = 0;
            int col = 0;
            int[] loc = { -1, -1 };
            foreach (Button btn in btnArray)
            {
                if (row == destination[0] && col == destination[1])
                {
                    btn.Text = BUTTON_FINISH;
                }
                else if (btn.Text.Equals(BUTTON_FINISH))
                {
                    btn.Text = BUTTON_OPEN;
                }
                col++;
                if (col % colNum == 0)
                {
                    row++;
                    col = 0;
                }
            }
        }

        // Paint the path green on the map
        // Path is array of integers
        // [5][4][3]
        // [6][ ][2]
        // [7][0][1]

        public void paintPath(List<int> path)
        {
            int[] currentLoc = getStartLoc();
            foreach (int dir in path)
            {
                switch (dir)
                {
                    //case 0: // south
                    //    currentLoc[0]++;
                    //    break;
                    //case 1: // southeast
                    //    currentLoc[0]++;
                    //    currentLoc[1]++;
                    //    break;
                    //case 2: // east
                    //    currentLoc[1]++;
                    //    break;
                    //case 3: // northeast
                    //    currentLoc[0]--;
                    //    currentLoc[1]++;
                    //    break;
                    //case 4: // north
                    //    currentLoc[0]--;
                    //    break;
                    //case 5: // northwest
                    //    currentLoc[0]--;
                    //    currentLoc[1]--;
                    //    break;
                    //case 6: //west
                    //    currentLoc[1]--;
                    //    break;
                    //case 7: // southwest
                    //    currentLoc[0]++;
                    //    currentLoc[1]--;
                    //    break;
                    //default:
                    //    break;
                    //// TODO throw error
                    case 0: // south
                        currentLoc[0]++;
                        break;
                    case 1: // east
                        currentLoc[1]++;
                        break;
                    case 2: // north
                        currentLoc[0]--;
                        break;
                    case 3: //west
                        currentLoc[1]--;
                        break;
                    default:
                        break;
                    // TODO throw error
                }
                btnArray[colNum * currentLoc[0] + currentLoc[1]].BackColor = COLOR_PATH;
            }
        }

        // Clear path
        public void clearPath()
        {
            foreach (Button btn in btnArray)
            {
                if (btn.Text == BUTTON_FINISH)
                {
                    btn.BackColor = COLOR_FINISH_BUTTON;
                }
                else if (btn.BackColor == COLOR_PATH)
                {
                    btn.BackColor = COLOR_OPEN_BUTTON;
                }
            }
        }

        // Parse map and return values
        public int[] getIntMap()
        {
            // Reallocate memory for map
            int[] mapArray = new int[btnArray.Length];

            for (int i = 0; i < btnArray.Length; i++)
            {
                if (btnArray[i].Text == BUTTON_WALL)
                {
                    mapArray[i] = 1;
                }
                else if (btnArray[i].Text == BUTTON_START)
                {
                    mapArray[i] = 2;
                }
                else if (btnArray[i].Text == BUTTON_FINISH)
                {
                    mapArray[i] = 3;
                }
                else
                {
                    mapArray[i] = 0;
                }
            }

            return mapArray;
        }

        // The click handler for each tile
        public void toggleButton(Object sender, System.EventArgs e)
        {
            Button btn = (Button)sender;
            // If white space, set to wall
            if (btn.Text == BUTTON_OPEN)
            {
                btn.Text = BUTTON_WALL;
                btn.BackColor = Color.Black;
            }
            // If wall, set to BUTTON_START if no start, set to BUTTON_FINISH if no finish
            // Else, clear
            else if (btn.Text == BUTTON_WALL)
            {
                // If start square does not exist, set to start
                if (!startExists())
                {
                    btn.BackColor = COLOR_START_BUTTON;
                    btn.Text = BUTTON_START;
                }
                // If end square does not exist, set to end
                //else if (!finishExists())
                //{
                //    btn.BackColor = COLOR_FINISH_BUTTON;
                //    btn.Text = BUTTON_FINISH;
                //}
                else if (numDest < DEST_MAX)
                {
                    btn.BackColor = COLOR_FINISH_BUTTON;
                    btn.Text = DEST_CHAR[numDest];
                    destinations.Add(btn.Text, getLoc(btn.Text));
                    this.numDest++;
                }
                else
                {
                    btn.BackColor = COLOR_OPEN_BUTTON;
                    btn.Text = BUTTON_OPEN;
                }
            }
            // If start, set to finish
            // Else, clear
            else if (btn.Text == BUTTON_START)
            {
                if (numDest < DEST_MAX)
                {
                    btn.BackColor = COLOR_FINISH_BUTTON;
                    btn.Text = DEST_CHAR[numDest];
                    destinations.Add(btn.Text, getLoc(btn.Text));
                    this.numDest++;
                }
                else
                {
                    btn.BackColor = COLOR_OPEN_BUTTON;
                    btn.Text = BUTTON_OPEN;
                }
            }
            // If finish, clear
            else
            {
                destinations.Remove(btn.Text);
                btn.BackColor = COLOR_OPEN_BUTTON;
                btn.Text = BUTTON_OPEN;
                this.numDest--;
            }
        }


    }

    // Class to find the shortest path to a destination, using A* method
    public class PathFinder
    {
        //static int dir = 8;
        //static int[] dx = { 1, 1, 0, -1, -1, -1, 0, 1 };
        //static int[] dy = { 0, 1, 1, 1, 0, -1, -1, -1 };

        // Use orthogonal directions only
        static int dir = 4;
        static int[] dx = { 1, 0, -1, 0 };
        static int[] dy = { 0, 1, 0, -1 };

        // Inner class to represent a location node
        private class Node
        {
            // current position
            private int xPos;
            private int yPos;
            // total distance already travelled to reach the node
            private int level;
            // priority=level+remaining distance estimate
            private int priority;  // smaller: higher priority

            public Node(int xp, int yp, int d, int p)
            { xPos = xp; yPos = yp; level = d; priority = p; }

            public int getxPos() { return xPos; }
            public int getyPos() { return yPos; }
            public int getLevel() { return level; }
            public int getPriority() { return priority; }

            public void updatePriority(int xDest, int yDest)
            {
                priority = level + estimate(xDest, yDest) * 10; //A*
            }

            // give better priority to going strait instead of diagonally
            public void nextLevel(int i) // i: direction
            {
                level += (i % 2 == 0 ? 10 : 14);
            }

            // Estimation function for the remaining distance to the goal.
            public int estimate(int xDest, int yDest)
            {
                int xd, yd, d;
                xd = xDest - xPos;
                yd = yDest - yPos;

                // Euclidian Distance
                d = (int)(Math.Sqrt(xd * xd + yd * yd));

                // Manhattan distance
                //d=abs(xd)+abs(yd);

                // Chebyshev distance
                //d=max(abs(xd), abs(yd));

                return (d);
            }
            public bool compare(Node other)
            {
                return this.getPriority() > other.getPriority();
            }
        }

        // Solve map using destination marked with button
        public static List<int> solve(Map map)
        {
            return solve(map, map.getFinishLoc());
        }

        // Solve map using destination passed as argument
        public static List<int> solve(Map map, int[] destination)
        {
            int n = map.getColNum(); // horizontal
            int m = map.getRowNum(); // vertical

            int xStart = map.getStartLoc()[0];
            int yStart = map.getStartLoc()[1];
            int xFinish = destination[0];
            int yFinish = destination[1];

            int[] int_map = map.getIntMap();
            int[] closed_nodes_map = new int[n * m];
            int[] open_nodes_map = new int[n * m];
            int[] dir_map = new int[n * m];

            //srand(time(NULL));

            List<Node>[] pq = new List<Node>[2];// list of open (not-yet-tried) nodes
            pq[0] = new List<Node>();
            pq[1] = new List<Node>();
            int pqi; // pq index
            Node n0;
            Node m0;
            int i, j, x, y, xdx, ydy;
            x = 0;
            char c;
            pqi = 0;

            // reset the node maps
            for (y = 0; y < m; y++)
            {
                for (x = 0; x < n; x++)
                {
                    closed_nodes_map[x * m + y] = 0;
                    open_nodes_map[x * m + y] = 0;
                }
            }

            // create the start node and push into list of open nodes
            n0 = new Node(xStart, yStart, 0, 0);
            n0.updatePriority(xFinish, yFinish);
            pq[pqi].Add(n0);
            open_nodes_map[xStart * m + yStart] = n0.getPriority(); // mark it on the open nodes map

            // A* search
            while (pq[pqi].Any())
            {
                // get the current node w/ the highest priority
                // from the list of open nodes
                pq[pqi].Sort((p1, p2) =>
                {
                    if (p1.getPriority() > p2.getPriority())
                    {
                        return 1;
                    }
                    else if (p1.getPriority() < p2.getPriority())
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                });
                n0 = new Node(pq[pqi].First().getxPos(), pq[pqi].First().getyPos(),
                             pq[pqi].First().getLevel(), pq[pqi].First().getPriority());

                x = n0.getxPos(); y = n0.getyPos();

                pq[pqi].RemoveAt(0); // remove the node from the open list
                open_nodes_map[x * m + y] = 0;
                // mark it on the closed nodes map
                closed_nodes_map[x * m + y] = 1;

                // quit searching when the goal state is reached
                //if((*n0).estimate(xFinish, yFinish) == 0)
                if (x == xFinish && y == yFinish)
                {
                    // generate the path from finish to start
                    // by following the directions
                    List<int> path = new List<int>();
                    while (!(x == xStart && y == yStart))
                    {
                        j = dir_map[x * m + y];
                        c = (char)('0' + (j + dir / 2) % dir);
                        path.Insert(0, (j + dir / 2) % dir);
                        x += dx[j];
                        y += dy[j];
                    }

                    // garbage collection
                    //delete n0;
                    // empty the leftover nodes
                    while (pq[pqi].Any()) pq[pqi].RemoveAt(0);
                    return path;
                }

                // generate moves (child nodes) in all possible directions
                for (i = 0; i < dir; i++)
                {
                    xdx = x + dx[i]; ydy = y + dy[i];

                    if (!(xdx < 0 || xdx > n - 1 || ydy < 0 || ydy > m - 1 || int_map[xdx * m + ydy] == 1
                        || closed_nodes_map[xdx * m + ydy] == 1))
                    {
                        // generate a child node
                        m0 = new Node(xdx, ydy, n0.getLevel(), n0.getPriority());
                        m0.nextLevel(i);
                        m0.updatePriority(xFinish, yFinish);

                        // if it is not in the open list then add into that
                        if (open_nodes_map[xdx * m + ydy] == 0)
                        {
                            open_nodes_map[xdx * m + ydy] = m0.getPriority();
                            pq[pqi].Add(m0);
                            // mark its parent node direction
                            dir_map[xdx * m + ydy] = (i + dir / 2) % dir;
                        }
                        else if (open_nodes_map[xdx * m + ydy] > m0.getPriority())
                        {
                            // update the priority info
                            open_nodes_map[xdx * m + ydy] = m0.getPriority();
                            // update the parent direction info
                            dir_map[xdx * m + ydy] = (i + dir / 2) % dir;

                            // replace the node
                            // by emptying one pq to the other one
                            // except the node to be replaced will be ignored
                            // and the new node will be pushed in instead

                            pq[pqi].Sort((p1, p2) =>
                            {
                                if (p1.getPriority() > p2.getPriority())
                                {
                                    return 1;
                                }
                                else if (p1.getPriority() < p2.getPriority())
                                {
                                    return -1;
                                }
                                else
                                {
                                    return 0;
                                }
                            });

                            while (!(pq[pqi].First().getxPos() == xdx &&
                                   pq[pqi].First().getyPos() == ydy))
                            {
                                pq[1 - pqi].Add(pq[pqi].First());
                                pq[pqi].RemoveAt(0);
                            }
                            pq[pqi].RemoveAt(0); // remove the wanted node

                            // TODO: Sort?
                            // empty the larger size pq to the smaller one
                            if (pq[pqi].Count() > pq[1 - pqi].Count()) pqi = 1 - pqi;
                            while (pq[pqi].Any())
                            {
                                pq[1 - pqi].Add(pq[pqi].First());
                                pq[pqi].RemoveAt(0);
                            }
                            pqi = 1 - pqi;
                            pq[pqi].Add(m0); // add the better node instead
                        }
                        //else delete m0; // garbage collection
                    }
                }
                //delete n0; // garbage collection
            }
            return new List<int>(); // no route found
        }
    }

    // Class of directions, with multiplier for distance and angle
    public class DirectionCommand
    {
        // [5][4][3]
        // [6][ ][2]
        // [7][0][1]
        public static readonly float ROOT_TWO = 1.414213562F;
        public static readonly DirectionCommand S = new DirectionCommand("S", 0, 1, 180);
        public static readonly DirectionCommand SE = new DirectionCommand("SE", 1, ROOT_TWO, 135);
        public static readonly DirectionCommand E = new DirectionCommand("E", 2, 1, 90);
        public static readonly DirectionCommand NE = new DirectionCommand("NE", 3, ROOT_TWO, 45);
        public static readonly DirectionCommand N = new DirectionCommand("N", 4, 1, 0);
        public static readonly DirectionCommand NW = new DirectionCommand("NW", 5, ROOT_TWO, 315);
        public static readonly DirectionCommand W = new DirectionCommand("W", 6, 1, 270);
        public static readonly DirectionCommand SW = new DirectionCommand("S", 7, ROOT_TWO, 225);
        public static readonly DirectionCommand NONE = new DirectionCommand("NN", 8, 0, 0);

        public static IEnumerable<DirectionCommand> Values
        {
            get
            {
                yield return S;
                yield return SE;
                yield return E;
                yield return NE;
                yield return N;
                yield return NW;
                yield return W;
                yield return SW;
                yield return NONE;
            }
        }

        // Get the command object corresponding to the code
        public static DirectionCommand getCommand(short code)
        {
            foreach (DirectionCommand dc in DirectionCommand.Values)
            {
                if (dc.Code == code)
                {
                    return dc;
                }
            }
            return NONE;
        }

        // Find the reverse command
        public static DirectionCommand reverse(DirectionCommand command)
        {
            switch (command.Code)
            {
                case 0:
                    return N;
                case 1:
                    return NW;
                case 2:
                    return W;
                case 3:
                    return SW;
                case 4:
                    return S;
                case 5:
                    return SE;
                case 6:
                    return E;
                case 7:
                    return NE;
                default:
                    return NONE;
            }
        }

        private readonly String name;       // String name of direction
        private readonly short code;        // code value for direction
        private readonly float angle;      // angle value
        private readonly double multiplier; // multiplier of distance

        DirectionCommand(string name, short code, double multiplier, float angle)
        {
            this.name = name;
            this.code = code;
            this.angle = angle;
            this.multiplier = multiplier;
        }

        public string Name { get { return name; } }

        public short Code { get { return code; } }

        public float Angle { get { return angle; } }

        public double Multiplier { get { return multiplier; } }

        public override string ToString()
        {
            return name;
        }
    }
}