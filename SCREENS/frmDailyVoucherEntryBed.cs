using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SGMOSOL.ADMIN.CommonFunctions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using SGMOSOL.ADMIN;
using System.Data.SqlClient;
using static SGMOSOL.BAL.LockerBAL;
using SGMOSOL.DAL;

namespace SGMOSOL.SCREENS
{
    public partial class frmDailyVoucherEntryBed : Form
    {
        private CommonFunctions cf = new CommonFunctions();
        private DailyVoucherTransactionDAL mClsDailyVoucher = new DailyVoucherTransactionDAL();
        private Int64 LocID;
        private Collection mDelColl = new Collection();
        private eScreenID mScreenId;
        private Int64 intCtrMachId;
        private string intCtrMachName;
        private Int64 ReportDeptId;
        private Int16 MOD_TYPE;
        private string MOD_NAME;

        public frmDailyVoucherEntryBed(eScreenID ScreenID)
        {

            // This call is required by the designer.
            InitializeComponent();
            this.nudToSerialNo.GotFocus += nudToSerialNo_GotFocus;
            mScreenId = ScreenID;
            if (mScreenId == eScreenID.DailyVoucherEntryBed)
            {
                this.Text = "Daily Voucher Entry Bed";
                MOD_NAME = "Bed";
            }
            else if (mScreenId == eScreenID.DailyVoucherEntryBhaktniwas)
            {
                this.Text = "Daily Voucher Entry Bhaktniwas";
                MOD_NAME = "Bhaktniwas";
            }
            else if (mScreenId == eScreenID.DailyVoucherEntryBhojnalay)
            {
                this.Text = "Daily Voucher Entry Bhojnalay";
                MOD_NAME = "Bhojnalay";
            }
            else if (mScreenId == eScreenID.DailyVoucherEntryGame)
            {
                this.Text = "Daily Voucher Entry Game";
                MOD_NAME = "Game";
            }
            else if (mScreenId == eScreenID.DailyVoucherEntryToyTrain)
            {
                this.Text = "Daily Voucher Entry Toy Train";
                MOD_NAME = "Toy Train";
            }
            else if (mScreenId == eScreenID.DailyVoucherEntryEntryGate)
            {
                this.Text = "Daily Voucher Entry Entry Gate";
                MOD_NAME = "Entry Gate";
            }
            else if (mScreenId == eScreenID.DailyVoucherEntryLocker)
            {
                this.Text = "Daily Voucher Entry Locker";
                MOD_NAME = "Locker";
            }
            else if (mScreenId == eScreenID.DailyVoucherEntryDengi)
            {
                this.Text = "Daily Voucher Entry Dengi";
                MOD_NAME = "Dengi";
            }
            else if (mScreenId == eScreenID.DailyVoucherEntryMedical)
            {
                this.Text = "Daily Voucher Entry Medical";
                MOD_NAME = "Medical";
            }
        }
        private void DailyVoucherEntryBed_Load(System.Object sender, System.EventArgs e)
        {
            if (mScreenId == eScreenID.DailyVoucherEntryBed)
                MOD_TYPE = 8;
            else if (mScreenId == eScreenID.DailyVoucherEntryBhaktniwas)
                MOD_TYPE = 7;
            else if (mScreenId == eScreenID.DailyVoucherEntryBhojnalay)
                MOD_TYPE = 1;
            else if (mScreenId == eScreenID.DailyVoucherEntryGame)
                MOD_TYPE = 2;
            else if (mScreenId == eScreenID.DailyVoucherEntryToyTrain)
                MOD_TYPE = 3;
            else if (mScreenId == eScreenID.DailyVoucherEntryEntryGate)
                MOD_TYPE = 4;
            else if (mScreenId == eScreenID.DailyVoucherEntryLocker)
                MOD_TYPE = 5;
            else if (mScreenId == eScreenID.DailyVoucherEntryDengi)
                MOD_TYPE = 6;
            else if (mScreenId == eScreenID.DailyVoucherEntryMedical)
                MOD_TYPE = 11;
            rbDatewise.Select();
            ScreenToCenter();
            cf.fncSetDateAndRange(dtpToDt);
            FillCounter();
            btnNew_Click(null, null);
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
            DataTable dr;
            dr = cf.GetDrCounterMachId(UserInfo.UserId, SystemHDDModelNo, SystemHDDSerialNo, SystemMacID, MOD_TYPE);
            cf.FillCombo(cboCounter, dr, "CounterMachineTitle", "CtrMachId", "DeptId", "LocId");
            if (cboCounter.Items.Count > 0)
            {
                cboCounter.SelectedIndex = 0;
                ReportDeptId = cf.cmbItemName2(cboCounter, cboCounter.SelectedIndex);

                LocID = cf.cmbItemName3(cboCounter, cboCounter.SelectedIndex);
            }

            cboCounter.Enabled = true;
        }
        private Collection GetPrnRcptDtDetColl()
        {
            Collection coll = new Collection();
            DailyVoucherTransaction PrnRcptDtDet = new DailyVoucherTransaction();

            double dblAmt = 0;
            // Dim item_name As String = ""
            // item_name = ""
            string item_name1;
            string strFromDate;
            string strToDate;

            {
                var withBlock = fpsDailyTrans;
                for (int ctr = 0; ctr <= withBlock.RowCount - 1; ctr++)
                {
                    // PrnRcptDtDet.PrintReceiptMstId = Val(txtVchNo.Tag & vbNullString)
                    strFromDate = fpsDailyTrans.Rows[ctr].Cells[0].Value.ToString().Trim();
                    strToDate = fpsDailyTrans.Rows[ctr].Cells[1].Value.ToString().Trim();
                    // DateTime.ParseExact
                    PrnRcptDtDet.fDate = DateTime.ParseExact(Strings.Trim(strFromDate), "dd/MM/yyyy", CultureInfo.InvariantCulture); // Val(.Cells(ctr, DayStkAcc.fDate).Value & vbNullString)
                    PrnRcptDtDet.TDate = DateTime.ParseExact(Strings.Trim(strToDate), "dd/MM/yyyy", CultureInfo.InvariantCulture); // Val(.Cells(ctr, DayStkAcc.fDate).Value & vbNullString)
                    PrnRcptDtDet.MinSerialNo = Convert.ToInt64(withBlock.Rows[ctr].Cells[2].Value + Constants.vbNullString);
                    PrnRcptDtDet.MaxSerialNo = Convert.ToInt64(withBlock.Rows[ctr].Cells[3].Value + Constants.vbNullString);
                    PrnRcptDtDet.TotalAmount = Convert.ToDouble(withBlock.Rows[ctr].Cells[4].Value + Constants.vbNullString);
                    PrnRcptDtDet.TotalReceipt = Convert.ToDouble(withBlock.Rows[ctr].Cells[5].Value + Constants.vbNullString);
                    PrnRcptDtDet.Status = (withBlock.Rows[ctr].Cells[6].Tag + Constants.vbNullString);
                    PrnRcptDtDet.LocationName = lblLocationName.Text;
                    PrnRcptDtDet.ModName = MOD_NAME;
                    coll.Add(PrnRcptDtDet);
                }
            }

            return coll;
        }

