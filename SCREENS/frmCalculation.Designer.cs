namespace SGMOSOL.SCREENS
{
    partial class frmCalculation
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
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.dgvItemDetails = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.cboCurrency = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtpPrnRcptDt = new System.Windows.Forms.DateTimePicker();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtCounter = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.lblQuntity = new System.Windows.Forms.Label();
            this.lblAmountInWords = new System.Windows.Forms.Label();
            this.lblAlertMsg = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.txtTotalAmount.Enabled = false;
            this.txtTotalAmount.Location = new System.Drawing.Point(757, 378);
            this.txtTotalAmount.Margin = new System.Windows.Forms.Padding(4);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(127, 22);
            this.txtTotalAmount.TabIndex = 40;
            this.txtTotalAmount.TabStop = false;
            this.txtTotalAmount.TextChanged += new System.EventHandler(this.txtTotalAmount_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(643, 382);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(108, 18);
            this.label13.TabIndex = 39;
            this.label13.Text = "Total Amount";
            // 
            // dgvItemDetails
            // 
            this.dgvItemDetails.AllowUserToAddRows = false;
            this.dgvItemDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemDetails.Location = new System.Drawing.Point(116, 178);
            this.dgvItemDetails.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvItemDetails.Name = "dgvItemDetails";
            this.dgvItemDetails.RowHeadersWidth = 51;
            this.dgvItemDetails.RowTemplate.Height = 24;
            this.dgvItemDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemDetails.Size = new System.Drawing.Size(768, 181);
            this.dgvItemDetails.TabIndex = 38;
            this.dgvItemDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemDetails_CellContentClick);
            this.dgvItemDetails.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemDetails_CellValueChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(772, 121);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(112, 42);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtAmount
            // 
            this.txtAmount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.txtAmount.Enabled = false;
            this.txtAmount.Location = new System.Drawing.Point(623, 129);
            this.txtAmount.Margin = new System.Windows.Forms.Padding(4);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.ReadOnly = true;
            this.txtAmount.Size = new System.Drawing.Size(127, 22);
            this.txtAmount.TabIndex = 34;
            this.txtAmount.TabStop = false;
            this.txtAmount.TextChanged += new System.EventHandler(this.txtAmount_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(551, 132);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 18);
            this.label12.TabIndex = 37;
            this.label12.Text = "Amount";
            // 
            // txtQuantity
            // 
            this.txtQuantity.BackColor = System.Drawing.Color.MistyRose;
            this.txtQuantity.Location = new System.Drawing.Point(444, 130);
            this.txtQuantity.Margin = new System.Windows.Forms.Padding(4);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(79, 22);
            this.txtQuantity.TabIndex = 2;
            this.txtQuantity.TextChanged += new System.EventHandler(this.txtQuantity_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(367, 130);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 18);
            this.label11.TabIndex = 36;
            this.label11.Text = "Quantity";
            // 
            // btnNew
            // 
            this.btnNew.CausesValidation = false;
            this.btnNew.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Location = new System.Drawing.Point(283, 428);
            this.btnNew.Margin = new System.Windows.Forms.Padding(4);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(133, 57);
            this.btnNew.TabIndex = 4;
            this.btnNew.Text = "&New";
            this.btnNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(565, 428);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(133, 57);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "&Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Location = new System.Drawing.Point(424, 428);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(133, 57);
            this.btnPrint.TabIndex = 5;
            this.btnPrint.Text = "&Print";
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // cboCurrency
            // 
            this.cboCurrency.FormattingEnabled = true;
            this.cboCurrency.Location = new System.Drawing.Point(227, 126);
            this.cboCurrency.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboCurrency.Name = "cboCurrency";
            this.cboCurrency.Size = new System.Drawing.Size(121, 24);
            this.cboCurrency.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(132, 129);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 18);
            this.label8.TabIndex = 47;
            this.label8.Text = "Currency";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtpPrnRcptDt);
            this.panel1.Controls.Add(this.Label9);
            this.panel1.Controls.Add(this.Label5);
            this.panel1.Controls.Add(this.txtUser);
            this.panel1.Controls.Add(this.txtCounter);
            this.panel1.Controls.Add(this.Label2);
            this.panel1.Location = new System.Drawing.Point(108, 38);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(813, 63);
            this.panel1.TabIndex = 48;
            // 
            // dtpPrnRcptDt
            // 
            this.dtpPrnRcptDt.Checked = false;
            this.dtpPrnRcptDt.CustomFormat = "dd/MM/yyyy";
            this.dtpPrnRcptDt.Enabled = false;
            this.dtpPrnRcptDt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPrnRcptDt.Location = new System.Drawing.Point(581, 28);
            this.dtpPrnRcptDt.Margin = new System.Windows.Forms.Padding(4);
            this.dtpPrnRcptDt.Name = "dtpPrnRcptDt";
            this.dtpPrnRcptDt.Size = new System.Drawing.Size(132, 22);
            this.dtpPrnRcptDt.TabIndex = 13;
            this.dtpPrnRcptDt.Value = new System.DateTime(2005, 9, 20, 0, 0, 0, 0);
            this.dtpPrnRcptDt.ValueChanged += new System.EventHandler(this.dtpPrnRcptDt_ValueChanged);
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label9.ForeColor = System.Drawing.Color.Black;
            this.Label9.Location = new System.Drawing.Point(520, 27);
            this.Label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(54, 23);
            this.Label9.TabIndex = 12;
            this.Label9.Text = "Date";
            this.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.Location = new System.Drawing.Point(293, 25);
            this.Label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(53, 23);
            this.Label5.TabIndex = 10;
            this.Label5.Text = "User";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtUser
            // 
            this.txtUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.txtUser.Enabled = false;
            this.txtUser.Location = new System.Drawing.Point(355, 27);
            this.txtUser.Margin = new System.Windows.Forms.Padding(4);
            this.txtUser.Name = "txtUser";
            this.txtUser.ReadOnly = true;
            this.txtUser.Size = new System.Drawing.Size(147, 22);
            this.txtUser.TabIndex = 11;
            this.txtUser.TabStop = false;
            // 
            // txtCounter
            // 
            this.txtCounter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.txtCounter.Enabled = false;
            this.txtCounter.Location = new System.Drawing.Point(131, 23);
            this.txtCounter.Margin = new System.Windows.Forms.Padding(4);
            this.txtCounter.Name = "txtCounter";
            this.txtCounter.ReadOnly = true;
            this.txtCounter.Size = new System.Drawing.Size(127, 22);
            this.txtCounter.TabIndex = 9;
            this.txtCounter.TabStop = false;
            // 
            // Label2
            // 
            this.Label2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(17, 14);
            this.Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(93, 37);
            this.Label2.TabIndex = 8;
            this.Label2.Text = "Counter";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label2.Click += new System.EventHandler(this.Label2_Click);
            // 
            // reportViewer1
            // 
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "SGMOSOL.Reports.Fund_Calculation_Print.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(95, 493);
            this.reportViewer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(806, 246);
            this.reportViewer1.TabIndex = 49;
            // 
            // lblCurrency
            // 
            this.lblCurrency.AutoSize = true;
            this.lblCurrency.ForeColor = System.Drawing.Color.Red;
            this.lblCurrency.Location = new System.Drawing.Point(227, 159);
            this.lblCurrency.Name = "lblCurrency";
            this.lblCurrency.Size = new System.Drawing.Size(0, 16);
            this.lblCurrency.TabIndex = 50;
            // 
            // lblQuntity
            // 
            this.lblQuntity.AutoSize = true;
            this.lblQuntity.ForeColor = System.Drawing.Color.Red;
            this.lblQuntity.Location = new System.Drawing.Point(444, 158);
            this.lblQuntity.Name = "lblQuntity";
            this.lblQuntity.Size = new System.Drawing.Size(0, 16);
            this.lblQuntity.TabIndex = 51;
            // 
            // lblAmountInWords
            // 
            this.lblAmountInWords.AutoSize = true;
            this.lblAmountInWords.Location = new System.Drawing.Point(113, 378);
            this.lblAmountInWords.Name = "lblAmountInWords";
            this.lblAmountInWords.Size = new System.Drawing.Size(0, 16);
            this.lblAmountInWords.TabIndex = 52;
            // 
            // lblAlertMsg
            // 
            this.lblAlertMsg.AutoSize = true;
            this.lblAlertMsg.ForeColor = System.Drawing.Color.Red;
            this.lblAlertMsg.Location = new System.Drawing.Point(249, 397);
            this.lblAlertMsg.Name = "lblAlertMsg";
            this.lblAlertMsg.Size = new System.Drawing.Size(94, 16);
            this.lblAlertMsg.TabIndex = 53;
            this.lblAlertMsg.Text = "Alert Message";
            // 
            // frmCalculation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1161, 750);
            this.Controls.Add(this.lblAlertMsg);
            this.Controls.Add(this.lblAmountInWords);
            this.Controls.Add(this.lblQuntity);
            this.Controls.Add(this.lblCurrency);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cboCurrency);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.txtTotalAmount);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.dgvItemDetails);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.label11);
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmCalculation";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmCalculation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtTotalAmount;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DataGridView dgvItemDetails;
        private System.Windows.Forms.Button btnAdd;
        internal System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label12;
        internal System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label label11;
        internal System.Windows.Forms.Button btnNew;
        internal System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.ComboBox cboCurrency;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.DateTimePicker dtpPrnRcptDt;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.TextBox txtUser;
        internal System.Windows.Forms.TextBox txtCounter;
        internal System.Windows.Forms.Label Label2;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Label lblCurrency;
        private System.Windows.Forms.Label lblQuntity;
        private System.Windows.Forms.Label lblAmountInWords;
        private System.Windows.Forms.Label lblAlertMsg;
    }
}