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
using System.Runtime.InteropServices;
using System.Collections.ObjectModel;
using System.Collections;
using SGMOSOL.ADMIN;
using static SGMOSOL.ADMIN.CommonFunctions;
using System.Data.SqlClient;
using SGMOSOL.DAL;
using System.Data.Metadata.Edm;
using static SGMOSOL.BAL.LockerBAL;
using SGMOSOL.BAL;
using Microsoft.VisualBasic;

namespace SGMOSOL.SCREENS
{
    public partial class frmLockerCheckIn : Form
    {
        private bool mBlnEdit = false;
        private eAction mAction;
        private ArrayList CtrlArr = new ArrayList();
        private ArrayList btnArr = new ArrayList();
        LockerCheckInDAL LockerCheckInDALobj = new LockerCheckInDAL();
        LockerMasterDAL LockerMasterDALobj = new LockerMasterDAL();
        //private OSOL_ADMIN.clsDsCommon mClsDsCom = new OSOL_ADMIN.clsDsCommon();
        //private OSOL_BLSDS.clsBlsLockerCheckIn objBlsLockerCheckIn = new OSOL_BLSDS.clsBlsLockerCheckIn();
        //private OSOL_BLSDS.clsDsLockerCheckInMst objDsLockerCheckInMst = new OSOL_BLSDS.clsDsLockerCheckInMst();
        //private OSOL_BLSDS.clsDsLockerCheckInDet objDsLockerCheckInDet = new OSOL_BLSDS.clsDsLockerCheckInDet();
        //private OSOL_BLSDS.clsDsLockerMaster objDsLockerMst = new OSOL_BLSDS.clsDsLockerMaster();
        private bool DisableSendKeys;

        private bool bkDateEntry = false;
        private bool blnformChange;
        private string[] col;
        private string[] mStrErrMsg;
        public long mSearchId;
        CommonFunctions CF = new CommonFunctions();
        private Collection mDelColl = new Collection();
        private DataTable TempTable1;
        //private DataTable TempTable2;
        //private OSOL23DataSet ds = new OSOL23DataSet();
        private DataRow MyRow;
        private DateTime dtEnteredOn;
        private double LockerAdvanceTariff;
        private double LockerRentPerday;
        private string mStrCounterMachineShortName;
        private int LockerCheckInDeptID;
        private string LockerCheckInDeptName;
        private string LockerCheckInLocName;
        private int PrintReceiptLocId;
        public frmLockerCheckIn()
        {
            InitializeComponent();
            this.Closing += new CancelEventHandler(this.frmLockerCheckIn_Closing);
        }

