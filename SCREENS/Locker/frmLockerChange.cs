using CrystalDecisions.ReportAppServer;
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
using SGMOSOL.DAL;
using System.Data.SqlClient;
using static SGMOSOL.BAL.LockerBAL;
using SGMOSOL.BAL;

namespace SGMOSOL.SCREENS.Locker
{
    public partial class frmLockerChange : Form
    {
        private eScreenID mScreenID;
        private bool mBlnEdit = false;
        private eAction mAction; 
        private ArrayList CtrlArr = new ArrayList(); 
        private ArrayList btnArr = new ArrayList();
        private CommonFunctions cf = new CommonFunctions();
        private LockerCheckInDAL LockerCheckInDALobj = new LockerCheckInDAL();
        //private OSOL_BLSDS.clsBlsLockerCheckOut objBlsLockerCheckOut = new OSOL_BLSDS.clsBlsLockerCheckOut();
        //private OSOL_BLSDS.clsDsLockerCheckInMst objDsLockerCheckInMst = new OSOL_BLSDS.clsDsLockerCheckInMst();
        private LockerChangeDAL objDsLockerChange = new LockerChangeDAL();
        //private OSOL_BLSDS.clsDsLockerCheckInDet objDsLockerCheckInDet = new OSOL_BLSDS.clsDsLockerCheckInDet();
        private LockerMasterDAL objDsLockerMst = new LockerMasterDAL();
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
        private double LockerAdvanceTariff;
        private double LockerRentPerday;
        private string mStrCounterMachineShortName;
        private int LockerCheckInDeptID;
        private int LockerCheckOutDeptID;
        private string LockerCheckInDeptName;
        private string LockerCheckInLocName;
        private string NameLockerHolder;
        private string PlaceLockerHolder;
        private Int16 oldDays;
        private int PrintReceiptLocId;

        public frmLockerChange(eScreenID ScreenID)
        {
            InitializeComponent();
            this.Closing += new CancelEventHandler(this.frmLockerChange_Closing);
            mScreenID = ScreenID;
            if (mScreenID == eScreenID.LockerExtend)
                this.Text = "Locker Extend";
            else
                this.Text = "Locker Change";
        }

        private void btnNew_Click(System.Object sender, System.EventArgs e)
        {
            SqlDataReader dr;
            FormClear();
            cf.fncSetDateAndRange(dtpCheckIn);
            cf.fncSysTime(dtpCheckInTime);
            dtpCheckOut.Value = dtpCheckIn.Value;
            dtpCheckOutTime.Value = dtpCheckInTime.Value;
            dtpCheckIn.Enabled = bkDateEntry;
            mAction = eAction.ActionInsert;
            cf.subLockForm(false, CtrlArr, false);
            btnSave.Enabled = btnNew.Enabled;
            // Try
            // dr = objDsLockerCheckInMst.GetDrMaxSrNo(txtCounter.Tag, UserInfo.CompanyID, PrintReceiptLocId, 0, UserInfo.FYID)
            // If dr.Read() Then
            // ' txtVchNo.Text = Val(dr["SerialNo") & vbNullString) + 1
            // Else
            // ' txtVchNo.Text = "1"
            // End If
            // dr.Close()
            // Catch ex As Exception
            // If Not dr Is Nothing Then dr.Close()
            // End Try
            FillAvailableLockers();
            chkLockers.Enabled = true;

            blnformChange = false;
            if (mAction == eAction.ActionInsert)
                btnPrint.Enabled = false;
            else
                btnPrint.Enabled = true;
        }

        private void FillAvailableLockers()
        {
            DataTable dr = null;
            try
            {
                dr = objDsLockerMst.GetDrLockerDetails(PrintReceiptLocId, (int)eTokenDetail.StatusActive, (int)eTokenDetail.StatusYes);
                cf.FillListBox(chkLockers, dr, "LockerName", "LockerId", "RecordModifiedCount");
            }
            catch (Exception ex)
            {
                //if (dr != null)
                //    dr.Close();
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
        }

        private void frmLockerChange_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter & !DisableSendKeys)
                SendKeys.Send("{tab}");
            if (e.KeyCode == Keys.End & (mAction == eAction.ActionInsert | mAction == eAction.ActionUpdate) & blnformChange)
                btnSave_Click(null, null);
        }

