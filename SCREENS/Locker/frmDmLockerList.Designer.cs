namespace SGMOSOL.SCREENS.Locker
{
    partial class frmDmLockerList
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
            this.txtLockersCt = new System.Windows.Forms.TextBox();
            this.lbCount = new System.Windows.Forms.Label();
            this.gvDamagedLkrs = new System.Windows.Forms.DataGridView();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtCounter = new System.Windows.Forms.TextBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.Label14 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gvDamagedLkrs)).BeginInit();
            this.SuspendLayout();
            // 
            // txtLockersCt
            // 
            this.txtLockersCt.BackColor = System.Drawing.Color.White;
            this.txtLockersCt.Enabled = false;
            this.txtLockersCt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLockersCt.Location = new System.Drawing.Point(344, 75);
            this.txtLockersCt.Name = "txtLockersCt";
            this.txtLockersCt.ReadOnly = true;
            this.txtLockersCt.Size = new System.Drawing.Size(86, 22);
            this.txtLockersCt.TabIndex = 82;
            this.txtLockersCt.TabStop = false;
            // 
            // lbCount
            // 
            this.lbCount.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.lbCount.Location = new System.Drawing.Point(228, 76);
            this.lbCount.Name = "lbCount";
            this.lbCount.Size = new System.Drawing.Size(106, 21);
            this.lbCount.TabIndex = 81;
            this.lbCount.Text = "No. of Lockers";
            this.lbCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gvDamagedLkrs
            // 
            this.gvDamagedLkrs.AllowUserToAddRows = false;
            this.gvDamagedLkrs.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gvDamagedLkrs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvDamagedLkrs.Location = new System.Drawing.Point(12, 111);
            this.gvDamagedLkrs.Name = "gvDamagedLkrs";
            this.gvDamagedLkrs.ReadOnly = true;
            this.gvDamagedLkrs.Size = new System.Drawing.Size(598, 242);
            this.gvDamagedLkrs.TabIndex = 80;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label4.Location = new System.Drawing.Point(19, 76);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(169, 18);
            this.Label4.TabIndex = 79;
            this.Label4.Text = "List of Damaged Lockers";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.Color.Black;
            this.Label3.Location = new System.Drawing.Point(9, 50);
            this.Label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(429, 2);
            this.Label3.TabIndex = 78;
            this.Label3.Text = "Label3";
            // 
            // txtCounter
            // 
            this.txtCounter.BackColor = System.Drawing.Color.White;
            this.txtCounter.Enabled = false;
            this.txtCounter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCounter.Location = new System.Drawing.Point(266, 10);
            this.txtCounter.Name = "txtCounter";
            this.txtCounter.ReadOnly = true;
            this.txtCounter.Size = new System.Drawing.Size(86, 22);
            this.txtCounter.TabIndex = 77;
            this.txtCounter.TabStop = false;
            // 
            // Label13
            // 
            this.Label13.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label13.Location = new System.Drawing.Point(192, 11);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(68, 21);
            this.Label13.TabIndex = 76;
            this.Label13.Text = "Counter";
            this.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtUser
            // 
            this.txtUser.BackColor = System.Drawing.Color.White;
            this.txtUser.Enabled = false;
            this.txtUser.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser.Location = new System.Drawing.Point(73, 12);
            this.txtUser.Name = "txtUser";
            this.txtUser.ReadOnly = true;
            this.txtUser.Size = new System.Drawing.Size(85, 22);
            this.txtUser.TabIndex = 75;
            this.txtUser.TabStop = false;
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label14.Location = new System.Drawing.Point(19, 12);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(38, 18);
            this.Label14.TabIndex = 74;
            this.Label14.Text = "User";
            this.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmDmLockerList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 377);
            this.Controls.Add(this.txtLockersCt);
            this.Controls.Add(this.lbCount);
            this.Controls.Add(this.gvDamagedLkrs);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.txtCounter);
            this.Controls.Add(this.Label13);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.Label14);
            this.Name = "frmDmLockerList";
            this.Text = "frmDmLockerList";
            this.Load += new System.EventHandler(this.frmDmLockerList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvDamagedLkrs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtLockersCt;
        internal System.Windows.Forms.Label lbCount;
        internal System.Windows.Forms.DataGridView gvDamagedLkrs;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox txtCounter;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.TextBox txtUser;
        internal System.Windows.Forms.Label Label14;
    }
}