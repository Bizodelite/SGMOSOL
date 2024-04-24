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
using static SGMOSOL.BAL.BhaktNiwasBAL;
using SGMOSOL.DAL.BhaktNiwas;

namespace SGMOSOL.SCREENS.BhaktNiwas
{
    public partial class frmRoomDamaged : Form
    {
        private long mScreenID;
        private bool mBlnEdit = false;
        private eAction mAction;        // Reference to Enum of Type Action for ActionView,ActionNew,ActionUpdate and ActionDelete
        private ArrayList CtrlArr = new ArrayList(); // To insert Form Control in Array List
        private ArrayList btnArr = new ArrayList();
        //private OSOL_ADMIN.clsDsCommon mClsDsCom = new OSOL_ADMIN.clsDsCommon();
        private RoomMasterDAL objDsRoomMst = new RoomMasterDAL();
        private Int16 flag;
        private Int64 SubLocID;
        private int PrintReceiptDeptID;
        private string PrintReceiptDeptName;
        private string PrintReceiptLocName;
        private Int64 PrintReceiptLocId;

        CommonFunctions cf = new CommonFunctions();
        public frmRoomDamaged()
        {
            InitializeComponent();
        }

        private void frmRoomDamaged_Load(object sender, System.EventArgs e)
        {
            // Me.WindowState = FormWindowState.Maximized
            dtpDate.Value = DateTime.Now;
            cf.setControlsonForm(this, CtrlArr, btnArr);
            cf.SetUserScreenActions(this, UserInfo.UserId, (int)eScreenID.RoomCheckIn, btnArr, null, mBlnEdit);
            ScreenToCenter();
            FillCounter();
            FillSublocations();
            txtUser.Text = UserInfo.UserName;

            rbDamaged.PerformClick();
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
                PrintReceiptDeptID = Convert.ToInt32(dr.Rows[0]["DeptId"]);
                PrintReceiptDeptName = dr.Rows[0]["DepartmentName"].ToString();
                PrintReceiptLocName = dr.Rows[0]["LocName"].ToString();
                PrintReceiptLocId = Convert.ToInt64(dr.Rows[0]["LocId"]);

                txtCounter.Tag = dr.Rows[0]["CtrMachId"];
                txtDept.Tag = dr.Rows[0]["DeptId"];
            }
        }



