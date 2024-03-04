namespace SGMOSOL.SCREENS
{
    partial class frmSearchDengi
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboDengiType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblmobile = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtLastRecNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFirstRecNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtToDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtFromDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvDengiReceipt = new System.Windows.Forms.DataGridView();
            this.txtMobile = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDengiReceipt)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cboDengiType);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.lblmobile);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.txtLastRecNo);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtFirstRecNo);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.dtToDate);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.dtFromDate);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(9, 17);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(995, 106);
            this.panel1.TabIndex = 0;
            // 
            // cboDengiType
            // 
            this.cboDengiType.FormattingEnabled = true;
            this.cboDengiType.Location = new System.Drawing.Point(544, 54);
            this.cboDengiType.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cboDengiType.Name = "cboDengiType";
            this.cboDengiType.Size = new System.Drawing.Size(92, 21);
            this.cboDengiType.TabIndex = 14;
            this.cboDengiType.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(472, 57);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Dengi Type";
            this.label7.Visible = false;
            // 
            // lblmobile
            // 
            this.lblmobile.AutoSize = true;
            this.lblmobile.ForeColor = System.Drawing.Color.Red;
            this.lblmobile.Location = new System.Drawing.Point(726, 41);
            this.lblmobile.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblmobile.Name = "lblmobile";
            this.lblmobile.Size = new System.Drawing.Size(0, 13);
            this.lblmobile.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(688, 22);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Mobile";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(400, 57);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(56, 19);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtLastRecNo
            // 
            this.txtLastRecNo.Location = new System.Drawing.Point(286, 57);
            this.txtLastRecNo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtLastRecNo.Name = "txtLastRecNo";
            this.txtLastRecNo.Size = new System.Drawing.Size(90, 20);
            this.txtLastRecNo.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(205, 62);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Last Receipt No";
            // 
            // txtFirstRecNo
            // 
            this.txtFirstRecNo.Location = new System.Drawing.Point(102, 57);
            this.txtFirstRecNo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtFirstRecNo.Name = "txtFirstRecNo";
            this.txtFirstRecNo.Size = new System.Drawing.Size(90, 20);
            this.txtFirstRecNo.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 59);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "First Serial no.";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(485, 20);
            this.txtName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(199, 20);
            this.txtName.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(448, 22);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Name";
            // 
            // dtToDate
            // 
            this.dtToDate.Location = new System.Drawing.Point(286, 18);
            this.dtToDate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.Size = new System.Drawing.Size(151, 20);
            this.dtToDate.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(240, 23);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "To Date";
            // 
            // dtFromDate
            // 
            this.dtFromDate.Location = new System.Drawing.Point(78, 19);
            this.dtFromDate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.Size = new System.Drawing.Size(151, 20);
            this.dtFromDate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "From Date";
            // 
            // dgvDengiReceipt
            // 
            this.dgvDengiReceipt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDengiReceipt.Location = new System.Drawing.Point(16, 147);
            this.dgvDengiReceipt.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvDengiReceipt.Name = "dgvDengiReceipt";
            this.dgvDengiReceipt.ReadOnly = true;
            this.dgvDengiReceipt.RowHeadersWidth = 51;
            this.dgvDengiReceipt.RowTemplate.Height = 24;
            this.dgvDengiReceipt.Size = new System.Drawing.Size(988, 323);
            this.dgvDengiReceipt.TabIndex = 1;
            this.dgvDengiReceipt.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvDengiReceipt_CellPainting);
            this.dgvDengiReceipt.SelectionChanged += new System.EventHandler(this.dgvDengiReceipt_SelectionChanged);
            // 
            // txtMobile
            // 
            this.txtMobile.Location = new System.Drawing.Point(737, 37);
            this.txtMobile.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.Size = new System.Drawing.Size(148, 20);
            this.txtMobile.TabIndex = 12;
            this.txtMobile.TextChanged += new System.EventHandler(this.txtMobile_TextChanged);
            // 
            // frmSearchDengi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1013, 479);
            this.Controls.Add(this.txtMobile);
            this.Controls.Add(this.dgvDengiReceipt);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmSearchDengi";
            this.Text = "frmSearchDengi";
            this.Load += new System.EventHandler(this.frmSearchDengi_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDengiReceipt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtToDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtFromDate;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtLastRecNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFirstRecNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvDengiReceipt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMobile;
        private System.Windows.Forms.Label lblmobile;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboDengiType;
    }
}