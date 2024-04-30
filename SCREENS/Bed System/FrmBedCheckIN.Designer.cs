namespace SGMOSOL.SCREENS.BedSystem
{
    partial class FrmBedCheckIN
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtScanDoc = new System.Windows.Forms.TextBox();
            this.btnScan = new System.Windows.Forms.Button();
            this.txtImagePath = new System.Windows.Forms.TextBox();
            this.BhaktImgCap = new System.Windows.Forms.Button();
            this.txtScan = new System.Windows.Forms.TextBox();
            this.Label20 = new System.Windows.Forms.Label();
            this.txtCheckIN = new System.Windows.Forms.TextBox();
            this.Label19 = new System.Windows.Forms.Label();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.rdBhakta = new System.Windows.Forms.RadioButton();
            this.rdTrip = new System.Windows.Forms.RadioButton();
            this.rdGroup = new System.Windows.Forms.RadioButton();
            this.txtTotalAmt = new System.Windows.Forms.NumericUpDown();
            this.Label17 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.dtpCheckOutTime = new System.Windows.Forms.DateTimePicker();
            this.dtpCheckOut = new System.Windows.Forms.DateTimePicker();
            this.Label6 = new System.Windows.Forms.Label();
            this.NumericUpDown1 = new System.Windows.Forms.NumericUpDown();
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
            this.dtpCheckInTime = new System.Windows.Forms.DateTimePicker();
            this.dtpCheckIn = new System.Windows.Forms.DateTimePicker();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.nudRent = new System.Windows.Forms.NumericUpDown();
            this.nudAdvance = new System.Windows.Forms.NumericUpDown();
            this.Label14 = new System.Windows.Forms.Label();
            this.Label13 = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.txtVchNo = new System.Windows.Forms.TextBox();
            this.txtCounter = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.fpsPrintReceipt = new System.Windows.Forms.DataGridView();
            this.ColLocation = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDengi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDengiAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PictureBox_Bhakt = new System.Windows.Forms.PictureBox();
            this.imgVideo_1 = new System.Windows.Forms.PictureBox();
            this.imgVideo = new System.Windows.Forms.PictureBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalAmt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdvance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpsPrintReceipt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Bhakt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgVideo_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgVideo)).BeginInit();
            this.SuspendLayout();
            // 
            // txtScanDoc
            // 
            this.txtScanDoc.Location = new System.Drawing.Point(706, 399);
            this.txtScanDoc.Name = "txtScanDoc";
            this.txtScanDoc.Size = new System.Drawing.Size(226, 20);
            this.txtScanDoc.TabIndex = 207;
            this.txtScanDoc.Visible = false;
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(417, 405);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(66, 25);
            this.btnScan.TabIndex = 206;
            this.btnScan.Text = "Scan";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Visible = false;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // txtImagePath
            // 
            this.txtImagePath.Location = new System.Drawing.Point(771, 170);
            this.txtImagePath.Name = "txtImagePath";
            this.txtImagePath.Size = new System.Drawing.Size(162, 20);
            this.txtImagePath.TabIndex = 204;
            this.txtImagePath.Visible = false;
            // 
            // BhaktImgCap
            // 
            this.BhaktImgCap.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BhaktImgCap.Location = new System.Drawing.Point(780, 140);
            this.BhaktImgCap.Name = "BhaktImgCap";
            this.BhaktImgCap.Size = new System.Drawing.Size(132, 23);
            this.BhaktImgCap.TabIndex = 203;
            this.BhaktImgCap.Text = "Bhakt Photo";
            this.BhaktImgCap.UseVisualStyleBackColor = true;
            this.BhaktImgCap.Click += new System.EventHandler(this.BhaktImgCap_Click);
            // 
            // txtScan
            // 
            this.txtScan.BackColor = System.Drawing.Color.White;
            this.txtScan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtScan.Location = new System.Drawing.Point(189, 405);
            this.txtScan.MaxLength = 100;
            this.txtScan.Name = "txtScan";
            this.txtScan.Size = new System.Drawing.Size(219, 26);
            this.txtScan.TabIndex = 201;
            // 
            // Label20
            // 
            this.Label20.AutoSize = true;
            this.Label20.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label20.Location = new System.Drawing.Point(15, 409);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(155, 18);
            this.Label20.TabIndex = 200;
            this.Label20.Text = "Scan Document Name";
            this.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCheckIN
            // 
            this.txtCheckIN.BackColor = System.Drawing.Color.White;
            this.txtCheckIN.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCheckIN.Location = new System.Drawing.Point(634, 84);
            this.txtCheckIN.Name = "txtCheckIN";
            this.txtCheckIN.Size = new System.Drawing.Size(85, 26);
            this.txtCheckIN.TabIndex = 166;
            this.txtCheckIN.TextChanged += new System.EventHandler(this.txtCheckIN_TextChanged);
            // 
            // Label19
            // 
            this.Label19.AutoSize = true;
            this.Label19.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label19.Location = new System.Drawing.Point(605, 88);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(24, 18);
            this.Label19.TabIndex = 165;
            this.Label19.Text = "ID";
            this.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.rdBhakta);
            this.GroupBox1.Controls.Add(this.rdTrip);
            this.GroupBox1.Controls.Add(this.rdGroup);
            this.GroupBox1.Location = new System.Drawing.Point(303, 44);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(370, 35);
            this.GroupBox1.TabIndex = 198;
            this.GroupBox1.TabStop = false;
            // 
            // rdBhakta
            // 
            this.rdBhakta.AutoSize = true;
            this.rdBhakta.Checked = true;
            this.rdBhakta.Location = new System.Drawing.Point(4, 12);
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
            this.rdTrip.Location = new System.Drawing.Point(64, 7);
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
            this.rdGroup.Location = new System.Drawing.Point(145, 8);
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
            this.txtTotalAmt.Location = new System.Drawing.Point(576, 289);
            this.txtTotalAmt.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            131072});
            this.txtTotalAmt.Name = "txtTotalAmt";
            this.txtTotalAmt.Size = new System.Drawing.Size(108, 26);
            this.txtTotalAmt.TabIndex = 189;
            this.txtTotalAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label17
            // 
            this.Label17.AutoSize = true;
            this.Label17.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label17.Location = new System.Drawing.Point(510, 297);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(41, 18);
            this.Label17.TabIndex = 188;
            this.Label17.Text = "Total";
            this.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label3.ForeColor = System.Drawing.Color.Black;
            this.Label3.Location = new System.Drawing.Point(213, 297);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(41, 18);
            this.Label3.TabIndex = 181;
            this.Label3.Text = "Time";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpCheckOutTime
            // 
            this.dtpCheckOutTime.Checked = false;
            this.dtpCheckOutTime.CustomFormat = "hh:mm tt";
            this.dtpCheckOutTime.Enabled = false;
            this.dtpCheckOutTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckOutTime.Location = new System.Drawing.Point(260, 295);
            this.dtpCheckOutTime.Name = "dtpCheckOutTime";
            this.dtpCheckOutTime.Size = new System.Drawing.Size(90, 20);
            this.dtpCheckOutTime.TabIndex = 182;
            this.dtpCheckOutTime.Value = new System.DateTime(2013, 8, 24, 0, 0, 0, 0);
            // 
            // dtpCheckOut
            // 
            this.dtpCheckOut.Checked = false;
            this.dtpCheckOut.CustomFormat = "dd/MM/yyyy";
            this.dtpCheckOut.Enabled = false;
            this.dtpCheckOut.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckOut.Location = new System.Drawing.Point(98, 295);
            this.dtpCheckOut.Name = "dtpCheckOut";
            this.dtpCheckOut.Size = new System.Drawing.Size(103, 20);
            this.dtpCheckOut.TabIndex = 178;
            this.dtpCheckOut.Value = new System.DateTime(2013, 8, 24, 0, 0, 0, 0);
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label6.ForeColor = System.Drawing.Color.Black;
            this.Label6.Location = new System.Drawing.Point(20, 297);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(68, 18);
            this.Label6.TabIndex = 177;
            this.Label6.Text = "Out Date";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NumericUpDown1
            // 
            this.NumericUpDown1.DecimalPlaces = 2;
            this.NumericUpDown1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumericUpDown1.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.NumericUpDown1.Location = new System.Drawing.Point(576, 321);
            this.NumericUpDown1.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            131072});
            this.NumericUpDown1.Name = "NumericUpDown1";
            this.NumericUpDown1.ReadOnly = true;
            this.NumericUpDown1.Size = new System.Drawing.Size(108, 26);
            this.NumericUpDown1.TabIndex = 187;
            this.NumericUpDown1.TabStop = false;
            this.NumericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumericUpDown1.Visible = false;
            // 
            // txtDays
            // 
            this.txtDays.BackColor = System.Drawing.Color.White;
            this.txtDays.Enabled = false;
            this.txtDays.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDays.Location = new System.Drawing.Point(554, 116);
            this.txtDays.Name = "txtDays";
            this.txtDays.ReadOnly = true;
            this.txtDays.Size = new System.Drawing.Size(109, 26);
            this.txtDays.TabIndex = 176;
            this.txtDays.TabStop = false;
            this.txtDays.Text = "1";
            this.txtDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label18.Location = new System.Drawing.Point(510, 119);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(34, 18);
            this.Label18.TabIndex = 175;
            this.Label18.Text = "Day";
            this.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNoOfPerson
            // 
            this.txtNoOfPerson.BackColor = System.Drawing.Color.White;
            this.txtNoOfPerson.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoOfPerson.Location = new System.Drawing.Point(350, 116);
            this.txtNoOfPerson.Name = "txtNoOfPerson";
            this.txtNoOfPerson.Size = new System.Drawing.Size(109, 26);
            this.txtNoOfPerson.TabIndex = 170;
            this.txtNoOfPerson.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNoOfPerson.TextChanged += new System.EventHandler(this.txtNoOfPerson_TextChanged);
            this.txtNoOfPerson.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNoOfPerson_KeyPress);
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label16.Location = new System.Drawing.Point(243, 119);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(93, 18);
            this.Label16.TabIndex = 169;
            this.Label16.Text = "No of person";
            this.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPlace
            // 
            this.txtPlace.BackColor = System.Drawing.Color.White;
            this.txtPlace.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlace.Location = new System.Drawing.Point(95, 116);
            this.txtPlace.Name = "txtPlace";
            this.txtPlace.Size = new System.Drawing.Size(123, 26);
            this.txtPlace.TabIndex = 168;
            this.txtPlace.TextChanged += new System.EventHandler(this.txtPlace_TextChanged);
            this.txtPlace.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlace_KeyPress);
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label15.Location = new System.Drawing.Point(14, 124);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(41, 18);
            this.Label15.TabIndex = 167;
            this.Label15.Text = "Place";
            this.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtmobno
            // 
            this.txtmobno.BackColor = System.Drawing.Color.White;
            this.txtmobno.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtmobno.Location = new System.Drawing.Point(471, 84);
            this.txtmobno.Name = "txtmobno";
            this.txtmobno.Size = new System.Drawing.Size(123, 26);
            this.txtmobno.TabIndex = 164;
            this.txtmobno.TextChanged += new System.EventHandler(this.txtmobno_TextChanged);
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label8.Location = new System.Drawing.Point(391, 88);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(71, 18);
            this.Label8.TabIndex = 162;
            this.Label8.Text = "Mobile No";
            this.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.White;
            this.txtName.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(94, 84);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(285, 26);
            this.txtName.TabIndex = 159;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            this.txtName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtName_KeyPress);
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.Location = new System.Drawing.Point(14, 90);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(47, 18);
            this.Label7.TabIndex = 158;
            this.Label7.Text = "Name";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label5
            // 
            this.Label5.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.Location = new System.Drawing.Point(240, 51);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(74, 21);
            this.Label5.TabIndex = 197;
            this.Label5.Text = "Type";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label10.ForeColor = System.Drawing.Color.Black;
            this.Label10.Location = new System.Drawing.Point(610, 19);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(41, 18);
            this.Label10.TabIndex = 196;
            this.Label10.Text = "Time";
            this.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpCheckInTime
            // 
            this.dtpCheckInTime.Checked = false;
            this.dtpCheckInTime.CustomFormat = "hh:mm tt";
            this.dtpCheckInTime.Enabled = false;
            this.dtpCheckInTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckInTime.Location = new System.Drawing.Point(657, 17);
            this.dtpCheckInTime.Name = "dtpCheckInTime";
            this.dtpCheckInTime.Size = new System.Drawing.Size(90, 20);
            this.dtpCheckInTime.TabIndex = 195;
            this.dtpCheckInTime.Value = new System.DateTime(2013, 8, 24, 0, 0, 0, 0);
            // 
            // dtpCheckIn
            // 
            this.dtpCheckIn.Checked = false;
            this.dtpCheckIn.CustomFormat = "dd/MM/yyyy";
            this.dtpCheckIn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckIn.Location = new System.Drawing.Point(496, 18);
            this.dtpCheckIn.Name = "dtpCheckIn";
            this.dtpCheckIn.Size = new System.Drawing.Size(103, 20);
            this.dtpCheckIn.TabIndex = 193;
            this.dtpCheckIn.Value = new System.DateTime(2013, 8, 24, 0, 0, 0, 0);
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label9.ForeColor = System.Drawing.Color.Black;
            this.Label9.Location = new System.Drawing.Point(445, 20);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(39, 18);
            this.Label9.TabIndex = 194;
            this.Label9.Text = "Date";
            this.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label4
            // 
            this.Label4.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(652, 297);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(21, 90);
            this.Label4.TabIndex = 190;
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
            this.nudRent.Location = new System.Drawing.Point(271, 323);
            this.nudRent.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            131072});
            this.nudRent.Name = "nudRent";
            this.nudRent.ReadOnly = true;
            this.nudRent.Size = new System.Drawing.Size(108, 26);
            this.nudRent.TabIndex = 184;
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
            this.nudAdvance.Location = new System.Drawing.Point(98, 321);
            this.nudAdvance.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            131072});
            this.nudAdvance.Name = "nudAdvance";
            this.nudAdvance.Size = new System.Drawing.Size(108, 26);
            this.nudAdvance.TabIndex = 180;
            this.nudAdvance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudAdvance.Visible = false;
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label14.Location = new System.Drawing.Point(504, 329);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(54, 18);
            this.Label14.TabIndex = 186;
            this.Label14.Text = "Refund";
            this.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label14.Visible = false;
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label13.Location = new System.Drawing.Point(224, 329);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(44, 18);
            this.Label13.TabIndex = 183;
            this.Label13.Text = "Dengi";
            this.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label12.Location = new System.Drawing.Point(20, 323);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(64, 18);
            this.Label12.TabIndex = 179;
            this.Label12.Text = "Advance";
            this.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label12.Visible = false;
            // 
            // txtVchNo
            // 
            this.txtVchNo.BackColor = System.Drawing.Color.White;
            this.txtVchNo.Enabled = false;
            this.txtVchNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVchNo.Location = new System.Drawing.Point(95, 47);
            this.txtVchNo.Name = "txtVchNo";
            this.txtVchNo.ReadOnly = true;
            this.txtVchNo.Size = new System.Drawing.Size(123, 26);
            this.txtVchNo.TabIndex = 163;
            this.txtVchNo.TabStop = false;
            this.txtVchNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCounter
            // 
            this.txtCounter.BackColor = System.Drawing.Color.White;
            this.txtCounter.Enabled = false;
            this.txtCounter.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCounter.Location = new System.Drawing.Point(309, 12);
            this.txtCounter.Name = "txtCounter";
            this.txtCounter.ReadOnly = true;
            this.txtCounter.Size = new System.Drawing.Size(109, 26);
            this.txtCounter.TabIndex = 192;
            this.txtCounter.TabStop = false;
            // 
            // Label2
            // 
            this.Label2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(240, 15);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(74, 21);
            this.Label2.TabIndex = 191;
            this.Label2.Text = "Counter";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(12, 19);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(38, 18);
            this.Label1.TabIndex = 157;
            this.Label1.Text = "User";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtUser
            // 
            this.txtUser.BackColor = System.Drawing.Color.White;
            this.txtUser.Enabled = false;
            this.txtUser.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser.Location = new System.Drawing.Point(95, 12);
            this.txtUser.Name = "txtUser";
            this.txtUser.ReadOnly = true;
            this.txtUser.Size = new System.Drawing.Size(123, 26);
            this.txtUser.TabIndex = 160;
            this.txtUser.TabStop = false;
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label11.Location = new System.Drawing.Point(11, 51);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(78, 18);
            this.Label11.TabIndex = 161;
            this.Label11.Text = "Receipt No";
            this.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fpsPrintReceipt
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.fpsPrintReceipt.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.fpsPrintReceipt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.fpsPrintReceipt.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColLocation,
            this.Column1,
            this.Column2,
            this.ColDengi,
            this.ColQty,
            this.Column3,
            this.ColDengiAmt});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.fpsPrintReceipt.DefaultCellStyle = dataGridViewCellStyle2;
            this.fpsPrintReceipt.Location = new System.Drawing.Point(17, 148);
            this.fpsPrintReceipt.Name = "fpsPrintReceipt";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.fpsPrintReceipt.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.fpsPrintReceipt.Size = new System.Drawing.Size(702, 135);
            this.fpsPrintReceipt.TabIndex = 208;
            this.fpsPrintReceipt.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.fpsPrintReceipt_CellEnter);
            this.fpsPrintReceipt.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.fpsPrintReceipt_CellLeave);
            this.fpsPrintReceipt.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.fpsPrintReceipt_CellValueChanged);
            this.fpsPrintReceipt.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.fpsPrintReceipt_EditingControlShowing);
            this.fpsPrintReceipt.Enter += new System.EventHandler(this.fpsPrintReceipt_Enter);
            this.fpsPrintReceipt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fpsPrintReceipt_KeyDown);
            this.fpsPrintReceipt.Leave += new System.EventHandler(this.fpsPrintReceipt_Leave);
            // 
            // ColLocation
            // 
            this.ColLocation.HeaderText = "Location";
            this.ColLocation.Name = "ColLocation";
            this.ColLocation.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColLocation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColLocation.Width = 200;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.Visible = false;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            this.Column2.Visible = false;
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
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            this.Column3.Visible = false;
            // 
            // ColDengiAmt
            // 
            this.ColDengiAmt.HeaderText = "Dengi Amt";
            this.ColDengiAmt.Name = "ColDengiAmt";
            // 
            // PictureBox_Bhakt
            // 
            this.PictureBox_Bhakt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureBox_Bhakt.Location = new System.Drawing.Point(775, 197);
            this.PictureBox_Bhakt.Name = "PictureBox_Bhakt";
            this.PictureBox_Bhakt.Size = new System.Drawing.Size(162, 118);
            this.PictureBox_Bhakt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBox_Bhakt.TabIndex = 205;
            this.PictureBox_Bhakt.TabStop = false;
            // 
            // imgVideo_1
            // 
            this.imgVideo_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imgVideo_1.Location = new System.Drawing.Point(771, 15);
            this.imgVideo_1.Name = "imgVideo_1";
            this.imgVideo_1.Size = new System.Drawing.Size(162, 118);
            this.imgVideo_1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgVideo_1.TabIndex = 202;
            this.imgVideo_1.TabStop = false;
            this.imgVideo_1.Click += new System.EventHandler(this.imgVideo_1_Click);
            // 
            // imgVideo
            // 
            this.imgVideo.Location = new System.Drawing.Point(519, 350);
            this.imgVideo.Name = "imgVideo";
            this.imgVideo.Size = new System.Drawing.Size(165, 112);
            this.imgVideo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgVideo.TabIndex = 199;
            this.imgVideo.TabStop = false;
            // 
            // btnSearch
            // 
            this.btnSearch.Enabled = false;
            this.btnSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Image = global::SGMOSOL.ResourceMain.Search;
            this.btnSearch.Location = new System.Drawing.Point(14, 465);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 46);
            this.btnSearch.TabIndex = 185;
            this.btnSearch.Text = "&Search";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::SGMOSOL.ResourceMain.Close;
            this.btnClose.Location = new System.Drawing.Point(619, 464);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 46);
            this.btnClose.TabIndex = 173;
            this.btnClose.Text = "&Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnNew
            // 
            this.btnNew.Enabled = false;
            this.btnNew.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Image = global::SGMOSOL.ResourceMain.NewAddress;
            this.btnNew.Location = new System.Drawing.Point(279, 464);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(100, 46);
            this.btnNew.TabIndex = 174;
            this.btnNew.Text = "&New";
            this.btnNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::SGMOSOL.ResourceMain.Save;
            this.btnSave.Location = new System.Drawing.Point(385, 464);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 46);
            this.btnSave.TabIndex = 171;
            this.btnSave.Text = "&Save\r\n / Print";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Enabled = false;
            this.btnPrint.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Image = global::SGMOSOL.ResourceMain.Print;
            this.btnPrint.Location = new System.Drawing.Point(496, 464);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 46);
            this.btnPrint.TabIndex = 172;
            this.btnPrint.Text = "&Print";
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // FrmBedCheckIN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 518);
            this.Controls.Add(this.fpsPrintReceipt);
            this.Controls.Add(this.txtScanDoc);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.PictureBox_Bhakt);
            this.Controls.Add(this.txtImagePath);
            this.Controls.Add(this.BhaktImgCap);
            this.Controls.Add(this.imgVideo_1);
            this.Controls.Add(this.txtScan);
            this.Controls.Add(this.Label20);
            this.Controls.Add(this.imgVideo);
            this.Controls.Add(this.txtCheckIN);
            this.Controls.Add(this.Label19);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.txtTotalAmt);
            this.Controls.Add(this.Label17);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.dtpCheckOutTime);
            this.Controls.Add(this.dtpCheckOut);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.NumericUpDown1);
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
            this.Controls.Add(this.dtpCheckInTime);
            this.Controls.Add(this.dtpCheckIn);
            this.Controls.Add(this.Label9);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.nudRent);
            this.Controls.Add(this.nudAdvance);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.Label14);
            this.Controls.Add(this.Label13);
            this.Controls.Add(this.Label12);
            this.Controls.Add(this.txtVchNo);
            this.Controls.Add(this.txtCounter);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.Label11);
            this.Name = "FrmBedCheckIN";
            this.Text = "FrmBedCheckIN";
            this.Load += new System.EventHandler(this.FrmBedCheckIN_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmBedCheckIN_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmBedCheckIN_KeyUp);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalAmt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdvance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpsPrintReceipt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Bhakt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgVideo_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgVideo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtScanDoc;
        internal System.Windows.Forms.Button btnScan;
        public System.Windows.Forms.PictureBox PictureBox_Bhakt;
        internal System.Windows.Forms.TextBox txtImagePath;
        internal System.Windows.Forms.Button BhaktImgCap;
        public System.Windows.Forms.PictureBox imgVideo_1;
        internal System.Windows.Forms.TextBox txtScan;
        internal System.Windows.Forms.Label Label20;
        public System.Windows.Forms.PictureBox imgVideo;
        internal System.Windows.Forms.TextBox txtCheckIN;
        internal System.Windows.Forms.Label Label19;
        internal System.Windows.Forms.Button btnSearch;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.RadioButton rdBhakta;
        internal System.Windows.Forms.RadioButton rdTrip;
        internal System.Windows.Forms.RadioButton rdGroup;
        internal System.Windows.Forms.NumericUpDown txtTotalAmt;
        internal System.Windows.Forms.Label Label17;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.DateTimePicker dtpCheckOutTime;
        internal System.Windows.Forms.DateTimePicker dtpCheckOut;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.NumericUpDown NumericUpDown1;
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
        internal System.Windows.Forms.DateTimePicker dtpCheckInTime;
        internal System.Windows.Forms.DateTimePicker dtpCheckIn;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.NumericUpDown nudRent;
        internal System.Windows.Forms.NumericUpDown nudAdvance;
        internal System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.Button btnNew;
        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.Button btnPrint;
        internal System.Windows.Forms.Label Label14;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.TextBox txtVchNo;
        internal System.Windows.Forms.TextBox txtCounter;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox txtUser;
        internal System.Windows.Forms.Label Label11;
        private System.Windows.Forms.DataGridView fpsPrintReceipt;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDengi;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDengiAmt;
    }
}