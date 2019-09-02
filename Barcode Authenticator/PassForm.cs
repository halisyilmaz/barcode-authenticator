using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Barcode_Authenticator
{
    public partial class PassForm : Form
    {
        public PassForm()
        {
            InitializeComponent();
        }

        private void PassForm_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Enter)
            {
                if (textBox1.Text == Properties.Settings.Default.formenPassword || textBox1.Text == Properties.Settings.Default.adminPassword)
                {
                    // The password is ok.
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    // The password is invalid.
                    textBox1.Clear();
                    MessageBox.Show("Yanlış etiket okuttunuz!");
                    textBox1.Focus();
                }
            }
        }

    }
}
