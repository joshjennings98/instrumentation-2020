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
            this.gbPortSettings = new System.Windows.Forms.GroupBox();
            this.waveformbox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.freqencySetButton = new System.Windows.Forms.Button();
            this.freqLabel = new System.Windows.Forms.Label();
            this.ConnectBtn = new System.Windows.Forms.Button();
            this.freqInput = new System.Windows.Forms.TextBox();
            this.Measure = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.timeoutBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.portNameBox = new System.Windows.Forms.ComboBox();
            this.baudRateBox = new System.Windows.Forms.ComboBox();
            this.lblComPort = new System.Windows.Forms.Label();
            this.lblBaudRate = new System.Windows.Forms.Label();
            this.rtfTerminal = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.resetbutton = new System.Windows.Forms.Button();
            this.gbPortSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbPortSettings
            // 
            this.gbPortSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
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
            this.gbPortSettings.Location = new System.Drawing.Point(12, 144);
            this.gbPortSettings.Name = "gbPortSettings";
            this.gbPortSettings.Size = new System.Drawing.Size(337, 133);
            this.gbPortSettings.TabIndex = 5;
            this.gbPortSettings.TabStop = false;
            this.gbPortSettings.Text = "COM Serial Port Settings";
            // 
            // waveformbox
            // 
            this.waveformbox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.waveformbox.DropDownWidth = 67;
            this.waveformbox.FormattingEnabled = true;
            this.waveformbox.ItemHeight = 13;
            this.waveformbox.Items.AddRange(new object[] {
            "Sine wave",
            "Square wave",
            "Triangle wave"});
            this.waveformbox.Location = new System.Drawing.Point(162, 74);
            this.waveformbox.Name = "waveformbox";
            this.waveformbox.Size = new System.Drawing.Size(94, 21);
            this.waveformbox.TabIndex = 18;
            this.waveformbox.SelectedIndexChanged += new System.EventHandler(this.waveformbox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(159, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Waveform:";
            // 
            // freqencySetButton
            // 
            this.freqencySetButton.Location = new System.Drawing.Point(264, 74);
            this.freqencySetButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.freqencySetButton.Name = "freqencySetButton";
            this.freqencySetButton.Size = new System.Drawing.Size(67, 20);
            this.freqencySetButton.TabIndex = 16;
            this.freqencySetButton.Text = "Set";
            this.freqencySetButton.UseVisualStyleBackColor = true;
            this.freqencySetButton.Click += new System.EventHandler(this.freqencySetButton_Click);
            // 
            // freqLabel
            // 
            this.freqLabel.AutoSize = true;
            this.freqLabel.Location = new System.Drawing.Point(12, 60);
            this.freqLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.freqLabel.Name = "freqLabel";
            this.freqLabel.Size = new System.Drawing.Size(82, 13);
            this.freqLabel.TabIndex = 13;
            this.freqLabel.Text = "Frequency (Hz):";
            // 
            // ConnectBtn
            // 
            this.ConnectBtn.Location = new System.Drawing.Point(88, 33);
            this.ConnectBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ConnectBtn.Name = "ConnectBtn";
            this.ConnectBtn.Size = new System.Drawing.Size(73, 21);
            this.ConnectBtn.TabIndex = 14;
            this.ConnectBtn.Text = "Connect";
            this.ConnectBtn.UseVisualStyleBackColor = true;
            this.ConnectBtn.Click += new System.EventHandler(this.ConnectBtn_Click);
            // 
            // freqInput
            // 
            this.freqInput.Location = new System.Drawing.Point(13, 74);
            this.freqInput.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.freqInput.Name = "freqInput";
            this.freqInput.Size = new System.Drawing.Size(140, 20);
            this.freqInput.TabIndex = 15;
            this.freqInput.TextChanged += new System.EventHandler(this.freqInput_TextChanged);
            // 
            // Measure
            // 
            this.Measure.Enabled = false;
            this.Measure.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Measure.Location = new System.Drawing.Point(12, 104);
            this.Measure.Name = "Measure";
            this.Measure.Size = new System.Drawing.Size(149, 23);
            this.Measure.TabIndex = 12;
            this.Measure.Text = "&Measure";
            this.Measure.UseVisualStyleBackColor = true;
            this.Measure.Click += new System.EventHandler(this.Measure_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(264, 104);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(67, 23);
            this.btnClear.TabIndex = 10;
            this.btnClear.Text = "&Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // timeoutBox
            // 
            this.timeoutBox.DropDownWidth = 67;
            this.timeoutBox.FormattingEnabled = true;
            this.timeoutBox.ItemHeight = 13;
            this.timeoutBox.Items.AddRange(new object[] {
            "1000",
            "2000",
            "5000",
            "7000",
            "10000"});
            this.timeoutBox.Location = new System.Drawing.Point(180, 33);
            this.timeoutBox.Name = "timeoutBox";
            this.timeoutBox.Size = new System.Drawing.Size(69, 21);
            this.timeoutBox.TabIndex = 5;
            this.timeoutBox.SelectedIndexChanged += new System.EventHandler(this.timeoutBox_SelectedIndexChanged);
            this.timeoutBox.TextUpdate += new System.EventHandler(this.timeoutBox_TextUpdate);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(175, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Timeout (ms):";
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
            this.portNameBox.Location = new System.Drawing.Point(13, 33);
            this.portNameBox.Name = "portNameBox";
            this.portNameBox.Size = new System.Drawing.Size(67, 21);
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
            this.baudRateBox.Location = new System.Drawing.Point(264, 33);
            this.baudRateBox.Name = "baudRateBox";
            this.baudRateBox.Size = new System.Drawing.Size(69, 21);
            this.baudRateBox.TabIndex = 3;
            this.baudRateBox.SelectedIndexChanged += new System.EventHandler(this.cmbBaudRate_SelectedIndexChanged);
            this.baudRateBox.TextUpdate += new System.EventHandler(this.cmbBaudRate_TextUpdate);
            // 
            // lblComPort
            // 
            this.lblComPort.AutoSize = true;
            this.lblComPort.Location = new System.Drawing.Point(12, 17);
            this.lblComPort.Name = "lblComPort";
            this.lblComPort.Size = new System.Drawing.Size(56, 13);
            this.lblComPort.TabIndex = 0;
            this.lblComPort.Text = "COM Port:";
            // 
            // lblBaudRate
            // 
            this.lblBaudRate.AutoSize = true;
            this.lblBaudRate.Location = new System.Drawing.Point(262, 18);
            this.lblBaudRate.Name = "lblBaudRate";
            this.lblBaudRate.Size = new System.Drawing.Size(61, 13);
            this.lblBaudRate.TabIndex = 2;
            this.lblBaudRate.Text = "Baud Rate:";
            // 
            // rtfTerminal
            // 
            this.rtfTerminal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtfTerminal.Location = new System.Drawing.Point(12, 8);
            this.rtfTerminal.Name = "rtfTerminal";
            this.rtfTerminal.Size = new System.Drawing.Size(338, 131);
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
            // resetbutton
            // 
            this.resetbutton.Location = new System.Drawing.Point(176, 104);
            this.resetbutton.Name = "resetbutton";
            this.resetbutton.Size = new System.Drawing.Size(72, 23);
            this.resetbutton.TabIndex = 13;
            this.resetbutton.Text = "Reset";
            this.resetbutton.UseVisualStyleBackColor = true;
            this.resetbutton.Click += new System.EventHandler(this.resetbutton_Click);
            // 
            // ComplexImpedanceAnalyser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 284);
            this.Controls.Add(this.rtfTerminal);
            this.Controls.Add(this.gbPortSettings);
            this.Name = "ComplexImpedanceAnalyser";
            this.Text = "Impedance Analyser";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gbPortSettings.ResumeLayout(false);
            this.gbPortSettings.PerformLayout();
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
    }
}

