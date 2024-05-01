namespace SGMOSOL.SCREENS.BhaktNiwas
{
    partial class frmRoomOccupied
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
            this.cmbBhaktaNiwas = new System.Windows.Forms.ComboBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.gvOccRooms = new System.Windows.Forms.DataGridView();
            this.txtCtOfRooms = new System.Windows.Forms.TextBox();
            this.lbCtOfRooms = new System.Windows.Forms.Label();
            this.txtCounter = new System.Windows.Forms.TextBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.btnExport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gvOccRooms)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbBhaktaNiwas
            // 
            this.cmbBhaktaNiwas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBhaktaNiwas.Location = new System.Drawing.Point(303, 11);
            this.cmbBhaktaNiwas.Name = "cmbBhaktaNiwas";
            this.cmbBhaktaNiwas.Size = new System.Drawing.Size(130, 21);
            this.cmbBhaktaNiwas.Sorted = true;
            this.cmbBhaktaNiwas.TabIndex = 53;
            this.cmbBhaktaNiwas.SelectedIndexChanged += new System.EventHandler(this.cmbBhaktaNiwas_SelectedIndexChanged);
            // 
            // Label1
            // 
            this.Label1.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label1.Location = new System.Drawing.Point(216, 9);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(81, 21);
            this.Label1.TabIndex = 52;
            this.Label1.Text = "Sublocation";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gvOccRooms
            // 
            this.gvOccRooms.AllowUserToAddRows = false;
            this.gvOccRooms.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gvOccRooms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvOccRooms.Location = new System.Drawing.Point(15, 92);
            this.gvOccRooms.Name = "gvOccRooms";
            this.gvOccRooms.ReadOnly = true;
            this.gvOccRooms.Size = new System.Drawing.Size(678, 326);
            this.gvOccRooms.TabIndex = 51;
            // 
            // txtCtOfRooms
            // 
            this.txtCtOfRooms.BackColor = System.Drawing.Color.White;
            this.txtCtOfRooms.Enabled = false;
            this.txtCtOfRooms.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCtOfRooms.Location = new System.Drawing.Point(201, 53);
            this.txtCtOfRooms.Name = "txtCtOfRooms";
            this.txtCtOfRooms.ReadOnly = true;
            this.txtCtOfRooms.Size = new System.Drawing.Size(78, 22);
            this.txtCtOfRooms.TabIndex = 50;
            this.txtCtOfRooms.TabStop = false;
            // 
            // lbCtOfRooms
            // 
            this.lbCtOfRooms.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.lbCtOfRooms.Location = new System.Drawing.Point(12, 52);
            this.lbCtOfRooms.Name = "lbCtOfRooms";
            this.lbCtOfRooms.Size = new System.Drawing.Size(180, 21);
            this.lbCtOfRooms.TabIndex = 49;
            this.lbCtOfRooms.Text = "No. Of Occupied  Rooms";
            this.lbCtOfRooms.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCounter
            // 
            this.txtCounter.BackColor = System.Drawing.Color.White;
            this.txtCounter.Enabled = false;
            this.txtCounter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCounter.Location = new System.Drawing.Point(577, 9);
            this.txtCounter.Name = "txtCounter";
            this.txtCounter.ReadOnly = true;
            this.txtCounter.Size = new System.Drawing.Size(89, 22);
            this.txtCounter.TabIndex = 48;
            this.txtCounter.TabStop = false;
            // 
            // Label13
            // 
            this.Label13.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label13.Location = new System.Drawing.Point(464, 9);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(74, 21);
            this.Label13.TabIndex = 47;
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
            this.Label14.TabIndex = 45;
            this.Label14.Text = "User";
            this.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtUser
            // 
            this.txtUser.BackColor = System.Drawing.Color.White;
            this.txtUser.Enabled = false;
            this.txtUser.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser.Location = new System.Drawing.Point(89, 9);
            this.txtUser.Name = "txtUser";
            this.txtUser.ReadOnly = true;
            this.txtUser.Size = new System.Drawing.Size(88, 22);
            this.txtUser.TabIndex = 46;
            this.txtUser.TabStop = false;
            // 
            // btnExport
            // 
            this.btnExport.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExport.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Location = new System.Drawing.Point(556, 52);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(110, 34);
            this.btnExport.TabIndex = 174;
            this.btnExport.Text = "&Export Excel";
            this.btnExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // frmRoomOccupied
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.cmbBhaktaNiwas);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.gvOccRooms);
            this.Controls.Add(this.txtCtOfRooms);
            this.Controls.Add(this.lbCtOfRooms);
            this.Controls.Add(this.txtCounter);
            this.Controls.Add(this.Label13);
            this.Controls.Add(this.Label14);
            this.Controls.Add(this.txtUser);
            this.Name = "frmRoomOccupied";
            this.Text = "frmRoomOccupied";
            this.Load += new System.EventHandler(this.frmRoomOccupied_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvOccRooms)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ComboBox cmbBhaktaNiwas;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.DataGridView gvOccRooms;
        internal System.Windows.Forms.TextBox txtCtOfRooms;
        internal System.Windows.Forms.Label lbCtOfRooms;
        internal System.Windows.Forms.TextBox txtCounter;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.Label Label14;
        internal System.Windows.Forms.TextBox txtUser;
        internal System.Windows.Forms.Button btnExport;
    }
}