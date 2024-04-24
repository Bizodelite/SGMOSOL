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
using System.Data.SqlClient;
using SGMOSOL.SCREENS;
using Microsoft.Office.Interop.Excel;

namespace SGMOSOL.DAL.Locker
{
    public partial class frmMessReport : Form
    {
        private CommonFunctions cf = new CommonFunctions();
        private Int64 ReportDeptId;
        private eScreenID mScreenId;
        private Int64 LocID;

        private MessPrintReceiptDAL mClsPrintReceipt = new MessPrintReceiptDAL();
        private ArrayList CtrlArr = new ArrayList(); // To insert Form Control in Array List
        private ArrayList btnArr = new ArrayList();

        public frmMessReport(eScreenID ScreenID)
        {
            InitializeComponent();
            mScreenId = ScreenID;
        }
        
        private void frmMessReport_Load(System.Object sender, System.EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            cf.setControlsonForm(this, CtrlArr, btnArr);
            cf.SetUserScreenActions(this, UserInfo.UserId, (int)mScreenId, btnArr);
            cf.fncSetDateAndRange(dtpFromDt);
            cf.fncSetDateAndRange(dtpToDt);
            FillCounter();
            FillCounter1();
            FillSublocation();
            FillPaymentType();

            if (mScreenId == eScreenID.lockercheckoutuserwise | mScreenId == eScreenID.lockercheckinuserwise | mScreenId == eScreenID.DengiUserwise | mScreenId == eScreenID.BNRoomCheckOutUserwise | mScreenId == eScreenID.BNRoomCheckINUserwise | mScreenId == eScreenID.BNRoomAdvanceVoucher | mScreenId == eScreenID.BNUserwiseCashReport | mScreenId == eScreenID.DengiBhetvastuUserwise)
            {
                btnPrint.Location = new System.Drawing.Point(150, 130);
                btnClose.Location = new System.Drawing.Point(252, 129);

                dtpFromTime.Visible = true;
                dtpToTime.Visible = true;
                lbFromTime.Visible = true;
                lbToTime.Visible = true;
            }
            if (mScreenId == eScreenID.DailyDengi)
            {
                lblFromDate.Visible = false;
                dtpToDt.Visible = false;
            }
            else if (mScreenId == eScreenID.ChequeDengiReceiptVoucher)
            {
                lblFromDate.Visible = false;
                dtpToDt.Visible = false;
            }
            else if (mScreenId == eScreenID.EntryGateDailyReceipt)
            {
                lblFromDate.Visible = false;
                dtpToDt.Visible = false;
            }
            else if (mScreenId == eScreenID.GameDailyReceipt)
            {
                lblFromDate.Visible = false;
                dtpToDt.Visible = false;
            }
            else if (mScreenId == eScreenID.ToyTrainDailyReceipt)
            {
                lblFromDate.Visible = false;
                dtpToDt.Visible = false;
            }
            else if (mScreenId == eScreenID.DailyDengiReceiptRpt)
            {
                lblFromDate.Visible = false;
                dtpToDt.Visible = false;
            }
            else if (mScreenId == eScreenID.lockercheckoutdaily)
            {
                lblFromDate.Visible = false;
                dtpToDt.Visible = false;
                cboCounter.Enabled = true;
            }
            else if (mScreenId == eScreenID.lockercheckindaily)
            {
                lblFromDate.Visible = false;
                dtpToDt.Visible = false;
                cboCounter.Enabled = true;
            }
            else if (mScreenId == eScreenID.DailyDengiReceipt)
            {
                lblFromDate.Visible = false;
                dtpToDt.Visible = false;
            }
            else if (mScreenId == eScreenID.BNDailyRoomCheckOut)
            {
                lbl_sublocation.Visible = true;
                cmbBhaktaNiwas.Visible = true;
                lblFromDate.Visible = false;
                dtpToDt.Visible = false;
                dtpFromTime.Visible = false;
                dtpToTime.Visible = false;
                cboPaymentType.Visible = true;
                lblPtype.Visible = true;
            }
            else if (mScreenId == eScreenID.BNDailyRoomCheckIN)
            {
                lbl_sublocation.Visible = true;
                cmbBhaktaNiwas.Visible = true;
                lblFromDate.Visible = false;
                dtpToDt.Visible = false;
                dtpFromTime.Visible = false;
                dtpToTime.Visible = false;
                cboPaymentType.Visible = true;
                lblPtype.Visible = true;
            }
            else if (mScreenId == eScreenID.BNRoomAdvanceVoucher)
            {
                lbl_sublocation.Visible = true;
                cmbBhaktaNiwas.Visible = true;
                lblFromDate.Visible = false;
                lblToDate.Visible = false;
                dtpFromDt.Visible = false;
                dtpToDt.Visible = false;
                dtpFromTime.Visible = false;
                dtpToTime.Visible = false;
                lbFromTime.Visible = false;
                lbToTime.Visible = false;
                cboPaymentType.Visible = true;
                lblPtype.Visible = true;
            }
            else if (mScreenId == eScreenID.BNUserwiseCashReport)
            {
                cboPaymentType.Visible = true;
                lblPtype.Visible = true;
            }
            else if (mScreenId == eScreenID.BNRoomCheckOutUserwise)
            {
                cboPaymentType.Visible = true;
                lblPtype.Visible = true;
            }
            else if (mScreenId == eScreenID.BNRoomCheckINUserwise)
            {
                cboPaymentType.Visible = true;
                lblPtype.Visible = true;
            }
            else if (mScreenId == eScreenID.DengiBhetvastuUserwise)
            {
                lblFromDate.Visible = true;
                lblToDate.Visible = true;
                // dtpToDt.Visible = False
                btnPrint.Enabled = true;
            }
        }


