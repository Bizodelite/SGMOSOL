namespace SGMOSOL.SCREENS
{
    partial class frmLockerCheckIn
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
            this.txtDays = new System.Windows.Forms.ComboBox();
            this.nudAdvance = new System.Windows.Forms.NumericUpDown();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtOldReceipt = new System.Windows.Forms.TextBox();
            this.lblFreeReceipt = new System.Windows.Forms.Label();
            this.chkAllowFreeReceipt = new System.Windows.Forms.CheckBox();
            this.txtRoomSrch = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtLkrAvlbleCt = new System.Windows.Forms.TextBox();
            this.lbAvailableLkrCt = new System.Windows.Forms.Label();
            this.Label15 = new System.Windows.Forms.Label();
            this.txtmobno = new System.Windows.Forms.TextBox();
            this.txtCounter = new System.Windows.Forms.TextBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.Label12 = new System.Windows.Forms.Label();
            this.dtpCheckOutTime = new System.Windows.Forms.DateTimePicker();
            this.Label10 = new System.Windows.Forms.Label();
            this.dtpCheckInTime = new System.Windows.Forms.DateTimePicker();
            this.txtNoOfLockers = new System.Windows.Forms.TextBox();
            this.nudRent = new System.Windows.Forms.NumericUpDown();
            this.dtpCheckOut = new System.Windows.Forms.DateTimePicker();
            this.Label6 = new System.Windows.Forms.Label();
            this.chkLockers = new System.Windows.Forms.CheckedListBox();
            this.lblRent = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtPlace = new System.Windows.Forms.TextBox();
            this.lblRemarks = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtVchNo = new System.Windows.Forms.TextBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.dtpCheckIn = new System.Windows.Forms.DateTimePicker();
            this.Label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdvance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRent)).BeginInit();
            this.SuspendLayout();
            // 
            // txtDays
            // 
            this.txtDays.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtDays.FormattingEnabled = true;
            this.txtDays.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.txtDays.Location = new System.Drawing.Point(153, 302);
            this.txtDays.Margin = new System.Windows.Forms.Padding(4);
            this.txtDays.Name = "txtDays";
            this.txtDays.Size = new System.Drawing.Size(132, 24);
            this.txtDays.TabIndex = 174;
            // 
            // nudAdvance
            // 
            this.nudAdvance.DecimalPlaces = 2;
            this.nudAdvance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudAdvance.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudAdvance.Location = new System.Drawing.Point(153, 396);
            this.nudAdvance.Margin = new System.Windows.Forms.Padding(4);
            this.nudAdvance.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            131072});
            this.nudAdvance.Name = "nudAdvance";
            this.nudAdvance.ReadOnly = true;
            this.nudAdvance.Size = new System.Drawing.Size(133, 26);
            this.nudAdvance.TabIndex = 178;
            this.nudAdvance.TabStop = false;
            this.nudAdvance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::SGMOSOL.ResourceMain.Close;
            this.btnClose.Location = new System.Drawing.Point(882, 587);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(133, 57);
            this.btnClose.TabIndex = 202;
            this.btnClose.Text = "&Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Enabled = false;
            this.btnPrint.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Image = global::SGMOSOL.ResourceMain.Print;
            this.btnPrint.Location = new System.Drawing.Point(723, 587);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(116, 57);
            this.btnPrint.TabIndex = 201;
            this.btnPrint.Text = "&Print";
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::SGMOSOL.ResourceMain.Save;
            this.btnSave.Location = new System.Drawing.Point(546, 587);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(133, 57);
            this.btnSave.TabIndex = 200;
            this.btnSave.Text = "&Save\r\n / Print";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnNew
            // 
            this.btnNew.CausesValidation = false;
            this.btnNew.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Image = global::SGMOSOL.ResourceMain._new;
            this.btnNew.Location = new System.Drawing.Point(377, 587);
            this.btnNew.Margin = new System.Windows.Forms.Padding(4);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(129, 57);
            this.btnNew.TabIndex = 199;
            this.btnNew.Text = "&New";
            this.btnNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Image = global::SGMOSOL.ResourceMain.Search;
            this.btnSearch.Location = new System.Drawing.Point(31, 587);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(139, 57);
            this.btnSearch.TabIndex = 198;
            this.btnSearch.Text = "&Find /\r\nSearch";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtOldReceipt
            // 
            this.txtOldReceipt.BackColor = System.Drawing.Color.White;
            this.txtOldReceipt.Location = new System.Drawing.Point(493, 54);
            this.txtOldReceipt.Margin = new System.Windows.Forms.Padding(4);
            this.txtOldReceipt.MaxLength = 15;
            this.txtOldReceipt.Name = "txtOldReceipt";
            this.txtOldReceipt.Size = new System.Drawing.Size(117, 22);
            this.txtOldReceipt.TabIndex = 197;
            this.txtOldReceipt.Visible = false;
            // 
            // lblFreeReceipt
            // 
            this.lblFreeReceipt.AutoSize = true;
            this.lblFreeReceipt.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.lblFreeReceipt.Location = new System.Drawing.Point(334, 54);
            this.lblFreeReceipt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFreeReceipt.Name = "lblFreeReceipt";
            this.lblFreeReceipt.Size = new System.Drawing.Size(137, 23);
            this.lblFreeReceipt.TabIndex = 196;
            this.lblFreeReceipt.Text = "Re-Use Receipt";
            this.lblFreeReceipt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFreeReceipt.Visible = false;
            // 
            // chkAllowFreeReceipt
            // 
            this.chkAllowFreeReceipt.AutoSize = true;
            this.chkAllowFreeReceipt.Location = new System.Drawing.Point(302, 59);
            this.chkAllowFreeReceipt.Margin = new System.Windows.Forms.Padding(4);
            this.chkAllowFreeReceipt.Name = "chkAllowFreeReceipt";
            this.chkAllowFreeReceipt.Size = new System.Drawing.Size(18, 17);
            this.chkAllowFreeReceipt.TabIndex = 195;
            this.chkAllowFreeReceipt.UseVisualStyleBackColor = true;
            this.chkAllowFreeReceipt.Visible = false;
            // 
            // txtRoomSrch
            // 
            this.txtRoomSrch.BackColor = System.Drawing.Color.White;
            this.txtRoomSrch.Location = new System.Drawing.Point(723, 56);
            this.txtRoomSrch.Margin = new System.Windows.Forms.Padding(4);
            this.txtRoomSrch.MaxLength = 3;
            this.txtRoomSrch.Name = "txtRoomSrch";
            this.txtRoomSrch.Size = new System.Drawing.Size(291, 22);
            this.txtRoomSrch.TabIndex = 182;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label2.Location = new System.Drawing.Point(639, 56);
            this.Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(64, 23);
            this.Label2.TabIndex = 194;
            this.Label2.Text = "Locker";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtLkrAvlbleCt
            // 
            this.txtLkrAvlbleCt.BackColor = System.Drawing.Color.White;
            this.txtLkrAvlbleCt.Enabled = false;
            this.txtLkrAvlbleCt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLkrAvlbleCt.Location = new System.Drawing.Point(902, 21);
            this.txtLkrAvlbleCt.Margin = new System.Windows.Forms.Padding(4);
            this.txtLkrAvlbleCt.Name = "txtLkrAvlbleCt";
            this.txtLkrAvlbleCt.ReadOnly = true;
            this.txtLkrAvlbleCt.Size = new System.Drawing.Size(117, 26);
            this.txtLkrAvlbleCt.TabIndex = 193;
            this.txtLkrAvlbleCt.TabStop = false;
            // 
            // lbAvailableLkrCt
            // 
            this.lbAvailableLkrCt.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.lbAvailableLkrCt.Location = new System.Drawing.Point(644, 22);
            this.lbAvailableLkrCt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbAvailableLkrCt.Name = "lbAvailableLkrCt";
            this.lbAvailableLkrCt.Size = new System.Drawing.Size(249, 26);
            this.lbAvailableLkrCt.TabIndex = 192;
            this.lbAvailableLkrCt.Text = "Available No.of Lockers";
            this.lbAvailableLkrCt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label15.Location = new System.Drawing.Point(18, 205);
            this.Label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(75, 23);
            this.Label15.TabIndex = 169;
            this.Label15.Text = "Mob No";
            this.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtmobno
            // 
            this.txtmobno.BackColor = System.Drawing.Color.White;
            this.txtmobno.Location = new System.Drawing.Point(153, 200);
            this.txtmobno.Margin = new System.Windows.Forms.Padding(4);
            this.txtmobno.MaxLength = 100;
            this.txtmobno.Name = "txtmobno";
            this.txtmobno.Size = new System.Drawing.Size(208, 22);
            this.txtmobno.TabIndex = 170;
            // 
            // txtCounter
            // 
            this.txtCounter.BackColor = System.Drawing.Color.White;
            this.txtCounter.Enabled = false;
            this.txtCounter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCounter.Location = new System.Drawing.Point(498, 20);
            this.txtCounter.Margin = new System.Windows.Forms.Padding(4);
            this.txtCounter.Name = "txtCounter";
            this.txtCounter.ReadOnly = true;
            this.txtCounter.Size = new System.Drawing.Size(117, 26);
            this.txtCounter.TabIndex = 191;
            this.txtCounter.TabStop = false;
            // 
            // Label13
            // 
            this.Label13.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label13.Location = new System.Drawing.Point(391, 21);
            this.Label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(99, 26);
            this.Label13.TabIndex = 190;
            this.Label13.Text = "Counter";
            this.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtUser
            // 
            this.txtUser.BackColor = System.Drawing.Color.White;
            this.txtUser.Enabled = false;
            this.txtUser.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser.Location = new System.Drawing.Point(159, 21);
            this.txtUser.Margin = new System.Windows.Forms.Padding(4);
            this.txtUser.Name = "txtUser";
            this.txtUser.ReadOnly = true;
            this.txtUser.Size = new System.Drawing.Size(111, 26);
            this.txtUser.TabIndex = 189;
            this.txtUser.TabStop = false;
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label12.ForeColor = System.Drawing.Color.Black;
            this.Label12.Location = new System.Drawing.Point(15, 550);
            this.Label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(51, 23);
            this.Label12.TabIndex = 185;
            this.Label12.Text = "Time";
            this.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpCheckOutTime
            // 
            this.dtpCheckOutTime.Checked = false;
            this.dtpCheckOutTime.CustomFormat = "hh:mm tt";
            this.dtpCheckOutTime.Enabled = false;
            this.dtpCheckOutTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckOutTime.Location = new System.Drawing.Point(153, 545);
            this.dtpCheckOutTime.Margin = new System.Windows.Forms.Padding(4);
            this.dtpCheckOutTime.Name = "dtpCheckOutTime";
            this.dtpCheckOutTime.Size = new System.Drawing.Size(132, 22);
            this.dtpCheckOutTime.TabIndex = 187;
            this.dtpCheckOutTime.Value = new System.DateTime(2005, 9, 20, 0, 0, 0, 0);
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label10.Location = new System.Drawing.Point(389, 110);
            this.Label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(51, 23);
            this.Label10.TabIndex = 188;
            this.Label10.Text = "Time";
            this.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpCheckInTime
            // 
            this.dtpCheckInTime.Checked = false;
            this.dtpCheckInTime.CustomFormat = "hh:mm tt";
            this.dtpCheckInTime.Enabled = false;
            this.dtpCheckInTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckInTime.Location = new System.Drawing.Point(493, 105);
            this.dtpCheckInTime.Margin = new System.Windows.Forms.Padding(4);
            this.dtpCheckInTime.Name = "dtpCheckInTime";
            this.dtpCheckInTime.Size = new System.Drawing.Size(119, 22);
            this.dtpCheckInTime.TabIndex = 166;
            this.dtpCheckInTime.Value = new System.DateTime(2013, 8, 24, 0, 0, 0, 0);
            // 
            // txtNoOfLockers
            // 
            this.txtNoOfLockers.BackColor = System.Drawing.Color.MistyRose;
            this.txtNoOfLockers.Location = new System.Drawing.Point(153, 345);
            this.txtNoOfLockers.Margin = new System.Windows.Forms.Padding(4);
            this.txtNoOfLockers.MaxLength = 3;
            this.txtNoOfLockers.Name = "txtNoOfLockers";
            this.txtNoOfLockers.Size = new System.Drawing.Size(132, 22);
            this.txtNoOfLockers.TabIndex = 176;
            this.txtNoOfLockers.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // nudRent
            // 
            this.nudRent.DecimalPlaces = 2;
            this.nudRent.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudRent.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudRent.Location = new System.Drawing.Point(153, 445);
            this.nudRent.Margin = new System.Windows.Forms.Padding(4);
            this.nudRent.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            131072});
            this.nudRent.Name = "nudRent";
            this.nudRent.ReadOnly = true;
            this.nudRent.Size = new System.Drawing.Size(133, 26);
            this.nudRent.TabIndex = 180;
            this.nudRent.TabStop = false;
            this.nudRent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dtpCheckOut
            // 
            this.dtpCheckOut.Checked = false;
            this.dtpCheckOut.CustomFormat = "dd/MM/yyyy";
            this.dtpCheckOut.Enabled = false;
            this.dtpCheckOut.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckOut.Location = new System.Drawing.Point(153, 496);
            this.dtpCheckOut.Margin = new System.Windows.Forms.Padding(4);
            this.dtpCheckOut.Name = "dtpCheckOut";
            this.dtpCheckOut.Size = new System.Drawing.Size(132, 22);
            this.dtpCheckOut.TabIndex = 183;
            this.dtpCheckOut.Value = new System.DateTime(2005, 9, 20, 0, 0, 0, 0);
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label6.Location = new System.Drawing.Point(639, 105);
            this.Label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(72, 23);
            this.Label6.TabIndex = 184;
            this.Label6.Text = "Lockers";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkLockers
            // 
            this.chkLockers.CheckOnClick = true;
            this.chkLockers.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.chkLockers.FormattingEnabled = true;
            this.chkLockers.Location = new System.Drawing.Point(723, 114);
            this.chkLockers.Margin = new System.Windows.Forms.Padding(4);
            this.chkLockers.MultiColumn = true;
            this.chkLockers.Name = "chkLockers";
            this.chkLockers.Size = new System.Drawing.Size(291, 404);
            this.chkLockers.TabIndex = 186;
            this.chkLockers.SelectedIndexChanged += new System.EventHandler(this.chkLockers_SelectedIndexChanged);
            // 
            // lblRent
            // 
            this.lblRent.AutoSize = true;
            this.lblRent.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.lblRent.Location = new System.Drawing.Point(15, 450);
            this.lblRent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRent.Name = "lblRent";
            this.lblRent.Size = new System.Drawing.Size(59, 23);
            this.lblRent.TabIndex = 179;
            this.lblRent.Text = "Dengi";
            this.lblRent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label7.Location = new System.Drawing.Point(15, 501);
            this.Label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(85, 23);
            this.Label7.TabIndex = 181;
            this.Label7.Text = "Out Date";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label5.Location = new System.Drawing.Point(15, 401);
            this.Label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(81, 23);
            this.Label5.TabIndex = 177;
            this.Label5.Text = "Advance";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label4.Location = new System.Drawing.Point(15, 350);
            this.Label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(123, 23);
            this.Label4.TabIndex = 175;
            this.Label4.Text = "No of Lockers";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label3.Location = new System.Drawing.Point(17, 302);
            this.Label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(50, 23);
            this.Label3.TabIndex = 173;
            this.Label3.Text = "Days";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label1.Location = new System.Drawing.Point(17, 256);
            this.Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(53, 23);
            this.Label1.TabIndex = 171;
            this.Label1.Text = "Place";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPlace
            // 
            this.txtPlace.BackColor = System.Drawing.Color.White;
            this.txtPlace.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPlace.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlace.Location = new System.Drawing.Point(154, 249);
            this.txtPlace.Margin = new System.Windows.Forms.Padding(4);
            this.txtPlace.MaxLength = 100;
            this.txtPlace.Name = "txtPlace";
            this.txtPlace.Size = new System.Drawing.Size(208, 30);
            this.txtPlace.TabIndex = 172;
            // 
            // lblRemarks
            // 
            this.lblRemarks.AutoSize = true;
            this.lblRemarks.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.lblRemarks.Location = new System.Drawing.Point(17, 158);
            this.lblRemarks.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(59, 23);
            this.lblRemarks.TabIndex = 167;
            this.lblRemarks.Text = "Name";
            this.lblRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.White;
            this.txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(153, 153);
            this.txtName.Margin = new System.Windows.Forms.Padding(4);
            this.txtName.MaxLength = 120;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(459, 30);
            this.txtName.TabIndex = 168;
            // 
            // txtVchNo
            // 
            this.txtVchNo.BackColor = System.Drawing.Color.White;
            this.txtVchNo.Enabled = false;
            this.txtVchNo.Location = new System.Drawing.Point(154, 56);
            this.txtVchNo.Margin = new System.Windows.Forms.Padding(4);
            this.txtVchNo.Name = "txtVchNo";
            this.txtVchNo.ReadOnly = true;
            this.txtVchNo.Size = new System.Drawing.Size(111, 22);
            this.txtVchNo.TabIndex = 163;
            this.txtVchNo.TabStop = false;
            this.txtVchNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label11.Location = new System.Drawing.Point(15, 61);
            this.Label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(101, 23);
            this.Label11.TabIndex = 162;
            this.Label11.Text = "Receipt No";
            this.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpCheckIn
            // 
            this.dtpCheckIn.Checked = false;
            this.dtpCheckIn.CustomFormat = "dd/MM/yyyy";
            this.dtpCheckIn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckIn.Location = new System.Drawing.Point(154, 105);
            this.dtpCheckIn.Margin = new System.Windows.Forms.Padding(4);
            this.dtpCheckIn.Name = "dtpCheckIn";
            this.dtpCheckIn.Size = new System.Drawing.Size(131, 22);
            this.dtpCheckIn.TabIndex = 165;
            this.dtpCheckIn.Value = new System.DateTime(2013, 8, 24, 0, 0, 0, 0);
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label9.ForeColor = System.Drawing.Color.Black;
            this.Label9.Location = new System.Drawing.Point(17, 110);
            this.Label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(49, 23);
            this.Label9.TabIndex = 164;
            this.Label9.Text = "Date";
            this.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.label8.Location = new System.Drawing.Point(15, 19);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 23);
            this.label8.TabIndex = 203;
            this.label8.Text = "USER";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmLockerCheckIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 665);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtDays);
            this.Controls.Add(this.nudAdvance);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtOldReceipt);
            this.Controls.Add(this.lblFreeReceipt);
            this.Controls.Add(this.chkAllowFreeReceipt);
            this.Controls.Add(this.txtRoomSrch);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.txtLkrAvlbleCt);
            this.Controls.Add(this.lbAvailableLkrCt);
            this.Controls.Add(this.Label15);
            this.Controls.Add(this.txtmobno);
            this.Controls.Add(this.txtCounter);
            this.Controls.Add(this.Label13);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.Label12);
            this.Controls.Add(this.dtpCheckOutTime);
            this.Controls.Add(this.Label10);
            this.Controls.Add(this.dtpCheckInTime);
            this.Controls.Add(this.txtNoOfLockers);
            this.Controls.Add(this.nudRent);
            this.Controls.Add(this.dtpCheckOut);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.chkLockers);
            this.Controls.Add(this.lblRent);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.txtPlace);
            this.Controls.Add(this.lblRemarks);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtVchNo);
            this.Controls.Add(this.Label11);
            this.Controls.Add(this.dtpCheckIn);
            this.Controls.Add(this.Label9);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmLockerCheckIn";
            this.Text = "Locker In-Time";
            this.Load += new System.EventHandler(this.frmLockerCheckIn_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmLockerCheckIn_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmLockerCheckIn_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.nudAdvance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox txtDays;
        internal System.Windows.Forms.NumericUpDown nudAdvance;
        internal System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.Button btnPrint;
        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.Button btnNew;
        internal System.Windows.Forms.Button btnSearch;
        internal System.Windows.Forms.TextBox txtOldReceipt;
        internal System.Windows.Forms.Label lblFreeReceipt;
        internal System.Windows.Forms.CheckBox chkAllowFreeReceipt;
        internal System.Windows.Forms.TextBox txtRoomSrch;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox txtLkrAvlbleCt;
        internal System.Windows.Forms.Label lbAvailableLkrCt;
        internal System.Windows.Forms.Label Label15;
        internal System.Windows.Forms.TextBox txtmobno;
        internal System.Windows.Forms.TextBox txtCounter;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.TextBox txtUser;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.DateTimePicker dtpCheckOutTime;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.DateTimePicker dtpCheckInTime;
        internal System.Windows.Forms.TextBox txtNoOfLockers;
        internal System.Windows.Forms.NumericUpDown nudRent;
        internal System.Windows.Forms.DateTimePicker dtpCheckOut;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.CheckedListBox chkLockers;
        internal System.Windows.Forms.Label lblRent;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox txtPlace;
        internal System.Windows.Forms.Label lblRemarks;
        internal System.Windows.Forms.TextBox txtName;
        internal System.Windows.Forms.TextBox txtVchNo;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.DateTimePicker dtpCheckIn;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Label label8;
    }
}