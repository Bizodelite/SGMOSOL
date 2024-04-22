using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using Microsoft.VisualBasic;
using System.Xml;
using SGMOSOL.DAL.BhaktNiwas;
using SGMOSOL.ADMIN;
using static SGMOSOL.ADMIN.CommonFunctions;
using static SGMOSOL.BAL.BhaktNiwasBAL;

namespace SGMOSOL.SCREENS.BhaktNiwas
{
    public partial class frmRoomLocked : Form
    {
        private RoomLockedDAL objBTRoomLocked = new RoomLockedDAL();
        private RoomMasterDAL objDsRoomMst = new RoomMasterDAL();
        private CommonFunctions cf = new CommonFunctions();
        private long RoomCheckInDeptID;
        private bool Flag = false;
        private int gridi = 0;

        public frmRoomLocked()
        {
            InitializeComponent();
        }

        private void frmRoomLocked_Load(System.Object sender, System.EventArgs e)
        {
            // FillDgv()
            btnNew.Enabled = true;
            btnSave.Enabled = true;
            btnSearch.Enabled = true;
            DataGridView2.Visible = false;
            FillCounter();
        }

        private void FillDgv()
        {
            System.Data.DataSet ds;
            System.Data.DataSet dsComoBox;
            BindingSource Combobox;
            string strFromDate = "";
            //string strToDate;
            int i;
            //strFromDate = FormatDateToString(dtpCheckIn.Value);
            ds = objBTRoomLocked.GetData(dtpCheckIn.Value);
            DataGridView1.DataSource = ds.Tables[0];
            dsComoBox = objBTRoomLocked.GetConbobox();
            Combobox = new BindingSource();
            Combobox.DataSource = dsComoBox.Tables["Table"];
            DataGridViewComboBoxColumn col = new DataGridViewComboBoxColumn();
            if (gridi > 0)
            {
                DataGridView1.Columns["ROOM1"].DisplayIndex = 17;
                DataGridView1.Columns["ROOM2"].DisplayIndex = 18;
                return;
            }
            gridi = gridi + 1;
            col = (DataGridViewComboBoxColumn)DataGridView1.Columns["ROOM1"];
            col.DisplayIndex = 17;
            col.DataSource = Combobox;
            col.DataPropertyName = "RoomId";
            col.HeaderText = "Room1";
            col.DataSource = Combobox;
            col.DisplayMember = "RoomName";
            col.ValueMember = "RoomID";
            col = new DataGridViewComboBoxColumn();
            col = (DataGridViewComboBoxColumn)DataGridView1.Columns["ROOM2"];
            col.DisplayIndex = 18;
            col.DataSource = Combobox;
            col.DataPropertyName = "RoomId";
            col.HeaderText = "Room2";
            col.DataSource = Combobox;
            col.DisplayMember = "RoomName";
            col.ValueMember = "RoomID";
        }

        private void Button1_Click(System.Object sender, System.EventArgs e)
        {
            DataGridView2.Visible = false;
            DataGridView1.Visible = true;
            Flag = false;

            FillDgv();
        }

        private void ADD_Click(System.Object sender, System.EventArgs e)
        {
            RoomLocked RoomLocked;
            Int64 i = 0;
            int j = 0;
            int k = 1;
            int m = 0;
            int sCount = 0;


            if (Flag == false)
            {
                for (j = 0; j <= DataGridView1.Rows.Count - 1; j++)
                {
                    if (DataGridView1.Rows[j].Cells["Room1"].Value == Constants.vbNullString && DataGridView1.Rows[j].Cells["Room2"].Value == Constants.vbNullString)
                    {
                        DataGridView1.Rows[j].DefaultCellStyle.BackColor = Color.CadetBlue;
                        Interaction.MsgBox("Please assign Proper Room And then save");
                        return;
                    }
                }

                for (j = 0; j <= DataGridView1.Rows.Count - 1; j++)
                {
                    for (k = j + 1; k <= DataGridView1.Rows.Count - 1; k++)
                    {
                        if (DataGridView1.Rows[k].Cells["Room1"].Value == DataGridView1.Rows[j].Cells["Room1"].Value | DataGridView1.Rows[j].Cells["Room1"].Value == DataGridView1.Rows[j].Cells["Room2"].Value | DataGridView1.Rows[k].Cells["Room1"].Value == DataGridView1.Rows[k].Cells["Room2"].Value | DataGridView1.Rows[k].Cells["Room1"].Value == DataGridView1.Rows[j].Cells["Room2"].Value | DataGridView1.Rows[k].Cells["Room2"].Value == DataGridView1.Rows[j].Cells["Room1"].Value)
                        {
                            DataGridView1.Rows[j].DefaultCellStyle.BackColor = Color.CadetBlue;
                            DataGridView1.Rows[k].DefaultCellStyle.BackColor = Color.CadetBlue;
                            sCount = sCount + 1;
                        }
                    }
                }
                if (sCount > 0)
                {
                    Interaction.MsgBox("There are duplicates Room Please Check it.");
                    Flag = false;
                    return;
                }
                else
                    DataGridView1.RowsDefaultCellStyle.BackColor = Color.White;
            }
            else
            {
                for (j = 0; j <= DataGridView2.RowCount - 1; j++)
                {
                    for (k = j + 1; k <= DataGridView2.RowCount - 1; k++)
                    {
                        if (DataGridView2.Rows[j].Cells["Room1"].Value == DataGridView2.Rows[k].Cells["Room1"].Value)
                        {
                            DataGridView2.Rows[j].DefaultCellStyle.BackColor = Color.CadetBlue;
                            DataGridView2.Rows[k].DefaultCellStyle.BackColor = Color.CadetBlue;
                            sCount = sCount + 1;
                        }
                    }
                }
                if (sCount > 0)
                {
                    Interaction.MsgBox("There are duplicates Room Please Check it.");
                    Flag = true;
                    return;
                }
                else
                    DataGridView1.RowsDefaultCellStyle.BackColor = Color.White;
            }

            try
            {
                // Exit Sub
                if (Flag == false)
                {
                    foreach (DataGridViewRow Row in DataGridView1.Rows)
                    {
                        if (Convert.ToInt32(Row.Cells["NOofRoom"].Value) == 1)
                        {
                            RoomLocked = getRowData(Row);
                            i = objBTRoomLocked.Insert(RoomLocked, UserInfo.UserName, UserInfo.Machine_Name, System.DateTime.Now);
                        }
                        else
                        {
                            RoomLocked = getRowData(Row);
                            i = objBTRoomLocked.Insert(RoomLocked, UserInfo.UserName, UserInfo.Machine_Name, System.DateTime.Now);
                            RoomLocked = getRowDatafortwoRoom(Row);
                            i = objBTRoomLocked.Insert(RoomLocked, UserInfo.UserName, UserInfo.Machine_Name, System.DateTime.Now);
                        }
                    }
                }
                else
                {
                    foreach (DataGridViewRow row in DataGridView2.Rows)
                    {
                        RoomLocked = getRowData(row);
                        i = objBTRoomLocked.Update(RoomLocked, UserInfo.UserName, UserInfo.Machine_Name, System.DateTime.Now);
                    }
                    btnEdit_Click(null, null);
                }
                if (i < 0)
                    Interaction.MsgBox("Records not saved Properly.please give proper room ");
                else
                {
                    Interaction.MsgBox("Records  saved Properly");
                    btnEdit_Click(null, null);
                }
                Flag = false;
            }
            catch (Exception ex)
            {
            }
        }

