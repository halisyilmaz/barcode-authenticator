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

namespace Barcode_Authenticator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            getFiles();
            openPort();
        }

        private void getFiles()
        {
            unlocked = true;    
            if (Directory.Exists(Properties.Settings.Default.filePath))
            {
                string[] days = Directory.EnumerateFiles(Properties.Settings.Default.filePath, "*.xls*", SearchOption.TopDirectoryOnly).Select(System.IO.Path.GetFileName).ToArray();
                //Adds file names with .xls or .xlsx extensions to an array
                for (int d = 0; d < days.Length; d++)
                {
                    fileList.Items.Add(days[d]);
                }
            barcodeRead.Focus();
            }
        }

        private void ReadExcelFile()
        {
            using (OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + selectedDay() + "; Extended Properties='Excel 12.0 Xml; HDR = YES;'"))
            {
                conn.Open();
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter("Select * From [Sayfa1$]", conn);
                System.Data.DataTable dataTable = new System.Data.DataTable();
                dataAdapter.Fill(dataTable);
                excelData.DataSource = dataTable;
                conn.Close();
                conn.Dispose();
            }
        }

        private void WriteExcelFile()   
        {
            //Writes OK to the status column of the production Excel file 
            //When we open the program again it can remember the last product
            //and we can also see the last produced product from Excel file if desired.

            using (OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + selectedDay() + "; Extended Properties='Excel 12.0 Xml; HDR = YES;'"))
            {
                //Connect to the selected days Excel file with OleDb
                conn.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;

                cmd.CommandText = "UPDATE [Sayfa1$] SET Durum = 'OK' WHERE Sıra=" + (excelData.CurrentCell.RowIndex + 1).ToString();    //Writes OK to the status column of appropriate row
                cmd.ExecuteNonQuery();

                conn.Close();
                conn.Dispose();

                excelData[3, excelData.CurrentCell.RowIndex].Value = "OK";  //Update excelData(dataGridView) for the current session
            }
        }

        private string selectedDay()    //Returns the selected filePath in the right format
        {
            string selected_day = Properties.Settings.Default.filePath + fileList.Items[fileList.SelectedIndex].ToString();
            return selected_day;
        }
        
        private void refreshButton_Click(object sender, EventArgs e)    //Refresh the fileList when desired
        {
            fileList.Items.Clear(); //Clears fileList
            getFiles();
            openPort();
        }

        private void openPort()
        {
            if (!(String.IsNullOrEmpty(Properties.Settings.Default.selectedPort) || Properties.Settings.Default.selectedPort == "Kullanma"))
            {
                if (serialPort1.IsOpen) serialPort1.Close();
                serialPort1.PortName = Properties.Settings.Default.selectedPort; //Assign selectedPort setting to Port Name to serialPort1 if it is not NULL 
                try
                {
                    serialPort1.Open(); //Open serial Port
                }
                catch
                {
                    Properties.Settings.Default.selectedPort = "Kullanma";
                    MessageBox.Show("Seri Port Açılamadı!");
                }
            }
        }

        private void settingsButton_Click(object sender, EventArgs e)   //Button to open settingsForm
        {
            Settings settings = new Settings(); //Creates a settingsForm
            if (settings.ShowDialog() == DialogResult.OK)   
            {
                fileList.Items.Clear();
                getFiles();
                openPort();
                barcodeRead.Focus();
            }
        }

        private void fileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fileList.SelectedIndex != -1)   //To avoid undesired clicks to empty space of fileList
            {
                selectedDayLabel.Text = selectedDay().Substring(Properties.Settings.Default.filePath.Length); //Update selected day label

                try
                {
                    ReadExcelFile();

                    while ("OK" == excelData.Rows[excelData.CurrentCell.RowIndex].Cells[3].Value.ToString())
                    //Skip rows in Excel file until status of the product is not OK
                    {
                        excelData.CurrentCell = excelData.Rows[excelData.CurrentCell.RowIndex + 1].Cells[0];
                        excelData.FirstDisplayedScrollingRowIndex = excelData.CurrentCell.RowIndex;
                    }

                    DataGridViewRow row0 = excelData.Rows[excelData.CurrentCell.RowIndex];
                    row0.Height = 50;
                    row0.DefaultCellStyle.BackColor = Color.LightGray;

                    if (String.IsNullOrEmpty(excelData.Rows[excelData.CurrentCell.RowIndex].Cells[1].Value.ToString())) 
                        //If the data of current cell is empty it means it is the end of file
                    {
                        MessageBox.Show("Liste sonu!"); 
                    }

                    barcodeRead.Focus();
                }
                catch
                {
                    MessageBox.Show("Excel dosyası açılamadı!");
                }

                
            }
        }

        private void barcodeRead_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyValue == (char)Keys.Enter) && (fileList.SelectedIndex != -1) && !String.IsNullOrEmpty(barcodeRead.Text) && !String.IsNullOrEmpty(excelData.Rows[excelData.CurrentCell.RowIndex].Cells[1].Value.ToString()))
                //Since barcode reader sends ENTER after barcode data this condition checks for ENTER and some other conditions that might cause bugs
            {
                if(unlocked == true)//Allows system to continue if the previous tag is placed in to its module
                {
                    if (barcodeRead.Text == excelData.Rows[excelData.CurrentCell.RowIndex].Cells[1].Value.ToString())   //Right barcode is scanned
                    {
                        statusLabel.Text = "Etiketi Yerleştir";
                        statusLabel.BackColor = System.Drawing.Color.Green;
                        timerStatus.Enabled = true;//Starts the timer for "Etiketi yerleştirin" label to be closed after a time period

                        WriteExcelFile();   //Write OK to Excel file

                        excelData.CurrentCell = excelData.Rows[excelData.CurrentCell.RowIndex + 1].Cells[0];    //Shifts current cell to desired place
                        excelData.FirstDisplayedScrollingRowIndex = excelData.CurrentCell.RowIndex;             //Scrolls to the selected cell

                        DataGridViewRow row0 = excelData.Rows[excelData.CurrentCell.RowIndex];  //Change selected rows style for better readibility
                        row0.Height = 50;
                        row0.DefaultCellStyle.BackColor = Color.LightGray;
                        DataGridViewRow rowBefore = excelData.Rows[excelData.CurrentCell.RowIndex - 1]; //Change previous row in dataGridView to default style
                        rowBefore.Height = 22;
                        rowBefore.DefaultCellStyle.BackColor = Color.White;

                        barcodeRead.Clear();

                        if (serialPort1.IsOpen)
                        {
                            try
                            {
                                serialPort1.Write("1"); //Send logic 1 to Arduino to tell barcode is right
                                unlocked = false;   //locks system until it gets 1 approval from arduino that shows tag is placed
                            }
                            catch
                            {
                                MessageBox.Show("Seri Port Hatası!");   //Shows error if it can'T send through the serial port
                            }

                        }
                    }
                    else    //Wrong barcode scanned
                    {
                        statusLabel.Text = "NG";
                        statusLabel.BackColor = System.Drawing.Color.Red;
                        barcodeRead.Clear();

                        PassForm passForm = new PassForm();  // Display the password form.
                        if (passForm.ShowDialog() != DialogResult.OK)
                        {
                            if (serialPort1.IsOpen) serialPort1.Close();
                            this.Close();   //Close all forms
                        }
                        else    //Turn statusLabel to idle mode
                        {
                            statusLabel.Text = "Barkodu Okutun";
                            statusLabel.BackColor = System.Drawing.Color.Gray;
                        }

                    }
                }
                else
                {
                    barcodeRead.Clear();
                    MessageBox.Show("Etiketi Yuvasına Yerleştirin!");
                }

                e.SuppressKeyPress = true;
            }            
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.Close();  //Eğer port açıksa kapat
        }

        private bool unlocked;  //locks the system until tag is placed in to its place and gets 1 as input data from serial port
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] content = new byte[serialPort1.BytesToRead];
            serialPort1.Read(content,0,content.Length);
            if (Encoding.Default.GetString(content) == "1") //Checks if the read data is 1. (Arduino sends 1 if tag is placed to module)
            {
                unlocked = true;
                //MessageBox.Show(unlocked.ToString());
            }
        }

        private void timerStatus_Tick(object sender, EventArgs e)
        {
            statusLabel.Text = "Barkodu Okutun";
            statusLabel.BackColor = System.Drawing.Color.Gray;
            timerStatus.Enabled = false;
        }

    }
}
