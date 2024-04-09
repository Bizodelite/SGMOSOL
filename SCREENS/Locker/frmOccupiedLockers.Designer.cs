namespace SGMOSOL.SCREENS.Locker
{
    partial class frmOccupiedLockers
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
            this.gvOccLockers = new System.Windows.Forms.DataGridView();
            this.txtCtOfLockers = new System.Windows.Forms.TextBox();
            this.lbCtOfLkrs = new System.Windows.Forms.Label();
            this.txtCounter = new System.Windows.Forms.TextBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.gvOccLockers)).BeginInit();
            this.SuspendLayout();
            // 
            // gvOccLockers
            // 
            this.gvOccLockers.AllowUserToAddRows = false;
            this.gvOccLockers.AllowUserToResizeRows = false;
            this.gvOccLockers.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gvOccLockers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvOccLockers.Location = new System.Drawing.Point(2, 63);
            this.gvOccLockers.Name = "gvOccLockers";
            this.gvOccLockers.ReadOnly = true;
            this.gvOccLockers.Size = new System.Drawing.Size(824, 369);
            this.gvOccLockers.TabIndex = 49;
            // 
            // txtCtOfLockers
            // 
            this.txtCtOfLockers.BackColor = System.Drawing.Color.White;
            this.txtCtOfLockers.Enabled = false;
            this.txtCtOfLockers.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCtOfLockers.Location = new System.Drawing.Point(586, 6);
            this.txtCtOfLockers.Name = "txtCtOfLockers";
            this.txtCtOfLockers.ReadOnly = true;
            this.txtCtOfLockers.Size = new System.Drawing.Size(78, 22);
            this.txtCtOfLockers.TabIndex = 48;
            this.txtCtOfLockers.TabStop = false;
            // 
            // lbCtOfLkrs
            // 
            this.lbCtOfLkrs.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.lbCtOfLkrs.Location = new System.Drawing.Point(392, 7);
            this.lbCtOfLkrs.Name = "lbCtOfLkrs";
            this.lbCtOfLkrs.Size = new System.Drawing.Size(180, 21);
            this.lbCtOfLkrs.TabIndex = 47;
            this.lbCtOfLkrs.Text = "No. Of Occupied  Lockers";
            this.lbCtOfLkrs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCounter
            // 
            this.txtCounter.BackColor = System.Drawing.Color.White;
            this.txtCounter.Enabled = false;
            this.txtCounter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCounter.Location = new System.Drawing.Point(276, 6);
            this.txtCounter.Name = "txtCounter";
            this.txtCounter.ReadOnly = true;
            this.txtCounter.Size = new System.Drawing.Size(89, 22);
            this.txtCounter.TabIndex = 46;
            this.txtCounter.TabStop = false;
            // 
            // Label13
            // 
            this.Label13.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label13.Location = new System.Drawing.Point(202, 7);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(74, 21);
            this.Label13.TabIndex = 45;
            this.Label13.Text = "Counter";
            this.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label14.Location = new System.Drawing.Point(12, 9);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(38, 18);
            this.Label14.TabIndex = 43;
            this.Label14.Text = "User";
            this.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtUser
            // 
            this.txtUser.BackColor = System.Drawing.Color.White;
            this.txtUser.Enabled = false;
            this.txtUser.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser.Location = new System.Drawing.Point(97, 7);
            this.txtUser.Name = "txtUser";
            this.txtUser.ReadOnly = true;
            this.txtUser.Size = new System.Drawing.Size(88, 22);
            this.txtUser.TabIndex = 44;
            this.txtUser.TabStop = false;
            // 
            // frmOccupiedLockers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 466);
            this.Controls.Add(this.gvOccLockers);
            this.Controls.Add(this.txtCtOfLockers);
            this.Controls.Add(this.lbCtOfLkrs);
            this.Controls.Add(this.txtCounter);
            this.Controls.Add(this.Label13);
            this.Controls.Add(this.Label14);
            this.Controls.Add(this.txtUser);
            this.Name = "frmOccupiedLockers";
            this.Text = "Locker";
            this.Load += new System.EventHandler(this.frmOccupiedLockers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvOccLockers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.DataGridView gvOccLockers;
        internal System.Windows.Forms.TextBox txtCtOfLockers;
        internal System.Windows.Forms.Label lbCtOfLkrs;
        internal System.Windows.Forms.TextBox txtCounter;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.Label Label14;
        internal System.Windows.Forms.TextBox txtUser;
    }
}