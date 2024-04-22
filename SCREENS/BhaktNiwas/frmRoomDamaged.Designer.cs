namespace SGMOSOL.SCREENS.BhaktNiwas
{
    partial class frmRoomDamaged
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
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.Label18 = new System.Windows.Forms.Label();
            this.cboSublocation = new System.Windows.Forms.ComboBox();
            this.txtDept = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.rbRepaired = new System.Windows.Forms.RadioButton();
            this.rbDamaged = new System.Windows.Forms.RadioButton();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.lbReason = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.Label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.RoomListBox = new System.Windows.Forms.CheckedListBox();
            this.txtCounter = new System.Windows.Forms.TextBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(15, 108);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(70, 17);
            this.chkSelectAll.TabIndex = 100;
            this.chkSelectAll.Text = "Select All";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label18.Location = new System.Drawing.Point(0, 79);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(59, 18);
            this.Label18.TabIndex = 98;
            this.Label18.Text = "Sub.Loc\r\n";
            this.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboSublocation
            // 
            this.cboSublocation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboSublocation.FormattingEnabled = true;
            this.cboSublocation.Location = new System.Drawing.Point(60, 77);
            this.cboSublocation.Name = "cboSublocation";
            this.cboSublocation.Size = new System.Drawing.Size(85, 21);
            this.cboSublocation.TabIndex = 99;
            this.cboSublocation.SelectedIndexChanged += new System.EventHandler(this.cboSublocation_SelectedIndexChanged);
            // 
            // txtDept
            // 
            this.txtDept.BackColor = System.Drawing.Color.White;
            this.txtDept.Enabled = false;
            this.txtDept.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDept.Location = new System.Drawing.Point(382, 124);
            this.txtDept.Name = "txtDept";
            this.txtDept.ReadOnly = true;
            this.txtDept.Size = new System.Drawing.Size(86, 22);
            this.txtDept.TabIndex = 97;
            this.txtDept.TabStop = false;
            this.txtDept.Visible = false;
            // 
            // Label3
            // 
            this.Label3.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label3.Location = new System.Drawing.Point(307, 125);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(68, 21);
            this.Label3.TabIndex = 96;
            this.Label3.Text = "Dept";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label3.Visible = false;
            // 
            // rbRepaired
            // 
            this.rbRepaired.AutoSize = true;
            this.rbRepaired.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.rbRepaired.Location = new System.Drawing.Point(348, 79);
            this.rbRepaired.Name = "rbRepaired";
            this.rbRepaired.Size = new System.Drawing.Size(168, 22);
            this.rbRepaired.TabIndex = 95;
            this.rbRepaired.Text = "Add Repaired Rooms";
            this.rbRepaired.UseVisualStyleBackColor = true;
            this.rbRepaired.Click += new System.EventHandler(this.rbRepaired_Click);
            // 
            // rbDamaged
            // 
            this.rbDamaged.AutoSize = true;
            this.rbDamaged.Checked = true;
            this.rbDamaged.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.rbDamaged.Location = new System.Drawing.Point(151, 78);
            this.rbDamaged.Name = "rbDamaged";
            this.rbDamaged.Size = new System.Drawing.Size(181, 22);
            this.rbDamaged.TabIndex = 94;
            this.rbDamaged.TabStop = true;
            this.rbDamaged.Text = "Add Damaged Rooms :";
            this.rbDamaged.UseVisualStyleBackColor = true;
            this.rbDamaged.Click += new System.EventHandler(this.rbDamaged_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRefresh.Image = global::SGMOSOL.ResourceMain.Refresh;
            this.btnRefresh.Location = new System.Drawing.Point(549, 73);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(46, 40);
            this.btnRefresh.TabIndex = 93;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // txtReason
            // 
            this.txtReason.Location = new System.Drawing.Point(15, 397);
            this.txtReason.Multiline = true;
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(191, 34);
            this.txtReason.TabIndex = 92;
            // 
            // lbReason
            // 
            this.lbReason.AutoSize = true;
            this.lbReason.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbReason.Location = new System.Drawing.Point(12, 368);
            this.lbReason.Name = "lbReason";
            this.lbReason.Size = new System.Drawing.Size(56, 18);
            this.lbReason.TabIndex = 91;
            this.lbReason.Text = "Reason";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(408, 9);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(99, 20);
            this.dtpDate.TabIndex = 90;
            this.dtpDate.Value = new System.DateTime(2013, 9, 14, 14, 54, 52, 0);
            // 
            // Label2
            // 
            this.Label2.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label2.Location = new System.Drawing.Point(352, 8);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(47, 21);
            this.Label2.TabIndex = 89;
            this.Label2.Text = "Date";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::SGMOSOL.ResourceMain.Close;
            this.btnClose.Location = new System.Drawing.Point(417, 422);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 46);
            this.btnClose.TabIndex = 88;
            this.btnClose.Text = "&Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::SGMOSOL.ResourceMain.Save;
            this.btnSave.Location = new System.Drawing.Point(303, 423);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 46);
            this.btnSave.TabIndex = 87;
            this.btnSave.Text = "&Save\r\n ";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(12, 54);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(0, 18);
            this.Label1.TabIndex = 86;
            // 
            // RoomListBox
            // 
            this.RoomListBox.FormattingEnabled = true;
            this.RoomListBox.Location = new System.Drawing.Point(15, 131);
            this.RoomListBox.Name = "RoomListBox";
            this.RoomListBox.Size = new System.Drawing.Size(191, 229);
            this.RoomListBox.TabIndex = 85;
            // 
            // txtCounter
            // 
            this.txtCounter.BackColor = System.Drawing.Color.White;
            this.txtCounter.Enabled = false;
            this.txtCounter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCounter.Location = new System.Drawing.Point(236, 9);
            this.txtCounter.Name = "txtCounter";
            this.txtCounter.ReadOnly = true;
            this.txtCounter.Size = new System.Drawing.Size(86, 22);
            this.txtCounter.TabIndex = 84;
            this.txtCounter.TabStop = false;
            // 
            // Label13
            // 
            this.Label13.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label13.Location = new System.Drawing.Point(167, 7);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(68, 21);
            this.Label13.TabIndex = 83;
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
            this.Label14.TabIndex = 81;
            this.Label14.Text = "User";
            this.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtUser
            // 
            this.txtUser.BackColor = System.Drawing.Color.White;
            this.txtUser.Enabled = false;
            this.txtUser.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser.Location = new System.Drawing.Point(53, 9);
            this.txtUser.Name = "txtUser";
            this.txtUser.ReadOnly = true;
            this.txtUser.Size = new System.Drawing.Size(85, 22);
            this.txtUser.TabIndex = 82;
            this.txtUser.TabStop = false;
            // 
            // frmRoomDamaged
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 609);
            this.Controls.Add(this.chkSelectAll);
            this.Controls.Add(this.Label18);
            this.Controls.Add(this.cboSublocation);
            this.Controls.Add(this.txtDept);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.rbRepaired);
            this.Controls.Add(this.rbDamaged);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.txtReason);
            this.Controls.Add(this.lbReason);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.RoomListBox);
            this.Controls.Add(this.txtCounter);
            this.Controls.Add(this.Label13);
            this.Controls.Add(this.Label14);
            this.Controls.Add(this.txtUser);
            this.Name = "frmRoomDamaged";
            this.Text = "Add Damaged/Repaired Room";
            this.Load += new System.EventHandler(this.frmRoomDamaged_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.CheckBox chkSelectAll;
        internal System.Windows.Forms.Label Label18;
        internal System.Windows.Forms.ComboBox cboSublocation;
        internal System.Windows.Forms.TextBox txtDept;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.RadioButton rbRepaired;
        internal System.Windows.Forms.RadioButton rbDamaged;
        internal System.Windows.Forms.Button btnRefresh;
        internal System.Windows.Forms.TextBox txtReason;
        internal System.Windows.Forms.Label lbReason;
        internal System.Windows.Forms.DateTimePicker dtpDate;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.CheckedListBox RoomListBox;
        internal System.Windows.Forms.TextBox txtCounter;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.Label Label14;
        internal System.Windows.Forms.TextBox txtUser;
    }
}