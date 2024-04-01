namespace SGMOSOL.SCREENS.BedSystem
{
    partial class FrmBedCheckOut
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
            this.Label6 = new System.Windows.Forms.Label();
            this.dtpCheckInTime = new System.Windows.Forms.DateTimePicker();
            this.dtpCheckIn = new System.Windows.Forms.DateTimePicker();
            this.Label19 = new System.Windows.Forms.Label();
            this.txtVchNo = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.rdBhakta = new System.Windows.Forms.RadioButton();
            this.rdTrip = new System.Windows.Forms.RadioButton();
            this.rdGroup = new System.Windows.Forms.RadioButton();
            this.txtTotalAmt = new System.Windows.Forms.NumericUpDown();
            this.Label17 = new System.Windows.Forms.Label();
            this.nudRefund = new System.Windows.Forms.NumericUpDown();
            this.txtDays = new System.Windows.Forms.TextBox();
            this.Label18 = new System.Windows.Forms.Label();
            this.txtNoOfPerson = new System.Windows.Forms.TextBox();
            this.Label16 = new System.Windows.Forms.Label();
            this.txtPlace = new System.Windows.Forms.TextBox();
            this.Label15 = new System.Windows.Forms.Label();
            this.txtmobno = new System.Windows.Forms.TextBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.dtpCheckOutTime = new System.Windows.Forms.DateTimePicker();
            this.dtpCurDate = new System.Windows.Forms.DateTimePicker();
            this.Label9 = new System.Windows.Forms.Label();
            this.nudRent = new System.Windows.Forms.NumericUpDown();
            this.nudAdvance = new System.Windows.Forms.NumericUpDown();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.Label14 = new System.Windows.Forms.Label();
            this.Label13 = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.txtCheckInVchNo = new System.Windows.Forms.TextBox();
            this.txtCounter = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.fpsPrintReceipt = new System.Windows.Forms.DataGridView();
            this.ColProduct = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColAdvance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDengi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColAdvAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDengiAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalAmt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRefund)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdvance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpsPrintReceipt)).BeginInit();
            this.SuspendLayout();
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label6.ForeColor = System.Drawing.Color.Black;
            this.Label6.Location = new System.Drawing.Point(599, 92);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(41, 18);
            this.Label6.TabIndex = 197;
            this.Label6.Text = "Time";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpCheckInTime
            // 
            this.dtpCheckInTime.Checked = false;
            this.dtpCheckInTime.CustomFormat = "hh:mm tt";
            this.dtpCheckInTime.Enabled = false;
            this.dtpCheckInTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckInTime.Location = new System.Drawing.Point(646, 91);
            this.dtpCheckInTime.Name = "dtpCheckInTime";
            this.dtpCheckInTime.Size = new System.Drawing.Size(90, 20);
            this.dtpCheckInTime.TabIndex = 196;
            this.dtpCheckInTime.Value = new System.DateTime(2013, 8, 24, 0, 0, 0, 0);
            // 
            // dtpCheckIn
            // 
            this.dtpCheckIn.Checked = false;
            this.dtpCheckIn.CustomFormat = "dd/MM/yyyy";
            this.dtpCheckIn.Enabled = false;
            this.dtpCheckIn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckIn.Location = new System.Drawing.Point(473, 92);
            this.dtpCheckIn.Name = "dtpCheckIn";
            this.dtpCheckIn.Size = new System.Drawing.Size(103, 20);
            this.dtpCheckIn.TabIndex = 194;
            this.dtpCheckIn.Value = new System.DateTime(2013, 8, 24, 0, 0, 0, 0);
            // 
            // Label19
            // 
            this.Label19.AutoSize = true;
            this.Label19.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label19.ForeColor = System.Drawing.Color.Black;
            this.Label19.Location = new System.Drawing.Point(365, 93);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(102, 18);
            this.Label19.TabIndex = 195;
            this.Label19.Text = "Check In Date";
            this.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtVchNo
            // 
            this.txtVchNo.BackColor = System.Drawing.Color.White;
            this.txtVchNo.Enabled = false;
            this.txtVchNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVchNo.Location = new System.Drawing.Point(119, 56);
            this.txtVchNo.Name = "txtVchNo";
            this.txtVchNo.ReadOnly = true;
            this.txtVchNo.Size = new System.Drawing.Size(123, 26);
            this.txtVchNo.TabIndex = 193;
            this.txtVchNo.TabStop = false;
            this.txtVchNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtVchNo.TextChanged += new System.EventHandler(this.txtVchNo_TextChanged);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(17, 59);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(99, 18);
            this.Label3.TabIndex = 192;
            this.Label3.Text = "Check Out No";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnLoad
            // 
            this.btnLoad.Image = global::SGMOSOL.ResourceMain.Data_Fetch_22;
            this.btnLoad.Location = new System.Drawing.Point(272, 89);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(44, 30);
            this.btnLoad.TabIndex = 191;
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Enabled = false;
            this.btnSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Image = global::SGMOSOL.ResourceMain.Search;
            this.btnSearch.Location = new System.Drawing.Point(9, 430);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 46);
            this.btnSearch.TabIndex = 190;
            this.btnSearch.Text = "&Search";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.rdBhakta);
            this.GroupBox1.Controls.Add(this.rdTrip);
            this.GroupBox1.Controls.Add(this.rdGroup);
            this.GroupBox1.Location = new System.Drawing.Point(368, 47);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(370, 35);
            this.GroupBox1.TabIndex = 189;
            this.GroupBox1.TabStop = false;
            // 
            // rdBhakta
            // 
            this.rdBhakta.AutoSize = true;
            this.rdBhakta.Checked = true;
            this.rdBhakta.Location = new System.Drawing.Point(8, 10);
            this.rdBhakta.Name = "rdBhakta";
            this.rdBhakta.Size = new System.Drawing.Size(53, 17);
            this.rdBhakta.TabIndex = 0;
            this.rdBhakta.TabStop = true;
            this.rdBhakta.Text = "Bhakt";
            this.rdBhakta.UseVisualStyleBackColor = true;
            this.rdBhakta.Click += new System.EventHandler(this.rdBhakta_Click);
            // 
            // rdTrip
            // 
            this.rdTrip.AutoSize = true;
            this.rdTrip.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdTrip.Location = new System.Drawing.Point(63, 9);
            this.rdTrip.Name = "rdTrip";
            this.rdTrip.Size = new System.Drawing.Size(51, 22);
            this.rdTrip.TabIndex = 125;
            this.rdTrip.Text = "Trip";
            this.rdTrip.UseVisualStyleBackColor = true;
            this.rdTrip.Click += new System.EventHandler(this.rdTrip_Click);
            // 
            // rdGroup
            // 
            this.rdGroup.AutoSize = true;
            this.rdGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdGroup.Location = new System.Drawing.Point(115, 10);
            this.rdGroup.Name = "rdGroup";
            this.rdGroup.Size = new System.Drawing.Size(68, 22);
            this.rdGroup.TabIndex = 126;
            this.rdGroup.Text = "Group";
            this.rdGroup.UseVisualStyleBackColor = true;
            this.rdGroup.Click += new System.EventHandler(this.rdGroup_Click);
            // 
            // txtTotalAmt
            // 
            this.txtTotalAmt.DecimalPlaces = 2;
            this.txtTotalAmt.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalAmt.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTotalAmt.Location = new System.Drawing.Point(577, 323);
            this.txtTotalAmt.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            131072});
            this.txtTotalAmt.Name = "txtTotalAmt";
            this.txtTotalAmt.Size = new System.Drawing.Size(108, 26);
            this.txtTotalAmt.TabIndex = 188;
            this.txtTotalAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label17
            // 
            this.Label17.AutoSize = true;
            this.Label17.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label17.Location = new System.Drawing.Point(508, 325);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(41, 18);
            this.Label17.TabIndex = 187;
            this.Label17.Text = "Total";
            this.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudRefund
            // 
            this.nudRefund.DecimalPlaces = 2;
            this.nudRefund.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudRefund.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudRefund.Location = new System.Drawing.Point(580, 367);
            this.nudRefund.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            131072});
            this.nudRefund.Name = "nudRefund";
            this.nudRefund.ReadOnly = true;
            this.nudRefund.Size = new System.Drawing.Size(108, 26);
            this.nudRefund.TabIndex = 186;
            this.nudRefund.TabStop = false;
            this.nudRefund.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDays
            // 
            this.txtDays.BackColor = System.Drawing.Color.White;
            this.txtDays.Enabled = false;
            this.txtDays.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDays.Location = new System.Drawing.Point(576, 159);
            this.txtDays.Name = "txtDays";
            this.txtDays.ReadOnly = true;
            this.txtDays.Size = new System.Drawing.Size(123, 26);
            this.txtDays.TabIndex = 159;
            this.txtDays.TabStop = false;
            this.txtDays.Text = "1";
            this.txtDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label18.Location = new System.Drawing.Point(524, 162);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(34, 18);
            this.Label18.TabIndex = 185;
            this.Label18.Text = "Day";
            this.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNoOfPerson
            // 
            this.txtNoOfPerson.BackColor = System.Drawing.Color.White;
            this.txtNoOfPerson.Enabled = false;
            this.txtNoOfPerson.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoOfPerson.Location = new System.Drawing.Point(368, 161);
            this.txtNoOfPerson.Name = "txtNoOfPerson";
            this.txtNoOfPerson.Size = new System.Drawing.Size(109, 26);
            this.txtNoOfPerson.TabIndex = 158;
            this.txtNoOfPerson.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label16.Location = new System.Drawing.Point(269, 164);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(93, 18);
            this.Label16.TabIndex = 184;
            this.Label16.Text = "No of person";
            this.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPlace
            // 
            this.txtPlace.BackColor = System.Drawing.Color.White;
            this.txtPlace.Enabled = false;
            this.txtPlace.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlace.Location = new System.Drawing.Point(118, 161);
            this.txtPlace.Name = "txtPlace";
            this.txtPlace.Size = new System.Drawing.Size(123, 26);
            this.txtPlace.TabIndex = 157;
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label15.Location = new System.Drawing.Point(18, 169);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(41, 18);
            this.Label15.TabIndex = 183;
            this.Label15.Text = "Place";
            this.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtmobno
            // 
            this.txtmobno.BackColor = System.Drawing.Color.White;
            this.txtmobno.Enabled = false;
            this.txtmobno.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtmobno.Location = new System.Drawing.Point(576, 122);
            this.txtmobno.Name = "txtmobno";
            this.txtmobno.Size = new System.Drawing.Size(123, 26);
            this.txtmobno.TabIndex = 156;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label8.Location = new System.Drawing.Point(487, 128);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(71, 18);
            this.Label8.TabIndex = 182;
            this.Label8.Text = "Mobile No";
            this.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.White;
            this.txtName.Enabled = false;
            this.txtName.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(119, 125);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(323, 26);
            this.txtName.TabIndex = 155;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.Location = new System.Drawing.Point(18, 130);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(47, 18);
            this.Label7.TabIndex = 181;
            this.Label7.Text = "Name";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label5
            // 
            this.Label5.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.Location = new System.Drawing.Point(289, 58);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(74, 21);
            this.Label5.TabIndex = 180;
            this.Label5.Text = "Type";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label10.ForeColor = System.Drawing.Color.Black;
            this.Label10.Location = new System.Drawing.Point(689, 18);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(41, 18);
            this.Label10.TabIndex = 179;
            this.Label10.Text = "Time";
            this.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpCheckOutTime
            // 
            this.dtpCheckOutTime.Checked = false;
            this.dtpCheckOutTime.CustomFormat = "hh:mm tt";
            this.dtpCheckOutTime.Enabled = false;
            this.dtpCheckOutTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckOutTime.Location = new System.Drawing.Point(736, 16);
            this.dtpCheckOutTime.Name = "dtpCheckOutTime";
            this.dtpCheckOutTime.Size = new System.Drawing.Size(90, 20);
            this.dtpCheckOutTime.TabIndex = 178;
            this.dtpCheckOutTime.Value = new System.DateTime(2013, 8, 24, 0, 0, 0, 0);
            // 
            // dtpCurDate
            // 
            this.dtpCurDate.Checked = false;
            this.dtpCurDate.CustomFormat = "dd/MM/yyyy";
            this.dtpCurDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCurDate.Location = new System.Drawing.Point(577, 16);
            this.dtpCurDate.Name = "dtpCurDate";
            this.dtpCurDate.Size = new System.Drawing.Size(103, 20);
            this.dtpCurDate.TabIndex = 176;
            this.dtpCurDate.Value = new System.DateTime(2013, 8, 24, 0, 0, 0, 0);
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label9.ForeColor = System.Drawing.Color.Black;
            this.Label9.Location = new System.Drawing.Point(523, 15);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(39, 18);
            this.Label9.TabIndex = 177;
            this.Label9.Text = "Date";
            this.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudRent
            // 
            this.nudRent.DecimalPlaces = 2;
            this.nudRent.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudRent.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudRent.Location = new System.Drawing.Point(292, 367);
            this.nudRent.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            131072});
            this.nudRent.Name = "nudRent";
            this.nudRent.ReadOnly = true;
            this.nudRent.Size = new System.Drawing.Size(108, 26);
            this.nudRent.TabIndex = 175;
            this.nudRent.TabStop = false;
            this.nudRent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // nudAdvance
            // 
            this.nudAdvance.DecimalPlaces = 2;
            this.nudAdvance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudAdvance.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudAdvance.Location = new System.Drawing.Point(97, 367);
            this.nudAdvance.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            131072});
            this.nudAdvance.Name = "nudAdvance";
            this.nudAdvance.Size = new System.Drawing.Size(108, 26);
            this.nudAdvance.TabIndex = 173;
            this.nudAdvance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::SGMOSOL.ResourceMain.Close;
            this.btnClose.Location = new System.Drawing.Point(588, 430);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 46);
            this.btnClose.TabIndex = 163;
            this.btnClose.Text = "&Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnNew
            // 
            this.btnNew.Enabled = false;
            this.btnNew.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Image = global::SGMOSOL.ResourceMain.NewAddress;
            this.btnNew.Location = new System.Drawing.Point(288, 430);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(100, 46);
            this.btnNew.TabIndex = 161;
            this.btnNew.Text = "&New";
            this.btnNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::SGMOSOL.ResourceMain.Save;
            this.btnSave.Location = new System.Drawing.Point(388, 430);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 46);
            this.btnSave.TabIndex = 160;
            this.btnSave.Text = "&Save\r\n / Print";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Enabled = false;
            this.btnPrint.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Image = global::SGMOSOL.ResourceMain.Print;
            this.btnPrint.Location = new System.Drawing.Point(488, 430);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 46);
            this.btnPrint.TabIndex = 162;
            this.btnPrint.Text = "&Print";
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label14.Location = new System.Drawing.Point(508, 375);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(54, 18);
            this.Label14.TabIndex = 174;
            this.Label14.Text = "Refund";
            this.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label13.Location = new System.Drawing.Point(246, 375);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(44, 18);
            this.Label13.TabIndex = 172;
            this.Label13.Text = "Dengi";
            this.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label12.Location = new System.Drawing.Point(28, 369);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(64, 18);
            this.Label12.TabIndex = 170;
            this.Label12.Text = "Advance";
            this.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCheckInVchNo
            // 
            this.txtCheckInVchNo.BackColor = System.Drawing.Color.White;
            this.txtCheckInVchNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCheckInVchNo.Location = new System.Drawing.Point(119, 91);
            this.txtCheckInVchNo.Name = "txtCheckInVchNo";
            this.txtCheckInVchNo.Size = new System.Drawing.Size(123, 26);
            this.txtCheckInVchNo.TabIndex = 167;
            this.txtCheckInVchNo.TabStop = false;
            this.txtCheckInVchNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCheckInVchNo.TextChanged += new System.EventHandler(this.txtCheckInVchNo_TextChanged);
            // 
            // txtCounter
            // 
            this.txtCounter.BackColor = System.Drawing.Color.White;
            this.txtCounter.Enabled = false;
            this.txtCounter.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCounter.Location = new System.Drawing.Point(368, 11);
            this.txtCounter.Name = "txtCounter";
            this.txtCounter.ReadOnly = true;
            this.txtCounter.Size = new System.Drawing.Size(115, 26);
            this.txtCounter.TabIndex = 169;
            this.txtCounter.TabStop = false;
            // 
            // Label2
            // 
            this.Label2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(288, 16);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(74, 21);
            this.Label2.TabIndex = 168;
            this.Label2.Text = "Counter";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(16, 19);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(38, 18);
            this.Label1.TabIndex = 164;
            this.Label1.Text = "User";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtUser
            // 
            this.txtUser.BackColor = System.Drawing.Color.White;
            this.txtUser.Enabled = false;
            this.txtUser.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser.Location = new System.Drawing.Point(119, 12);
            this.txtUser.Name = "txtUser";
            this.txtUser.ReadOnly = true;
            this.txtUser.Size = new System.Drawing.Size(123, 26);
            this.txtUser.TabIndex = 165;
            this.txtUser.TabStop = false;
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label11.Location = new System.Drawing.Point(21, 94);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(89, 18);
            this.Label11.TabIndex = 166;
            this.Label11.Text = "Check In No";
            this.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fpsPrintReceipt
            // 
            this.fpsPrintReceipt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.fpsPrintReceipt.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColProduct,
            this.ColAdvance,
            this.ColDengi,
            this.ColQty,
            this.ColAdvAmount,
            this.ColDengiAmt});
            this.fpsPrintReceipt.Location = new System.Drawing.Point(20, 193);
            this.fpsPrintReceipt.Name = "fpsPrintReceipt";
            this.fpsPrintReceipt.Size = new System.Drawing.Size(744, 124);
            this.fpsPrintReceipt.TabIndex = 198;
            this.fpsPrintReceipt.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.fpsPrintReceipt_CellEnter);
            this.fpsPrintReceipt.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.fpsPrintReceipt_CellLeave);
            this.fpsPrintReceipt.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.fpsPrintReceipt_CellValueChanged);
            this.fpsPrintReceipt.Enter += new System.EventHandler(this.fpsPrintReceipt_Enter);
            this.fpsPrintReceipt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fpsPrintReceipt_KeyDown);
            this.fpsPrintReceipt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCheckInVchNo_KeyPress);
            this.fpsPrintReceipt.Leave += new System.EventHandler(this.fpsPrintReceipt_Leave);
            // 
            // ColProduct
            // 
            this.ColProduct.HeaderText = "Product";
            this.ColProduct.Name = "ColProduct";
            this.ColProduct.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColProduct.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColProduct.Width = 200;
            // 
            // ColAdvance
            // 
            this.ColAdvance.HeaderText = "Advance";
            this.ColAdvance.Name = "ColAdvance";
            // 
            // ColDengi
            // 
            this.ColDengi.HeaderText = "Dengi";
            this.ColDengi.Name = "ColDengi";
            // 
            // ColQty
            // 
            this.ColQty.HeaderText = "Qty";
            this.ColQty.Name = "ColQty";
            // 
            // ColAdvAmount
            // 
            this.ColAdvAmount.HeaderText = "Adv Amount";
            this.ColAdvAmount.Name = "ColAdvAmount";
            // 
            // ColDengiAmt
            // 
            this.ColDengiAmt.HeaderText = "Dengi Amt";
            this.ColDengiAmt.Name = "ColDengiAmt";
            // 
            // FrmBedCheckOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(841, 493);
            this.Controls.Add(this.fpsPrintReceipt);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.dtpCheckInTime);
            this.Controls.Add(this.dtpCheckIn);
            this.Controls.Add(this.Label19);
            this.Controls.Add(this.txtVchNo);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.txtTotalAmt);
            this.Controls.Add(this.Label17);
            this.Controls.Add(this.nudRefund);
            this.Controls.Add(this.txtDays);
            this.Controls.Add(this.Label18);
            this.Controls.Add(this.txtNoOfPerson);
            this.Controls.Add(this.Label16);
            this.Controls.Add(this.txtPlace);
            this.Controls.Add(this.Label15);
            this.Controls.Add(this.txtmobno);
            this.Controls.Add(this.Label8);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label10);
            this.Controls.Add(this.dtpCheckOutTime);
            this.Controls.Add(this.dtpCurDate);
            this.Controls.Add(this.Label9);
            this.Controls.Add(this.nudRent);
            this.Controls.Add(this.nudAdvance);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.Label14);
            this.Controls.Add(this.Label13);
            this.Controls.Add(this.Label12);
            this.Controls.Add(this.txtCheckInVchNo);
            this.Controls.Add(this.txtCounter);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.Label11);
            this.Name = "FrmBedCheckOut";
            this.Text = "FrmBedCheckOut";
            this.Load += new System.EventHandler(this.FrmBedCheckOut_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmBedCheckOut_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmBedCheckOut_KeyUp);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalAmt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRefund)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdvance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpsPrintReceipt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.DateTimePicker dtpCheckInTime;
        internal System.Windows.Forms.DateTimePicker dtpCheckIn;
        internal System.Windows.Forms.Label Label19;
        internal System.Windows.Forms.TextBox txtVchNo;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Button btnLoad;
        internal System.Windows.Forms.Button btnSearch;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.RadioButton rdBhakta;
        internal System.Windows.Forms.RadioButton rdTrip;
        internal System.Windows.Forms.RadioButton rdGroup;
        internal System.Windows.Forms.NumericUpDown txtTotalAmt;
        internal System.Windows.Forms.Label Label17;
        internal System.Windows.Forms.NumericUpDown nudRefund;
        internal System.Windows.Forms.TextBox txtDays;
        internal System.Windows.Forms.Label Label18;
        internal System.Windows.Forms.TextBox txtNoOfPerson;
        internal System.Windows.Forms.Label Label16;
        internal System.Windows.Forms.TextBox txtPlace;
        internal System.Windows.Forms.Label Label15;
        internal System.Windows.Forms.TextBox txtmobno;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.TextBox txtName;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.DateTimePicker dtpCheckOutTime;
        internal System.Windows.Forms.DateTimePicker dtpCurDate;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.NumericUpDown nudRent;
        internal System.Windows.Forms.NumericUpDown nudAdvance;
        internal System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.Button btnNew;
        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.Button btnPrint;
        internal System.Windows.Forms.Label Label14;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.TextBox txtCheckInVchNo;
        internal System.Windows.Forms.TextBox txtCounter;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox txtUser;
        internal System.Windows.Forms.Label Label11;
        private System.Windows.Forms.DataGridView fpsPrintReceipt;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColProduct;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColAdvance;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDengi;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColAdvAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDengiAmt;
    }
}