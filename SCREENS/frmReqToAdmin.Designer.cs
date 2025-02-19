﻿namespace SGMOSOL.SCREENS
{
    partial class frmReqToAdmin
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
            this.pnlMaster = new System.Windows.Forms.Panel();
            this.lblItemCode = new System.Windows.Forms.Label();
            this.lblQty = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cboItemCode = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dgvItemDetails = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Label5 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txtReqID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCounter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlMaster.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMaster
            // 
            this.pnlMaster.BackColor = System.Drawing.Color.Honeydew;
            this.pnlMaster.Controls.Add(this.lblItemCode);
            this.pnlMaster.Controls.Add(this.lblQty);
            this.pnlMaster.Controls.Add(this.btnClose);
            this.pnlMaster.Controls.Add(this.btnSave);
            this.pnlMaster.Controls.Add(this.btnNew);
            this.pnlMaster.Controls.Add(this.lblQuantity);
            this.pnlMaster.Controls.Add(this.txtItemName);
            this.pnlMaster.Controls.Add(this.txtQuantity);
            this.pnlMaster.Controls.Add(this.label11);
            this.pnlMaster.Controls.Add(this.label8);
            this.pnlMaster.Controls.Add(this.cboItemCode);
            this.pnlMaster.Controls.Add(this.label7);
            this.pnlMaster.Controls.Add(this.dgvItemDetails);
            this.pnlMaster.Controls.Add(this.panel2);
            this.pnlMaster.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlMaster.Location = new System.Drawing.Point(34, 24);
            this.pnlMaster.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlMaster.Name = "pnlMaster";
            this.pnlMaster.Size = new System.Drawing.Size(998, 448);
            this.pnlMaster.TabIndex = 0;
            this.pnlMaster.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMaster_Paint);
            // 
            // lblItemCode
            // 
            this.lblItemCode.AutoSize = true;
            this.lblItemCode.ForeColor = System.Drawing.Color.Red;
            this.lblItemCode.Location = new System.Drawing.Point(238, 89);
            this.lblItemCode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblItemCode.Name = "lblItemCode";
            this.lblItemCode.Size = new System.Drawing.Size(0, 18);
            this.lblItemCode.TabIndex = 52;
            // 
            // lblQty
            // 
            this.lblQty.AutoSize = true;
            this.lblQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQty.ForeColor = System.Drawing.Color.Red;
            this.lblQty.Location = new System.Drawing.Point(535, 124);
            this.lblQty.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(0, 13);
            this.lblQty.TabIndex = 51;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::SGMOSOL.ResourceMain.Close;
            this.btnClose.Location = new System.Drawing.Point(346, 304);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(68, 46);
            this.btnClose.TabIndex = 50;
            this.btnClose.Text = "&Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::SGMOSOL.ResourceMain.Save;
            this.btnSave.Location = new System.Drawing.Point(267, 304);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(73, 46);
            this.btnSave.TabIndex = 49;
            this.btnSave.Text = "Save\r\n";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnNew
            // 
            this.btnNew.CausesValidation = false;
            this.btnNew.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Image = global::SGMOSOL.ResourceMain._new;
            this.btnNew.Location = new System.Drawing.Point(186, 304);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 46);
            this.btnNew.TabIndex = 48;
            this.btnNew.Text = "&New";
            this.btnNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.ForeColor = System.Drawing.Color.Red;
            this.lblQuantity.Location = new System.Drawing.Point(466, 152);
            this.lblQuantity.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(0, 18);
            this.lblQuantity.TabIndex = 47;
            // 
            // txtItemName
            // 
            this.txtItemName.Enabled = false;
            this.txtItemName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItemName.Location = new System.Drawing.Point(447, 106);
            this.txtItemName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(192, 24);
            this.txtItemName.TabIndex = 46;
            this.txtItemName.TextChanged += new System.EventHandler(this.txtItemName_TextChanged);
            // 
            // txtQuantity
            // 
            this.txtQuantity.BackColor = System.Drawing.Color.MistyRose;
            this.txtQuantity.Location = new System.Drawing.Point(730, 103);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(114, 24);
            this.txtQuantity.TabIndex = 2;
            this.txtQuantity.TextChanged += new System.EventHandler(this.txtQuantity_TextChanged);
            this.txtQuantity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQuantity_KeyDown);
            this.txtQuantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuantity_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(655, 106);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 18);
            this.label11.TabIndex = 45;
            this.label11.Text = "Quantity";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(354, 106);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 18);
            this.label8.TabIndex = 44;
            this.label8.Text = "Item Name";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // cboItemCode
            // 
            this.cboItemCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboItemCode.FormattingEnabled = true;
            this.cboItemCode.Location = new System.Drawing.Point(204, 103);
            this.cboItemCode.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cboItemCode.Name = "cboItemCode";
            this.cboItemCode.Size = new System.Drawing.Size(136, 25);
            this.cboItemCode.TabIndex = 1;
            this.cboItemCode.SelectedIndexChanged += new System.EventHandler(this.cboItemCode_SelectedIndexChanged);
            this.cboItemCode.TextChanged += new System.EventHandler(this.cboItemCode_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(105, 106);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 18);
            this.label7.TabIndex = 43;
            this.label7.Text = "Item Code";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // dgvItemDetails
            // 
            this.dgvItemDetails.AllowUserToAddRows = false;
            this.dgvItemDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.PaleTurquoise;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItemDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvItemDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemDetails.Location = new System.Drawing.Point(108, 142);
            this.dgvItemDetails.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvItemDetails.Name = "dgvItemDetails";
            this.dgvItemDetails.ReadOnly = true;
            this.dgvItemDetails.RowHeadersWidth = 51;
            this.dgvItemDetails.RowTemplate.Height = 24;
            this.dgvItemDetails.Size = new System.Drawing.Size(781, 140);
            this.dgvItemDetails.TabIndex = 40;
            this.dgvItemDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemDetails_CellContentClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.Label5);
            this.panel2.Controls.Add(this.txtUser);
            this.panel2.Controls.Add(this.dtpDate);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtReqID);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtCounter);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(17, 14);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(978, 58);
            this.panel2.TabIndex = 39;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.ForeColor = System.Drawing.Color.MidnightBlue;
            this.Label5.Location = new System.Drawing.Point(535, 20);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(44, 18);
            this.Label5.TabIndex = 6;
            this.Label5.Text = "User";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtUser
            // 
            this.txtUser.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtUser.Enabled = false;
            this.txtUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser.Location = new System.Drawing.Point(580, 23);
            this.txtUser.Name = "txtUser";
            this.txtUser.ReadOnly = true;
            this.txtUser.Size = new System.Drawing.Size(181, 24);
            this.txtUser.TabIndex = 7;
            this.txtUser.TabStop = false;
            // 
            // dtpDate
            // 
            this.dtpDate.Enabled = false;
            this.dtpDate.Location = new System.Drawing.Point(818, 20);
            this.dtpDate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(145, 24);
            this.dtpDate.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(775, 21);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "Date";
            // 
            // txtReqID
            // 
            this.txtReqID.Enabled = false;
            this.txtReqID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReqID.Location = new System.Drawing.Point(415, 20);
            this.txtReqID.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtReqID.Name = "txtReqID";
            this.txtReqID.Size = new System.Drawing.Size(104, 24);
            this.txtReqID.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(289, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Requirement ID";
            // 
            // txtCounter
            // 
            this.txtCounter.Enabled = false;
            this.txtCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCounter.Location = new System.Drawing.Point(130, 21);
            this.txtCounter.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtCounter.Name = "txtCounter";
            this.txtCounter.Size = new System.Drawing.Size(130, 24);
            this.txtCounter.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Counter";
            // 
            // frmReqToAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 482);
            this.Controls.Add(this.pnlMaster);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmReqToAdmin";
            this.Text = "frmReqToAdmin";
            this.Load += new System.EventHandler(this.frmReqToAdmin_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmReqToAdmin_KeyDown);
            this.pnlMaster.ResumeLayout(false);
            this.pnlMaster.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMaster;
        internal System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.TextBox txtItemName;
        internal System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboItemCode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dgvItemDetails;
        private System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtReqID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCounter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.Label lblItemCode;
    }
}