        private void frmLockerChange_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End & (mAction == eAction.ActionInsert | mAction == eAction.ActionUpdate) & blnformChange)
                btnSave_Click(null, null);
        }

        private void frmLockerChange_Load(System.Object sender, System.EventArgs e)
        {
            // Me.WindowState = FormWindowState.Maximized
            cf.setControlsonForm(this, CtrlArr, btnArr);
            cf.SetUserScreenActions(this, UserInfo.UserId, (Int64)mScreenID, btnArr, null, mBlnEdit);
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
            DataTable dr = null;
            try
            {
                dr = objDsLockerMst.GetDrLockerTariff();
                if (dr != null && dr.Rows.Count > 0)
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
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            if (btnNew.Enabled)
                btnNew_Click(null, null);
            else
                cf.subLockForm(true, CtrlArr, false);
        }

        private void FillCounter()
        {
            try
            {
                DataTable dr;
                dr = cf.GetDrCounterMachId(UserInfo.UserId, SystemHDDModelNo, SystemHDDSerialNo, SystemMacID, Convert.ToInt16(eModType.Locker));
                if (dr.Rows.Count > 0)
                {
                    txtCounter.Text = dr.Rows[0]["CounterMachineTitle"].ToString();
                    txtCounter.Tag = dr.Rows[0]["CtrMachId"];
                    LockerCheckInDeptID = Convert.ToInt32(dr.Rows[0]["DeptId"]);
                    LockerCheckOutDeptID = Convert.ToInt32(dr.Rows[0]["DeptId"]);
                    LockerCheckInDeptName = dr.Rows[0]["DepartmentName"].ToString();
                    LockerCheckInLocName = dr.Rows[0]["LocName"].ToString();
                    PrintReceiptLocId = Convert.ToInt32(dr.Rows[0]["LocId"]);
                    mStrCounterMachineShortName = dr.Rows[0]["CounterMachineShortName"].ToString();
                }
                //dr.Close();
            }catch (Exception ex) { cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version); }
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
            txtDays.SelectedIndex = 0;
            txtNoOfLockers.Text = "0";
            nudRentPaid.Value = 0;
            nudRentPending.Value = 0;
            nudAdvance.Value = 0;
            nudRent.Value = 0;
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
        }

        private bool fncSave()
        {
            long lngError = -1;
            LockerCheckInMst LockerCheckInMst;
            LockerCheckOutMst LockerCheckOutMst;
            LockerChangeMst LockerChangeMst;
            Collection coll;
            Collection coll1;
            long lngSerialNo;
            string strErr;
            bool flag = false;
            if (IsValidForm() == false)
            {
                return false;
            }
            setCursor(this, false);
            lngSerialNo = Convert.ToInt64(txtVchNo.Text + Constants.vbNullString);
            LockerCheckInMst = GetCheckInMst();
            coll = GetCheckInDetColl();
            LockerChangeMst = GetLockerChangeMst();
            // LockerCheckOutMst = GetCheckOutMst()
            coll1 = GetCheckOutDetColl();
            mDelColl = getlocker();
            mAction = eAction.ActionUpdate;
            // lngError = objBlsLockerCheckIn.Insert(LockerCheckInMst, coll, UserInfo.UserName, UserInfo.MacineName, lngSerialNo, dtEnteredOn)
            if (mAction == eAction.ActionUpdate)
            {
                lngError = LockerCheckInDALobj.Update(LockerCheckInMst, coll, mDelColl, UserInfo.UserName, UserInfo.Machine_Name, lngSerialNo);
                // lngError = objBlsLockerCheckOut.Insert(LockerCheckOutMst, coll1, UserInfo.UserName, UserInfo.MacineName, lngSerialNo, dtEnteredOn)
                if (mScreenID == eScreenID.Lockerchange)
                    lngError = LockerCheckInDALobj.Insert1(LockerChangeMst, mDelColl, UserInfo.UserName, UserInfo.Machine_Name, lngSerialNo, dtEnteredOn);
                else
                    lngError = LockerCheckInDALobj.InsertRoomExtend(LockerCheckInMst, UserInfo.UserName, UserInfo.Machine_Name);
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

        private LockerCheckInMst GetCheckInMst()
        {
            LockerCheckInMst CheckInMst = new LockerCheckInMst();
            CheckInMst.CheckInMstId = Convert.ToInt64(txtVchNo.Tag + Constants.vbNullString);
            CheckInMst.ComId = UserInfo.CompanyID;
            CheckInMst.LocId = PrintReceiptLocId;
            CheckInMst.DeptId = LockerCheckInDeptID;
            CheckInMst.CtrMachId = Convert.ToInt64(txtCounter.Tag);
            CheckInMst.FyId = UserInfo.fy_id;
            //CheckInMst.InDate = FormatDateToString(dtpCheckIn.Value);
            CheckInMst.InDate = dtpCheckIn.Value;
            //CheckInMst.InTime = Format(Hour(dtpCheckInTime.Text), "00") + ":" + Format(Minute(dtpCheckInTime.Text), "00") + ":00";
            //CheckInMst.InTime = Convert.ToDateTime(dtpCheckInTime.Text).Hour.ToString("00") + ":" + Convert.ToDateTime(dtpCheckInTime.Text).Minute.ToString("00") + ":00";
            CheckInMst.InTime = dtpCheckInTime.Value;
            CheckInMst.SerialNo = Convert.ToInt64(txtVchNo.Tag + Constants.vbNullString);
            // CheckInMst.AppNo = Val(txtAppNo.Text)
            CheckInMst.Name = txtName.Text;
            CheckInMst.Place = txtPlace.Text;
            CheckInMst.mob_no = txtmobno.Text;
            CheckInMst.Days = Convert.ToInt32(txtDays.Text);
            CheckInMst.NoOfLockers = Convert.ToInt32(txtNoOfLockers.Text);
            //CheckInMst.OutDate = FormatDateToString(dtpCheckOut.Value);
            CheckInMst.OutDate = dtpCheckOut.Value;
            //CheckInMst.OutTime = Format(Hour(dtpCheckOutTime.Text), "00") + ":" + Format(Minute(dtpCheckOutTime.Text), "00") + ":00";
            //CheckInMst.OutTime = Convert.ToDateTime(dtpCheckOutTime.Text).Hour.ToString("00") + ":" + Convert.ToDateTime(dtpCheckOutTime.Text).Minute.ToString("00") + ":00";
            CheckInMst.OutTime = dtpCheckOutTime.Value;
            CheckInMst.Advance = Convert.ToDouble(nudAdvance.Value);
            CheckInMst.Rent = Convert.ToDouble(nudRent.Value);
            CheckInMst.UserId = UserInfo.UserId;
            CheckInMst.ExtDay = Convert.ToInt32(txtDays.Text);
            CheckInMst.ExtRent = Convert.ToDouble(nudRentPending.Value);
            CheckInMst.ExtDate = DateTime.Now.ToString("dd/MM/yyyy");
            CheckInMst.ServerName = UserInfo.serverName;
            CheckInMst.EnteredBy = UserInfo.UserName;
            CheckInMst.ModifiedBy = UserInfo.UserName;
            CheckInMst.RecordModifiedCount = Convert.ToInt64(dtpCheckIn.Tag + Constants.vbNullString) + 1;
            return CheckInMst;
        }


        private Collection GetCheckInDetColl()
        {
            Collection coll = new Collection();

            LockerCheckInDet CheckInDet = new LockerCheckInDet();
            CheckInDet.CheckInMstId = Convert.ToInt64(txtVchNo.Tag + Constants.vbNullString);
            for (var i = 0; i <= chkLockers.Items.Count - 1; i++)
            {
                if (chkLockers.GetItemChecked(i))
                {
                    CheckInDet.LockerId = cf.lsbItemData(chkLockers, i);
                    CheckInDet.LockerAvailableStatus = (int)eTokenDetail.StatusNo;
                    CheckInDet.LockerRecordModifiedCount = cf.lsbItemData(chkLockers, i) + 1;
                    coll.Add(CheckInDet);
                }
            }


            return coll;
        }
        private Collection GetCheckOutDetColl()
        {
            Collection coll = new Collection();
            LockerCheckOutDet CheckOutDet = new LockerCheckOutDet();
            CheckOutDet.CheckOutMstId = Convert.ToInt64(txtVchNo.Tag + Constants.vbNullString);
            for (var i = 0; i <= chkLockers.Items.Count - 1; i++)
            {
                if (!chkLockers.GetItemChecked(i))
                {
                    CheckOutDet.LockerId = cf.lsbItemData(chkLockers, i);
                    CheckOutDet.LockerAvailableStatus = (int)eTokenDetail.StatusYes;
                    CheckOutDet.LockerRecordModifiedCount = Convert.ToInt64(cf.lsbItemName2(chkLockers, i) + Constants.vbNullString) + 1;
                    coll.Add(CheckOutDet);
                }
            }
            return coll;
        }

        public Collection getlocker()
        {
            Collection coll = new Collection();
            try
            {
                LockerCheckInDet CheckInDet = new LockerCheckInDet();
                CheckInDet.CheckInMstId = Convert.ToInt64(txtVchNo.Tag + Constants.vbNullString);
                DataTable dr;
                dr = objDsLockerChange.FindLocker(CheckInDet.CheckInMstId);
                while (dr != null && dr.Rows.Count > 0)
                {
                    CheckInDet.LockerId = Convert.ToInt32(dr.Rows[0]["LOCKER_ID"]);
                    CheckInDet.LockerAvailableStatus = (int)eTokenDetail.StatusYes;
                    coll.Add(CheckInDet);
                }

                //dr.Close();
            }catch(Exception ex) { cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version); }
            return coll;
        }
        private LockerCheckOutMst GetCheckOutMst()
        {
            LockerCheckOutMst CheckOutMst = new LockerCheckOutMst();
            CheckOutMst.CheckOutMstId = Convert.ToInt64(txtVchNo.Tag + Constants.vbNullString);
            CheckOutMst.ComId = UserInfo.CompanyID;
            CheckOutMst.LocId = PrintReceiptLocId;
            CheckOutMst.DeptId = LockerCheckOutDeptID;
            CheckOutMst.CtrMachId = Convert.ToInt64(txtCounter.Tag);
            CheckOutMst.FyId = UserInfo.fy_id;
            //CheckOutMst.OutDate = FormatDateToString(dtpCheckOut.Value);
            CheckOutMst.OutDate = dtpCheckOut.Value;
            //CheckOutMst.OutTime = Format(Hour(dtpCheckOutTime.Text), "00") + ":" + Format(Minute(dtpCheckOutTime.Text), "00") + ":00";
            //CheckOutMst.OutTime = Convert.ToDateTime(dtpCheckOutTime.Text).Hour.ToString("00") + ":" + Convert.ToDateTime(dtpCheckOutTime.Text).Minute.ToString("00") + ":00";
            CheckOutMst.OutTime = dtpCheckOutTime.Value;
            CheckOutMst.SerialNo = Convert.ToInt64(txtVchNo.Tag + Constants.vbNullString);
            CheckOutMst.CheckInMstId = Convert.ToInt64(txtVchNo.Tag + Constants.vbNullString);
            CheckOutMst.Days = Convert.ToInt32(txtDays.Text);
            CheckOutMst.NoOfLockers = chkLockers.CheckedItems.Count;
            CheckOutMst.Advance = Convert.ToDouble(nudAdvance.Value);
            CheckOutMst.Rent = Convert.ToDouble(nudRent.Value);
            // CheckOutMst.Refund = NudRefund.Value
            CheckOutMst.UserId = UserInfo.UserId;
            CheckOutMst.ServerName = UserInfo.serverName;
            CheckOutMst.EnteredBy = UserInfo.UserName;
            CheckOutMst.ModifiedBy = UserInfo.UserName;
            CheckOutMst.RecordModifiedCount = Convert.ToInt64((dtpCheckOut.Tag + Constants.vbNullString) + 1);
            return CheckOutMst;
        }


        private LockerChangeMst GetLockerChangeMst()
        {
            LockerChangeMst LkrChangeMst = new LockerChangeMst();

            LkrChangeMst.ComId = UserInfo.CompanyID;
            LkrChangeMst.LocId = PrintReceiptLocId;
            LkrChangeMst.DeptId = LockerCheckOutDeptID;
            LkrChangeMst.CtrMachId = Convert.ToInt64(txtCounter.Tag);
            LkrChangeMst.FyId = UserInfo.fy_id;
            LkrChangeMst.OutDate = FormatDateToString(dtpCheckOut.Value);
            LkrChangeMst.OutTime = Convert.ToDateTime(dtpCheckOutTime.Text).Hour.ToString("00") + ":" + Convert.ToDateTime(dtpCheckOutTime.Text).Minute.ToString("00") + ":00";
            LkrChangeMst.SerialNo = Convert.ToInt64(txtVchNo.Tag + Constants.vbNullString);
            LkrChangeMst.CheckInMstId = Convert.ToInt64(txtVchNo.Tag + Constants.vbNullString);

            LkrChangeMst.Reason = txtReason.Text;
            LkrChangeMst.UserId = UserInfo.UserId;
            LkrChangeMst.ServerName = UserInfo.serverName;
            LkrChangeMst.EnteredBy = UserInfo.UserName;
            LkrChangeMst.ModifiedBy = UserInfo.UserName;

            return LkrChangeMst;
        }
        private bool IsValidForm()
        {
            int count_lockers;
            int Count_given;
            if (txtVchNo.Text == "")
            {
                mStrErrMsg = new string[1];
                mStrErrMsg[0] = "Rcpt No";
                return ShowValidateError(txtVchNo, 0, mStrErrMsg, 1);
            }

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
            count_lockers = chkLockers.CheckedItems.Count;
            if (Convert.ToInt32(txtNoOfLockers.Text) != count_lockers)
            {
                mStrErrMsg = new string[1];
                mStrErrMsg[0] = "Lockers";
                MessageBox.Show("No of Lockers specified doen't match with Locker Selected.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                chkLockers.Focus();
                return false;
            }
            if (mScreenID == eScreenID.Lockerchange)
            {
                Count_given = chklockers_given.CheckedItems.Count;
                if (Count_given != count_lockers)
                {
                    mStrErrMsg = new string[1];
                    mStrErrMsg[0] = "Locker";
                    MessageBox.Show("No of Locker change should be same as No of Locker given.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    chkLockers.Focus();
                    return false;
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
            frmSearchNew form1 = new frmSearchNew("LOCK_LOCKER_CHANGE_MST_T_FIND_V", false, eModType.Locker);
            long lngSearchId;
            form1.mIntCtrMachId = Convert.ToInt32(txtCounter.Tag + Constants.vbNullString);
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
                    chkLockers.Enabled = true;
                    txtVchNo.Focus();
                    btnPrint.Enabled = true;
                }
                else
                {
                    mAction = eAction.ActionLocked;
                    dtpCheckIn.Enabled = false;
                    dtpCheckInTime.Enabled = false;
                    chkLockers.Enabled = false;
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
        }

        private bool LoadTransaction(long lngSearchId, ref bool blnLockForm)
        {
            DataTable dr = null;
            // Dim CheckInDet As New OSOL_CONNECTION.LockerCheckInMst
            LockerCheckInDet CheckInDet = new LockerCheckInDet();
            int ctr = 0;
            bool blnFlag = false;
            mDelColl = new Collection();
            FormClear();
            try
            {
                dr = LockerCheckInDALobj.GetDrLockerCheckInMst(lngSearchId);
                if (dr != null && dr.Rows.Count > 0)
                {
                    // txtAppNo.Text = dr["AppNo")
                    dtpCheckIn.Value = Convert.ToDateTime(dr.Rows[0]["InDate"]);
                    dtpCheckInTime.Value = Convert.ToDateTime(dr.Rows[0]["InTime"]);
                    txtVchNo.Text = dr.Rows[0]["SerialNo"] + Constants.vbNullString;
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

                    if (dr.Rows[0]["NoOfLockers"] != dr.Rows[0]["PendLockerCount"])
                        blnLockForm = true;
                }
                //dr.Close();


                dr = LockerCheckInDALobj.GetDrLockerCheckInDet(lngSearchId, !blnLockForm, Convert.ToInt64(txtCounter.Tag));
                mDelColl = new Collection();
                CheckInDet = new LockerCheckInDet();
                CheckInDet.CheckInMstId = Convert.ToInt64(txtVchNo.Tag + Constants.vbNullString);
                foreach (DataRow drrow in dr.Rows)    
                {
                    ctr = ctr + 1;
                    chkLockers.Items.Add(new clsItemData(drrow["LockerName"].ToString(), Convert.ToInt32(drrow["LockerId"]), drrow["RecordModifiedCount"].ToString()));

                    if (Convert.ToInt32(drrow["LockerCheckInDetId"] + Constants.vbNullString) > 0)
                    {
                        chkLockers.SetItemChecked(ctr - 1, true);
                        CheckInDet.LockerId = Convert.ToInt32(drrow["LockerId"]);
                        CheckInDet.LockerAvailableStatus = (int)eTokenDetail.StatusYes;
                        CheckInDet.LockerRecordModifiedCount = Convert.ToInt64(drrow["RecordModifiedCount"]) + 1;
                        CheckInDet.CheckInDetId = Convert.ToInt64(drrow["LockerCheckInDetId"] + Constants.vbNullString);
                        CheckInDet.CheckInMstId = Convert.ToInt64(drrow["LockerCheckInMstId"] + Constants.vbNullString);
                        mDelColl.Add(CheckInDet);
                    }
                }
                //dr.Close();
                blnFlag = true;
            }
            catch (Exception ex)
            {
                //if (dr != null)
                //    dr.Close();
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
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

        private void frmLockerChange_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                MessageBox.Show(ex.Message, PrjMsgBoxTitle);
                //MsgBox(ex.Message, MsgBoxStyle.Information, PrjMsgBoxTitle);
            }
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

            //string strReportName;
            //Form sForm;
            //Collection pColl = new Collection();
            //setCursor(this, false);
            //System.Drawing.Printing.PrintDocument printDoc = new System.Drawing.Printing.PrintDocument();
            //string printD1;

            //myPrinters.SetDefaultPrinter(System.Configuration.ConfigurationSettings.AppSettings.Get("Printer_name"]);
            //printD1 = printDoc.PrinterSettings.PrinterName;
            //if (printDoc.PrinterSettings.PrinterName == printD1)
            //{
            //    strReportName = "LockerCheckInReceipt_New.rpt";
            //    FillDataInDataset();
            //    sForm = new frmCrystalViewer(UserInfo.ReportPath + strReportName, null, ds, null, pColl, eReportID.LockerCheckIn, true);
            //    sForm.Text = "Locker Check In : " + eReportID.LockerCheckIn;


            //    // Dim printDoc As New System.Drawing.Printing.PrintDocument()
            //    // Dim printD1 As String
            //    // printD1 = printDoc.PrinterSettings.PrinterName
            //    // If printDoc.PrinterSettings.PrinterName = "Send To OneNote 2007" Then
            //    sForm.Show();
            //    // End If
            //    setCursor(this, true);
            //}
            //else
            //    MessageBox.Show("Printer does not exists");
            // If Val(dtpCheckIn.Tag & vbNullString) = 0 Or blnformChange Then Exit Sub
            Collection pColl = new Collection();
            setCursor(this, false);
            FillDataInDataset();
            frmReportViewer frm = new frmReportViewer("PRINT", null, null, TempTable1);
            frm.createReport("LockerCheckIN");
            setCursor(this, true);
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
            MyRow["IN_TIME"] = dtpCheckInTime.Value;
            MyRow["SERIAL_NO"] = txtVchNo.Text + Constants.vbNullString;
            // MyRow("APP_NO"] = txtAppNo.Text

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
            total_val = (nudAdvance.Value + nudRent.Value);
            if (total_val > 0)
                MyRow["AMT_IN_WORDS"] = cf.getNumbersInWords(total_val.ToString(), eCurrencyType.Rupees);
            else
                MyRow["AMT_IN_WORDS"] = " -- ";


            string Roomname = "";
            for (i = 0; i <= chkLockers.Items.Count - 1; i++)
            {
                if (chkLockers.GetItemChecked(i))
                {
                    if (Roomname == "")
                        Roomname = cf.lsbItemName(chkLockers, i);
                    else
                        Roomname = Roomname + ", " + cf.lsbItemName(chkLockers, i);
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
            blnformChange = true;
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
            //if (!((e.KeyChar >= "0" & e.KeyChar <= "9") | Asc(e.KeyChar) == 8 | Asc(e.KeyChar) == 46))
            //    e.Handled = true;
        }
        private void txtDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            int NewDays = 0;
            int MaxDays = 0;
            SqlDataReader dr;

            if (txtDays.Text != "")
            {
                NewDays = Convert.ToInt16(txtDays.Text);
                if (NewDays < oldDays)
                    txtDays.Text = oldDays.ToString();
            }

            MaxDays = LockerCheckInDALobj.GetDaysLimit();
            //if (dr.Read())
            //    MaxDays = dr["EXPIRY_DAYS");
            //dr.Close();
            if (NewDays > MaxDays)
            {
                MessageBox.Show("Days Are Not Acceptable!", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDays.Text = oldDays.ToString();
            }
            else
            {
                blnformChange = true;
                dtpCheckOut.Value = dtpCheckIn.Value.AddDays(Convert.ToDouble(txtDays.Text + Constants.vbNullString));
                nudRent.Value = Convert.ToDecimal(Math.Round(LockerRentPerday * Convert.ToDouble(txtNoOfLockers.Text != "" ? txtNoOfLockers.Text : "0") * Convert.ToDouble(txtDays.Text + Constants.vbNullString), 2));
                nudAdvance.Value = Convert.ToDecimal(Math.Round(LockerAdvanceTariff * Convert.ToDouble(txtNoOfLockers.Text != "" ? txtNoOfLockers.Text : "0"), 2));
                nudRentPending.Value = Math.Round(nudRent.Value - nudRentPaid.Value, 2);
            }
        }
        private void txtDays_TextChanged(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
            dtpCheckOut.Value = dtpCheckIn.Value.AddDays(Convert.ToDouble(txtDays.Text + Constants.vbNullString));
            nudRent.Value = Convert.ToDecimal(Math.Round(LockerRentPerday * Convert.ToDouble(txtNoOfLockers.Text + Constants.vbNullString) * Convert.ToDouble(txtDays.Text + Constants.vbNullString), 2));
            nudAdvance.Value = Convert.ToDecimal(Math.Round(LockerAdvanceTariff * Convert.ToDouble(txtNoOfLockers.Text + Constants.vbNullString), 2));
            nudRentPending.Value = Math.Round(nudRent.Value - nudRentPaid.Value, 2);
        }

        private void txtNoOfLockers_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            //if (!((e.KeyChar >= "0" & e.KeyChar <= "9") | Asc(e.KeyChar) == 8 | Asc(e.KeyChar) == 46))
            //    e.Handled = true;
        }

        private void txtNoOfLockers_TextChanged(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
            nudRent.Value = Convert.ToDecimal(Math.Round(LockerRentPerday * Convert.ToDouble(txtNoOfLockers.Text + Constants.vbNullString) * Convert.ToDouble(txtDays.Text + Constants.vbNullString), 2));
            // nudAdvance.Value = RoundIt(LockerAdvanceTariff * Val(txtNoOfLockers.Text & vbNullString) * Val(txtDays.Text & vbNullString), 2)
            nudAdvance.Value = Convert.ToDecimal(Math.Round(LockerAdvanceTariff * Convert.ToDouble(txtNoOfLockers.Text + Constants.vbNullString), 2));
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




        private void chkLockers_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
            txtNoOfLockers.Text = chkLockers.CheckedItems.Count.ToString();
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
            clsConnection.mErrorResult = clsConnection.mErrorResult + Constants.vbNewLine + str;
        }


        private void btnLoad_Click(System.Object sender, System.EventArgs e)
        {
            DataTable dr = null;
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
                dr = objDsLockerChange.GetDrLockerChangeMst(0, "", Convert.ToInt64(strRcptNo), Convert.ToInt64(txtCounter.Tag), UserInfo.CompanyID, PrintReceiptLocId, LockerCheckOutDeptID, 0);

                if (dr != null && dr.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dr.Rows[0]["PendLockerCount"]) == 0)
                    {
                        MessageBox.Show("No pending lockers against the Receipt No. " + txtVchNo.Text, PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //dr.Close();
                        txtVchNo.Text = "";
                        txtVchNo.Focus();
                        return;
                    }

                    txtVchNo.Tag = dr.Rows[0]["CheckInMstId"];
                    dtpCheckIn.Value = Convert.ToDateTime(dr.Rows[0]["InDate"]);
                    dtpCheckInTime.Value = Convert.ToDateTime(dr.Rows[0]["InTime"]);
                    txtVchNo.Text = (dr.Rows[0]["SerialNo"] + Constants.vbNullString);
                    dtpCheckIn.Tag = dr.Rows[0]["RecordModifiedCount"];

                    NameLockerHolder = dr.Rows[0]["Name"].ToString();
                    txtName.Text = NameLockerHolder;
                    PlaceLockerHolder = dr.Rows[0]["Place"].ToString();
                    txtPlace.Text = PlaceLockerHolder;
                    LockerAdvanceTariff = Math.Round(Convert.ToDouble(dr.Rows[0]["Advance"]) / Convert.ToDouble(dr.Rows[0]["NoOfLockers"]) / Convert.ToDouble(dr.Rows[0]["Days"]), 2);
                    nudAdvance.Value = Convert.ToDecimal(LockerAdvanceTariff);

                    txtNoOfLockers.Text = dr.Rows[0]["NoOfLockers"].ToString();
                    txtmobno.Text = dr.Rows[0]["MOB_NO"].ToString();
                    LockerRentPerday = Math.Round(Convert.ToDouble(dr.Rows[0]["Rent"]) / Convert.ToDouble(dr.Rows[0]["NoOfLockers"]) / Convert.ToDouble(dr.Rows[0]["Days"]), 2);
                    nudRentPaid.Value = Convert.ToDecimal(dr.Rows[0]["Rent"]);
                    nudRent.Value = Convert.ToDecimal(dr.Rows[0]["Rent"]);
                    nudRent.Value = Convert.ToDecimal(LockerRentPerday);

                    txtDays.Tag = dr.Rows[0]["Days"];

                    txtDays.Text = dr.Rows[0]["Days"].ToString();
                    oldDays = Convert.ToInt16(dr.Rows[0]["Days"]);
                    // dtpCheckOut.Value = dr.Rows[0]["Outdate"]
                    dtpCheckOut.Value = dtpCheckIn.Value.AddDays(Convert.ToDouble(txtDays.Text + Constants.vbNullString));
                    // DateDiff(DateInterval.Day, dtpCheckIn.Value, dtpCheckOut.Value)
                    //dr.Close();

                    dr = objDsLockerChange.GetDrLockerCheckInDet(Convert.ToInt64(txtVchNo.Tag));
                    cf.FillListBox(chklockers_given, dr, "LockerName", "LockerId", "RecordModifiedCount");






                    for (i = 0; i <= chklockers_given.Items.Count - 1; i++)
                        chklockers_given.SetItemChecked(i, true);

                    // '''''FillAvailableLockers()
                    if (mScreenID == eScreenID.Lockerchange)
                        FillAvailableLockers();
                    for (i = 0; i <= chklockers_given.Items.Count - 1; i++)
                    {
                        // chklockers_given.Items.CopyTo(chkLockers, i)
                        chkLockers.Items.Insert(i, chklockers_given.Items[i]);
                        chkLockers.SetItemChecked(i, true);
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
                    if (mScreenID == eScreenID.LockerExtend)
                        chkLockers.Enabled = false;
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
                //if (dr != null)
                //    dr.Close();
                MessageBox.Show("Load Check In Details failed.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
        }

        private void txtDays_KeyUp(System.Object sender, System.Windows.Forms.KeyEventArgs e)
        {
            int NewDays = 0;
            int MaxDays = 0;
            SqlDataReader dr;

            if (txtDays.Text != "")
            {
                NewDays = Convert.ToInt16(txtDays.Text);
                if (NewDays < oldDays)
                    txtDays.Text = oldDays.ToString();
            }

            MaxDays = LockerCheckInDALobj.GetDaysLimit();
            //if (dr.Read())
            //    MaxDays = dr["EXPIRY_DAYS");
            //dr.Close();
            if (NewDays > MaxDays)
            {
                MessageBox.Show("Days Are Not Acceptable!", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDays.Text = oldDays.ToString();
            }
        }
    }

}