        private void btnLoad_Click(System.Object sender, System.EventArgs e)
        {
            string strFromDate;
            string strToDate = "";
            DataTable dr = null;
            DataTable drpend = null;
            MessDayStockAccDet DayStkAccDet = new MessDayStockAccDet();
            string strReportName;
            System.Data.DataSet ds = new System.Data.DataSet();
            Form sForm;
            Collection pColl = new Collection();
            Int16 payTypeid;



            if ((rbDatewise.Checked))
                strToDate = FormatDateToString(dtpToDt.Value);
            else if ((rbSerialNo.Checked))
            {
                if (Convert.ToInt32(nudToSerialNo.Text + Constants.vbNullString) <= 0)
                {
                    MessageBox.Show("Please enter valid Receipt No.", PrjMsgBoxTitle, MessageBoxButtons.OK);
                    nudToSerialNo.Focus();
                    // IsValidForm = False
                    return;
                }
            }


            // strFromDate = FormatDateToString(dtpFromDt.Value)

            // strFromTime = (dtpFromTime.Text)
            // strToTime = (dtpToTime.Text)
            // payTypeid = cmbItemdata(cboPaymentType, cboPaymentType.SelectedIndex)

            intCtrMachId = cf.cmbItemdata(cboCounter, cboCounter.SelectedIndex);
            intCtrMachName = cboCounter.Text;
            if (mScreenId == eScreenID.DailyVoucherEntryBhojnalay | mScreenId == eScreenID.DailyVoucherEntryGame | mScreenId == eScreenID.DailyVoucherEntryToyTrain)
                drpend = mClsDailyVoucher.GetDrPendingTransactionByCTR_MAC_ID(strToDate, intCtrMachId, LocID, UserInfo.fy_id, MOD_TYPE);
            else
                drpend = mClsDailyVoucher.GetDrPendingTransaction(strToDate, intCtrMachId, LocID, UserInfo.fy_id, MOD_TYPE);

            var withBlock = FpSpreadPendingDetails;
            withBlock.RowCount = 0;
            //while (drpend.Read())
            foreach (DataRow drpendrow in drpend.Rows)
            {
                withBlock.RowCount = withBlock.RowCount + 1;
                withBlock.Rows[withBlock.RowCount - 1].Cells[0].Value = drpendrow["ToDate"];
                withBlock.Rows[withBlock.RowCount - 1].Cells[1].Value = drpendrow["MinSerialNo"];
                withBlock.Rows[withBlock.RowCount - 1].Cells[2].Value = drpendrow["MaxSerialNo"];
                withBlock.Rows[withBlock.RowCount - 1].Cells[3].Value = drpendrow["TotalAmount"];
                withBlock.Rows[withBlock.RowCount - 1].Cells[4].Value = drpendrow["TotalReceipt"];
                withBlock.Rows[withBlock.RowCount - 1].Cells[5].Value = drpendrow["StatusName"];
                // .Cells(.RowCount - 1, DayStkAcc.Status).Tag = 76
                mDelColl.Add(DayStkAccDet);
            }
            //drpend.Close();

            if (mScreenId == eScreenID.DailyVoucherEntryBed)
                dr = mClsDailyVoucher.GetDrDailyVoucherBed(strToDate, LocID, UserInfo.fy_id, MOD_TYPE, Convert.ToInt32(nudToSerialNo.Text));
            else if (mScreenId == eScreenID.DailyVoucherEntryBhaktniwas)
                dr = mClsDailyVoucher.GetDrDailyVoucherBhaktniwas(strToDate, LocID, UserInfo.fy_id, MOD_TYPE, Convert.ToInt32(nudToSerialNo.Text));
            else if (mScreenId == eScreenID.DailyVoucherEntryBhojnalay)
                dr = mClsDailyVoucher.GetDrDailyVoucherBhojnalay(strToDate, intCtrMachId, UserInfo.fy_id, MOD_TYPE, Convert.ToInt32(nudToSerialNo.Text));
            else if (mScreenId == eScreenID.DailyVoucherEntryGame)
                dr = mClsDailyVoucher.GetDrDailyVoucherGame(strToDate, intCtrMachId, UserInfo.fy_id, MOD_TYPE, Convert.ToInt32(nudToSerialNo.Text));
            else if (mScreenId == eScreenID.DailyVoucherEntryToyTrain)
                dr = mClsDailyVoucher.GetDrDailyVoucherToyTrain(strToDate, intCtrMachId, UserInfo.fy_id, MOD_TYPE, Convert.ToInt32(nudToSerialNo.Text));
            else if (mScreenId == eScreenID.DailyVoucherEntryLocker)
                dr = mClsDailyVoucher.GetDrDailyVoucherLocker(strToDate, LocID, UserInfo.fy_id, MOD_TYPE, Convert.ToInt32(nudToSerialNo.Text));
            else if (mScreenId == eScreenID.DailyVoucherEntryDengi)
                dr = mClsDailyVoucher.GetDrDailyVoucherDengi(strToDate, LocID, UserInfo.fy_id, MOD_TYPE, Convert.ToInt32(nudToSerialNo.Text));
            else if (mScreenId == eScreenID.DailyVoucherEntryEntryGate)
                dr = mClsDailyVoucher.GetDrDailyVoucherEntryGate(strToDate, LocID, UserInfo.fy_id, MOD_TYPE, Convert.ToInt32(nudToSerialNo.Text));
            else if (mScreenId == eScreenID.DailyVoucherEntryMedical)
                dr = mClsDailyVoucher.GetDrDailyVoucherMedical(strToDate, intCtrMachId, UserInfo.fy_id, MOD_TYPE, Convert.ToInt32(nudToSerialNo.Text));

                withBlock = fpsDailyTrans;
                withBlock.RowCount = 0;
                //while (dr.Read())
                foreach (DataRow dritem in dr.Rows)
                {
                    lblLocationName.Text = dritem["LOCATION_NAME"].ToString();
                    withBlock.RowCount = withBlock.RowCount + 1;
                    withBlock.Rows[withBlock.RowCount - 1].Cells[0].Value = dritem["FROM_DATE"];
                    //withBlock.Rows[withBlock.RowCount - 1].Cells[(int)eEntryGate.fDate].Text = dr("FROM_DATE");
                    withBlock.Rows[withBlock.RowCount - 1].Cells[1].Value = dritem["TO_DATE"];
                    //withBlock.Rows[withBlock.RowCount - 1].Cells[(int)eEntryGate.tDate].Text = dr("TO_DATE");
                    withBlock.Rows[withBlock.RowCount - 1].Cells[2].Value = dritem["FIRST_RECEIPT_NO"];
                    withBlock.Rows[withBlock.RowCount - 1].Cells[3].Value = dritem["LAST_RECEIPT_NO"];
                    withBlock.Rows[withBlock.RowCount - 1].Cells[4].Value = dritem["TOTAL_AMOUNT"];
                    withBlock.Rows[withBlock.RowCount - 1].Cells[5].Value = dritem["RECEIPT_COUNT"];
                    withBlock.Rows[withBlock.RowCount - 1].Cells[6].Value = "PENDING";
                    withBlock.Rows[withBlock.RowCount - 1].Cells[6].Tag = 76;
                    mDelColl.Add(DayStkAccDet);
                }
                //dr.Close();
            
        }

