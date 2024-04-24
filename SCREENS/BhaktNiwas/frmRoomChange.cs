using CrystalDecisions.ReportAppServer;
using IDAutomation.Windows.Forms.LinearBarCode;
using Microsoft.Office.Interop.Excel;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
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
using SGMOSOL.ADMIN;
using static SGMOSOL.BAL.BhaktNiwasBAL;
using SGMOSOL.DAL.BhaktNiwas;
using SGMOSOL.BAL;

namespace SGMOSOL.SCREENS.BhaktNiwas
{
    public partial class frmRoomChange : Form
    {
        private eScreenID mScreenID;
        private bool mBlnEdit = false;
        private eAction mAction;        // Reference to Enum of Type Action for ActionView,ActionNew,ActionUpdate and ActionDelete
        private ArrayList CtrlArr = new ArrayList(); // To insert Form Control in Array List
        private ArrayList btnArr = new ArrayList();
        private int flag1;
        //private OSOL_ADMIN.clsDsCommon mClsDsCom = new OSOL_ADMIN.clsDsCommon();
        private RoomCheckInDAL objBlsRoomCheckIn = new RoomCheckInDAL();
        RoomMasterDAL objDsRoomMst = new RoomMasterDAL();
        //private OSOL_BLSDS.clsBlsRoomCheckOut objBlsRoomCheckOut = new OSOL_BLSDS.clsBlsRoomCheckOut();
        //private OSOL_BLSDS.clsDsBNRoomCheckInMst objDsRoomCheckInMst = new OSOL_BLSDS.clsDsBNRoomCheckInMst();
        //private OSOL_BLSDS.clsDsBNRoomChange RoomChangeDALobj = new OSOL_BLSDS.clsDsBNRoomChange();
        //private OSOL_BLSDS.clsDsBNRoomCheckInDet objDsRoomCheckInDet = new OSOL_BLSDS.clsDsBNRoomCheckInDet();
        //private OSOL_BLSDS.clsDsBNRoomMaster objDsRoomMst = new OSOL_BLSDS.clsDsBNRoomMaster();
        private bool DisableSendKeys;
        private int BhaktTypeId;
        private int cmbday;
        private double rent;
        private double Advance;
        private int isonline;
        private int RoomCheckInLockID;
        private bool bkDateEntry = false;
        private bool blnformChange;
        private string[] col;
        private string[] mStrErrMsg;
        public long mSearchId;
        // Added By Roshan
        private int DnrAllowedDays1;

        private Collection mDelColl = new Collection();
        private System.Data.DataTable TempTable1;
        private System.Data.DataTable TempTable2;
        private System.Data.DataSet ds = new System.Data.DataSet();
        private DataRow MyRow;
        private DateTime dtEnteredOn;
        // 'Dim RoomAdvanceTariff As Double
        // 'Dim RoomRentPerday As Double
        private string mStrCounterMachineShortName;
        private int RoomCheckInDeptID;
        private int PrintReceiptLocId;
        private int RoomCheckOutDeptID;
        private string RoomCheckInDeptName;
        private string RoomCheckInLocName;
        private string NameRoomHolder;
        private string PlaceRoomHolder;
        private int oldDays;
        private Int64 SublocId = 0;
        private string SublocAs;
        CommonFunctions cf;
        RoomChangeDAL RoomChangeDALobj;

        public frmRoomChange(eScreenID ScreenID)
        {
            InitializeComponent();
            this.Closing += new CancelEventHandler(this.frmRoomChange_Closing);
            mScreenID = ScreenID;
            cf = new CommonFunctions();
            RoomChangeDALobj = new RoomChangeDAL();
        }

        private void btnNew_Click(System.Object sender, System.EventArgs e)
        {
            System.Data.DataTable dr;
            FormClear();
            cf.fncSetDateAndRange(dtpCheckIn);
            cf.fncSysTime(dtpCheckInTime);
            dtpCheckOut.Value = dtpCheckIn.Value;
            dtpCheckOutTime.Value = dtpCheckInTime.Value;
            dtpCheckIn.Enabled = bkDateEntry;
            mAction = eAction.ActionInsert;
            cf.subLockForm(false, CtrlArr, false);
            btnSave.Enabled = btnNew.Enabled;
            //try
            //{
            //    dr = objDsRoomCheckInMst.GetDrMaxSrNo(txtCounter.Tag, UserInfo.CompanyID, PrintReceiptLocId, 0, UserInfo.fy_id);
            //    if (dr.Read())
            //    {
            //    }
            //    else
            //    {
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
            FillAvailableRooms();
            chkRooms.Enabled = true;
            FillSublocations();

            blnformChange = false;
            if (mAction == eAction.ActionInsert)
                btnPrint.Enabled = false;
            else
                btnPrint.Enabled = true;
        }



