using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace Instrumentation2020
{
    public partial class ComplexImpedanceAnalyser : Form
    {
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

            rtfTerminal.Clear();

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
        }

        private void changePort()
        {
            Console.WriteLine(baudRateBox);
            // Make a new serial port object with the new baud rate
            if (baudRateBox.Text != "") {
                _serialPort = new SerialPort(portNameBox.Text, Int32.Parse(baudRateBox.Text));
            }
            else
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
            string data = "";
            var done = new Dictionary<int, Message>();

            try
            {
                var startTime = DateTime.UtcNow;
                var timeout = TimeSpan.FromMilliseconds(Convert.ToDouble(timeoutBox.Text)); // Calculate once for efficiency
                // Assuming data is a 12 byte string |FF|id|datatype|data|checksum|                
                // Want all the information, so repeat until you have everything or you timeout
                while (done.Count < numParams && DateTime.UtcNow - startTime < timeout) // Make sure it times out
                {
                    if (_serialDataRxFlag) { // If Data has been recieved
                        // Convert into message form
                        Message msg = new Message(_serialRxBuffer);
                        
                        // Only add it to map if it isn't already in there
                        if (!done.ContainsKey(msg.ID)) {
                            done.Add(msg.ID, msg);
                        }

                        _serialDataRxFlag = false;
                    }
                }

                if (DateTime.UtcNow - startTime > timeout)
                {
                    if (done.Count != 0)
                    {
                        data += _serialPort.PortName + " timed out but returned the following data:\n";
                    } 
                    else
                    {
                        data = _serialPort.PortName + " timed out and returned no data.";
                    }
                }

                // Make it pretty
                foreach (KeyValuePair<int, Message> message in done)
                {
                    data += message.Value.prettyVersion;
                }
            }
            catch (TimeoutException)
            {
                data = _serialPort.PortName + " timed out and returned no data.";
            }
            return data;
        }

        private void Measure_Click(object sender, EventArgs e)
        {
            // Set the timeout and open the port
            _serialPort.ReadTimeout = Int32.Parse(timeoutBox.Text);
            // Try and read the serial port data
            string message = readSerialEvent();

            // Write the data and close the port
            rtfTerminal.Text += message + "\n";
        }

        private void changeTimeOut()
        {
            // Read and set timeout data
            rtfTerminal.Text += "Port timeout set to " + timeoutBox.Text + "ms.\n";
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
            currentWaveform = "Sinewave";
            waveformbox.SelectedIndex = 0;
            setFrequency(false);
    }

        private void ConnectBtn_Click(object sender, EventArgs e)
        {
            if (!noCOMsFlag)
            {
                changePort();
                _serialDataRxFlag = false;
                _serialPort.Open();
                _serialPort.ReceivedBytesThreshold = 1;
                _serialPort.DataReceived += _serialPort_DataReceived;
                baudRateBox.Enabled = false;
                Measure.Enabled = true;
                ConnectBtn.Enabled = false;
                ConnectBtn.Visible = false;
                DisconnectButton.Enabled = true;
                DisconnectButton.Visible = true;
                portNameBox.Enabled = false;

                rtfTerminal.Clear();
                rtfTerminal.Text += "Connected to " + _serialPort.PortName + " with a baud rate of " + _serialPort.BaudRate.ToString() + ".\n";
            }
            else
            {
                rtfTerminal.Text += "Cannot connect, no COM ports available.\n";
            }
        }

        private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte header = (byte)_serialPort.ReadByte();

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
            }

            else {
                _serialPort.DiscardInBuffer();
            };
        }

        private void freqInput_TextChanged(object sender, EventArgs e)
        {
            if (freqInput.Text != "" && int.TryParse(freqInput.Text, out _))
            {
                currentFreq = Int32.Parse(freqInput.Text);
            }
        }

        private void setFrequency(bool useField = true)
        {
            int i = 0;
            if ((freqInput.Text != "" && int.TryParse(freqInput.Text, out i) && i < 0xFFFFFFF) || useField == false)
            {
                if (_serialPort.IsOpen)
                {
                    rtfTerminal.Text += "Frequency set to a " + currentFreq + "Hz " + currentWaveform + ".\n";
                    freqInput.Text = "";
                    byte[] message = formFreqMessage();
                    try
                    {
                        _serialPort.Write(message, 0, message.Length);
                    }
                    catch (Exception ex)
                    {
                        rtfTerminal.Text += "Set frequency failed failed! \nError: " + ex.Message + "\n";
                    }
                }
                else
                {
                    rtfTerminal.Text += "Serial port not open.\n";
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
            else if (i > 0xFFFFFFF)
            {
                rtfTerminal.Text += "Frequency must be less than 268435455.\n";
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
            baudRateBox.Enabled = true;
            Measure.Enabled = false;
            ConnectBtn.Enabled = true;
            ConnectBtn.Visible = true;
            DisconnectButton.Enabled = false;
            DisconnectButton.Visible = false;
            portNameBox.Enabled = true;
            _serialPort.Close();
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            disconnectSerialStuff();
        }
    }
}