        private void FillCounter()
        {
            System.Data.DataTable dr = cf.GetDrCounterMachId(UserInfo.UserId, SystemHDDModelNo, SystemHDDSerialNo, SystemMacID, 0);


            cf.FillCombo(cboCounter, dr, "CounterMachineTitle", "CtrMachId", "DeptId", "LocId");
            if (cboCounter.Items.Count > 0)
            {
                cboCounter.SelectedIndex = 0;
                ReportDeptId = cf.cmbItemdata(cboCounter, cboCounter.SelectedIndex);

                LocID = cf.cmbItemdata(cboCounter, cboCounter.SelectedIndex);
            }

            cboCounter.Enabled = false;
        }
        private void FillCounter1()
        {
             System.Data.DataTable dr = cf.GetDrCounterMachId(UserInfo.UserId, SystemHDDModelNo, SystemHDDSerialNo, SystemMacID, Convert.ToInt16(eModType.BhaktaNiwas));
            if (dr.Rows.Count > 0)
                LocID = Convert.ToInt64(dr.Rows[0]["LocId"]);
            //dr.Close();
        }
        private void FillPaymentType()
        {
            System.Data.DataSet dsPaymentType1;
            int[] array = { (int)eTokenDetail.Swap, (int)eTokenDetail.Cash, (int)eTokenDetail.Cheque };
            dsPaymentType1 = cf.GetDsPaymentType2(array);
            cf.FillComboWithDataSet(cboPaymentType, dsPaymentType1.Tables[0], "Token_Detail_Name", "Token_Detail_Name", "Token_Detail_Id");
        }

        private void FillSublocation()
        {
            System.Data.DataTable dr = cf.GetDrSublocation(7, LocID);
            cf.FillCombo(cmbBhaktaNiwas, dr, "Department", "Department_Id");
            if (cmbBhaktaNiwas.Items.Count > 0)
            {
                cmbBhaktaNiwas.SelectedIndex = -1;
                cmbBhaktaNiwas.SelectedText = "All";
            }
        }

