using CrystalDecisions.ReportAppServer;
using Microsoft.Office.Interop.Excel;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using Microsoft.VisualBasic;
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
using static SGMOSOL.BAL.BadBAL;
using SGMOSOL.ADMIN;
using SGMOSOL.DAL;

namespace SGMOSOL.SCREENS.BedSystem
{
    public partial class FrmBedCheckOut : Form
    {
        private eScreenID mScreenID;
        private int rbFlag;
        private bool mBlnEdit = false;
        private eAction mAction;        // Reference to Enum of Type Action for ActionView,ActionNew,ActionUpdate and ActionDelete
        private ArrayList CtrlArr = new ArrayList(); // To insert Form Control in Array List
        private ArrayList btnArr = new ArrayList();
        //private OSOL_ADMIN.clsDsCommon mClsDsCom = new OSOL_ADMIN.clsDsCommon();
        //private OSOL_BLSDS.clsBedRoomCheckout objBlsInvAlloc = new OSOL_BLSDS.clsBedRoomCheckout();
        //private OSOL_BLSDS.clsDsBedCheckoutMst objDsPrintReceiptationMst = new OSOL_BLSDS.clsDsBedCheckoutMst();
        //private OSOL_BLSDS.clsDsBedCheckoutDet objDsMessPrintReceiptDet = new OSOL_BLSDS.clsDsBedCheckoutDet();
        private bool DisableSendKeys;
        private DateTime dtEnteredOn;
        // 'Dim RoomAdvanceTariff As Double
        private double RoomTotalRent;
        private bool bkDateEntry = false;
        private bool blnformChange;
        private System.Windows.Forms.ListBox lstItemMaster = new System.Windows.Forms.ListBox();
        private System.Data.DataSet dsItemMaster;
        private string[] col;
        private string[] mStrErrMsg;
        public long mSearchId;
        private Collection mDelColl = new Collection();

        private string mStrCounterMachineShortName;
        private int PrintReceiptDeptID;
        private string PrintReceiptDeptName;
        private string PrintReceiptLocName;
        private int PrintReceiptLocId;
        private string PrintReceiptitem_names;
        public static string itemnames;
        public static string item_name = "";
        private string NameRoomHolder;
        private string PlaceRoomHolder;
        private long countedDays;
        private long oldDays;
        // Dim t1 As New ThreadStart(AddressOf LoadItemMinLevel)
        // Dim objThread As New Thread(t1)
        CommonFunctions cf = new CommonFunctions();
        BedCheckOutDAL BedCheckOutDALobj = new BedCheckOutDAL();

        public FrmBedCheckOut(eScreenID pScreenId)
        {
            InitializeComponent();
            mScreenID = pScreenId;
            this.Closing += new CancelEventHandler(this.FrmBedCheckOut_Closing);
            this.fpsPrintReceipt.GotFocus += fpsPrintReceipt_GotFocus;
        }

        private void FrmBedCheckOut_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter & !DisableSendKeys)
                SendKeys.Send("{tab}");

