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
            this.txbGetUIDd = new System.Windows.Forms.TextBox();
            this.txbGetUIDh = new System.Windows.Forms.TextBox();
            this.btnGetUID = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(281, 43);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(36, 12);
            this.Label2.TabIndex = 12;
            this.Label2.Text = "(DEC)";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(281, 15);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(36, 12);
            this.Label1.TabIndex = 13;
            this.Label1.Text = "(HEX)";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(103, 82);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(85, 23);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txbGetUIDd
            // 
            this.txbGetUIDd.Location = new System.Drawing.Point(103, 40);
            this.txbGetUIDd.Name = "txbGetUIDd";
            this.txbGetUIDd.Size = new System.Drawing.Size(172, 22);
            this.txbGetUIDd.TabIndex = 10;
            // 
            // txbGetUIDh
            // 
            this.txbGetUIDh.Location = new System.Drawing.Point(103, 12);
            this.txbGetUIDh.Name = "txbGetUIDh";
            this.txbGetUIDh.Size = new System.Drawing.Size(172, 22);
            this.txbGetUIDh.TabIndex = 9;
            // 
            // btnGetUID
            // 
            this.btnGetUID.Location = new System.Drawing.Point(12, 12);
            this.btnGetUID.Name = "btnGetUID";
            this.btnGetUID.Size = new System.Drawing.Size(75, 23);
            this.btnGetUID.TabIndex = 8;
            this.btnGetUID.Text = "Get UID";
            this.btnGetUID.UseVisualStyleBackColor = true;
            this.btnGetUID.Click += new System.EventHandler(this.btnGetUID_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 137);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txbGetUIDd);
            this.Controls.Add(this.txbGetUIDh);
            this.Controls.Add(this.btnGetUID);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.TextBox txbGetUIDd;
        internal System.Windows.Forms.TextBox txbGetUIDh;
        internal System.Windows.Forms.Button btnGetUID;
    }
}