        private void btnNew_Click(System.Object sender, System.EventArgs e)
        {
            FpSpreadPendingDetails.RowCount = 0;
            fpsDailyTrans.RowCount = 0;
            nudToSerialNo.ReadOnly = false;
            nudToSerialNo.Value = 0;
            btnSubmit.Enabled = true;
        }

        private void btnSubmit_Click(System.Object sender, System.EventArgs e)
        {
            Collection coll;
            long lngError = -1;
            int rowcount;
            // If fpsStockDet.Sheets(0).RowCount > 0 Then
            // MessageBox.Show("Please firest Clear The Pending Transaction.")
            // Exit Sub
            // End If


            rowcount = fpsDailyTrans.RowCount;
            if (rowcount <= 0)
            {
                MessageBox.Show("Please load transaction");
                return;
            }

            coll = GetPrnRcptDtDetColl();
            lngError = mClsDailyVoucher.InsertMst(coll, UserInfo.UserName, UserInfo.Machine_Name, intCtrMachId, LocID, ReportDeptId, UserInfo.UserId, UserInfo.CompanyID, UserInfo.fy_id, MOD_TYPE, intCtrMachName, FormatDateToString(dtpToDt.Value), 0);
            if (lngError >= 0)
            {
                MessageBox.Show("Record Inserted Successfully...");
                btnNew_Click(null, null);
                return;
            }
            else
                MessageBox.Show("Record Not Inserted Successfully. Please Try again.");
        }


