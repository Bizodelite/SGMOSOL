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
using System.Data.SqlClient;
using SGMOSOL.ADMIN;
using SGMOSOL.DAL;
using static SGMOSOL.BAL.LockerBAL;
using SGMOSOL.BAL;


namespace SGMOSOL.SCREENS
{
    public partial class frmLockerCheckOut : Form
    {
        private eScreenID mScreenID;
        private bool mBlnEdit = false;
        private eAction mAction;
        private ArrayList CtrlArr = new ArrayList();
        private ArrayList btnArr = new ArrayList();
        private CommonFunctions cf = new CommonFunctions();
        LockerCheckOutDAL LockerCheckOutDALobj = new LockerCheckOutDAL();
        private bool DisableSendKeys;
        private bool bkDateEntry = false;
        private bool blnformChange;
        private string[] col;
        private string[] mStrErrMsg;
        public long mSearchId;
        private Collection mDelColl = new Collection();
        private DataTable TempTable1;
        //private DataTable TempTable2;
        private System.Data.DataSet ds = new System.Data.DataSet();
        private DataRow MyRow;
        private DateTime dtEnteredOn;
        private double LockerAdvanceTariff;
        private double LockerRentPerday;
        private string mStrCounterMachineShortName;
        private int LockerCheckOutDeptID;
        private string LockerCheckOutDeptName;
        private string LockerCheckOutLocName;
        private string NameLockerHolder;
        private long PrintReceiptLocId;
        private string PlaceLockerHolder;
        private long countedDays;
        private long oldDays;

        public frmLockerCheckOut(eScreenID ScreenID)
        {
            InitializeComponent();
            this.Closing += new CancelEventHandler(this.frmLockerCheckOut_Closing);
            mScreenID = ScreenID;
        }

        private void btnNew_Click(System.Object sender, System.EventArgs e)
        {
            DataTable dr = new DataTable();
            FormClear();
          
            cf.fncSysTime(dtpCheckOutTime);
            cf.fncSetDateAndRange(dtpCurDate);
            cf.fncSysTime(dtpCurTime);
            cf.fncSetDateAndRange(dtpCheckOut);
            cf.fncSysTime(dtpCheckOutTime);
            dtpCheckOut.Enabled = bkDateEntry;
            mAction = eAction.ActionInsert;
            cf.subLockForm(false, CtrlArr, false);
            btnPrint.Enabled = btnNew.Enabled;
            btnLoad.Enabled = true;
            chkLockers.Enabled = true;
            try
            {
                dr = LockerCheckOutDALobj.GetDrMaxSrNo(Convert.ToInt64(txtCounter.Tag), UserInfo.CompanyID, PrintReceiptLocId, 0, UserInfo.fy_id);
                if (dr.Rows.Count > 0)
                    txtVchNo.Text = (Convert.ToInt32(dr.Rows[0]["SerialNo"]) + 1).ToString();
                else
                    txtVchNo.Text = "1";
                //dr.Close();
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                //if (dr != null)
                //    dr.Close();
            }

            txtCheckInVchNo.Focus();

            blnformChange = false;
            if (mAction == eAction.ActionInsert)
                btnPrint.Enabled = false;
            else
                btnPrint.Enabled = true;
        }

