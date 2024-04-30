namespace SGMOSOL.SCREENS
{
    partial class frmEncryption
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtEncrypt = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.pnlEncrypted = new System.Windows.Forms.Panel();
            this.lblEncrypted = new System.Windows.Forms.Label();
            this.txtDecrypt = new System.Windows.Forms.TextBox();
            this.btnDec = new System.Windows.Forms.Button();
            this.pnlDecrypted = new System.Windows.Forms.Panel();
            this.lblDecrypted = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btncpydec = new System.Windows.Forms.Button();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.pnlEncrypted.SuspendLayout();
            this.pnlDecrypted.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Input Text to Encrypt";
            // 
            // txtEncrypt
            // 
            this.txtEncrypt.Location = new System.Drawing.Point(196, 73);
            this.txtEncrypt.Name = "txtEncrypt";
            this.txtEncrypt.Size = new System.Drawing.Size(473, 22);
            this.txtEncrypt.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(373, 230);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(675, 150);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 6;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Encrypted Text";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(454, 230);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // pnlEncrypted
            // 
            this.pnlEncrypted.BackColor = System.Drawing.Color.Aquamarine;
            this.pnlEncrypted.Controls.Add(this.lblEncrypted);
            this.pnlEncrypted.Location = new System.Drawing.Point(196, 122);
            this.pnlEncrypted.Name = "pnlEncrypted";
            this.pnlEncrypted.Size = new System.Drawing.Size(473, 86);
            this.pnlEncrypted.TabIndex = 9;
            // 
            // lblEncrypted
            // 
            this.lblEncrypted.AutoSize = true;
            this.lblEncrypted.Location = new System.Drawing.Point(3, 9);
            this.lblEncrypted.Name = "lblEncrypted";
            this.lblEncrypted.Size = new System.Drawing.Size(0, 16);
            this.lblEncrypted.TabIndex = 0;
            // 
            // txtDecrypt
            // 
            this.txtDecrypt.Location = new System.Drawing.Point(196, 304);
            this.txtDecrypt.Name = "txtDecrypt";
            this.txtDecrypt.Size = new System.Drawing.Size(473, 22);
            this.txtDecrypt.TabIndex = 10;
            // 
            // btnDec
            // 
            this.btnDec.Location = new System.Drawing.Point(675, 304);
            this.btnDec.Name = "btnDec";
            this.btnDec.Size = new System.Drawing.Size(75, 23);
            this.btnDec.TabIndex = 11;
            this.btnDec.Text = "Decrypt";
            this.btnDec.UseVisualStyleBackColor = true;
            this.btnDec.Click += new System.EventHandler(this.btnDec_Click);
            // 
            // pnlDecrypted
            // 
            this.pnlDecrypted.BackColor = System.Drawing.Color.Aquamarine;
            this.pnlDecrypted.Controls.Add(this.lblDecrypted);
            this.pnlDecrypted.Controls.Add(this.label3);
            this.pnlDecrypted.Location = new System.Drawing.Point(196, 352);
            this.pnlDecrypted.Name = "pnlDecrypted";
            this.pnlDecrypted.Size = new System.Drawing.Size(473, 86);
            this.pnlDecrypted.TabIndex = 12;
            // 
            // lblDecrypted
            // 
            this.lblDecrypted.AutoSize = true;
            this.lblDecrypted.Location = new System.Drawing.Point(55, 24);
            this.lblDecrypted.Name = "lblDecrypted";
            this.lblDecrypted.Size = new System.Drawing.Size(44, 16);
            this.lblDecrypted.TabIndex = 1;
            this.lblDecrypted.Text = "label4";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 16);
            this.label3.TabIndex = 0;
            // 
            // btncpydec
            // 
            this.btncpydec.Location = new System.Drawing.Point(687, 385);
            this.btncpydec.Name = "btncpydec";
            this.btncpydec.Size = new System.Drawing.Size(75, 23);
            this.btncpydec.TabIndex = 13;
            this.btncpydec.Text = "Copy";
            this.btncpydec.UseVisualStyleBackColor = true;
            this.btncpydec.Click += new System.EventHandler(this.btncpydec_Click);
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(675, 76);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(75, 23);
            this.btnEncrypt.TabIndex = 14;
            this.btnEncrypt.Text = "Encrypt";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // frmEncryption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(1112, 622);
            this.Controls.Add(this.btnEncrypt);
            this.Controls.Add(this.btncpydec);
            this.Controls.Add(this.pnlDecrypted);
            this.Controls.Add(this.btnDec);
            this.Controls.Add(this.txtDecrypt);
            this.Controls.Add(this.pnlEncrypted);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtEncrypt);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Name = "frmEncryption";
            this.Text = "frmEncryption";
            this.Load += new System.EventHandler(this.frmEncryption_Load);
            this.pnlEncrypted.ResumeLayout(false);
            this.pnlEncrypted.PerformLayout();
            this.pnlDecrypted.ResumeLayout(false);
            this.pnlDecrypted.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEncrypt;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Panel pnlEncrypted;
        private System.Windows.Forms.Label lblEncrypted;
        private System.Windows.Forms.TextBox txtDecrypt;
        private System.Windows.Forms.Button btnDec;
        private System.Windows.Forms.Panel pnlDecrypted;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblDecrypted;
        private System.Windows.Forms.Button btncpydec;
        private System.Windows.Forms.Button btnEncrypt;
    }
}