namespace SGMOSOL.SCREENS
{
    partial class frmBhojnalaySearch
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
            this.dgvbhojnalay = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvbhojnalay)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
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
            this.panel1.Location = new System.Drawing.Point(28, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(931, 131);
            this.panel1.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(538, 65);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtLastRecNo
            // 
            this.txtLastRecNo.Location = new System.Drawing.Point(382, 70);
            this.txtLastRecNo.Name = "txtLastRecNo";
            this.txtLastRecNo.Size = new System.Drawing.Size(118, 22);
            this.txtLastRecNo.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(273, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "Last Receipt No";
            // 
            // txtFirstRecNo
            // 
            this.txtFirstRecNo.Location = new System.Drawing.Point(136, 70);
            this.txtFirstRecNo.Name = "txtFirstRecNo";
            this.txtFirstRecNo.Size = new System.Drawing.Size(118, 22);
            this.txtFirstRecNo.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "First Receipt No";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(647, 25);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(264, 22);
            this.txtName.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(597, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Name";
            // 
            // dtToDate
            // 
            this.dtToDate.Location = new System.Drawing.Point(382, 22);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.Size = new System.Drawing.Size(200, 22);
            this.dtToDate.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(320, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "To Date";
            // 
            // dtFromDate
            // 
            this.dtFromDate.Location = new System.Drawing.Point(104, 23);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.Size = new System.Drawing.Size(200, 22);
            this.dtFromDate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "From Date";
            // 
            // dgvbhojnalay
            // 
            this.dgvbhojnalay.AllowUserToAddRows = false;
            this.dgvbhojnalay.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgvbhojnalay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvbhojnalay.Location = new System.Drawing.Point(28, 179);
            this.dgvbhojnalay.Name = "dgvbhojnalay";
            this.dgvbhojnalay.ReadOnly = true;
            this.dgvbhojnalay.RowHeadersWidth = 51;
            this.dgvbhojnalay.RowTemplate.Height = 24;
            this.dgvbhojnalay.Size = new System.Drawing.Size(942, 330);
            this.dgvbhojnalay.TabIndex = 2;
            this.dgvbhojnalay.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvbhojnalay_CellPainting);
            this.dgvbhojnalay.SelectionChanged += new System.EventHandler(this.dgvbhojnalay_SelectionChanged);
            // 
            // frmBhojnalaySearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 538);
            this.Controls.Add(this.dgvbhojnalay);
            this.Controls.Add(this.panel1);
            this.Name = "frmBhojnalaySearch";
            this.Text = "frmBhojnalaySearch";
            this.Load += new System.EventHandler(this.frmBhojnalaySearch_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvbhojnalay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtLastRecNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFirstRecNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtToDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtFromDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvbhojnalay;
    }
}