        private RoomLocked getRowData(DataGridViewRow Row)
        {
            RoomLocked RoomLocked = new RoomLocked();
            RoomLocked.BookingID = Convert.ToInt64(Row.Cells["BookingID"].Value);
            RoomLocked.DEPT_ID = RoomCheckInDeptID;
            if (Flag == false)
                RoomLocked.LOCK_DATE = dtpCheckIn.Value;
            else
                RoomLocked.ROOM_LOCK_ID = Convert.ToInt64(Row.Cells["ROOM_LOCK_ID"].Value);

            RoomLocked.ROOM_ID = Convert.ToInt64(Row.Cells["Room1"].Value);
            if (Flag == false)
                RoomLocked.LOC_ID = Convert.ToInt64(Row.Cells["LocID"].Value);
            return RoomLocked;
        }
        private RoomLocked getRowDatafortwoRoom(DataGridViewRow Row)
        {
            RoomLocked RoomLocked = new RoomLocked();
            RoomLocked.BookingID = Convert.ToInt64(Row.Cells["BookingID"].Value);
            RoomLocked.DEPT_ID = RoomCheckInDeptID;
            RoomLocked.LOCK_DATE = dtpCheckIn.Value;
            RoomLocked.ROOM_ID = Convert.ToInt64(Row.Cells["Room2"].Value);
            RoomLocked.LOC_ID = Convert.ToInt64(Row.Cells["LocID"].Value);
            return RoomLocked;
        }
        private void FillCounter()
        {
            System.Data.DataTable dr;

            dr = cf.GetDrCounterMachId(UserInfo.UserId, SystemHDDModelNo, SystemHDDSerialNo, SystemMacID, Convert.ToInt16(eModType.BhaktaNiwas));
            if (dr.Rows.Count > 0)
                RoomCheckInDeptID = Convert.ToInt64(dr.Rows[0]["DeptId"]);
        }

        private void btnEdit_Click(System.Object sender, System.EventArgs e)
        {
            DataGridView1.Visible = false;
            DataGridView2.Visible = true;
            DataGridView2.Location = new Point(12, 93);
            System.Data.DataSet ds;
            System.Data.DataSet dsComoBox;
            BindingSource Combobox;
            string strFromDate;
            string strToDate;
            DateTime st1;
            int i;
            Flag = true;
            strFromDate = FormatDateToString(dtpCheckIn.Value);
            ds = objBTRoomLocked.GetDataforEdit(strFromDate);
            DataGridView2.DataSource = ds.Tables[0];
            DataGridView2.Columns["ROOM_LOCK_ID"].Visible = false;
            DataGridView2.Columns["SERIAL_NO"].Visible = false;
            DataGridView2.Columns["BookingID"].Visible = false;

            dsComoBox = objBTRoomLocked.GetConbobox();
            if (DataGridView2.Columns.Contains("Room1"))
            {
                foreach (DataGridViewRow Row in DataGridView2.Rows)
                    Row.Cells["Room1"].Value = Row.Cells["Room_ID"].Value;
                return;
            }
            // For ComboBox
            Combobox = new BindingSource();
            Combobox.DataSource = null;
            Combobox.DataSource = dsComoBox.Tables["Table"];
            DataGridViewComboBoxColumn col = new DataGridViewComboBoxColumn();
            col.DataPropertyName = "RoomId";
            col.HeaderText = "Room1";
            col.DataSource = Combobox;
            col.DisplayMember = "RoomName";
            col.ValueMember = "RoomID";
            col.Name = "Room1";
            DataGridView2.Columns.Add(col);
            foreach (DataGridViewRow Row in DataGridView2.Rows)
                Row.Cells["Room1"].Value = Row.Cells["Room_ID"].Value;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
