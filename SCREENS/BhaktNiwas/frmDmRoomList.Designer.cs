namespace SGMOSOL.SCREENS.BhaktNiwas
{
    partial class frmDmRoomList
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
            this.txtRoomsCt = new System.Windows.Forms.TextBox();
            this.lbCount = new System.Windows.Forms.Label();
            this.gvDamagedRooms = new System.Windows.Forms.DataGridView();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtCounter = new System.Windows.Forms.TextBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.Label14 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gvDamagedRooms)).BeginInit();
            this.SuspendLayout();
            // 
            // txtRoomsCt
            // 
            this.txtRoomsCt.BackColor = System.Drawing.Color.White;
            this.txtRoomsCt.Enabled = false;
            this.txtRoomsCt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRoomsCt.Location = new System.Drawing.Point(403, 71);
            this.txtRoomsCt.Name = "txtRoomsCt";
            this.txtRoomsCt.ReadOnly = true;
            this.txtRoomsCt.Size = new System.Drawing.Size(86, 22);
            this.txtRoomsCt.TabIndex = 82;
            this.txtRoomsCt.TabStop = false;
            // 
            // lbCount
            // 
            this.lbCount.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.lbCount.Location = new System.Drawing.Point(252, 73);
            this.lbCount.Name = "lbCount";
            this.lbCount.Size = new System.Drawing.Size(106, 21);
            this.lbCount.TabIndex = 81;
            this.lbCount.Text = "No. of Rooms";
            this.lbCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gvDamagedRooms
            // 
            this.gvDamagedRooms.AllowUserToAddRows = false;
            this.gvDamagedRooms.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gvDamagedRooms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvDamagedRooms.Location = new System.Drawing.Point(5, 108);
            this.gvDamagedRooms.Name = "gvDamagedRooms";
            this.gvDamagedRooms.ReadOnly = true;
            this.gvDamagedRooms.Size = new System.Drawing.Size(546, 322);
            this.gvDamagedRooms.TabIndex = 80;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label4.Location = new System.Drawing.Point(12, 73);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(165, 18);
            this.Label4.TabIndex = 79;
            this.Label4.Text = "List of Damaged Rooms";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.Color.Black;
            this.Label3.Location = new System.Drawing.Point(2, 47);
            this.Label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(530, 2);
            this.Label3.TabIndex = 78;
            this.Label3.Text = "Label3";
            // 
            // txtCounter
            // 
            this.txtCounter.BackColor = System.Drawing.Color.White;
            this.txtCounter.Enabled = false;
            this.txtCounter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCounter.Location = new System.Drawing.Point(403, 8);
            this.txtCounter.Name = "txtCounter";
            this.txtCounter.ReadOnly = true;
            this.txtCounter.Size = new System.Drawing.Size(86, 22);
            this.txtCounter.TabIndex = 77;
            this.txtCounter.TabStop = false;
            // 
            // Label13
            // 
            this.Label13.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label13.Location = new System.Drawing.Point(290, 8);
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
            this.txtUser.Location = new System.Drawing.Point(66, 9);
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
            this.Label14.Location = new System.Drawing.Point(12, 9);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(38, 18);
            this.Label14.TabIndex = 74;
            this.Label14.Text = "User";
            this.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmDmRoomList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtRoomsCt);
            this.Controls.Add(this.lbCount);
            this.Controls.Add(this.gvDamagedRooms);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.txtCounter);
            this.Controls.Add(this.Label13);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.Label14);
            this.Name = "frmDmRoomList";
            this.Text = "frmDmRoomList";
            this.Load += new System.EventHandler(this.frmDmRoomList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvDamagedRooms)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtRoomsCt;
        internal System.Windows.Forms.Label lbCount;
        internal System.Windows.Forms.DataGridView gvDamagedRooms;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox txtCounter;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.TextBox txtUser;
        internal System.Windows.Forms.Label Label14;
    }
}