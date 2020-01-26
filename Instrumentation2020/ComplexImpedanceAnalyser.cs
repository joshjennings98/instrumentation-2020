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
        int readTimeout = 1000;

        public class Message
        {
            public int ID;
            private string dataType;
            private string data;
            public string prettyVersion;

            public Message(string message)
            {
                this.ID = Int32.Parse(message.Substring(2, 2));
                dataType = this.getDataType(message.Substring(4, 2));
                data = this.getData(message.Substring(6, 4), dataType);
                this.prettyVersion = String.Concat("The ", dataType, " is ", data, ".\n");
            }
            private string getDataType(string data)
            {
                string dataType = "NULL"; // get rid of once I sort the exception case

                Console.WriteLine(data);
                switch (data)
                {
                    case "00":
                        dataType = "Magnitude";
                        break;
                    case "01":
                        dataType = "Phase";
                        break;
                    // Add the other cases
                    default:
                        throw new Exception("Run out of data types numbers");
                }
                return dataType;
            }

            private string getData(string data, string dataType)
            {
                string newData = data;

                Console.WriteLine(data, " ", dataType);
                switch (dataType)
                {
                    case "Magnitude":
                        newData = String.Concat(data, "Ω");
                        break;
                    case "Phase":
                        newData = String.Concat(data, "⌀");
                        break;
                    // Add the other cases
                    default:
                        throw new Exception("Run out of data types.");
                }
                return newData;
            }
        }

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
            cmbPortName.SelectedIndex = 0;
            cmbBaudRate.SelectedIndex = 0;
            timeoutBox.SelectedIndex = 0;
            rtfTerminal.Clear();

            // Get list of ports available and put into list
            List<string> ports = new List<string>();
            foreach (string s in SerialPort.GetPortNames()) {
                ports.Add(s);
            };

            // Set data source
            cmbPortName.DataSource = ports;

            // Create a new SerialPort object with default settings.
            _serialPort = new SerialPort();            
        }

        private void changePort()
        {
            Console.WriteLine(cmbBaudRate);
            // Make a new serial port object with the new baud rate
            if (cmbBaudRate.Text != "") {
                _serialPort = new SerialPort(cmbPortName.Text, Int32.Parse(cmbBaudRate.Text));
            }
            else
            {
                _serialPort = new SerialPort(cmbPortName.Text);
            }
            rtfTerminal.Text += "Set port to " + _serialPort.PortName + " with a baud rate of " + _serialPort.BaudRate.ToString() + ".\n";
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

        private string readSerialEvent(SerialPort port)
        {
            string data = "";
            int numParams = 1;
            var done = new Dictionary<int, Message>();

            try
            {
                // Assuming data is a 12 byte string |FF|id|datatype|data|checksum|                
                // Want all the information, so repeat until you have everything
                while (done.Count < numParams)
                {
                    string serialData = port.ReadLine(); // "FF000010002";
                    if (true) { // need to make this a thing that checks the checksum to make sure it is valid
                        // Convert into message form
                        Message msg = new Message(serialData);
                        
                        // Only add it to map if it isn't already in there
                        if (!done.ContainsKey(msg.ID)) {
                            done.Add(msg.ID, msg);
                        }
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
                data = _serialPort.PortName + " timed out.";
            }
            return data;
        }

        private void Measure_Click(object sender, EventArgs e)
        {
            // Set the timeout and open the port
            _serialPort.ReadTimeout = readTimeout;
            _serialPort.Open();

            // Try and read the serial port data
            string message = readSerialEvent(_serialPort);

            // Write the data and close the port
            rtfTerminal.Text += message + "\n";
            _serialPort.Close();
        }

        private void changeTimeOut()
        {
            // Read and set timeout data
            readTimeout = Int32.Parse(timeoutBox.Text);
            rtfTerminal.Text += "Port timeout set to " + readTimeout + "ms.\n";
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
            cmbPortName.SelectedIndex = 0;
            cmbBaudRate.SelectedIndex = 0;
            timeoutBox.SelectedIndex = 0;
            rtfTerminal.Clear();
            rtfTerminal.Text += "Settings reset to defaults.\n";
        }
    }
}