            if (e.KeyCode == Keys.End & (mAction == eAction.ActionInsert | mAction == eAction.ActionUpdate) & blnformChange)
                btnSave_Click(null, null);
        }

        private void FrmBedCheckOut_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End & (mAction == eAction.ActionInsert | mAction == eAction.ActionUpdate) & blnformChange)
                btnSave_Click(null, null);
        }



        private void FrmBedCheckOut_Load(System.Object sender, System.EventArgs e)
        {
            cf.setControlsonForm(this, CtrlArr, btnArr);
            cf.SetUserScreenActions(this, UserInfo.UserId, (int)mScreenID, btnArr, null, mBlnEdit);
            ScreenToCenter();

            txtTotalAmt.Enabled = false;
            nudAdvance.Enabled = false;
            txtUser.Text = UserInfo.UserName;
            FillCounter();

            if (txtCounter.Tag == null || Convert.ToInt32(txtCounter.Tag) == 0)
            {
                MessageBox.Show("User/Counter/Machine ID assign conflict. Please contact system administrator.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            FillItemMaster();




            // commented by girish

            // objThread.Start()
            btnNew_Click(null, null);
        }
        private void ScreenToCenter()
        {
            //Int32 ctr;
            //ctr = (MDI.Size.Width - this.Size.Width) / (double)2;
            //this.Location = new Point(ctr, 0);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }
        private void FormClear()
        {
            txtCheckInVchNo.Text = "";
            txtDays.Text = "1";
            txtmobno.Text = "";
            txtName.Text = "";
            txtNoOfPerson.Text = "";
            txtPlace.Text = "";
            nudAdvance.Value = 0;
            nudRefund.Value = 0;
            nudRent.Value = 0;
            txtTotalAmt.Value = 0;
            fpsPrintReceipt.RowCount = 0;
            fpsPrintReceipt.RowCount = 1;
            txtCheckInVchNo.Tag = null;
        }

        private void FillCounter()
        {
            System.Data.DataTable dr;
            dr = cf.GetDrCounterMachId(UserInfo.UserId, SystemHDDModelNo, SystemHDDSerialNo, SystemMacID, Convert.ToInt16(eModType.BedSystem));
            if (dr.Rows.Count > 0)
            {
                txtCounter.Text = dr.Rows[0]["CounterMachineTitle"].ToString();
                txtCounter.Tag = dr.Rows[0]["CtrMachId"];
                PrintReceiptDeptID = Convert.ToInt32(dr.Rows[0]["DeptId"]);
                PrintReceiptDeptName = dr.Rows[0]["DepartmentName"].ToString();
                PrintReceiptLocName = dr.Rows[0]["LocName"].ToString();
                PrintReceiptLocId = Convert.ToInt32(dr.Rows[0]["LocId"]);
                mStrCounterMachineShortName = dr.Rows[0]["CounterMachineShortName"].ToString();
            }
        }

        private void GetmaxNo()
        {
            System.Data.DataTable dr;
            try
            {
                dr = BedCheckOutDALobj.GetMaxSerialNumber(UserInfo.CompanyID, 0, 0, UserInfo.fy_id);
                if (dr.Rows.Count > 0)
                    txtVchNo.Text = (Convert.ToInt32(dr.Rows[0]["SerialNo"]) + 1).ToString();
                else
                    txtVchNo.Text = "1";
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
        }

        private void btnNew_Click(System.Object sender, System.EventArgs e)
        {
            FormClear();
            cf.fncSetDateAndRange(dtpCurDate);
            cf.fncSysTime(dtpCheckOutTime);


            dtpCurDate.Enabled = bkDateEntry;
            mAction = eAction.ActionInsert;

            FillItemMaster();
            btnSave.Enabled = btnNew.Enabled;
            GetmaxNo();

            fpsPrintReceipt.Focus();
            fpsPrintReceipt.Rows[0].Cells[(int)ePrintReceipt.ProductN].Style.BackColor = Color.Cyan;
            fpsPrintReceipt.CurrentCell = fpsPrintReceipt.Rows[0].Cells[(int)ePrintReceipt.ProductN];
            SendKeys.Send("%{DOWN}");

            blnformChange = false;
        }
        private void FillItemMaster()
        {
            string strDate;

            dsItemMaster = null;
            dsItemMaster = new System.Data.DataSet();
            lstItemMaster.Items.Clear();
            strDate = FormatDateToString(dtpCurDate.Value);
            try
            {
                dsItemMaster = cf.GetDsProductMenu(UserInfo.UserId);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            if (dsItemMaster.Tables[0].Rows.Count > 0)
                cf.FillComboWithDataSet(lstItemMaster, dsItemMaster.Tables[0], "ItemTitle", "ItemTitle", "ItemId", "ItemCode", "");

            DataGridViewComboBoxCell cboItem = new DataGridViewComboBoxCell();
            cboItem.DataSource = lstItemMaster;
            cboItem.ReadOnly = true;
            fpsPrintReceipt.Columns[(int)ePrintReceipt.ProductN].CellTemplate = cboItem;
        }

        private void fpsPrintReceipt_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string Filtercriteria;
            DataView Dv;
            var withBlock = fpsPrintReceipt;
            if (e.ColumnIndex == (int)ePrintReceipt.ProductN)
            {
                if (withBlock.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "")
                {
                    Filtercriteria = "ItemTitle = " + FormatData.FormatCharData(withBlock.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    Dv = new DataView(dsItemMaster.Tables[0], Filtercriteria, "", DataViewRowState.CurrentRows);
                    withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.ProductN].Tag = 0;
                    foreach (DataRowView Drv in Dv)
                    {
                        withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.ProductN].Tag = Drv["ItemId"];
                        withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.ProductC].Value = Drv["ItemCode"];
                        withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.Advance].Value = Drv["ADVANCE"];
                        withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.Nidhi].Value = Drv["Price"];
                        withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.TotAdv].Value = Math.Round(Convert.ToDouble(withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.Advance].Value) * Convert.ToDouble(withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.Qty].Value), 2);
                        withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.TotNidhi].Value = Math.Round(Convert.ToDouble(withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.Nidhi].Value) * Convert.ToDouble(withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.Qty].Value), 2);
                        break;
                    }
                }
            }
            else if (e.ColumnIndex == (int)ePrintReceipt.Qty)
            {

                // .Cells(e.Row, ePrintReceipt.Amount).Text = RoundIt(.Cells(e.Row, ePrintReceipt.Price).Value * .Cells(e.Row, ePrintReceipt.Qty).Value, 2)
                withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.TotAdv].Value = Convert.ToDouble(withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.Advance].Value) * Convert.ToDouble(withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.Qty].Value);
                withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.TotNidhi].Value = Convert.ToDouble(withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.Nidhi].Value) * Convert.ToDouble(withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.Qty].Value);
            }
            blnformChange = true;
            CalculateAmt();
        }

        private void fpsPrintReceipt_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            try
            {
                var withBlock = fpsPrintReceipt;
                if (e.KeyCode == Keys.Delete & withBlock.CurrentCell.ColumnIndex == (int)ePrintReceipt.ProductN)
                {
                    if (withBlock.RowCount == 1 | withBlock.CurrentCell.RowIndex == withBlock.RowCount - 1)
                        return;
                    withBlock.Rows.RemoveAt(withBlock.CurrentCell.RowIndex);
                    CalculateAmt();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    if (withBlock.CurrentCell.ColumnIndex == (int)ePrintReceipt.ProductN)
                        withBlock.CurrentCell = fpsPrintReceipt.Rows[withBlock.CurrentCell.RowIndex].Cells[(int)ePrintReceipt.Qty];
                    else if (withBlock.CurrentCell.ColumnIndex == (int)ePrintReceipt.Qty)
                    {
                    }
                }
                blnformChange = true;
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
        }


        private void txtCheckInVchNo_KeyPress(System.Object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            // If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or Asc(e.KeyChar) = 8 Or Asc(e.KeyChar) = 46) Then
            // e.Handled = True
            // End If

            if (e.KeyChar == (int)Keys.Enter)
            {
                System.Data.DataTable dr;
                BedCheckInDet PrnRcptDtDet = new BedCheckInDet();
                long lngCtrMachId;
                int bhaktype;
                long mstId;
                mDelColl = new Collection();
                // FormClear()
                try
                {
                    dr = BedCheckOutDALobj.GetDsBedCheckInMstBarcode(0, "", txtCheckInVchNo.Text, Convert.ToInt64(txtCounter.Tag), UserInfo.CompanyID, PrintReceiptLocId, 0, 0, "");
                    if (dr.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dr.Rows[0]["PendRoomCount"]) == 0)
                        {
                            MessageBox.Show("No pending Rooms against the Receipt No. " + txtCheckInVchNo.Text, PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCheckInVchNo.Text = "";
                            txtCheckInVchNo.Focus();
                            return;
                        }


                        // dtpCheckIn.Value = dr("InDate")
                        txtmobno.Text = dr.Rows[0]["MOB_NO"].ToString();
                        // dtpCheckInTime.Value = dr.Rows[0]["InTime"]
                        txtCheckInVchNo.Text = (dr.Rows[0]["SerialNo"].ToString());
                        txtCheckInVchNo.Tag = dr.Rows[0]["CheckInMstId"];
                        // txtVchNo.Tag = lngSearchId
                        // dtpCheckIn.Tag = dr.Rows[0]["RecordModifiedCount"]
                        txtName.Text = dr.Rows[0]["Name"].ToString();
                        txtPlace.Text = dr.Rows[0]["Place"].ToString();
                        dtpCheckIn.Value = Convert.ToDateTime(dr.Rows[0]["InDate"]);
                        dtpCheckInTime.Value = Convert.ToDateTime(dr.Rows[0]["InTime"]);
                        txtDays.Text = dr.Rows[0]["Days"].ToString();
                        // dtpCheckOut.Value = dr.Rows[0]["OutDate"]
                        // dtpCheckOutTime.Value = dr.Rows[0]["OutTime"]
                        txtNoOfPerson.Text = dr.Rows[0]["NoOfPersons"].ToString();
                        mstId = Convert.ToInt64(dr.Rows[0]["CheckInMstId"]);

                        nudAdvance.Text = dr.Rows[0]["Advance"].ToString();
                        nudRent.Text = dr.Rows[0]["Rent"].ToString();
                        dtEnteredOn = Convert.ToDateTime(dr.Rows[0]["EnteredOn"]);
                        txtCheckInVchNo.Tag = dr.Rows[0]["CheckInMstId"];
                        bhaktype = Convert.ToInt32(dr.Rows[0]["BHAKT_TYPE"]);
                        txtTotalAmt.Value = Convert.ToDecimal(dr.Rows[0]["Advance"]) + Convert.ToDecimal(dr.Rows[0]["Rent"]);
                        nudRefund.Value = Convert.ToDecimal(dr.Rows[0]["Advance"]);
                        if (bhaktype == (int)eBhaktaType.Bhkta)
                            rdBhakta.Checked = true;
                        else if (bhaktype == (int)eBhaktaType.Gorup)
                            rdGroup.Checked = true;
                        else if (bhaktype == (int)eBhaktaType.Trip)
                            rdTrip.Checked = true;
                        dr = BedCheckOutDALobj.GetDrPrintRcptDet(mstId, UserInfo.UserId);
                        {
                            var withBlock = fpsPrintReceipt;
                            withBlock.RowCount = 0;
                            withBlock.RowCount = 1;
                            //while (dr.Read)
                            foreach (DataRow drrow in dr.Rows)
                            {
                                withBlock.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.ProductN].Value = drrow["ItemTitle"];
                                withBlock.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.ProductC].ReadOnly = true;
                                withBlock.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.ProductN].Tag = drrow["ItemId"];
                                withBlock.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.ProductC].Value = drrow["ItemCode"];
                                withBlock.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.Qty].Value = (drrow["Qty"]);
                                withBlock.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.Nidhi].Value = (drrow["RENT"]);
                                withBlock.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.Advance].Value = (drrow["ADVANCE"]);
                                withBlock.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.TotAdv].Value = (drrow["TOTAL_ADVANCE"]);
                                withBlock.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.TotNidhi].Value = (drrow["TOTAL_RENT"]);
                                PrnRcptDtDet.CheckInDetId = Convert.ToInt64(drrow["PrintRcptDetId"]);
                                mDelColl.Add(PrnRcptDtDet);
                                withBlock.RowCount = withBlock.RowCount + 1;
                            }
                            //dr.Close();
                        }

                        FillItemMaster();

                        // mAction = eAction.ActionUpdate
                        // blnformChange = False
                        // dtpCheckIn.Enabled = False
                        fpsPrintReceipt.Focus();
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
                    //if (!dr == null)
                    //    dr.Close();
                    MessageBox.Show("Load Transaction failed.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnNew_Click(null, null);
                }
            }
        }
        //private void fpsPrintReceipt_ComboCloseUp(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        //{
        //    Int32 lngItemId;
        //    {
        //        var withBlock = fpsPrintReceipt;
        //        if (withBlock.CurrentCell.ColumnIndex == (int)ePrintReceipt.ProductN & e.Row == withBlock.RowCount - 1)
        //            withBlock.RowCount += 1;
        //        if (withBlock.CurrentCell.ColumnIndex == ePrintReceipt.ProductN)
        //        {
        //            lngItemId = Val(withBlock.Cells(withBlock.CurrentCell.RowIndex, withBlock.CurrentCell.ColumnIndex).Tag);
        //            if (lngItemId > 0)
        //                withBlock.SetActiveCell(withBlock.CurrentCell.RowIndex, ePrintReceipt.Qty);
        //        }
        //    }
        //}
        private void btnSave_Click(System.Object sender, System.EventArgs e)
        {
            long lngError = -1;
            if (blnformChange == false)
                return;
            if (fncSave())
            {
                // commented by girish
                // LoadItemMinLevel()
                blnformChange = false;
                btnNew_Click(null, null);
            }
        }

        private bool fncSave()
        {
            long lngError = -1;
            BedCheckInMst PrintRcptMst;
            Collection coll;
            long lngSerialNo;
            string strErr;
            bool flag = false;
            if (IsValidForm() == false)
            {
                return false;
            }
            setCursor(this, false);
            lngSerialNo = Convert.ToInt64(txtCheckInVchNo.Text);
            PrintRcptMst = GetPrnRcptDtMst();
            coll = GetPrnRcptDtDetColl();
            string s = item_name;
            if (mAction == eAction.ActionInsert)
            {
                lngError = BedCheckOutDALobj.Insert(PrintRcptMst, coll, UserInfo.UserName, UserInfo.Machine_Name, lngSerialNo, dtEnteredOn);
                if (lngError > 0)
                    txtVchNo.Text = lngSerialNo.ToString();
            }
            else if (mAction == eAction.ActionUpdate)
                lngError = BedCheckOutDALobj.Update(PrintRcptMst, coll, mDelColl, UserInfo.UserName, UserInfo.Machine_Name, lngSerialNo);
            setCursor(this, true);
            if (lngError < 0)
            {
                strErr = ProcErr(lngError);
                col = new string[1];
                col[0] = "";
                // commented by girish
                // If Len(strErr) = 0 Then strErr = GetErrorMessage(mStrErrMsg, 10)
                if (Strings.Len(strErr) == 0)
                    strErr = "Could Not Save Data with Error No:" + lngError.ToString();

                MessageBox.Show(strErr, PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                setCursor(this, true);
                flag = false;
            }
            else if (lngError >= 0)
            {
                col = new string[2];
                col[0] = "Receipt";
                col[1] = Conversion.Str(lngSerialNo);
                // MessageBox.Show(GetErrorMessage(col, 7), PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                setCursor(this, true);
                flag = true;

                // If Val(dtpPrnRcptDt.Tag & vbNullString) = 0 And mScreenID = ScreenID.PrintReceipt Then
                dtpCurDate.Tag = 1;
                blnformChange = false;
                btnPrint_Click(null, null);
            }
            return flag;
        }

        private BedCheckInMst GetPrnRcptDtMst()
        {
            BedCheckInMst PrnRcptDtMst = new BedCheckInMst();
            double dblRentAmt = 0;
            double dblAdvAmt = 0;
            double Qty = 0;
            Int16 ctr;
            PrnRcptDtMst.CheckOutMstId = Convert.ToInt64(txtVchNo.Tag);
            PrnRcptDtMst.CheckInMstId = Convert.ToInt64(txtCheckInVchNo.Tag);
            PrnRcptDtMst.ComId = UserInfo.CompanyID;
            PrnRcptDtMst.LocId = PrintReceiptLocId;  // UserInfo.LocationID
            PrnRcptDtMst.DeptId = PrintReceiptDeptID; // UserInfo.DepartmentID
            PrnRcptDtMst.FyId = UserInfo.fy_id;
            PrnRcptDtMst.UserId = UserInfo.UserId;
            PrnRcptDtMst.CtrMachId = Convert.ToInt64(txtCounter.Tag);
            PrnRcptDtMst.OutDate = dtpCurDate.Value;
            PrnRcptDtMst.OutTime = dtpCheckOutTime.Value;
            for (ctr = 0; ctr <= fpsPrintReceipt.RowCount - 2; ctr++)
            {
                dblRentAmt = dblRentAmt + Math.Round(Convert.ToDouble(fpsPrintReceipt.Rows[ctr].Cells[(int)ePrintReceipt.TotNidhi].Value), 0);
                dblAdvAmt = dblAdvAmt + Math.Round(Convert.ToDouble(fpsPrintReceipt.Rows[ctr].Cells[(int)ePrintReceipt.TotAdv].Value), 0);
                Qty = Qty + Math.Round(Convert.ToDouble(fpsPrintReceipt.Rows[ctr].Cells[(int)ePrintReceipt.Qty].Value), 0);
            }
            PrnRcptDtMst.NoOfBeds = Qty;
            // PrnRcptDtMst.Amount = nudAmount.Value
            PrnRcptDtMst.Advance = dblAdvAmt;
            PrnRcptDtMst.Refund = dblAdvAmt;
            PrnRcptDtMst.Rent = dblRentAmt;
            PrnRcptDtMst.Place = txtPlace.Text;
            PrnRcptDtMst.Name = txtName.Text;
            PrnRcptDtMst.mob_no = txtmobno.Text;
            PrnRcptDtMst.Days = Convert.ToInt32(txtDays.Text);
            PrnRcptDtMst.NoOfPersons = Convert.ToInt32(txtNoOfPerson.Text);
            PrnRcptDtMst.UserId = UserInfo.UserId;
            // PrnRcptDtMst.NoOfPersons = Val(txtNoOfPnersons.Text)
            // PrnRcptDtMst.OutDate = FormatDateToString(dtpCheckOut.Value)
            // PrnRcptDtMst.OutTime = Format(Hour(dtpCheckOutTime.Text), "00") & ":" & Format(Minute(dtpCheckOutTime.Text), "00") & ":00"
            if (rbFlag == 0)
                PrnRcptDtMst.BhaktTypeId = (int)eBhaktaType.Bhkta;
            else if (rbFlag == 1)
                PrnRcptDtMst.BhaktTypeId = (int)eBhaktaType.Trip;
            else if (rbFlag == 2)
                PrnRcptDtMst.BhaktTypeId = (int)eBhaktaType.Gorup;
            // PrnRcptDtMst.RecordModifiedCount = Val(dtpCheckOut.Tag & vbNullString) + 1
            PrnRcptDtMst.ServerName = UserInfo.serverName;
            return PrnRcptDtMst;
        }

        private Collection GetPrnRcptDtDetColl()
        {
            Collection coll = new Collection();
            BedCheckInDet PrnRcptDtDet = new BedCheckInDet();
            double dblAmt = 0;
            // Dim item_name As String = ""
            item_name = "";
            string item_name1;
            PrnRcptDtDet.CheckOutMstId = Convert.ToInt64(txtVchNo.Tag);
            {
                var withBlock = fpsPrintReceipt;
                for (int ctr = 0; ctr <= withBlock.RowCount - 2; ctr++)
                {
                    PrnRcptDtDet.ProdId = Convert.ToInt32(withBlock.Rows[ctr].Cells[(int)ePrintReceipt.ProductN].Tag);
                    PrnRcptDtDet.Qty = Convert.ToInt32(withBlock.Rows[ctr].Cells[(int)ePrintReceipt.Qty].Value);
                    PrnRcptDtDet.Rent = Convert.ToDouble(withBlock.Rows[ctr].Cells[(int)ePrintReceipt.TotNidhi].Value);
                    PrnRcptDtDet.Advance = Convert.ToDouble(withBlock.Rows[ctr].Cells[(int)ePrintReceipt.Advance].Value);
                    PrnRcptDtDet.TotalRent = Convert.ToDouble(withBlock.Rows[ctr].Cells[(int)ePrintReceipt.TotNidhi].Value);
                    PrnRcptDtDet.TotalAdv = Convert.ToDouble(withBlock.Rows[ctr].Cells[(int)ePrintReceipt.TotAdv].Value);
                    coll.Add(PrnRcptDtDet);
                }
            }
            return coll;
        }

        private bool IsValidForm()
        {
            int i;
            col = new string[3];
            bool blnValidTag;
            string Filtercriteria;
            DataView Dv;
            DataRowView Drv;

            {
                var withBlock = fpsPrintReceipt;
                if (withBlock.RowCount >= 2)
                {
                    if (withBlock.Rows[withBlock.RowCount - 2].Cells[(int)ePrintReceipt.ProductN].Value.ToString() == "" && Convert.ToInt32(withBlock.Rows[withBlock.RowCount - 2].Cells[(int)ePrintReceipt.Qty].Value) == 0)
                        withBlock.Rows.RemoveAt(withBlock.RowCount - 2);
                }

                if (withBlock.RowCount == 1)
                {
                    MessageBox.Show("Please enter Item Details to save.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                // If .RowCount >= 2 Then
                // MessageBox.Show("Please Select only one Product.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                // IsValidForm = False
                // Exit Function
                // End If
                for (i = 0; i <= withBlock.RowCount - 2; i++)
                {
                    if (Convert.ToInt32(withBlock.Rows[i].Cells[(int)ePrintReceipt.ProductN].Value) == 0)
                    {
                        col[0] = "Menu";
                        col[1] = (i + 1).ToString();
                        blnValidTag = showError(fpsPrintReceipt, PrjMsgBoxTitle, cf.GetErrorMessage(col, 48), i, (int)ePrintReceipt.ProductN);
                        withBlock.CurrentCell = fpsPrintReceipt.Rows[i].Cells[(int)ePrintReceipt.ProductN];
                        return false;
                    }


                    if (Convert.ToInt32(withBlock.Rows[i].Cells[(int)ePrintReceipt.Qty].Value) == 0)
                    {
                        col[0] = "Qty";
                        col[1] = (i + 1).ToString();
                        blnValidTag = showError(fpsPrintReceipt, PrjMsgBoxTitle, cf.GetErrorMessage(col, 48), i, (int)ePrintReceipt.Qty);
                        withBlock.CurrentCell = fpsPrintReceipt.Rows[i].Cells[(int)ePrintReceipt.Qty];
                        return false;
                    }
                }
            }

            if (IsDuplicatePR())
            {
                return false;
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

            if (txtNoOfPerson.Text == "")
            {
                mStrErrMsg = new string[1];
                mStrErrMsg[0] = "No. of Person";
                return ShowValidateError(txtNoOfPerson, 0, mStrErrMsg, 1);
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

            return true;
        }
        private bool ShowValidateError(Control myObject, int tabIndex, string[] ErrMsg, int ErrNo)
        {
            setCursor(this, true);
            MessageBox.Show(cf.GetErrorMessage(ErrMsg, ErrNo), PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            myObject.Focus();
            return false;
        }
        private bool showError(DataGridView pControlObject, string pTitle, string pErrMessage, int intRow = -1, int intcol = -1)
        {
            try
            {
                setCursor(this, true);
                if (pControlObject.Name == "fpsAllctn")
                {
                }
                else
                {
                }
                MessageBox.Show(pErrMessage, pTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (intRow >= 0)
                {
                    pControlObject.CurrentCell = fpsPrintReceipt.Rows[intRow].Cells[intcol];
                }
                pControlObject.Focus();
                return false;
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return false;
            }
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
                        ProcErr = "Menu aready used.";
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
            frmSearchNew form1 = new frmSearchNew("BED_RECEIPT_MST_CHECK_OUT_FIND_V", false, eModType.BedSystem);
            //frmSearch form1 = new frmSearch("BED_RECEIPT_MST_CHECK_OUT_FIND_V", Convert.ToInt16(eModType.BedSystem));
            long lngSearchId;
            DialogResult sReply;


            form1.mIntCtrMachId = Convert.ToInt32(txtCounter.Tag);

            form1.ShowDialog();
            lngSearchId = form1.mLngSearchId;
            if (lngSearchId == 0)
                return;

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
            if (lngSearchId <= 0)
                return;
            setCursor(this, false);
            LoadTransaction(lngSearchId);
            if (mBlnEdit)
                mAction = eAction.ActionUpdate;
            else
                mAction = eAction.ActionLocked;
            LockDetail(!mBlnEdit);
            btnSave.Enabled = mBlnEdit;
            setCursor(this, true);
        }

        private void LoadTransaction(long lngSearchId)
        {
            System.Data.DataTable dr;
            BedCheckInDet PrnRcptDtDet = new BedCheckInDet();
            long lngCtrMachId;
            int bhaktype = 0;
            mDelColl = new Collection();
            FormClear();
            try
            {
                dr = BedCheckOutDALobj.GetDrBedCheckOutMst(lngSearchId);
                if (dr.Rows.Count > 0)
                {
                    dtpCheckIn.Value = Convert.ToDateTime(dr.Rows[0]["InDate"]);
                    txtmobno.Text = dr.Rows[0]["MOB_NO"].ToString();
                    dtpCheckInTime.Value = Convert.ToDateTime(dr.Rows[0]["InTime"]);
                    txtCheckInVchNo.Text = (dr.Rows[0]["InSerialNo"]).ToString();
                    txtCheckInVchNo.Tag = dr.Rows[0]["CheckInMstId"];
                    dtpCurDate.Tag = dr.Rows[0]["RecordModifiedCount"];
                    dtpCurDate.Value = Convert.ToDateTime(dr.Rows[0]["OutDate"]);
                    txtName.Text = dr.Rows[0]["Name"].ToString();
                    txtPlace.Text = dr.Rows[0]["Place"].ToString();

                    txtDays.Text = dr.Rows[0]["Days"].ToString();
                    // dtpCheckOut.Value = dr.Rows[0]["OutDate"]
                    dtpCheckOutTime.Value = Convert.ToDateTime(dr.Rows[0]["OutTime"]);
                    txtNoOfPerson.Text = dr.Rows[0]["NoOfPersons"].ToString();
                    txtVchNo.Text = (dr.Rows[0]["SerialNo"].ToString());
                    txtVchNo.Tag = lngSearchId;
                    dtpCheckIn.Value = Convert.ToDateTime(dr.Rows[0]["InDate"]);
                    dtpCheckInTime.Value = Convert.ToDateTime(dr.Rows[0]["InTime"]);
                    nudAdvance.Text = dr.Rows[0]["Advance"].ToString();
                    nudRent.Text = dr.Rows[0]["Rent"].ToString();
                    nudRefund.Value = Convert.ToDecimal(dr.Rows[0]["Refund"]);
                    dtEnteredOn = Convert.ToDateTime(dr.Rows[0]["EnteredOn"]);
                    txtName.Text = dr.Rows[0]["Name"].ToString();
                    bhaktype = Convert.ToInt32(dr.Rows[0]["BHAKT_TYPE"]);
                    txtPlace.Text = dr.Rows[0]["Place"] + "'";
                    txtTotalAmt.Value = Convert.ToDecimal(dr.Rows[0]["Advance"]) + Convert.ToDecimal(dr.Rows[0]["Rent"]);
                }
                //dr.Close();
                if (bhaktype == (int)eBhaktaType.Bhkta)
                    rdBhakta.Checked = true;
                else if (bhaktype == (int)eBhaktaType.Gorup)
                    rdGroup.Checked = true;
                else if (bhaktype == (int)eBhaktaType.Trip)
                    rdTrip.Checked = true;
                dr = BedCheckOutDALobj.GetDrPrintRcptDetOut(lngSearchId);
                {
                    var withBlock = fpsPrintReceipt;
                    withBlock.RowCount = 0;
                    withBlock.RowCount = 1;
                    //while (dr.Read)
                    foreach (DataRow drrow in dr.Rows)
                    {
                        withBlock.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.ProductN].Value = drrow["ItemTitle"];
                        withBlock.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.ProductC].ReadOnly = true;
                        withBlock.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.ProductN].Tag = drrow["ItemId"];
                        withBlock.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.ProductC].Value = drrow["ItemCode"];
                        withBlock.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.Qty].Value = (drrow["Qty"]);
                        withBlock.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.Nidhi].Value = (drrow["RENT"]);
                        withBlock.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.Advance].Value = (drrow["ADVANCE"]);
                        withBlock.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.TotAdv].Value = (drrow["TOTAL_ADVANCE"]);
                        withBlock.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.TotNidhi].Value = (drrow["TOTAL_RENT"]);
                        PrnRcptDtDet.CheckInDetId = Convert.ToInt64(drrow["PrintRcptDetId"]);
                        mDelColl.Add(PrnRcptDtDet);
                        withBlock.RowCount = withBlock.RowCount + 1;
                    }
                    //dr.Close();
                }

                FillItemMaster();

                mAction = eAction.ActionUpdate;
                blnformChange = false;
                dtpCurDate.Enabled = false;
                fpsPrintReceipt.Focus();
            }
            catch (Exception ex)
            {
                //if (!dr == null)
                //    dr.Close();
                MessageBox.Show("Load Transaction failed.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnNew_Click(null, null);
            }
        }
        private void btnClose_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void FrmBedCheckOut_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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
                            cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
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
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
        }

        private void CalculateAmt()
        {
            double dblAmt = 0;
            double dblAmtAdv = 0;
            {
                var withBlock = fpsPrintReceipt;
                for (int ctr = 0; ctr <= withBlock.RowCount - 1; ctr++)
                {
                    dblAmt = dblAmt + Math.Round(Convert.ToDouble(withBlock.Rows[ctr].Cells[(int)ePrintReceipt.TotNidhi].Value), 2);
                    dblAmtAdv = dblAmtAdv + Math.Round(Convert.ToDouble(withBlock.Rows[ctr].Cells[(int)ePrintReceipt.TotAdv].Value), 2);
                }
                nudRent.Value = Convert.ToDecimal(dblAmt);
                nudAdvance.Value = Convert.ToDecimal(dblAmtAdv);
                txtTotalAmt.Value = nudRent.Value + nudAdvance.Value;
            }
        }

        private void btnPrint_Click(System.Object sender, System.EventArgs e)
        {
            if (Convert.ToInt32(dtpCurDate.Tag) == 0 | blnformChange)
                return;
            if (mScreenID == eScreenID.PrintReceiptSup)
                return;
            string strReportName;
            Form sForm;
            Collection pColl = new Collection();
            System.Data.DataSet ds;
            setCursor(this, false);
            strReportName = "BedPrintReceiptCheckOut.rpt";
            try
            {
                {
                    var withBlock = fpsPrintReceipt;
                    if (withBlock.RowCount >= 2)
                    {
                        if (withBlock.Rows[withBlock.RowCount - 2].Cells[(int)ePrintReceipt.ProductN].Value.ToString() == "" && Convert.ToInt32(withBlock.Rows[withBlock.RowCount - 2].Cells[(int)ePrintReceipt.Qty].Value) == 0)
                            withBlock.Rows.RemoveAt(withBlock.RowCount - 2);
                    }
                    for (int ctr = 0; ctr <= withBlock.RowCount - 2; ctr++)
                    {
                        ds = FillDataInSystem(ctr);
                        sForm = new frmCrystalViewer(UserInfo.ReportPath + strReportName, null, ds, null, pColl, eScreenID.BedCheckIn, true);
                        sForm.Text = "Print Receipt : " + eReportID.BedCheckIn;
                        sForm.Show();
                        System.Threading.Thread.Sleep(500);
                        sForm.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("btnPrint_Click : " + ex.ToString(), PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            ds = null/* TODO Change to default(_) if this is not a reference type */;
            setCursor(this, true);
        }

        private System.Data.DataSet FillDataInSystem(int srno)
        {
            System.Data.DataTable TempTable1;
            //OSOL_ADMIN.clsDsCommon mObjDsCommon = new OSOL_ADMIN.clsDsCommon();
            System.Data.DataSet ds = new System.Data.DataSet();
            DataRow MyRow;
            int Qty = 0;
            TempTable1 = new System.Data.DataTable("BED_PRINT_RECEIPT_MST_T");


            TempTable1.Columns.Add("PRINT_RECEIPT_MST_ID", System.Type.GetType("System.Int64"));
            TempTable1.Columns.Add("PR_DATE", System.Type.GetType("System.DateTime"));
            TempTable1.Columns.Add("SERIAL_NO", System.Type.GetType("System.Int64"));
            TempTable1.Columns.Add("LOC_SH_NAME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("DEPT_SH_NAME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("COUNTER", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("AMOUNT", System.Type.GetType("System.Double"));
            TempTable1.Columns.Add("AMTINWORDS", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("NAME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("MOBILE", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("PLACE", System.Type.GetType("System.String"));

            TempTable1.Columns.Add("ENTERED_BY", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("ENTERED_ON", System.Type.GetType("System.DateTime"));
            TempTable1.Columns.Add("PRINT_TIME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("BED", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("BED_T", System.Type.GetType("System.String"));
            // TempTable2.Columns.Add("PRINT_RECEIP&k T_MST_ID", System.Type.GetType("System.Int64"))
            // 'TempTable2.Columns.Add("PRODUCT_CODE", System.Type.GetType("System.String"))
            // 'TempTable2.Columns.Add("PRODUCT_TITLE", System.Type.GetType("System.String"))
            // TempTable2.Columns.Add("QTY", System.Type.GetType("System.Double"))

            // TempTable2.Columns.Add("AMOUNT", System.Type.GetType("System.Double"))
            // TempTable2.Columns.Add("DET_AMT_WORDS", System.Type.GetType("System.String"))

            ds.Tables.Add(TempTable1);
            // ds.Tables.Add(TempTable2)

            TempTable1.Rows.Clear();
            // TempTable2.Rows.Clear()

            try
            {
                var withBlock = fpsPrintReceipt;
                MyRow = TempTable1.NewRow();
                MyRow["PRINT_RECEIPT_MST_ID"] = srno + 1;
                MyRow["PR_DATE"] = dtpCurDate.Value;
                MyRow["SERIAL_NO"] = (txtVchNo.Text);
                MyRow["LOC_SH_NAME"] = PrintReceiptLocName;
                MyRow["DEPT_SH_NAME"] = PrintReceiptDeptName;
                MyRow["COUNTER"] = mStrCounterMachineShortName;
                MyRow["AMOUNT"] = nudRent.Value;
                MyRow["AMTINWORDS"] = cf.getNumbersInWords(nudRent.Value, eCurrencyType.Rupees);
                MyRow["NAME"] = txtName.Text + "";
                MyRow["PLACE"] = txtPlace.Text + "";
                MyRow["MOBILE"] = txtmobno.Text + "";
                MyRow["ENTERED_BY"] = txtUser.Text;
                MyRow["ENTERED_ON"] = DateTime.Now;
                MyRow["PRINT_TIME"] = cf.GetComSysPrintTimeRpt();
                for (var ctr = 0; ctr <= withBlock.RowCount - 2; ctr++)
                {
                    if (ctr == srno)
                    {
                        Qty = Qty + Convert.ToInt32(withBlock.Rows[ctr].Cells[(int)ePrintReceipt.Qty].Value);
                        MyRow["BED_T"] = withBlock.Rows[ctr].Cells[(int)ePrintReceipt.ProductN].Value;
                    }
                }
                MyRow["BED"] = Qty;
                TempTable1.Rows.Add(MyRow);
            }
            catch (Exception ex)
            {
                MessageBox.Show("FillDataInSystem.Data.DataSet : " + ex.ToString(), PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                ds = null;
            }
            return ds;
        }


        private void fpsPrintReceipt_Enter(object sender, System.EventArgs e)
        {
            var withBlock = fpsPrintReceipt;
            withBlock.Rows[withBlock.CurrentCell.RowIndex].Cells[withBlock.CurrentCell.ColumnIndex].Style.BackColor = Color.White;

            DisableSendKeys = true;
        }

        private void fpsPrintReceipt_Leave(object sender, System.EventArgs e)
        {
            {
                var withBlock = fpsPrintReceipt;
                withBlock.Rows[withBlock.CurrentCell.RowIndex].Cells[withBlock.CurrentCell.ColumnIndex].Style.BackColor = Color.White;
                if (withBlock.RowCount >= 2)
                {
                    if (withBlock.Rows[withBlock.RowCount - 2].Cells[(int)ePrintReceipt.ProductN].Value.ToString() == "" && Convert.ToInt32(withBlock.Rows[withBlock.RowCount - 2].Cells[(int)ePrintReceipt.Qty].Value) == 0)
                        withBlock.Rows.RemoveAt(withBlock.RowCount - 2);
                }
            }
            DisableSendKeys = false;
        }


        private void LockDetail(bool blnLock)
        {
            {
                var withBlock = fpsPrintReceipt;
                withBlock.Columns[(int)ePrintReceipt.ProductN].ReadOnly = blnLock;
                withBlock.Columns[(int)ePrintReceipt.Qty].ReadOnly = blnLock;
            }
        }




        // Dim t1 As New ThreadStart(AddressOf CheckVersion)
        // Dim objThread As New Thread(t1)
        // Try

        // If Application.ProductVersion < FileVersionInfo.GetVersionInfo(System.Configuration.ConfigurationSettings.AppSettings.Get("ServerFilePath") & "JwlMfg.EXE").FileVersion Then
        // MsgBox("Upgrading JMG.", MsgBoxStyle.Information, PrjMsgBoxTitle)
        // objThread.Start()

        private bool IsDuplicatePR()
        {
            int i = 0;
            int j = 0;
            int index;
            index = (int)ePrintReceipt.ProductN;
            {
                var withBlock = fpsPrintReceipt;
                if (withBlock.RowCount == 1)
                    return false;
                for (i = 0; i <= withBlock.RowCount - 2; i++)
                {
                    for (j = i + 1; j <= withBlock.RowCount - 2; j++)
                    {
                        if (((withBlock.Rows[i].Cells[index].Tag == withBlock.Rows[j].Cells[index].Tag) || (withBlock.Rows[i].Cells[index].Value == withBlock.Rows[j].Cells[index].Value)) && withBlock.Rows[i].Cells[index].Value.ToString().Trim() != "" && Convert.ToInt32(withBlock.Rows[i].Cells[index].Tag) != 0)
                        {
                            MessageBox.Show("Menu Item : " + withBlock.Rows[j].Cells[index].Value.ToString() + " selected more than once. ", PrjMsgBoxTitle, MessageBoxButtons.OK);
                            withBlock.CurrentCell = fpsPrintReceipt.Rows[j].Cells[index];
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void fpsPrintReceipt_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            int ctr;
            int ctr1 = 0;
            long lngItemId;
            bool blnFlag = false;
            {
                var withBlock = fpsPrintReceipt;
                withBlock.Rows[withBlock.CurrentCell.RowIndex].Cells[withBlock.CurrentCell.ColumnIndex].Style.BackColor = Color.White;
                if (withBlock.CurrentCell.ColumnIndex == (int)ePrintReceipt.Qty)
                {
                    ctr1 = withBlock.CurrentCell.RowIndex;
                    lngItemId = Convert.ToInt64(withBlock.Rows[withBlock.CurrentCell.RowIndex].Cells[(int)ePrintReceipt.ProductN].Tag);
                    if (lngItemId == 0)
                        return;
                    for (ctr = 0; ctr <= ctr1 - 1; ctr++)
                    {
                        if (Convert.ToInt64(withBlock.Rows[ctr].Cells[(int)ePrintReceipt.ProductN].Tag) == lngItemId)
                        {
                            withBlock.Rows[ctr].Cells[(int)ePrintReceipt.Qty].Value = Convert.ToDouble(withBlock.Rows[ctr].Cells[(int)ePrintReceipt.Qty].Value) + Convert.ToDouble(withBlock.Rows[ctr1].Cells[(int)ePrintReceipt.Qty].Value);
                            withBlock.Rows[ctr].Cells[(int)ePrintReceipt.TotAdv].Value = Math.Round(Convert.ToDouble(withBlock.Rows[ctr].Cells[(int)ePrintReceipt.Advance].Value) * Convert.ToDouble(withBlock.Rows[ctr].Cells[(int)ePrintReceipt.Qty].Value), 2);
                            withBlock.Rows[ctr].Cells[(int)ePrintReceipt.TotNidhi].Value = Math.Round(Convert.ToDouble(withBlock.Rows[ctr].Cells[(int)ePrintReceipt.Nidhi].Value) * Convert.ToDouble(withBlock.Rows[ctr].Cells[(int)ePrintReceipt.Qty].Value), 2);
                            // .Cells(ctr, ePrintReceipt.Amount).Text = .Cells(ctr, ePrintReceipt.Price).Value * .Cells(ctr, ePrintReceipt.Qty).Value
                            blnFlag = true;
                            break;
                        }
                    }
                }
                if (blnFlag == true)
                {
                    withBlock.RowCount = withBlock.RowCount + 1;
                    withBlock.Rows.RemoveAt(ctr1);
                    withBlock.CurrentCell = fpsPrintReceipt.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.ProductN];
                    CalculateAmt();
                }
            }
        }

        private void fpsPrintReceipt_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            var withBlock = fpsPrintReceipt;
            // If fpsPrintReceipt.CurrentCell.ColumnIndex <> ePrintReceipt.MenuM Then
            withBlock.Rows[withBlock.CurrentCell.RowIndex].Cells[withBlock.CurrentCell.ColumnIndex].Style.BackColor = Color.Cyan;
            // End If
            if (withBlock.RowCount > 2)
            {
                if (withBlock.Rows[withBlock.RowCount - 2].Cells[(int)ePrintReceipt.ProductN].Value.ToString() == "")
                {
                    withBlock.Rows.RemoveAt(withBlock.RowCount - 2);
                    CalculateAmt();
                }
            }
            if (fpsPrintReceipt.CurrentCell.ColumnIndex == (int)ePrintReceipt.ProductN)
                SendKeys.Send("%{DOWN}");
        }

        private void fpsPrintReceipt_GotFocus(object sender, System.EventArgs e)
        {
            // fpsPrintReceipt.Cells(fpsPrintReceipt.CurrentCell.RowIndex, fpsPrintReceipt.CurrentCell.ColumnIndex).BackColor = Color.Cyan
            if (fpsPrintReceipt.CurrentCell.ColumnIndex == (int)ePrintReceipt.ProductN && mScreenID == eScreenID.PrintReceiptSup)
                SendKeys.Send("%{DOWN}");
        }

        //private void fpsPrintReceipt_SubEditorOpening(object sender, FarPoint.Win.Spread.SubEditorOpeningEventArgs e)
        //{
        //    e.Cancel = true;
        //}

        private void rdBhakta_Click(System.Object sender, System.EventArgs e)
        {
            rbFlag = 0;
        }

        private void rdTrip_Click(System.Object sender, System.EventArgs e)
        {
            rbFlag = 1;
        }

        private void rdGroup_Click(System.Object sender, System.EventArgs e)
        {
            rbFlag = 2;
        }

        private void btnLoad_Click(System.Object sender, System.EventArgs e)
        {
            System.Data.DataTable dr;
            BedCheckInDet PrnRcptDtDet = new BedCheckInDet();
            long lngCtrMachId;
            int bhaktype = 0;
            long mstId;
            // FormClear()
            try
            {
                dr = BedCheckOutDALobj.GetDsBedCheckInMstBarcode(0, "", txtCheckInVchNo.Text, Convert.ToInt64(txtCounter.Tag), UserInfo.CompanyID, PrintReceiptLocId, 0, 0, "");
                if (dr.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dr.Rows[0]["PendRoomCount"]) == 0)
                    {
                        MessageBox.Show("No pending Rooms against the Receipt No. " + txtCheckInVchNo.Text, PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCheckInVchNo.Text = "";
                        txtCheckInVchNo.Focus();
                        return;
                    }


                    // dtpCheckIn.Value = dr("InDate")
                    txtmobno.Text = dr.Rows[0]["MOB_NO"].ToString();
                    // dtpCheckInTime.Value = dr.Rows[0]["InTime"]
                    txtCheckInVchNo.Text = (dr.Rows[0]["SerialNo"].ToString());
                    txtCheckInVchNo.Tag = dr.Rows[0]["CheckInMstId"];
                    // txtVchNo.Tag = lngSearchId
                    // dtpCheckIn.Tag = dr.Rows[0]["RecordModifiedCount"]
                    txtName.Text = dr.Rows[0]["Name"].ToString();
                    txtPlace.Text = dr.Rows[0]["Place"].ToString();
                    dtpCheckIn.Value = Convert.ToDateTime(dr.Rows[0]["InDate"]);
                    dtpCheckInTime.Value = Convert.ToDateTime(dr.Rows[0]["InTime"]);
                    txtDays.Text = dr.Rows[0]["Days"].ToString();
                    // dtpCheckOut.Value = dr.Rows[0]["OutDate"]
                    // dtpCheckOutTime.Value = dr.Rows[0]["OutTime"]
                    txtNoOfPerson.Text = dr.Rows[0]["NoOfPersons"].ToString();
                    mstId = Convert.ToInt64(dr.Rows[0]["CheckInMstId"]);

                    nudAdvance.Text = dr.Rows[0]["Advance"].ToString();
                    nudRent.Text = dr.Rows[0]["Rent"].ToString();
                    dtEnteredOn = Convert.ToDateTime(dr.Rows[0]["EnteredOn"]);
                    txtCheckInVchNo.Tag = dr.Rows[0]["CheckInMstId"];
                    bhaktype = Convert.ToInt32(dr.Rows[0]["BHAKT_TYPE"]);
                    txtTotalAmt.Value = Convert.ToDecimal(dr.Rows[0]["Advance"]) + Convert.ToDecimal(dr.Rows[0]["Rent"]);
                    nudRefund.Value = Convert.ToDecimal(dr.Rows[0]["Advance"]);
                    if (bhaktype == (int)eBhaktaType.Bhkta)
                        rdBhakta.Checked = true;
                    else if (bhaktype == (int)eBhaktaType.Gorup)
                        rdGroup.Checked = true;
                    else if (bhaktype == (int)eBhaktaType.Trip)
                        rdTrip.Checked = true;
                    dr = BedCheckOutDALobj.GetDrPrintRcptDet(mstId, UserInfo.UserId);
                    {
                        var withBlock = fpsPrintReceipt;
                        withBlock.RowCount = 0;
                        withBlock.RowCount = 1;
                        //while (dr.Read)
                        foreach (DataRow drrow in dr.Rows)
                        {
                            withBlock.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.ProductN].Value = drrow["ItemTitle"];
                            withBlock.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.ProductC].ReadOnly = true;
                            withBlock.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.ProductN].Tag = drrow["ItemId"];
                            withBlock.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.ProductC].Value = drrow["ItemCode"];
                            withBlock.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.Qty].Value = (drrow["Qty"]);
                            withBlock.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.Nidhi].Value = (drrow["RENT"]);
                            withBlock.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.Advance].Value = (drrow["ADVANCE"]);
                            withBlock.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.TotAdv].Value = (drrow["TOTAL_ADVANCE"]);
                            withBlock.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.TotNidhi].Value = (drrow["TOTAL_RENT"]);
                            PrnRcptDtDet.CheckInDetId = Convert.ToInt64(drrow["PrintRcptDetId"]);
                            mDelColl.Add(PrnRcptDtDet);
                            withBlock.RowCount = withBlock.RowCount + 1;
                        }
                        //dr.Close();
                    }

                    FillItemMaster();

                    // mAction = eAction.ActionUpdate
                    // blnformChange = False
                    // dtpCheckIn.Enabled = False
                    fpsPrintReceipt.Focus();
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
                //if (!dr == null)
                //    dr.Close();
                MessageBox.Show("Load Transaction failed.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnNew_Click(null, null);
            }
        }

        private void txtVchNo_TextChanged(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
        }
        private void txtCheckInVchNo_TextChanged(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
        }


    }

}
