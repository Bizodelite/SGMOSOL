using CrystalDecisions.ReportAppServer;
using Microsoft.VisualBasic;
using SGMOSOL.SCREENS;
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
using SGMOSOL.DAL.BhaktNiwas;
using static SGMOSOL.BAL.BhaktNiwasBAL;
using SGMOSOL.BAL;


namespace SGMOSOL.SCREENS.BhaktNiwas
{
    public partial class frmRoomCheckOut : Form
    {
        private eScreenID mScreenID;
        private bool mBlnEdit = false;
        private eAction mAction;
        private ArrayList CtrlArr = new ArrayList(); 
        private ArrayList btnArr = new ArrayList();

        private RoomCheckInDAL RoomCheckInDALobj = new RoomCheckInDAL();
        private bool DisableSendKeys;

        private bool bkDateEntry = false;
        private bool blnformChange;
        private string[] col;
        private string[] mStrErrMsg;
        public long mSearchId;
        private Collection mDelColl = new Collection();
        private DataTable TempTable1;
        private DataTable TempTable2;
        private System.Data.DataSet ds = new System.Data.DataSet();
        private DataRow MyRow;
        private DateTime dtEnteredOn;
        private double RoomTotalRent;
        private string mStrCounterMachineShortName;
        private int RoomCheckOutDeptID;
        private int RoomCheckInLockID;

        private string RoomCheckOutDeptName;
        private string RoomCheckOutLocName;
        private string NameRoomHolder;
        private string PlaceRoomHolder;
        private long countedDays;
        private long oldDays;
        private bool Eprint;
        CommonFunctions cf;
        RoomCheckOutDAL RoomCheckOutDALobj;
        public frmRoomCheckOut(eScreenID ScreenID)
        {
            InitializeComponent();
            mScreenID = ScreenID;
            this.Closing += new CancelEventHandler(this.frmRoomCheckOut_Closing);
            cf = new CommonFunctions();
            RoomCheckOutDALobj = new RoomCheckOutDAL();
        }
        private void btnNew_Click(System.Object sender, System.EventArgs e)
        {
            DataTable dr;
            FormClear();
            cf.fncSysTime(dtpCheckOutTime);
            cf.fncSetDateAndRange(dtpCurDate);
            cf.fncSysTime(dtpCurTime);
            cf.fncSysTime(dtpCheckOutTime);
            mAction = eAction.ActionInsert;
            cf.subLockForm(false, CtrlArr, false);
            btnSave.Enabled = btnNew.Enabled;
            btnLoad.Enabled = true;
            chkRooms.Enabled = true;
            try
            {
                dr = RoomCheckOutDALobj.GetDrMaxSrNo(Convert.ToInt64(txtCounter.Tag), UserInfo.CompanyID, RoomCheckInLockID, 0, UserInfo.fy_id);
                if (dr.Rows.Count > 0)
                    txtVchNo.Text = (Convert.ToInt32(dr.Rows[0]["SERIAL_NO"]) + 1).ToString();
                else
                    txtVchNo.Text = "1";
            }
            catch (Exception ex)
            {
            }

            txtCheckInVchNo.Focus();
            blnformChange = false;
            if (mAction == eAction.ActionInsert)
                btnPrint.Enabled = false;
            else
                btnPrint.Enabled = Convert.ToBoolean(Interaction.IIf(Eprint, true, false));
        }

