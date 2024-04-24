namespace SGMOSOL.SCREENS.BhaktNiwas
{
    partial class frmRoomCheckinOnline
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
            this.rbBhakt = new System.Windows.Forms.RadioButton();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.lblBarCode = new System.Windows.Forms.Label();
            this.Label19 = new System.Windows.Forms.Label();
            this.txtScan = new System.Windows.Forms.TextBox();
            this.txtTotalAmt = new System.Windows.Forms.TextBox();
            this.lbTotal = new System.Windows.Forms.Label();
            this.cboAuthPer = new System.Windows.Forms.ComboBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.lbAuthPer = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lbRemark = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.dtpCheckOutTime = new System.Windows.Forms.DateTimePicker();
            this.Label8 = new System.Windows.Forms.Label();
            this.nudRent = new System.Windows.Forms.NumericUpDown();
            this.nudAdvance = new System.Windows.Forms.NumericUpDown();
            this.dtpCheckOut = new System.Windows.Forms.DateTimePicker();
            this.lblRent = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label18 = new System.Windows.Forms.Label();
            this.cboSublocation = new System.Windows.Forms.ComboBox();
            this.txtVehcleNo = new System.Windows.Forms.TextBox();
            this.Label17 = new System.Windows.Forms.Label();
            this.txtAppNo = new System.Windows.Forms.TextBox();
            this.Label16 = new System.Windows.Forms.Label();
            this.cboDonner = new System.Windows.Forms.ComboBox();
            this.txtNoOfPersons = new System.Windows.Forms.TextBox();
            this.lbNoOfPersons = new System.Windows.Forms.Label();
            this.Label15 = new System.Windows.Forms.Label();
            this.txtmobno = new System.Windows.Forms.TextBox();
            this.txtNoOfRooms = new System.Windows.Forms.TextBox();
            this.chkRooms = new System.Windows.Forms.CheckedListBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtPlace = new System.Windows.Forms.TextBox();
            this.lblRemarks = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtRoomSrch = new System.Windows.Forms.TextBox();
            this.Label10 = new System.Windows.Forms.Label();
            this.txtLkrAvlbleCt = new System.Windows.Forms.TextBox();
            this.lbAvailableLkrCt = new System.Windows.Forms.Label();
            this.txtCounter = new System.Windows.Forms.TextBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.dtpCheckInTime = new System.Windows.Forms.DateTimePicker();
            this.Label6 = new System.Windows.Forms.Label();
            this.txtVchNo = new System.Windows.Forms.TextBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.dtpCheckIn = new System.Windows.Forms.DateTimePicker();
            this.Label9 = new System.Windows.Forms.Label();
            this.imgVideo = new System.Windows.Forms.PictureBox();
            this.txtDays = new System.Windows.Forms.TextBox();
            this.MultiColumnComboBox1 = new SGMOSOL.SCREENS.MultiColumnComboBox();
            this.MultiColumnComboBox2 = new SGMOSOL.SCREENS.MultiColumnComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudRent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdvance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgVideo)).BeginInit();
            this.SuspendLayout();
            // 
            // rbBhakt
            // 
            this.rbBhakt.AutoSize = true;
            this.rbBhakt.Checked = true;
            this.rbBhakt.Location = new System.Drawing.Point(870, 4);
            this.rbBhakt.Name = "rbBhakt";
            this.rbBhakt.Size = new System.Drawing.Size(53, 17);
            this.rbBhakt.TabIndex = 205;
            this.rbBhakt.TabStop = true;
            this.rbBhakt.Text = "Bhakt";
            this.rbBhakt.UseVisualStyleBackColor = true;
            // 
            // txtBarcode
            // 
            this.txtBarcode.BackColor = System.Drawing.Color.White;
            this.txtBarcode.Location = new System.Drawing.Point(134, 88);
            this.txtBarcode.MaxLength = 20;
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(227, 20);
            this.txtBarcode.TabIndex = 203;
            this.txtBarcode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBarcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBarcode_KeyPress);
            // 
            // lblBarCode
            // 
            this.lblBarCode.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.lblBarCode.Location = new System.Drawing.Point(12, 88);
            this.lblBarCode.Name = "lblBarCode";
            this.lblBarCode.Size = new System.Drawing.Size(67, 21);
            this.lblBarCode.TabIndex = 204;
            this.lblBarCode.Text = "BarCode";
            this.lblBarCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label19
            // 
            this.Label19.AutoSize = true;
            this.Label19.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label19.Location = new System.Drawing.Point(540, 436);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(155, 18);
            this.Label19.TabIndex = 202;
            this.Label19.Text = "Scan Document Name";
            this.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtScan
            // 
            this.txtScan.BackColor = System.Drawing.Color.White;
            this.txtScan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtScan.Location = new System.Drawing.Point(707, 432);
            this.txtScan.MaxLength = 100;
            this.txtScan.Name = "txtScan";
            this.txtScan.Size = new System.Drawing.Size(219, 26);
            this.txtScan.TabIndex = 201;
            // 
            // txtTotalAmt
            // 
            this.txtTotalAmt.BackColor = System.Drawing.Color.White;
            this.txtTotalAmt.Enabled = false;
            this.txtTotalAmt.Location = new System.Drawing.Point(598, 326);
            this.txtTotalAmt.MaxLength = 10;
            this.txtTotalAmt.Name = "txtTotalAmt";
            this.txtTotalAmt.ReadOnly = true;
            this.txtTotalAmt.Size = new System.Drawing.Size(110, 20);
            this.txtTotalAmt.TabIndex = 200;
            this.txtTotalAmt.Text = "0";
            this.txtTotalAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbTotal
            // 
            this.lbTotal.AutoSize = true;
            this.lbTotal.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.lbTotal.Location = new System.Drawing.Point(540, 328);
            this.lbTotal.Name = "lbTotal";
            this.lbTotal.Size = new System.Drawing.Size(41, 18);
            this.lbTotal.TabIndex = 199;
            this.lbTotal.Text = "Total";
            this.lbTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboAuthPer
            // 
            this.cboAuthPer.FormattingEnabled = true;
            this.cboAuthPer.ItemHeight = 13;
            this.cboAuthPer.Location = new System.Drawing.Point(138, 436);
            this.cboAuthPer.Name = "cboAuthPer";
            this.cboAuthPer.Size = new System.Drawing.Size(381, 21);
            this.cboAuthPer.TabIndex = 190;
            this.cboAuthPer.Visible = false;
            // 
            // Label2
            // 
            this.Label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(498, 324);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(21, 25);
            this.Label2.TabIndex = 198;
            // 
            // lbAuthPer
            // 
            this.lbAuthPer.AutoSize = true;
            this.lbAuthPer.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.lbAuthPer.Location = new System.Drawing.Point(15, 436);
            this.lbAuthPer.Name = "lbAuthPer";
            this.lbAuthPer.Size = new System.Drawing.Size(115, 18);
            this.lbAuthPer.TabIndex = 197;
            this.lbAuthPer.Text = "Authority Person";
            this.lbAuthPer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbAuthPer.Visible = false;
            // 
            // txtRemark
            // 
            this.txtRemark.BackColor = System.Drawing.Color.White;
            this.txtRemark.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemark.Location = new System.Drawing.Point(137, 396);
            this.txtRemark.MaxLength = 100;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(382, 26);
            this.txtRemark.TabIndex = 189;
            // 
            // lbRemark
            // 
            this.lbRemark.AutoSize = true;
            this.lbRemark.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.lbRemark.Location = new System.Drawing.Point(15, 400);
            this.lbRemark.Name = "lbRemark";
            this.lbRemark.Size = new System.Drawing.Size(58, 18);
            this.lbRemark.TabIndex = 195;
            this.lbRemark.Text = "Remark";
            this.lbRemark.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label12.ForeColor = System.Drawing.Color.Black;
            this.Label12.Location = new System.Drawing.Point(297, 367);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(41, 18);
            this.Label12.TabIndex = 194;
            this.Label12.Text = "Time";
            this.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpCheckOutTime
            // 
            this.dtpCheckOutTime.Checked = false;
            this.dtpCheckOutTime.CustomFormat = "hh:mm tt";
            this.dtpCheckOutTime.Enabled = false;
            this.dtpCheckOutTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckOutTime.Location = new System.Drawing.Point(404, 363);
            this.dtpCheckOutTime.Name = "dtpCheckOutTime";
            this.dtpCheckOutTime.Size = new System.Drawing.Size(90, 20);
            this.dtpCheckOutTime.TabIndex = 196;
            this.dtpCheckOutTime.Value = new System.DateTime(2005, 9, 20, 0, 0, 0, 0);
            // 
            // Label8
            // 
            this.Label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label8.Location = new System.Drawing.Point(252, 324);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(21, 25);
            this.Label8.TabIndex = 185;
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
            this.nudRent.Location = new System.Drawing.Point(404, 327);
            this.nudRent.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            131072});
            this.nudRent.Name = "nudRent";
            this.nudRent.ReadOnly = true;
            this.nudRent.Size = new System.Drawing.Size(108, 22);
            this.nudRent.TabIndex = 191;
            this.nudRent.TabStop = false;
            this.nudRent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // nudAdvance
            // 
            this.nudAdvance.Cursor = System.Windows.Forms.Cursors.Default;
            this.nudAdvance.DecimalPlaces = 2;
            this.nudAdvance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudAdvance.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudAdvance.Location = new System.Drawing.Point(138, 327);
            this.nudAdvance.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            131072});
            this.nudAdvance.Name = "nudAdvance";
            this.nudAdvance.ReadOnly = true;
            this.nudAdvance.Size = new System.Drawing.Size(113, 22);
            this.nudAdvance.TabIndex = 187;
            this.nudAdvance.TabStop = false;
            this.nudAdvance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudAdvance.ValueChanged += new System.EventHandler(this.nudAdvance_ValueChanged);
            // 
            // dtpCheckOut
            // 
            this.dtpCheckOut.Checked = false;
            this.dtpCheckOut.CustomFormat = "dd/MM/yyyy";
            this.dtpCheckOut.Enabled = false;
            this.dtpCheckOut.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckOut.Location = new System.Drawing.Point(137, 363);
            this.dtpCheckOut.Name = "dtpCheckOut";
            this.dtpCheckOut.Size = new System.Drawing.Size(90, 20);
            this.dtpCheckOut.TabIndex = 193;
            this.dtpCheckOut.Value = new System.DateTime(2005, 9, 20, 0, 0, 0, 0);
            // 
            // lblRent
            // 
            this.lblRent.AutoSize = true;
            this.lblRent.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.lblRent.Location = new System.Drawing.Point(297, 327);
            this.lblRent.Name = "lblRent";
            this.lblRent.Size = new System.Drawing.Size(44, 18);
            this.lblRent.TabIndex = 188;
            this.lblRent.Text = "Dengi";
            this.lblRent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label7.Location = new System.Drawing.Point(15, 367);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(68, 18);
            this.Label7.TabIndex = 192;
            this.Label7.Text = "Out Date";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label5.Location = new System.Drawing.Point(15, 331);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(64, 18);
            this.Label5.TabIndex = 186;
            this.Label5.Text = "Advance";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label18.Location = new System.Drawing.Point(463, 131);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(80, 18);
            this.Label18.TabIndex = 184;
            this.Label18.Text = "Sublocation";
            this.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboSublocation
            // 
            this.cboSublocation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboSublocation.FormattingEnabled = true;
            this.cboSublocation.Location = new System.Drawing.Point(561, 127);
            this.cboSublocation.Name = "cboSublocation";
            this.cboSublocation.Size = new System.Drawing.Size(161, 21);
            this.cboSublocation.TabIndex = 183;
            this.cboSublocation.SelectedIndexChanged += new System.EventHandler(this.cboSublocation_SelectedIndexChanged);
            // 
            // txtVehcleNo
            // 
            this.txtVehcleNo.BackColor = System.Drawing.Color.White;
            this.txtVehcleNo.Location = new System.Drawing.Point(401, 250);
            this.txtVehcleNo.MaxLength = 30;
            this.txtVehcleNo.Name = "txtVehcleNo";
            this.txtVehcleNo.Size = new System.Drawing.Size(160, 20);
            this.txtVehcleNo.TabIndex = 169;
            this.txtVehcleNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label17
            // 
            this.Label17.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label17.Location = new System.Drawing.Point(294, 251);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(102, 21);
            this.Label17.TabIndex = 170;
            this.Label17.Text = "Vehicle No.";
            this.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtAppNo
            // 
            this.txtAppNo.BackColor = System.Drawing.Color.White;
            this.txtAppNo.Location = new System.Drawing.Point(134, 127);
            this.txtAppNo.MaxLength = 20;
            this.txtAppNo.Name = "txtAppNo";
            this.txtAppNo.Size = new System.Drawing.Size(94, 20);
            this.txtAppNo.TabIndex = 162;
            this.txtAppNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label16
            // 
            this.Label16.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label16.Location = new System.Drawing.Point(12, 131);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(67, 21);
            this.Label16.TabIndex = 182;
            this.Label16.Text = "App No.";
            this.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboDonner
            // 
            this.cboDonner.FormattingEnabled = true;
            this.cboDonner.Location = new System.Drawing.Point(240, 128);
            this.cboDonner.Name = "cboDonner";
            this.cboDonner.Size = new System.Drawing.Size(217, 21);
            this.cboDonner.TabIndex = 160;
            this.cboDonner.Visible = false;
            // 
            // txtNoOfPersons
            // 
            this.txtNoOfPersons.BackColor = System.Drawing.Color.White;
            this.txtNoOfPersons.Location = new System.Drawing.Point(401, 290);
            this.txtNoOfPersons.MaxLength = 3;
            this.txtNoOfPersons.Name = "txtNoOfPersons";
            this.txtNoOfPersons.Size = new System.Drawing.Size(94, 20);
            this.txtNoOfPersons.TabIndex = 172;
            this.txtNoOfPersons.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbNoOfPersons
            // 
            this.lbNoOfPersons.AutoSize = true;
            this.lbNoOfPersons.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.lbNoOfPersons.Location = new System.Drawing.Point(294, 294);
            this.lbNoOfPersons.Name = "lbNoOfPersons";
            this.lbNoOfPersons.Size = new System.Drawing.Size(105, 18);
            this.lbNoOfPersons.TabIndex = 174;
            this.lbNoOfPersons.Text = "No. of Persons";
            this.lbNoOfPersons.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label15.Location = new System.Drawing.Point(12, 214);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(59, 18);
            this.Label15.TabIndex = 161;
            this.Label15.Text = "Mob No";
            this.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtmobno
            // 
            this.txtmobno.BackColor = System.Drawing.Color.White;
            this.txtmobno.Location = new System.Drawing.Point(134, 210);
            this.txtmobno.MaxLength = 100;
            this.txtmobno.Name = "txtmobno";
            this.txtmobno.Size = new System.Drawing.Size(153, 20);
            this.txtmobno.TabIndex = 164;
            // 
            // txtNoOfRooms
            // 
            this.txtNoOfRooms.BackColor = System.Drawing.Color.White;
            this.txtNoOfRooms.Location = new System.Drawing.Point(134, 290);
            this.txtNoOfRooms.MaxLength = 3;
            this.txtNoOfRooms.Name = "txtNoOfRooms";
            this.txtNoOfRooms.Size = new System.Drawing.Size(94, 20);
            this.txtNoOfRooms.TabIndex = 171;
            this.txtNoOfRooms.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkRooms
            // 
            this.chkRooms.CheckOnClick = true;
            this.chkRooms.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.chkRooms.FormattingEnabled = true;
            this.chkRooms.Location = new System.Drawing.Point(743, 78);
            this.chkRooms.Name = "chkRooms";
            this.chkRooms.Size = new System.Drawing.Size(219, 340);
            this.chkRooms.TabIndex = 175;
            this.chkRooms.SelectedIndexChanged += new System.EventHandler(this.chkRooms_SelectedIndexChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Enabled = false;
            this.btnSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Image = global::SGMOSOL.ResourceMain.Search;
            this.btnSearch.Location = new System.Drawing.Point(5, 468);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 46);
            this.btnSearch.TabIndex = 177;
            this.btnSearch.Text = "&Search";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::SGMOSOL.ResourceMain.Close;
            this.btnClose.Location = new System.Drawing.Point(661, 468);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 46);
            this.btnClose.TabIndex = 180;
            this.btnClose.Text = "&Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnNew
            // 
            this.btnNew.Enabled = false;
            this.btnNew.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Image = global::SGMOSOL.ResourceMain.NewAddress;
            this.btnNew.Location = new System.Drawing.Point(263, 468);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(100, 46);
            this.btnNew.TabIndex = 178;
            this.btnNew.Text = "&New";
            this.btnNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::SGMOSOL.ResourceMain.Save;
            this.btnSave.Location = new System.Drawing.Point(395, 468);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 46);
            this.btnSave.TabIndex = 176;
            this.btnSave.Text = "&Save\r\n / Print";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Enabled = false;
            this.btnPrint.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Image = global::SGMOSOL.ResourceMain.Print;
            this.btnPrint.Location = new System.Drawing.Point(529, 468);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 46);
            this.btnPrint.TabIndex = 179;
            this.btnPrint.Text = "&Print";
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label4.Location = new System.Drawing.Point(12, 294);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(94, 18);
            this.Label4.TabIndex = 173;
            this.Label4.Text = "No of Rooms";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label3.Location = new System.Drawing.Point(12, 254);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(41, 18);
            this.Label3.TabIndex = 167;
            this.Label3.Text = "Days";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label1.Location = new System.Drawing.Point(294, 214);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(41, 18);
            this.Label1.TabIndex = 165;
            this.Label1.Text = "Place";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPlace
            // 
            this.txtPlace.BackColor = System.Drawing.Color.White;
            this.txtPlace.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlace.Location = new System.Drawing.Point(401, 210);
            this.txtPlace.MaxLength = 100;
            this.txtPlace.Name = "txtPlace";
            this.txtPlace.Size = new System.Drawing.Size(160, 26);
            this.txtPlace.TabIndex = 166;
            // 
            // lblRemarks
            // 
            this.lblRemarks.AutoSize = true;
            this.lblRemarks.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.lblRemarks.Location = new System.Drawing.Point(12, 174);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(47, 18);
            this.lblRemarks.TabIndex = 159;
            this.lblRemarks.Text = "Name";
            this.lblRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.White;
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(134, 166);
            this.txtName.MaxLength = 120;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(427, 26);
            this.txtName.TabIndex = 163;
            // 
            // txtRoomSrch
            // 
            this.txtRoomSrch.BackColor = System.Drawing.Color.White;
            this.txtRoomSrch.Location = new System.Drawing.Point(741, 40);
            this.txtRoomSrch.MaxLength = 3;
            this.txtRoomSrch.Name = "txtRoomSrch";
            this.txtRoomSrch.Size = new System.Drawing.Size(219, 20);
            this.txtRoomSrch.TabIndex = 158;
            this.txtRoomSrch.TextChanged += new System.EventHandler(this.txtRoomSrch_TextChanged);
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label10.ForeColor = System.Drawing.Color.Black;
            this.Label10.Location = new System.Drawing.Point(510, 49);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(41, 18);
            this.Label10.TabIndex = 157;
            this.Label10.Text = "Time";
            this.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtLkrAvlbleCt
            // 
            this.txtLkrAvlbleCt.BackColor = System.Drawing.Color.White;
            this.txtLkrAvlbleCt.Enabled = false;
            this.txtLkrAvlbleCt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLkrAvlbleCt.Location = new System.Drawing.Point(739, 5);
            this.txtLkrAvlbleCt.Name = "txtLkrAvlbleCt";
            this.txtLkrAvlbleCt.ReadOnly = true;
            this.txtLkrAvlbleCt.Size = new System.Drawing.Size(89, 22);
            this.txtLkrAvlbleCt.TabIndex = 156;
            this.txtLkrAvlbleCt.TabStop = false;
            // 
            // lbAvailableLkrCt
            // 
            this.lbAvailableLkrCt.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.lbAvailableLkrCt.Location = new System.Drawing.Point(565, 5);
            this.lbAvailableLkrCt.Name = "lbAvailableLkrCt";
            this.lbAvailableLkrCt.Size = new System.Drawing.Size(162, 21);
            this.lbAvailableLkrCt.TabIndex = 155;
            this.lbAvailableLkrCt.Text = "Available No.of Rooms";
            this.lbAvailableLkrCt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCounter
            // 
            this.txtCounter.BackColor = System.Drawing.Color.White;
            this.txtCounter.Enabled = false;
            this.txtCounter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCounter.Location = new System.Drawing.Point(401, 4);
            this.txtCounter.Name = "txtCounter";
            this.txtCounter.ReadOnly = true;
            this.txtCounter.Size = new System.Drawing.Size(94, 22);
            this.txtCounter.TabIndex = 154;
            this.txtCounter.TabStop = false;
            // 
            // Label13
            // 
            this.Label13.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label13.Location = new System.Drawing.Point(294, 4);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(67, 21);
            this.Label13.TabIndex = 153;
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
            this.Label14.TabIndex = 151;
            this.Label14.Text = "User";
            this.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtUser
            // 
            this.txtUser.BackColor = System.Drawing.Color.White;
            this.txtUser.Enabled = false;
            this.txtUser.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser.Location = new System.Drawing.Point(134, 5);
            this.txtUser.Name = "txtUser";
            this.txtUser.ReadOnly = true;
            this.txtUser.Size = new System.Drawing.Size(88, 22);
            this.txtUser.TabIndex = 152;
            this.txtUser.TabStop = false;
            // 
            // dtpCheckInTime
            // 
            this.dtpCheckInTime.Checked = false;
            this.dtpCheckInTime.CustomFormat = "hh:mm tt";
            this.dtpCheckInTime.Enabled = false;
            this.dtpCheckInTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckInTime.Location = new System.Drawing.Point(569, 45);
            this.dtpCheckInTime.Name = "dtpCheckInTime";
            this.dtpCheckInTime.Size = new System.Drawing.Size(90, 20);
            this.dtpCheckInTime.TabIndex = 149;
            this.dtpCheckInTime.Value = new System.DateTime(2013, 8, 24, 0, 0, 0, 0);
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label6.Location = new System.Drawing.Point(688, 44);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(53, 18);
            this.Label6.TabIndex = 150;
            this.Label6.Text = "Rooms";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtVchNo
            // 
            this.txtVchNo.BackColor = System.Drawing.Color.White;
            this.txtVchNo.Enabled = false;
            this.txtVchNo.Location = new System.Drawing.Point(134, 47);
            this.txtVchNo.Name = "txtVchNo";
            this.txtVchNo.ReadOnly = true;
            this.txtVchNo.Size = new System.Drawing.Size(88, 20);
            this.txtVchNo.TabIndex = 146;
            this.txtVchNo.TabStop = false;
            this.txtVchNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label11.Location = new System.Drawing.Point(12, 49);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(78, 18);
            this.Label11.TabIndex = 145;
            this.Label11.Text = "Receipt No";
            this.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpCheckIn
            // 
            this.dtpCheckIn.Checked = false;
            this.dtpCheckIn.CustomFormat = "dd/MM/yyyy";
            this.dtpCheckIn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckIn.Location = new System.Drawing.Point(401, 45);
            this.dtpCheckIn.Name = "dtpCheckIn";
            this.dtpCheckIn.Size = new System.Drawing.Size(103, 20);
            this.dtpCheckIn.TabIndex = 148;
            this.dtpCheckIn.Value = new System.DateTime(2013, 8, 24, 0, 0, 0, 0);
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label9.ForeColor = System.Drawing.Color.Black;
            this.Label9.Location = new System.Drawing.Point(294, 49);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(39, 18);
            this.Label9.TabIndex = 147;
            this.Label9.Text = "Date";
            this.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // imgVideo
            // 
            this.imgVideo.Location = new System.Drawing.Point(590, 170);
            this.imgVideo.Name = "imgVideo";
            this.imgVideo.Size = new System.Drawing.Size(133, 123);
            this.imgVideo.TabIndex = 181;
            this.imgVideo.TabStop = false;
            // 
            // txtDays
            // 
            this.txtDays.Location = new System.Drawing.Point(134, 250);
            this.txtDays.Name = "txtDays";
            this.txtDays.Size = new System.Drawing.Size(100, 20);
            this.txtDays.TabIndex = 206;
            // 
            // MultiColumnComboBox1
            // 
            this.MultiColumnComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.MultiColumnComboBox1.FormattingEnabled = true;
            this.MultiColumnComboBox1.Location = new System.Drawing.Point(376, 88);
            this.MultiColumnComboBox1.Name = "MultiColumnComboBox1";
            this.MultiColumnComboBox1.Size = new System.Drawing.Size(319, 21);
            this.MultiColumnComboBox1.TabIndex = 207;
            // 
            // MultiColumnComboBox2
            // 
            this.MultiColumnComboBox2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.MultiColumnComboBox2.FormattingEnabled = true;
            this.MultiColumnComboBox2.Location = new System.Drawing.Point(376, 88);
            this.MultiColumnComboBox2.Name = "MultiColumnComboBox2";
            this.MultiColumnComboBox2.Size = new System.Drawing.Size(319, 21);
            this.MultiColumnComboBox2.TabIndex = 208;
            // 
            // frmRoomCheckinOnline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 531);
            this.Controls.Add(this.MultiColumnComboBox2);
            this.Controls.Add(this.MultiColumnComboBox1);
            this.Controls.Add(this.txtDays);
            this.Controls.Add(this.rbBhakt);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.lblBarCode);
            this.Controls.Add(this.Label19);
            this.Controls.Add(this.txtScan);
            this.Controls.Add(this.txtTotalAmt);
            this.Controls.Add(this.lbTotal);
            this.Controls.Add(this.cboAuthPer);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.lbAuthPer);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.lbRemark);
            this.Controls.Add(this.Label12);
            this.Controls.Add(this.dtpCheckOutTime);
            this.Controls.Add(this.Label8);
            this.Controls.Add(this.nudRent);
            this.Controls.Add(this.nudAdvance);
            this.Controls.Add(this.dtpCheckOut);
            this.Controls.Add(this.lblRent);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label18);
            this.Controls.Add(this.cboSublocation);
            this.Controls.Add(this.txtVehcleNo);
            this.Controls.Add(this.Label17);
            this.Controls.Add(this.txtAppNo);
            this.Controls.Add(this.Label16);
            this.Controls.Add(this.cboDonner);
            this.Controls.Add(this.imgVideo);
            this.Controls.Add(this.txtNoOfPersons);
            this.Controls.Add(this.lbNoOfPersons);
            this.Controls.Add(this.Label15);
            this.Controls.Add(this.txtmobno);
            this.Controls.Add(this.txtNoOfRooms);
            this.Controls.Add(this.chkRooms);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.txtPlace);
            this.Controls.Add(this.lblRemarks);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtRoomSrch);
            this.Controls.Add(this.Label10);
            this.Controls.Add(this.txtLkrAvlbleCt);
            this.Controls.Add(this.lbAvailableLkrCt);
            this.Controls.Add(this.txtCounter);
            this.Controls.Add(this.Label13);
            this.Controls.Add(this.Label14);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.dtpCheckInTime);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.txtVchNo);
            this.Controls.Add(this.Label11);
            this.Controls.Add(this.dtpCheckIn);
            this.Controls.Add(this.Label9);
            this.Name = "frmRoomCheckinOnline";
            this.Text = "frm Room In Time Online";
            this.Load += new System.EventHandler(this.frmRoomCheckinOnline_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudRent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdvance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgVideo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.RadioButton rbBhakt;
        internal System.Windows.Forms.TextBox txtBarcode;
        internal System.Windows.Forms.Label lblBarCode;
        internal System.Windows.Forms.Label Label19;
        internal System.Windows.Forms.TextBox txtScan;
        internal System.Windows.Forms.TextBox txtTotalAmt;
        internal System.Windows.Forms.Label lbTotal;
        internal System.Windows.Forms.ComboBox cboAuthPer;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label lbAuthPer;
        internal System.Windows.Forms.TextBox txtRemark;
        internal System.Windows.Forms.Label lbRemark;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.DateTimePicker dtpCheckOutTime;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.NumericUpDown nudRent;
        internal System.Windows.Forms.NumericUpDown nudAdvance;
        internal System.Windows.Forms.DateTimePicker dtpCheckOut;
        internal System.Windows.Forms.Label lblRent;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label18;
        internal System.Windows.Forms.ComboBox cboSublocation;
        internal System.Windows.Forms.TextBox txtVehcleNo;
        internal System.Windows.Forms.Label Label17;
        internal System.Windows.Forms.TextBox txtAppNo;
        internal System.Windows.Forms.Label Label16;
        internal System.Windows.Forms.ComboBox cboDonner;
        public System.Windows.Forms.PictureBox imgVideo;
        internal System.Windows.Forms.TextBox txtNoOfPersons;
        internal System.Windows.Forms.Label lbNoOfPersons;
        internal System.Windows.Forms.Label Label15;
        internal System.Windows.Forms.TextBox txtmobno;
        internal System.Windows.Forms.TextBox txtNoOfRooms;
        internal System.Windows.Forms.CheckedListBox chkRooms;
        internal System.Windows.Forms.Button btnSearch;
        internal System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.Button btnNew;
        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.Button btnPrint;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox txtPlace;
        internal System.Windows.Forms.Label lblRemarks;
        internal System.Windows.Forms.TextBox txtName;
        internal System.Windows.Forms.TextBox txtRoomSrch;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.TextBox txtLkrAvlbleCt;
        internal System.Windows.Forms.Label lbAvailableLkrCt;
        internal System.Windows.Forms.TextBox txtCounter;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.Label Label14;
        internal System.Windows.Forms.TextBox txtUser;
        internal System.Windows.Forms.DateTimePicker dtpCheckInTime;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.TextBox txtVchNo;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.DateTimePicker dtpCheckIn;
        internal System.Windows.Forms.Label Label9;
        private System.Windows.Forms.TextBox txtDays;
        private MultiColumnComboBox MultiColumnComboBox1;
        private MultiColumnComboBox MultiColumnComboBox2;
    }
}