﻿namespace SGMOSOL.SCREENS.Bed_System
{
    partial class FrmBedO
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
            this.Label10 = new System.Windows.Forms.Label();
            this.dtpCheckInTime = new System.Windows.Forms.DateTimePicker();
            this.dtpCheckIn = new System.Windows.Forms.DateTimePicker();
            this.Label9 = new System.Windows.Forms.Label();
            this.txtCounter = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.fpsPrintReceipt = new System.Windows.Forms.DataGridView();
            this.ColLocation = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDengi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColOccupied = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPending = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColBedOutofOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.fpsPrintReceipt)).BeginInit();
            this.SuspendLayout();
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label10.ForeColor = System.Drawing.Color.Black;
            this.Label10.Location = new System.Drawing.Point(608, 18);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(41, 18);
            this.Label10.TabIndex = 132;
            this.Label10.Text = "Time";
            this.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpCheckInTime
            // 
            this.dtpCheckInTime.Checked = false;
            this.dtpCheckInTime.CustomFormat = "hh:mm tt";
            this.dtpCheckInTime.Enabled = false;
            this.dtpCheckInTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckInTime.Location = new System.Drawing.Point(655, 19);
            this.dtpCheckInTime.Name = "dtpCheckInTime";
            this.dtpCheckInTime.Size = new System.Drawing.Size(90, 20);
            this.dtpCheckInTime.TabIndex = 131;
            this.dtpCheckInTime.Value = new System.DateTime(2013, 8, 24, 0, 0, 0, 0);
            // 
            // dtpCheckIn
            // 
            this.dtpCheckIn.Checked = false;
            this.dtpCheckIn.CustomFormat = "dd/MM/yyyy";
            this.dtpCheckIn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckIn.Location = new System.Drawing.Point(484, 15);
            this.dtpCheckIn.Name = "dtpCheckIn";
            this.dtpCheckIn.Size = new System.Drawing.Size(103, 20);
            this.dtpCheckIn.TabIndex = 129;
            this.dtpCheckIn.Value = new System.DateTime(2013, 8, 24, 0, 0, 0, 0);
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label9.ForeColor = System.Drawing.Color.Black;
            this.Label9.Location = new System.Drawing.Point(431, 16);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(39, 18);
            this.Label9.TabIndex = 130;
            this.Label9.Text = "Date";
            this.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCounter
            // 
            this.txtCounter.BackColor = System.Drawing.Color.White;
            this.txtCounter.Enabled = false;
            this.txtCounter.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCounter.Location = new System.Drawing.Point(287, 13);
            this.txtCounter.Name = "txtCounter";
            this.txtCounter.ReadOnly = true;
            this.txtCounter.Size = new System.Drawing.Size(116, 26);
            this.txtCounter.TabIndex = 128;
            this.txtCounter.TabStop = false;
            // 
            // Label2
            // 
            this.Label2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(216, 15);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(74, 21);
            this.Label2.TabIndex = 127;
            this.Label2.Text = "Counter";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(15, 15);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(38, 18);
            this.Label1.TabIndex = 125;
            this.Label1.Text = "User";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtUser
            // 
            this.txtUser.BackColor = System.Drawing.Color.White;
            this.txtUser.Enabled = false;
            this.txtUser.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser.Location = new System.Drawing.Point(59, 12);
            this.txtUser.Name = "txtUser";
            this.txtUser.ReadOnly = true;
            this.txtUser.Size = new System.Drawing.Size(123, 26);
            this.txtUser.TabIndex = 126;
            this.txtUser.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::SGMOSOL.ResourceMain.Close;
            this.btnClose.Location = new System.Drawing.Point(633, 398);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 46);
            this.btnClose.TabIndex = 124;
            this.btnClose.Text = "&Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
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
            this.ColOccupied,
            this.ColPending,
            this.ColBedOutofOrder});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.fpsPrintReceipt.DefaultCellStyle = dataGridViewCellStyle2;
            this.fpsPrintReceipt.Location = new System.Drawing.Point(18, 44);
            this.fpsPrintReceipt.Name = "fpsPrintReceipt";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.fpsPrintReceipt.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.fpsPrintReceipt.Size = new System.Drawing.Size(744, 348);
            this.fpsPrintReceipt.TabIndex = 209;
            this.fpsPrintReceipt.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.fpsPrintReceipt_CellEnter);
            // 
            // ColLocation
            // 
            this.ColLocation.HeaderText = "Location";
            this.ColLocation.Name = "ColLocation";
            this.ColLocation.ReadOnly = true;
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
            this.ColDengi.ReadOnly = true;
            // 
            // ColQty
            // 
            this.ColQty.HeaderText = "Qty";
            this.ColQty.Name = "ColQty";
            this.ColQty.ReadOnly = true;
            // 
            // ColOccupied
            // 
            this.ColOccupied.HeaderText = "Occupied";
            this.ColOccupied.Name = "ColOccupied";
            this.ColOccupied.ReadOnly = true;
            // 
            // ColPending
            // 
            this.ColPending.HeaderText = "Pending";
            this.ColPending.Name = "ColPending";
            this.ColPending.ReadOnly = true;
            // 
            // ColBedOutofOrder
            // 
            this.ColBedOutofOrder.HeaderText = "BedOutofOrder";
            this.ColBedOutofOrder.Name = "ColBedOutofOrder";
            // 
            // FrmBedO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.fpsPrintReceipt);
            this.Controls.Add(this.Label10);
            this.Controls.Add(this.dtpCheckInTime);
            this.Controls.Add(this.dtpCheckIn);
            this.Controls.Add(this.Label9);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtCounter);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.txtUser);
            this.Name = "FrmBedO";
            this.Text = "FrmBedO";
            this.Load += new System.EventHandler(this.FrmBedO_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fpsPrintReceipt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.DateTimePicker dtpCheckInTime;
        internal System.Windows.Forms.DateTimePicker dtpCheckIn;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.TextBox txtCounter;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.DataGridView fpsPrintReceipt;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDengi;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColOccupied;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPending;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColBedOutofOrder;
    }
}