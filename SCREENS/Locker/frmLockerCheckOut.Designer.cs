namespace SGMOSOL.SCREENS
{
    partial class frmLockerCheckOut
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
            this.chkYes = new System.Windows.Forms.CheckBox();
            this.lbYes = new System.Windows.Forms.Label();
            this.dtpCurTime = new System.Windows.Forms.DateTimePicker();
            this.Label22 = new System.Windows.Forms.Label();
            this.dtpCurDate = new System.Windows.Forms.DateTimePicker();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtDays = new System.Windows.Forms.TextBox();
            this.txtCounter = new System.Windows.Forms.TextBox();
            this.Label20 = new System.Windows.Forms.Label();
            this.Label21 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.Label19 = new System.Windows.Forms.Label();
            this.txtVchNo = new System.Windows.Forms.TextBox();
            this.Label18 = new System.Windows.Forms.Label();
            this.Label15 = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.dtpCheckOutTime = new System.Windows.Forms.DateTimePicker();
            this.dtpCheckOut = new System.Windows.Forms.DateTimePicker();
            this.Label7 = new System.Windows.Forms.Label();
            this.nudTotalRent = new System.Windows.Forms.NumericUpDown();
            this.Label13 = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label17 = new System.Windows.Forms.Label();
            this.Label16 = new System.Windows.Forms.Label();
            this.NudRefund = new System.Windows.Forms.NumericUpDown();
            this.nudAdvance = new System.Windows.Forms.NumericUpDown();
            this.Label10 = new System.Windows.Forms.Label();
            this.dtpCheckInTime = new System.Windows.Forms.DateTimePicker();
            this.Label6 = new System.Windows.Forms.Label();
            this.chkLockers = new System.Windows.Forms.CheckedListBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtCheckInVchNo = new System.Windows.Forms.TextBox();
            this.dtpCheckIn = new System.Windows.Forms.DateTimePicker();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudTotalRent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudRefund)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdvance)).BeginInit();
            this.SuspendLayout();
            // 
            // chkYes
            // 
            this.chkYes.AutoSize = true;
            this.chkYes.Location = new System.Drawing.Point(499, 14);
            this.chkYes.Name = "chkYes";
            this.chkYes.Size = new System.Drawing.Size(15, 14);
            this.chkYes.TabIndex = 100;
            this.chkYes.UseVisualStyleBackColor = true;
            this.chkYes.Visible = false;
            // 
            // lbYes
            // 
            this.lbYes.AutoSize = true;
            this.lbYes.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.lbYes.Location = new System.Drawing.Point(451, 13);
            this.lbYes.Name = "lbYes";
            this.lbYes.Size = new System.Drawing.Size(33, 18);
            this.lbYes.TabIndex = 99;
            this.lbYes.Text = "Yes";
            this.lbYes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbYes.Visible = false;
            // 
            // dtpCurTime
            // 
            this.dtpCurTime.Checked = false;
            this.dtpCurTime.CustomFormat = "hh:mm tt";
            this.dtpCurTime.Enabled = false;
            this.dtpCurTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCurTime.Location = new System.Drawing.Point(495, 49);
            this.dtpCurTime.Name = "dtpCurTime";
            this.dtpCurTime.Size = new System.Drawing.Size(90, 20);
            this.dtpCurTime.TabIndex = 98;
            this.dtpCurTime.Value = new System.DateTime(2013, 8, 26, 0, 0, 0, 0);
            // 
            // Label22
            // 
            this.Label22.AutoSize = true;
            this.Label22.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label22.Location = new System.Drawing.Point(446, 49);
            this.Label22.Name = "Label22";
            this.Label22.Size = new System.Drawing.Size(41, 18);
            this.Label22.TabIndex = 97;
            this.Label22.Text = "Time";
            this.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpCurDate
            // 
            this.dtpCurDate.Checked = false;
            this.dtpCurDate.CustomFormat = "dd/MM/yyyy";
            this.dtpCurDate.Enabled = false;
            this.dtpCurDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCurDate.Location = new System.Drawing.Point(337, 46);
            this.dtpCurDate.Name = "dtpCurDate";
            this.dtpCurDate.Size = new System.Drawing.Size(103, 20);
            this.dtpCurDate.TabIndex = 96;
            this.dtpCurDate.Value = new System.DateTime(2013, 8, 26, 0, 0, 0, 0);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label1.Location = new System.Drawing.Point(288, 49);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(39, 18);
            this.Label1.TabIndex = 95;
            this.Label1.Text = "Date";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDays
            // 
            this.txtDays.BackColor = System.Drawing.Color.White;
            this.txtDays.Enabled = false;
            this.txtDays.Location = new System.Drawing.Point(114, 279);
            this.txtDays.Name = "txtDays";
            this.txtDays.ReadOnly = true;
            this.txtDays.Size = new System.Drawing.Size(95, 20);
            this.txtDays.TabIndex = 77;
            this.txtDays.TabStop = false;
            this.txtDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDays.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDays_KeyPress);
            // 
            // txtCounter
            // 
            this.txtCounter.BackColor = System.Drawing.Color.White;
            this.txtCounter.Enabled = false;
            this.txtCounter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCounter.Location = new System.Drawing.Point(339, 13);
            this.txtCounter.Name = "txtCounter";
            this.txtCounter.ReadOnly = true;
            this.txtCounter.Size = new System.Drawing.Size(89, 22);
            this.txtCounter.TabIndex = 94;
            this.txtCounter.TabStop = false;
            // 
            // Label20
            // 
            this.Label20.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label20.Location = new System.Drawing.Point(274, 13);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(74, 21);
            this.Label20.TabIndex = 93;
            this.Label20.Text = "Counter";
            this.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label21
            // 
            this.Label21.AutoSize = true;
            this.Label21.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label21.Location = new System.Drawing.Point(30, 13);
            this.Label21.Name = "Label21";
            this.Label21.Size = new System.Drawing.Size(38, 18);
            this.Label21.TabIndex = 91;
            this.Label21.Text = "User";
            this.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtUser
            // 
            this.txtUser.BackColor = System.Drawing.Color.White;
            this.txtUser.Enabled = false;
            this.txtUser.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser.Location = new System.Drawing.Point(118, 14);
            this.txtUser.Name = "txtUser";
            this.txtUser.ReadOnly = true;
            this.txtUser.Size = new System.Drawing.Size(88, 22);
            this.txtUser.TabIndex = 92;
            this.txtUser.TabStop = false;
            // 
            // Label19
            // 
            this.Label19.AutoSize = true;
            this.Label19.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label19.Location = new System.Drawing.Point(24, 197);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(78, 18);
            this.Label19.TabIndex = 69;
            this.Label19.Text = "Receipt No";
            this.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtVchNo
            // 
            this.txtVchNo.BackColor = System.Drawing.Color.White;
            this.txtVchNo.Enabled = false;
            this.txtVchNo.Location = new System.Drawing.Point(115, 193);
            this.txtVchNo.Name = "txtVchNo";
            this.txtVchNo.ReadOnly = true;
            this.txtVchNo.Size = new System.Drawing.Size(95, 20);
            this.txtVchNo.TabIndex = 70;
            this.txtVchNo.TabStop = false;
            this.txtVchNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtVchNo.TextChanged += new System.EventHandler(this.txtVchNo_TextChanged);
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label18.Location = new System.Drawing.Point(24, 132);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(123, 18);
            this.Label18.TabIndex = 67;
            this.Label18.Text = "Check Out Details";
            this.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label15
            // 
            this.Label15.BackColor = System.Drawing.Color.Black;
            this.Label15.Location = new System.Drawing.Point(24, 167);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(572, 2);
            this.Label15.TabIndex = 68;
            this.Label15.Text = "Label15";
            // 
            // Label14
            // 
            this.Label14.BackColor = System.Drawing.Color.Black;
            this.Label14.Location = new System.Drawing.Point(24, 71);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(572, 2);
            this.Label14.TabIndex = 59;
            this.Label14.Text = "Label14";
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label12.Location = new System.Drawing.Point(448, 198);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(41, 18);
            this.Label12.TabIndex = 73;
            this.Label12.Text = "Time";
            this.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpCheckOutTime
            // 
            this.dtpCheckOutTime.Checked = false;
            this.dtpCheckOutTime.CustomFormat = "hh:mm tt";
            this.dtpCheckOutTime.Enabled = false;
            this.dtpCheckOutTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckOutTime.Location = new System.Drawing.Point(495, 194);
            this.dtpCheckOutTime.Name = "dtpCheckOutTime";
            this.dtpCheckOutTime.Size = new System.Drawing.Size(90, 20);
            this.dtpCheckOutTime.TabIndex = 74;
            this.dtpCheckOutTime.Value = new System.DateTime(2013, 8, 24, 0, 0, 0, 0);
            // 
            // dtpCheckOut
            // 
            this.dtpCheckOut.Checked = false;
            this.dtpCheckOut.CustomFormat = "dd/MM/yyyy";
            this.dtpCheckOut.Enabled = false;
            this.dtpCheckOut.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckOut.Location = new System.Drawing.Point(342, 194);
            this.dtpCheckOut.Name = "dtpCheckOut";
            this.dtpCheckOut.Size = new System.Drawing.Size(98, 20);
            this.dtpCheckOut.TabIndex = 72;
            this.dtpCheckOut.Value = new System.DateTime(2013, 8, 25, 0, 0, 0, 0);
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label7.Location = new System.Drawing.Point(288, 198);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(39, 18);
            this.Label7.TabIndex = 71;
            this.Label7.Text = "Date";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudTotalRent
            // 
            this.nudTotalRent.Enabled = false;
            this.nudTotalRent.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudTotalRent.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudTotalRent.Location = new System.Drawing.Point(114, 323);
            this.nudTotalRent.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudTotalRent.Name = "nudTotalRent";
            this.nudTotalRent.ReadOnly = true;
            this.nudTotalRent.Size = new System.Drawing.Size(108, 22);
            this.nudTotalRent.TabIndex = 80;
            this.nudTotalRent.TabStop = false;
            this.nudTotalRent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label13.Location = new System.Drawing.Point(21, 327);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(76, 18);
            this.Label13.TabIndex = 79;
            this.Label13.Text = "Total Rent";
            this.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnLoad
            // 
            this.btnLoad.Image = global::SGMOSOL.ResourceMain.Data_Fetch_22;
            this.btnLoad.Location = new System.Drawing.Point(221, 86);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(44, 30);
            this.btnLoad.TabIndex = 60;
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label2.Location = new System.Drawing.Point(22, 49);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(113, 18);
            this.Label2.TabIndex = 58;
            this.Label2.Text = "Check In Details";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label17
            // 
            this.Label17.AutoSize = true;
            this.Label17.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label17.Location = new System.Drawing.Point(288, 94);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(39, 18);
            this.Label17.TabIndex = 61;
            this.Label17.Text = "Date";
            this.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label16.Location = new System.Drawing.Point(24, 90);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(78, 18);
            this.Label16.TabIndex = 65;
            this.Label16.Text = "Receipt No";
            this.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NudRefund
            // 
            this.NudRefund.DecimalPlaces = 2;
            this.NudRefund.Enabled = false;
            this.NudRefund.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NudRefund.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.NudRefund.Location = new System.Drawing.Point(115, 367);
            this.NudRefund.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            131072});
            this.NudRefund.Minimum = new decimal(new int[] {
            1410065407,
            2,
            0,
            -2147352576});
            this.NudRefund.Name = "NudRefund";
            this.NudRefund.ReadOnly = true;
            this.NudRefund.Size = new System.Drawing.Size(108, 22);
            this.NudRefund.TabIndex = 82;
            this.NudRefund.TabStop = false;
            this.NudRefund.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // nudAdvance
            // 
            this.nudAdvance.DecimalPlaces = 2;
            this.nudAdvance.Enabled = false;
            this.nudAdvance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudAdvance.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudAdvance.Location = new System.Drawing.Point(114, 234);
            this.nudAdvance.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            131072});
            this.nudAdvance.Name = "nudAdvance";
            this.nudAdvance.ReadOnly = true;
            this.nudAdvance.Size = new System.Drawing.Size(108, 22);
            this.nudAdvance.TabIndex = 76;
            this.nudAdvance.TabStop = false;
            this.nudAdvance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label10.Location = new System.Drawing.Point(448, 95);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(41, 18);
            this.Label10.TabIndex = 63;
            this.Label10.Text = "Time";
            this.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpCheckInTime
            // 
            this.dtpCheckInTime.Checked = false;
            this.dtpCheckInTime.CustomFormat = "hh:mm tt";
            this.dtpCheckInTime.Enabled = false;
            this.dtpCheckInTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckInTime.Location = new System.Drawing.Point(495, 90);
            this.dtpCheckInTime.Name = "dtpCheckInTime";
            this.dtpCheckInTime.Size = new System.Drawing.Size(90, 20);
            this.dtpCheckInTime.TabIndex = 64;
            this.dtpCheckInTime.Value = new System.DateTime(2005, 9, 20, 0, 0, 0, 0);
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label6.Location = new System.Drawing.Point(288, 279);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(57, 18);
            this.Label6.TabIndex = 83;
            this.Label6.Text = "Lockers";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label6.Visible = false;
            // 
            // chkLockers
            // 
            this.chkLockers.CheckOnClick = true;
            this.chkLockers.Enabled = false;
            this.chkLockers.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.chkLockers.FormattingEnabled = true;
            this.chkLockers.Location = new System.Drawing.Point(351, 280);
            this.chkLockers.Name = "chkLockers";
            this.chkLockers.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.chkLockers.Size = new System.Drawing.Size(219, 109);
            this.chkLockers.TabIndex = 84;
            this.chkLockers.SelectedIndexChanged += new System.EventHandler(this.chkLockers_SelectedIndexChanged);
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label5.Location = new System.Drawing.Point(21, 238);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(64, 18);
            this.Label5.TabIndex = 75;
            this.Label5.Text = "Advance";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label4.Location = new System.Drawing.Point(24, 371);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(54, 18);
            this.Label4.TabIndex = 81;
            this.Label4.Text = "Refund";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label3.Location = new System.Drawing.Point(21, 283);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(41, 18);
            this.Label3.TabIndex = 78;
            this.Label3.Text = "Days";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCheckInVchNo
            // 
            this.txtCheckInVchNo.BackColor = System.Drawing.Color.White;
            this.txtCheckInVchNo.Location = new System.Drawing.Point(114, 90);
            this.txtCheckInVchNo.Name = "txtCheckInVchNo";
            this.txtCheckInVchNo.Size = new System.Drawing.Size(83, 20);
            this.txtCheckInVchNo.TabIndex = 66;
            this.txtCheckInVchNo.TabStop = false;
            this.txtCheckInVchNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCheckInVchNo.TextChanged += new System.EventHandler(this.txtCheckInVchNo_TextChanged);
            this.txtCheckInVchNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCheckInVchNo_KeyPress);
            // 
            // dtpCheckIn
            // 
            this.dtpCheckIn.Checked = false;
            this.dtpCheckIn.CustomFormat = "dd/MM/yyyy";
            this.dtpCheckIn.Enabled = false;
            this.dtpCheckIn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckIn.Location = new System.Drawing.Point(337, 90);
            this.dtpCheckIn.Name = "dtpCheckIn";
            this.dtpCheckIn.Size = new System.Drawing.Size(103, 20);
            this.dtpCheckIn.TabIndex = 62;
            this.dtpCheckIn.Value = new System.DateTime(2013, 8, 26, 0, 0, 0, 0);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::SGMOSOL.ResourceMain.Close;
            this.btnClose.Location = new System.Drawing.Point(506, 427);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 46);
            this.btnClose.TabIndex = 124;
            this.btnClose.Text = "&Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Enabled = false;
            this.btnPrint.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Image = global::SGMOSOL.ResourceMain.Print;
            this.btnPrint.Location = new System.Drawing.Point(406, 427);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 46);
            this.btnPrint.TabIndex = 123;
            this.btnPrint.Text = "&Print";
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::SGMOSOL.ResourceMain.Save;
            this.btnSave.Location = new System.Drawing.Point(306, 427);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 46);
            this.btnSave.TabIndex = 122;
            this.btnSave.Text = "&Save\r\n / Print";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnNew
            // 
            this.btnNew.CausesValidation = false;
            this.btnNew.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Image = global::SGMOSOL.ResourceMain._new;
            this.btnNew.Location = new System.Drawing.Point(209, 427);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(97, 46);
            this.btnNew.TabIndex = 121;
            this.btnNew.Text = "&New";
            this.btnNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Image = global::SGMOSOL.ResourceMain.Search;
            this.button5.Location = new System.Drawing.Point(12, 427);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(104, 46);
            this.button5.TabIndex = 120;
            this.button5.Text = "&Find /\r\nSearch";
            this.button5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button5.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // frmLockerCheckOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 480);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.chkYes);
            this.Controls.Add(this.lbYes);
            this.Controls.Add(this.dtpCurTime);
            this.Controls.Add(this.Label22);
            this.Controls.Add(this.dtpCurDate);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.txtDays);
            this.Controls.Add(this.txtCounter);
            this.Controls.Add(this.Label20);
            this.Controls.Add(this.Label21);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.Label19);
            this.Controls.Add(this.txtVchNo);
            this.Controls.Add(this.Label18);
            this.Controls.Add(this.Label15);
            this.Controls.Add(this.Label14);
            this.Controls.Add(this.Label12);
            this.Controls.Add(this.dtpCheckOutTime);
            this.Controls.Add(this.dtpCheckOut);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.nudTotalRent);
            this.Controls.Add(this.Label13);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label17);
            this.Controls.Add(this.Label16);
            this.Controls.Add(this.NudRefund);
            this.Controls.Add(this.nudAdvance);
            this.Controls.Add(this.Label10);
            this.Controls.Add(this.dtpCheckInTime);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.chkLockers);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.txtCheckInVchNo);
            this.Controls.Add(this.dtpCheckIn);
            this.Name = "frmLockerCheckOut";
            this.Text = "Locker Exit Receipt";
            this.Load += new System.EventHandler(this.frmLockerCheckOut_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmLockerCheckOut_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmLockerCheckOut_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.nudTotalRent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudRefund)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdvance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.CheckBox chkYes;
        internal System.Windows.Forms.Label lbYes;
        internal System.Windows.Forms.DateTimePicker dtpCurTime;
        internal System.Windows.Forms.Label Label22;
        internal System.Windows.Forms.DateTimePicker dtpCurDate;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox txtDays;
        internal System.Windows.Forms.TextBox txtCounter;
        internal System.Windows.Forms.Label Label20;
        internal System.Windows.Forms.Label Label21;
        internal System.Windows.Forms.TextBox txtUser;
        internal System.Windows.Forms.Label Label19;
        internal System.Windows.Forms.TextBox txtVchNo;
        internal System.Windows.Forms.Label Label18;
        internal System.Windows.Forms.Label Label15;
        internal System.Windows.Forms.Label Label14;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.DateTimePicker dtpCheckOutTime;
        internal System.Windows.Forms.DateTimePicker dtpCheckOut;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.NumericUpDown nudTotalRent;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.Button btnLoad;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label17;
        internal System.Windows.Forms.Label Label16;
        internal System.Windows.Forms.NumericUpDown NudRefund;
        internal System.Windows.Forms.NumericUpDown nudAdvance;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.DateTimePicker dtpCheckInTime;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.CheckedListBox chkLockers;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox txtCheckInVchNo;
        internal System.Windows.Forms.DateTimePicker dtpCheckIn;
        internal System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.Button btnPrint;
        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.Button btnNew;
        internal System.Windows.Forms.Button button5;
    }
}