        private void frmLockerCheckOut_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter & !DisableSendKeys)
                SendKeys.Send("{tab}");
            if (e.KeyCode == Keys.End & (mAction == eAction.ActionInsert | mAction == eAction.ActionUpdate) & blnformChange)
                btnSave_Click(null, null);
        }

        private void frmLockerCheckOut_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End & (mAction == eAction.ActionInsert | mAction == eAction.ActionUpdate) & blnformChange)
                btnSave_Click(null, null);
        }

        private void frmLockerCheckOut_Load(System.Object sender, System.EventArgs e)
        {
            // Me.WindowState = FormWindowState.Maximized
            // dtpCheckIn.Value = DateTime.Now
            // dtpCurDate.Value = DateTime.Now
            // dtpCurTime.Value = DateTime.Now
            // dtpCheckOut.Value = DateTime.Now
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

            if (mScreenID == eScreenID.LockerCheckOutStar)
            {
                // chkYes.Visible = True
                // lbYes.Visible = True
                // chkYes.Checked = True
                // chkYes.Enabled = False
                this.BackColor = Color.BlanchedAlmond;
                // dtpCheckIn.Enabled = True
                txtDays.Enabled = true;
                txtDays.ReadOnly = false;
                nudAdvance.ReadOnly = false;
                nudAdvance.Enabled = true;
            }

            txtUser.Text = UserInfo.UserName;
            FillCounter();

            //Int32 ctr;
            //ctr = (MDI.Size.Width - this.Size.Width) / (double)2;
            //this.Location = new Point(ctr, 0);
            CreateDs();
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
                    LockerCheckOutDeptID = (int)dr.Rows[0]["DeptId"];
                    LockerCheckOutDeptName = dr.Rows[0]["DepartmentName"].ToString();
                    PrintReceiptLocId = Convert.ToInt64(dr.Rows[0]["LocId"]);
                    LockerCheckOutLocName = dr.Rows[0]["LocName"].ToString();
                    mStrCounterMachineShortName = dr.Rows[0]["CounterMachineShortName"].ToString();
                }
                //dr.Close();
            }catch(Exception ex) { cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version); }
        }

        private void FormClear()
        {
            txtVchNo.Text = "";
            txtVchNo.Tag = null;
            txtDays.Text = "";
            // txtCheckInVchNo.Text = ""
            nudAdvance.Value = 0;
            nudTotalRent.Value = 0;
            NudRefund.Value = 0;
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
            LockerCheckOutMst LockerCheckOutMst;
            Collection coll;
            long lngSerialNo;
            string strErr;
            bool flag = false;
            if (IsValidForm() == false)
            {
                return false;
            }
            setCursor(this, false);
            lngSerialNo = Convert.ToInt64(txtVchNo.Text);
            LockerCheckOutMst = GetCheckOutMst();
            coll = GetCheckOutDetColl();
            if (mAction == eAction.ActionInsert)
            {
                lngError = LockerCheckOutDALobj.Insert(LockerCheckOutMst, coll, UserInfo.UserName, UserInfo.Machine_Name, lngSerialNo, dtEnteredOn);
                if (lngError > 0)
                    txtVchNo.Text = lngError.ToString();
            }
            else if (mAction == eAction.ActionUpdate)
                lngError = LockerCheckOutDALobj.Update(LockerCheckOutMst, coll, mDelColl, UserInfo.UserName, UserInfo.Machine_Name, lngSerialNo);
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
                if (countedDays > oldDays)
                    lngError = LockerCheckOutDALobj.UpdateDays_Rent(txtCheckInVchNo.Tag, countedDays, nudTotalRent.Value);
                // If Val(dtpCheckIn.Tag & vbNullString) = 0 Then
                blnformChange = false;
            }
            return flag;
        }

        private LockerCheckOutMst GetCheckOutMst()
        {
            LockerCheckOutMst CheckOutMst = new LockerCheckOutMst();
            CheckOutMst.CheckOutMstId = Convert.ToInt64(txtVchNo.Tag);
            CheckOutMst.ComId = UserInfo.CompanyID;
            CheckOutMst.LocId = PrintReceiptLocId;
            CheckOutMst.DeptId = LockerCheckOutDeptID;
            CheckOutMst.CtrMachId = Convert.ToInt64(txtCounter.Tag);
            CheckOutMst.FyId = UserInfo.fy_id;
            //CheckOutMst.OutDate = FormatDateToString(dtpCurDate.Value);
            CheckOutMst.OutDate = dtpCurDate.Value;
            //CheckOutMst.OutTime = Convert.ToDateTime(dtpCurTime.Text).Hour.ToString("00") + ":" + Convert.ToDateTime(dtpCurTime.Text).Minute.ToString("00") + ":00";
            CheckOutMst.OutTime = dtpCurTime.Value;
            CheckOutMst.SerialNo = Convert.ToInt64(txtVchNo.Tag);
            CheckOutMst.CheckInMstId = Convert.ToInt64(txtCheckInVchNo.Tag);
            CheckOutMst.Days = Convert.ToInt32(txtDays.Text);
            CheckOutMst.NoOfLockers = chkLockers.CheckedItems.Count;
            CheckOutMst.Advance = Convert.ToDouble(nudAdvance.Value);
            CheckOutMst.Rent = Convert.ToDouble(nudTotalRent.Value);
            CheckOutMst.Refund = Convert.ToDouble(NudRefund.Value);
            CheckOutMst.UserId = UserInfo.UserId;
            CheckOutMst.ServerName = UserInfo.serverName;
            CheckOutMst.EnteredBy = UserInfo.UserName;
            CheckOutMst.ModifiedBy = UserInfo.UserName;
            CheckOutMst.RecordModifiedCount = (Convert.ToInt32(dtpCheckOut.Tag) + 1);
            return CheckOutMst;
        }


        private Collection GetCheckOutDetColl()
        {
            Collection coll = new Collection();
            LockerCheckOutDet CheckOutDet = new LockerCheckOutDet();
            CheckOutDet.CheckOutMstId = txtVchNo.Tag != null ? (Int64)txtVchNo.Tag : 0;
            for (var i = 0; i <= chkLockers.Items.Count - 1; i++)
            {
                if (chkLockers.GetItemChecked(i))
                {
                    CheckOutDet.LockerId = cf.lsbItemData(chkLockers, i);
                    CheckOutDet.LockerAvailableStatus = (int)eTokenDetail.StatusYes;
                    CheckOutDet.LockerRecordModifiedCount = cf.lsbItemData(chkLockers, i) + 1;
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


            if (chkLockers.CheckedItems.Count == 0)
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
            string ProcErr;
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
            frmSearchNew form1 = new frmSearchNew("LOCK_LOCKER_CHECK_OUT_MST_FIND_V",true, eModType.Locker);
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
                    btnPrint.Enabled = true;
                }
                else
                {
                    mAction = eAction.ActionLocked;
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
            btnPrint.Enabled = mBlnEdit;
            if (btnPrint.Enabled)
                btnPrint.Enabled = !blnFormLock;
            btnPrint.Enabled = true;
            setCursor(this, true);
        }

        private bool LoadTransaction(long lngSearchId, ref bool blnFormLock)
        {
            DataTable dr = new DataTable();
            LockerCheckOutDet CheckOutDet = new LockerCheckOutDet();
            int ctr = 0;
            bool blnFlag = false;
            mDelColl = new Collection();
            FormClear();
            try
            {
                dr = LockerCheckOutDALobj.GetDrLockerCheckOutMst(lngSearchId);
                if (dr.Rows.Count > 0)
                {
                    // txtAppNo.Text = dr("AppNo")
                    dtpCheckIn.Value = Convert.ToDateTime(dr.Rows[0]["InDate"]);
                    dtpCheckInTime.Value = Convert.ToDateTime(dr.Rows[0]["InTime"]);
                    txtCheckInVchNo.Text = dr.Rows[0]["InSerialNo"].ToString();
                    txtCheckInVchNo.Tag = dr.Rows[0]["CheckInMstId"];
                    txtDays.Tag = dr.Rows[0]["InDays"];
                    dtpCheckIn.Tag = dr.Rows[0]["InRecordModifiedCount"];

                    NameLockerHolder = dr.Rows[0]["Name"].ToString();
                    PlaceLockerHolder = dr.Rows[0]["Place"].ToString();

                    txtVchNo.Text = dr.Rows[0]["SerialNo"].ToString();
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
                //dr.Close();


                dr = LockerCheckOutDALobj.GetDrLockerCheckOutDet(lngSearchId, true, Convert.ToInt64(txtCounter.Tag));
                mDelColl = new Collection();
                //while (dr.Read())
                    foreach (DataRow row in dr.Rows)    
                {
                    if ((Convert.ToInt32(row["LockerStatus"]) != (int)eTokenDetail.StatusActive || Convert.ToInt32(row["AvailableStatus"]) != (int)eTokenDetail.StatusYes) && Convert.ToInt32(row["LockerCheckOutDetId"]) > 0)
                        blnFormLock = true;

                    if (row["LockerName"].ToString() != "")
                        chkLockers.Items.Add(new clsItemData(row["LockerName"].ToString(), Convert.ToInt32(row["LockerId"]), row["RecordModifiedCount"].ToString()));
                    if (Convert.ToInt32(row["LockerCheckOutDetId"]) > 0 && row["LockerName"].ToString() != "")
                    {
                        ctr = ctr + 1;
                        chkLockers.SetItemChecked(ctr - 1, true);
                        CheckOutDet = new LockerCheckOutDet();
                        CheckOutDet.CheckOutMstId = (long)txtVchNo.Tag;
                        CheckOutDet.LockerId = Convert.ToInt32(row["LockerId"]);
                        CheckOutDet.LockerAvailableStatus = (int)eTokenDetail.StatusNo;
                        CheckOutDet.LockerRecordModifiedCount = Convert.ToInt64(row["RecordModifiedCount"]) + 1;
                        CheckOutDet.CheckOutDetId = Convert.ToInt64(row["LockerCheckOutDetId"]);
                        CheckOutDet.CheckOutMstId = Convert.ToInt64(row["LockerCheckOutMstId"]);
                        mDelColl.Add(CheckOutDet);
                    }
                }
                //dr.Close();

                LockerAdvanceTariff = Math.Round(Convert.ToDouble(nudAdvance.Tag) / Convert.ToDouble(chkLockers.Items.Count) / Convert.ToDouble(txtDays.Tag), 2);
                LockerRentPerday = Math.Round(Convert.ToDouble(nudTotalRent.Tag) / Convert.ToDouble(chkLockers.Items.Count) / Convert.ToDouble(txtDays.Tag), 2);

                blnFlag = true;
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                //if (dr != null)
                //    dr.Close();
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

        private void frmLockerCheckOut_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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
                            if (btnPrint.Enabled == false)
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
            }
        }

        private void CalculateAmt()
        {
            double dblAmt = 0;
        }

        private void btnPrint_Click(System.Object sender, System.EventArgs e)
        {
            if (Convert.ToInt32(dtpCheckIn.Tag) == 0 | blnformChange)
                return;
            string strReportName;
            
            Collection pColl = new Collection();
            setCursor(this, false);
            System.Drawing.Printing.PrintDocument printDoc = new System.Drawing.Printing.PrintDocument();
            string printD1;
            // myPrinters.SetDefaultPrinter(System.Configuration.ConfigurationSettings.AppSettings.Get("Printer_name1"))
            // printD1 = printDoc.PrinterSettings.PrinterName
            // If printDoc.PrinterSettings.PrinterName = printD1 Then
            strReportName = "LockerCheckOutReceipt.rpt";
            FillDataInDataset();

            frmReportViewer frm = new frmReportViewer("PRINT", null, null, TempTable1);
            frm.createReport("LockerCheckOut");
            //Form sForm = new frmCrystalViewer(UserInfo.ReportPath + strReportName, null, ds, null, pColl, eReportID.LockerCheckOut, true);
            //sForm.Text = "Locker Check Out : " + eReportID.LockerCheckOut;
            //sForm.Show();
            setCursor(this, true);
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

            TempTable1.Rows.Clear();
            //TempTable2.Rows.Clear();

            MyRow = TempTable1.NewRow();
            MyRow["CHECK_OUT_MST_ID"] = 1;
            MyRow["LOC_SH_NAME"] = LockerCheckOutLocName;
            MyRow["DEPT_SH_NAME"] = LockerCheckOutDeptName;
            MyRow["COUNTER"] = mStrCounterMachineShortName;
                 
            MyRow["IN_DATE"] = dtpCheckIn.Value;
            MyRow["IN_TIME"] = dtpCheckInTime.Value;
            MyRow["SERIAL_NO"] = txtVchNo.Text;
            MyRow["APP_NO"] = "0";
                 
            MyRow["NAME"] = NameLockerHolder;
            MyRow["PLACE"] = PlaceLockerHolder;
            MyRow["DAYS"] = txtDays.Text;
                 
            MyRow["NO_OF_LOCKERS"] = chkLockers.CheckedItems.Count;
            MyRow["OUT_DATE"] = dtpCurDate.Value;
            MyRow["OUT_TIME"] = dtpCurTime.Text;
                 
            MyRow["ADVANCE"] = nudAdvance.Value;
            MyRow["RENT"] = nudTotalRent.Value;
            MyRow["REFUND"] = NudRefund.Value;
                 
            MyRow["USER_NAME"] = txtUser.Text;
            MyRow["SERVER_NAME"] = UserInfo.serverName;
            MyRow["MACHINE_NAME"] = UserInfo.Machine_Name;
            MyRow["AMT_IN_WORDS"] = cf.getNumbersInWords(nudAdvance.Value.ToString(), eCurrencyType.Rupees);

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
            //MyRow = TempTable2.NewRow;
            //MyRow["CHECK_OUT_MST_ID"] = 1;
            MyRow["LOCKER_NAME"] = Roomname;
            TempTable1.Rows.Add(MyRow);
            //TempTable2.Rows.Add(MyRow);
        }

        private void CreateDs()
        {
            TempTable1 = new DataTable("LOCK_LOCKER_CHECK_OUT_MST_T");
            //TempTable2 = new DataTable("LOCK_LOCKER_CHECK_OUT_DET_T");

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

            TempTable1.Columns.Add("NO_OF_LOCKERS", System.Type.GetType("System.Int32"));
            TempTable1.Columns.Add("OUT_DATE", System.Type.GetType("System.DateTime"));
            TempTable1.Columns.Add("OUT_TIME", System.Type.GetType("System.DateTime"));

            TempTable1.Columns.Add("ADVANCE", System.Type.GetType("System.Double"));
            TempTable1.Columns.Add("RENT", System.Type.GetType("System.Double"));
            TempTable1.Columns.Add("REFUND", System.Type.GetType("System.Double"));

            TempTable1.Columns.Add("USER_NAME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("SERVER_NAME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("MACHINE_NAME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("AMT_IN_WORDS", System.Type.GetType("System.String"));

            //TempTable2.Columns.Add("CHECK_OUT_MST_ID", System.Type.GetType("System.Int64"));
            //TempTable2.Columns.Add("LOCKER_NAME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("LOCKER_NAME", System.Type.GetType("System.String"));

            ds.Tables.Add(TempTable1);
            //ds.Tables.Add(TempTable2);
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

        private void txtVchNo_TextChanged(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
        }

        // Private Sub txtAppNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        // blnformChange = True
        // End Sub

        // Private Sub txtAppNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        // If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or Asc(e.KeyChar) = 8 Or Asc(e.KeyChar) = 46) Then
        // e.Handled = True
        // End If
        // End Sub

        private void chkLockers_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
        }

        private bool ShowValidateError(Control myObject, int tabIndex, string[] ErrMsg, int ErrNo)
        {
            setCursor(this, true);
            MessageBox.Show(cf.GetErrorMessage(ErrMsg, ErrNo), PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            myObject.Focus();
            return false;
        }

        // Private Sub Label12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label12.Click

        // End Sub

        private void btnLoad_Click(System.Object sender, System.EventArgs e)
        {
            DataTable dr = new DataTable();
            // Dim strApp As String
            long lngError = -1;
            string strRcptNo;
            long strchkinRcptno;
            long DiffHour;

            long totalrent;
            // Dim diff As Long
            // Dim diff1 As System.TimeSpan
            // Dim date1 As Date
            // Dim date2 As Date

            if (txtCheckInVchNo.Text == "")
            {
                mStrErrMsg = new string[1];
                mStrErrMsg[0] = "Rcpt No";
                ShowValidateError(txtCheckInVchNo, 0, mStrErrMsg, 1);
                return;
            }
            // strApp = txtAppNo.Text
            btnNew_Click(null, null);

            strchkinRcptno = Convert.ToInt64(txtCheckInVchNo.Text);
            strRcptNo = txtVchNo.Text;
            FormClear();
            // '''''fncSetDateAndRange(dtpCheckOut)
            cf.fncSysTime(dtpCheckOutTime);
            // txtAppNo.Text = strApp
            txtVchNo.Text = strRcptNo;


            try
            {
                // dr = objDsLockerCheckInMst.GetDrLockerCheckInMst(, , , txtCounter.Tag, UserInfo.CompanyID, UserInfo.LocationID, LockerCheckOutDeptID, , , Val(txtAppNo.Text & vbNullString))
                dr = LockerCheckOutDALobj.GetDrLockerCheckInMst(0, "", strchkinRcptno, Convert.ToInt64(txtCounter.Tag), UserInfo.CompanyID, PrintReceiptLocId, LockerCheckOutDeptID, 0, "");

                if (dr.Rows.Count > 0)
                {
                    if (Convert.ToInt64(dr.Rows[0]["PendLockerCount"]) == 0)
                    {
                        MessageBox.Show("No pending lockers against the Receipt No. " + txtCheckInVchNo.Text, PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //dr.Close();
                        txtCheckInVchNo.Text = "";
                        txtCheckInVchNo.Focus();
                        return;
                    }

                    txtCheckInVchNo.Tag = dr.Rows[0]["CheckInMstId"];
                    dtpCheckIn.Value = Convert.ToDateTime(dr.Rows[0]["InDate"]);
                    dtpCheckInTime.Value = Convert.ToDateTime(dr.Rows[0]["InTime"]);
                    // dtpCheckOut.MaxDate = Convert.ToDateTime("12/31/9998 00:00:00")
                    dtpCheckOut.MaxDate = Convert.ToDateTime(dr.Rows[0]["MaxDate"]);
                   // dtpCheckOut.Value = Convert.ToDateTime(dr.Rows[0]["OutDate"]);
                    dtpCheckOutTime.Value = Convert.ToDateTime(dr.Rows[0]["OutTime"]);
                    txtCheckInVchNo.Text = dr.Rows[0]["SerialNo"].ToString();
                    dtpCheckIn.Tag = Convert.ToInt64(dr.Rows[0]["RecordModifiedCount"]);

                    NameLockerHolder = dr.Rows[0]["Name"].ToString();
                    PlaceLockerHolder = dr.Rows[0]["Place"].ToString();


                    // txtDays.Tag = dr("Days")


                    txtDays.Text = DateDiff(DateInterval.Day, dtpCheckIn.Value, dtpCurDate.Value);
                    countedDays = Convert.ToInt64(txtDays.Text);

                    // If txtDays.Text = 0 Then
                    // txtDays.Text = "1"
                    // End If
                    DiffHour = Convert.ToInt64(dr.Rows[0]["CALC_HOUR"]);
                    if (DiffHour <= (24 * 60))
                        txtDays.Text = 1.ToString();
                    else
                    {
                        txtDays.Text = Math.Ceiling(DiffHour / (double)(24 * 60)).ToString();
                        if (((DiffHour % (24 * 60)) > 0 & (DiffHour % (24 * 60)) <= (2 * 60)))
                            txtDays.Text = (Convert.ToInt64(txtDays.Text) - 1).ToString();
                    }

                    oldDays = Convert.ToInt64(dr.Rows[0]["Days"]);
                    totalrent = Convert.ToInt64(dr.Rows[0]["Rent"]);

                    if (mScreenID == eScreenID.LockerCheckOutStar)
                    {
                        LockerAdvanceTariff = Math.Round(Convert.ToDouble(dr.Rows[0]["Advance"]) / Convert.ToDouble(dr.Rows[0]["NoOfLockers"]) / Convert.ToDouble(dr.Rows[0]["Days"]), 2);
                        LockerRentPerday = Math.Round(Convert.ToDouble(dr.Rows[0]["Rent"]) / Convert.ToDouble(dr.Rows[0]["NoOfLockers"]) / Convert.ToDouble(dr.Rows[0]["Days"]), 2);
                        nudTotalRent.Value = Convert.ToDecimal(LockerRentPerday) * Convert.ToDecimal(txtDays.Text) * Convert.ToDecimal(dr.Rows[0]["NoOfLockers"]);
                    }
                    else if (mScreenID == eScreenID.LockerCheckOut)
                    {
                        if (txtDays.Text != dr.Rows[0]["Days"].ToString())
                        {
                            LockerAdvanceTariff = Math.Round(Convert.ToDouble(dr.Rows[0]["Advance"]) / Convert.ToDouble(dr.Rows[0]["NoOfLockers"]) / Convert.ToDouble(txtDays.Text), 2);
                            LockerRentPerday = Math.Round(Convert.ToDouble(dr.Rows[0]["Rent"]) / Convert.ToDouble(dr.Rows[0]["NoOfLockers"]) / Convert.ToDouble(txtDays.Text), 2);
                            nudTotalRent.Value = Convert.ToDecimal(dr.Rows[0]["Rent"]);
                        }
                        else
                        {
                            LockerAdvanceTariff = Math.Round(Convert.ToDouble(dr.Rows[0]["Advance"]) / Convert.ToDouble(dr.Rows[0]["NoOfLockers"]) / Convert.ToDouble(dr.Rows[0]["Days"]), 2);
                            LockerRentPerday = Math.Round(Convert.ToDouble(dr.Rows[0]["Rent"]) / Convert.ToDouble(dr.Rows[0]["NoOfLockers"]) / Convert.ToDouble(dr.Rows[0]["Days"]), 2);
                            nudTotalRent.Value = Convert.ToDecimal(dr.Rows[0]["Rent"]);
                        }
                    }
                    nudAdvance.Value = Convert.ToDecimal(dr.Rows[0]["Advance"]);
                    NudRefund.Value = Convert.ToDecimal(dr.Rows[0]["Advance"]) + (totalrent - (nudTotalRent.Value));

                    //dr.Close();
                    dr = LockerCheckOutDALobj.GetDrLockerCheckInDet(Convert.ToInt64(txtCheckInVchNo.Tag));

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

                    cf.FillListBox(chkLockers, dr, "LockerName", "LockerId", "RecordModifiedCount");
                    for (var i = 0; i <= chkLockers.Items.Count - 1; i++)
                        chkLockers.SetItemChecked(i, true);
                }
                else
                {
                    MessageBox.Show("Receipt No. not found." + txtCheckInVchNo.Text, PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //dr.Close();
                    txtCheckInVchNo.Text = "";
                    txtCheckInVchNo.Focus();
                }
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                //if (dr != null)
                //    dr.Close();
                MessageBox.Show("Load Check In Details failed.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Private Function GetCheckInDetColl() As Collection
        // Dim coll As New Collection
        // Dim CheckInDet As New OSOL_CONNECTION.LockerCheckInDet
        // CheckInDet.CheckInMstId = Val(txtVchNo.Tag & vbNullString)
        // For i = 0 To chkLockers.Items.Count - 1
        // If chkLockers.GetItemChecked(i) Then
        // CheckInDet.LockerId = lsbItemData(chkLockers, i)
        // CheckInDet.LockerAvailableStatus = TokenDetail.StatusNo
        // CheckInDet.LockerRecordModifiedCount = Val(lsbItemName2(chkLockers, i) & vbNullString) + 1
        // coll.Add(CheckInDet)
        // End If
        // Next
        // GetCheckInDetColl = coll
        // End Function






        private void txtCheckInVchNo_TextChanged(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
        }

        private void txtCheckInVchNo_KeyPress(System.Object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            //if (!((e.KeyChar >= "0" & e.KeyChar <= "9"] | Asc(e.KeyChar) == 8 | Asc(e.KeyChar) == 46))
            //    e.Handled = true;
        }

    }
}
