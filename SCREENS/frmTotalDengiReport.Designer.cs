namespace SGMOSOL.SCREENS
{
    partial class frmTotalDengiReport
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
            this.pnlMaster = new System.Windows.Forms.Panel();
            this.brnClose = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.dtToDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtfromDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCounter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnlMaster.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMaster
            // 
            this.pnlMaster.BackColor = System.Drawing.Color.Honeydew;
            this.pnlMaster.Controls.Add(this.reportViewer1);
            this.pnlMaster.Controls.Add(this.brnClose);
            this.pnlMaster.Controls.Add(this.btnPrint);
            this.pnlMaster.Controls.Add(this.dtToDate);
            this.pnlMaster.Controls.Add(this.label4);
            this.pnlMaster.Controls.Add(this.dtfromDate);
            this.pnlMaster.Controls.Add(this.label3);
            this.pnlMaster.Controls.Add(this.txtUserName);
            this.pnlMaster.Controls.Add(this.label2);
            this.pnlMaster.Controls.Add(this.txtCounter);
            this.pnlMaster.Controls.Add(this.label1);
            this.pnlMaster.Location = new System.Drawing.Point(83, 28);
            this.pnlMaster.Name = "pnlMaster";
            this.pnlMaster.Size = new System.Drawing.Size(1040, 559);
            this.pnlMaster.TabIndex = 0;
            this.pnlMaster.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMaster_Paint);
            // 
            // brnClose
            // 
            this.brnClose.Image = global::SGMOSOL.ResourceMain.Close;
            this.brnClose.Location = new System.Drawing.Point(274, 192);
            this.brnClose.Name = "brnClose";
            this.brnClose.Size = new System.Drawing.Size(100, 44);
            this.brnClose.TabIndex = 9;
            this.brnClose.Text = "CLOSE";
            this.brnClose.UseVisualStyleBackColor = true;
            this.brnClose.Click += new System.EventHandler(this.brnClose_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Image = global::SGMOSOL.ResourceMain.Print;
            this.btnPrint.Location = new System.Drawing.Point(176, 192);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(92, 44);
            this.btnPrint.TabIndex = 8;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // dtToDate
            // 
            this.dtToDate.Location = new System.Drawing.Point(414, 101);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.Size = new System.Drawing.Size(200, 22);
            this.dtToDate.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(352, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "To Date";
            // 
            // dtfromDate
            // 
            this.dtfromDate.Location = new System.Drawing.Point(137, 101);
            this.dtfromDate.Name = "dtfromDate";
            this.dtfromDate.Size = new System.Drawing.Size(200, 22);
            this.dtfromDate.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "From Date";
            // 
            // txtUserName
            // 
            this.txtUserName.Enabled = false;
            this.txtUserName.Location = new System.Drawing.Point(451, 36);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(186, 22);
            this.txtUserName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(352, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "USERNAME";
            // 
            // txtCounter
            // 
            this.txtCounter.Enabled = false;
            this.txtCounter.Location = new System.Drawing.Point(137, 39);
            this.txtCounter.Name = "txtCounter";
            this.txtCounter.Size = new System.Drawing.Size(186, 22);
            this.txtCounter.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "COUNTER";
            // 
            // reportViewer1
            // 
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "SGMOSOL.Reports.DengiCalculation.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(112, 266);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(396, 246);
            this.reportViewer1.TabIndex = 10;
            this.reportViewer1.Visible = false;
            // 
            // frmTotalDengiReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1172, 633);
            this.Controls.Add(this.pnlMaster);
            this.Name = "frmTotalDengiReport";
            this.Text = "frmTotalDengiReport";
            this.Load += new System.EventHandler(this.frmTotalDengiReport_Load);
            this.pnlMaster.ResumeLayout(false);
            this.pnlMaster.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMaster;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCounter;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtToDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtfromDate;
        private System.Windows.Forms.Button brnClose;
        private System.Windows.Forms.Button btnPrint;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
    }
}