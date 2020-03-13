using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;
using System.Timers;
using ZedGraph;

namespace Instrumentation2020
{
    public partial class ComplexImpedanceAnalyser : Form
    {
        Thread t;

        static SerialPort _serialPort;
        static bool _serialDataRxFlag;
        static byte[] _serialRxBuffer;
        static int numParams = 2;

        public class Message
        {
            public byte ID;
            private string data;
            public string messageType;
            public string prettyVersion;

            public Message(byte[] message)
            {
                this.ID = message[1];
                messageType = this.getMessageType(ID);
                data = this.getData(message, ID);
                this.prettyVersion = String.Concat("The ", messageType, " is ", data, ".\n");
            }

            private string getMessageType(byte data)
            {
                string dataType = "NULL"; // get rid of once I sort the exception case

                switch (data)
                {
                    case 0x00:
                        dataType = "Not Assigned";
                        break;
                    case 0x01:
                        dataType = "Status";
                        break;
                    // Add the other cases
                    default:
                        throw new Exception("Data ID not assigned");
                }
                return dataType;
            }

            private string getData(byte[] message, byte ID)
            {
                string newData = data;

                switch (ID)
                {
                    case 0x00:
                        newData = String.Concat(data, "Ω");
                        break;
                    case 0x01:
                        // Status Message, Extract Sys Count
                        UInt32 count = 0;
                        count |= (UInt32)(message[2] << 24);
                        count |= (UInt32)(message[3] << 16);
                        count |= (UInt32)(message[4] << 8);
                        count |= (UInt32)(message[5] << 4);
                        newData = count.ToString();
                        break;
                    // Add the other cases
                    default:
                        throw new Exception("Run out of data types.");
                }
                return newData;
            }
        }

        private bool noCOMsFlag = false;
        private int currentFreq = 1000;
        private string currentWaveform = "Sinewave";
        private bool connectBool = false;
        private int PGAGainValue = 1;
        private byte toggleRelayValue = 0x01;
        private byte statusFlag = 0x00;
        private int timeoutA = 0;
        private int magnitude = 0;
        private int phase = 0;
        private bool receivedMeasureFlag = false;

        public ComplexImpedanceAnalyser()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // Clear and scroll back to top
            rtfTerminal.Clear();
            rtfTerminal.SelectionStart = 0;
            rtfTerminal.ScrollToCaret();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Set initial indexes (and clear display so no clutter)
            portNameBox.SelectedIndex = 0;
            baudRateBox.SelectedIndex = 7;
            timeoutBox.SelectedIndex = 0;
            waveformbox.SelectedIndex = 0;
            PGAGainBox.SelectedIndex = 0;
            ToggleRelayBox.SelectedIndex = 0;

            rtfTerminal.Clear();

            InitGraph(zedGraphControl1);

            // Get list of ports available and put into list
            List<string> ports = new List<string>();
            foreach (string s in SerialPort.GetPortNames()) {
                ports.Add(s);
            };

            if (ports.Count == 0)
            {
                rtfTerminal.Text += "No COM ports available.\n";
                noCOMsFlag = true;
            }

            // Set data source
            portNameBox.DataSource = ports;

            // Create a new SerialPort object with default settings.
            _serialPort = new SerialPort();

            t = new Thread(DoThisAllTheTime);
            t.Start();
        }

        private void changePort()
        {
            
            // Make a new serial port object with the new baud rate
            if (baudRateBox.Text != "" && !noCOMsFlag) {
                _serialPort = new SerialPort(portNameBox.Text, Int32.Parse(baudRateBox.Text));
            }
            else if (!noCOMsFlag)
            {
                _serialPort = new SerialPort(portNameBox.Text);
            }
        }

        private byte[] checksum(byte[] data)
        {
            byte checksum = 0x00;
            
            foreach (var b in data)
            {
                checksum ^= b;
            }

            return new byte[] {checksum};
        }

        private static byte[] CombineByteArrays(byte[][] arrays)
        {
            byte[] bytes = new byte[arrays.Sum(a => a.Length)];
            int offset = 0;

            foreach (byte[] array in arrays)
            {
                Buffer.BlockCopy(array, 0, bytes, offset, array.Length);
                offset += array.Length;
            }

            return bytes;
        }

        private byte[] formPGAGainMessage()
        {
            
            byte[] id = { 0xFF, 0x05, 0x08 };
            byte[] gain = { BitConverter.GetBytes(PGAGainValue)[0] };
            byte[] empty = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            byte[] data = CombineByteArrays(new[] { id, gain, empty });
            byte[] message = CombineByteArrays(new[] { data, checksum(data) });

            //string x = BitConverter.ToString(message);
            //rtfTerminal.Text += "Sending message: " + x + "\n";

            return message;
        }

