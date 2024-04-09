namespace SGMOSOL.SCREENS.Locker
{
    partial class frmLockerList
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
            this.txtCtOfLockers = new System.Windows.Forms.TextBox();
            this.lbCtOfLkrs = new System.Windows.Forms.Label();
            this.txtCounter = new System.Windows.Forms.TextBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.fpsLockers = new System.Windows.Forms.DataGridView();
            this.ColA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.fpsLockers)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCtOfLockers
            // 
            this.txtCtOfLockers.BackColor = System.Drawing.Color.White;
            this.txtCtOfLockers.Enabled = false;
            this.txtCtOfLockers.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCtOfLockers.Location = new System.Drawing.Point(580, 11);
            this.txtCtOfLockers.Name = "txtCtOfLockers";
            this.txtCtOfLockers.ReadOnly = true;
            this.txtCtOfLockers.Size = new System.Drawing.Size(73, 22);
            this.txtCtOfLockers.TabIndex = 47;
            this.txtCtOfLockers.TabStop = false;
            // 
            // lbCtOfLkrs
            // 
            this.lbCtOfLkrs.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.lbCtOfLkrs.Location = new System.Drawing.Point(377, 12);
            this.lbCtOfLkrs.Name = "lbCtOfLkrs";
            this.lbCtOfLkrs.Size = new System.Drawing.Size(166, 21);
            this.lbCtOfLkrs.TabIndex = 46;
            this.lbCtOfLkrs.Text = "No. Of Available Lockers";
            this.lbCtOfLkrs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCounter
            // 
            this.txtCounter.BackColor = System.Drawing.Color.White;
            this.txtCounter.Enabled = false;
            this.txtCounter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCounter.Location = new System.Drawing.Point(282, 11);
            this.txtCounter.Name = "txtCounter";
            this.txtCounter.ReadOnly = true;
            this.txtCounter.Size = new System.Drawing.Size(89, 22);
            this.txtCounter.TabIndex = 45;
            this.txtCounter.TabStop = false;
            // 
            // Label13
            // 
            this.Label13.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label13.Location = new System.Drawing.Point(208, 12);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(74, 21);
            this.Label13.TabIndex = 44;
            this.Label13.Text = "Counter";
            this.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label14.Location = new System.Drawing.Point(18, 14);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(38, 18);
            this.Label14.TabIndex = 42;
            this.Label14.Text = "User";
            this.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtUser
            // 
            this.txtUser.BackColor = System.Drawing.Color.White;
            this.txtUser.Enabled = false;
            this.txtUser.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser.Location = new System.Drawing.Point(103, 12);
            this.txtUser.Name = "txtUser";
            this.txtUser.ReadOnly = true;
            this.txtUser.Size = new System.Drawing.Size(88, 22);
            this.txtUser.TabIndex = 43;
            this.txtUser.TabStop = false;
            // 
            // fpsLockers
            // 
            this.fpsLockers.AllowUserToAddRows = false;
            this.fpsLockers.AllowUserToDeleteRows = false;
            this.fpsLockers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.fpsLockers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColA,
            this.ColB,
            this.ColC,
            this.ColD});
            this.fpsLockers.Location = new System.Drawing.Point(12, 40);
            this.fpsLockers.Name = "fpsLockers";
            this.fpsLockers.ReadOnly = true;
            this.fpsLockers.Size = new System.Drawing.Size(647, 404);
            this.fpsLockers.TabIndex = 48;
            // 
            // ColA
            // 
            this.ColA.HeaderText = "A";
            this.ColA.Name = "ColA";
            this.ColA.ReadOnly = true;
            this.ColA.Width = 150;
            // 
            // ColB
            // 
            this.ColB.HeaderText = "B";
            this.ColB.Name = "ColB";
            this.ColB.ReadOnly = true;
            this.ColB.Width = 150;
            // 
            // ColC
            // 
            this.ColC.HeaderText = "C";
            this.ColC.Name = "ColC";
            this.ColC.ReadOnly = true;
            this.ColC.Width = 150;
            // 
            // ColD
            // 
            this.ColD.HeaderText = "D";
            this.ColD.Name = "ColD";
            this.ColD.ReadOnly = true;
            this.ColD.Width = 150;
            // 
            // frmLockerList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 450);
            this.Controls.Add(this.fpsLockers);
            this.Controls.Add(this.txtCtOfLockers);
            this.Controls.Add(this.lbCtOfLkrs);
            this.Controls.Add(this.txtCounter);
            this.Controls.Add(this.Label13);
            this.Controls.Add(this.Label14);
            this.Controls.Add(this.txtUser);
            this.Name = "frmLockerList";
            this.Text = "Locker";
            this.Load += new System.EventHandler(this.frmLockerList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fpsLockers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtCtOfLockers;
        internal System.Windows.Forms.Label lbCtOfLkrs;
        internal System.Windows.Forms.TextBox txtCounter;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.Label Label14;
        internal System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.DataGridView fpsLockers;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColA;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColB;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColC;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColD;
    }
}