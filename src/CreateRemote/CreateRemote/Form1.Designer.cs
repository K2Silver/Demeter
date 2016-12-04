namespace CreateRemote
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnForward = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnReverse = new System.Windows.Forms.Button();
            this.btnCharger = new System.Windows.Forms.Button();
            this.btnSong = new System.Windows.Forms.Button();
            this.chkboxDisplayRaw = new System.Windows.Forms.CheckBox();
            this.trackBarSpeed = new System.Windows.Forms.TrackBar();
            this.labelSpeed = new System.Windows.Forms.Label();
            this.btnDock = new System.Windows.Forms.Button();
            this.btnNorth = new System.Windows.Forms.Button();
            this.btnNorthEast = new System.Windows.Forms.Button();
            this.btnEast = new System.Windows.Forms.Button();
            this.btnSouthEast = new System.Windows.Forms.Button();
            this.btnSouth = new System.Windows.Forms.Button();
            this.btnSouthWest = new System.Windows.Forms.Button();
            this.btnWest = new System.Windows.Forms.Button();
            this.buttonNorthWest = new System.Windows.Forms.Button();
            this.textBoxBaseL = new System.Windows.Forms.TextBox();
            this.groupBoxInfo = new System.Windows.Forms.GroupBox();
            this.labelAngleCalc = new System.Windows.Forms.Label();
            this.textBoxAngleCalc = new System.Windows.Forms.TextBox();
            this.labelReceived = new System.Windows.Forms.Label();
            this.textBoxReceived = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.labelCommand = new System.Windows.Forms.Label();
            this.textBoxCommand = new System.Windows.Forms.TextBox();
            this.labelTurningDistance = new System.Windows.Forms.Label();
            this.labelAngle = new System.Windows.Forms.Label();
            this.textBoxTurningDistance = new System.Windows.Forms.TextBox();
            this.textBoxAngle = new System.Windows.Forms.TextBox();
            this.labelCurrentR = new System.Windows.Forms.Label();
            this.labelCurrentL = new System.Windows.Forms.Label();
            this.textBoxCurrentR = new System.Windows.Forms.TextBox();
            this.textBoxCurrentL = new System.Windows.Forms.TextBox();
            this.labelBaseR = new System.Windows.Forms.Label();
            this.labelBaseL = new System.Windows.Forms.Label();
            this.textBoxBaseR = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.labelTempL = new System.Windows.Forms.Label();
            this.textBoxTempL = new System.Windows.Forms.TextBox();
            this.comboBoxCOM = new System.Windows.Forms.ComboBox();
            this.labelCOM = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnSolve = new System.Windows.Forms.Button();
            this.txtboxColumn = new System.Windows.Forms.TextBox();
            this.txtboxRow = new System.Windows.Forms.TextBox();
            this.labelColumn = new System.Windows.Forms.Label();
            this.labelRow = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnRunPython = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpeed)).BeginInit();
            this.groupBoxInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(200, 78);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(112, 35);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnForward
            // 
            this.btnForward.Location = new System.Drawing.Point(200, 162);
            this.btnForward.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(112, 35);
            this.btnForward.TabIndex = 1;
            this.btnForward.Text = "Forward";
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(200, 254);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(112, 35);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.Location = new System.Drawing.Point(44, 254);
            this.btnLeft.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(112, 35);
            this.btnLeft.TabIndex = 3;
            this.btnLeft.Text = "Left";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnRight
            // 
            this.btnRight.Location = new System.Drawing.Point(362, 254);
            this.btnRight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(112, 35);
            this.btnRight.TabIndex = 4;
            this.btnRight.Text = "Right";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnReverse
            // 
            this.btnReverse.Location = new System.Drawing.Point(200, 351);
            this.btnReverse.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnReverse.Name = "btnReverse";
            this.btnReverse.Size = new System.Drawing.Size(112, 35);
            this.btnReverse.TabIndex = 5;
            this.btnReverse.Text = "Reverse";
            this.btnReverse.UseVisualStyleBackColor = true;
            this.btnReverse.Click += new System.EventHandler(this.btnReverse_Click);
            // 
            // btnCharger
            // 
            this.btnCharger.Location = new System.Drawing.Point(44, 440);
            this.btnCharger.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCharger.Name = "btnCharger";
            this.btnCharger.Size = new System.Drawing.Size(112, 35);
            this.btnCharger.TabIndex = 6;
            this.btnCharger.Text = "Charger";
            this.btnCharger.UseVisualStyleBackColor = true;
            this.btnCharger.Click += new System.EventHandler(this.btnCharger_Click);
            // 
            // btnSong
            // 
            this.btnSong.Location = new System.Drawing.Point(362, 440);
            this.btnSong.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSong.Name = "btnSong";
            this.btnSong.Size = new System.Drawing.Size(112, 35);
            this.btnSong.TabIndex = 7;
            this.btnSong.Text = "Song";
            this.btnSong.UseVisualStyleBackColor = true;
            this.btnSong.Click += new System.EventHandler(this.btnSong_Click);
            // 
            // chkboxDisplayRaw
            // 
            this.chkboxDisplayRaw.AutoSize = true;
            this.chkboxDisplayRaw.Location = new System.Drawing.Point(142, 542);
            this.chkboxDisplayRaw.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkboxDisplayRaw.Name = "chkboxDisplayRaw";
            this.chkboxDisplayRaw.Size = new System.Drawing.Size(238, 24);
            this.chkboxDisplayRaw.TabIndex = 13;
            this.chkboxDisplayRaw.Text = "Display Raw Sensor Packets";
            this.chkboxDisplayRaw.UseVisualStyleBackColor = true;
            this.chkboxDisplayRaw.CheckedChanged += new System.EventHandler(this.checkBoxRawSensor_CheckedChanged);
            // 
            // trackBarSpeed
            // 
            this.trackBarSpeed.Location = new System.Drawing.Point(524, 78);
            this.trackBarSpeed.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.trackBarSpeed.Maximum = 500;
            this.trackBarSpeed.Minimum = 50;
            this.trackBarSpeed.Name = "trackBarSpeed";
            this.trackBarSpeed.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarSpeed.Size = new System.Drawing.Size(69, 160);
            this.trackBarSpeed.SmallChange = 10;
            this.trackBarSpeed.TabIndex = 14;
            this.trackBarSpeed.TickFrequency = 10;
            this.trackBarSpeed.Value = 50;
            this.trackBarSpeed.Scroll += new System.EventHandler(this.trackBarSpeed_Scroll);
            // 
            // labelSpeed
            // 
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.Location = new System.Drawing.Point(519, 40);
            this.labelSpeed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(56, 20);
            this.labelSpeed.TabIndex = 15;
            this.labelSpeed.Text = "Speed";
            // 
            // btnDock
            // 
            this.btnDock.Location = new System.Drawing.Point(200, 440);
            this.btnDock.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDock.Name = "btnDock";
            this.btnDock.Size = new System.Drawing.Size(112, 35);
            this.btnDock.TabIndex = 16;
            this.btnDock.Text = "Dock";
            this.btnDock.UseVisualStyleBackColor = true;
            this.btnDock.Click += new System.EventHandler(this.btnDock_Click);
            // 
            // btnNorth
            // 
            this.btnNorth.Location = new System.Drawing.Point(780, 32);
            this.btnNorth.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnNorth.Name = "btnNorth";
            this.btnNorth.Size = new System.Drawing.Size(112, 35);
            this.btnNorth.TabIndex = 17;
            this.btnNorth.Text = "N";
            this.btnNorth.UseVisualStyleBackColor = true;
            this.btnNorth.Click += new System.EventHandler(this.btnNorth_Click);
            // 
            // btnNorthEast
            // 
            this.btnNorthEast.Location = new System.Drawing.Point(921, 32);
            this.btnNorthEast.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnNorthEast.Name = "btnNorthEast";
            this.btnNorthEast.Size = new System.Drawing.Size(112, 35);
            this.btnNorthEast.TabIndex = 18;
            this.btnNorthEast.Text = "NE";
            this.btnNorthEast.UseVisualStyleBackColor = true;
            this.btnNorthEast.Click += new System.EventHandler(this.btnNorthEast_Click);
            // 
            // btnEast
            // 
            this.btnEast.Location = new System.Drawing.Point(921, 125);
            this.btnEast.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnEast.Name = "btnEast";
            this.btnEast.Size = new System.Drawing.Size(112, 35);
            this.btnEast.TabIndex = 19;
            this.btnEast.Text = "E";
            this.btnEast.UseVisualStyleBackColor = true;
            this.btnEast.Click += new System.EventHandler(this.btnEast_Click);
            // 
            // btnSouthEast
            // 
            this.btnSouthEast.Location = new System.Drawing.Point(921, 222);
            this.btnSouthEast.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSouthEast.Name = "btnSouthEast";
            this.btnSouthEast.Size = new System.Drawing.Size(112, 35);
            this.btnSouthEast.TabIndex = 20;
            this.btnSouthEast.Text = "SE";
            this.btnSouthEast.UseVisualStyleBackColor = true;
            this.btnSouthEast.Click += new System.EventHandler(this.btnSouthEast_Click);
            // 
            // btnSouth
            // 
            this.btnSouth.Location = new System.Drawing.Point(780, 222);
            this.btnSouth.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSouth.Name = "btnSouth";
            this.btnSouth.Size = new System.Drawing.Size(112, 35);
            this.btnSouth.TabIndex = 21;
            this.btnSouth.Text = "S";
            this.btnSouth.UseVisualStyleBackColor = true;
            this.btnSouth.Click += new System.EventHandler(this.btnSouth_Click);
            // 
            // btnSouthWest
            // 
            this.btnSouthWest.Location = new System.Drawing.Point(636, 222);
            this.btnSouthWest.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSouthWest.Name = "btnSouthWest";
            this.btnSouthWest.Size = new System.Drawing.Size(112, 35);
            this.btnSouthWest.TabIndex = 22;
            this.btnSouthWest.Text = "SW";
            this.btnSouthWest.UseVisualStyleBackColor = true;
            this.btnSouthWest.Click += new System.EventHandler(this.btnSouthWest_Click);
            // 
            // btnWest
            // 
            this.btnWest.Location = new System.Drawing.Point(636, 125);
            this.btnWest.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnWest.Name = "btnWest";
            this.btnWest.Size = new System.Drawing.Size(112, 35);
            this.btnWest.TabIndex = 23;
            this.btnWest.Text = "W";
            this.btnWest.UseVisualStyleBackColor = true;
            this.btnWest.Click += new System.EventHandler(this.btnWest_Click);
            // 
            // buttonNorthWest
            // 
            this.buttonNorthWest.Location = new System.Drawing.Point(636, 32);
            this.buttonNorthWest.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonNorthWest.Name = "buttonNorthWest";
            this.buttonNorthWest.Size = new System.Drawing.Size(112, 35);
            this.buttonNorthWest.TabIndex = 24;
            this.buttonNorthWest.Text = "NW";
            this.buttonNorthWest.UseVisualStyleBackColor = true;
            this.buttonNorthWest.Click += new System.EventHandler(this.btnNorthWest_Click);
            // 
            // textBoxBaseL
            // 
            this.textBoxBaseL.Location = new System.Drawing.Point(128, 42);
            this.textBoxBaseL.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxBaseL.Name = "textBoxBaseL";
            this.textBoxBaseL.ReadOnly = true;
            this.textBoxBaseL.Size = new System.Drawing.Size(148, 26);
            this.textBoxBaseL.TabIndex = 25;
            // 
            // groupBoxInfo
            // 
            this.groupBoxInfo.Controls.Add(this.labelAngleCalc);
            this.groupBoxInfo.Controls.Add(this.textBoxAngleCalc);
            this.groupBoxInfo.Controls.Add(this.labelReceived);
            this.groupBoxInfo.Controls.Add(this.textBoxReceived);
            this.groupBoxInfo.Controls.Add(this.btnSend);
            this.groupBoxInfo.Controls.Add(this.labelCommand);
            this.groupBoxInfo.Controls.Add(this.textBoxCommand);
            this.groupBoxInfo.Controls.Add(this.labelTurningDistance);
            this.groupBoxInfo.Controls.Add(this.labelAngle);
            this.groupBoxInfo.Controls.Add(this.textBoxTurningDistance);
            this.groupBoxInfo.Controls.Add(this.textBoxAngle);
            this.groupBoxInfo.Controls.Add(this.labelCurrentR);
            this.groupBoxInfo.Controls.Add(this.labelCurrentL);
            this.groupBoxInfo.Controls.Add(this.textBoxCurrentR);
            this.groupBoxInfo.Controls.Add(this.textBoxCurrentL);
            this.groupBoxInfo.Controls.Add(this.labelBaseR);
            this.groupBoxInfo.Controls.Add(this.labelBaseL);
            this.groupBoxInfo.Controls.Add(this.textBoxBaseR);
            this.groupBoxInfo.Controls.Add(this.textBoxBaseL);
            this.groupBoxInfo.Location = new System.Drawing.Point(636, 292);
            this.groupBoxInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxInfo.Name = "groupBoxInfo";
            this.groupBoxInfo.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxInfo.Size = new System.Drawing.Size(462, 452);
            this.groupBoxInfo.TabIndex = 26;
            this.groupBoxInfo.TabStop = false;
            this.groupBoxInfo.Text = "Info";
            // 
            // labelAngleCalc
            // 
            this.labelAngleCalc.AutoSize = true;
            this.labelAngleCalc.Location = new System.Drawing.Point(10, 286);
            this.labelAngleCalc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAngleCalc.Name = "labelAngleCalc";
            this.labelAngleCalc.Size = new System.Drawing.Size(76, 20);
            this.labelAngleCalc.TabIndex = 42;
            this.labelAngleCalc.Text = "anglecalc";
            // 
            // textBoxAngleCalc
            // 
            this.textBoxAngleCalc.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxAngleCalc.Location = new System.Drawing.Point(128, 282);
            this.textBoxAngleCalc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxAngleCalc.Name = "textBoxAngleCalc";
            this.textBoxAngleCalc.ReadOnly = true;
            this.textBoxAngleCalc.Size = new System.Drawing.Size(148, 26);
            this.textBoxAngleCalc.TabIndex = 41;
            // 
            // labelReceived
            // 
            this.labelReceived.AutoSize = true;
            this.labelReceived.Location = new System.Drawing.Point(10, 366);
            this.labelReceived.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelReceived.Name = "labelReceived";
            this.labelReceived.Size = new System.Drawing.Size(68, 20);
            this.labelReceived.TabIndex = 40;
            this.labelReceived.Text = "received";
            // 
            // textBoxReceived
            // 
            this.textBoxReceived.Location = new System.Drawing.Point(128, 362);
            this.textBoxReceived.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxReceived.Name = "textBoxReceived";
            this.textBoxReceived.ReadOnly = true;
            this.textBoxReceived.Size = new System.Drawing.Size(326, 26);
            this.textBoxReceived.TabIndex = 39;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(144, 408);
            this.btnSend.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(112, 35);
            this.btnSend.TabIndex = 27;
            this.btnSend.Text = "send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // labelCommand
            // 
            this.labelCommand.AutoSize = true;
            this.labelCommand.Location = new System.Drawing.Point(9, 323);
            this.labelCommand.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCommand.Name = "labelCommand";
            this.labelCommand.Size = new System.Drawing.Size(79, 20);
            this.labelCommand.TabIndex = 38;
            this.labelCommand.Text = "command";
            // 
            // textBoxCommand
            // 
            this.textBoxCommand.Location = new System.Drawing.Point(128, 323);
            this.textBoxCommand.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxCommand.Name = "textBoxCommand";
            this.textBoxCommand.Size = new System.Drawing.Size(148, 26);
            this.textBoxCommand.TabIndex = 37;
            this.textBoxCommand.TextChanged += new System.EventHandler(this.textBoxComand_TextChanged);
            // 
            // labelTurningDistance
            // 
            this.labelTurningDistance.AutoSize = true;
            this.labelTurningDistance.Location = new System.Drawing.Point(10, 246);
            this.labelTurningDistance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTurningDistance.Name = "labelTurningDistance";
            this.labelTurningDistance.Size = new System.Drawing.Size(121, 20);
            this.labelTurningDistance.TabIndex = 36;
            this.labelTurningDistance.Text = "turningDistance";
            // 
            // labelAngle
            // 
            this.labelAngle.AutoSize = true;
            this.labelAngle.Location = new System.Drawing.Point(10, 206);
            this.labelAngle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAngle.Name = "labelAngle";
            this.labelAngle.Size = new System.Drawing.Size(48, 20);
            this.labelAngle.TabIndex = 35;
            this.labelAngle.Text = "angle";
            // 
            // textBoxTurningDistance
            // 
            this.textBoxTurningDistance.Location = new System.Drawing.Point(128, 242);
            this.textBoxTurningDistance.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxTurningDistance.Name = "textBoxTurningDistance";
            this.textBoxTurningDistance.ReadOnly = true;
            this.textBoxTurningDistance.Size = new System.Drawing.Size(148, 26);
            this.textBoxTurningDistance.TabIndex = 34;
            // 
            // textBoxAngle
            // 
            this.textBoxAngle.Location = new System.Drawing.Point(128, 202);
            this.textBoxAngle.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxAngle.Name = "textBoxAngle";
            this.textBoxAngle.ReadOnly = true;
            this.textBoxAngle.Size = new System.Drawing.Size(148, 26);
            this.textBoxAngle.TabIndex = 33;
            // 
            // labelCurrentR
            // 
            this.labelCurrentR.AutoSize = true;
            this.labelCurrentR.Location = new System.Drawing.Point(10, 166);
            this.labelCurrentR.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCurrentR.Name = "labelCurrentR";
            this.labelCurrentR.Size = new System.Drawing.Size(14, 20);
            this.labelCurrentR.TabIndex = 32;
            this.labelCurrentR.Text = "r";
            // 
            // labelCurrentL
            // 
            this.labelCurrentL.AutoSize = true;
            this.labelCurrentL.Location = new System.Drawing.Point(10, 132);
            this.labelCurrentL.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCurrentL.Name = "labelCurrentL";
            this.labelCurrentL.Size = new System.Drawing.Size(12, 20);
            this.labelCurrentL.TabIndex = 31;
            this.labelCurrentL.Text = "l";
            // 
            // textBoxCurrentR
            // 
            this.textBoxCurrentR.Location = new System.Drawing.Point(128, 162);
            this.textBoxCurrentR.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxCurrentR.Name = "textBoxCurrentR";
            this.textBoxCurrentR.ReadOnly = true;
            this.textBoxCurrentR.Size = new System.Drawing.Size(148, 26);
            this.textBoxCurrentR.TabIndex = 30;
            // 
            // textBoxCurrentL
            // 
            this.textBoxCurrentL.Location = new System.Drawing.Point(128, 122);
            this.textBoxCurrentL.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxCurrentL.Name = "textBoxCurrentL";
            this.textBoxCurrentL.ReadOnly = true;
            this.textBoxCurrentL.Size = new System.Drawing.Size(148, 26);
            this.textBoxCurrentL.TabIndex = 29;
            // 
            // labelBaseR
            // 
            this.labelBaseR.AutoSize = true;
            this.labelBaseR.Location = new System.Drawing.Point(10, 86);
            this.labelBaseR.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelBaseR.Name = "labelBaseR";
            this.labelBaseR.Size = new System.Drawing.Size(58, 20);
            this.labelBaseR.TabIndex = 28;
            this.labelBaseR.Text = "base_r";
            // 
            // labelBaseL
            // 
            this.labelBaseL.AutoSize = true;
            this.labelBaseL.Location = new System.Drawing.Point(10, 46);
            this.labelBaseL.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelBaseL.Name = "labelBaseL";
            this.labelBaseL.Size = new System.Drawing.Size(56, 20);
            this.labelBaseL.TabIndex = 27;
            this.labelBaseL.Text = "base_l";
            // 
            // textBoxBaseR
            // 
            this.textBoxBaseR.Location = new System.Drawing.Point(128, 82);
            this.textBoxBaseR.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxBaseR.Name = "textBoxBaseR";
            this.textBoxBaseR.ReadOnly = true;
            this.textBoxBaseR.Size = new System.Drawing.Size(148, 26);
            this.textBoxBaseR.TabIndex = 26;
            // 
            // labelTempL
            // 
            this.labelTempL.AutoSize = true;
            this.labelTempL.Location = new System.Drawing.Point(646, 758);
            this.labelTempL.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTempL.Name = "labelTempL";
            this.labelTempL.Size = new System.Drawing.Size(57, 20);
            this.labelTempL.TabIndex = 42;
            this.labelTempL.Text = "temp_l";
            // 
            // textBoxTempL
            // 
            this.textBoxTempL.Location = new System.Drawing.Point(764, 754);
            this.textBoxTempL.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxTempL.Name = "textBoxTempL";
            this.textBoxTempL.ReadOnly = true;
            this.textBoxTempL.Size = new System.Drawing.Size(148, 26);
            this.textBoxTempL.TabIndex = 41;
            // 
            // comboBoxCOM
            // 
            this.comboBoxCOM.FormattingEnabled = true;
            this.comboBoxCOM.Location = new System.Drawing.Point(200, 652);
            this.comboBoxCOM.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBoxCOM.Name = "comboBoxCOM";
            this.comboBoxCOM.Size = new System.Drawing.Size(180, 28);
            this.comboBoxCOM.TabIndex = 43;
            // 
            // labelCOM
            // 
            this.labelCOM.AutoSize = true;
            this.labelCOM.Location = new System.Drawing.Point(90, 658);
            this.labelCOM.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCOM.Name = "labelCOM";
            this.labelCOM.Size = new System.Drawing.Size(78, 20);
            this.labelCOM.TabIndex = 44;
            this.labelCOM.Text = "COM Port";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(80, 608);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(112, 35);
            this.btnRefresh.TabIndex = 45;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(1674, 52);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(112, 35);
            this.btnClear.TabIndex = 55;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(1430, 97);
            this.btnExport.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(112, 35);
            this.btnExport.TabIndex = 54;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(1551, 97);
            this.btnImport.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(112, 35);
            this.btnImport.TabIndex = 53;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnSolve
            // 
            this.btnSolve.Location = new System.Drawing.Point(1551, 52);
            this.btnSolve.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSolve.Name = "btnSolve";
            this.btnSolve.Size = new System.Drawing.Size(112, 35);
            this.btnSolve.TabIndex = 51;
            this.btnSolve.Text = "Solve";
            this.btnSolve.UseVisualStyleBackColor = true;
            this.btnSolve.Click += new System.EventHandler(this.btnSolve_Click);
            // 
            // txtboxColumn
            // 
            this.txtboxColumn.Location = new System.Drawing.Point(1248, 57);
            this.txtboxColumn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtboxColumn.Name = "txtboxColumn";
            this.txtboxColumn.Size = new System.Drawing.Size(148, 26);
            this.txtboxColumn.TabIndex = 50;
            // 
            // txtboxRow
            // 
            this.txtboxRow.Location = new System.Drawing.Point(1077, 57);
            this.txtboxRow.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtboxRow.Name = "txtboxRow";
            this.txtboxRow.Size = new System.Drawing.Size(148, 26);
            this.txtboxRow.TabIndex = 49;
            // 
            // labelColumn
            // 
            this.labelColumn.AutoSize = true;
            this.labelColumn.Location = new System.Drawing.Point(1244, 32);
            this.labelColumn.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelColumn.Name = "labelColumn";
            this.labelColumn.Size = new System.Drawing.Size(71, 20);
            this.labelColumn.TabIndex = 48;
            this.labelColumn.Text = "Columns";
            // 
            // labelRow
            // 
            this.labelRow.AutoSize = true;
            this.labelRow.Location = new System.Drawing.Point(1072, 32);
            this.labelRow.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelRow.Name = "labelRow";
            this.labelRow.Size = new System.Drawing.Size(49, 20);
            this.labelRow.TabIndex = 47;
            this.labelRow.Text = "Rows";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(1430, 52);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(112, 35);
            this.btnGenerate.TabIndex = 46;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnRunPython
            // 
            this.btnRunPython.Location = new System.Drawing.Point(0, 0);
            this.btnRunPython.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRunPython.Name = "btnRunPython";
            this.btnRunPython.Size = new System.Drawing.Size(112, 35);
            this.btnRunPython.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1804, 835);
            this.Controls.Add(this.btnRunPython);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnSolve);
            this.Controls.Add(this.txtboxColumn);
            this.Controls.Add(this.txtboxRow);
            this.Controls.Add(this.labelColumn);
            this.Controls.Add(this.labelRow);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.labelCOM);
            this.Controls.Add(this.comboBoxCOM);
            this.Controls.Add(this.labelTempL);
            this.Controls.Add(this.groupBoxInfo);
            this.Controls.Add(this.textBoxTempL);
            this.Controls.Add(this.buttonNorthWest);
            this.Controls.Add(this.btnWest);
            this.Controls.Add(this.btnSouthWest);
            this.Controls.Add(this.btnSouth);
            this.Controls.Add(this.btnSouthEast);
            this.Controls.Add(this.btnEast);
            this.Controls.Add(this.btnNorthEast);
            this.Controls.Add(this.btnNorth);
            this.Controls.Add(this.btnDock);
            this.Controls.Add(this.labelSpeed);
            this.Controls.Add(this.trackBarSpeed);
            this.Controls.Add(this.chkboxDisplayRaw);
            this.Controls.Add(this.btnSong);
            this.Controls.Add(this.btnCharger);
            this.Controls.Add(this.btnReverse);
            this.Controls.Add(this.btnRight);
            this.Controls.Add(this.btnLeft);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnForward);
            this.Controls.Add(this.btnStart);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "CreateRemote";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpeed)).EndInit();
            this.groupBoxInfo.ResumeLayout(false);
            this.groupBoxInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnReverse;
        private System.Windows.Forms.Button btnCharger;
        private System.Windows.Forms.Button btnSong;
        private System.Windows.Forms.CheckBox chkboxDisplayRaw;
        private System.Windows.Forms.TrackBar trackBarSpeed;
        private System.Windows.Forms.Label labelSpeed;
        private System.Windows.Forms.Button btnDock;
        private System.Windows.Forms.Button btnNorth;
        private System.Windows.Forms.Button btnNorthEast;
        private System.Windows.Forms.Button btnEast;
        private System.Windows.Forms.Button btnSouthEast;
        private System.Windows.Forms.Button btnSouth;
        private System.Windows.Forms.Button btnSouthWest;
        private System.Windows.Forms.Button btnWest;
        private System.Windows.Forms.Button buttonNorthWest;
        private System.Windows.Forms.TextBox textBoxBaseL;
        private System.Windows.Forms.GroupBox groupBoxInfo;
        private System.Windows.Forms.Label labelBaseR;
        private System.Windows.Forms.Label labelBaseL;
        private System.Windows.Forms.TextBox textBoxBaseR;
        private System.Windows.Forms.Label labelCurrentL;
        private System.Windows.Forms.TextBox textBoxCurrentR;
        private System.Windows.Forms.TextBox textBoxCurrentL;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label labelTurningDistance;
        private System.Windows.Forms.Label labelAngle;
        private System.Windows.Forms.TextBox textBoxTurningDistance;
        private System.Windows.Forms.TextBox textBoxAngle;
        private System.Windows.Forms.Label labelCurrentR;
        private System.Windows.Forms.Label labelCommand;
        private System.Windows.Forms.TextBox textBoxCommand;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label labelReceived;
        private System.Windows.Forms.TextBox textBoxReceived;
        private System.Windows.Forms.Label labelTempL;
        private System.Windows.Forms.TextBox textBoxTempL;
        private System.Windows.Forms.ComboBox comboBoxCOM;
        private System.Windows.Forms.Label labelCOM;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnSolve;
        private System.Windows.Forms.TextBox txtboxColumn;
        private System.Windows.Forms.TextBox txtboxRow;
        private System.Windows.Forms.Label labelColumn;
        private System.Windows.Forms.Label labelRow;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label labelAngleCalc;
        private System.Windows.Forms.TextBox textBoxAngleCalc;
        private System.Windows.Forms.Button btnRunPython;
    }
}

