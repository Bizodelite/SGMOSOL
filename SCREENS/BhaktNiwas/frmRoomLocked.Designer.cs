namespace SGMOSOL.SCREENS.BhaktNiwas
{
    partial class frmRoomLocked
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.DataGridView2 = new System.Windows.Forms.DataGridView();
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
            this.BookingID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CATID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BOOKING_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ROOM_BOOKING_DET_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RoomCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Adult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Child = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOofRoom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoofDays = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoofPerson = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Charges = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REG_IMAGE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NO_OF_BED = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOCNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BOOK_STATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ROOM1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ROOM2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Label9 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.dtpCheckIn = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnNew
            // 
            this.btnNew.Enabled = false;
            this.btnNew.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Image = global::SGMOSOL.ResourceMain.Edit;
            this.btnNew.Location = new System.Drawing.Point(542, 15);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(100, 36);
            this.btnNew.TabIndex = 31;
            this.btnNew.Text = "&Edit";
            this.btnNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNew.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Enabled = false;
            this.btnSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Image = global::SGMOSOL.ResourceMain.Search;
            this.btnSearch.Location = new System.Drawing.Point(411, 15);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 36);
            this.btnSearch.TabIndex = 30;
            this.btnSearch.Text = "&Search";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.Click += new System.EventHandler(this.Button1_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::SGMOSOL.ResourceMain.Save;
            this.btnSave.Location = new System.Drawing.Point(475, 465);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 44);
            this.btnSave.TabIndex = 29;
            this.btnSave.Text = "&Save\r\n ";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnSave.Click += new System.EventHandler(this.ADD_Click);
            // 
            // DataGridView2
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView2.Location = new System.Drawing.Point(244, 251);
            this.DataGridView2.Name = "DataGridView2";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 9F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView2.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.DataGridView2.Size = new System.Drawing.Size(562, 150);
            this.DataGridView2.TabIndex = 28;
            // 
            // DataGridView1
            // 
            this.DataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 9F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BookingID,
            this.CATID,
            this.LocID,
            this.BOOKING_NO,
            this.ROOM_BOOKING_DET_ID,
            this.RoomCategory,
            this.Adult,
            this.Child,
            this.NOofRoom,
            this.NoofDays,
            this.NoofPerson,
            this.Charges,
            this.REG_IMAGE,
            this.NO_OF_BED,
            this.LOCNAME,
            this.BOOK_STATUS,
            this.ROOM1,
            this.ROOM2});
            this.DataGridView1.Location = new System.Drawing.Point(13, 78);
            this.DataGridView1.Name = "DataGridView1";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Tahoma", 9F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridView1.Size = new System.Drawing.Size(1005, 269);
            this.DataGridView1.TabIndex = 27;
            // 
            // BookingID
            // 
            this.BookingID.DataPropertyName = "ID";
            this.BookingID.HeaderText = "Booking ID";
            this.BookingID.Name = "BookingID";
            this.BookingID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.BookingID.Visible = false;
            this.BookingID.Width = 50;
            // 
            // CATID
            // 
            this.CATID.DataPropertyName = "CatID";
            this.CATID.HeaderText = "CATID";
            this.CATID.Name = "CATID";
            this.CATID.Visible = false;
            // 
            // LocID
            // 
            this.LocID.DataPropertyName = "LocID";
            this.LocID.HeaderText = "LocID";
            this.LocID.Name = "LocID";
            this.LocID.Visible = false;
            // 
            // BOOKING_NO
            // 
            this.BOOKING_NO.DataPropertyName = "BOOKING_NO";
            this.BOOKING_NO.HeaderText = "Booking_No";
            this.BOOKING_NO.Name = "BOOKING_NO";
            // 
            // ROOM_BOOKING_DET_ID
            // 
            this.ROOM_BOOKING_DET_ID.DataPropertyName = "ROOM_BOOKING_DET_ID";
            this.ROOM_BOOKING_DET_ID.HeaderText = "RoomBookingDetailsID";
            this.ROOM_BOOKING_DET_ID.Name = "ROOM_BOOKING_DET_ID";
            this.ROOM_BOOKING_DET_ID.Visible = false;
            // 
            // RoomCategory
            // 
            this.RoomCategory.DataPropertyName = "ROOM_CAT";
            this.RoomCategory.HeaderText = "Room Category";
            this.RoomCategory.Name = "RoomCategory";
            this.RoomCategory.Width = 70;
            // 
            // Adult
            // 
            this.Adult.DataPropertyName = "ADULT";
            this.Adult.HeaderText = "Adult";
            this.Adult.Name = "Adult";
            this.Adult.Width = 60;
            // 
            // Child
            // 
            this.Child.DataPropertyName = "CHILD";
            this.Child.HeaderText = "Chile";
            this.Child.Name = "Child";
            this.Child.Width = 60;
            // 
            // NOofRoom
            // 
            this.NOofRoom.DataPropertyName = "NO_OF_ROOM";
            this.NOofRoom.HeaderText = "No of Room";
            this.NOofRoom.Name = "NOofRoom";
            // 
            // NoofDays
            // 
            this.NoofDays.DataPropertyName = "NO_OF_DAYS";
            this.NoofDays.HeaderText = "No of Days";
            this.NoofDays.Name = "NoofDays";
            // 
            // NoofPerson
            // 
            this.NoofPerson.DataPropertyName = "NO_OF_PERSON";
            this.NoofPerson.HeaderText = "No of Person";
            this.NoofPerson.Name = "NoofPerson";
            // 
            // Charges
            // 
            this.Charges.DataPropertyName = "Charges";
            this.Charges.HeaderText = "Charges";
            this.Charges.Name = "Charges";
            // 
            // REG_IMAGE
            // 
            this.REG_IMAGE.DataPropertyName = "REG_IMAGE";
            this.REG_IMAGE.HeaderText = "REG_IMAGE";
            this.REG_IMAGE.Name = "REG_IMAGE";
            this.REG_IMAGE.Visible = false;
            // 
            // NO_OF_BED
            // 
            this.NO_OF_BED.DataPropertyName = "NO_OF_BED";
            this.NO_OF_BED.HeaderText = "No of Bed";
            this.NO_OF_BED.Name = "NO_OF_BED";
            this.NO_OF_BED.Visible = false;
            // 
            // LOCNAME
            // 
            this.LOCNAME.DataPropertyName = "LOC_NAME";
            this.LOCNAME.HeaderText = "LOC NAME";
            this.LOCNAME.Name = "LOCNAME";
            // 
            // BOOK_STATUS
            // 
            this.BOOK_STATUS.DataPropertyName = "BOOK_STATUS";
            this.BOOK_STATUS.HeaderText = "Book Status";
            this.BOOK_STATUS.Name = "BOOK_STATUS";
            this.BOOK_STATUS.Visible = false;
            // 
            // ROOM1
            // 
            this.ROOM1.HeaderText = "ROOM1";
            this.ROOM1.Name = "ROOM1";
            this.ROOM1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ROOM2
            // 
            this.ROOM2.HeaderText = "ROOM2";
            this.ROOM2.Name = "ROOM2";
            this.ROOM2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ROOM2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Label9.ForeColor = System.Drawing.Color.Black;
            this.Label9.Location = new System.Drawing.Point(66, 23);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(49, 18);
            this.Label9.TabIndex = 25;
            this.Label9.Text = "Date :";
            this.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::SGMOSOL.ResourceMain.Close;
            this.btnClose.Location = new System.Drawing.Point(581, 465);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 44);
            this.btnClose.TabIndex = 32;
            this.btnClose.Text = "&Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dtpCheckIn
            // 
            this.dtpCheckIn.CustomFormat = "dd/MM/yyyy";
            this.dtpCheckIn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckIn.Location = new System.Drawing.Point(121, 23);
            this.dtpCheckIn.Name = "dtpCheckIn";
            this.dtpCheckIn.Size = new System.Drawing.Size(115, 20);
            this.dtpCheckIn.TabIndex = 105;
            // 
            // frmRoomLocked
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1033, 519);
            this.Controls.Add(this.dtpCheckIn);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.DataGridView2);
            this.Controls.Add(this.DataGridView1);
            this.Controls.Add(this.Label9);
            this.Name = "frmRoomLocked";
            this.Text = "frmRoomLocked";
            this.Load += new System.EventHandler(this.frmRoomLocked_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button btnNew;
        internal System.Windows.Forms.Button btnSearch;
        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.DataGridView DataGridView2;
        internal System.Windows.Forms.DataGridView DataGridView1;
        internal System.Windows.Forms.DataGridViewTextBoxColumn BookingID;
        internal System.Windows.Forms.DataGridViewTextBoxColumn CATID;
        internal System.Windows.Forms.DataGridViewTextBoxColumn LocID;
        internal System.Windows.Forms.DataGridViewTextBoxColumn BOOKING_NO;
        internal System.Windows.Forms.DataGridViewTextBoxColumn ROOM_BOOKING_DET_ID;
        internal System.Windows.Forms.DataGridViewTextBoxColumn RoomCategory;
        internal System.Windows.Forms.DataGridViewTextBoxColumn Adult;
        internal System.Windows.Forms.DataGridViewTextBoxColumn Child;
        internal System.Windows.Forms.DataGridViewTextBoxColumn NOofRoom;
        internal System.Windows.Forms.DataGridViewTextBoxColumn NoofDays;
        internal System.Windows.Forms.DataGridViewTextBoxColumn NoofPerson;
        internal System.Windows.Forms.DataGridViewTextBoxColumn Charges;
        internal System.Windows.Forms.DataGridViewTextBoxColumn REG_IMAGE;
        internal System.Windows.Forms.DataGridViewTextBoxColumn NO_OF_BED;
        internal System.Windows.Forms.DataGridViewTextBoxColumn LOCNAME;
        internal System.Windows.Forms.DataGridViewTextBoxColumn BOOK_STATUS;
        internal System.Windows.Forms.DataGridViewComboBoxColumn ROOM1;
        internal System.Windows.Forms.DataGridViewComboBoxColumn ROOM2;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DateTimePicker dtpCheckIn;
    }
}