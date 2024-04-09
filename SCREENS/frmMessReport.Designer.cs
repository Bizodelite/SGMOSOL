namespace SGMOSOL.DAL.Locker
{
    partial class frmMessReport
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
            this.cboPaymentType = new System.Windows.Forms.ComboBox();
            this.lblPtype = new System.Windows.Forms.Label();
            this.lbl_sublocation = new System.Windows.Forms.Label();
            this.cmbBhaktaNiwas = new System.Windows.Forms.ComboBox();
            this.dtpToTime = new System.Windows.Forms.DateTimePicker();
            this.lbToTime = new System.Windows.Forms.Label();
            this.dtpFromTime = new System.Windows.Forms.DateTimePicker();
            this.lbFromTime = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.dtpToDt = new System.Windows.Forms.DateTimePicker();
            this.lblToDate = new System.Windows.Forms.Label();
            this.dtpFromDt = new System.Windows.Forms.DateTimePicker();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.cboCounter = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cboPaymentType
            // 
            this.cboPaymentType.BackColor = System.Drawing.Color.MistyRose;
            this.cboPaymentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPaymentType.FormattingEnabled = true;
            this.cboPaymentType.Location = new System.Drawing.Point(563, 4);
            this.cboPaymentType.Name = "cboPaymentType";
            this.cboPaymentType.Size = new System.Drawing.Size(119, 21);
            this.cboPaymentType.TabIndex = 88;
            this.cboPaymentType.Visible = false;
            // 
            // lblPtype
            // 
            this.lblPtype.AutoSize = true;
            this.lblPtype.ForeColor = System.Drawing.Color.Black;
            this.lblPtype.Location = new System.Drawing.Point(470, 9);
            this.lblPtype.Name = "lblPtype";
            this.lblPtype.Size = new System.Drawing.Size(75, 13);
            this.lblPtype.TabIndex = 87;
            this.lblPtype.Text = "Payment Type";
            this.lblPtype.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPtype.Visible = false;
            // 
            // lbl_sublocation
            // 
            this.lbl_sublocation.Location = new System.Drawing.Point(219, 10);
            this.lbl_sublocation.Name = "lbl_sublocation";
            this.lbl_sublocation.Size = new System.Drawing.Size(85, 16);
            this.lbl_sublocation.TabIndex = 85;
            this.lbl_sublocation.Text = "Bhakta Niwas";
            this.lbl_sublocation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_sublocation.Visible = false;
            // 
            // cmbBhaktaNiwas
            // 
            this.cmbBhaktaNiwas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBhaktaNiwas.Location = new System.Drawing.Point(324, 7);
            this.cmbBhaktaNiwas.Name = "cmbBhaktaNiwas";
            this.cmbBhaktaNiwas.Size = new System.Drawing.Size(130, 21);
            this.cmbBhaktaNiwas.Sorted = true;
            this.cmbBhaktaNiwas.TabIndex = 86;
            this.cmbBhaktaNiwas.Visible = false;
            // 
            // dtpToTime
            // 
            this.dtpToTime.Checked = false;
            this.dtpToTime.CustomFormat = "hh:mm tt";
            this.dtpToTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpToTime.Location = new System.Drawing.Point(244, 72);
            this.dtpToTime.Name = "dtpToTime";
            this.dtpToTime.ShowUpDown = true;
            this.dtpToTime.Size = new System.Drawing.Size(103, 20);
            this.dtpToTime.TabIndex = 84;
            this.dtpToTime.Value = new System.DateTime(2005, 9, 20, 0, 0, 0, 0);
            this.dtpToTime.Visible = false;
            // 
            // lbToTime
            // 
            this.lbToTime.AutoSize = true;
            this.lbToTime.ForeColor = System.Drawing.Color.Black;
            this.lbToTime.Location = new System.Drawing.Point(189, 76);
            this.lbToTime.Name = "lbToTime";
            this.lbToTime.Size = new System.Drawing.Size(46, 13);
            this.lbToTime.TabIndex = 83;
            this.lbToTime.Text = "To Time";
            this.lbToTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbToTime.Visible = false;
            // 
            // dtpFromTime
            // 
            this.dtpFromTime.Checked = false;
            this.dtpFromTime.CustomFormat = "hh:mm tt";
            this.dtpFromTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpFromTime.Location = new System.Drawing.Point(83, 72);
            this.dtpFromTime.Name = "dtpFromTime";
            this.dtpFromTime.ShowUpDown = true;
            this.dtpFromTime.Size = new System.Drawing.Size(103, 20);
            this.dtpFromTime.TabIndex = 82;
            this.dtpFromTime.Value = new System.DateTime(2005, 9, 20, 0, 0, 0, 0);
            this.dtpFromTime.Visible = false;
            // 
            // lbFromTime
            // 
            this.lbFromTime.AutoSize = true;
            this.lbFromTime.ForeColor = System.Drawing.Color.Black;
            this.lbFromTime.Location = new System.Drawing.Point(13, 76);
            this.lbFromTime.Name = "lbFromTime";
            this.lbFromTime.Size = new System.Drawing.Size(56, 13);
            this.lbFromTime.TabIndex = 81;
            this.lbFromTime.Text = "From Time";
            this.lbFromTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbFromTime.Visible = false;
            // 
            // btnPrint
            // 
            this.btnPrint.Enabled = false;
            this.btnPrint.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Image = global::SGMOSOL.ResourceMain.Print;
            this.btnPrint.Location = new System.Drawing.Point(138, 100);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 46);
            this.btnPrint.TabIndex = 79;
            this.btnPrint.Text = "&Print";
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::SGMOSOL.ResourceMain.Close;
            this.btnClose.Location = new System.Drawing.Point(240, 100);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 46);
            this.btnClose.TabIndex = 80;
            this.btnClose.Text = "&Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dtpToDt
            // 
            this.dtpToDt.Checked = false;
            this.dtpToDt.CustomFormat = "dd/MM/yyyy";
            this.dtpToDt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDt.Location = new System.Drawing.Point(240, 34);
            this.dtpToDt.Name = "dtpToDt";
            this.dtpToDt.Size = new System.Drawing.Size(103, 20);
            this.dtpToDt.TabIndex = 78;
            this.dtpToDt.Value = new System.DateTime(2005, 9, 20, 0, 0, 0, 0);
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.ForeColor = System.Drawing.Color.Black;
            this.lblToDate.Location = new System.Drawing.Point(189, 38);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(46, 13);
            this.lblToDate.TabIndex = 77;
            this.lblToDate.Text = "To Date";
            this.lblToDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpFromDt
            // 
            this.dtpFromDt.Checked = false;
            this.dtpFromDt.CustomFormat = "dd/MM/yyyy";
            this.dtpFromDt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDt.Location = new System.Drawing.Point(80, 34);
            this.dtpFromDt.Name = "dtpFromDt";
            this.dtpFromDt.Size = new System.Drawing.Size(103, 20);
            this.dtpFromDt.TabIndex = 76;
            this.dtpFromDt.Value = new System.DateTime(2005, 9, 20, 0, 0, 0, 0);
            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.ForeColor = System.Drawing.Color.Black;
            this.lblFromDate.Location = new System.Drawing.Point(13, 38);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(56, 13);
            this.lblFromDate.TabIndex = 75;
            this.lblFromDate.Text = "From Date";
            this.lblFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(12, 9);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(59, 16);
            this.Label1.TabIndex = 73;
            this.Label1.Text = "Counter   ";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboCounter
            // 
            this.cboCounter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCounter.Location = new System.Drawing.Point(80, 6);
            this.cboCounter.Name = "cboCounter";
            this.cboCounter.Size = new System.Drawing.Size(130, 21);
            this.cboCounter.Sorted = true;
            this.cboCounter.TabIndex = 74;
            // 
            // frmMessReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 154);
            this.Controls.Add(this.cboPaymentType);
            this.Controls.Add(this.lblPtype);
            this.Controls.Add(this.lbl_sublocation);
            this.Controls.Add(this.cmbBhaktaNiwas);
            this.Controls.Add(this.dtpToTime);
            this.Controls.Add(this.lbToTime);
            this.Controls.Add(this.dtpFromTime);
            this.Controls.Add(this.lbFromTime);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dtpToDt);
            this.Controls.Add(this.lblToDate);
            this.Controls.Add(this.dtpFromDt);
            this.Controls.Add(this.lblFromDate);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.cboCounter);
            this.Name = "frmMessReport";
            this.Text = "Mess Report";
            this.Load += new System.EventHandler(this.frmMessReport_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ComboBox cboPaymentType;
        internal System.Windows.Forms.Label lblPtype;
        internal System.Windows.Forms.Label lbl_sublocation;
        internal System.Windows.Forms.ComboBox cmbBhaktaNiwas;
        internal System.Windows.Forms.DateTimePicker dtpToTime;
        internal System.Windows.Forms.Label lbToTime;
        internal System.Windows.Forms.DateTimePicker dtpFromTime;
        internal System.Windows.Forms.Label lbFromTime;
        internal System.Windows.Forms.Button btnPrint;
        internal System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.DateTimePicker dtpToDt;
        internal System.Windows.Forms.Label lblToDate;
        internal System.Windows.Forms.DateTimePicker dtpFromDt;
        internal System.Windows.Forms.Label lblFromDate;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.ComboBox cboCounter;
    }
}