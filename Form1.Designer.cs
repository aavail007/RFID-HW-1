namespace WindowsFormsApplication6
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.btnReadData = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbSector = new System.Windows.Forms.ComboBox();
            this.cbBlock = new System.Windows.Forms.ComboBox();
            this.cbKeyType = new System.Windows.Forms.ComboBox();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(111, 18);
            this.Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(63, 18);
            this.Label2.TabIndex = 12;
            this.Label2.Text = "Block : ";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(13, 18);
            this.Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(67, 18);
            this.Label1.TabIndex = 13;
            this.Label1.Text = "Sector : ";
            this.Label1.Click += new System.EventHandler(this.Label1_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(171, 159);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(128, 34);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(16, 111);
            this.txtResult.Margin = new System.Windows.Forms.Padding(4);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(311, 29);
            this.txtResult.TabIndex = 10;
            this.txtResult.TextChanged += new System.EventHandler(this.txbGetUIDd_TextChanged);
            // 
            // btnReadData
            // 
            this.btnReadData.Location = new System.Drawing.Point(357, 111);
            this.btnReadData.Margin = new System.Windows.Forms.Padding(4);
            this.btnReadData.Name = "btnReadData";
            this.btnReadData.Size = new System.Drawing.Size(112, 34);
            this.btnReadData.TabIndex = 8;
            this.btnReadData.Text = "Read Data";
            this.btnReadData.UseVisualStyleBackColor = true;
            this.btnReadData.Click += new System.EventHandler(this.btnReadData_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(215, 18);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 18);
            this.label3.TabIndex = 14;
            this.label3.Text = "KeyAB : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(318, 18);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 18);
            this.label4.TabIndex = 15;
            this.label4.Text = "Load Key :";
            // 
            // cbSector
            // 
            this.cbSector.FormattingEnabled = true;
            this.cbSector.Location = new System.Drawing.Point(16, 51);
            this.cbSector.Name = "cbSector";
            this.cbSector.Size = new System.Drawing.Size(81, 26);
            this.cbSector.TabIndex = 16;
            this.cbSector.Text = "00";
            // 
            // cbBlock
            // 
            this.cbBlock.FormattingEnabled = true;
            this.cbBlock.Location = new System.Drawing.Point(114, 51);
            this.cbBlock.Name = "cbBlock";
            this.cbBlock.Size = new System.Drawing.Size(81, 26);
            this.cbBlock.TabIndex = 17;
            this.cbBlock.Text = "00";
            // 
            // cbKeyType
            // 
            this.cbKeyType.FormattingEnabled = true;
            this.cbKeyType.Location = new System.Drawing.Point(218, 51);
            this.cbKeyType.Name = "cbKeyType";
            this.cbKeyType.Size = new System.Drawing.Size(81, 26);
            this.cbKeyType.TabIndex = 18;
            this.cbKeyType.Text = "A";
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(321, 48);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(148, 29);
            this.txtKey.TabIndex = 19;
            this.txtKey.Text = "FFFFFFFFFFFF";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 206);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.cbKeyType);
            this.Controls.Add(this.cbBlock);
            this.Controls.Add(this.cbSector);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.btnReadData);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.TextBox txtResult;
        internal System.Windows.Forms.Button btnReadData;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbSector;
        private System.Windows.Forms.ComboBox cbBlock;
        private System.Windows.Forms.ComboBox cbKeyType;
        private System.Windows.Forms.TextBox txtKey;
    }
}

