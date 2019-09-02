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

namespace Barcode_Authenticator
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            portList.Items.Add("Kullanma");
            string[] ports = SerialPort.GetPortNames();  //Assign serial ports to a strng array
            foreach (string port in ports)
            {
                portList.Items.Add(port);   //Add serial ports to list
            }

            filePath.Text = Properties.Settings.Default.filePath;
            portList.SelectedItem = Properties.Settings.Default.selectedPort;
        }


        private void pathButton_Click(object sender, EventArgs e)
        {
            var folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();  //Creates new folderbrowserdialog

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath.Text = folderBrowserDialog1.SelectedPath+"\\";
            }
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.filePath = filePath.Text;
            Properties.Settings.Default.selectedPort = portList.SelectedItem.ToString();
            if (oldPass.Text == Properties.Settings.Default.adminPassword)
            {
                Properties.Settings.Default.formenPassword = newPass.Text;
            }
            Properties.Settings.Default.Save();
            this.DialogResult = DialogResult.OK;
        }

        private void oldPass_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyValue == (char)Keys.Enter))
            {
                newPass.Focus();
            }
        }

        private void newPass_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyValue == (char)Keys.Enter))
            {
                e.SuppressKeyPress = true;
            }
        }

    }
}
