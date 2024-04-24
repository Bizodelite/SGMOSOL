using Microsoft.Office.Interop.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SGMOSOL.ADMIN.CommonFunctions;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using Microsoft.VisualBasic;
using SGMOSOL.ADMIN;
using SGMOSOL.DAL.BhaktNiwas;

namespace SGMOSOL.SCREENS.BhaktNiwas
{
    public partial class frmRoomList : Form
    {
        private eScreenID mScreenID;
        private bool mBlnEdit = false;
        private eAction mAction;        // Reference to Enum of Type Action for ActionView,ActionNew,ActionUpdate and ActionDelete
        private ArrayList CtrlArr = new ArrayList(); // To insert Form Control in Array List
        private ArrayList btnArr = new ArrayList();
        private CommonFunctions cf = new CommonFunctions();
        private int PrintReceiptLocId;
        private RoomMasterDAL objDsRoomMst = new RoomMasterDAL();

        public frmRoomList(eScreenID ScreenID)
        {
            InitializeComponent();
            mScreenID = ScreenID;
        }

        private void frmRoomList_Load(object sender, System.EventArgs e)
        {
            // Me.WindowState = FormWindowState.Maximized
            cf.setControlsonForm(this, CtrlArr, btnArr);
            cf.SetUserScreenActions(this, UserInfo.UserId, (int)eScreenID.RoomCheckIn, btnArr, null, mBlnEdit);
            ScreenToCenter();

            txtUser.Text = UserInfo.UserName;
            FillCounter();
            FillSublocation();

            FillRooms();
            FillLabels();
        }
        private void FillSublocation()
        {
            System.Data.DataTable dr;
            dr = cf.GetDrSublocation(7, PrintReceiptLocId);
            cf.FillCombo(cmbBhaktaNiwas, dr, "Department", "Department_Id");
            if (cmbBhaktaNiwas.Items.Count > 0)
            {
                cmbBhaktaNiwas.SelectedIndex = -1;
                cmbBhaktaNiwas.SelectedText = "All";
            }
        }
        private void ScreenToCenter()
        {
            //Int32 ctr;
            //ctr = (MDI.Size.Width - this.Size.Width) / (double)2;
            //this.Location = new Point(ctr, 0);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }
        private void FillCounter()
        {
            System.Data.DataTable dr;
            dr = cf.GetDrCounterMachId(UserInfo.UserId, SystemHDDModelNo, SystemHDDSerialNo, SystemMacID, Convert.ToInt16(eModType.BhaktaNiwas));
            if (dr.Rows.Count > 0)
            {
                txtCounter.Text = dr.Rows[0]["CounterMachineTitle"].ToString();
                txtCounter.Tag = dr.Rows[0]["CtrMachId"];
                PrintReceiptLocId = Convert.ToInt32(dr.Rows[0]["LocId"]);
            }
        }
        private void ClearRecord()
        {
            int row_start = 0;
            int row_end = fpsLockers.RowCount;
            int colg = 0;
            {
                var withBlock = fpsLockers;
                for (int i = 0; i <= row_end - 1; i++)
                {
                    for (colg = 0; colg <= 6; colg++)
                        withBlock.Rows[i].Cells[colg].Value = "";
                }
            }
        }
        private void FillRooms()
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            Int32 ctr;
            Int32 row = 0;
            Int32 col = 0;
            DataView Dv;
            string Filtercriteria = "";
            Int64 Deptid;

            try
            {
                Deptid = cf.cmbItemdata(cmbBhaktaNiwas, cmbBhaktaNiwas.SelectedIndex);
                if (mScreenID == eScreenID.RoomAvailable)
                    ds = objDsRoomMst.GetDsRoomDetails(Deptid, (int)eTokenDetail.StatusActive, (int)eTokenDetail.StatusYes, PrintReceiptLocId);
                else
                    ds = objDsRoomMst.GetDsRoomDetails(Deptid, (int)eTokenDetail.StatusActive, (int)eTokenDetail.StatusNo, PrintReceiptLocId);
                ctr = ds.Tables[0].Rows.Count;

                if (ctr == 0)
                    return;
                ctr = Convert.ToInt32(Math.Ceiling(ds.Tables[0].Rows.Count / (double)7));
                Dv = new DataView(ds.Tables[0], Filtercriteria, "", DataViewRowState.CurrentRows);
                // fpsLockers.Sheets(0).RowCount = ctr
                int cnt2 = 0;
                string newCat1 = "";
                string oldca1 = "";
                foreach (DataRowView Drv in Dv)
                {
                    newCat1 = Drv["CAT_NAME"].ToString();
                    if (oldca1 != newCat1)
                    {
                        col = 0;
                        cnt2 = cnt2 + 1;

                        cnt2 = cnt2 + 1;
                        oldca1 = Drv["CAT_NAME"].ToString();
                    }

                    col = col + 1;
                    if (col >= 7)
                    {
                        col = 0;
                        cnt2 = cnt2 + 1;
                    }
                }
                fpsLockers.RowCount = ctr + cnt2;
                {
                    var withBlock = fpsLockers;
                    // row = 0
                    // For Each Drv In Dv
                    // .Cells(row, col).Text = Drv("RoomName")
                    // row = row + 1
                    // If row >= ctr Then
                    // row = 0
                    // col = col + 1
                    // End If
                    // Next
                    row = 0;
                    string newCat = "";
                    string oldca = "";

                    foreach (DataRowView Drv in Dv)
                    {
                        newCat = Drv["CAT_NAME"].ToString();
                        if (oldca != newCat)
                        {
                            col = 0;
                            row = row + 1;
                            withBlock.Rows[row].Cells[0].Value = Drv["CAT_NAME"];
                            withBlock.Rows[row].Cells[0].Style.BackColor = Color.AliceBlue;

                            row = row + 1;
                            oldca = Drv["CAT_NAME"].ToString();
                        }
                        withBlock.Rows[row].Cells[col].Value = Drv["RoomName"];
                        col = col + 1;
                        if (col >= 7)
                        {
                            col = 0;
                            row = row + 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void FillLabels()
        {
            System.Data.DataTable dr;
            Int64 Deptid;
            try
            {
                Deptid = cf.cmbItemdata(cmbBhaktaNiwas, cmbBhaktaNiwas.SelectedIndex);
                dr = objDsRoomMst.GetDrRoomLDetails(Deptid, 0, 0, PrintReceiptLocId);
                if (dr.Rows.Count > 0)
                {
                    lbl_bhakta.Text = dr.Rows[0]["TOTAL_BHAKT"].ToString();
                    lbl_donner.Text = dr.Rows[0]["TOTAL_DONNER"].ToString();
                    lbl_empty.Text = dr.Rows[0]["TOTAL_AVL"].ToString();
                    lbl_guest1.Text = dr.Rows[0]["TOTAL_GUEST"].ToString();
                    lbl_guest2.Text = dr.Rows[0]["TOTAL_GUEST2"].ToString();
                    lbl_dam.Text = dr.Rows[0]["TOTAL_DAM"].ToString();
                    lbl_lbl.Text = dr.Rows[0]["TOTAL_GUEST1"].ToString();
                    lbl_occ.Text = dr.Rows[0]["TOTAL_OCCU"].ToString();
                    lbl_total.Text = dr.Rows[0]["TOTAL"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void cmbBhaktaNiwas_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            ClearRecord();
            FillRooms();
            FillLabels();
        }
    }

}
