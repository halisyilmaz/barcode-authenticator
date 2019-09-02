using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Data.OleDb;
using System.Timers;
using System.Diagnostics;

namespace Yazaki_C_Sharp_App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if(Directory.Exists(Properties.Settings.Default.filePath))
            {
                string[] days = Directory.GetFiles(Properties.Settings.Default.filePath);
                for (int d = 0; d < days.Length; d++)
                {
                    listBox1.Items.Add(days[d].Substring(85));
                }
            }
            

            string[] ports = SerialPort.GetPortNames();  //Seri portları diziye ekleme
            foreach (string port in ports)
            {
                comboBox1.Items.Add(port); //Seri portları comBox1' ekleme
            }
        }

        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
        }

        private void ReadExcelFile()
        {
            using (OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + listBox1.Items[listBox1.SelectedIndex].ToString() + "; Extended Properties='Excel 12.0 Xml; HDR = YES;'"))
            {
                conn.Open();
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter("Select * From [Sayfa1$]", conn);
                System.Data.DataTable dataTable = new System.Data.DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                conn.Close();
                conn.Dispose();
            }
        }
            private void WriteExcelFile()
            {

            using (OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + listBox1.Items[listBox1.SelectedIndex].ToString() + "; Extended Properties='Excel 12.0 Xml; HDR = YES;'"))
                {
                    conn.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = conn;

                    cmd.CommandText = "UPDATE [Sayfa1$] SET Durum = 'OK' WHERE Sıra=" + (dataGridView1.CurrentCell.RowIndex+1).ToString();
                    cmd.ExecuteNonQuery();

                    conn.Close();
                    conn.Dispose();

                dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value = "OK";
                }
            }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            { 
                string selected_day = listBox1.Items[listBox1.SelectedIndex].ToString();
                label3.Text = selected_day.Substring(selected_day.IndexOf("Üretim") + 7);

                ReadExcelFile();
                
                DataGridViewColumn column0 = dataGridView1.Columns[0];
                column0.Width = 40;
                DataGridViewColumn column1 = dataGridView1.Columns[1];
                column1.Width = 139;
                DataGridViewColumn column2 = dataGridView1.Columns[2];
                column2.Width = 110;
                DataGridViewColumn column3 = dataGridView1.Columns[3];
                column3.Width = 50;

                while ("OK" == dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value.ToString())
                {
                    dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex + 1].Cells[0];
                    dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.CurrentCell.RowIndex;
                }

                DataGridViewRow row0 = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];
                row0.Height = 40;
                row0.DefaultCellStyle.BackColor = Color.LightGray;
                textBox1.Focus();
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (Directory.Exists(Properties.Settings.Default.filePath))
            {
                string[] days = Directory.GetFiles(Properties.Settings.Default.filePath);
                for (int d = 0; d < days.Length; d++)
                {
                    listBox1.Items.Add(days[d]);
                }
            }

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyValue == (char)Keys.Enter) && (listBox1.SelectedIndex !=-1))
            {
                
                if (textBox1.Text == dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString())
                {
                    label4.Text = "OK";
                    label4.BackColor = System.Drawing.Color.Green;
                    timerStatus.Enabled = true;
                    WriteExcelFile();

                    dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex + 1].Cells[0];
                    dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.CurrentCell.RowIndex;
                    textBox1.Clear();
                    if (serialPort1.IsOpen)
                    {
                        serialPort1.Write("1");
                    }


                }
                else
                {
                    label4.Text = "NG";
                    label4.BackColor = System.Drawing.Color.Red;
                    //MessageBox.Show("Lütfen doğru etiketi okutunuz", "Hatalı Etiket", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Clear();
                    // Display the password form.
                    PassForm passForm = new PassForm();
                    if (passForm.ShowDialog() != DialogResult.OK)
                    {
                        this.Close();
                    }
                    else
                    {
                        label4.Text = "Durum";
                        label4.BackColor = System.Drawing.Color.Gray;
                    }

                }

                DataGridViewRow row0 = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];
                row0.Height = 40;
                row0.DefaultCellStyle.BackColor = Color.LightGray;
                DataGridViewRow rowBefore = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex-1];
                rowBefore.Height = 22;
                rowBefore.DefaultCellStyle.BackColor = Color.White;

                e.SuppressKeyPress = true;
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort1.PortName = comboBox1.SelectedItem.ToString(); //comboBox1'de seçili olan portu port ismine ata
            serialPort1.Open(); //Seri portu aç
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.Close();  //Eğer port açıksa kapat

        }

        private void timerStatus_Tick(object sender, EventArgs e)
        {
            label4.Text = "Durum";
            label4.BackColor = System.Drawing.Color.Gray;
            timerStatus.Enabled = false;
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            if (settings.ShowDialog() != DialogResult.OK)
            {
                
            }
        }
    }
}
