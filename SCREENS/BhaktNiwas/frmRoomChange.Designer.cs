namespace SGMOSOL.SCREENS.BhaktNiwas
{
    partial class frmRoomChange
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
            this.cmbDays = new System.Windows.Forms.ComboBox();
            this.lblRoomchange = new System.Windows.Forms.Label();
            this.cboSublocation = new System.Windows.Forms.ComboBox();
            this.nudRentPending = new System.Windows.Forms.NumericUpDown();
            this.Label17 = new System.Windows.Forms.Label();
            this.nudRentPaid = new System.Windows.Forms.NumericUpDown();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.lbReason = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.nudAdvance = new System.Windows.Forms.NumericUpDown();
            this.Label16 = new System.Windows.Forms.Label();
            this.chkRooms_given = new System.Windows.Forms.CheckedListBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.Label15 = new System.Windows.Forms.Label();
            this.txtmobno = new System.Windows.Forms.TextBox();
            this.txtCounter = new System.Windows.Forms.TextBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.Label12 = new System.Windows.Forms.Label();
            this.dtpCheckOutTime = new System.Windows.Forms.DateTimePicker();
            this.Label10 = new System.Windows.Forms.Label();
            this.dtpCheckInTime = new System.Windows.Forms.DateTimePicker();
            this.txtNoOfRooms = new System.Windows.Forms.TextBox();
            this.nudRent = new System.Windows.Forms.NumericUpDown();
            this.dtpCheckOut = new System.Windows.Forms.DateTimePicker();
            this.Label6 = new System.Windows.Forms.Label();
            this.chkRooms = new System.Windows.Forms.CheckedListBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
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
            ((System.ComponentModel.ISupportInitialize)(this.nudRentPending)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRentPaid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdvance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRent)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbDays
            // 
            this.cmbDays.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDays.FormattingEnabled = true;
            this.cmbDays.Location = new System.Drawing.Point(113, 241);
            this.cmbDays.Name = "cmbDays";
            this.cmbDays.Size = new System.Drawing.Size(150, 21);
            this.cmbDays.TabIndex = 79;
            this.cmbDays.SelectedIndexChanged += new System.EventHandler(this.cmbDays_SelectedIndexChanged);
            this.cmbDays.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDays_KeyPress);
            this.cmbDays.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDays_KeyUp);
            // 
            // lblRoomchange
            // 
            this.lblRoomchange.AutoSize = true;
            this.lblRoomchange.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.lblRoomchange.Location = new System.Drawing.Point(386, 125);
            this.lblRoomchange.Name = "lblRoomchange";
            this.lblRoomchange.Size = new System.Drawing.Size(60, 18);
            this.lblRoomchange.TabIndex = 113;
            this.lblRoomchange.Text = "BN. No.";
            this.lblRoomchange.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboSublocation
            // 
            this.cboSublocation.FormattingEnabled = true;
            this.cboSublocation.Location = new System.Drawing.Point(452, 121);
            this.cboSublocation.Name = "cboSublocation";
            this.cboSublocation.Size = new System.Drawing.Size(150, 21);
            this.cboSublocation.TabIndex = 112;
            this.cboSublocation.SelectedIndexChanged += new System.EventHandler(this.cboSublocation_SelectedIndexChanged);
            // 
            // nudRentPending
            // 
            this.nudRentPending.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudRentPending.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudRentPending.Location = new System.Drawing.Point(289, 402);
            this.nudRentPending.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            131072});
            this.nudRentPending.Minimum = new decimal(new int[] {
            1410065407,
            2,
            0,
            -2147352576});
            this.nudRentPending.Name = "nudRentPending";
            this.nudRentPending.ReadOnly = true;
            this.nudRentPending.Size = new System.Drawing.Size(65, 22);
            this.nudRentPending.TabIndex = 111;
            this.nudRentPending.TabStop = false;
            this.nudRentPending.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label17
            // 
            this.Label17.AutoSize = true;
            this.Label17.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label17.Location = new System.Drawing.Point(194, 399);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(93, 18);
            this.Label17.TabIndex = 110;
            this.Label17.Text = "Rent Pending";
            this.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudRentPaid
            // 
            this.nudRentPaid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudRentPaid.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudRentPaid.Location = new System.Drawing.Point(112, 399);
            this.nudRentPaid.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            131072});
            this.nudRentPaid.Minimum = new decimal(new int[] {
            1215752191,
            23,
            0,
            -2147352576});
            this.nudRentPaid.Name = "nudRentPaid";
            this.nudRentPaid.ReadOnly = true;
            this.nudRentPaid.Size = new System.Drawing.Size(65, 22);
            this.nudRentPaid.TabIndex = 109;
            this.nudRentPaid.TabStop = false;
            this.nudRentPaid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label2.Location = new System.Drawing.Point(12, 399);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(69, 18);
            this.Label2.TabIndex = 108;
            this.Label2.Text = "Rent Paid";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtReason
            // 
            this.txtReason.BackColor = System.Drawing.Color.White;
            this.txtReason.Location = new System.Drawing.Point(121, 430);
            this.txtReason.MaxLength = 100;
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(236, 20);
            this.txtReason.TabIndex = 107;
            // 
            // lbReason
            // 
            this.lbReason.AutoSize = true;
            this.lbReason.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.lbReason.Location = new System.Drawing.Point(18, 430);
            this.lbReason.Name = "lbReason";
            this.lbReason.Size = new System.Drawing.Size(56, 18);
            this.lbReason.TabIndex = 106;
            this.lbReason.Text = "Reason";
            this.lbReason.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label8
            // 
            this.Label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label8.Location = new System.Drawing.Point(205, 322);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(23, 62);
            this.Label8.TabIndex = 105;
            // 
            // nudAdvance
            // 
            this.nudAdvance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudAdvance.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudAdvance.Location = new System.Drawing.Point(115, 327);
            this.nudAdvance.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            131072});
            this.nudAdvance.Name = "nudAdvance";
            this.nudAdvance.ReadOnly = true;
            this.nudAdvance.Size = new System.Drawing.Size(105, 22);
            this.nudAdvance.TabIndex = 104;
            this.nudAdvance.TabStop = false;
            this.nudAdvance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label16.Location = new System.Drawing.Point(280, 166);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(94, 18);
            this.Label16.TabIndex = 103;
            this.Label16.Text = "Given Rooms";
            this.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label16.Visible = false;
            // 
            // chkRooms_given
            // 
            this.chkRooms_given.CheckOnClick = true;
            this.chkRooms_given.Enabled = false;
            this.chkRooms_given.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.chkRooms_given.FormattingEnabled = true;
            this.chkRooms_given.Location = new System.Drawing.Point(383, 165);
            this.chkRooms_given.Name = "chkRooms_given";
            this.chkRooms_given.Size = new System.Drawing.Size(219, 67);
            this.chkRooms_given.TabIndex = 102;
            // 
            // btnLoad
            // 
            this.btnLoad.Image = global::SGMOSOL.ResourceMain.Data_Fetch_22;
            this.btnLoad.Location = new System.Drawing.Point(210, 41);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(44, 30);
            this.btnLoad.TabIndex = 101;
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label15.Location = new System.Drawing.Point(14, 167);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(59, 18);
            this.Label15.TabIndex = 74;
            this.Label15.Text = "Mob No";
            this.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtmobno
            // 
            this.txtmobno.BackColor = System.Drawing.Color.White;
            this.txtmobno.Enabled = false;
            this.txtmobno.Location = new System.Drawing.Point(112, 163);
            this.txtmobno.MaxLength = 100;
            this.txtmobno.Name = "txtmobno";
            this.txtmobno.ReadOnly = true;
            this.txtmobno.Size = new System.Drawing.Size(160, 20);
            this.txtmobno.TabIndex = 75;
            // 
            // txtCounter
            // 
            this.txtCounter.BackColor = System.Drawing.Color.White;
            this.txtCounter.Enabled = false;
            this.txtCounter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCounter.Location = new System.Drawing.Point(388, 4);
            this.txtCounter.Name = "txtCounter";
            this.txtCounter.ReadOnly = true;
            this.txtCounter.Size = new System.Drawing.Size(89, 22);
            this.txtCounter.TabIndex = 100;
            this.txtCounter.TabStop = false;
            // 
            // Label13
            // 
            this.Label13.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label13.Location = new System.Drawing.Point(290, 5);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(74, 21);
            this.Label13.TabIndex = 99;
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
            this.Label14.TabIndex = 97;
            this.Label14.Text = "User";
            this.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtUser
            // 
            this.txtUser.BackColor = System.Drawing.Color.White;
            this.txtUser.Enabled = false;
            this.txtUser.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser.Location = new System.Drawing.Point(112, 5);
            this.txtUser.Name = "txtUser";
            this.txtUser.ReadOnly = true;
            this.txtUser.Size = new System.Drawing.Size(88, 22);
            this.txtUser.TabIndex = 98;
            this.txtUser.TabStop = false;
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label12.ForeColor = System.Drawing.Color.Black;
            this.Label12.Location = new System.Drawing.Point(231, 455);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(41, 18);
            this.Label12.TabIndex = 88;
            this.Label12.Text = "Time";
            this.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpCheckOutTime
            // 
            this.dtpCheckOutTime.Checked = false;
            this.dtpCheckOutTime.CustomFormat = "hh:mm tt";
            this.dtpCheckOutTime.Enabled = false;
            this.dtpCheckOutTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckOutTime.Location = new System.Drawing.Point(289, 466);
            this.dtpCheckOutTime.Name = "dtpCheckOutTime";
            this.dtpCheckOutTime.Size = new System.Drawing.Size(90, 20);
            this.dtpCheckOutTime.TabIndex = 90;
            this.dtpCheckOutTime.Value = new System.DateTime(2005, 9, 20, 0, 0, 0, 0);
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label10.Location = new System.Drawing.Point(292, 90);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(41, 18);
            this.Label10.TabIndex = 96;
            this.Label10.Text = "Time";
            this.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpCheckInTime
            // 
            this.dtpCheckInTime.Checked = false;
            this.dtpCheckInTime.CustomFormat = "hh:mm tt";
            this.dtpCheckInTime.Enabled = false;
            this.dtpCheckInTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckInTime.Location = new System.Drawing.Point(389, 86);
            this.dtpCheckInTime.Name = "dtpCheckInTime";
            this.dtpCheckInTime.Size = new System.Drawing.Size(90, 20);
            this.dtpCheckInTime.TabIndex = 71;
            this.dtpCheckInTime.Value = new System.DateTime(2013, 8, 24, 0, 0, 0, 0);
            // 
            // txtNoOfRooms
            // 
            this.txtNoOfRooms.BackColor = System.Drawing.Color.White;
            this.txtNoOfRooms.Enabled = false;
            this.txtNoOfRooms.Location = new System.Drawing.Point(115, 281);
            this.txtNoOfRooms.MaxLength = 3;
            this.txtNoOfRooms.Name = "txtNoOfRooms";
            this.txtNoOfRooms.ReadOnly = true;
            this.txtNoOfRooms.Size = new System.Drawing.Size(94, 20);
            this.txtNoOfRooms.TabIndex = 81;
            this.txtNoOfRooms.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNoOfRooms.TextChanged += new System.EventHandler(this.txtNoOfRooms_TextChanged);
            this.txtNoOfRooms.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNoOfRooms_KeyPress);
            // 
            // nudRent
            // 
            this.nudRent.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudRent.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudRent.Location = new System.Drawing.Point(115, 362);
            this.nudRent.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            131072});
            this.nudRent.Name = "nudRent";
            this.nudRent.ReadOnly = true;
            this.nudRent.Size = new System.Drawing.Size(108, 22);
            this.nudRent.TabIndex = 84;
            this.nudRent.TabStop = false;
            this.nudRent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dtpCheckOut
            // 
            this.dtpCheckOut.Checked = false;
            this.dtpCheckOut.CustomFormat = "dd/MM/yyyy";
            this.dtpCheckOut.Enabled = false;
            this.dtpCheckOut.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckOut.Location = new System.Drawing.Point(121, 465);
            this.dtpCheckOut.Name = "dtpCheckOut";
            this.dtpCheckOut.Size = new System.Drawing.Size(90, 20);
            this.dtpCheckOut.TabIndex = 86;
            this.dtpCheckOut.Value = new System.DateTime(2005, 9, 20, 0, 0, 0, 0);
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label6.Location = new System.Drawing.Point(325, 304);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(53, 18);
            this.Label6.TabIndex = 87;
            this.Label6.Text = "Rooms";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkRooms
            // 
            this.chkRooms.CheckOnClick = true;
            this.chkRooms.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.chkRooms.FormattingEnabled = true;
            this.chkRooms.Location = new System.Drawing.Point(383, 241);
            this.chkRooms.Name = "chkRooms";
            this.chkRooms.Size = new System.Drawing.Size(219, 214);
            this.chkRooms.TabIndex = 89;
            this.chkRooms.SelectedIndexChanged += new System.EventHandler(this.chkRooms_SelectedIndexChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Enabled = false;
            this.btnSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Image = global::SGMOSOL.ResourceMain.Search;
            this.btnSearch.Location = new System.Drawing.Point(6, 510);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 46);
            this.btnSearch.TabIndex = 94;
            this.btnSearch.Text = "&Search";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::SGMOSOL.ResourceMain.Close;
            this.btnClose.Location = new System.Drawing.Point(513, 510);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 46);
            this.btnClose.TabIndex = 93;
            this.btnClose.Text = "&Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnNew
            // 
            this.btnNew.Enabled = false;
            this.btnNew.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Image = global::SGMOSOL.ResourceMain.NewAddress;
            this.btnNew.Location = new System.Drawing.Point(210, 510);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(100, 46);
            this.btnNew.TabIndex = 95;
            this.btnNew.Text = "&New";
            this.btnNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::SGMOSOL.ResourceMain.Save;
            this.btnSave.Location = new System.Drawing.Point(311, 510);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 46);
            this.btnSave.TabIndex = 91;
            this.btnSave.Text = "&Save\r\n / Print";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Enabled = false;
            this.btnPrint.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Image = global::SGMOSOL.ResourceMain.Print;
            this.btnPrint.Location = new System.Drawing.Point(412, 510);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 46);
            this.btnPrint.TabIndex = 92;
            this.btnPrint.Text = "&Print";
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // lblRent
            // 
            this.lblRent.AutoSize = true;
            this.lblRent.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.lblRent.Location = new System.Drawing.Point(12, 366);
            this.lblRent.Name = "lblRent";
            this.lblRent.Size = new System.Drawing.Size(38, 18);
            this.lblRent.TabIndex = 83;
            this.lblRent.Text = "Rent";
            this.lblRent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label7.Location = new System.Drawing.Point(18, 469);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(68, 18);
            this.Label7.TabIndex = 85;
            this.Label7.Text = "Out Date";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label5.Location = new System.Drawing.Point(12, 326);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(64, 18);
            this.Label5.TabIndex = 82;
            this.Label5.Text = "Advance";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label4.Location = new System.Drawing.Point(12, 285);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(94, 18);
            this.Label4.TabIndex = 80;
            this.Label4.Text = "No of Rooms";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label3.Location = new System.Drawing.Point(13, 246);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(41, 18);
            this.Label3.TabIndex = 78;
            this.Label3.Text = "Days";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label1.Location = new System.Drawing.Point(13, 208);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(41, 18);
            this.Label1.TabIndex = 76;
            this.Label1.Text = "Place";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPlace
            // 
            this.txtPlace.BackColor = System.Drawing.Color.White;
            this.txtPlace.Enabled = false;
            this.txtPlace.Location = new System.Drawing.Point(113, 203);
            this.txtPlace.MaxLength = 100;
            this.txtPlace.Name = "txtPlace";
            this.txtPlace.ReadOnly = true;
            this.txtPlace.Size = new System.Drawing.Size(160, 20);
            this.txtPlace.TabIndex = 77;
            this.txtPlace.TextChanged += new System.EventHandler(this.txtPlace_TextChanged);
            // 
            // lblRemarks
            // 
            this.lblRemarks.AutoSize = true;
            this.lblRemarks.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.lblRemarks.Location = new System.Drawing.Point(13, 129);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(47, 18);
            this.lblRemarks.TabIndex = 72;
            this.lblRemarks.Text = "Name";
            this.lblRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.White;
            this.txtName.Enabled = false;
            this.txtName.Location = new System.Drawing.Point(112, 125);
            this.txtName.MaxLength = 120;
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(268, 20);
            this.txtName.TabIndex = 73;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // txtVchNo
            // 
            this.txtVchNo.BackColor = System.Drawing.Color.White;
            this.txtVchNo.Location = new System.Drawing.Point(112, 46);
            this.txtVchNo.Name = "txtVchNo";
            this.txtVchNo.Size = new System.Drawing.Size(88, 20);
            this.txtVchNo.TabIndex = 68;
            this.txtVchNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtVchNo.TextChanged += new System.EventHandler(this.txtVchNo_TextChanged);
            this.txtVchNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVchNo_KeyPress);
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label11.Location = new System.Drawing.Point(12, 46);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(78, 18);
            this.Label11.TabIndex = 67;
            this.Label11.Text = "Receipt No";
            this.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpCheckIn
            // 
            this.dtpCheckIn.Checked = false;
            this.dtpCheckIn.CustomFormat = "dd/MM/yyyy";
            this.dtpCheckIn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckIn.Location = new System.Drawing.Point(112, 86);
            this.dtpCheckIn.Name = "dtpCheckIn";
            this.dtpCheckIn.Size = new System.Drawing.Size(103, 20);
            this.dtpCheckIn.TabIndex = 70;
            this.dtpCheckIn.Value = new System.DateTime(2013, 8, 24, 0, 0, 0, 0);
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label9.ForeColor = System.Drawing.Color.Black;
            this.Label9.Location = new System.Drawing.Point(13, 90);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(39, 18);
            this.Label9.TabIndex = 69;
            this.Label9.Text = "Date";
            this.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmRoomChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 571);
            this.Controls.Add(this.cmbDays);
            this.Controls.Add(this.lblRoomchange);
            this.Controls.Add(this.cboSublocation);
            this.Controls.Add(this.nudRentPending);
            this.Controls.Add(this.Label17);
            this.Controls.Add(this.nudRentPaid);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.txtReason);
            this.Controls.Add(this.lbReason);
            this.Controls.Add(this.Label8);
            this.Controls.Add(this.nudAdvance);
            this.Controls.Add(this.Label16);
            this.Controls.Add(this.chkRooms_given);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.Label15);
            this.Controls.Add(this.txtmobno);
            this.Controls.Add(this.txtCounter);
            this.Controls.Add(this.Label13);
            this.Controls.Add(this.Label14);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.Label12);
            this.Controls.Add(this.dtpCheckOutTime);
            this.Controls.Add(this.Label10);
            this.Controls.Add(this.dtpCheckInTime);
            this.Controls.Add(this.txtNoOfRooms);
            this.Controls.Add(this.nudRent);
            this.Controls.Add(this.dtpCheckOut);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.chkRooms);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnPrint);
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
            this.Name = "frmRoomChange";
            this.Text = "frmRoomChange";
            this.Load += new System.EventHandler(this.frmRoomChange_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmRoomChange_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmRoomChange_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.nudRentPending)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRentPaid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdvance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ComboBox cmbDays;
        internal System.Windows.Forms.Label lblRoomchange;
        internal System.Windows.Forms.ComboBox cboSublocation;
        internal System.Windows.Forms.NumericUpDown nudRentPending;
        internal System.Windows.Forms.Label Label17;
        internal System.Windows.Forms.NumericUpDown nudRentPaid;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox txtReason;
        internal System.Windows.Forms.Label lbReason;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.NumericUpDown nudAdvance;
        internal System.Windows.Forms.Label Label16;
        internal System.Windows.Forms.CheckedListBox chkRooms_given;
        internal System.Windows.Forms.Button btnLoad;
        internal System.Windows.Forms.Label Label15;
        internal System.Windows.Forms.TextBox txtmobno;
        internal System.Windows.Forms.TextBox txtCounter;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.Label Label14;
        internal System.Windows.Forms.TextBox txtUser;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.DateTimePicker dtpCheckOutTime;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.DateTimePicker dtpCheckInTime;
        internal System.Windows.Forms.TextBox txtNoOfRooms;
        internal System.Windows.Forms.NumericUpDown nudRent;
        internal System.Windows.Forms.DateTimePicker dtpCheckOut;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.CheckedListBox chkRooms;
        internal System.Windows.Forms.Button btnSearch;
        internal System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.Button btnNew;
        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.Button btnPrint;
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
    }
}