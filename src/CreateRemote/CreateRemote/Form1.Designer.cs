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
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpeed)).BeginInit();
            this.groupBoxInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(133, 51);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnForward
            // 
            this.btnForward.Location = new System.Drawing.Point(133, 105);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(75, 23);
            this.btnForward.TabIndex = 1;
            this.btnForward.Text = "Forward";
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(133, 165);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.Location = new System.Drawing.Point(29, 165);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(75, 23);
            this.btnLeft.TabIndex = 3;
            this.btnLeft.Text = "Left";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnRight
            // 
            this.btnRight.Location = new System.Drawing.Point(241, 165);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(75, 23);
            this.btnRight.TabIndex = 4;
            this.btnRight.Text = "Right";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnReverse
            // 
            this.btnReverse.Location = new System.Drawing.Point(133, 228);
            this.btnReverse.Name = "btnReverse";
            this.btnReverse.Size = new System.Drawing.Size(75, 23);
            this.btnReverse.TabIndex = 5;
            this.btnReverse.Text = "Reverse";
            this.btnReverse.UseVisualStyleBackColor = true;
            this.btnReverse.Click += new System.EventHandler(this.btnReverse_Click);
            // 
            // btnCharger
            // 
            this.btnCharger.Location = new System.Drawing.Point(29, 286);
            this.btnCharger.Name = "btnCharger";
            this.btnCharger.Size = new System.Drawing.Size(75, 23);
            this.btnCharger.TabIndex = 6;
            this.btnCharger.Text = "Charger";
            this.btnCharger.UseVisualStyleBackColor = true;
            this.btnCharger.Click += new System.EventHandler(this.btnCharger_Click);
            // 
            // btnSong
            // 
            this.btnSong.Location = new System.Drawing.Point(241, 286);
            this.btnSong.Name = "btnSong";
            this.btnSong.Size = new System.Drawing.Size(75, 23);
            this.btnSong.TabIndex = 7;
            this.btnSong.Text = "Song";
            this.btnSong.UseVisualStyleBackColor = true;
            this.btnSong.Click += new System.EventHandler(this.btnSong_Click);
            // 
            // chkboxDisplayRaw
            // 
            this.chkboxDisplayRaw.AutoSize = true;
            this.chkboxDisplayRaw.Location = new System.Drawing.Point(95, 352);
            this.chkboxDisplayRaw.Name = "chkboxDisplayRaw";
            this.chkboxDisplayRaw.Size = new System.Drawing.Size(163, 17);
            this.chkboxDisplayRaw.TabIndex = 13;
            this.chkboxDisplayRaw.Text = "Display Raw Sensor Packets";
            this.chkboxDisplayRaw.UseVisualStyleBackColor = true;
            this.chkboxDisplayRaw.CheckedChanged += new System.EventHandler(this.checkBoxRawSensor_CheckedChanged);
            // 
            // trackBarSpeed
            // 
            this.trackBarSpeed.Location = new System.Drawing.Point(349, 51);
            this.trackBarSpeed.Maximum = 500;
            this.trackBarSpeed.Minimum = 50;
            this.trackBarSpeed.Name = "trackBarSpeed";
            this.trackBarSpeed.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarSpeed.Size = new System.Drawing.Size(45, 104);
            this.trackBarSpeed.SmallChange = 10;
            this.trackBarSpeed.TabIndex = 14;
            this.trackBarSpeed.TickFrequency = 10;
            this.trackBarSpeed.Value = 50;
            this.trackBarSpeed.Scroll += new System.EventHandler(this.trackBarSpeed_Scroll);
            // 
            // labelSpeed
            // 
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.Location = new System.Drawing.Point(346, 26);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(38, 13);
            this.labelSpeed.TabIndex = 15;
            this.labelSpeed.Text = "Speed";
            // 
            // btnDock
            // 
            this.btnDock.Location = new System.Drawing.Point(133, 286);
            this.btnDock.Name = "btnDock";
            this.btnDock.Size = new System.Drawing.Size(75, 23);
            this.btnDock.TabIndex = 16;
            this.btnDock.Text = "Dock";
            this.btnDock.UseVisualStyleBackColor = true;
            this.btnDock.Click += new System.EventHandler(this.button9_Click);
            // 
            // btnNorth
            // 
            this.btnNorth.Location = new System.Drawing.Point(520, 21);
            this.btnNorth.Name = "btnNorth";
            this.btnNorth.Size = new System.Drawing.Size(75, 23);
            this.btnNorth.TabIndex = 17;
            this.btnNorth.Text = "N";
            this.btnNorth.UseVisualStyleBackColor = true;
            this.btnNorth.Click += new System.EventHandler(this.btnNorth_Click);
            // 
            // btnNorthEast
            // 
            this.btnNorthEast.Location = new System.Drawing.Point(614, 21);
            this.btnNorthEast.Name = "btnNorthEast";
            this.btnNorthEast.Size = new System.Drawing.Size(75, 23);
            this.btnNorthEast.TabIndex = 18;
            this.btnNorthEast.Text = "NE";
            this.btnNorthEast.UseVisualStyleBackColor = true;
            this.btnNorthEast.Click += new System.EventHandler(this.btnNorthEast_Click);
            // 
            // btnEast
            // 
            this.btnEast.Location = new System.Drawing.Point(614, 81);
            this.btnEast.Name = "btnEast";
            this.btnEast.Size = new System.Drawing.Size(75, 23);
            this.btnEast.TabIndex = 19;
            this.btnEast.Text = "E";
            this.btnEast.UseVisualStyleBackColor = true;
            this.btnEast.Click += new System.EventHandler(this.btnEast_Click);
            // 
            // btnSouthEast
            // 
            this.btnSouthEast.Location = new System.Drawing.Point(614, 144);
            this.btnSouthEast.Name = "btnSouthEast";
            this.btnSouthEast.Size = new System.Drawing.Size(75, 23);
            this.btnSouthEast.TabIndex = 20;
            this.btnSouthEast.Text = "SE";
            this.btnSouthEast.UseVisualStyleBackColor = true;
            this.btnSouthEast.Click += new System.EventHandler(this.btnSouthEast_Click);
            // 
            // btnSouth
            // 
            this.btnSouth.Location = new System.Drawing.Point(520, 144);
            this.btnSouth.Name = "btnSouth";
            this.btnSouth.Size = new System.Drawing.Size(75, 23);
            this.btnSouth.TabIndex = 21;
            this.btnSouth.Text = "S";
            this.btnSouth.UseVisualStyleBackColor = true;
            this.btnSouth.Click += new System.EventHandler(this.btnSouth_Click);
            // 
            // btnSouthWest
            // 
            this.btnSouthWest.Location = new System.Drawing.Point(424, 144);
            this.btnSouthWest.Name = "btnSouthWest";
            this.btnSouthWest.Size = new System.Drawing.Size(75, 23);
            this.btnSouthWest.TabIndex = 22;
            this.btnSouthWest.Text = "SW";
            this.btnSouthWest.UseVisualStyleBackColor = true;
            this.btnSouthWest.Click += new System.EventHandler(this.btnSouthWest_Click);
            // 
            // btnWest
            // 
            this.btnWest.Location = new System.Drawing.Point(424, 81);
            this.btnWest.Name = "btnWest";
            this.btnWest.Size = new System.Drawing.Size(75, 23);
            this.btnWest.TabIndex = 23;
            this.btnWest.Text = "W";
            this.btnWest.UseVisualStyleBackColor = true;
            this.btnWest.Click += new System.EventHandler(this.btnWest_Click);
            // 
            // buttonNorthWest
            // 
            this.buttonNorthWest.Location = new System.Drawing.Point(424, 21);
            this.buttonNorthWest.Name = "buttonNorthWest";
            this.buttonNorthWest.Size = new System.Drawing.Size(75, 23);
            this.buttonNorthWest.TabIndex = 24;
            this.buttonNorthWest.Text = "NW";
            this.buttonNorthWest.UseVisualStyleBackColor = true;
            this.buttonNorthWest.Click += new System.EventHandler(this.btnNorthWest_Click);
            // 
            // textBoxBaseL
            // 
            this.textBoxBaseL.Location = new System.Drawing.Point(85, 27);
            this.textBoxBaseL.Name = "textBoxBaseL";
            this.textBoxBaseL.ReadOnly = true;
            this.textBoxBaseL.Size = new System.Drawing.Size(100, 20);
            this.textBoxBaseL.TabIndex = 25;
            // 
            // groupBoxInfo
            // 
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
            this.groupBoxInfo.Location = new System.Drawing.Point(424, 207);
            this.groupBoxInfo.Name = "groupBoxInfo";
            this.groupBoxInfo.Size = new System.Drawing.Size(200, 277);
            this.groupBoxInfo.TabIndex = 26;
            this.groupBoxInfo.TabStop = false;
            this.groupBoxInfo.Text = "Info";
            // 
            // labelReceived
            // 
            this.labelReceived.AutoSize = true;
            this.labelReceived.Location = new System.Drawing.Point(7, 212);
            this.labelReceived.Name = "labelReceived";
            this.labelReceived.Size = new System.Drawing.Size(48, 13);
            this.labelReceived.TabIndex = 40;
            this.labelReceived.Text = "received";
            // 
            // textBoxReceived
            // 
            this.textBoxReceived.Location = new System.Drawing.Point(85, 209);
            this.textBoxReceived.Name = "textBoxReceived";
            this.textBoxReceived.ReadOnly = true;
            this.textBoxReceived.Size = new System.Drawing.Size(100, 20);
            this.textBoxReceived.TabIndex = 39;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(96, 235);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 27;
            this.btnSend.Text = "send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // labelCommand
            // 
            this.labelCommand.AutoSize = true;
            this.labelCommand.Location = new System.Drawing.Point(7, 186);
            this.labelCommand.Name = "labelCommand";
            this.labelCommand.Size = new System.Drawing.Size(53, 13);
            this.labelCommand.TabIndex = 38;
            this.labelCommand.Text = "command";
            // 
            // textBoxCommand
            // 
            this.textBoxCommand.Location = new System.Drawing.Point(85, 183);
            this.textBoxCommand.Name = "textBoxCommand";
            this.textBoxCommand.Size = new System.Drawing.Size(100, 20);
            this.textBoxCommand.TabIndex = 37;
            this.textBoxCommand.TextChanged += new System.EventHandler(this.textBoxComand_TextChanged);
            // 
            // labelTurningDistance
            // 
            this.labelTurningDistance.AutoSize = true;
            this.labelTurningDistance.Location = new System.Drawing.Point(7, 160);
            this.labelTurningDistance.Name = "labelTurningDistance";
            this.labelTurningDistance.Size = new System.Drawing.Size(81, 13);
            this.labelTurningDistance.TabIndex = 36;
            this.labelTurningDistance.Text = "turningDistance";
            // 
            // labelAngle
            // 
            this.labelAngle.AutoSize = true;
            this.labelAngle.Location = new System.Drawing.Point(7, 134);
            this.labelAngle.Name = "labelAngle";
            this.labelAngle.Size = new System.Drawing.Size(33, 13);
            this.labelAngle.TabIndex = 35;
            this.labelAngle.Text = "angle";
            // 
            // textBoxTurningDistance
            // 
            this.textBoxTurningDistance.Location = new System.Drawing.Point(85, 157);
            this.textBoxTurningDistance.Name = "textBoxTurningDistance";
            this.textBoxTurningDistance.ReadOnly = true;
            this.textBoxTurningDistance.Size = new System.Drawing.Size(100, 20);
            this.textBoxTurningDistance.TabIndex = 34;
            // 
            // textBoxAngle
            // 
            this.textBoxAngle.Location = new System.Drawing.Point(85, 131);
            this.textBoxAngle.Name = "textBoxAngle";
            this.textBoxAngle.ReadOnly = true;
            this.textBoxAngle.Size = new System.Drawing.Size(100, 20);
            this.textBoxAngle.TabIndex = 33;
            // 
            // labelCurrentR
            // 
            this.labelCurrentR.AutoSize = true;
            this.labelCurrentR.Location = new System.Drawing.Point(7, 108);
            this.labelCurrentR.Name = "labelCurrentR";
            this.labelCurrentR.Size = new System.Drawing.Size(10, 13);
            this.labelCurrentR.TabIndex = 32;
            this.labelCurrentR.Text = "r";
            // 
            // labelCurrentL
            // 
            this.labelCurrentL.AutoSize = true;
            this.labelCurrentL.Location = new System.Drawing.Point(7, 86);
            this.labelCurrentL.Name = "labelCurrentL";
            this.labelCurrentL.Size = new System.Drawing.Size(9, 13);
            this.labelCurrentL.TabIndex = 31;
            this.labelCurrentL.Text = "l";
            // 
            // textBoxCurrentR
            // 
            this.textBoxCurrentR.Location = new System.Drawing.Point(85, 105);
            this.textBoxCurrentR.Name = "textBoxCurrentR";
            this.textBoxCurrentR.ReadOnly = true;
            this.textBoxCurrentR.Size = new System.Drawing.Size(100, 20);
            this.textBoxCurrentR.TabIndex = 30;
            // 
            // textBoxCurrentL
            // 
            this.textBoxCurrentL.Location = new System.Drawing.Point(85, 79);
            this.textBoxCurrentL.Name = "textBoxCurrentL";
            this.textBoxCurrentL.ReadOnly = true;
            this.textBoxCurrentL.Size = new System.Drawing.Size(100, 20);
            this.textBoxCurrentL.TabIndex = 29;
            // 
            // labelBaseR
            // 
            this.labelBaseR.AutoSize = true;
            this.labelBaseR.Location = new System.Drawing.Point(7, 56);
            this.labelBaseR.Name = "labelBaseR";
            this.labelBaseR.Size = new System.Drawing.Size(39, 13);
            this.labelBaseR.TabIndex = 28;
            this.labelBaseR.Text = "base_r";
            // 
            // labelBaseL
            // 
            this.labelBaseL.AutoSize = true;
            this.labelBaseL.Location = new System.Drawing.Point(7, 30);
            this.labelBaseL.Name = "labelBaseL";
            this.labelBaseL.Size = new System.Drawing.Size(38, 13);
            this.labelBaseL.TabIndex = 27;
            this.labelBaseL.Text = "base_l";
            // 
            // textBoxBaseR
            // 
            this.textBoxBaseR.Location = new System.Drawing.Point(85, 53);
            this.textBoxBaseR.Name = "textBoxBaseR";
            this.textBoxBaseR.ReadOnly = true;
            this.textBoxBaseR.Size = new System.Drawing.Size(100, 20);
            this.textBoxBaseR.TabIndex = 26;
            // 
            // labelTempL
            // 
            this.labelTempL.AutoSize = true;
            this.labelTempL.Location = new System.Drawing.Point(431, 493);
            this.labelTempL.Name = "labelTempL";
            this.labelTempL.Size = new System.Drawing.Size(38, 13);
            this.labelTempL.TabIndex = 42;
            this.labelTempL.Text = "temp_l";
            // 
            // textBoxTempL
            // 
            this.textBoxTempL.Location = new System.Drawing.Point(509, 490);
            this.textBoxTempL.Name = "textBoxTempL";
            this.textBoxTempL.ReadOnly = true;
            this.textBoxTempL.Size = new System.Drawing.Size(100, 20);
            this.textBoxTempL.TabIndex = 41;
            // 
            // comboBoxCOM
            // 
            this.comboBoxCOM.FormattingEnabled = true;
            this.comboBoxCOM.Location = new System.Drawing.Point(133, 424);
            this.comboBoxCOM.Name = "comboBoxCOM";
            this.comboBoxCOM.Size = new System.Drawing.Size(121, 21);
            this.comboBoxCOM.TabIndex = 43;
            // 
            // labelCOM
            // 
            this.labelCOM.AutoSize = true;
            this.labelCOM.Location = new System.Drawing.Point(60, 428);
            this.labelCOM.Name = "labelCOM";
            this.labelCOM.Size = new System.Drawing.Size(53, 13);
            this.labelCOM.TabIndex = 44;
            this.labelCOM.Text = "COM Port";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(53, 395);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 45;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(1116, 34);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 55;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(953, 63);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 54;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(1034, 63);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 53;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnSolve
            // 
            this.btnSolve.Location = new System.Drawing.Point(1034, 34);
            this.btnSolve.Name = "btnSolve";
            this.btnSolve.Size = new System.Drawing.Size(75, 23);
            this.btnSolve.TabIndex = 51;
            this.btnSolve.Text = "Solve";
            this.btnSolve.UseVisualStyleBackColor = true;
            this.btnSolve.Click += new System.EventHandler(this.btnSolve_Click);
            // 
            // txtboxColumn
            // 
            this.txtboxColumn.Location = new System.Drawing.Point(832, 37);
            this.txtboxColumn.Name = "txtboxColumn";
            this.txtboxColumn.Size = new System.Drawing.Size(100, 20);
            this.txtboxColumn.TabIndex = 50;
            // 
            // txtboxRow
            // 
            this.txtboxRow.Location = new System.Drawing.Point(718, 37);
            this.txtboxRow.Name = "txtboxRow";
            this.txtboxRow.Size = new System.Drawing.Size(100, 20);
            this.txtboxRow.TabIndex = 49;
            // 
            // labelColumn
            // 
            this.labelColumn.AutoSize = true;
            this.labelColumn.Location = new System.Drawing.Point(829, 21);
            this.labelColumn.Name = "labelColumn";
            this.labelColumn.Size = new System.Drawing.Size(47, 13);
            this.labelColumn.TabIndex = 48;
            this.labelColumn.Text = "Columns";
            // 
            // labelRow
            // 
            this.labelRow.AutoSize = true;
            this.labelRow.Location = new System.Drawing.Point(715, 21);
            this.labelRow.Name = "labelRow";
            this.labelRow.Size = new System.Drawing.Size(34, 13);
            this.labelRow.TabIndex = 47;
            this.labelRow.Text = "Rows";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(953, 34);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 46;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 543);
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
    }
}