        private void frmRoomCheckOut_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter & !DisableSendKeys)
                SendKeys.Send("{tab}");
            if (e.KeyCode == Keys.End & (mAction == eAction.ActionInsert | mAction == eAction.ActionUpdate) & blnformChange)
                btnSave_Click(null, null);
        }

        private void frmRoomCheckOut_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End & (mAction == eAction.ActionInsert | mAction == eAction.ActionUpdate) & blnformChange)
                btnSave_Click(null, null);
        }

        private void frmRoomCheckOut_Load(System.Object sender, System.EventArgs e)
        {
            dtpCurTime.Value = DateTime.Now;
            cf.setControlsonForm(this, CtrlArr, btnArr);
            cf.SetUserScreenActions(this, UserInfo.UserId, (int)mScreenID, btnArr, null, mBlnEdit);
            CtrlArr.Remove(dtpCurDate);
            CtrlArr.Remove(dtpCurTime);
            CtrlArr.Remove(dtpCheckIn);
            CtrlArr.Remove(dtpCheckInTime);
            CtrlArr.Remove(txtVchNo);
            CtrlArr.Remove(dtpCheckOut);
            CtrlArr.Remove(dtpCheckOutTime);
            CtrlArr.Remove(nudAdvance);
            CtrlArr.Remove(nudTotalRent);
            CtrlArr.Remove(NudRefund);
            CtrlArr.Remove(txtDays);
            CtrlArr.Remove(txtUser);
            CtrlArr.Remove(txtCounter);

            if (mScreenID == eScreenID.RoomCheckOutStar)
            {
                this.BackColor = Color.BlanchedAlmond;
                txtDays.Enabled = true;
                txtDays.ReadOnly = false;
                nudAdvance.ReadOnly = false;
                nudAdvance.Enabled = true;
            }

            txtUser.Text = UserInfo.UserName;
            FillCounter();

            CreateDs();
            if (btnNew.Enabled)
                btnNew_Click(null, null);
            else
                cf.subLockForm(true, CtrlArr, false);
            try
            {
                DataTable rstAction = cf.GetUserTotalAction(System.Convert.ToInt32(UserInfo.UserId), (Int64)eScreenID.RoomCheckIn);
                string strAction = string.Empty;
                foreach (DataRow drrstAction in rstAction.Rows)
                {
                    strAction = Interaction.IIf(strAction == string.Empty, drrstAction["ActionId"], strAction + "," + drrstAction["ActionId"]).ToString();
                }
                if (strAction.Contains("8"))
                {
                    Eprint = true;
                    btnPrint.Enabled = true;
                }
                else
                {
                    Eprint = false;
                    btnPrint.Enabled = false;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void FillCounter()
        {
            DataTable dr;
            dr = cf.GetDrCounterMachId(UserInfo.UserId, SystemHDDModelNo, SystemHDDSerialNo, SystemMacID, Convert.ToInt16(eModType.BhaktaNiwas));
            if (dr.Rows.Count > 0)
            {
                txtCounter.Text = dr.Rows[0]["CounterMachineTitle"].ToString();
                txtCounter.Tag = dr.Rows[0]["CtrMachId"];
                RoomCheckOutDeptID = Convert.ToInt32(dr.Rows[0]["DeptId"]);
                RoomCheckOutDeptName = dr.Rows[0]["DepartmentName"].ToString();
                RoomCheckOutLocName = dr.Rows[0]["LOC_FNAME"].ToString();
                RoomCheckInLockID = Convert.ToInt32(dr.Rows[0]["LocId"]);
                mStrCounterMachineShortName = dr.Rows[0]["CounterMachineShortName"].ToString();
            }
        }

        private void FormClear()
        {
            txtVchNo.Text = "";
            txtName.Text = "";
            txtPlace.Text = "";
            txtmobno.Text = "";
            txtVchNo.Tag = null;
            txtDays.Text = "";
            nudAdvance.Value = 0;
            nudTotalRent.Value = 0;
            NudRefund.Value = 0;
            chkRooms.Items.Clear();
        }

        private void btnSave_Click(System.Object sender, System.EventArgs e)
        {
            long lngError = -1;
            if (blnformChange == false)
                return;

            try
            {
                DataTable dr;
                dr = RoomCheckInDALobj.GetDrRoomCheckInMst(0, "", Convert.ToInt32(txtCheckInVchNo.Text), Convert.ToInt32(txtCounter.Tag), UserInfo.CompanyID, RoomCheckInLockID, RoomCheckOutDeptID, 0, "");
                if (dr.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dr.Rows[0]["PendRoomCount"]) == 0)
                    {
                        MessageBox.Show("No pending Rooms against the Receipt No. " + txtCheckInVchNo.Text, PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        txtCheckInVchNo.Text = "";
                        txtCheckInVchNo.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
            }

            if (fncSave())
            {
                blnformChange = false;
                btnNew_Click(null, null);
                blnformChange = false;
            }
        }
        private bool checkChkOutDays()
        {
            if (Convert.ToInt64(txtDays.Text) < oldDays)
            {
                if (mScreenID == eScreenID.RoomCheckOutBefore)
                    return true;
                else
                    return false;
            }
            else
                return true;
        }

        private bool fncSave()
        {
            long lngError = -1;
            RoomCheckOutMst RoomCheckOutMst;
            DataTable dr;
            Collection coll;
            long lngSerialNo;
            string strErr;
            string strcheckinNo;
            string strchkinRcptno;
            bool flag = true;

            if (IsValidForm() == false)
            {
                return false;
            }

            strchkinRcptno = txtCheckInVchNo.Text;
            try
            {
                dr = RoomCheckInDALobj.GetDrRoomCheckInMst(0, "", Convert.ToInt32(txtCheckInVchNo.Text), Convert.ToInt32(txtCounter.Tag), UserInfo.CompanyID, RoomCheckInLockID, RoomCheckOutDeptID, 0, "");
                if (dr.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dr.Rows[0]["PendRoomCount"]) == 0)
                    {
                        MessageBox.Show("No pending Rooms against the Receipt No. " + txtCheckInVchNo.Text, PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCheckInVchNo.Text = "";
                        txtCheckInVchNo.Focus();
                        return false;
                    }
                    oldDays = Convert.ToInt64(dr.Rows[0]["Days"]);
                }
                else
                {
                    MessageBox.Show("Receipt No. not found." + txtCheckInVchNo.Text, PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCheckInVchNo.Text = "";
                    txtCheckInVchNo.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Data kindly reload form." + ex.Message, PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (checkChkOutDays() == false)
            {
                MessageBox.Show("Check Out Days Not Completed for the Reciept No. " + txtCheckInVchNo.Text, PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            setCursor(this, false);
            strcheckinNo = txtCheckInVchNo.Text;
            lngSerialNo = Convert.ToInt64(txtVchNo.Text);
            RoomCheckOutMst = GetCheckOutMst();
            coll = GetCheckOutDetColl();
            if (mAction == eAction.ActionInsert)
            {
                lngError = RoomCheckOutDALobj.Insert(RoomCheckOutMst, coll, UserInfo.UserName, UserInfo.Machine_Name, lngSerialNo, dtEnteredOn);
                if (lngError > 0)
                    txtVchNo.Text = lngError.ToString();
            }
            else if (mAction == eAction.ActionUpdate)
                lngError = RoomCheckOutDALobj.Update(RoomCheckOutMst, coll, mDelColl, UserInfo.UserName, UserInfo.Machine_Name, lngSerialNo);
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
                
                if (Convert.ToInt32(txtBhaktType.Text) == 4)
                {
                    MethodforPrint();
                    txtCheckInVchNo.Text = "";
                }
                setCursor(this, true);
                flag = true;
                blnformChange = false;
                if (NudRefund.Value < nudAdvance.Value | NudRefund.Value > nudAdvance.Value)
                {
                    MethodforPrint();
                    txtCheckInVchNo.Text = "";
                }
            }
            return flag;
        }
        private void txtDays_TextChanged(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
            if (txtDays.Text != "" & oldDays != 0)
            {
                nudTotalRent.Value = Convert.ToDecimal((RoomTotalRent / oldDays) * Convert.ToDouble(txtDays.Text));
                NudRefund.Value = nudAdvance.Value;
            }
        }
        private RoomCheckOutMst GetCheckOutMst()
        {
            RoomCheckOutMst CheckOutMst = new RoomCheckOutMst();
            DataTable dr;
            CheckOutMst.CheckOutMstId = Convert.ToInt64(txtVchNo.Tag);
            CheckOutMst.ComId = UserInfo.CompanyID;
            CheckOutMst.LocId = RoomCheckInLockID;
            CheckOutMst.DeptId = RoomCheckOutDeptID;
            CheckOutMst.CtrMachId = Convert.ToInt64(txtCounter.Tag);
            CheckOutMst.FyId = UserInfo.fy_id;
            CheckOutMst.OutDate = dtpCurDate.Value;
            CheckOutMst.OutTime = dtpCurTime.Value;
            CheckOutMst.SerialNo = Convert.ToInt64(txtVchNo.Tag);
            CheckOutMst.CheckInMstId = Convert.ToInt64(txtCheckInVchNo.Tag);
            CheckOutMst.Days = Convert.ToInt32(txtDays.Text);
            CheckOutMst.NoOfRooms = chkRooms.CheckedItems.Count;
            CheckOutMst.Advance = nudAdvance.Value;
            CheckOutMst.Rent = nudTotalRent.Value;
            CheckOutMst.BhaktTypeId = Convert.ToInt32(txtBhaktType.Text);
            if (CheckOutMst.BhaktTypeId == 4 | CheckOutMst.BhaktTypeId == 5)
            {
                CheckOutMst.DonerId = Convert.ToInt32(txtDonerId.Text);
                CheckOutMst.DnrRoomId = RoomCheckOutDALobj.GetrDonnerRoomId(CheckOutMst.CheckInMstId);
            }

            CheckOutMst.Refund = NudRefund.Value;
            CheckOutMst.UserId = UserInfo.UserId;
            CheckOutMst.ServerName = UserInfo.serverName;
            CheckOutMst.EnteredBy = UserInfo.UserName;
            CheckOutMst.ModifiedBy = UserInfo.UserName;
            CheckOutMst.RecordModifiedCount = Convert.ToInt64(dtpCheckOut.Tag) + 1;
            return CheckOutMst;
        }


        private Collection GetCheckOutDetColl()
        {
            Collection coll = new Collection();
            RoomCheckOutDet CheckOutDet = new RoomCheckOutDet();
            CheckOutDet.CheckOutMstId = Convert.ToInt64(txtVchNo.Tag);
            for (var i = 0; i <= chkRooms.Items.Count - 1; i++)
            {
                if (chkRooms.GetItemChecked(i))
                {
                    CheckOutDet.LockerId = cf.lsbItemData(chkRooms, i);
                    CheckOutDet.LockerAvailableStatus = (int)eTokenDetail.StatusYes;
                    CheckOutDet.LockerRecordModifiedCount = Convert.ToInt64(cf.lsbItemName2(chkRooms, i)) + 1;
                    coll.Add(CheckOutDet);
                }
            }
            return coll;
        }

        private bool IsValidForm()
        {
            if (txtCheckInVchNo.Text == "")
            {
                mStrErrMsg = new string[1];
                mStrErrMsg[0] = "Rcpt No";
                return ShowValidateError(txtCheckInVchNo, 0, mStrErrMsg, 1);
            }


            if (chkRooms.CheckedItems.Count == 0)
            {
                mStrErrMsg = new string[1];
                mStrErrMsg[0] = "Lockers";
                MessageBox.Show("Please select Lockers", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
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

            setCursor(this, false);
            frmSearchNew form1 = new frmSearchNew("BN_ROOM_CHECK_OUT_MST_T_FIND_V", true, eModType.BhaktaNiwas);
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
                btnLoad.Enabled = false;
                if (mBlnEdit & !blnFormLock)
                {
                    mAction = eAction.ActionUpdate;
                    cf.subLockForm(false, CtrlArr, false);
                    btnPrint.Enabled = Convert.ToBoolean(Interaction.IIf(Eprint, true, false));
                }
                else
                {
                    mAction = eAction.ActionLocked;
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
            btnSave.Enabled = false;
            btnPrint.Enabled = Convert.ToBoolean(Interaction.IIf(Eprint, true, false));
            setCursor(this, true);
        }

        private bool LoadTransaction(long lngSearchId, ref bool blnFormLock)
        {
            DataTable dr;
            RoomCheckOutDet CheckOutDet = new RoomCheckOutDet();
            int ctr = 0;
            bool blnFlag = false;
            mDelColl = new Collection();
            FormClear();
            try
            {
                dr = RoomCheckOutDALobj.GetDrRoomCheckOutMst(lngSearchId);
                if (dr.Rows.Count > 0)
                {
                    dtpCheckIn.Value = Convert.ToDateTime(dr.Rows[0]["InDate"]);
                    dtpCheckInTime.Value = Convert.ToDateTime(dr.Rows[0]["InTime"]);
                    txtCheckInVchNo.Text = dr.Rows[0]["InSerialNo"].ToString();
                    txtCheckInVchNo.Tag = dr.Rows[0]["CheckInMstId"];
                    txtDays.Tag = dr.Rows[0]["InDays"];
                    dtpCheckIn.Tag = dr.Rows[0]["InRecordModifiedCount"];
                    txtmobno.Text = dr.Rows[0]["MOBILE"].ToString();
                    NameRoomHolder = dr.Rows[0]["Name"].ToString();
                    PlaceRoomHolder = dr.Rows[0]["Place"].ToString();
                    txtName.Text = NameRoomHolder;
                    txtPlace.Text = PlaceRoomHolder;
                    txtVchNo.Text = (dr.Rows[0]["SerialNo"]).ToString();
                    txtVchNo.Tag = lngSearchId;
                    dtpCheckOut.Tag = dr.Rows[0]["RecordModifiedCount"];
                    txtDays.Text = dr.Rows[0]["Days"].ToString();
                    dtpCheckOut.Value = Convert.ToDateTime(dr.Rows[0]["OutDate"]);
                    dtpCheckOutTime.Value = Convert.ToDateTime(dr.Rows[0]["OutTime"]);
                    nudAdvance.Value = Convert.ToDecimal(dr.Rows[0]["Advance"]);
                    nudTotalRent.Value = Convert.ToDecimal(dr.Rows[0]["Rent"]);

                    nudAdvance.Tag = dr.Rows[0]["InAdvance"];
                    nudTotalRent.Tag = dr.Rows[0]["InRent"];

                    NudRefund.Value = Convert.ToDecimal(dr.Rows[0]["Refund"]);
                    dtEnteredOn = Convert.ToDateTime(dr.Rows[0]["EnteredOn"]);
                }


                dr = RoomCheckOutDALobj.GetDrRoomCheckOutDet(lngSearchId, true, Convert.ToInt64(txtCounter.Tag));
                mDelColl = new Collection();
                CheckOutDet = new RoomCheckOutDet();
                CheckOutDet.CheckOutMstId = Convert.ToInt64(txtVchNo.Tag);
                foreach (DataRow drrow in dr.Rows)
                {
                    if ((Convert.ToInt32(drrow["RoomStatus"]) != (int)eTokenDetail.StatusActive || Convert.ToInt32(drrow["AvailableStatus"]) != (int)eTokenDetail.StatusYes) && Convert.ToInt32(drrow["RoomCheckOutDetId"]) > 0)
                        blnFormLock = true;

                    if (drrow["RoomName"].ToString() != "")
                        chkRooms.Items.Add(new clsItemData(drrow["RoomName"].ToString(), Convert.ToInt32(drrow["RoomId"]), drrow["RecordModifiedCount"].ToString()));
                    if (Convert.ToInt32(drrow["RoomCheckOutDetId"]) > 0 && drrow["RoomName"].ToString() != "")
                    {
                        ctr = ctr + 1;
                        chkRooms.SetItemChecked(ctr - 1, true);
                        CheckOutDet.LockerId = Convert.ToInt32(drrow["RoomId"]);
                        CheckOutDet.LockerAvailableStatus = (int)eTokenDetail.StatusNo;
                        CheckOutDet.LockerRecordModifiedCount = Convert.ToInt32(drrow["RecordModifiedCount"]) + 1;
                        CheckOutDet.CheckOutDetId = Convert.ToInt64(drrow["RoomCheckOutDetId"]);
                        CheckOutDet.CheckOutMstId = Convert.ToInt64(drrow["RoomCheckOutMstId"]);
                        mDelColl.Add(CheckOutDet);
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

        private void frmRoomCheckOut_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message, MsgBoxStyle.Information, PrjMsgBoxTitle);
            }
        }

        private void CalculateAmt()
        {
            double dblAmt = 0;
        }

        private void MethodforPrint()
        {
            string strReportName;
            Form sForm;
            Collection pColl = new Collection();
            setCursor(this, false);
            System.Drawing.Printing.PrintDocument printDoc = new System.Drawing.Printing.PrintDocument();

            strReportName = "RoomCheckOutReceipt.rpt";
            FillDataInDataset();
            sForm = new frmCrystalViewer(UserInfo.ReportPath + strReportName, null, ds, null, pColl, eScreenID.RoomCheckOut, true);
            sForm.Text = "Room Check Out : " + eReportID.RoomCheckOut;
            sForm.Show();
            sForm.Close();
        }

        private void btnPrint_Click(System.Object sender, System.EventArgs e)
        {
            string strReportName;
            Form sForm;
            Collection pColl = new Collection();
            setCursor(this, false);
            System.Drawing.Printing.PrintDocument printDoc = new System.Drawing.Printing.PrintDocument();

            strReportName = "RoomCheckOutReceiptDup.rpt";
            sForm = new frmCrystalViewer(UserInfo.ReportPath + strReportName, null, ds, null, pColl, eScreenID.RoomCheckOut, true);
            sForm.Text = "Room Check Out : " + eReportID.RoomCheckOut;
            sForm.Show();
            sForm.Close();
        }

        private void FillDataInDataset()
        {
            Int16 i;

            TempTable1.Rows.Clear();
            TempTable2.Rows.Clear();

            MyRow = TempTable1.NewRow();
            MyRow["CHECK_OUT_MST_ID"] = 1;
            MyRow["LOC_SH_NAME"] = RoomCheckOutLocName;
            MyRow["DEPT_SH_NAME"] = RoomCheckOutDeptName;
            MyRow["COUNTER"] = mStrCounterMachineShortName;
                 
            MyRow["IN_DATE"] = dtpCheckIn.Value;
            MyRow["IN_TIME"] = dtpCheckInTime.Value;
            MyRow["SERIAL_NO"] = (txtVchNo.Text);
            MyRow["APP_NO"] = "0";
                 
            MyRow["NAME"] = NameRoomHolder;
            MyRow["PLACE"] = PlaceRoomHolder;
            MyRow["DAYS"] = txtDays.Text;
                 
            MyRow["NO_OF_ROOMS"] = chkRooms.CheckedItems.Count;
            MyRow["OUT_DATE"] = dtpCurDate.Value;
            MyRow["OUT_TIME"] = dtpCurTime.Value;
                 
            MyRow["ADVANCE"] = nudAdvance.Value;
            MyRow["RENT"] = nudTotalRent.Value;
            MyRow["REFUND"] = NudRefund.Value;
                 
            MyRow["USER_NAME"] = txtUser.Text;
            MyRow["SERVER_NAME"] = UserInfo.serverName;
            MyRow["MACHINE_NAME"] = UserInfo.Machine_Name;
            MyRow["AMT_IN_WORDS"] = cf.getNumbersInWords(nudAdvance.Value, eCurrencyType.Rupees);


            TempTable1.Rows.Add(MyRow);
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
            MyRow["CHECK_OUT_MST_ID"] = 1;
            MyRow["ROOM_NAME"] = Roomname;
            TempTable2.Rows.Add(MyRow);
        }

        private void CreateDs()
        {
            TempTable1 = new DataTable("BN_ROOM_CHECK_OUT_MST_T");
            TempTable2 = new DataTable("BN_ROOM_CHECK_OUT_DET_T");

            TempTable1.Columns.Add("CHECK_OUT_MST_ID", System.Type.GetType("System.Int64"));
            TempTable1.Columns.Add("LOC_SH_NAME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("DEPT_SH_NAME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("COUNTER", System.Type.GetType("System.String"));

            TempTable1.Columns.Add("IN_DATE", System.Type.GetType("System.DateTime"));
            TempTable1.Columns.Add("IN_TIME", System.Type.GetType("System.DateTime"));
            TempTable1.Columns.Add("SERIAL_NO", System.Type.GetType("System.Int64"));
            TempTable1.Columns.Add("APP_NO", System.Type.GetType("System.String"));

            TempTable1.Columns.Add("NAME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("PLACE", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("DAYS", System.Type.GetType("System.Int32"));

            TempTable1.Columns.Add("NO_OF_ROOMS", System.Type.GetType("System.Int32"));
            TempTable1.Columns.Add("OUT_DATE", System.Type.GetType("System.DateTime"));
            TempTable1.Columns.Add("OUT_TIME", System.Type.GetType("System.DateTime"));

            TempTable1.Columns.Add("ADVANCE", System.Type.GetType("System.Double"));
            TempTable1.Columns.Add("RENT", System.Type.GetType("System.Double"));
            TempTable1.Columns.Add("REFUND", System.Type.GetType("System.Double"));

            TempTable1.Columns.Add("USER_NAME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("SERVER_NAME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("MACHINE_NAME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("AMT_IN_WORDS", System.Type.GetType("System.String"));

            TempTable2.Columns.Add("CHECK_OUT_MST_ID", System.Type.GetType("System.Int64"));
            TempTable2.Columns.Add("ROOM_NAME", System.Type.GetType("System.String"));

            ds.Tables.Add(TempTable1);
            ds.Tables.Add(TempTable2);
        }

        private void txtDays_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtVchNo_TextChanged(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
        }

        private void chkRooms_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
        }

        private bool ShowValidateError(Control myObject, int tabIndex, string[] ErrMsg, int ErrNo)
        {
            setCursor(this, true);
            MessageBox.Show(cf.GetErrorMessage(ErrMsg, ErrNo), PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            myObject.Focus();
            return false;
        }

        private void btnLoad_Click(System.Object sender, System.EventArgs e)
        {
            DataTable dr;
            long lngError = -1;
            string strRcptNo;
            string strchkinRcptno;
            long DiffHour;
            long DiffDays;
            long RemHor;

            if (txtCheckInVchNo.Text == "")
            {
                mStrErrMsg = new string[1];
                mStrErrMsg[0] = "Rcpt No";
                ShowValidateError(txtCheckInVchNo, 0, mStrErrMsg, 1);
                return;
            }
            btnNew_Click(null, null);
            strchkinRcptno = txtCheckInVchNo.Text;
            strRcptNo = txtVchNo.Text;
            FormClear();
            cf.fncSysTime(dtpCheckOutTime);
            txtVchNo.Text = strRcptNo;


            try
            {
                dr = RoomCheckInDALobj.GetDrRoomCheckInMst(0, "", Convert.ToInt32(strchkinRcptno), Convert.ToInt32(txtCounter.Tag), UserInfo.CompanyID, RoomCheckInLockID, RoomCheckOutDeptID, 0, "");

                if (dr.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dr.Rows[0]["PendRoomCount"]) == 0)
                    {
                        MessageBox.Show("No pending Rooms against the Receipt No. " + txtCheckInVchNo.Text, PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCheckInVchNo.Text = "";
                        txtCheckInVchNo.Focus();
                        return;
                    }

                    txtCheckInVchNo.Tag = dr.Rows[0]["CheckInMstId"];
                    dtpCheckIn.Value = Convert.ToDateTime(dr.Rows[0]["InDate"]);
                    dtpCheckInTime.Value = Convert.ToDateTime(dr.Rows[0]["InTime"]);
                    dtpCheckOut.MaxDate = Convert.ToDateTime(dr.Rows[0]["MaxDate"]);
                    dtpCheckOut.Value = Convert.ToDateTime(dr.Rows[0]["OutDate"]);
                    dtpCheckOutTime.Value = Convert.ToDateTime(dr.Rows[0]["OutTime"]);
                    txtCheckInVchNo.Text = (dr.Rows[0]["SerialNo"]).ToString();
                    txtmobno.Text = dr.Rows[0]["MOB_NO"].ToString();
                    dtpCheckIn.Tag = dr.Rows[0]["RecordModifiedCount"];

                    NameRoomHolder = dr.Rows[0]["Name"].ToString();
                    PlaceRoomHolder = dr.Rows[0]["Place"].ToString();
                    txtName.Text = NameRoomHolder;
                    txtPlace.Text = PlaceRoomHolder;

                    RoomTotalRent = Convert.ToDouble(dr.Rows[0]["Rent"]);

                    txtBhaktType.Text = dr.Rows[0]["BHAKT_TYPE"].ToString();
                    if (Convert.ToInt32(txtBhaktType.Text) == 4 || Convert.ToInt32(txtBhaktType.Text) == 5)
                        txtDonerId.Text = dr.Rows[0]["Donner_Id"].ToString();
                    DiffHour = Convert.ToInt64(dr.Rows[0]["CALC_HOUR"]);
                    if (DiffHour <= (24 * 60))
                        txtDays.Text = 1.ToString();
                    else
                    {
                        txtDays.Text = Math.Ceiling(DiffHour / (double)(24 * 60)).ToString();
                        if (((DiffHour % (24 * 60)) > 0 & (DiffHour % (24 * 60)) <= (4 * 60)))
                            txtDays.Text = (Convert.ToInt32(txtDays.Text) - 1).ToString();
                    }

                    countedDays = Convert.ToInt64(txtDays.Text);

                    oldDays = Convert.ToInt64(dr.Rows[0]["Days"]);
                    nudTotalRent.Value = Convert.ToDecimal((RoomTotalRent / oldDays) * Convert.ToDouble(txtDays.Text));
                    nudAdvance.Value = Convert.ToDecimal(dr.Rows[0]["Advance"]);

                    NudRefund.Value = Convert.ToDecimal(Convert.ToDecimal(dr.Rows[0]["Advance"]) + (Convert.ToDecimal(RoomTotalRent) - (nudTotalRent.Value)));


                    dr = RoomCheckInDALobj.GetDrRoomCheckInDet(Convert.ToInt64(txtCheckInVchNo.Tag));


                    cf.FillListBox(chkRooms, dr, "RoomName", "RoomId", "RecordModifiedCount");
                    for (var i = 0; i <= chkRooms.Items.Count - 1; i++)
                        chkRooms.SetItemChecked(i, true);
                }
                else
                {
                    MessageBox.Show("Receipt No. not found." + txtCheckInVchNo.Text, PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCheckInVchNo.Text = "";
                    txtCheckInVchNo.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load Check In Details failed ." + ex.Message, PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtCheckInVchNo_TextChanged(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
        }

        private void dtpCurDate_ValueChanged(System.Object sender, System.EventArgs e)
        {
            if (txtCheckInVchNo.Text != "")
                btnLoad_Click(null, null);
        }
    }
}
