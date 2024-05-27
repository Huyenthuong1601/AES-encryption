using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ATHTN0011
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void serialPort_DataReceived(object sender, EventArgs e)
        {
            string read = serialPort1.ReadExisting();
            inTextBox.Text = inTextBox.Text + read;
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            comPort.Text = "";
            comPort.Items.Clear();
            String[] ports = SerialPort.GetPortNames();
            comPort.Items.AddRange(ports);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (ConnectPort() == true)
            {
                btnConnect.Enabled = false;
                comPort.Enabled = false;
               baudrate.Enabled = false;

            }
        }
        public bool ConnectPort()
        {  
            try
            {
                if (comPort.Text != "" || baudrate.Text != "")
                {
                    serialPort1.PortName = comPort.Text;
                    serialPort1.Open();
                    serialPort1.BaudRate = Int32.Parse(baudrate.Text);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }
            return false;
        }

        private void outTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void inTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
                try
                {
                    if (!(serialPort1.IsOpen))   
                    {
                        serialPort1.Open();
                    }
                    serialPort1.Write(outTextBox.Text);
                  //  outTextBox.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
                }
            
            /*try
            {
                if (!(serialPort1.IsOpen))
                {
                    serialPort1.Open();
                }
                serialPort1.Write(outTextBox.Text);
                outTextBox.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }*/

        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            this.Invoke(new EventHandler(serialPort_DataReceived));
        }

        private void baudrate_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