        private void btnClose_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(System.Object sender, System.EventArgs e)
        {
            string strFromDate;
            string strToDate;
            string strFromTime;
            string strToTime;
            Int64 intCtrMachId;
            Int64 intSublocId;

            string strReportName;
            System.Data.DataSet ds = new System.Data.DataSet();
            Form sForm;
            Collection pColl = new Collection();
            Int64 payTypeid;

            strFromDate = FormatDateToString(dtpFromDt.Value);
            strToDate = FormatDateToString(dtpToDt.Value);
            strFromTime = (dtpFromTime.Text);
            strToTime = (dtpToTime.Text);
            payTypeid = cf.cmbItemdata(cboPaymentType, cboPaymentType.SelectedIndex);

            intCtrMachId = cf.cmbItemdata(cboCounter, cboCounter.SelectedIndex);
            intSublocId = cf.cmbItemdata(cmbBhaktaNiwas, cmbBhaktaNiwas.SelectedIndex);
            setCursor(this, false);

            if (mScreenId == eScreenID.DailyDengi)
            {
                // strReportName = "crDailyReceiptDetails_desk2.rdlc"
                strReportName = "crDailyDengiRcptVchr.rdlc";
                ds = mClsPrintReceipt.GetDsDailyDengi(strFromDate, intCtrMachId);


                System.Data.DataTable DR = null;
                string strUsername = "";
                DataView objDv;
                try
                {
                    DR = mClsPrintReceipt.GetDrDailyDengi_UserNames(strFromDate, intCtrMachId);
                    foreach (DataRow DRitem in DR.Rows)
                        strUsername = strUsername + DRitem["ENTERED_BY"] + ",";
                    if (strUsername.Length > 0)
                        strUsername = Strings.Mid(strUsername, 1, strUsername.Length - 1);
                    //DR.Close();
                }
                catch (Exception ex)
                {
                    //if (DR != null)
                    //    DR.Close();
                }
                objDv = new DataView(ds.Tables[0], "", "", DataViewRowState.CurrentRows);
                foreach (DataRowView objDrv in objDv)
                {
                    objDrv.BeginEdit();
                    // objDrv("USER_NAME") = strUsername
                    objDrv.EndEdit();
                }
            }
            else if (mScreenId == eScreenID.PrasadWatap)
            {
                ds = mClsPrintReceipt.GetDsPrasadWatap(strFromDate, strToDate, intCtrMachId);
                strReportName = "crpPrasadWtp_desk.rdlc";
            }
            else if (mScreenId == eScreenID.ReceiptDetail)
            {
                ds = mClsPrintReceipt.GetDsReceiptDetail(strFromDate, strToDate, intCtrMachId);
                strReportName = "crReceiptDetails_desk.rdlc";
            }
            else if (mScreenId == eScreenID.EntryGateUserWise)
            {
                ds = mClsPrintReceipt.GetDsEntryGateUserWise(strFromDate, strToDate, intCtrMachId);
                strReportName = "crpEntryGateUserWise.rdlc";
            }
            else if (mScreenId == eScreenID.EntryGateDailyReceipt)
            {
                ds = mClsPrintReceipt.GetDsEntryGateDailyReceipt(strFromDate, intCtrMachId);
                strReportName = "crpEntryGateDailyReceipt.rdlc";
            }
            else if (mScreenId == eScreenID.ToyTrainUserWise)
            {
                ds = mClsPrintReceipt.GetDsToyTrainUserWise(strFromDate, strToDate, intCtrMachId);
                strReportName = "crpToyTrainUserWise.rdlc";
            }
            else if (mScreenId == eScreenID.ToyTrainDailyReceipt)
            {
                ds = mClsPrintReceipt.GetDsToyTrainDailyReceipt(strFromDate, intCtrMachId);
                strReportName = "crpToyTrainDailyReceipt.rdlc";
            }
            else if (mScreenId == eScreenID.GameUserWise)
            {
                ds = mClsPrintReceipt.GetDsGameUserWise(strFromDate, strToDate, intCtrMachId);
                strReportName = "crpGameUserWise.rdlc";
            }
            else if (mScreenId == eScreenID.DengiUserwise)
            {
                ds = mClsPrintReceipt.GetDengiUserwise(strFromDate, strToDate, strFromTime, strToTime, intCtrMachId);
                strReportName = "crDengiUserWise.rdlc";
            }
            else if (mScreenId == eScreenID.dengireceiptdetail)
            {
                ds = mClsPrintReceipt.GetDsDengiReceiptDetail(strFromDate, strToDate, intCtrMachId);
                strReportName = "crDengiReceiptDetail.rdlc";
            }
            else if (mScreenId == eScreenID.GameDailyReceipt)
            {
                ds = mClsPrintReceipt.GetDsGameDailyReceipt(strFromDate, intCtrMachId);
                strReportName = "crpGameDailyReceipt.rdlc";
            }
            else if (mScreenId == eScreenID.DailyDengiReceiptRpt)
            {
                ds = mClsPrintReceipt.GetDsDailyDengiMess(strFromDate, intCtrMachId);
                strReportName = "crpDailyDengiReceipt.rdlc";
            }
            else if (mScreenId == eScreenID.EntryGateReceiptDetail)
            {
                ds = mClsPrintReceipt.GetDsEntryGateReceiptDetail(strFromDate, strToDate, intCtrMachId);
                strReportName = "crpEGRDtls.rdlc";
            }
            else if (mScreenId == eScreenID.ChequeDengiReceiptVoucher)
            {
                ds = mClsPrintReceipt.GetChequeReptDetail(strFromDate, intCtrMachId);
                strReportName = "crDailyDengiRcptVchr.rdlc";
            }
            else if (mScreenId == eScreenID.AbhishekRegister)
            {
                ds = mClsPrintReceipt.GetDengiVoucherDetails(strFromDate, strToDate, intCtrMachId);
                strReportName = "crDengiVoucherDtls.rdlc";
            }
            else if (mScreenId == eScreenID.DailyDengiReceipt)
            {
                ds = mClsPrintReceipt.GetDsDailyDengiReceipt(strFromDate, intCtrMachId);
                strReportName = "crpDaily_Dengi_Receipt_Updated.rdlc";
            }
            else if (mScreenId == eScreenID.ChequeDengiReceiptVoucher)
            {
                ds = mClsPrintReceipt.GetChequeReptDetail(strFromDate, intCtrMachId);
                strReportName = "crChequeReceiptDetails_desk1.rdlc";
            }
            else if (mScreenId == eScreenID.lockercheckoutuserwise)
            {
                ds = mClsPrintReceipt.GetDsckeckoutLockeruserwise(strFromDate, strToDate, strFromTime, strToTime, LocID);
                strReportName = "crLockerChkOutUserWise_Desk.rdlc";
            }
            else if (mScreenId == eScreenID.lockercheckinuserwise)
            {
                ds = mClsPrintReceipt.GetDschkinLockeruserwise(strFromDate, strToDate, strFromTime, strToTime, LocID);
                strReportName = "crLockerChkInUserWise_Desk.rdlc";
            }
            else if (mScreenId == eScreenID.lockerreceiptdetail)
            {
                ds = mClsPrintReceipt.GetDslockerreceiptdetail(strFromDate, strToDate, LocID);
                strReportName = "crpLockerRDtls_Desk.rdlc";
            }
            else if (mScreenId == eScreenID.lockerCheckInCheckOut)
            {
                ds = mClsPrintReceipt.GetDsLockerCheckInCheckOut(strFromDate, strToDate, LocID);
                strReportName = "crpLockerChkInChkOut.rdlc";
            }
            else if (mScreenId == eScreenID.lockercheckoutdaily)
            {
                ds = mClsPrintReceipt.GetDsDailyLockerCheckOut(strFromDate, LocID);
                strReportName = "crCheckOutDailyRpt_desk.rdlc";
            }
            else if (mScreenId == eScreenID.lockercheckindaily)
            {
                ds = mClsPrintReceipt.GetDsDailyLockerCheckIn(strFromDate, LocID);
                strReportName = "crCheckInDailyRpt_desk.rdlc";
            }
            else if (mScreenId == eScreenID.BNRoomReceiptDetail)
            {
                ds = mClsPrintReceipt.GetDsRoomReceiptDetail(strFromDate, strToDate, intCtrMachId);
                strReportName = "crpRoomReceiptDetails.rdlc";
            }
            else if (mScreenId == eScreenID.BNDailyRoomCheckOut)
            {
                clsConnection objCon = new clsConnection();
                 //System.Data.DataTable dt = objCon.GetRecordDataTable("Select User_First_Name + ' ' + User_Last_Name as UserFName from sec_user_mst_t where User_Id=" + UserInfo.UserId);
                ds = mClsPrintReceipt.GetDsDailyRoomCheckOut(strFromDate, intCtrMachId, intSublocId, LocID, payTypeid, UserInfo.UserName);
                strReportName = "crDailyRoomCheckOut.rdlc";
            }
            else if (mScreenId == eScreenID.BNDailyRoomCheckIN)
            {
                ds = mClsPrintReceipt.GetDsDailyRoomCheckIn(strFromDate, intCtrMachId, intSublocId, LocID, payTypeid);
                strReportName = "crDailyRoomCheckIN.rdlc";
            }
            else if (mScreenId == eScreenID.lockeradvancevouchure)
            {
                ds = mClsPrintReceipt.GetDsLockerAdvanceVouchers(strFromDate, strToDate, strFromTime, strToTime, intCtrMachId);
                strReportName = "crpLockerAdvVchr.rdlc";
            }
            else if (mScreenId == eScreenID.BNRoomCheckOutUserwise)
            {
                ds = mClsPrintReceipt.GetDsRoomCheckOutUserwise(strFromDate, strToDate, strFromTime, strToTime, intSublocId, LocID, payTypeid);
                strReportName = "crRoomChkOutUserWise.rdlc";
            }
            else if (mScreenId == eScreenID.BNRoomCheckINUserwise)
            {
                ds = mClsPrintReceipt.GetDsRoomCheckInUserwise(strFromDate, strToDate, strFromTime, strToTime, intSublocId, LocID, payTypeid);
                strReportName = "crRoomChkINUserWise.rdlc";
            }
            else if (mScreenId == eScreenID.BNRoomAdvanceVoucher)
            {
                ds = mClsPrintReceipt.GetDsRoomAdvanceVouchers(strFromDate, strToDate, strFromTime, strToTime, intSublocId, LocID, payTypeid);
                strReportName = "crpRoomAdvVchr.rdlc";
            }
            else if (mScreenId == eScreenID.BEDRoomAdvanceVoucher)
            {
                // ds = mClsPrintReceipt.GetDsRoomAdvanceVouchers(strFromDate, strToDate, strFromTime, strToTime, intSublocId)
                ds = mClsPrintReceipt.GetDsBedAdvanceVouchers(strFromDate, strToDate, strFromTime, strToTime, intSublocId, payTypeid);
                strReportName = "crpBedAdvVchr.rdlc";
            }
            else if (mScreenId == eScreenID.BNUserwiseCashReport)
            {
                ds = mClsPrintReceipt.GetDsBNUserwiseCashReport(strFromDate, strToDate, strFromTime, strToTime, intCtrMachId);
                strReportName = "crpRoomChkInChkOut.rdlc";
            }
            else if (mScreenId == eScreenID.DengiBhetvastuUserwise)
            {
                ds = mClsPrintReceipt.GetDengiBhetVastuUserwise(strFromDate, strToDate, strFromTime, strToTime, intCtrMachId);
                strReportName = "crDengiBhetvastuUserWise.rdlc";
            }
            else
            {
                ds = mClsPrintReceipt.GetDsAnnadanRec(strFromDate, strToDate, intCtrMachId);
                strReportName = "crpAnnadanRec_desk.rdlc";
            }
            setCursor(this, true);

            //sForm = new frmCrystalViewer(UserInfo.ReportPath & strReportName,null , ds, , pColl, mScreenId, False)

            sForm = new frmCrystalViewer(UserInfo.ReportPath + strReportName, null, ds, null, pColl, (long)mScreenId, false);
            sForm.Text = this.Text;
            setCursor(this, true);
            sForm.Show();
        }
    }

}
