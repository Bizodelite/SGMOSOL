namespace SGMOSOL.SCREENS
{
    partial class frmDailyVoucherEntryBed
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
            this.Button1 = new System.Windows.Forms.Button();
            this.rbSerialNo = new System.Windows.Forms.RadioButton();
            this.rbDatewise = new System.Windows.Forms.RadioButton();
            this.nudToSerialNo = new System.Windows.Forms.NumericUpDown();
            this.lblSerialNo = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblLocationName = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.dtpToDt = new System.Windows.Forms.DateTimePicker();
            this.lblToDate = new System.Windows.Forms.Label();
            this.cboCounter = new System.Windows.Forms.ComboBox();
            this.fpsDailyTrans = new System.Windows.Forms.DataGridView();
            this.FpSpreadPendingDetails = new System.Windows.Forms.DataGridView();
            this.ColFromDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColToDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColMinReceiptNo1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColMaxReceiptNo1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColAmount1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColTotalReceipt1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColStatus1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColMinReceiptNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColMaxReceiptNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColTotalReceipt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.nudToSerialNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpsDailyTrans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpreadPendingDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // Button1
            // 
            this.Button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button1.Location = new System.Drawing.Point(606, 32);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(86, 30);
            this.Button1.TabIndex = 217;
            this.Button1.Text = "ImportExcel";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // rbSerialNo
            // 
            this.rbSerialNo.AutoSize = true;
            this.rbSerialNo.Location = new System.Drawing.Point(348, 11);
            this.rbSerialNo.Name = "rbSerialNo";
            this.rbSerialNo.Size = new System.Drawing.Size(71, 17);
            this.rbSerialNo.TabIndex = 216;
            this.rbSerialNo.TabStop = true;
            this.rbSerialNo.Text = "Serial No.";
            this.rbSerialNo.UseVisualStyleBackColor = true;
            this.rbSerialNo.CheckedChanged += new System.EventHandler(this.rbSerialNo_CheckedChanged);
            this.rbSerialNo.Click += new System.EventHandler(this.rbSerialNo_Click);
            // 
            // rbDatewise
            // 
            this.rbDatewise.AutoSize = true;
            this.rbDatewise.Location = new System.Drawing.Point(227, 11);
            this.rbDatewise.Name = "rbDatewise";
            this.rbDatewise.Size = new System.Drawing.Size(69, 17);
            this.rbDatewise.TabIndex = 215;
            this.rbDatewise.TabStop = true;
            this.rbDatewise.Text = "Datewise";
            this.rbDatewise.UseVisualStyleBackColor = true;
            this.rbDatewise.CheckedChanged += new System.EventHandler(this.rbDatewise_CheckedChanged);
            this.rbDatewise.Click += new System.EventHandler(this.rbDatewise_Click);
            // 
            // nudToSerialNo
            // 
            this.nudToSerialNo.Location = new System.Drawing.Point(406, 38);
            this.nudToSerialNo.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudToSerialNo.Name = "nudToSerialNo";
            this.nudToSerialNo.Size = new System.Drawing.Size(97, 20);
            this.nudToSerialNo.TabIndex = 205;
            this.nudToSerialNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblSerialNo
            // 
            this.lblSerialNo.AutoSize = true;
            this.lblSerialNo.ForeColor = System.Drawing.Color.Black;
            this.lblSerialNo.Location = new System.Drawing.Point(336, 41);
            this.lblSerialNo.Name = "lblSerialNo";
            this.lblSerialNo.Size = new System.Drawing.Size(66, 13);
            this.lblSerialNo.TabIndex = 213;
            this.lblSerialNo.Text = "To Serial No";
            this.lblSerialNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnClose
            // 
            this.btnClose.Image = global::SGMOSOL.ResourceMain.Close;
            this.btnClose.Location = new System.Drawing.Point(421, 384);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(94, 42);
            this.btnClose.TabIndex = 212;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblLocationName
            // 
            this.lblLocationName.AutoSize = true;
            this.lblLocationName.Location = new System.Drawing.Point(72, 61);
            this.lblLocationName.Name = "lblLocationName";
            this.lblLocationName.Size = new System.Drawing.Size(0, 13);
            this.lblLocationName.TabIndex = 211;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(12, 61);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(54, 13);
            this.Label2.TabIndex = 210;
            this.Label2.Text = "Location :";
            // 
            // btnLoad
            // 
            this.btnLoad.Image = global::SGMOSOL.ResourceMain.Data_Fetch_22;
            this.btnLoad.Location = new System.Drawing.Point(515, 32);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(85, 30);
            this.btnLoad.TabIndex = 206;
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(10, 82);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(139, 17);
            this.Label1.TabIndex = 209;
            this.Label1.Text = "Pending Transaction";
            // 
            // btnNew
            // 
            this.btnNew.Image = global::SGMOSOL.ResourceMain.NewAddress;
            this.btnNew.Location = new System.Drawing.Point(142, 384);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(97, 42);
            this.btnNew.TabIndex = 208;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Image = global::SGMOSOL.ResourceMain.Save;
            this.btnSubmit.Location = new System.Drawing.Point(284, 384);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(97, 42);
            this.btnSubmit.TabIndex = 207;
            this.btnSubmit.Text = "  Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // dtpToDt
            // 
            this.dtpToDt.Checked = false;
            this.dtpToDt.CustomFormat = "dd/MM/yyyy";
            this.dtpToDt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDt.Location = new System.Drawing.Point(227, 38);
            this.dtpToDt.Name = "dtpToDt";
            this.dtpToDt.Size = new System.Drawing.Size(103, 20);
            this.dtpToDt.TabIndex = 204;
            this.dtpToDt.Value = new System.DateTime(2005, 9, 20, 0, 0, 0, 0);
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.ForeColor = System.Drawing.Color.Black;
            this.lblToDate.Location = new System.Drawing.Point(172, 42);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(46, 13);
            this.lblToDate.TabIndex = 203;
            this.lblToDate.Text = "To Date";
            this.lblToDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboCounter
            // 
            this.cboCounter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCounter.Location = new System.Drawing.Point(12, 22);
            this.cboCounter.Name = "cboCounter";
            this.cboCounter.Size = new System.Drawing.Size(130, 21);
            this.cboCounter.Sorted = true;
            this.cboCounter.TabIndex = 202;
            this.cboCounter.SelectedIndexChanged += new System.EventHandler(this.cboCounter_SelectedIndexChanged);
            // 
            // fpsDailyTrans
            // 
            this.fpsDailyTrans.AllowUserToAddRows = false;
            this.fpsDailyTrans.AllowUserToDeleteRows = false;
            this.fpsDailyTrans.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.fpsDailyTrans.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColFromDate,
            this.ColToDate,
            this.ColMinReceiptNo1,
            this.ColMaxReceiptNo1,
            this.ColAmount1,
            this.ColTotalReceipt1,
            this.ColStatus1});
            this.fpsDailyTrans.Location = new System.Drawing.Point(12, 243);
            this.fpsDailyTrans.Name = "fpsDailyTrans";
            this.fpsDailyTrans.ReadOnly = true;
            this.fpsDailyTrans.Size = new System.Drawing.Size(853, 135);
            this.fpsDailyTrans.TabIndex = 219;
            // 
            // FpSpreadPendingDetails
            // 
            this.FpSpreadPendingDetails.AllowUserToAddRows = false;
            this.FpSpreadPendingDetails.AllowUserToDeleteRows = false;
            this.FpSpreadPendingDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FpSpreadPendingDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColDate,
            this.ColMinReceiptNo,
            this.ColMaxReceiptNo,
            this.ColAmount,
            this.ColTotalReceipt,
            this.ColStatus});
            this.FpSpreadPendingDetails.Location = new System.Drawing.Point(12, 102);
            this.FpSpreadPendingDetails.Name = "FpSpreadPendingDetails";
            this.FpSpreadPendingDetails.ReadOnly = true;
            this.FpSpreadPendingDetails.Size = new System.Drawing.Size(680, 126);
            this.FpSpreadPendingDetails.TabIndex = 218;
            // 
            // ColFromDate
            // 
            this.ColFromDate.HeaderText = "From Date";
            this.ColFromDate.Name = "ColFromDate";
            this.ColFromDate.ReadOnly = true;
            // 
            // ColToDate
            // 
            this.ColToDate.HeaderText = "To Date";
            this.ColToDate.Name = "ColToDate";
            this.ColToDate.ReadOnly = true;
            // 
            // ColMinReceiptNo1
            // 
            this.ColMinReceiptNo1.HeaderText = "Min Receipt No";
            this.ColMinReceiptNo1.Name = "ColMinReceiptNo1";
            this.ColMinReceiptNo1.ReadOnly = true;
            // 
            // ColMaxReceiptNo1
            // 
            this.ColMaxReceiptNo1.HeaderText = "Max Receipt No";
            this.ColMaxReceiptNo1.Name = "ColMaxReceiptNo1";
            this.ColMaxReceiptNo1.ReadOnly = true;
            // 
            // ColAmount1
            // 
            this.ColAmount1.HeaderText = "Amount";
            this.ColAmount1.Name = "ColAmount1";
            this.ColAmount1.ReadOnly = true;
            // 
            // ColTotalReceipt1
            // 
            this.ColTotalReceipt1.HeaderText = "Total Receipt";
            this.ColTotalReceipt1.Name = "ColTotalReceipt1";
            this.ColTotalReceipt1.ReadOnly = true;
            // 
            // ColStatus1
            // 
            this.ColStatus1.HeaderText = "Status";
            this.ColStatus1.Name = "ColStatus1";
            this.ColStatus1.ReadOnly = true;
            // 
            // ColDate
            // 
            this.ColDate.HeaderText = "Date";
            this.ColDate.Name = "ColDate";
            this.ColDate.ReadOnly = true;
            // 
            // ColMinReceiptNo
            // 
            this.ColMinReceiptNo.HeaderText = "Min Receipt No";
            this.ColMinReceiptNo.Name = "ColMinReceiptNo";
            this.ColMinReceiptNo.ReadOnly = true;
            // 
            // ColMaxReceiptNo
            // 
            this.ColMaxReceiptNo.HeaderText = "Max Receipt No";
            this.ColMaxReceiptNo.Name = "ColMaxReceiptNo";
            this.ColMaxReceiptNo.ReadOnly = true;
            // 
            // ColAmount
            // 
            this.ColAmount.HeaderText = "Amount";
            this.ColAmount.Name = "ColAmount";
            this.ColAmount.ReadOnly = true;
            // 
            // ColTotalReceipt
            // 
            this.ColTotalReceipt.HeaderText = "Total Receipt";
            this.ColTotalReceipt.Name = "ColTotalReceipt";
            this.ColTotalReceipt.ReadOnly = true;
            // 
            // ColStatus
            // 
            this.ColStatus.HeaderText = "Status";
            this.ColStatus.Name = "ColStatus";
            this.ColStatus.ReadOnly = true;
            // 
            // frmDailyVoucherEntryBed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 467);
            this.Controls.Add(this.fpsDailyTrans);
            this.Controls.Add(this.FpSpreadPendingDetails);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.rbSerialNo);
            this.Controls.Add(this.rbDatewise);
            this.Controls.Add(this.nudToSerialNo);
            this.Controls.Add(this.lblSerialNo);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblLocationName);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.dtpToDt);
            this.Controls.Add(this.lblToDate);
            this.Controls.Add(this.cboCounter);
            this.Name = "frmDailyVoucherEntryBed";
            this.Text = "DailyVoucherEntryBed";
            this.Load += new System.EventHandler(this.DailyVoucherEntryBed_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudToSerialNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpsDailyTrans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpSpreadPendingDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.RadioButton rbSerialNo;
        internal System.Windows.Forms.RadioButton rbDatewise;
        internal System.Windows.Forms.NumericUpDown nudToSerialNo;
        internal System.Windows.Forms.Label lblSerialNo;
        internal System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.Label lblLocationName;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Button btnLoad;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button btnNew;
        internal System.Windows.Forms.Button btnSubmit;
        internal System.Windows.Forms.DateTimePicker dtpToDt;
        internal System.Windows.Forms.Label lblToDate;
        internal System.Windows.Forms.ComboBox cboCounter;
        internal System.Windows.Forms.DataGridView fpsDailyTrans;
        internal System.Windows.Forms.DataGridView FpSpreadPendingDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColFromDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColToDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColMinReceiptNo1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColMaxReceiptNo1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColAmount1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColTotalReceipt1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColStatus1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColMinReceiptNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColMaxReceiptNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColTotalReceipt;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColStatus;
    }
}