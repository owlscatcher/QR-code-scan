namespace QR_code_scan
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.Stream_pictureBox = new System.Windows.Forms.PictureBox();
            this.QrDecode_textBox = new System.Windows.Forms.TextBox();
            this.StartStop_button = new System.Windows.Forms.Button();
            this.Device_listBox = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.Stream_pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // Stream_pictureBox
            // 
            this.Stream_pictureBox.Location = new System.Drawing.Point(12, 12);
            this.Stream_pictureBox.Name = "Stream_pictureBox";
            this.Stream_pictureBox.Size = new System.Drawing.Size(400, 400);
            this.Stream_pictureBox.TabIndex = 0;
            this.Stream_pictureBox.TabStop = false;
            // 
            // QrDecode_textBox
            // 
            this.QrDecode_textBox.Location = new System.Drawing.Point(418, 165);
            this.QrDecode_textBox.Multiline = true;
            this.QrDecode_textBox.Name = "QrDecode_textBox";
            this.QrDecode_textBox.ReadOnly = true;
            this.QrDecode_textBox.Size = new System.Drawing.Size(249, 247);
            this.QrDecode_textBox.TabIndex = 1;
            // 
            // StartStop_button
            // 
            this.StartStop_button.Location = new System.Drawing.Point(418, 136);
            this.StartStop_button.Name = "StartStop_button";
            this.StartStop_button.Size = new System.Drawing.Size(249, 23);
            this.StartStop_button.TabIndex = 2;
            this.StartStop_button.Text = "Start scan";
            this.StartStop_button.UseVisualStyleBackColor = true;
            this.StartStop_button.Click += new System.EventHandler(this.StartStop_button_Click);
            // 
            // Device_listBox
            // 
            this.Device_listBox.FormattingEnabled = true;
            this.Device_listBox.Location = new System.Drawing.Point(418, 12);
            this.Device_listBox.Name = "Device_listBox";
            this.Device_listBox.Size = new System.Drawing.Size(249, 121);
            this.Device_listBox.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 429);
            this.Controls.Add(this.Device_listBox);
            this.Controls.Add(this.StartStop_button);
            this.Controls.Add(this.QrDecode_textBox);
            this.Controls.Add(this.Stream_pictureBox);
            this.Name = "MainForm";
            this.Text = "Radioactive Raccoon: QR scan";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Stream_pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Stream_pictureBox;
        private System.Windows.Forms.TextBox QrDecode_textBox;
        private System.Windows.Forms.Button StartStop_button;
        private System.Windows.Forms.ListBox Device_listBox;
    }
}

