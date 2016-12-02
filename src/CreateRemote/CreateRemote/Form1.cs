using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;
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
        public SerialPort port;
        public Int16 left = 20; // default speed left wheel
        public Int16 right = 20; // default speed right wheel
        private bool ProcessingSensorDataPacket = false;
        public bool raw_sensor_debug = false;
        // public volatile ushort travelDistance = 10;

        // Constants
        public volatile short d45 = 92; // turning distance for 45 degrees when both wheels are turning
        public const short d_block = 200;
        public const short d_block_diag = (short)(d_block * 1.414);

        // variables for control system
        public volatile short new_l, old_l, l, base_l, new_r, old_r, r, base_r;
        public volatile short lr_difference;
        public volatile short angle, new_angle;
        public volatile short x, y;
        public volatile short new_distance;

        public volatile short temp_l, temp_r;
        

        // Flags
        public volatile bool setBase = true;
        public volatile bool turning = false;
        public volatile bool move_forward = false;
        public volatile bool turn_right = false;        
        public volatile bool turn_left = false;
        public volatile bool waitForCommand = true;

        // Serial Buffer
        public volatile byte[] buf = new byte[6];

        public Form1()
        {
            InitializeComponent();
            // Serial Port Open Code - used to communicate with iRobot Create robot
            port = new SerialPort("COM223", 115200, Parity.None, 8, StopBits.One);
            port.Handshake = Handshake.None;
            port.ReadTimeout = 60000;
            port.WriteTimeout = 60000;
            try // Just in case .NET CF not updated
            {
                port.Open();
            }
            catch (Exception com_open_except) { }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // Start Button - send start and safe mode, start streaming sensor data
        private void button1_Click(object sender, EventArgs e)
        {
            port.Write(new byte[] { (byte)Commands.Start, (byte)Commands.SafeMode }, 0, 2);
            Thread.Sleep(100);
            System.Diagnostics.Debug.WriteLine("Requesting data from bot");
            port.Write(new byte[] { (byte)Commands.Stream, 2, (byte)Sensors.LeftEncoderCounts,
            (byte)Sensors.RightEncoderCounts}, 0, 4);
            Thread.Sleep(200);
            
            // Attaching two handlers because it works better for some reason
            port.DataReceived += new
            SerialDataReceivedEventHandler(port_DataReceived);
            port.DataReceived += new
            SerialDataReceivedEventHandler(port_DataReceived);
            
        }
        // Stop Button - turn off drive motors
        private void button3_Click(object sender, EventArgs e)
        {
            port.Write(new byte[] { (byte)Commands.DriveDirect, (byte)0, (byte)0, (byte)0, (byte)0 }, 0, 5);
        }
        // Forward Button - turn on drive motors
        private void button2_Click(object sender, EventArgs e)
        {
            port.Write(new byte[] { (byte)Commands.DriveDirect,
                (byte)(right >> 8), (byte)right,
                (byte)(left >> 8), (byte)left }, 0, 5);
        }
        // Reverse Button - reverse drive motors
        private void button6_Click(object sender, EventArgs e)
        {
            port.Write(new byte[] { (byte)Commands.DriveDirect,
                (byte)(-right >> 8), (byte)-right,
                (byte)(-left >> 8), (byte)-left }, 0, 5);
        }
        // Left Button - drive motors set to rotate to left
        private void button4_Click(object sender, EventArgs e)
        {
            port.Write(new byte[] { (byte)Commands.DriveDirect,
                (byte)(right >> 8), (byte)right,
                (byte)(-left >> 8), (byte)-left }, 0, 5);
        }
        // Right Button - drive motors set to rotate to right
        private void button5_Click(object sender, EventArgs e)
        {
            port.Write(new byte[] { (byte)Commands.DriveDirect,
                (byte)(-right >> 8), (byte)-right,
                (byte)(left >> 8), (byte)left }, 0, 5);
        }
        // Charger Button - return to charger using IR beacons
        private void button7_Click(object sender, EventArgs e)
        {
            port.Write(new byte[] { (byte)Commands.Demo, (byte)1 }, 0, 2);
        }
        // Play Song Button - define and play a song on robot
        private void button8_Click(object sender, EventArgs e)
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
        private void trackBar1_Scroll(object sender, EventArgs e)
        {

            left = (Int16)trackBar1.Value;
            right = (Int16)trackBar1.Value;
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
                    //byte[] buf = new byte[port.ReadByte()];
                    //port.Read(buf, 0, buf.Length);

                    port.ReadByte(); // skip first byte
                    port.Read(buf, 0, 6);


                    // grab final checksum, validate checksum, and update sensor data
                    //if (validData(buf, (byte)(port.ReadByte() + (buf.Length + 19))))
                    //{ // Display raw sensor data on console for debug?
                    //    //if (raw_sensor_debug)
                    //    //{
                    //    //    foreach (byte b in buf) Console.Write(b + " ");
                    //    //    Console.WriteLine();
                    //    //};

                    //     //System.Diagnostics.Debug.WriteLine("Received data: " + travelDistance);
                    //}
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
        private void checkBox6_CheckStateChanged(object sender,
        EventArgs e)
        {
            raw_sensor_debug = checkBox6.Checked;
        }
        // Seek Dock
        private void button9_Click(object sender, EventArgs e)
        {
            port.Write(new byte[] { (byte)Commands.CoverandDock }, 0, 1);
        }

        // 0 degree and 1 distance
        private void button10_Click(object sender, EventArgs e)
        {
            temp_l = l;
            temp_r = r;
            move_forward = true;
        }
        // 45 degree and sqrt(2) distance
        private void button11_Click(object sender, EventArgs e)
        {
            turn_right = true;
            new_angle = 45;
        }
        // 90 degree and 1 distance
        private void button12_Click(object sender, EventArgs e)
        {

        }
        // 135 degree and sqrt(2) distance
        private void button13_Click(object sender, EventArgs e)
        {

        }
        // 180 degree and 1 distance
        private void button14_Click(object sender, EventArgs e)
        {

        }
        // 225 degree and sqrt(2) distance
        private void button15_Click(object sender, EventArgs e)
        {

        }
        // 270 degree and 1 distance
        private void button16_Click(object sender, EventArgs e)
        {

        }
        // 315 degree and sqrt(2) distance
        private void button17_Click(object sender, EventArgs e)
        {

        }


 

        private void label2_Click(object sender, EventArgs e)
        {

        }

 
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            raw_sensor_debug = checkBox6.Checked;
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Compute the new encoder values
            new_l = (short)((buf[1] << 8) | buf[2]);
            new_r = (short)((buf[4] << 8) | buf[5]);

            // Continuously updating l and r values
            old_l = (short)((new_l) * 72 * 3.14 / 508.8);
            old_r = (short)((new_r) * 72 * 3.14 / 508.8);

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

            // Motion Implementation

            if (turn_right)
            {
                if ((new_angle - angle) > 0)
                {
                    turnRight();
                }
                else
                {
                    stop();
                    Thread.Sleep(1000);
                    move_forward = true;
                    turn_right = false;
                    temp_l = l;
                    
                }
            }

            if (turn_left)
            {
                if ((new_angle - angle) < 0)
                {
                    turnLeft();
                }
                else
                {
                    stop();
                    Thread.Sleep(1000);
                    move_forward = true;
                    turn_left = false;
                    temp_l = l;
                }
            }

            if (move_forward)
            {
                if ((temp_l + new_distance) > l)
                {
                    moveForward();
                }
                else
                {
                    stop();
                    Thread.Sleep(1000);
                    move_forward = false;
                    
                }
            }

            // Print encoder information
            textBox1.Text = (base_l).ToString();
            textBox2.Text = (base_r).ToString();
            textBox3.Text = (l).ToString();
            textBox4.Text = (r).ToString();
            textBox5.Text = (angle).ToString();
            textBox6.Text = (lr_difference).ToString();

            textBox9.Text = "" + temp_l;
        }

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

        private void moveForward()
        {
            port.Write(new byte[] { (byte)Commands.DriveDirect, (byte)(right >> 8), (byte)right, (byte)(left >> 8), (byte)left }, 0, 5);
        }

        private void moveBackward()
        {
            port.Write(new byte[] { (byte)Commands.DriveDirect, (byte)(-right >> 8), (byte)-right, (byte)(-left >> 8), (byte)-left }, 0, 5);
        }

        private void computeAngle() {
            lr_difference = (short)(l - r);
            angle = (short)((float)((l - r) / 2.0) / (float)d45 * 45.0);   // this angle relative to left wheel motion

        }

        // moveTo function only sets certain flags
        // The rest will be handled by timer interrupt
        private void moveTo(short new_angle, short new_distance)
        {
            int difference = new_angle - angle;
            if (difference > 0)
            {
                turn_right = true;
            }
            else if (difference < 0)
            {
                turn_left = true;
            }
            else
            {
                move_forward = true;
                temp_l = l;
            }
 
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        // Command Input
        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }
        // Command Send Button
        private void button18_Click(object sender, EventArgs e)
        {
            // Parse 1 command
            short x = short.Parse(textBox7.Text);

            // Range check
            if ((x > 7) || (x < 0))
            {
                textBox8.Text = "invalid";
                return;
            }

            new_angle = (short)(45 * x);
            if (x == 0 || x == 2 || x == 4 || x == 6)
            {
                new_distance = d_block;
            }
            else
            {
                new_distance = d_block_diag;
            }

            textBox8.Text = "" + new_angle + " " + new_distance;

            moveTo(new_angle, new_distance);
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

    }
}