        private void btnSave_Click(System.Object sender, System.EventArgs e)
        {
            frmRoomDamaged objfrmDmRoomList = new frmRoomDamaged();

            DamagedRooms objDamagedLkrs;
            objDamagedLkrs = GetData();
            try
            {
                clsConnection.glbTransaction = clsConnection.glbCon.BeginTransaction();
                long lngErrNo = 0;

                for (var i = 0; i <= RoomListBox.Items.Count - 1; i++)
                {
                    if (RoomListBox.GetItemChecked(i))
                    {
                        if (flag == 0)
                        {
                            lngErrNo = objDsRoomMst.updateRoomMst(cf.lsbItemData(RoomListBox, i), (int)eTokenDetail.StatusInActive, (int)eTokenDetail.Damaged);
                            if (lngErrNo < 0 & lngErrNo != -1)
                            {
                                clsConnection.glbTransaction.Rollback();
                                return;
                            }
                            lngErrNo = objDsRoomMst.Insert(objDamagedLkrs, cf.lsbItemData(RoomListBox, i));
                            if (lngErrNo < 0 & lngErrNo != -1)
                            {
                                clsConnection.glbTransaction.Rollback();
                                return;
                            }
                        }
                        else if (flag == 1)
                        {
                            lngErrNo = objDsRoomMst.updateRoomMst(cf.lsbItemData(RoomListBox, i), (int)eTokenDetail.StatusActive, (int)eTokenDetail.StatusYes);
                            if (lngErrNo < 0 & lngErrNo != -1)
                            {
                                clsConnection.glbTransaction.Rollback();
                                return;
                            }
                            lngErrNo = objDsRoomMst.Delete(objDamagedLkrs, cf.lsbItemData(RoomListBox, i));
                            if (lngErrNo < 0 & lngErrNo != -1)
                            {
                                clsConnection.glbTransaction.Rollback();
                                return;
                            }
                        }
                    }
                }
                if (lngErrNo < 0 & lngErrNo != -1)
                {
                    clsConnection.glbTransaction.Rollback();
                    return;
                }
                else
                {
                    clsConnection.glbTransaction.Commit();
                    MessageBox.Show("Room Updated Successfully!", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                RoomListBox.Refresh();
            }
        }

        private void btnClose_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

       
        private void FillSublocations()
        {
            System.Data.DataTable dr;
            try
            {
                dr = objDsRoomMst.GetDrSublocations(PrintReceiptLocId, (Int64)eModType.BhaktaNiwas);
                cf.FillCombo(cboSublocation, dr, "Name", "DeptId");
            }
            catch (Exception ex)
            {
            }
        }
        private void FillAvailableRooms()
        {
            System.Data.DataTable dr;
            try
            {
                dr = objDsRoomMst.GetDrRoomDetails(Convert.ToInt32(txtDept.Tag), (int)eTokenDetail.StatusActive, (int)eTokenDetail.StatusYes, SubLocID, PrintReceiptLocId);

                cf.FillListBox(RoomListBox, dr, "RoomName", "RoomId", "RecordModifiedCount");
            }
            catch (Exception ex)
            {
            }
        }

        private void FillDamagedRooms()
        {
            System.Data.DataTable dr;
            try
            {
                dr = objDsRoomMst.GetDrRoomDetails(Convert.ToInt32(txtDept.Tag), (int)eTokenDetail.StatusInActive, (int)eTokenDetail.Damaged, SubLocID, PrintReceiptLocId);

                cf.FillListBox(RoomListBox, dr, "RoomName", "RoomId", "RecordModifiedCount");
            }
            catch (Exception ex)
            {
            }
        }

        private DamagedRooms GetData()
        {
            DamagedRooms objDmLkrs = new DamagedRooms();
            objDmLkrs.Reason = txtReason.Text;
            objDmLkrs.sDate = dtpDate.Value;
            objDmLkrs.EnteredBy = UserInfo.UserName;
            return objDmLkrs;
        }

        private void btnRefresh_Click(System.Object sender, System.EventArgs e)
        {
            if (flag == 0)
                FillAvailableRooms();
            else
                FillDamagedRooms();
            txtReason.Text = "";
        }



        private void rbDamaged_Click(System.Object sender, System.EventArgs e)
        {
            txtReason.Visible = true;
            lbReason.Visible = true;
            if (chkSelectAll.Checked == true)
                chkSelectAll.Checked = false;
            FillAvailableRooms();
            flag = 0;
        }

        private void rbRepaired_Click(System.Object sender, System.EventArgs e)
        {
            flag = 1;
            txtReason.Visible = false;
            lbReason.Visible = false;
            Label1.Text = "Select Repaired Rooms";
            if (chkSelectAll.Checked == true)
                chkSelectAll.Checked = false;
            FillDamagedRooms();
        }

        private void cboSublocation_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            if (cboSublocation.SelectedIndex < 0)
                SubLocID = 0;
            else
            {
                SubLocID = cf.cmbItemdata(cboSublocation, cboSublocation.SelectedIndex);
                if (chkSelectAll.Checked == true)
                    chkSelectAll.Checked = false;
                //FillDamagedRooms();
            }

            if (flag == 0)
                FillAvailableRooms();
            else
                FillDamagedRooms();
            RoomListBox.Refresh();
        }

        private void chkSelectAll_CheckedChanged(System.Object sender, System.EventArgs e)
        {
            int i = 0;
            if (chkSelectAll.Checked == true)
            {
                for (i = 0; i <= RoomListBox.Items.Count - 1; i++)
                    RoomListBox.SetItemCheckState(i, CheckState.Checked);
            }
            else
                for (i = 0; i <= RoomListBox.Items.Count - 1; i++)
                    RoomListBox.SetItemCheckState(i, CheckState.Unchecked);
        }
    }

}
