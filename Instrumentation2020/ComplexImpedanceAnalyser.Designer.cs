namespace Instrumentation2020
{
    partial class ComplexImpedanceAnalyser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComplexImpedanceAnalyser));
            this.gbPortSettings = new System.Windows.Forms.GroupBox();
            this.autoRangeButton = new System.Windows.Forms.Button();
            this.ToggleRelayBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.PGAGainBox = new System.Windows.Forms.ComboBox();
            this.PGAGainLabel = new System.Windows.Forms.Label();
            this.SETPGAGAINButton = new System.Windows.Forms.Button();
            this.waveformbox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.freqencySetButton = new System.Windows.Forms.Button();
            this.freqLabel = new System.Windows.Forms.Label();
            this.ConnectBtn = new System.Windows.Forms.Button();
            this.freqInput = new System.Windows.Forms.TextBox();
            this.Measure = new System.Windows.Forms.Button();
            this.resetbutton = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.timeoutBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.portNameBox = new System.Windows.Forms.ComboBox();
            this.baudRateBox = new System.Windows.Forms.ComboBox();
            this.lblComPort = new System.Windows.Forms.Label();
            this.lblBaudRate = new System.Windows.Forms.Label();
            this.rtfTerminal = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.ImpedanceGraphTab = new System.Windows.Forms.TabPage();
            this.DebugTab = new System.Windows.Forms.TabPage();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.measurementValueLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.QDlabel = new System.Windows.Forms.Label();
            this.qdval = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.rLabel = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.capacitanceRadioButton = new System.Windows.Forms.RadioButton();
            this.impedanceRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.qualityFactorRadioButton = new System.Windows.Forms.RadioButton();
            this.dissipationRadioFactorButton = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.seriesRadioButton = new System.Windows.Forms.RadioButton();
            this.parallelRadioButton = new System.Windows.Forms.RadioButton();
            this.gbPortSettings.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.ImpedanceGraphTab.SuspendLayout();
            this.DebugTab.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbPortSettings
            // 
            this.gbPortSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gbPortSettings.Controls.Add(this.autoRangeButton);
            this.gbPortSettings.Controls.Add(this.ToggleRelayBox);
            this.gbPortSettings.Controls.Add(this.label3);
            this.gbPortSettings.Controls.Add(this.button1);
            this.gbPortSettings.Controls.Add(this.PGAGainBox);
            this.gbPortSettings.Controls.Add(this.PGAGainLabel);
            this.gbPortSettings.Controls.Add(this.SETPGAGAINButton);
            this.gbPortSettings.Controls.Add(this.waveformbox);
            this.gbPortSettings.Controls.Add(this.label2);
            this.gbPortSettings.Controls.Add(this.freqencySetButton);
            this.gbPortSettings.Controls.Add(this.freqLabel);
            this.gbPortSettings.Controls.Add(this.ConnectBtn);
            this.gbPortSettings.Controls.Add(this.freqInput);
            this.gbPortSettings.Controls.Add(this.Measure);
            this.gbPortSettings.Controls.Add(this.resetbutton);
            this.gbPortSettings.Controls.Add(this.btnClear);
            this.gbPortSettings.Controls.Add(this.timeoutBox);
            this.gbPortSettings.Controls.Add(this.label1);
            this.gbPortSettings.Controls.Add(this.portNameBox);
            this.gbPortSettings.Controls.Add(this.baudRateBox);
            this.gbPortSettings.Controls.Add(this.lblComPort);
            this.gbPortSettings.Controls.Add(this.lblBaudRate);
            this.gbPortSettings.Location = new System.Drawing.Point(15, 535);
            this.gbPortSettings.Margin = new System.Windows.Forms.Padding(6);
            this.gbPortSettings.Name = "gbPortSettings";
            this.gbPortSettings.Padding = new System.Windows.Forms.Padding(6);
            this.gbPortSettings.Size = new System.Drawing.Size(1067, 296);
            this.gbPortSettings.TabIndex = 5;
            this.gbPortSettings.TabStop = false;
            this.gbPortSettings.Text = "Analyser Settings";
            // 
            // autoRangeButton
            // 
            this.autoRangeButton.Enabled = false;
            this.autoRangeButton.Location = new System.Drawing.Point(473, 219);
            this.autoRangeButton.Margin = new System.Windows.Forms.Padding(4);
            this.autoRangeButton.Name = "autoRangeButton";
            this.autoRangeButton.Size = new System.Drawing.Size(184, 65);
            this.autoRangeButton.TabIndex = 25;
            this.autoRangeButton.Text = "Enable Auto Range";
            this.autoRangeButton.UseVisualStyleBackColor = true;
            this.autoRangeButton.Click += new System.EventHandler(this.autoRangeButton_Click);
            // 
            // ToggleRelayBox
            // 
            this.ToggleRelayBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ToggleRelayBox.DropDownWidth = 67;
            this.ToggleRelayBox.Enabled = false;
            this.ToggleRelayBox.FormattingEnabled = true;
            this.ToggleRelayBox.ItemHeight = 25;
            this.ToggleRelayBox.Items.AddRange(new object[] {
            "10",
            "100",
            "1k",
            "10k",
            "100k",
            "1M"});
            this.ToggleRelayBox.Location = new System.Drawing.Point(26, 243);
            this.ToggleRelayBox.Margin = new System.Windows.Forms.Padding(6);
            this.ToggleRelayBox.Name = "ToggleRelayBox";
            this.ToggleRelayBox.Size = new System.Drawing.Size(152, 33);
            this.ToggleRelayBox.TabIndex = 23;
            this.ToggleRelayBox.SelectedIndexChanged += new System.EventHandler(this.ToggleRelayBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 188);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 50);
            this.label3.TabIndex = 21;
            this.label3.Text = "Reference\r\nResistance:";
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(201, 220);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(252, 65);
            this.button1.TabIndex = 22;
            this.button1.Text = "Set Reference\r\nImpedance\r\n";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PGAGainBox
            // 
            this.PGAGainBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PGAGainBox.DropDownWidth = 67;
            this.PGAGainBox.FormattingEnabled = true;
            this.PGAGainBox.ItemHeight = 25;
            this.PGAGainBox.Items.AddRange(new object[] {
            "1",
            "2",
            "4",
            "5",
            "8",
            "10",
            "16",
            "32"});
            this.PGAGainBox.Location = new System.Drawing.Point(681, 62);
            this.PGAGainBox.Margin = new System.Windows.Forms.Padding(6);
            this.PGAGainBox.Name = "PGAGainBox";
            this.PGAGainBox.Size = new System.Drawing.Size(172, 33);
            this.PGAGainBox.TabIndex = 20;
            this.PGAGainBox.SelectedIndexChanged += new System.EventHandler(this.PGAGainBox_SelectedIndexChanged);
            // 
            // PGAGainLabel
            // 
            this.PGAGainLabel.AutoSize = true;
            this.PGAGainLabel.Location = new System.Drawing.Point(676, 35);
            this.PGAGainLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.PGAGainLabel.Name = "PGAGainLabel";
            this.PGAGainLabel.Size = new System.Drawing.Size(113, 25);
            this.PGAGainLabel.TabIndex = 19;
            this.PGAGainLabel.Text = "PGA Gain:";
            // 
            // SETPGAGAINButton
            // 
            this.SETPGAGAINButton.Location = new System.Drawing.Point(873, 57);
            this.SETPGAGAINButton.Margin = new System.Windows.Forms.Padding(4);
            this.SETPGAGAINButton.Name = "SETPGAGAINButton";
            this.SETPGAGAINButton.Size = new System.Drawing.Size(184, 41);
            this.SETPGAGAINButton.TabIndex = 19;
            this.SETPGAGAINButton.Text = "Set PGA Gain";
            this.SETPGAGAINButton.UseVisualStyleBackColor = true;
            this.SETPGAGAINButton.Click += new System.EventHandler(this.SETPGAGAINButton_Click);
            // 
            // waveformbox
            // 
            this.waveformbox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.waveformbox.DropDownWidth = 67;
            this.waveformbox.Enabled = false;
            this.waveformbox.FormattingEnabled = true;
            this.waveformbox.ItemHeight = 25;
            this.waveformbox.Items.AddRange(new object[] {
            "Sine wave",
            "Square wave",
            "Triangle wave"});
            this.waveformbox.Location = new System.Drawing.Point(324, 142);
            this.waveformbox.Margin = new System.Windows.Forms.Padding(6);
            this.waveformbox.Name = "waveformbox";
            this.waveformbox.Size = new System.Drawing.Size(184, 33);
            this.waveformbox.TabIndex = 18;
            this.waveformbox.SelectedIndexChanged += new System.EventHandler(this.waveformbox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(318, 115);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 25);
            this.label2.TabIndex = 17;
            this.label2.Text = "Waveform:";
            // 
            // freqencySetButton
            // 
            this.freqencySetButton.Enabled = false;
            this.freqencySetButton.Location = new System.Drawing.Point(528, 141);
            this.freqencySetButton.Margin = new System.Windows.Forms.Padding(4);
            this.freqencySetButton.Name = "freqencySetButton";
            this.freqencySetButton.Size = new System.Drawing.Size(131, 44);
            this.freqencySetButton.TabIndex = 16;
            this.freqencySetButton.Text = "Set";
            this.freqencySetButton.UseVisualStyleBackColor = true;
            this.freqencySetButton.Click += new System.EventHandler(this.freqencySetButton_Click);
            // 
            // freqLabel
            // 
            this.freqLabel.AutoSize = true;
            this.freqLabel.Location = new System.Drawing.Point(24, 115);
            this.freqLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.freqLabel.Name = "freqLabel";
            this.freqLabel.Size = new System.Drawing.Size(166, 25);
            this.freqLabel.TabIndex = 13;
            this.freqLabel.Text = "Frequency (Hz):";
            // 
            // ConnectBtn
            // 
            this.ConnectBtn.Location = new System.Drawing.Point(176, 63);
            this.ConnectBtn.Margin = new System.Windows.Forms.Padding(4);
            this.ConnectBtn.Name = "ConnectBtn";
            this.ConnectBtn.Size = new System.Drawing.Size(126, 40);
            this.ConnectBtn.TabIndex = 14;
            this.ConnectBtn.Text = "Connect";
            this.ConnectBtn.UseVisualStyleBackColor = true;
            this.ConnectBtn.Click += new System.EventHandler(this.ConnectBtn_Click);
            // 
            // freqInput
            // 
            this.freqInput.Enabled = false;
            this.freqInput.Location = new System.Drawing.Point(26, 142);
            this.freqInput.Margin = new System.Windows.Forms.Padding(4);
            this.freqInput.Name = "freqInput";
            this.freqInput.Size = new System.Drawing.Size(276, 31);
            this.freqInput.TabIndex = 15;
            this.freqInput.Text = "1000";
            this.freqInput.TextChanged += new System.EventHandler(this.freqInput_TextChanged);
            this.freqInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.freqInput_KeyPress);
            // 
            // Measure
            // 
            this.Measure.Enabled = false;
            this.Measure.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Measure.Location = new System.Drawing.Point(681, 141);
            this.Measure.Margin = new System.Windows.Forms.Padding(6);
            this.Measure.Name = "Measure";
            this.Measure.Size = new System.Drawing.Size(374, 44);
            this.Measure.TabIndex = 12;
            this.Measure.Text = "&Measure";
            this.Measure.UseVisualStyleBackColor = true;
            this.Measure.Click += new System.EventHandler(this.Measure_Click);
            // 
            // resetbutton
            // 
            this.resetbutton.Enabled = false;
            this.resetbutton.Location = new System.Drawing.Point(681, 220);
            this.resetbutton.Margin = new System.Windows.Forms.Padding(6);
            this.resetbutton.Name = "resetbutton";
            this.resetbutton.Size = new System.Drawing.Size(172, 65);
            this.resetbutton.TabIndex = 13;
            this.resetbutton.Text = "Reset";
            this.resetbutton.UseVisualStyleBackColor = true;
            this.resetbutton.Click += new System.EventHandler(this.resetbutton_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(873, 219);
            this.btnClear.Margin = new System.Windows.Forms.Padding(6);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(184, 65);
            this.btnClear.TabIndex = 10;
            this.btnClear.Text = "&Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // timeoutBox
            // 
            this.timeoutBox.DropDownWidth = 67;
            this.timeoutBox.Enabled = false;
            this.timeoutBox.ItemHeight = 25;
            this.timeoutBox.Items.AddRange(new object[] {
            "1000",
            "2000",
            "5000",
            "7000",
            "10000"});
            this.timeoutBox.Location = new System.Drawing.Point(324, 63);
            this.timeoutBox.Margin = new System.Windows.Forms.Padding(6);
            this.timeoutBox.Name = "timeoutBox";
            this.timeoutBox.Size = new System.Drawing.Size(184, 33);
            this.timeoutBox.TabIndex = 5;
            this.timeoutBox.SelectedIndexChanged += new System.EventHandler(this.timeoutBox_SelectedIndexChanged);
            this.timeoutBox.TextUpdate += new System.EventHandler(this.timeoutBox_TextUpdate);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(319, 35);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Timeout (ms):";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // portNameBox
            // 
            this.portNameBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.portNameBox.FormattingEnabled = true;
            this.portNameBox.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6"});
            this.portNameBox.Location = new System.Drawing.Point(26, 63);
            this.portNameBox.Margin = new System.Windows.Forms.Padding(6);
            this.portNameBox.Name = "portNameBox";
            this.portNameBox.Size = new System.Drawing.Size(130, 33);
            this.portNameBox.TabIndex = 1;
            this.portNameBox.SelectedIndexChanged += new System.EventHandler(this.cmbPortName_SelectedIndexChanged);
            // 
            // baudRateBox
            // 
            this.baudRateBox.DropDownWidth = 67;
            this.baudRateBox.FormattingEnabled = true;
            this.baudRateBox.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.baudRateBox.Location = new System.Drawing.Point(528, 63);
            this.baudRateBox.Margin = new System.Windows.Forms.Padding(6);
            this.baudRateBox.Name = "baudRateBox";
            this.baudRateBox.Size = new System.Drawing.Size(131, 33);
            this.baudRateBox.TabIndex = 3;
            this.baudRateBox.SelectedIndexChanged += new System.EventHandler(this.cmbBaudRate_SelectedIndexChanged);
            this.baudRateBox.TextUpdate += new System.EventHandler(this.cmbBaudRate_TextUpdate);
            // 
            // lblComPort
            // 
            this.lblComPort.AutoSize = true;
            this.lblComPort.Location = new System.Drawing.Point(24, 33);
            this.lblComPort.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblComPort.Name = "lblComPort";
            this.lblComPort.Size = new System.Drawing.Size(112, 25);
            this.lblComPort.TabIndex = 0;
            this.lblComPort.Text = "COM Port:";
            // 
            // lblBaudRate
            // 
            this.lblBaudRate.AutoSize = true;
            this.lblBaudRate.Location = new System.Drawing.Point(524, 35);
            this.lblBaudRate.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblBaudRate.Name = "lblBaudRate";
            this.lblBaudRate.Size = new System.Drawing.Size(119, 25);
            this.lblBaudRate.TabIndex = 2;
            this.lblBaudRate.Text = "Baud Rate:";
            // 
            // rtfTerminal
            // 
            this.rtfTerminal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtfTerminal.Location = new System.Drawing.Point(6, 6);
            this.rtfTerminal.Margin = new System.Windows.Forms.Padding(6);
            this.rtfTerminal.Name = "rtfTerminal";
            this.rtfTerminal.Size = new System.Drawing.Size(646, 566);
            this.rtfTerminal.TabIndex = 11;
            this.rtfTerminal.Text = "";
            this.rtfTerminal.TextChanged += new System.EventHandler(this.rtfTerminal_TextChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zedGraphControl1.Location = new System.Drawing.Point(6, 6);
            this.zedGraphControl1.Margin = new System.Windows.Forms.Padding(12);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(646, 453);
            this.zedGraphControl1.TabIndex = 22;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.ImpedanceGraphTab);
            this.tabControl1.Controls.Add(this.DebugTab);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(674, 517);
            this.tabControl1.TabIndex = 24;
            // 
            // ImpedanceGraphTab
            // 
            this.ImpedanceGraphTab.Controls.Add(this.zedGraphControl1);
            this.ImpedanceGraphTab.Location = new System.Drawing.Point(8, 39);
            this.ImpedanceGraphTab.Margin = new System.Windows.Forms.Padding(4);
            this.ImpedanceGraphTab.Name = "ImpedanceGraphTab";
            this.ImpedanceGraphTab.Padding = new System.Windows.Forms.Padding(4);
            this.ImpedanceGraphTab.Size = new System.Drawing.Size(658, 470);
            this.ImpedanceGraphTab.TabIndex = 0;
            this.ImpedanceGraphTab.Text = "Impedance";
            this.ImpedanceGraphTab.UseVisualStyleBackColor = true;
            // 
            // DebugTab
            // 
            this.DebugTab.Controls.Add(this.rtfTerminal);
            this.DebugTab.Location = new System.Drawing.Point(8, 39);
            this.DebugTab.Margin = new System.Windows.Forms.Padding(4);
            this.DebugTab.Name = "DebugTab";
            this.DebugTab.Padding = new System.Windows.Forms.Padding(4);
            this.DebugTab.Size = new System.Drawing.Size(658, 470);
            this.DebugTab.TabIndex = 1;
            this.DebugTab.Text = "Debug";
            this.DebugTab.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // measurementValueLabel
            // 
            this.measurementValueLabel.AutoSize = true;
            this.measurementValueLabel.Font = new System.Drawing.Font("Lucida Console", 16.875F, System.Drawing.FontStyle.Bold);
            this.measurementValueLabel.Location = new System.Drawing.Point(18, 45);
            this.measurementValueLabel.Name = "measurementValueLabel";
            this.measurementValueLabel.Size = new System.Drawing.Size(324, 90);
            this.measurementValueLabel.TabIndex = 23;
            this.measurementValueLabel.Text = "Z:0.0∠0.00 \r\n  0.0+0.0j\r\n";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.QDlabel);
            this.groupBox1.Controls.Add(this.qdval);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.comLabel);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.rLabel);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.measurementValueLabel);
            this.groupBox1.Location = new System.Drawing.Point(693, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(389, 531);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Impedance Information:";
            // 
            // QDlabel
            // 
            this.QDlabel.AutoSize = true;
            this.QDlabel.Location = new System.Drawing.Point(7, 333);
            this.QDlabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.QDlabel.Name = "QDlabel";
            this.QDlabel.Size = new System.Drawing.Size(152, 25);
            this.QDlabel.TabIndex = 39;
            this.QDlabel.Text = "Quality Factor:";
            // 
            // qdval
            // 
            this.qdval.AutoSize = true;
            this.qdval.Font = new System.Drawing.Font("Lucida Console", 14.875F, System.Drawing.FontStyle.Bold);
            this.qdval.Location = new System.Drawing.Point(16, 361);
            this.qdval.Name = "qdval";
            this.qdval.Size = new System.Drawing.Size(42, 40);
            this.qdval.TabIndex = 40;
            this.qdval.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 250);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(205, 25);
            this.label6.TabIndex = 37;
            this.label6.Text = "Series Capacitance:";
            // 
            // comLabel
            // 
            this.comLabel.AutoSize = true;
            this.comLabel.Font = new System.Drawing.Font("Lucida Console", 14.875F, System.Drawing.FontStyle.Bold);
            this.comLabel.Location = new System.Drawing.Point(16, 278);
            this.comLabel.Name = "comLabel";
            this.comLabel.Size = new System.Drawing.Size(42, 40);
            this.comLabel.TabIndex = 38;
            this.comLabel.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 167);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(192, 25);
            this.label5.TabIndex = 26;
            this.label5.Text = "Series Resistance:";
            // 
            // rLabel
            // 
            this.rLabel.AutoSize = true;
            this.rLabel.Font = new System.Drawing.Font("Lucida Console", 14.875F, System.Drawing.FontStyle.Bold);
            this.rLabel.Location = new System.Drawing.Point(16, 195);
            this.rLabel.Name = "rLabel";
            this.rLabel.Size = new System.Drawing.Size(42, 40);
            this.rLabel.TabIndex = 36;
            this.rLabel.Text = "0";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.capacitanceRadioButton);
            this.groupBox4.Controls.Add(this.impedanceRadioButton);
            this.groupBox4.Location = new System.Drawing.Point(6, 410);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(153, 107);
            this.groupBox4.TabIndex = 34;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Impedance:";
            // 
            // capacitanceRadioButton
            // 
            this.capacitanceRadioButton.AutoSize = true;
            this.capacitanceRadioButton.Checked = true;
            this.capacitanceRadioButton.Location = new System.Drawing.Point(6, 30);
            this.capacitanceRadioButton.Name = "capacitanceRadioButton";
            this.capacitanceRadioButton.Size = new System.Drawing.Size(163, 29);
            this.capacitanceRadioButton.TabIndex = 29;
            this.capacitanceRadioButton.TabStop = true;
            this.capacitanceRadioButton.Text = "Capacitance";
            this.capacitanceRadioButton.UseVisualStyleBackColor = true;
            this.capacitanceRadioButton.CheckedChanged += new System.EventHandler(this.capacitanceRadioButton_CheckedChanged);
            // 
            // impedanceRadioButton
            // 
            this.impedanceRadioButton.AutoSize = true;
            this.impedanceRadioButton.Location = new System.Drawing.Point(6, 65);
            this.impedanceRadioButton.Name = "impedanceRadioButton";
            this.impedanceRadioButton.Size = new System.Drawing.Size(148, 29);
            this.impedanceRadioButton.TabIndex = 27;
            this.impedanceRadioButton.Text = "Inductance";
            this.impedanceRadioButton.UseVisualStyleBackColor = true;
            this.impedanceRadioButton.CheckedChanged += new System.EventHandler(this.impedanceRadioButton_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.qualityFactorRadioButton);
            this.groupBox5.Controls.Add(this.dissipationRadioFactorButton);
            this.groupBox5.Location = new System.Drawing.Point(166, 409);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(218, 108);
            this.groupBox5.TabIndex = 35;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Q/D Factor:";
            // 
            // qualityFactorRadioButton
            // 
            this.qualityFactorRadioButton.AutoSize = true;
            this.qualityFactorRadioButton.Checked = true;
            this.qualityFactorRadioButton.Location = new System.Drawing.Point(6, 30);
            this.qualityFactorRadioButton.Name = "qualityFactorRadioButton";
            this.qualityFactorRadioButton.Size = new System.Drawing.Size(177, 29);
            this.qualityFactorRadioButton.TabIndex = 30;
            this.qualityFactorRadioButton.TabStop = true;
            this.qualityFactorRadioButton.Text = "Quality Factor";
            this.qualityFactorRadioButton.UseVisualStyleBackColor = true;
            this.qualityFactorRadioButton.CheckedChanged += new System.EventHandler(this.qualityFactorRadioButton_CheckedChanged);
            // 
            // dissipationRadioFactorButton
            // 
            this.dissipationRadioFactorButton.AutoSize = true;
            this.dissipationRadioFactorButton.Location = new System.Drawing.Point(6, 65);
            this.dissipationRadioFactorButton.Name = "dissipationRadioFactorButton";
            this.dissipationRadioFactorButton.Size = new System.Drawing.Size(216, 29);
            this.dissipationRadioFactorButton.TabIndex = 32;
            this.dissipationRadioFactorButton.Text = "Dissipation Factor";
            this.dissipationRadioFactorButton.UseVisualStyleBackColor = true;
            this.dissipationRadioFactorButton.CheckedChanged += new System.EventHandler(this.dissipationRadioFactorButton_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.seriesRadioButton);
            this.groupBox3.Controls.Add(this.parallelRadioButton);
            this.groupBox3.Enabled = false;
            this.groupBox3.Location = new System.Drawing.Point(348, 28);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(165, 107);
            this.groupBox3.TabIndex = 33;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Mode:";
            this.groupBox3.Visible = false;
            // 
            // seriesRadioButton
            // 
            this.seriesRadioButton.AutoSize = true;
            this.seriesRadioButton.Checked = true;
            this.seriesRadioButton.Location = new System.Drawing.Point(6, 30);
            this.seriesRadioButton.Name = "seriesRadioButton";
            this.seriesRadioButton.Size = new System.Drawing.Size(104, 29);
            this.seriesRadioButton.TabIndex = 26;
            this.seriesRadioButton.TabStop = true;
            this.seriesRadioButton.Text = "Series";
            this.seriesRadioButton.UseVisualStyleBackColor = true;
            this.seriesRadioButton.CheckedChanged += new System.EventHandler(this.seriesRadioButton_CheckedChanged);
            // 
            // parallelRadioButton
            // 
            this.parallelRadioButton.AutoSize = true;
            this.parallelRadioButton.Location = new System.Drawing.Point(6, 65);
            this.parallelRadioButton.Name = "parallelRadioButton";
            this.parallelRadioButton.Size = new System.Drawing.Size(115, 29);
            this.parallelRadioButton.TabIndex = 24;
            this.parallelRadioButton.Text = "Parallel";
            this.parallelRadioButton.UseVisualStyleBackColor = true;
            this.parallelRadioButton.CheckedChanged += new System.EventHandler(this.parallelRadioButton_CheckedChanged);
            // 
            // ComplexImpedanceAnalyser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1094, 839);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.gbPortSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.Name = "ComplexImpedanceAnalyser";
            this.Text = "Complex Impedance Analyser";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ComplexImpedanceAnalyser_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gbPortSettings.ResumeLayout(false);
            this.gbPortSettings.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ImpedanceGraphTab.ResumeLayout(false);
            this.DebugTab.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gbPortSettings;
        private System.Windows.Forms.ComboBox portNameBox;
        private System.Windows.Forms.ComboBox baudRateBox;
        private System.Windows.Forms.Label lblComPort;
        private System.Windows.Forms.Label lblBaudRate;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.RichTextBox rtfTerminal;
        private System.Windows.Forms.Button Measure;
        private System.Windows.Forms.ComboBox timeoutBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ConnectBtn;
        private System.Windows.Forms.TextBox freqInput;
        private System.Windows.Forms.Button freqencySetButton;
        private System.Windows.Forms.Label freqLabel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ComboBox waveformbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button resetbutton;
        private System.Windows.Forms.ComboBox PGAGainBox;
        private System.Windows.Forms.Label PGAGainLabel;
        private System.Windows.Forms.Button SETPGAGAINButton;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage ImpedanceGraphTab;
        private System.Windows.Forms.TabPage DebugTab;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox ToggleRelayBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label measurementValueLabel;
        private System.Windows.Forms.Button autoRangeButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton dissipationRadioFactorButton;
        private System.Windows.Forms.RadioButton qualityFactorRadioButton;
        private System.Windows.Forms.RadioButton capacitanceRadioButton;
        private System.Windows.Forms.RadioButton impedanceRadioButton;
        private System.Windows.Forms.RadioButton seriesRadioButton;
        private System.Windows.Forms.RadioButton parallelRadioButton;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label comLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label rLabel;
        private System.Windows.Forms.Label QDlabel;
        private System.Windows.Forms.Label qdval;
    }
}