        private void frmRoomChange_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter & !DisableSendKeys)
                SendKeys.Send("{tab}");
            if (e.KeyCode == Keys.End & (mAction == eAction.ActionInsert | mAction == eAction.ActionUpdate) & blnformChange)
                btnSave_Click(null, null);
        }

        private void frmRoomChange_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End & (mAction == eAction.ActionInsert | mAction == eAction.ActionUpdate) & blnformChange)
                btnSave_Click(null, null);
        }

        private void frmRoomChange_Load(System.Object sender, System.EventArgs e)
        {
            // Me.WindowState = FormWindowState.Maximized
            cf.setControlsonForm(this, CtrlArr, btnArr);
            cf.SetUserScreenActions(this, UserInfo.UserId, (int)eScreenID.RoomCheckIn, btnArr, null, mBlnEdit);
            CtrlArr.Remove(dtpCheckIn);
            CtrlArr.Remove(dtpCheckInTime);
            CtrlArr.Remove(txtVchNo);
            CtrlArr.Remove(dtpCheckOut);
            CtrlArr.Remove(dtpCheckOutTime);
            CtrlArr.Remove(nudAdvance);
            CtrlArr.Remove(nudRent);
            CtrlArr.Remove(txtUser);
            CtrlArr.Remove(txtCounter);

            txtUser.Text = UserInfo.UserName;
            FillCounter();

            //Int32 ctr;
            //ctr = (MDI.Size.Width - this.Size.Width) / (double)2;
            //this.Location = new Point(ctr, 0);
            CreateDs();
            cmbDays.Items.Clear();
            cmbDays.Items.Add("1");
            cmbDays.Items.Add("2");
            cmbDays.Items.Add("3");
            cmbDays.Items.Add("4");
            cmbDays.Items.Add("5");


            // Dim dr As SqlClient.SqlDataReader
            // Try
            // dr = objDsRoomMst.GetDrRoomTariff()
            // If dr.Read Then
            // RoomAdvanceTariff = dr("Advance")
            // RoomRentPerday = dr("RentPerDay")
            // End If
            // dr.Close()
            // Catch ex As Exception
            // If Not dr Is Nothing Then dr.Close()
            // End Try
            if (btnNew.Enabled)
                btnNew_Click(null, null);
            else
                cf.subLockForm(true, CtrlArr, false);
        }

        private void FillCounter()
        {
            System.Data.DataTable dr;
            dr = cf.GetDrCounterMachId(UserInfo.UserId, SystemHDDModelNo, SystemHDDSerialNo, SystemMacID, Convert.ToInt16(eModType.BhaktaNiwas));
            if (dr.Rows.Count > 0)
            {
                txtCounter.Text = dr.Rows[0]["CounterMachineTitle"].ToString();
                txtCounter.Tag = dr.Rows[0]["CtrMachId"];
                RoomCheckInDeptID = Convert.ToInt32(dr.Rows[0]["DeptId"]);
                PrintReceiptLocId = Convert.ToInt32(dr.Rows[0]["LocId"]);
                RoomCheckOutDeptID = Convert.ToInt32(dr.Rows[0]["DeptId"]);
                RoomCheckInDeptName = dr.Rows[0]["DepartmentName"].ToString();
                RoomCheckInLocName = dr.Rows[0]["LOC_FNAME"].ToString();
                RoomCheckInLockID = Convert.ToInt32(dr.Rows[0]["LocId"]);
                mStrCounterMachineShortName = dr.Rows[0]["CounterMachineShortName"].ToString();
            }
        }

        private void FormClear()
        {
            txtVchNo.Focus();
            txtVchNo.Text = "";
            txtVchNo.Tag = null;
            txtReason.Text = "";
            txtName.Text = "";
            txtmobno.Text = "";
            txtPlace.Text = "";
            cmbDays.Text = "";
            txtNoOfRooms.Text = "";
            nudAdvance.Value = 0;
            nudRent.Value = 0;
            nudRentPaid.Value = 0;
            nudRentPending.Value = 0;
            chkRooms.Items.Clear();
        }

        private void btnSave_Click(System.Object sender, System.EventArgs e)
        {
            long lngError = -1;
            if (blnformChange == false)
                return;
            if (fncSave())
            {
                blnformChange = false;
                btnNew_Click(null, null);
                blnformChange = false;
            }
        }

        private bool fncSave()
        {
            long lngError = -1;
            RoomCheckInMst RoomCheckInMst;
            RoomCheckOutMst RoomCheckOutMst;
            RoomChangeMst RoomChangeMst;
            Collection coll;
            Collection coll1;
            long lngSerialNo;
            string strErr;
            bool flag = true;
            if (IsValidForm() == false)
            {
                return false;
            }


            // added by roshan
            if (BhaktTypeId == 4 | BhaktTypeId == 5)
            {
                if ((DnrAllowedDays1 == 0))
                {
                    MessageBox.Show("Allocated Room for Donner crossed for max limit. You can not allocate room under Donner..!", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbDays.Text = DnrAllowedDays1.ToString();
                    return false;
                }
                if ((Convert.ToInt32(cmbDays.Text) > DnrAllowedDays1))
                {
                    MessageBox.Show("Donner has only remaning: " + DnrAllowedDays1.ToString() + " Days", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbDays.Text = DnrAllowedDays1.ToString();
                    return false;
                }
            }
            System.Data.DataTable dr;
            string strRcptNo;
            strRcptNo = txtVchNo.Text;
            dr = RoomChangeDALobj.GetDrRoomChangeMst(0, "", strRcptNo, Convert.ToInt64(txtCounter.Tag), UserInfo.CompanyID, PrintReceiptLocId, RoomCheckOutDeptID, 0, "");
            if (dr.Rows.Count > 0)
            {
                if (Convert.ToInt32(dr.Rows[0]["PendRoomCount"]) == 0)
                {
                    MessageBox.Show("No pending Rooms against the Receipt No. " + txtVchNo.Text, PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtVchNo.Text = "";
                    txtVchNo.Focus();
                    return false;
                }
            }

            setCursor(this, false);
            lngSerialNo = Convert.ToInt64(txtVchNo.Text);
            RoomCheckInMst = GetCheckInMst();
            coll = GetCheckInDetColl();
            RoomChangeMst = GetRoomChangeMst();

            coll1 = GetCheckOutDetColl();
            mDelColl = getRoom();
            mAction = eAction.ActionUpdate;
            if (mAction == eAction.ActionUpdate)
            {
                lngError = objBlsRoomCheckIn.UpdateChange(RoomCheckInMst, coll, mDelColl, UserInfo.UserName, UserInfo.Machine_Name, lngSerialNo);
                if (mScreenID == eScreenID.RoomChange)
                    lngError = objBlsRoomCheckIn.Insert1(RoomChangeMst, mDelColl, UserInfo.UserName, UserInfo.Machine_Name, lngSerialNo, dtEnteredOn);
                lngError = objBlsRoomCheckIn.InsertRoomExtend(RoomCheckInMst, UserInfo.UserName, UserInfo.Machine_Name);
            }
            setCursor(this, true);
            if (lngError < 0)
            {
                strErr = ProcErr(lngError);
                col = new string[1];
                col[0] = "";
                if (Strings.Len(strErr) == 0)
                    strErr = cf.GetErrorMessage(mStrErrMsg, 10);
                MessageBox.Show(strErr, PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                setCursor(this, true);
                flag = false;
            }
            else if (lngError >= 0)
            {
                col = new string[2];
                col[0] = "Receipt";
                col[1] = Conversion.Str(lngSerialNo);
                MessageBox.Show(cf.GetErrorMessage(col, 7), PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                setCursor(this, true);
                flag = true;

                // If Val(dtpCheckIn.Tag & vbNullString) = 0 Then
                blnformChange = false;
                btnPrint_Click(null, null);
            }
            return flag;
        }
        private void FillSublocations()
        {
            System.Data.DataTable dr;
            try
            {
                dr = objDsRoomMst.GetDrSublocations(PrintReceiptLocId, (Int64)eModType.BhaktaNiwas);
                cf.FillCombo(cboSublocation, dr, "Name", "DeptId");
            }
            // dr.Close()
            catch (Exception ex)
            {
            }
        }
        private void FillAvailableRooms()
        {
            flag1 = 0;
            // Dim str = txtRoomSrch.Text
            var str = "";
            System.Data.DataTable dr;
            try
            {
                if (cboSublocation.SelectedIndex < 0)
                    SublocId = 0;
                else
                    SublocId = cf.cmbItemdata(cboSublocation, cboSublocation.SelectedIndex);

                dr = objDsRoomMst.GetDrRoomDetails1(str, SublocId, (int)eTokenDetail.StatusYes, RoomCheckInLockID);
                cf.FillListBox(chkRooms, dr, "RoomName", "RoomId", "RecordModifiedCount");
            }

            // dr.Close()
            catch (Exception ex)
            {
            }
        }
        private void cboSublocation_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            FillAvailableRooms();
        }
        private RoomCheckInMst GetCheckInMst()
        {
            RoomCheckInMst CheckInMst = new RoomCheckInMst();
            CheckInMst.CheckInMstId = Convert.ToInt64(txtVchNo.Tag);
            CheckInMst.ComId = UserInfo.CompanyID;
            CheckInMst.LocId = RoomCheckInLockID;
            CheckInMst.DeptId = RoomCheckInDeptID;
            CheckInMst.CtrMachId = Convert.ToInt64(txtCounter.Tag);
            CheckInMst.FyId = UserInfo.fy_id;
            CheckInMst.InDate = dtpCheckIn.Value;
            CheckInMst.InTime = dtpCheckInTime.Value;
            CheckInMst.SerialNo = Convert.ToInt64(txtVchNo.Tag);
            // CheckInMst.AppNo = Val(txtAppNo.Text)
            CheckInMst.Name = txtName.Text;
            CheckInMst.Place = txtPlace.Text;
            CheckInMst.mob_no = txtmobno.Text;
            CheckInMst.Days = Convert.ToInt32(cmbDays.Text);
            CheckInMst.ExtDay = Convert.ToInt32(cmbDays.Text);
            CheckInMst.ExtRent = nudRentPending.Value;
            CheckInMst.ExtDate = DateTime.Now;
            CheckInMst.Sublocid = cf.cmbItemdata(cboSublocation, cboSublocation.SelectedIndex);
            CheckInMst.sublocn = cboSublocation.Text;
            CheckInMst.NoOfRooms = Convert.ToInt32(txtNoOfRooms.Text);
            CheckInMst.OutDate = dtpCheckOut.Value;
            CheckInMst.OutTime = dtpCheckOutTime.Value;
            CheckInMst.Advance = nudAdvance.Value;
            CheckInMst.Rent = nudRent.Value;
            CheckInMst.UserId = UserInfo.UserId;
            CheckInMst.ServerName = UserInfo.serverName;
            CheckInMst.EnteredBy = UserInfo.UserName;
            CheckInMst.ModifiedBy = UserInfo.UserName;
            CheckInMst.RecordModifiedCount = Convert.ToInt64(dtpCheckIn.Tag) + 1;
            return CheckInMst;
        }


        private Collection GetCheckInDetColl()
        {
            Collection coll = new Collection();

            RoomCheckInDet CheckInDet = new RoomCheckInDet();
            CheckInDet.CheckInMstId = Convert.ToInt64(txtVchNo.Tag);
            for (var i = 0; i <= chkRooms.Items.Count - 1; i++)
            {
                if (chkRooms.GetItemChecked(i))
                {
                    CheckInDet.LockerId = cf.lsbItemData(chkRooms, i);
                    CheckInDet.LockerAvailableStatus = (int)eTokenDetail.StatusNo;
                    CheckInDet.LockerRecordModifiedCount = Convert.ToInt64(cf.lsbItemName2(chkRooms, i)) + 1;
                    coll.Add(CheckInDet);
                }
            }
            return coll;
        }
        private Collection GetCheckOutDetColl()
        {
            Collection coll = new Collection();
            RoomCheckOutDet CheckOutDet = new RoomCheckOutDet();
            CheckOutDet.CheckOutMstId = Convert.ToInt64(txtVchNo.Tag);
            for (var i = 0; i <= chkRooms.Items.Count - 1; i++)
            {
                if (!chkRooms.GetItemChecked(i))
                {
                    CheckOutDet.LockerId = cf.lsbItemData(chkRooms, i);
                    CheckOutDet.LockerAvailableStatus = (int)eTokenDetail.StatusYes;
                    CheckOutDet.LockerRecordModifiedCount = Convert.ToInt64(cf.lsbItemName2(chkRooms, i)) + 1;
                    coll.Add(CheckOutDet);
                }
            }
            return coll;
        }

        public Collection getRoom()
        {
            Collection coll = new Collection();
            RoomCheckInDet CheckInDet = new RoomCheckInDet();
            CheckInDet.CheckInMstId = Convert.ToInt64(txtVchNo.Tag);
            System.Data.DataTable dr;
            dr = RoomChangeDALobj.findRoom(CheckInDet.CheckInMstId);
            //while (dr.Read)
            foreach (DataRow item in dr.Rows)
            {
                CheckInDet.LockerId = Convert.ToInt32(item["ROOM_ID"]);
                CheckInDet.LockerAvailableStatus = (int)eTokenDetail.StatusYes;
                coll.Add(CheckInDet);
            }
            return coll;
        }
        private RoomCheckOutMst GetCheckOutMst()
        {
            RoomCheckOutMst CheckOutMst = new RoomCheckOutMst();
            CheckOutMst.CheckOutMstId = Convert.ToInt64(txtVchNo.Tag);
            CheckOutMst.ComId = UserInfo.CompanyID;
            CheckOutMst.LocId = PrintReceiptLocId;
            CheckOutMst.DeptId = RoomCheckOutDeptID;
            CheckOutMst.CtrMachId = Convert.ToInt64(txtCounter.Tag);
            CheckOutMst.FyId = UserInfo.fy_id;
            CheckOutMst.OutDate = dtpCheckOut.Value;
            CheckOutMst.OutTime = dtpCheckOutTime.Value;
            CheckOutMst.SerialNo = Convert.ToInt64(txtVchNo.Tag);
            CheckOutMst.CheckInMstId = Convert.ToInt64(txtVchNo.Tag);
            CheckOutMst.Days = Convert.ToInt32(cmbDays.Text);
            CheckOutMst.NoOfRooms = Convert.ToInt32(txtNoOfRooms.Text);
            CheckOutMst.Advance = nudAdvance.Value;
            CheckOutMst.Rent = nudRent.Value;
            // CheckOutMst.Refund = NudRefund.Value
            CheckOutMst.UserId = UserInfo.UserId;
            CheckOutMst.ServerName = UserInfo.serverName;
            CheckOutMst.EnteredBy = UserInfo.UserName;
            CheckOutMst.ModifiedBy = UserInfo.UserName;
            CheckOutMst.RecordModifiedCount = Convert.ToInt64(dtpCheckOut.Tag) + 1;
            return CheckOutMst;
        }


        private RoomChangeMst GetRoomChangeMst()
        {
            RoomChangeMst RoomChangeMst = new RoomChangeMst();

            RoomChangeMst.ComId = UserInfo.CompanyID;
            RoomChangeMst.LocId = RoomCheckInLockID;
            RoomChangeMst.DeptId = RoomCheckOutDeptID;
            RoomChangeMst.CtrMachId = Convert.ToInt64(txtCounter.Tag);
            RoomChangeMst.FyId = UserInfo.fy_id;
            RoomChangeMst.OutDate = dtpCheckOut.Value;
            RoomChangeMst.OutTime = dtpCheckOutTime.Value;
            RoomChangeMst.SerialNo = Convert.ToInt64(txtVchNo.Tag);
            RoomChangeMst.CheckInMstId = Convert.ToInt64(txtVchNo.Tag);

            RoomChangeMst.Reason = txtReason.Text;
            RoomChangeMst.UserId = UserInfo.UserId;
            RoomChangeMst.ServerName = UserInfo.serverName;
            RoomChangeMst.EnteredBy = UserInfo.UserName;
            RoomChangeMst.ModifiedBy = UserInfo.UserName;

            return RoomChangeMst;
        }
        private bool IsValidForm()
        {
            int count_Rooms;
            int Count_given;
            DialogResult msg;
            if (txtVchNo.Text == "")
            {
                mStrErrMsg = new string[1];
                mStrErrMsg[0] = "Rcpt No";
                return ShowValidateError(txtVchNo, 0, mStrErrMsg, 1);
            }

            if (txtPlace.Text == "")
            {
                mStrErrMsg = new string[1];
                mStrErrMsg[0] = "Place";
                return ShowValidateError(txtPlace, 0, mStrErrMsg, 1);
            }

            if (cmbDays.Text == "")
            {
                mStrErrMsg = new string[1];
                mStrErrMsg[0] = "Days";
                return ShowValidateError(cmbDays, 0, mStrErrMsg, 1);
            }



            if (mScreenID == eScreenID.RoomExtend)
            {
                if (Convert.ToInt32(cmbDays.Text) < cmbday)
                {
                    mStrErrMsg = new string[1];
                    mStrErrMsg[0] = "";
                    msg = MessageBox.Show(cf.GetErrorMessage(mStrErrMsg, 201), PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                if (Convert.ToInt32(cmbDays.Text) == cmbday)
                {
                    mStrErrMsg = new string[1];
                    mStrErrMsg[0] = "";
                    msg = MessageBox.Show(cf.GetErrorMessage(mStrErrMsg, 202), PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            if (txtNoOfRooms.Text == "")
            {
                mStrErrMsg = new string[1];
                mStrErrMsg[0] = "No. of Rooms";
                return ShowValidateError(txtNoOfRooms, 0, mStrErrMsg, 1);
            }

            if (chkRooms.CheckedItems.Count == 0)
            {
                mStrErrMsg = new string[1];
                mStrErrMsg[0] = "Rooms";
                return ShowValidateError(txtNoOfRooms, 0, mStrErrMsg, 37);
            }


            // added by payal for Same Room Validation 

            // If chkRooms_given.SelectedIndex.Equals(chkRooms.SelectedIndex) Then
            // MessageBox.Show("You Can not select Same Room", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
            // chkRooms.Focus()
            // return False
            // Exit Function
            // End If

            count_Rooms = chkRooms.CheckedItems.Count;
            if (Convert.ToInt32(txtNoOfRooms.Text) != count_Rooms)
            {
                mStrErrMsg = new string[1];
                mStrErrMsg[0] = "Rooms";
                MessageBox.Show("No. of Rooms specified doesn't match with Rooms Selected.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                chkRooms.Focus();
                return false;
            }
            if (mScreenID == eScreenID.RoomChange)
            {
                Count_given = chkRooms_given.CheckedItems.Count;
                if (Count_given != count_Rooms)
                {
                    mStrErrMsg = new string[1];
                    mStrErrMsg[0] = "Rooms";
                    MessageBox.Show("No of room change should be same as No of room given.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    chkRooms.Focus();
                    return false;
                }
                DateTime dt;
                dt = UserInfo.TodaysDate;

                if (dt >= dtpCheckIn.Value)
                {
                    if ((nudRentPending.Value != 0))
                    {
                        mStrErrMsg = new string[1];
                        mStrErrMsg[0] = "Rooms";
                        MessageBox.Show("You can change room of same amount only.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        chkRooms.Focus();
                        return false;
                    }
                }
            }
            return true;
        }

        private string ProcErr(long pErrNum)
        {
            string ProcErr = "";
            mStrErrMsg = new string[1];
            mStrErrMsg[0] = "";
            switch (pErrNum)
            {
                case -50000:
                    {
                        ProcErr = cf.GetErrorMessage(mStrErrMsg, 6);
                        break;
                    }

                case -101:
                    {
                        ProcErr = cf.GetErrorMessage(mStrErrMsg, 23);
                        break;
                    }

                case -102:
                    {
                        ProcErr = cf.GetErrorMessage(mStrErrMsg, 23);
                        break;
                    }

                case -103:
                    {
                        ProcErr = cf.GetErrorMessage(mStrErrMsg, 24);
                        break;
                    }

                case -104:
                    {
                        ProcErr = cf.GetErrorMessage(mStrErrMsg, 25);
                        break;
                    }

                case -547:
                    {
                        ProcErr = "Cannot be deleted as child record exist.";
                        break;
                    }

                case -2627:
                    {
                        ProcErr = "Room already used.";
                        break;
                    }

                default:
                    {
                        ProcErr = "";
                        break;
                    }
            }
            return ProcErr;
        }




        private void btnSearch_Click(System.Object sender, System.EventArgs e)
        {
            DialogResult sReply;
            if ((mAction == eAction.ActionInsert | mAction == eAction.ActionUpdate) & blnformChange)
            {
                mStrErrMsg = new string[1];
                mStrErrMsg[0] = "";
                sReply = MessageBox.Show(cf.GetErrorMessage(mStrErrMsg, 18), PrjMsgBoxTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (sReply == DialogResult.Yes)
                {
                    if (!fncSave())
                        return;
                }
                else if (sReply == DialogResult.Cancel)
                    return;
            }
            // btnPrint.Enabled = True
            setCursor(this, false);
            frmSearchNew form1 = new frmSearchNew("BN_ROOM_CHANGE_MST_T_FIND_V", false, eModType.BhaktaNiwas);
            long lngSearchId;
            form1.mIntCtrMachId = Convert.ToInt32(txtCounter.Tag);
            form1.ShowDialog();
            lngSearchId = form1.mLngSearchId;
            if (lngSearchId <= 0)
            {
                setCursor(this, true);
                return;
            }
            bool blnFormLock = false;
            if (LoadTransaction(lngSearchId, ref blnFormLock) == true)
            {
                blnformChange = false;
                if (mBlnEdit & !blnFormLock)
                {
                    mAction = eAction.ActionUpdate;
                    cf.subLockForm(false, CtrlArr, false);
                    dtpCheckIn.Enabled = false;
                    dtpCheckInTime.Enabled = false;
                    chkRooms.Enabled = true;
                    btnPrint.Enabled = true;
                    txtVchNo.Focus();
                }
                else
                {
                    mAction = eAction.ActionLocked;
                    dtpCheckIn.Enabled = false;
                    dtpCheckInTime.Enabled = false;
                    chkRooms.Enabled = false;
                    cf.subLockForm(true, CtrlArr, false);
                }
            }
            else
            {
                FormClear();
                setCursor(this, true);
                return;
            }
            blnformChange = false;
            btnSave.Enabled = mBlnEdit;
            if (btnSave.Enabled)
                btnSave.Enabled = !blnFormLock;
            setCursor(this, true);
            btnPrint.Enabled = true;
        }

        private bool LoadTransaction(long lngSearchId, ref bool blnLockForm)
        {
            System.Data.DataTable dr;
            // Dim CheckInDet As New LockerCheckInMst
            RoomCheckInDet CheckInDet = new RoomCheckInDet();
            string sublocation = "";
            string sublocationID;
            int ctr = 0;
            bool blnFlag = false;
            mDelColl = new Collection();
            FormClear();
            try
            {
                dr = objBlsRoomCheckIn.GetDrRoomCheckInMst(lngSearchId);
                if (dr.Rows.Count > 0)
                {
                    // txtAppNo.Text = dr("AppNo")
                    dtpCheckIn.Value = Convert.ToDateTime(dr.Rows[0]["InDate"]);
                    dtpCheckInTime.Value = Convert.ToDateTime(dr.Rows[0]["InTime"]);
                    txtVchNo.Text = dr.Rows[0]["SerialNo"].ToString();
                    txtVchNo.Tag = lngSearchId;
                    dtpCheckIn.Tag = dr.Rows[0]["RecordModifiedCount"];
                    txtName.Text = dr.Rows[0]["Name"].ToString();
                    txtPlace.Text = dr.Rows[0]["Place"].ToString();
                    cmbDays.Text = dr.Rows[0]["Days"].ToString();

                    dtpCheckOut.Value = Convert.ToDateTime(dr.Rows[0]["OutDate"]);
                    dtpCheckOutTime.Value = Convert.ToDateTime(dr.Rows[0]["OutTime"]);
                    txtNoOfRooms.Text = dr.Rows[0]["NoOfRooms"].ToString();
                    sublocationID = dr.Rows[0]["SUBLOC_ID"].ToString();
                    sublocation = dr.Rows[0]["SUBLOCATION"].ToString();
                    nudAdvance.Text = dr.Rows[0]["Advance"].ToString();
                    nudRent.Text = dr.Rows[0]["Rent"].ToString();
                    dtEnteredOn = Convert.ToDateTime(dr.Rows[0]["EnteredOn"]);

                    // cboSublocation.Text = sublocation

                    if (dr.Rows[0]["NoOfRooms"] != dr.Rows[0]["PendRoomCount"])
                        blnLockForm = true;
                }
                cboSublocation.Text = sublocation;
                if (chkRooms.Items.Count > 0)
                    chkRooms.Items.Clear();

                dr = objBlsRoomCheckIn.GetDrRoomCheckInDet(lngSearchId, !blnLockForm, Convert.ToInt64(txtCounter.Tag));
                mDelColl = new Collection();
                CheckInDet = new RoomCheckInDet();
                CheckInDet.CheckInMstId = Convert.ToInt64(txtVchNo.Tag);
                //while (dr.Read)
                foreach (DataRow drItem in dr.Rows)
                {
                    ctr = ctr + 1;
                    chkRooms.Items.Add(new clsItemData(drItem["RoomName"].ToString(), Convert.ToInt32(drItem["RoomId"]), drItem["RecordModifiedCount"].ToString()));

                    if (Convert.ToInt32(drItem["RoomCheckInDetId"]) > 0)
                    {
                        chkRooms.SetItemChecked(ctr - 1, true);
                        CheckInDet.LockerId = Convert.ToInt32(drItem["RoomId"]);
                        CheckInDet.LockerAvailableStatus = (int)eTokenDetail.StatusYes;
                        CheckInDet.LockerRecordModifiedCount = Convert.ToInt64(drItem["RecordModifiedCount"]) + 1;
                        CheckInDet.CheckInDetId = Convert.ToInt64(drItem["RoomCheckInDetId"]);
                        CheckInDet.CheckInMstId = Convert.ToInt64(drItem["RoomCheckInMstId"]);
                        mDelColl.Add(CheckInDet);
                    }
                }
                blnFlag = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load Transaction failed.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnNew_Click(null, null);
                blnFlag = false;
            }
            return blnFlag;
        }

        private void btnClose_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void frmRoomChange_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult msg;
            try
            {
                if (blnformChange == true)
                {
                    mStrErrMsg = new string[1];
                    mStrErrMsg[0] = "";
                    msg = MessageBox.Show(cf.GetErrorMessage(mStrErrMsg, 18), PrjMsgBoxTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                    if (msg == DialogResult.Yes)
                    {
                        try
                        {
                            if (btnSave.Enabled == false)
                                return;
                            if (!fncSave())
                            {
                                e.Cancel = true;
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            Interaction.MsgBox(ex.Message);
                        }
                    }
                    else if (msg == DialogResult.No)
                    {
                    }
                    else if (msg == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }
            // objThread.Abort()
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message, MsgBoxStyle.Information, PrjMsgBoxTitle);
            }
        }
        //public sealed class myPrinters
        //{
        //    private myPrinters()
        //    {
        //    }
        //    [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        //    public static bool SetDefaultPrinter(string Name)
        //    {
        //    }
        //}
        private void btnPrint_Click(System.Object sender, System.EventArgs e)
        {
            // If Val(dtpCheckIn.Tag & vbNullString) = 0 Or blnformChange Then Exit Sub

            // Dim strReportName As String
            // Dim sForm As Form
            // Dim pColl As New Collection()
            // setCursor(Me, False)
            // strReportName = "LockerCheckInReceipt.rpt"
            // FillDataInDataset()
            // sForm = New frmCrystalViewer(UserInfo.ReportPath & strReportName, , ds, , pColl, ReportID.LockerCheckIn, True)
            // sForm.Text = "Locker Check In : " & ReportID.LockerCheckIn
            // sForm.Show()
            // setCursor(Me, True)

            string strReportName;
            Form sForm;
            Collection pColl = new Collection();
            setCursor(this, false);
            System.Drawing.Printing.PrintDocument printDoc = new System.Drawing.Printing.PrintDocument();
            //string printD1;

            //myPrinters.SetDefaultPrinter(System.Configuration.ConfigurationSettings.AppSettings.Get("Printer_name"));
            //printD1 = printDoc.PrinterSettings.PrinterName;
            //if (printDoc.PrinterSettings.PrinterName == printD1)
            {
                strReportName = "RoomCheckInReceipt.rdlc";
                FillDataInDataset(txtmobno.Tag.ToString());
                sForm = new frmCrystalViewer(UserInfo.ReportPath + strReportName, null, ds, null, pColl, (long)eScreenID.RoomCheckIn, true);
                sForm.Text = "Room Check In : " + eReportID.RoomCheckIn;


                // Dim printDoc As New System.Drawing.Printing.PrintDocument()
                // Dim printD1 As String
                // printD1 = printDoc.PrinterSettings.PrinterName
                // If printDoc.PrinterSettings.PrinterName = "Send To OneNote 2007" Then
                sForm.Show();

                // comment by roshan for not print amount recept in room change
                // System.Threading.Thread.Sleep(850)
                // sForm.Close()
                // strReportName = "RoomCheckInNew.rpt"
                // sForm = New frmCrystalViewer(UserInfo.ReportPath & strReportName, , ds, , pColl, ReportID.RoomCheckIn1, True)
                // sForm.Text = "Room Check IN : " & ReportID.RoomCheckIn1

                // sForm.Show()
                // System.Threading.Thread.Sleep(850)
                // sForm.Close()

                // End If
                setCursor(this, true);
            }
            //else
            //    MessageBox.Show("Printer does not exists");
        }

        private void FillDataInDataset(string Barcode)
        {
            Int16 i;
            decimal total_val;

            TempTable1.Rows.Clear();
            TempTable2.Rows.Clear();

            MyRow = TempTable1.NewRow();
            MyRow["CHECK_IN_MST_ID"] = 1;
            MyRow["LOC_SH_NAME"] = RoomCheckInLocName;
            MyRow["DEPT_SH_NAME"] = RoomCheckInDeptName;
            MyRow["COUNTER"] = mStrCounterMachineShortName;

            MyRow["IN_DATE"] = dtpCheckIn.Value;
            MyRow["IN_TIME"] = dtpCheckInTime.Value;
            MyRow["SERIAL_NO"] = (txtVchNo.Text);
            // MyRow["APP_NO"] = txtAppNo.Text
            MyRow["SublocationNm"] = cboSublocation.Text;
            MyRow["NAME"] = txtName.Text;
            MyRow["PLACE"] = txtPlace.Text;
            MyRow["MOB_NO"] = txtmobno.Text;
            MyRow["DAYS"] = cmbDays.Text;

            MyRow["NO_OF_ROOMS"] = txtNoOfRooms.Text;
            MyRow["OUT_DATE"] = dtpCheckOut.Value;
            MyRow["OUT_TIME"] = dtpCheckOutTime.Value;

            MyRow["ADVANCE"] = nudAdvance.Value;
            MyRow["RENT"] = nudRent.Value;

            MyRow["USER_NAME"] = txtUser.Text;
            MyRow["SERVER_NAME"] = UserInfo.serverName;
            MyRow["MACHINE_NAME"] = UserInfo.Machine_Name;
            total_val = nudAdvance.Value + nudRent.Value;
            MyRow["AMT_IN_WORDS"] = cf.getNumbersInWords(total_val, eCurrencyType.Rupees);
            byte[] _tempByte = null;
            IDAutomation.Windows.Forms.LinearBarCode.Barcode NewBarcode = new Barcode();
            string imageName = "img" + txtVchNo.Text + DateTime.Now.Millisecond + ".Jpeg";

            // NewBarcode.DataToEncode = txtName.Text.Substring(0, 4) & txtVchNo.Text 'Input of textbox to generate barcode 
            NewBarcode.DataToEncode = Barcode; // Input of textbox to generate barcode 
            NewBarcode.SymbologyID = Symbologies.Code39;
            NewBarcode.Code128Set = Code128CharacterSets.A;
            NewBarcode.RotationAngle = RotationAngles.Zero_Degrees;
            NewBarcode.RefreshImage();
            NewBarcode.Resolution = Resolutions.Screen;
            NewBarcode.ResolutionCustomDPI = 96;
            NewBarcode.RefreshImage();
            NewBarcode.ShowText = true;

            NewBarcode.SaveImageAs(System.Configuration.ConfigurationSettings.AppSettings.Get("BN_BARCODE_PATH") + @"\" + imageName, System.Drawing.Imaging.ImageFormat.Jpeg);
            NewBarcode.Resolution = Resolutions.Printer;

            // 'Image(img = Image.FromFile(Application.StartupPath & "\" & "SavedBarcode.Jpeg"))


            // Dim _fileInfo As New IO.FileInfo(Application.StartupPath & "\" & "Barcode1.Jpeg")
            System.IO.FileInfo _fileInfo = new System.IO.FileInfo(System.Configuration.ConfigurationSettings.AppSettings.Get("BN_BARCODE_PATH") + @"\" + imageName);
            string FileName = System.Configuration.ConfigurationSettings.AppSettings.Get("BN_BARCODE_PATH") + @"\" + imageName;
            long _NumBytes = _fileInfo.Length;
            System.IO.FileStream _FStream = new System.IO.FileStream(System.Configuration.ConfigurationSettings.AppSettings.Get("BN_BARCODE_PATH") + @"\" + imageName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader _BinaryReader = new System.IO.BinaryReader(_FStream);
            _tempByte = _BinaryReader.ReadBytes(Convert.ToInt32(_NumBytes));
            _fileInfo = null;
            _NumBytes = 0;
            _FStream.Close();
            _FStream.Dispose();
            _BinaryReader.Close();
            MyRow["BARCODE"] = _tempByte;

            TempTable1.Rows.Add(MyRow);
            if (System.IO.File.Exists(FileName) == true)
                System.IO.File.Delete(FileName);
            string Roomname = "";
            for (i = 0; i <= chkRooms.Items.Count - 1; i++)
            {
                if (chkRooms.GetItemChecked(i))
                {
                    if (Roomname == "")
                        Roomname = cf.lsbItemName(chkRooms, i);
                    else
                        Roomname = Roomname + ", " + cf.lsbItemName(chkRooms, i);
                }
            }
            MyRow = TempTable2.NewRow();
            MyRow["CHECK_IN_MST_ID"] = 1;
            MyRow["ROOM_NAME"] = Roomname;
            TempTable2.Rows.Add(MyRow);
        }

        private void CreateDs()
        {
            TempTable1 = new System.Data.DataTable("BN_ROOM_CHECK_IN_MST_T");
            TempTable2 = new System.Data.DataTable("BN_ROOM_CHECK_IN_DET_T");

            TempTable1.Columns.Add("CHECK_IN_MST_ID", System.Type.GetType("System.Int64"));
            TempTable1.Columns.Add("LOC_SH_NAME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("DEPT_SH_NAME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("COUNTER", System.Type.GetType("System.String"));

            TempTable1.Columns.Add("IN_DATE", System.Type.GetType("System.DateTime"));
            TempTable1.Columns.Add("IN_TIME", System.Type.GetType("System.DateTime"));
            TempTable1.Columns.Add("SERIAL_NO", System.Type.GetType("System.Int64"));
            // TempTable1.Columns.Add("APP_NO", System.Type.GetType("System.String"))
            TempTable1.Columns.Add("SublocationNm", System.Type.GetType("System.String"));

            TempTable1.Columns.Add("NAME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("PLACE", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("MOB_NO", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("DAYS", System.Type.GetType("System.Int32"));

            TempTable1.Columns.Add("NO_OF_ROOMS", System.Type.GetType("System.Int32"));
            TempTable1.Columns.Add("OUT_DATE", System.Type.GetType("System.DateTime"));
            TempTable1.Columns.Add("OUT_TIME", System.Type.GetType("System.DateTime"));

            TempTable1.Columns.Add("ADVANCE", System.Type.GetType("System.Double"));
            TempTable1.Columns.Add("RENT", System.Type.GetType("System.Double"));
            TempTable1.Columns.Add("BARCODE", System.Type.GetType("System.Byte[]"));
            TempTable1.Columns.Add("USER_NAME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("SERVER_NAME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("MACHINE_NAME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("AMT_IN_WORDS", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("REPORT_TYPE", System.Type.GetType("System.String"));

            TempTable2.Columns.Add("CHECK_IN_MST_ID", System.Type.GetType("System.Int64"));
            TempTable2.Columns.Add("ROOM_NAME", System.Type.GetType("System.String"));

            ds.Tables.Add(TempTable1);
            ds.Tables.Add(TempTable2);
        }


        private void txtName_TextChanged(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
        }

        private void txtPlace_TextChanged(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
        }

        private void txtDays_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        // Private Sub txtDays_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        // blnformChange = True
        // dtpCheckOut.Value = dtpCheckIn.Value.AddDays(Val(cmbDays.Text & vbNullString))
        // showRent()
        // 'nudRent.Value = RoundIt(RoomRentPerday * Val(txtNoOfRooms.Text & vbNullString) * Val(txtDays.Text & vbNullString), 2)
        // 'nudAdvance.Value = RoundIt(RoomAdvanceTariff * Val(txtNoOfRooms.Text & vbNullString) * Val(txtDays.Text & vbNullString), 2)
        // End Sub

        private void txtNoOfRooms_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtNoOfRooms_TextChanged(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
            showRent();
        }


        private void txtVchNo_TextChanged(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
        }

        // Private Sub txtAppNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        // If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or Asc(e.KeyChar) = 8 Or Asc(e.KeyChar) = 46) Then
        // e.Handled = True
        // End If
        // End Sub




        private void chkRooms_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
            double RoomRentPerday = 0;
            double RoomAdvanceTariff = 0;
            if (BhaktTypeId == 1 | BhaktTypeId == 3)
            {
                for (var i = 0; i <= chkRooms.Items.Count - 1; i++)
                {
                    if (chkRooms.GetItemChecked(i))
                    {
                        var id = cf.lsbItemData(chkRooms, i);
                        System.Data.DataTable dr;
                        try
                        {
                            dr = objDsRoomMst.GetRent(id);
                            if (dr.Rows.Count > 0)
                            {
                                RoomRentPerday += Convert.ToDouble(dr.Rows[0]["RentPerDay"]);
                                RoomAdvanceTariff += Convert.ToDouble(dr.Rows[0]["Advance"]);
                            }
                            if (isonline == 1)
                                RoomRentPerday = 0;
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }

            rent = RoomRentPerday;
            Advance = RoomAdvanceTariff;
            showRent();
            txtNoOfRooms.Text = chkRooms.CheckedItems.Count.ToString();
        }
        private void showRent()
        {
            if (rent != 0 & cmbDays.Text != "")
            {
                nudRent.Value = Convert.ToDecimal(Math.Round(rent * Convert.ToDouble(cmbDays.Text), 2));
                nudRentPending.Value = Math.Round(nudRent.Value - nudRentPaid.Value, 2);
                nudAdvance.Value = Convert.ToDecimal(Math.Round(Convert.ToDouble(Advance), 0));
            }
        }
        private bool ShowValidateError(Control myObject, int tabIndex, string[] ErrMsg, int ErrNo)
        {
            setCursor(this, true);
            MessageBox.Show(cf.GetErrorMessage(ErrMsg, ErrNo), PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            myObject.Focus();
            return false;
        }



        private void SetError(string str)
        {
            clsConnection.mErrorResult = clsConnection.mErrorResult + str;
        }


        private void btnLoad_Click(System.Object sender, System.EventArgs e)
        {
            double RoomAdvanceTariff1;
            double RoomRentPerday1;

            System.Data.DataTable dr;
            Int16 i;
            // Dim strApp As String
            string strRcptNo;
            if (txtVchNo.Text == "")
            {
                mStrErrMsg = new string[1];
                mStrErrMsg[0] = "Rcpt No";
                ShowValidateError(txtVchNo, 0, mStrErrMsg, 1);
                return;
            }

            strRcptNo = txtVchNo.Text;
            FormClear();
            // fncSetDateAndRange(dtpCheckOut)
            // fncSysTime(dtpCheckOutTime)

            txtVchNo.Text = strRcptNo;

            try
            {
                // dr = objDsLockerChange.GetDrLockerChangeMst(, , , txtCounter.Tag, UserInfo.CompanyID, UserInfo.LocationID, LockerCheckOutDeptID, , , Val(txtAppNo.Text & vbNullString))
                dr = RoomChangeDALobj.GetDrRoomChangeMst(0, "", strRcptNo, Convert.ToInt64(txtCounter.Tag), UserInfo.CompanyID, PrintReceiptLocId, RoomCheckOutDeptID, 0, "");

                if (dr.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dr.Rows[0]["PendRoomCount"]) == 0)
                    {
                        MessageBox.Show("No pending Rooms against the Receipt No. " + txtVchNo.Text, PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtVchNo.Text = "";
                        txtVchNo.Focus();
                        return;
                    }
                    txtVchNo.Tag = Convert.ToInt32(dr.Rows[0]["CheckInMstId"]);
                    dtpCheckIn.Value = Convert.ToDateTime(dr.Rows[0]["InDate"]);
                    dtpCheckInTime.Value = Convert.ToDateTime(dr.Rows[0]["InTime"]);
                    txtVchNo.Text = dr.Rows[0]["SerialNo"].ToString();
                    dtpCheckIn.Tag = dr.Rows[0]["RecordModifiedCount"];
                    BhaktTypeId = Convert.ToInt32(dr.Rows[0]["BhaktType"]);
                    NameRoomHolder = dr.Rows[0]["Name"].ToString();
                    txtName.Text = NameRoomHolder;
                    PlaceRoomHolder = dr.Rows[0]["Place"].ToString();
                    txtPlace.Text = PlaceRoomHolder;
                    txtmobno.Tag = dr.Rows[0]["BARCODE"];
                    // 'line change by girish
                    oldDays = Convert.ToInt16(dr.Rows[0]["Days"]);
                    rent = Convert.ToDouble(dr.Rows[0]["Rent"]) / oldDays;
                    // 'RoomAdvanceTariff = RoundIt(dr.Rows[0]["Advance"] / dr.Rows[0]["NoOfRooms"] / dr.Rows[0]["Days"], 2)
                    nudAdvance.Value = Convert.ToDecimal(dr.Rows[0]["Advance"]);
                    isonline = Convert.ToInt32(dr.Rows[0]["IsOnline"]);

                    nudRentPaid.Value = Convert.ToDecimal(dr.Rows[0]["Rent"]);

                    txtNoOfRooms.Text = dr.Rows[0]["NoOfRooms"].ToString();
                    txtmobno.Text = dr.Rows[0]["MOB_NO"].ToString();
                    Advance = Convert.ToDouble(dr.Rows[0]["Advance"]);
                    /// RoomRentPerday = RoundIt(dr.Rows[0]["Rent"] / dr.Rows[0]["NoOfRooms"] / dr.Rows[0]["Days"], 2)
                    // nudRentPaid.Value = dr.Rows[0]["Rent"]
                    nudRent.Value = Convert.ToDecimal(dr.Rows[0]["Rent"]);
                    cmbDays.Tag = dr.Rows[0]["Days"];
                    SublocId = Convert.ToInt64(dr.Rows[0]["SUBLOC_ID"]);
                    SublocAs = dr.Rows[0]["SUBLOCATION"].ToString();
                    cmbDays.Text = dr.Rows[0]["Days"].ToString();
                    cmbday = Convert.ToInt32(dr.Rows[0]["Days"]);
                    // 'code commented by girish
                    // ''' oldDays = Convert.ToInt16(dr.Rows[0]["Days"])
                    /// rent = dr.Rows[0]["Rent"] / oldDays
                    // dtpCheckOut.Value = dr.Rows[0]["Outdate"]
                    // dtpCheckOut.Value = dtpCheckIn.Value.AddDays(Val(txtDays.Text & vbNullString))
                    dtpCheckOut.Value = Convert.ToDateTime(dr.Rows[0]["OutDate"]);
                    dtpCheckOutTime.Value = Convert.ToDateTime(dr.Rows[0]["OutTime"]);

                    // ''''Advance = dr("Advance") / oldDays

                    // DateDiff(DateInterval.Day, dtpCheckIn.Value, dtpCheckOut.Value)
                    FillSublocations();
                    cboSublocation.Text = SublocAs;
                    int index;
                    index = cboSublocation.FindString(SublocAs);
                    cboSublocation.SelectedItem = SublocId;
                    // cboSublocation.SelectedIndex = index
                    dr = RoomChangeDALobj.GetDrRoomCheckInDet(Convert.ToInt64(txtVchNo.Tag));
                    cf.FillListBox(chkRooms_given, dr, "RoomName", "RoomId", "RecordModifiedCount");

                    donnerallowed();   // added by Roshan/Payal

                    for (i = 0; i <= chkRooms_given.Items.Count - 1; i++)
                        chkRooms_given.SetItemChecked(i, true);

                    if (mScreenID == eScreenID.RoomChange)
                    {
                        FillAvailableRooms();
                        cboSublocation.Enabled = true;
                        cmbDays.Enabled = false;
                    }
                    else
                    {
                        cboSublocation.Enabled = false;
                        cmbDays.Enabled = true;
                    }

                    for (i = 0; i <= chkRooms_given.Items.Count - 1; i++)
                    {
                        // chklockers_given.Items.CopyTo(chkLockers, i)
                        chkRooms.Items.Insert(i, chkRooms_given.Items[i]);
                        chkRooms.SetItemChecked(i, true);
                    }


                    // If DateDiff(DateInterval.Hour, dtpCheckOutTime.Value, dtpCheckInTime.Value) >= 24 Then
                    // txtDays.Text = Val(txtDays.Text) + 1
                    // End If


                    // If dtpCheckOutTime.Value > dtpCheckInTime.Value Then
                    // txtDays.Text = Val(txtDays.Text) + 1
                    // End If
                    // nudAdvance.Value = 0
                    // nudRent.Value = 0




                    // dr = objDsLockerCheckOutDet.GetDrLockerCheckInDet(txtVchNo.Tag)

                    // strSQL.Append("CHECK_OUT_DET_ID AS LockerCheckOutDetId, ")
                    // strSQL.Append("CHECK_OUT_MST_ID AS LockerCheckOutMstId, ")
                    // strSQL.Append("L.LOCKER_ID AS LockerId, ")
                    // strSQL.Append("L.LOCKER_NAME AS LockerName, ")
                    // strSQL.Append("L.STATUS AS Status, ")
                    // strSQL.Append("L.AVAILABLE_STATUS AS AvailableStatus,, ")
                    // strSQL.Append("ISNULL(L.Record_Modified_Count,0) AS RecordModifiedCount ")

                    // While dr.Read
                    // chkLockers.Items.Add(New OSOL_ADMIN.clsItemData(dr.Item("LockerName"), dr.Item("LockerId"), dr.Item("RecordModifiedCount")))
                    // End While
                    if (mScreenID == eScreenID.RoomExtend)
                        chkRooms.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Receipt No. not found." + txtVchNo.Text, PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //dr.Close();
                    txtVchNo.Text = "";
                    txtVchNo.Focus();
                }
            }
            catch (Exception ex)
            {
                //if (!dr == null)
                //    dr.Close();
                MessageBox.Show("Load Check In Details failed.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void donnerallowed()
        {
            System.Data.DataTable dr;
            // Added by Roshan               
            try
            {
                dr = objDsRoomMst.GetDaysForDonnerextend(Convert.ToInt64(txtVchNo.Text), Convert.ToInt64(txtVchNo.Tag));
                if (dr.Rows.Count > 0)
                    DnrAllowedDays1 = Convert.ToInt32(dr.Rows[0]["NoOfDays"]);
            }
            catch (Exception ex)
            {
            }
        }
        private void txtDays_KeyUp(System.Object sender, System.Windows.Forms.KeyEventArgs e)
        {
            int NewDays = 0;
            int MaxDays = 0;
            System.Data.DataTable dr;

            if (cmbDays.Text != "")
            {
                NewDays = Convert.ToInt16(cmbDays.Text);
                if (NewDays < oldDays)
                    cmbDays.Text = oldDays.ToString();
            }

            dr = objBlsRoomCheckIn.GetDaysLimit();
            if (dr.Rows.Count > 0)
                MaxDays = Convert.ToInt32(dr.Rows[0]["EXPIRY_DAYS"]);
            if (NewDays > MaxDays)
            {
                MessageBox.Show("Days Are Not Acceptable!", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbDays.Text = oldDays.ToString();
            }
        }

        private void txtVchNo_KeyPress(System.Object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            double RoomAdvanceTariff1;
            double RoomRentPerday1;
            if (e.KeyChar == (char)Keys.Enter)
            {
                System.Data.DataTable dr;
                Int16 i;
                // Dim strApp As String
                string strRcptNo;
                if (txtVchNo.Text == "")
                {
                    mStrErrMsg = new string[1];
                    mStrErrMsg[0] = "Rcpt No";
                    ShowValidateError(txtVchNo, 0, mStrErrMsg, 1);
                    return;
                }

                strRcptNo = txtVchNo.Text;
                FormClear();
                // fncSetDateAndRange(dtpCheckOut)
                // fncSysTime(dtpCheckOutTime)

                txtVchNo.Text = strRcptNo;

                try
                {
                    // dr = objDsLockerChange.GetDrLockerChangeMst(, , , txtCounter.Tag, UserInfo.CompanyID, UserInfo.LocationID, LockerCheckOutDeptID, , , Val(txtAppNo.Text & vbNullString))
                    dr = RoomChangeDALobj.GetDrRoomChangeMst(0, "", strRcptNo, Convert.ToInt64(txtCounter.Tag), UserInfo.CompanyID, PrintReceiptLocId, RoomCheckOutDeptID, 0, "");

                    if (dr.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dr.Rows[0]["PendRoomCount"]) == 0)
                        {
                            MessageBox.Show("No pending Rooms against the Receipt No. " + txtVchNo.Text, PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtVchNo.Text = "";
                            txtVchNo.Focus();
                            return;
                        }
                        txtVchNo.Tag = dr.Rows[0]["CheckInMstId"];
                        dtpCheckIn.Value = Convert.ToDateTime(dr.Rows[0]["InDate"]);
                        dtpCheckInTime.Value = Convert.ToDateTime(dr.Rows[0]["InTime"]);
                        txtVchNo.Text = dr.Rows[0]["SerialNo"].ToString();
                        dtpCheckIn.Tag = dr.Rows[0]["RecordModifiedCount"];
                        BhaktTypeId = Convert.ToInt32(dr.Rows[0]["BhaktType"]);
                        NameRoomHolder = dr.Rows[0]["Name"].ToString();
                        txtName.Text = NameRoomHolder;
                        PlaceRoomHolder = dr.Rows[0]["Place"].ToString();
                        txtPlace.Text = PlaceRoomHolder;
                        oldDays = Convert.ToInt32(dr.Rows[0]["Days"]);
                        rent = Convert.ToDouble(dr.Rows[0]["Rent"]) / (double)oldDays;
                        nudAdvance.Value = Convert.ToDecimal(dr.Rows[0]["Advance"]);
                        isonline = Convert.ToInt32(dr.Rows[0]["IsOnline"]);
                        txtNoOfRooms.Text = dr.Rows[0]["NoOfRooms"].ToString();
                        txtmobno.Text = dr.Rows[0]["MOB_NO"].ToString();
                        txtmobno.Tag = dr.Rows[0]["BARCODE"];
                        Advance = Convert.ToDouble(dr.Rows[0]["Advance"]);
                        nudRentPaid.Value = Convert.ToDecimal(dr.Rows[0]["Rent"]);
                        nudRent.Value = Convert.ToDecimal(dr.Rows[0]["Rent"]);
                        cmbDays.Tag = dr.Rows[0]["Days"];
                        SublocId = Convert.ToInt64(dr.Rows[0]["SUBLOC_ID"]);
                        SublocAs = dr.Rows[0]["SUBLOCATION"].ToString();
                        cmbDays.Text = dr.Rows[0]["Days"].ToString();
                        cmbday = Convert.ToInt32(dr.Rows[0]["Days"]);
                        dtpCheckOut.Value = Convert.ToDateTime(dr.Rows[0]["OutDate"]);
                        dtpCheckOutTime.Value = Convert.ToDateTime(dr.Rows[0]["OutTime"]);

                        FillSublocations();
                        cboSublocation.Text = SublocAs;
                        cboSublocation.SelectedItem = SublocId;


                        dr = RoomChangeDALobj.GetDrRoomCheckInDet(Convert.ToInt64(txtVchNo.Tag));


                        cf.FillListBox(chkRooms_given, dr, "RoomName", "RoomId", "RecordModifiedCount");

                        for (i = 0; i <= chkRooms_given.Items.Count - 1; i++)
                            chkRooms_given.SetItemChecked(i, true);

                        if (mScreenID == eScreenID.RoomChange)
                        {
                            FillAvailableRooms();
                            cboSublocation.Enabled = true;
                            cmbDays.Enabled = false;
                        }
                        else
                            cboSublocation.Enabled = false;

                        for (i = 0; i <= chkRooms_given.Items.Count - 1; i++)
                        {
                            chkRooms.Items.Insert(i, chkRooms_given.Items[i]);
                            chkRooms.SetItemChecked(i, true);
                        }

                        donnerallowed();

                        if (mScreenID == eScreenID.RoomExtend)
                            chkRooms.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Receipt No. not found." + txtVchNo.Text, PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtVchNo.Text = "";
                        txtVchNo.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Load Check In Details failed.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void cmbDays_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
            dtpCheckOut.Value = dtpCheckIn.Value.AddDays(Convert.ToDouble(cmbDays.Text));
            showRent();
        }
    }

}
