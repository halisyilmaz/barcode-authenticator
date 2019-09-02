namespace Barcode_Authenticator
{
    partial class Settings
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
            this.portList = new System.Windows.Forms.ComboBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.pathLabel = new System.Windows.Forms.Label();
            this.portLabel = new System.Windows.Forms.Label();
            this.filePath = new System.Windows.Forms.TextBox();
            this.pathButton = new System.Windows.Forms.Button();
            this.oldPass = new System.Windows.Forms.TextBox();
            this.oldPassLabel = new System.Windows.Forms.Label();
            this.passChangeBox = new System.Windows.Forms.GroupBox();
            this.newPassLabel = new System.Windows.Forms.Label();
            this.newPass = new System.Windows.Forms.TextBox();
            this.passChangeBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // portList
            // 
            this.portList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.portList.FormattingEnabled = true;
            this.portList.Location = new System.Drawing.Point(113, 74);
            this.portList.Name = "portList";
            this.portList.Size = new System.Drawing.Size(109, 24);
            this.portList.TabIndex = 10;
            this.portList.Text = "Bağlı Değil";
            // 
            // saveButton
            // 
            this.saveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.saveButton.Location = new System.Drawing.Point(131, 231);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(134, 43);
            this.saveButton.TabIndex = 11;
            this.saveButton.Text = "Kaydet";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // pathLabel
            // 
            this.pathLabel.AutoSize = true;
            this.pathLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.pathLabel.Location = new System.Drawing.Point(21, 18);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(136, 16);
            this.pathLabel.TabIndex = 12;
            this.pathLabel.Text = "Üretim Planı Güncelle";
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.portLabel.Location = new System.Drawing.Point(21, 77);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(88, 16);
            this.portLabel.TabIndex = 13;
            this.portLabel.Text = "Port Güncelle";
            // 
            // filePath
            // 
            this.filePath.Enabled = false;
            this.filePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filePath.Location = new System.Drawing.Point(113, 38);
            this.filePath.Name = "filePath";
            this.filePath.Size = new System.Drawing.Size(263, 22);
            this.filePath.TabIndex = 14;
            // 
            // pathButton
            // 
            this.pathButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pathButton.Location = new System.Drawing.Point(15, 37);
            this.pathButton.Name = "pathButton";
            this.pathButton.Size = new System.Drawing.Size(92, 23);
            this.pathButton.TabIndex = 15;
            this.pathButton.Text = "Dosya Seç";
            this.pathButton.UseVisualStyleBackColor = true;
            this.pathButton.Click += new System.EventHandler(this.pathButton_Click);
            // 
            // oldPass
            // 
            this.oldPass.Location = new System.Drawing.Point(98, 29);
            this.oldPass.Name = "oldPass";
            this.oldPass.Size = new System.Drawing.Size(263, 22);
            this.oldPass.TabIndex = 17;
            this.oldPass.UseSystemPasswordChar = true;
            this.oldPass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.oldPass_KeyDown);
            // 
            // oldPassLabel
            // 
            this.oldPassLabel.AutoSize = true;
            this.oldPassLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.oldPassLabel.Location = new System.Drawing.Point(6, 32);
            this.oldPassLabel.Name = "oldPassLabel";
            this.oldPassLabel.Size = new System.Drawing.Size(86, 16);
            this.oldPassLabel.TabIndex = 18;
            this.oldPassLabel.Text = "Admin Şifresi";
            // 
            // passChangeBox
            // 
            this.passChangeBox.Controls.Add(this.newPassLabel);
            this.passChangeBox.Controls.Add(this.newPass);
            this.passChangeBox.Controls.Add(this.oldPassLabel);
            this.passChangeBox.Controls.Add(this.oldPass);
            this.passChangeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.passChangeBox.Location = new System.Drawing.Point(15, 113);
            this.passChangeBox.Name = "passChangeBox";
            this.passChangeBox.Size = new System.Drawing.Size(367, 103);
            this.passChangeBox.TabIndex = 19;
            this.passChangeBox.TabStop = false;
            this.passChangeBox.Text = "Formen Etiketi Güncelle";
            // 
            // newPassLabel
            // 
            this.newPassLabel.AutoSize = true;
            this.newPassLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.newPassLabel.Location = new System.Drawing.Point(6, 66);
            this.newPassLabel.Name = "newPassLabel";
            this.newPassLabel.Size = new System.Drawing.Size(71, 16);
            this.newPassLabel.TabIndex = 20;
            this.newPassLabel.Text = "Yeni Etiket";
            // 
            // newPass
            // 
            this.newPass.Location = new System.Drawing.Point(83, 63);
            this.newPass.Name = "newPass";
            this.newPass.Size = new System.Drawing.Size(278, 22);
            this.newPass.TabIndex = 19;
            this.newPass.UseSystemPasswordChar = true;
            this.newPass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.newPass_KeyDown);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 286);
            this.Controls.Add(this.passChangeBox);
            this.Controls.Add(this.pathButton);
            this.Controls.Add(this.filePath);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.pathLabel);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.portList);
            this.Name = "Settings";
            this.Text = "Ayarlar";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.passChangeBox.ResumeLayout(false);
            this.passChangeBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox portList;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label pathLabel;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.TextBox filePath;
        private System.Windows.Forms.Button pathButton;
        private System.Windows.Forms.TextBox oldPass;
        private System.Windows.Forms.Label oldPassLabel;
        private System.Windows.Forms.GroupBox passChangeBox;
        private System.Windows.Forms.Label newPassLabel;
        private System.Windows.Forms.TextBox newPass;
    }
}