        private byte[] formFreqMessage()
        {
            byte[] waveform = {0x00, 0x00, 0x20, 0x00};
       
            switch (currentWaveform)
            {
                case "Square wave":
                    waveform = new byte[] {0x00, 0x00, 0x20, 0x28};
                    break;
                case "Triangle wave":
                    waveform = new byte[] {0x00, 0x00, 0x20, 0x02};
                    break;
                case "Sine wave":
                    waveform = new byte[] {0x00, 0x00, 0x20, 0x00};
                    break;
            }
            byte[] id = {0xFF, 0x04, 0x08};
            byte[] frequency = BitConverter.GetBytes(currentFreq);
            Array.Reverse(frequency);
            byte[] data = CombineByteArrays(new [] { id, frequency, waveform});//"FF0408" + frequency + waveform;
            byte[] message = CombineByteArrays(new[] { data, checksum(data) });

            //string x = BitConverter.ToString(message);
            //rtfTerminal.Text += "Sending message: " + x + "\n";

            return message;
        }

        private void cmbPortName_SelectedIndexChanged(object sender, EventArgs e)
        {
            changePort();
        }
        
        private void cmbBaudRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            changePort();
        }

        private void cmbBaudRate_TextUpdate(object sender, EventArgs e)
        {
            changePort();
        }

        private void rtfTerminal_TextChanged(object sender, EventArgs e)
        {
            // Auto scroll to bottom
            rtfTerminal.SelectionStart = rtfTerminal.Text.Length;
            rtfTerminal.ScrollToCaret();
        }

        private string readSerialEvent()
        {
            return "";
        }

        private byte[] formSendMeasureMessage()
        {

            byte[] id = { 0xFF, 0x02};
            byte[] empty = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            byte[] data = CombineByteArrays(new[] { id, empty });
            byte[] message = CombineByteArrays(new[] { data, checksum(data) });

            return message;
        }

        private void Measure_Click(object sender, EventArgs e)
        {
            if (!_serialPort.IsOpen)
            {
                rtfTerminal.Text += "Serial port not open.\n";
            }
            else
            {
                byte[] message = formSendMeasureMessage();
                try
                {
                    Measure.Enabled = false;
                    _serialPort.Write(message, 0, message.Length);
                }
                catch (Exception ex)
                {
                    rtfTerminal.Text += "Measure failed! \nError: " + ex.Message + "\n";
                }
            }
        }

        private void changeTimeOut()
        {
            // Read and set timeout data
            rtfTerminal.Text += "Port timeout set to " + timeoutBox.Text + "ms.\n";
        }

        private byte[] formRelayMessage()
        {

            byte[] id = { 0xFF, 0x06 };
            byte[] empty = { 0x08, toggleRelayValue, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00};

            byte[] data = CombineByteArrays(new[] { id, empty });
            byte[] message = CombineByteArrays(new[] { data, checksum(data) });

            return message;
        }

        private void changeRelay()
        {           

            byte[] message = formRelayMessage();

            try
            {
                _serialPort.Write(message, 0, message.Length);
            }
            catch (Exception ex)
            {
                rtfTerminal.Text += "Failed to toggle relay! \nError: " + ex.Message + "\n";
            }
        }

        private void timeoutBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeTimeOut();
        }

        private void timeoutBox_TextUpdate(object sender, EventArgs e)
        {
            changeTimeOut();
        }

        private void resetbutton_Click(object sender, EventArgs e)
        {
            // Reset everything
            rtfTerminal.Clear();
            rtfTerminal.Text += "Settings reset to defaults:\n";

            if (!noCOMsFlag)
            {
                portNameBox.SelectedIndex = 0;
            } else
            {
                rtfTerminal.Text += "No COM ports available.\n";
            }
            //baudRateBox.SelectedIndex = 7;
            timeoutBox.SelectedIndex = 0;
            currentFreq = 1000;
            freqInput.Text = "1000";
            currentWaveform = "Sinewave";
            waveformbox.SelectedIndex = 0;
            PGAGainBox.SelectedIndex = 0;
            setFrequency(false);
            PGAGainValue = 1;
            setPGAGain();
            magnitude = 0;
            phase = 0;
            Measure.Enabled = true;
    }

        private void ConnectBtn_Click(object sender, EventArgs e)
        {
            if (connectBool == false)
            {
                if (!noCOMsFlag)
                {
                    changePort();
                    _serialDataRxFlag = false;
                    _serialPort.Open();
                    _serialPort.ReceivedBytesThreshold = 12;
                    _serialPort.DataReceived += _serialPort_DataReceived;
                    baudRateBox.Enabled = false;
                    Measure.Enabled = true;
                    portNameBox.Enabled = false;
                    PGAGainBox.Enabled = true;
                    SETPGAGAINButton.Enabled = true;
                    freqInput.Enabled = true;
                    freqencySetButton.Enabled = true;
                    Measure.Enabled = true;
                    timeoutBox.Enabled = true;
                    waveformbox.Enabled = true;
                    resetbutton.Enabled = true;

                    button1.Enabled = true;
                    ToggleRelayBox.Enabled = true;

                    rtfTerminal.Clear();
                    rtfTerminal.Text += "Connected to " + _serialPort.PortName + " with a baud rate of " + _serialPort.BaudRate.ToString() + ".\n";

                    ConnectBtn.Text = "Disconnect";
                    connectBool = true;
                }
                else
                {
                    rtfTerminal.Text += "Cannot connect, no COM ports available.\n";
                }
            } else if (connectBool == true)
            {
                disconnectSerialStuff();
                connectBool = false;
            }
        }

        private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte header = 0;
            do
            {
                header = (byte)_serialPort.ReadByte();
            } while (header != 0xFF && _serialPort.BytesToRead > 0);
            

            if (header == 0xFF)
            {
                byte[] buffer = new byte[12];

                buffer[0] = 0xFF;

                for (int i = 1; i < 12; i++)
                {
                    buffer[i] = (byte)_serialPort.ReadByte();
                }

                // Calculate Checksum and Check Data is Valid

                // Copy Data to Global Buffer
                _serialRxBuffer = buffer;
                // Set a message recieved flag
                _serialDataRxFlag = true;
               
                if (_serialRxBuffer[1] == 0x01)
                {
                    statusFlag = _serialRxBuffer[3];
                }
                if (_serialRxBuffer[1] == 0x03)
                {

                    magnitude = BitConverter.ToInt32(_serialRxBuffer.Skip(3).Take(4).ToArray(), 0);
                    phase = BitConverter.ToInt32(_serialRxBuffer.Skip(7).Take(4).ToArray(), 0);
                    receivedMeasureFlag = true;
                }
            }

            else {
               // _serialPort.DiscardInBuffer();
            };
        }

        private void freqInput_TextChanged(object sender, EventArgs e)
        {
            if (freqInput.Text != "" && int.TryParse(freqInput.Text, out _))
            {
                currentFreq = Int32.Parse(freqInput.Text);
            }
        }

        private void setPGAGain() {
            if (!_serialPort.IsOpen)
            {
                rtfTerminal.Text += "Serial port not open.\n";
            }
            else
            {
                rtfTerminal.Text += "PGA Gain set to " + PGAGainValue + ".\n";
                byte[] message = formPGAGainMessage();
                try
                {
                    _serialPort.Write(message, 0, message.Length);
                }
                catch (Exception ex)
                {
                    rtfTerminal.Text += "Set PGA Gain failed! \nError: " + ex.Message + "\n";
                }
            }
        }

        private void setFrequency(bool useField = true)
        {
            int i = 0;
            if (!_serialPort.IsOpen)
            {
                rtfTerminal.Text += "Serial port not open.\n";
            }
            else if ((freqInput.Text != "" && int.TryParse(freqInput.Text, out i) && i < 0xFFFFFFF) || useField == false)
            {
                rtfTerminal.Text += "Frequency set to a " + currentFreq + "Hz " + currentWaveform + ".\n";
                //freqInput.Text = "";
                byte[] message = formFreqMessage();
                try
                {
                    _serialPort.Write(message, 0, message.Length);
                }
                catch (Exception ex)
                {
                    rtfTerminal.Text += "Set frequency failed! \nError: " + ex.Message + "\n";
                }
            }
            else if (freqInput.Text == "")
            {
                rtfTerminal.Text += "Please specify a frequency.\n";
            }
            else if (!int.TryParse(freqInput.Text, out _))
            {
                rtfTerminal.Text += "Frequency must be a number.\n";
            }
            else if (i > 0x989680)
            {
                rtfTerminal.Text += "Frequency must be less than 10MHz.\n";
            }
            else
            {
                rtfTerminal.Text += "Invalid input.\n";
            }
        }

        private void freqencySetButton_Click(object sender, EventArgs e)
        {
            setFrequency();
        }

        private void waveformbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentWaveform = waveformbox.Text;
        }

        private void disconnectSerialStuff()
        {
            _serialPort.Close();
            baudRateBox.Enabled = true;
            Measure.Enabled = false;
            ConnectBtn.Text = "Connect";
            portNameBox.Enabled = true;
            PGAGainBox.Enabled = false;
            SETPGAGAINButton.Enabled = false;
            freqInput.Enabled = false;
            freqencySetButton.Enabled = false;
            Measure.Enabled = false;
            timeoutBox.Enabled = false;
            waveformbox.Enabled = false;
            resetbutton.Enabled = false;
            button1.Enabled = false;
            ToggleRelayBox.Enabled = false;
        }

        private void PGAGainBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            PGAGainValue = Int32.Parse(PGAGainBox.Text);
        }

        private void SETPGAGAINButton_Click(object sender, EventArgs e)
        {
            setPGAGain();
        }


        private void InitGraph(ZedGraphControl zgc)
        {
            GraphPane myPane = zgc.GraphPane;

            myPane.Title.Text = "Complex Impedance";
            myPane.XAxis.Title.Text = "Real";
            myPane.YAxis.Title.Text = "Imaginary";

            //myPane.XAxis.MajorGrid.IsVisible = true;
            //myPane.YAxis.MajorGrid.IsVisible = true;

            myPane.XAxis.Scale.Min = 0;
            myPane.XAxis.Scale.Max = 100;
            myPane.XAxis.Scale.MajorStep = 10;

            myPane.YAxis.Scale.Min = 0;
            myPane.YAxis.Scale.Max = 100;
            myPane.YAxis.Scale.MajorStep = 10;

            myPane.YAxis.MajorGrid.IsZeroLine = true;
            myPane.XAxis.MajorGrid.IsZeroLine = true;

            ArrowObj arrow = new ArrowObj(Color.Red, 30, 0.0, 0.0, 0.0, 0.0);
            arrow.Line.Width = 2;
            myPane.GraphObjList.Add(arrow);
        }

        public void updateGraph(ZedGraphControl zgc, double x, double y)
        {
            GraphPane myPane = zgc.GraphPane;
            ArrowObj arrow = new ArrowObj(Color.Red, 30, 0.0, 0.0, x, y);
            arrow.Line.Width = 2;
            myPane.GraphObjList.RemoveAt(0);
            myPane.GraphObjList.Add(arrow);
            zgc.Refresh();
        }


        public void DoThisAllTheTime()
        {            
            while (true)
            {
                //you need to use Invoke because the new thread can't access the UI elements directly
                Thread.Sleep(10);
                MethodInvoker mi = delegate () {
                    //updateGraph(zedGraphControl1, (double)(Control.MousePosition.X) / Screen.PrimaryScreen.Bounds.Width, (double)(Screen.PrimaryScreen.Bounds.Height - Control.MousePosition.Y) / Screen.PrimaryScreen.Bounds.Height);
                    double phaseRad = phase * Math.PI / 180.0;
                    updateGraph(zedGraphControl1, magnitude * Math.Cos(phaseRad), magnitude * Math.Sin(phaseRad));
                };
                this.Invoke(mi);
            }
        }

        private void ComplexImpedanceAnalyser_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (t.IsAlive) {
                t.Abort();
            }
        }

        private void toolStripProgressBar1_Click(object sender, EventArgs e)
        {

        }

        private void freqInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                setFrequency();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (statusFlag != 0x00)
            {
                rtfTerminal.Text += "syscount: " + statusFlag + ".\n";
                statusFlag = 0x00;
                timeoutA = 0;
            }
            timeoutA += 1;
            if (timeoutA > 5)
            {
                //rtfTerminal.Text += "No connection.\n";
            }
            if (receivedMeasureFlag)
            {
                rtfTerminal.Text += "Magnitude: " + magnitude + ".\n";
                rtfTerminal.Text += "Phase: " + phase + ".\n";
                measurementValueLabel.Text = "Impedance: " + magnitude + "∠" + phase;
                receivedMeasureFlag = false;
                Measure.Enabled = true;
            }
        }

        private void resetTimer()
        {
            timer1.Stop();
            timer1.Start();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //Test
            resetTimer();
        }

        private void ToggleRelayBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ToggleRelayBox.Text == "1M")
            {
                toggleRelayValue = 0x01;
            }
            else if (ToggleRelayBox.Text == "100k")
            {
                toggleRelayValue = 0x02;
            }
            else if (ToggleRelayBox.Text == "10k")
            {
                toggleRelayValue = 0x03;
            }
            else if (ToggleRelayBox.Text == "1k")
            {
                toggleRelayValue = 0x04;
            }
            else if (ToggleRelayBox.Text == "100")
            {
                toggleRelayValue = 0x05;
            }
            else if (ToggleRelayBox.Text == "10")
            {
                toggleRelayValue = 0x06;
            }
            rtfTerminal.Text += "Selected relay set to " + toggleRelayValue + ".\n";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            changeRelay();
        }
    }
}