        private void btnNew_Click(System.Object sender, System.EventArgs e)
        {
            try
            {
                FormClear();
                CF.fncSetDateAndRange(dtpCheckIn);
                CF.fncSysTime(dtpCheckInTime);
                //fncSysTimePlus2(dtpCheckOutTime)
                CF.fncSysTime(dtpCheckOutTime);
                dtpCheckOut.Value = dtpCheckIn.Value;
                //dtpCheckOutTime.Value = dtpCheckInTime.Value
                dtpCheckIn.Enabled = bkDateEntry;
                mAction = eAction.ActionInsert;
                CF.subLockForm(false, CtrlArr, false);
                btnSave.Enabled = btnNew.Enabled;

                DataTable dr = LockerCheckInDALobj.GetDrMaxSrNo(Convert.ToInt64(txtCounter.Tag), UserInfo.CompanyID, Convert.ToInt64(PrintReceiptLocId), 0, UserInfo.fy_id);
                if (dr.Rows.Count > 0)
                    txtVchNo.Text = (Convert.ToInt32(dr.Rows[0]["SerialNo"].ToString()) + 1).ToString();
                else
                    txtVchNo.Text = "1";
                //dr.Close();

                FillAvailableLockers();
                txtLkrAvlbleCt.Text = chkLockers.Items.Count.ToString();
                chkLockers.Enabled = true;
                // txtAppNo.Focus()
                txtName.Focus();
                blnformChange = false;
                if (mAction == eAction.ActionInsert)
                    btnPrint.Enabled = false;
                else
                    btnPrint.Enabled = true;
            }
            catch (Exception ex)
            {
                CF.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
        }
        private void FillAvailableLockers()
        {
            try
            {
                var str = txtRoomSrch.Text;
                DataTable dr = LockerMasterDALobj.GetDrLockerDetails(PrintReceiptLocId, (int)eTokenDetail.StatusActive, (int)eTokenDetail.StatusYes, str);
                CF.FillListBox(chkLockers, dr, "LockerName", "LockerId", "RecordModifiedCount");
            }
            catch (Exception ex)
            {
                CF.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
        }
        private void frmLockerCheckIn_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter & !DisableSendKeys)
                SendKeys.Send("{tab}");
            if (e.KeyCode == Keys.End & (mAction == eAction.ActionInsert | mAction == eAction.ActionUpdate) & blnformChange)
                btnSave_Click(null, null);
        }
        private void frmLockerCheckIn_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End & (mAction == eAction.ActionInsert | mAction == eAction.ActionUpdate) & blnformChange)
                btnSave_Click(null, null);
        }

        private void frmLockerCheckIn_Load(System.Object sender, System.EventArgs e)
        {
            txtDays.Text = "1";
            CF.setControlsonForm(this, CtrlArr, btnArr);
            CF.SetUserScreenActions(this, UserInfo.UserId, (int)eScreenID.LockerCheckIn, btnArr, null, mBlnEdit);
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
            DataTable dr = new DataTable();
            try
            {
                dr = LockerMasterDALobj.GetDrLockerTariff();
                if (dr.Rows.Count > 0)
                {
                    LockerAdvanceTariff = Convert.ToDouble(dr.Rows[0]["Advance"]);
                    LockerRentPerday = Convert.ToDouble(dr.Rows[0]["RentPerDay"]);
                }
                //dr.Close();
            }
            catch (Exception ex)
            {
                //if (dr != null)
                //    dr.Close();
                CF.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            if (btnNew.Enabled)
                btnNew_Click(null, null);
            else
                CF.subLockForm(true, CtrlArr, false);
        }
        private void FillCounter()
        {
            DataTable dr;
            dr = CF.GetDrCounterMachId(UserInfo.UserId, SystemHDDModelNo, SystemHDDSerialNo, SystemMacID, (int)eModType.Locker);
            if (dr.Rows.Count > 0)
            {
                txtCounter.Text = dr.Rows[0]["CounterMachineTitle"].ToString();
                txtCounter.Tag = dr.Rows[0]["CtrMachId"];
                LockerCheckInDeptID = (int)dr.Rows[0]["DeptId"];
                LockerCheckInDeptName = dr.Rows[0]["DepartmentName"].ToString();
                PrintReceiptLocId = (int)dr.Rows[0]["LocId"];
                LockerCheckInLocName = dr.Rows[0]["LocName"].ToString();
                mStrCounterMachineShortName = dr.Rows[0]["CounterMachineShortName"].ToString();
            }
            //dr.Close();
        }

        private void FormClear()
        {
            txtVchNo.Text = "";
            txtVchNo.Tag = null;
            // txtAppNo.Text = ""
            txtName.Text = "";
            txtPlace.Text = "";
            txtDays.SelectedIndex = 0;
            txtmobno.Text = "";
            txtNoOfLockers.Text = "0";
            nudAdvance.Value = 0;
            nudRent.Value = 0;
            txtOldReceipt.Text = "";
            chkAllowFreeReceipt.Checked = false;
            txtOldReceipt.Visible = false;
            chkLockers.Items.Clear();
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
            setCursor(this, true);
        }

        private bool fncSave()
        {
            long lngError = -1;
            LockerCheckInMst LockerCheckInMst;
            Collection coll;
            long lngSerialNo;
            string strErr;
            bool flag = true;
            int MaxDays = 0;
            int MaxLockers;
            if (IsValidForm() == false)
            {
                return false;
            }
            setCursor(this, false);
            lngSerialNo = Convert.ToInt64(txtVchNo.Text);
            LockerCheckInMst = GetCheckInMst();
            MaxDays = LockerCheckInDALobj.GetDaysLimit();
            if (LockerCheckInMst.Days > MaxDays || LockerCheckInMst.Days == 0)
            {
                MessageBox.Show("Days Are Not Acceptable!", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDays.Text = "";
                return false;
            }
            MaxLockers = LockerCheckInDALobj.GetLockersLimit();
            if (LockerCheckInMst.NoOfLockers > MaxLockers)
            {
                MessageBox.Show("No.of Lockers are Exceeding!", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNoOfLockers.Text = "";
                for (int i = 0; i <= chkLockers.Items.Count - 1; i++)
                {
                    if (chkLockers.GetSelected(i))
                        chkLockers.SetItemChecked(i, false);
                }
                return false;
            }

            coll = GetCheckInDetColl();
            if (mAction == eAction.ActionInsert)
            {
                lngError = LockerCheckInDALobj.Insert(LockerCheckInMst, coll, UserInfo.UserName, UserInfo.Machine_Name, lngSerialNo, dtEnteredOn);
                if (lngError > 0)
                    txtVchNo.Text = lngError.ToString();
            }
            else if (mAction == eAction.ActionUpdate)
                lngError = LockerCheckInDALobj.Update(LockerCheckInMst, coll, mDelColl, UserInfo.UserName, UserInfo.Machine_Name, lngSerialNo);
            setCursor(this, true);

            if (lngError < 0)
            {
                strErr = ProcErr(lngError);
                col = new string[1];
                col[0] = "";
                if (strErr.Length == 0)
                    strErr = CF.GetErrorMessage(mStrErrMsg, 10);
                MessageBox.Show(strErr, PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                setCursor(this, true);
                flag = false;
            }
            else if (lngError >= 0)
            {
                col = new string[2];
                col[0] = "Receipt";
                col[1] = lngSerialNo.ToString();
                MessageBox.Show(CF.GetErrorMessage(col, 7), PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                setCursor(this, true);
                flag = true;

                // If Val(dtpCheckIn.Tag & vbNullString) = 0 Then
                blnformChange = false;
                btnPrint_Click(null, null);
            }
            return flag;
        }

        private LockerCheckInMst GetCheckInMst()
        {
            LockerCheckInMst CheckInMst = new LockerCheckInMst();
            CheckInMst.CheckInMstId = Convert.ToInt64(txtVchNo.Tag);
            CheckInMst.ComId = UserInfo.CompanyID;
            CheckInMst.LocId = PrintReceiptLocId;
            CheckInMst.DeptId = LockerCheckInDeptID;
            CheckInMst.CtrMachId = Convert.ToInt64(txtCounter.Tag);
            CheckInMst.FyId = UserInfo.fy_id;
            //CheckInMst.InDate = FormatDateToString(dtpCheckIn.Value);
            CheckInMst.InDate = dtpCheckIn.Value;
            // CheckInMst.InTime = dtpCheckInTime.Value
            //CheckInMst.InTime = Convert.ToDateTime(dtpCheckInTime.Text).Hour.ToString("00") + ":" + Convert.ToDateTime(dtpCheckInTime.Text).Minute.ToString("00") + ":00";
            CheckInMst.InTime = dtpCheckInTime.Value;
            CheckInMst.SerialNo = Convert.ToInt64(txtVchNo.Tag);
            // CheckInMst.AppNo = Val(txtAppNo.Text)
            CheckInMst.Name = txtName.Text;
            CheckInMst.Place = txtPlace.Text;
            CheckInMst.mob_no = txtmobno.Text;
            CheckInMst.Days = Convert.ToInt32(txtDays.Text);
            CheckInMst.NoOfLockers = Convert.ToInt32(txtNoOfLockers.Text);
            //CheckInMst.OutDate = FormatDateToString(dtpCheckOut.Value);
            CheckInMst.OutDate = dtpCheckOut.Value;
            //CheckInMst.OutTime = Convert.ToDateTime(dtpCheckOutTime.Text).Hour.ToString("00") + ":" + Convert.ToDateTime(dtpCheckOutTime.Text).Minute.ToString("00") + ":00";
            CheckInMst.OutTime = dtpCheckOutTime.Value;
            CheckInMst.Advance = Convert.ToDouble(nudAdvance.Value);
            CheckInMst.Rent = Convert.ToDouble(nudRent.Value);
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
            LockerCheckInDet CheckInDet = new LockerCheckInDet();
            CheckInDet.CheckInMstId = Convert.ToInt64(txtVchNo.Tag);
            for (var i = 0; i <= chkLockers.Items.Count - 1; i++)
            {
                if (chkLockers.GetItemChecked(i))
                {
                    CheckInDet.LockerId = CF.lsbItemData(chkLockers, i);
                    CheckInDet.LockerAvailableStatus = (int)eTokenDetail.StatusNo;
                    CheckInDet.LockerRecordModifiedCount = CF.lsbItemData(chkLockers, i) + 1;
                    coll.Add(CheckInDet);
                }
            }
            return coll;
        }

        private bool IsValidForm()
        {
            // If txtAppNo.Text = "" Then
            // ReDim mStrErrMsg(0)
            // mStrErrMsg(0) = "App No"
            // IsValidForm = ShowValidateError(txtAppNo, 0, mStrErrMsg, 1)
            // Exit Function
            // End If

            if (txtName.Text == "")
            {
                mStrErrMsg = new string[1];
                mStrErrMsg[0] = "Name";
                return ShowValidateError(txtName, 0, mStrErrMsg, 1);
            }

            if (txtPlace.Text == "")
            {
                mStrErrMsg = new string[1];
                mStrErrMsg[0] = "Place";
                return ShowValidateError(txtPlace, 0, mStrErrMsg, 1);
            }

            if (txtDays.Text == "")
            {
                mStrErrMsg = new string[1];
                mStrErrMsg[0] = "Days";
                return ShowValidateError(txtDays, 0, mStrErrMsg, 1);
            }

            if (txtNoOfLockers.Text == "")
            {
                mStrErrMsg = new string[1];
                mStrErrMsg[0] = "No of Lockers";
                return ShowValidateError(txtNoOfLockers, 0, mStrErrMsg, 1);
            }

            if (chkLockers.CheckedItems.Count == 0)
            {
                mStrErrMsg = new string[1];
                mStrErrMsg[0] = "Lockers";
                return ShowValidateError(txtNoOfLockers, 0, mStrErrMsg, 37);
            }


            if (txtmobno.Text != "")
            {
                System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex("^[7-9][0-9]{9}");

                if (re.IsMatch(txtmobno.Text.Trim()) == false)
                {
                    MessageBox.Show("Invalid Mobile Number");
                    txtmobno.Focus();
                    return false;
                }
            }

            if (txtNoOfLockers.Text != chkLockers.CheckedItems.Count.ToString())
            {
                mStrErrMsg = new string[1];
                mStrErrMsg[0] = "Lockers";
                MessageBox.Show("No of Lockers specified doen't match with Locker Selected.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                chkLockers.Focus();
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
                        ProcErr = CF.GetErrorMessage(mStrErrMsg, 6);
                        break;
                    }

                case -101:
                    {
                        ProcErr = CF.GetErrorMessage(mStrErrMsg, 23);
                        break;
                    }

                case -102:
                    {
                        ProcErr = CF.GetErrorMessage(mStrErrMsg, 23);
                        break;
                    }

                case -103:
                    {
                        ProcErr = CF.GetErrorMessage(mStrErrMsg, 24);
                        break;
                    }

                case -104:
                    {
                        ProcErr = CF.GetErrorMessage(mStrErrMsg, 25);
                        break;
                    }

                case -547:
                    {
                        ProcErr = "Cannot be deleted as child record exist.";
                        break;
                    }

                case -2627:
                    {
                        ProcErr = "Locker aready used.";
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
            if ((mAction == eAction.ActionInsert || mAction == eAction.ActionUpdate) & blnformChange)
            {
                mStrErrMsg = new string[1];
                mStrErrMsg[0] = "";
                sReply = MessageBox.Show(CF.GetErrorMessage(mStrErrMsg, 18), PrjMsgBoxTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (sReply == DialogResult.Yes)
                {
                    if (!fncSave())
                        return;
                }
                else if (sReply == DialogResult.Cancel)
                    return;
            }

            setCursor(this, false);
            frmSearchNew form1 = new frmSearchNew("LOCK_LOCKER_CHECK_IN_MST_FIND_V",true, eModType.Locker);
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
            if (LoadTransaction(lngSearchId,blnFormLock) == true)
            {
                blnformChange = false;
                if (mBlnEdit & !blnFormLock)
                {
                    mAction = eAction.ActionUpdate;
                    CF.subLockForm(false, CtrlArr, false);
                    dtpCheckIn.Enabled = false;
                    dtpCheckInTime.Enabled = false;
                    chkLockers.Enabled = true;
                    btnPrint.Enabled = true;
                }
                else
                {
                    mAction = eAction.ActionLocked;
                    dtpCheckIn.Enabled = false;
                    dtpCheckInTime.Enabled = false;
                    chkLockers.Enabled = false;
                    CF.subLockForm(true, CtrlArr, false);
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
            btnPrint.Enabled = true;
            setCursor(this, true);
        }

        private bool LoadTransaction(long lngSearchId,bool blnLockForm)
        {
            DataTable dr = new DataTable();
            LockerCheckInDet CheckInDet = new LockerCheckInDet();
            int ctr = 0;
            bool blnFlag = false;
            mDelColl = new Collection();
            FormClear();

            try
            {
                dr = LockerCheckInDALobj.GetDrLockerCheckInMst(lngSearchId);
                if (dr.Rows.Count > 0)
                {
                    dtpCheckIn.Value = Convert.ToDateTime(dr.Rows[0]["InDate"]);
                    dtpCheckInTime.Value = Convert.ToDateTime(dr.Rows[0]["InTime"]);
                    txtVchNo.Text = Convert.ToInt64(dr.Rows[0]["SerialNo"]).ToString();
                    txtVchNo.Tag = lngSearchId;
                    dtpCheckIn.Tag = dr.Rows[0]["RecordModifiedCount"];
                    txtName.Text = dr.Rows[0]["Name"].ToString();
                    txtPlace.Text = dr.Rows[0]["Place"].ToString();
                    txtDays.Text = dr.Rows[0]["Days"].ToString();
                    dtpCheckOut.Value = Convert.ToDateTime(dr.Rows[0]["OutDate"]);
                    dtpCheckOutTime.Value = Convert.ToDateTime(dr.Rows[0]["OutTime"]);
                    txtNoOfLockers.Text = dr.Rows[0]["NoOfLockers"].ToString();
                    nudAdvance.Text = dr.Rows[0]["Advance"].ToString();
                    nudRent.Text = dr.Rows[0]["Rent"].ToString();
                    dtEnteredOn = Convert.ToDateTime(dr.Rows[0]["EnteredOn"]);

                    if (Convert.ToInt32(dr.Rows[0]["NoOfLockers"]) != Convert.ToInt32(dr.Rows[0]["PendLockerCount"]))
                    {
                        blnLockForm = true;
                    }
                }
                //dr.Close();

                dr = LockerCheckInDALobj.GetDrLockerCheckInDet(lngSearchId, !blnLockForm, Convert.ToInt16(txtCounter.Tag));
                mDelColl = new Collection();
                CheckInDet = new LockerCheckInDet();
                CheckInDet.CheckInMstId = Convert.ToInt64(txtVchNo.Tag);

                //while (dr.Read())
                foreach (DataRow row in dr.Rows)    
                {
                    ctr++;
                    chkLockers.Items.Add(new clsItemData(row["LockerName"].ToString(), Convert.ToInt32(row["LockerId"]), row["RecordModifiedCount"].ToString()));

                    if (Convert.ToInt64(row["LockerCheckInDetId"]) > 0)
                    {
                        chkLockers.SetItemChecked(ctr - 1, true);
                        CheckInDet.LockerId = Convert.ToInt32(row["LockerId"]);
                        CheckInDet.LockerAvailableStatus = (int)eTokenDetail.StatusYes;
                        CheckInDet.LockerRecordModifiedCount = Convert.ToInt32(row["RecordModifiedCount"]) + 1;
                        CheckInDet.CheckInDetId = Convert.ToInt64(row["LockerCheckInDetId"]);
                        CheckInDet.CheckInMstId = Convert.ToInt64(row["LockerCheckInMstId"]);
                        mDelColl.Add(CheckInDet);
                    }
                }
                //dr.Close();
                blnFlag = true;
            }
            catch (Exception ex)
            {
                CF.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                //if (dr != null) dr.Close();
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

        private void frmLockerCheckIn_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult msg;
            try
            {
                if (blnformChange == true)
                {
                    mStrErrMsg = new string[1];
                    mStrErrMsg[0] = "";
                    msg = MessageBox.Show(CF.GetErrorMessage(mStrErrMsg, 18), PrjMsgBoxTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
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
                            MessageBox.Show(ex.Message);
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
                CF.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                MessageBox.Show(ex.Message, PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnPrint_Click(System.Object sender, System.EventArgs e)
        {
            // If Val(dtpCheckIn.Tag & vbNullString) = 0 Or blnformChange Then Exit Sub
            string strReportName;
            Collection pColl = new Collection();
            setCursor(this, false);
            // Dim printDoc As New System.Drawing.Printing.PrintDocument()

            // strReportName = "LockerCheckInReceipt.rpt"
            strReportName = "LockerCheckInReceipt_New.rpt";
            FillDataInDataset();

            frmReportViewer frm = new frmReportViewer("PRINT", null,null, TempTable1);
            frm.createReport("LockerCheckIN");
            setCursor(this, true);
            //Form sForm = new frmCrystalViewer(UserInfo.ReportPath + strReportName, null, ds, null, pColl, eReportID.LockerCheckIn, true);
            //sForm.Text = "Locker Check In : " + eReportID.LockerCheckIn;
            //sForm.Show();
            //sForm.Close();
        }

        public sealed class myPrinters
        {
            private myPrinters()
            {
            }

            //[DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
            //public static bool SetDefaultPrinter(string Name)
            //{
            //}
        }

        private void FillDataInDataset()
        {
            Int16 i;
            decimal total_val;
            TempTable1.Rows.Clear();
            //TempTable2.Rows.Clear();
            MyRow = TempTable1.NewRow();
            MyRow["CHECK_IN_MST_ID"] = 1;
            MyRow["LOC_SH_NAME"] = LockerCheckInLocName;
            MyRow["DEPT_SH_NAME"] = LockerCheckInDeptName;
            MyRow["COUNTER"] = mStrCounterMachineShortName;

            MyRow["IN_DATE"] = dtpCheckIn.Value;
            MyRow["IN_TIME"] = dtpCheckInTime.Text;
            MyRow["SERIAL_NO"] = txtVchNo.Text;
            // My[ow("APP_NO") = txtAppNo.Text

            MyRow["NAME"] = txtName.Text;
            MyRow["PLACE"] = txtPlace.Text;
            MyRow["MOB_NO"] = txtmobno.Text;
            MyRow["DAYS"] = txtDays.Text;

            MyRow["NO_OF_LOCKERS"] = txtNoOfLockers.Text;
            MyRow["OUT_DATE"] = dtpCheckOut.Value;
            MyRow["OUT_TIME"] = dtpCheckOutTime.Value;

            MyRow["ADVANCE"] = nudAdvance.Value;
            MyRow["RENT"] = nudRent.Value;

            MyRow["USER_NAME"] = txtUser.Text;
            MyRow["SERVER_NAME"] = UserInfo.serverName;
            MyRow["MACHINE_NAME"] = UserInfo.Machine_Name;
            total_val = nudAdvance.Value;
            if (total_val > 0)
                MyRow["AMT_IN_WORDS"] = CF.getNumbersInWords(total_val.ToString(), eCurrencyType.Rupees);
            else
                MyRow["AMT_IN_WORDS"] = " -- ";


            string Roomname = "";
            for (i = 0; i <= chkLockers.Items.Count - 1; i++)
            {
                if (chkLockers.GetItemChecked(i))
                {
                    if (Roomname == "")
                        Roomname = CF.lsbItemName(chkLockers, i);
                    else
                        Roomname = Roomname + ", " + CF.lsbItemName(chkLockers, i);
                }
            }
            //MyRow = TempTable2.NewRow();
            //MyRow["CHECK_IN_MST_ID"] = 1;
            MyRow["LOCKER_NAME"] = Roomname;
            TempTable1.Rows.Add(MyRow);
            //TempTable2.Rows.Add(MyRow);
        }

        private void CreateDs()
        {
            TempTable1 = new DataTable("LOCK_LOCKER_CHECK_IN_MST_T");
            //TempTable2 = new DataTable("LOCK_LOCKER_CHECK_IN_DET_T");

            TempTable1.Columns.Add("CHECK_IN_MST_ID", System.Type.GetType("System.Int64"));
            TempTable1.Columns.Add("LOC_SH_NAME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("DEPT_SH_NAME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("COUNTER", System.Type.GetType("System.String"));

            TempTable1.Columns.Add("IN_DATE", System.Type.GetType("System.DateTime"));
            TempTable1.Columns.Add("IN_TIME", System.Type.GetType("System.DateTime"));
            TempTable1.Columns.Add("SERIAL_NO", System.Type.GetType("System.Int64"));
            // TempTable1.Columns.Add("APP_NO", System.Type.GetType("System.String"))

            TempTable1.Columns.Add("NAME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("PLACE", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("MOB_NO", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("DAYS", System.Type.GetType("System.Int32"));

            TempTable1.Columns.Add("NO_OF_LOCKERS", System.Type.GetType("System.Int32"));
            TempTable1.Columns.Add("OUT_DATE", System.Type.GetType("System.DateTime"));
            TempTable1.Columns.Add("OUT_TIME", System.Type.GetType("System.DateTime"));

            TempTable1.Columns.Add("ADVANCE", System.Type.GetType("System.Double"));
            TempTable1.Columns.Add("RENT", System.Type.GetType("System.Double"));

            TempTable1.Columns.Add("USER_NAME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("SERVER_NAME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("MACHINE_NAME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("AMT_IN_WORDS", System.Type.GetType("System.String"));

            TempTable1.Columns.Add("LOCKER_NAME", System.Type.GetType("System.String"));
            //TempTable2.Columns.Add("CHECK_IN_MST_ID", System.Type.GetType("System.Int64"));
            //TempTable2.Columns.Add("LOCKER_NAME", System.Type.GetType("System.String"));

            //ds.Tables.Add(TempTable1);
            //ds.Tables.Add(TempTable2);
        }


        private void txtName_TextChanged(System.Object sender, System.EventArgs e)
        {
            string strname = txtName.Text;
        }

        private void txtPlace_TextChanged(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
        }

        private void txtDays_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            //if (!((e.KeyChar >= 0 && e.KeyChar <= 9) || e.KeyChar == 8 || e.KeyChar == 46))
            //    e.Handled = true;
        }

        private void txtmobno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            blnformChange = true;
            // Add Code to Allow Free Counter
            // 20/12/2019 - 
            if (chkAllowFreeReceipt.Checked & txtOldReceipt.Text != "")
            {
                dtpCheckOut.Value = dtpCheckIn.Value.AddDays(Convert.ToInt32(txtDays.Text));
                nudRent.Value = 0; // RoundIt(LockerRentPerday * Val(txtNoOfLockers.Text & vbNullString) * Val(txtDays.Text & vbNullString), 2)
            }
            else
            {
                dtpCheckOut.Value = dtpCheckIn.Value.AddDays(Convert.ToInt32(txtDays.Text == "" ? "0" : txtDays.Text));
                nudRent.Value = (decimal)Math.Round(LockerRentPerday * Convert.ToDouble(txtNoOfLockers.Text == "" ? "0" : txtNoOfLockers.Text) * Convert.ToDouble(txtDays.Text == "" ? "0" : txtDays.Text), 2);
            }
        }
        private void txtDays_TextChanged(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
            // Add Code to Allow Free Counter
            // 20/12/2019 - 
            if (chkAllowFreeReceipt.Checked & txtOldReceipt.Text != "")
            {
                dtpCheckOut.Value = dtpCheckIn.Value.AddDays(Convert.ToInt32(txtDays.Text));
                nudRent.Value = 0; // RoundIt(LockerRentPerday * Val(txtNoOfLockers.Text & vbNullString) * Val(txtDays.Text & vbNullString), 2)
            }
            else
            {
                dtpCheckOut.Value = dtpCheckIn.Value.AddDays(Convert.ToInt32(txtDays.Text == "" ? "0" : txtDays.Text));
                nudRent.Value = (decimal)Math.Round(LockerRentPerday * Convert.ToDouble(txtNoOfLockers.Text == "" ? "0" : txtNoOfLockers.Text) * Convert.ToDouble(txtDays.Text == "" ? "0" : txtDays.Text), 2);
            }
        }

        private void txtNoOfLockers_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            //if (!((e.KeyChar >= 0 & e.KeyChar <= 9) | e.KeyChar == 8 | e.KeyChar == 46))
            //    e.Handled = true;
        }

        private void txtNoOfLockers_TextChanged(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
            // Add Code to Allow Free Counter
            // 20/12/2019 - 
            if (chkAllowFreeReceipt.Checked & txtOldReceipt.Text != "")
            {
                nudRent.Value = 0; // RoundIt(LockerRentPerday * Val(txtNoOfLockers.Text & vbNullString) * Val(txtDays.Text & vbNullString), 2)
                                   // nudAdvance.Value = RoundIt(LockerAdvanceTariff * Val(txtNoOfLockers.Text & vbNullString) * Val(txtDays.Text & vbNullString), 2)
                nudAdvance.Value = 0; // RoundIt(LockerAdvanceTariff * Val(txtNoOfLockers.Text & vbNullString), 2)
            }
            else
            {
                nudRent.Value = (decimal)Math.Round(LockerRentPerday * Convert.ToDouble(txtNoOfLockers.Text) * Convert.ToDouble(txtDays.Text == "" ? "0" : txtDays.Text), 2);
                // nudAdvance.Value = RoundIt(LockerAdvanceTariff * Val(txtNoOfLockers.Text & vbNullString) * Val(txtDays.Text & vbNullString), 2)
                nudAdvance.Value = (decimal)Math.Round(LockerAdvanceTariff * Convert.ToDouble(txtNoOfLockers.Text), 2);
            }
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

        // Private Sub txtAppNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        // blnformChange = True
        // End Sub


        private void chkLockers_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
            txtNoOfLockers.Text = chkLockers.CheckedItems.Count.ToString();
        }

        private bool ShowValidateError(Control myObject, int tabIndex, string[] ErrMsg, int ErrNo)
        {
            setCursor(this, true);
            MessageBox.Show(CF.GetErrorMessage(ErrMsg, ErrNo), PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            myObject.Focus();
            return false;
        }



        private void SetError(string str)
        {
            //OSOL_CONNECTION.clsConnection.mErrorResult = OSOL_CONNECTION.clsConnection.mErrorResult + Constants.vbNewLine + str;
        }




        private void txtName_KeyPress(System.Object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (char.IsLower(e.KeyChar))
                e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtPlace_KeyPress(System.Object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (char.IsLower(e.KeyChar))
                e.KeyChar = char.ToUpper(e.KeyChar);
        }


        private void txtDays_Enter(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
            dtpCheckOut.Value = dtpCheckIn.Value.AddDays(Convert.ToDouble(txtDays.Text == "" ? "0" : txtDays.Text));
            nudRent.Value = (decimal)Math.Round(LockerRentPerday * Convert.ToDouble(txtNoOfLockers.Text == "" ? "0" : txtNoOfLockers.Text) * Convert.ToDouble(txtDays.Text == "" ? "0" : txtDays.Text), 2);

        }




        // Private Sub chkLockers_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles chkLockers.PreviewKeyDown
        // Dim intN As Integer
        // Dim intCount As Integer = chkLockers.Items.Count
        // If chkLockers.SelectedIndex < 0 Then
        // intN = 0
        // Else
        // intN = chkLockers.SelectedIndex
        // End If

        // Select Case e.KeyCode

        // Case Keys.Down
        // intN = chkLockers.SelectedIndex + 1
        // If intN = intCount Then
        // chkLockers.SelectedIndex = 0
        // Else
        // chkLockers.SelectedIndex = intN
        // End If

        // Case Keys.Up
        // intN = chkLockers.SelectedIndex - 1
        // If intN < 0 Then
        // chkLockers.SelectedIndex = intCount - 1
        // Else
        // chkLockers.SelectedIndex = intN
        // End If

        // Case Keys.Enter
        // chkLockers.SelectedIndex = intN
        // chkLockers.SetItemChecked(intN, True)
        // End Select
        // txtNoOfLockers.Text = chkLockers.CheckedItems.Count
        // End Sub

        private void txtRoomSrch_TextChanged(System.Object sender, System.EventArgs e)
        {
            FillAvailableLockers();
        }

        // Code added - Free Receipt 20/12/2019
        private void chkAllowFreeReceipt_CheckedChanged(System.Object sender, System.EventArgs e)
        {
            if (chkAllowFreeReceipt.Checked)
                txtOldReceipt.Visible = true;
            else
                txtOldReceipt.Visible = false;
            txtOldReceipt.Text = "";
        }


    }
}
