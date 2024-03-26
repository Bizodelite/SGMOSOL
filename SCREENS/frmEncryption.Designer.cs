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
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.pnlEncrypted = new System.Windows.Forms.Panel();
            this.lblEncrypted = new System.Windows.Forms.Label();
            this.pnlEncrypted.SuspendLayout();
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
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(675, 72);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(75, 23);
            this.btnEncrypt.TabIndex = 3;
            this.btnEncrypt.Text = "Encrypt";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
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
            // frmEncryption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlEncrypted);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnEncrypt);
            this.Controls.Add(this.txtEncrypt);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Name = "frmEncryption";
            this.Text = "frmEncryption";
            this.Load += new System.EventHandler(this.frmEncryption_Load);
            this.pnlEncrypted.ResumeLayout(false);
            this.pnlEncrypted.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEncrypt;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Panel pnlEncrypted;
        private System.Windows.Forms.Label lblEncrypted;
    }
}