        private void btnClose_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void nudToSerialNo_GotFocus(object sender, System.EventArgs e)
        {
            nudToSerialNo.Select(0, nudToSerialNo.ToString().Length);
        }

        private void rbDatewise_CheckedChanged(System.Object sender, System.EventArgs e)
        {
        }

        private void rbDatewise_Click(object sender, System.EventArgs e)
        {
            nudToSerialNo.Visible = false;
            dtpToDt.Visible = true;
            lblToDate.Visible = true;
            lblSerialNo.Visible = false;
            btnNew_Click(null, null);
        }



        private void rbSerialNo_Click(object sender, System.EventArgs e)
        {
            dtpToDt.Visible = false;
            // dtpToDt.Text = ""
            nudToSerialNo.Visible = true;
            lblToDate.Visible = false;
            lblSerialNo.Visible = true;
            btnNew_Click(null, null);
        }

        private void rbSerialNo_CheckedChanged(System.Object sender, System.EventArgs e)
        {
        }


        private void Button1_Click(System.Object sender, System.EventArgs e)
        {
            string strFromDate;
            string strToDate = "";
            DataTable dr = new DataTable();
            SqlDataReader drpend = null;

            MessDayStockAccDet DayStkAccDet = new MessDayStockAccDet();
            string strReportName;
            System.Data.DataSet ds = new System.Data.DataSet();
            Form sForm;
            Collection pColl = new Collection();
            Int64 UpdateId = 0;
            long DengiId;
            Collection coll;
            long lngError = -1;

            btnNew_Click(null, null);
            btnSubmit.Enabled = false;
            if ((rbDatewise.Checked))
                strToDate = FormatDateToString(dtpToDt.Value);
            else if ((rbSerialNo.Checked))
            {
                if (Convert.ToInt32(nudToSerialNo.Text + Constants.vbNullString) <= 0)
                {
                    MessageBox.Show("Please enter valid Receipt No.", PrjMsgBoxTitle, MessageBoxButtons.OK);
                    nudToSerialNo.Focus();
                    // IsValidForm = False
                    return;
                }
            }


            intCtrMachId = cf.cmbItemdata(cboCounter, cboCounter.SelectedIndex);
            intCtrMachName = cboCounter.Text;

            // If mScreenId = eScreenID.DailyVoucherEntryBhojnalay Or mScreenId = eScreenID.DailyVoucherEntryGame Or mScreenId = eScreenID.DailyVoucherEntryToyTrain Then
            // drpend = mClsDailyVoucher.GetDrPendingTransactionByCTR_MAC_ID(strToDate, intCtrMachId, LocID, UserInfo.fy_id, MOD_TYPE)
            // Else
            // drpend = mClsDailyVoucher.GetDrPendingTransaction(strToDate, intCtrMachId, LocID, UserInfo.fy_id, MOD_TYPE)
            // End If

            // With FpSpreadPendingDetails.Sheets(0)
            // .RowCount = 0
            // While drpend.Read()
            // .RowCount = .RowCount + 1
            // .Cells(.RowCount - 1, EntryGate.fDate).Value = drpend("ToDate")
            // .Cells(.RowCount - 1, EntryGate.fDate).Text = drpend("ToDate")
            // .Cells(.RowCount - 1, EntryGate.MinSerial).Value = drpend("MinSerialNo")
            // .Cells(.RowCount - 1, EntryGate.MaxSerial).Value = drpend("MaxSerialNo")
            // .Cells(.RowCount - 1, EntryGate.TatalAmount).Value = drpend("TotalAmount")
            // .Cells(.RowCount - 1, EntryGate.TatalTransCount).Value = drpend("TotalReceipt")
            // .Cells(.RowCount - 1, EntryGate.Status).Text = drpend("StatusName")
            // '.Cells(.RowCount - 1, DayStkAcc.Status).Tag = 76
            // mDelColl.Add(DayStkAccDet)
            // End While
            // drpend.Close()
            // End With

            if (mScreenId == eScreenID.DailyVoucherEntryBed)
                dr = mClsDailyVoucher.GetDrDailyVoucherBed(strToDate, LocID, UserInfo.fy_id, MOD_TYPE, Convert.ToInt32(nudToSerialNo.Text));
            else if (mScreenId == eScreenID.DailyVoucherEntryBhaktniwas)
                dr = mClsDailyVoucher.GetDrDailyVoucherBhaktniwas(strToDate, LocID, UserInfo.fy_id, MOD_TYPE, Convert.ToInt32(nudToSerialNo.Text));
            else if (mScreenId == eScreenID.DailyVoucherEntryBhojnalay)
                dr = mClsDailyVoucher.GetDrDailyVoucherBhojnalay(strToDate, intCtrMachId, UserInfo.fy_id, MOD_TYPE, Convert.ToInt32(nudToSerialNo.Text));
            else if (mScreenId == eScreenID.DailyVoucherEntryGame)
                dr = mClsDailyVoucher.GetDrDailyVoucherGame(strToDate, intCtrMachId, UserInfo.fy_id, MOD_TYPE, Convert.ToInt32(nudToSerialNo.Text));
            else if (mScreenId == eScreenID.DailyVoucherEntryToyTrain)
                dr = mClsDailyVoucher.GetDrDailyVoucherToyTrain(strToDate, intCtrMachId, UserInfo.fy_id, MOD_TYPE, Convert.ToInt32(nudToSerialNo.Text));
            else if (mScreenId == eScreenID.DailyVoucherEntryLocker)
                dr = mClsDailyVoucher.GetDrDailyVoucherLocker(strToDate, LocID, UserInfo.fy_id, MOD_TYPE, Convert.ToInt32(nudToSerialNo.Text));
            else if (mScreenId == eScreenID.DailyVoucherEntryDengi)
                dr = mClsDailyVoucher.GetDrDailyVoucherDengi(strToDate, LocID, UserInfo.fy_id, MOD_TYPE, Convert.ToInt32(nudToSerialNo.Text));
            else if (mScreenId == eScreenID.DailyVoucherEntryEntryGate)
                dr = mClsDailyVoucher.GetDrDailyVoucherEntryGate(strToDate, LocID, UserInfo.fy_id, MOD_TYPE, Convert.ToInt32(nudToSerialNo.Text));
            else if (mScreenId == eScreenID.DailyVoucherEntryMedical)
                dr = mClsDailyVoucher.GetDrDailyVoucherMedical(strToDate, intCtrMachId, UserInfo.fy_id, MOD_TYPE, Convert.ToInt32(nudToSerialNo.Text));

            // mClsDailyVoucher.GetDrExcelDownloadWithData(strToDate, intCtrMachId, LocID, UserInfo.fy_id, MOD_TYPE, nudToSerialNo.Text)


            // If dr.Read()() Then
            // If dr.IsDBNull(0) Then
            // 'Dim officeexcel As Excel.Application
            // 'officeexcel = CreateObject("Excel.Application")
            // 'Dim workbook As Object = officeexcel.Workbooks.Add("E:\Santosh\BackUp_Latest\OSOL\DailyVoucherXcel\DailyVoucherOSOLExcelUpload.xlsx")
            // 'officeexcel.Visible = False
            // MessageBox.Show("Sorry Excel Could not Download !!!!!")
            // 'Return
            // End If

            // Else
            // If Not dr.HasRows < 0 Then
            // End If

            // Dim officeexcel As Excel.Application
            // officeexcel = CreateObject("Excel.Application")
            string path = AppDomain.CurrentDomain.BaseDirectory;
            // replace \bin\Debug\ the part of the path you dont want  with part you do want
            string path1 = path.Replace(@"\bin\Debug\", @"\DailyVoucherXcel\DailyVoucherOSOLExcelUpload.xlsx");
            Excel.Application officeexcel;
            officeexcel = (Excel.Application)Interaction.CreateObject("Excel.Application");
            object workbook = officeexcel.Workbooks.Add(path1);

            // Dim d As String (AppDomain.CurrentDomain.BaseDirectory + "DailyVoucherXcel\DailyVoucherOSOLExcelUpload.xlsx")
            // Dim workbook As Object = officeexcel.Workbooks.Add("\DailyVoucherXcel\DailyVoucherOSOLExcelUpload.xlsx")
            // Dim workbook As Object = officeexcel.Workbooks.Add("E:\Santosh\BackUp_Latest\OSOL\DailyVoucherXcel\DailyVoucherOSOLExcelUpload.xlsx")
            officeexcel.Visible = true;



            {
                var withBlock = fpsDailyTrans;
                withBlock.RowCount = 0;
                foreach (DataRow drRow in dr.Rows)
                {

                //}
                //while (dr.Read())
                //{
                    lblLocationName.Text = drRow["LOCATION_NAME"].ToString();
                    withBlock.RowCount = withBlock.RowCount + 1;
                    // .Range("A" + (i + 3).ToString).Value = dr("FROM_DATE")
                    withBlock.Rows[withBlock.RowCount - 1].Cells[0].Value = drRow["FROM_DATE"];
                    withBlock.Rows[withBlock.RowCount - 1].Cells[1].Value = drRow["TO_DATE"];
                    //withBlock.Rows[withBlock.RowCount - 1].Cells[(int)eEntryGate.tDate].Text = dr("TO_DATE");
                    withBlock.Rows[withBlock.RowCount - 1].Cells[2].Value = drRow["FIRST_RECEIPT_NO"];
                    withBlock.Rows[withBlock.RowCount - 1].Cells[3].Value = drRow["LAST_RECEIPT_NO"];
                    withBlock.Rows[withBlock.RowCount - 1].Cells[4].Value = drRow["TOTAL_AMOUNT"];
                    withBlock.Rows[withBlock.RowCount - 1].Cells[5].Value = drRow["RECEIPT_COUNT"];
                    withBlock.Rows[withBlock.RowCount - 1].Cells[6].Value = "PENDING";
                    withBlock.Rows[withBlock.RowCount - 1].Cells[6].Tag = 76;
                    mDelColl.Add(DayStkAccDet);
                }
                //dr.Close();
            }


            coll = GetPrnRcptDtDetColl();
            lngError = mClsDailyVoucher.InsertMst(coll, UserInfo.UserName, UserInfo.Machine_Name, intCtrMachId, LocID, ReportDeptId, UserInfo.UserId, UserInfo.CompanyID, UserInfo.fy_id, MOD_TYPE, intCtrMachName, FormatDateToString(dtpToDt.Value), 1);
            if (lngError >= 0)
                // If mScreenId = eScreenID.DailyVoucherEntryDengi Or mScreenId = eScreenID.DailyVoucherEntryEntryGate Or mScreenId = eScreenID.DailyVoucherEntryBhaktniwas Then

                // dr = mClsDailyVoucher.GetDrDailyVoucherData_Den(lngError)
                // ' If dr.Read() Then
                // 'DengiId = dr("DailyVoucherMstId")
                // ' End If
                // 'dr.Close()

                // Else
                dr = mClsDailyVoucher.GetDrDailyVoucherData(lngError);
            // End If




            //while (dr.Read())
            foreach (DataRow dritem in dr.Rows)
            {
                var i = 0;
                {
                    var withBlock = officeexcel;
                    lngError = Convert.ToInt64(dritem["ID"]);
                    withBlock.Range["A" + (i + 2).ToString()].Value = dritem["ID"];
                    withBlock.Range["B" + (i + 2).ToString()].Value = dritem["FromDate"];
                    withBlock.Range["C" + (i + 2).ToString()].Value = dritem["ToDate"];
                    withBlock.Range["D" + (i + 2).ToString()].Value = dritem["MinSerialNo"];
                    withBlock.Range["E" + (i + 2).ToString()].Value = dritem["MaxSerialNo"];
                    withBlock.Range["F" + (i + 2).ToString()].Value = dritem["TotalAmount"];
                    withBlock.Range["G" + (i + 2).ToString()].Value = dritem["TotalReceipt"];
                    withBlock.Range["H" + (i + 2).ToString()].Value = dritem["Status"];
                    withBlock.Range["I" + (i + 2).ToString()].Value = dritem["COM_ID"];
                    withBlock.Range["J" + (i + 2).ToString()].Value = dritem["Location_Name"];
                    withBlock.Range["K" + (i + 2).ToString()].Value = dritem["LOC_ID"];
                    withBlock.Range["L" + (i + 2).ToString()].Value = dritem["DEPT_NAME"];
                    withBlock.Range["M" + (i + 2).ToString()].Value = dritem["DEPT_ID"];
                    withBlock.Range["N" + (i + 2).ToString()].Value = dritem["FY_ID"];
                    withBlock.Range["O" + (i + 2).ToString()].Value = dritem["CTR_NAME"];
                    withBlock.Range["P" + (i + 2).ToString()].Value = dritem["CTR_MACH_ID"];
                    withBlock.Range["Q" + (i + 2).ToString()].Value = dritem["MOD_ID"];
                    withBlock.Range["R" + (i + 2).ToString()].Value = dritem["MOD_NAME"];
                    withBlock.Range["S" + (i + 2).ToString()].Value = dritem["ERP_COMP_ID"];
                    withBlock.Range["T" + (i + 2).ToString()].Value = dritem["ERP_BRANCH_ID"];
                    withBlock.Range["U" + (i + 2).ToString()].Value = dritem["PaymetTypeId"];
                    withBlock.Range["V" + (i + 2).ToString()].Value = dritem["PaymentType"];
                    withBlock.Range["W" + (i + 2).ToString()].Value = dritem["detTotalAmount"];
                    withBlock.Range["X" + (i + 2).ToString()].Value = dritem["detTotalReceipt"];
                    withBlock.Range["Y" + (i + 2).ToString()].Value = dritem["DENGI_RECEIPT_ID"];
                    withBlock.Range["Z" + (i + 2).ToString()].Value = dritem["SerialNo"];
                    withBlock.Range["AA" + (i + 2).ToString()].Value = dritem["DR_DATE"];
                    withBlock.Range["AB" + (i + 2).ToString()].Value = dritem["AMOUNT"];
                    withBlock.Range["AC" + (i + 2).ToString()].Value = dritem["NAME"];
                    withBlock.Range["AD" + (i + 2).ToString()].Value = dritem["CHQ_BANK_NAME"];
                    withBlock.Range["AE" + (i + 2).ToString()].Value = dritem["CHQ_NO"];
                    withBlock.Range["AF" + (i + 2).ToString()].Value = dritem["CHQ_DATE"];
                    withBlock.Range["AG" + (i + 2).ToString()].Value = dritem["DD_BANK_NAME"];
                    withBlock.Range["AH" + (i + 2).ToString()].Value = dritem["DD_NO"];
                    withBlock.Range["AI" + (i + 2).ToString()].Value = dritem["DD_DATE"];
                    withBlock.Range["AJ" + (i + 2).ToString()].Value = dritem["DENGI_TYPE"];
                    withBlock.Range["AK" + (i + 2).ToString()].Value = dritem["ADDRESS"];
                    withBlock.Range["AL" + (i + 2).ToString()].Value = dritem["DISTRICT"];
                    withBlock.Range["AM" + (i + 2).ToString()].Value = dritem["STATE"];
                    withBlock.Range["AN" + (i + 2).ToString()].Value = dritem["COUNTRY"];
                }
            }
            //dr.Close();
            // Next
            officeexcel = null/* TODO Change to default(_) if this is not a reference type */;
            // officeexcel.Close()
            workbook = null;
            // workbook.Close()
            if (lngError > 0)
                UpdateId = mClsDailyVoucher.UpdateStatusDailyVoucher(lngError);

            if (UpdateId >= 0)
            {
                MessageBox.Show("Excel download successfully !");
                btnNew_Click(null, null);
            }
            else
                MessageBox.Show("Failed To download Excel");
        }

        private void myobject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }


        private void cboCounter_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            LocID = cf.cmbItemName3(cboCounter, cboCounter.SelectedIndex);
        }
    }

}
