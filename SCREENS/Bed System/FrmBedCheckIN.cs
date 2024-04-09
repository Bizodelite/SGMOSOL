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
using static SGMOSOL.BAL.BadBAL;
using System.Web.UI.WebControls;

namespace SGMOSOL.SCREENS.BedSystem
{
    public partial class FrmBedCheckIN : Form
    {
        private eScreenID mScreenID;
        private int rbFlag;
        private bool mBlnEdit = false;
        private eAction mAction;        // Reference to Enum of Type Action for ActionView,ActionNew,ActionUpdate and ActionDelete
        private ArrayList CtrlArr = new ArrayList(); // To insert Form Control in Array List
        private ArrayList btnArr = new ArrayList();
        //private OSOL_ADMIN.clsDsCommon cf = new OSOL_ADMIN.clsDsCommon();
        //private OSOL_BLSDS.clsBlsBedPrintReceipt objBlsInvAlloc = new OSOL_BLSDS.clsBlsBedPrintReceipt();
        //private OSOL_BLSDS.clsDsBedReceipt objDsPrintReceiptationMst = new OSOL_BLSDS.clsDsBedReceipt();
        //private OSOL_BLSDS.clsDsBedReceiptDet objDsMessPrintReceiptDet = new OSOL_BLSDS.clsDsBedReceiptDet();
        private bool DisableSendKeys;

        private Helper ObjImageFilePath = new Helper();

        private bool bkDateEntry = false;
        private bool blnformChange;
        private System.Windows.Forms.ListBox lstItemMaster = new System.Windows.Forms.ListBox();
        private System.Data.DataSet dsItemMaster;
        private string[] col;
        private string[] mStrErrMsg;
        public long mSearchId;
        private Collection mDelColl = new Collection();
        private DateTime dtEnteredOn;
        private string mStrCounterMachineShortName;
        private int PrintReceiptDeptID;
        private string PrintReceiptDeptName;
        private string PrintReceiptLocName;
        private int PrintReceiptLocId;
        private string PrintReceiptitem_names;
        public static string itemnames; // 
                                        // ' Public RandomNo As String = GenerateRandomString(4)
        public string BarcodeRet = "";
        const short WM_CAP = 0x400;
        public static string item_name = "";
        const int WM_CAP_DRIVER_CONNECT = WM_CAP + 10;
        const int WM_CAP_DRIVER_DISCONNECT = WM_CAP + 11;
        const int WM_CAP_EDIT_COPY = WM_CAP + 30;

        const int WM_CAP_SET_PREVIEW = WM_CAP + 50;
        const int WM_CAP_SET_PREVIEWRATE = WM_CAP + 52;
        const int WM_CAP_SET_SCALE = WM_CAP + 53;
        const int WS_CHILD = 0x40000000;
        const int WS_VISIBLE = 0x10000000;
        const short SWP_NOMOVE = 0x2;
        const short SWP_NOSIZE = 1;
        const short SWP_NOZORDER = 0x4;
        const short HWND_BOTTOM = 1;

        private int iDevice = 0; // Current device ID
        private int hHwnd; // Handle to preview window

        [System.Runtime.InteropServices.DllImport("user32")]
        static extern int SendMessage(int hwnd, int wMsg, int wParam, [MarshalAs(UnmanagedType.AsAny)] object lParam);

        [System.Runtime.InteropServices.DllImport("user32")]
        static extern int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);

        [System.Runtime.InteropServices.DllImport("user32")]
        static extern bool DestroyWindow(int hndw);

        [System.Runtime.InteropServices.DllImport("avicap32.dll"
            )]
        static extern int capCreateCaptureWindowA(string lpszWindowName, int dwStyle, int x, int y, int nWidth, short nHeight, int hWndParent, int nID);

        [System.Runtime.InteropServices.DllImport("avicap32.dll")]
        static extern bool capGetDriverDescriptionA(short wDriver, string lpszName, int cbName, string lpszVer, int cbVer);

        CommonFunctions cf = new CommonFunctions();
        BedCheckINDAL BedReceiptDALobj = new BedCheckINDAL();
        public FrmBedCheckIN(eScreenID pScreenId)
        {
            InitializeComponent();
            mScreenID = pScreenId;
            this.Closing += new CancelEventHandler(this.FrmBedCheckIN_Closing);
            this.fpsPrintReceipt.GotFocus += fpsPrintReceipt_GotFocus;
            this.txtCheckIN.GotFocus += txtCheckIN_GotFocus;
            this.txtNoOfPerson.GotFocus += txtNoOfPerson_GotFocus;
            this.txtNoOfPerson.LostFocus += txtNoOfPerson_LostFocus;
        }

        public string GenerateRandomString(int iLength)
        {
            Random rdm = new Random();
            char[] allowChrs = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLOMNOPQRSTUVWXYZ0123456789".ToCharArray();
            string sResult = "";
            for (int i = 0; i <= iLength - 1; i++)
                sResult += allowChrs[rdm.Next(0, allowChrs.Length)];

            return sResult;
        }
        private void FrmBedCheckIN_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            // fpsPrintReceipt.Sheets(0).Cells(fpsPrintReceipt.Sheets(0).ActiveRowIndex, fpsPrintReceipt.Sheets(0).ActiveColumnIndex).BackColor = Color.White
            if (e.KeyCode == Keys.Enter & !DisableSendKeys)
                SendKeys.Send("{tab}");

            // If e.KeyCode = Keys.Enter And fpsPrintReceipt.Focus And DisableSendKeys And _
            // fpsPrintReceipt.Sheets(0).ActiveColumnIndex = ePrintReceipt.MenuM Then
            // SendKeys.Send("%{DOWN}")
            // End If

            // If fpsPrintReceipt.Focus Then
            // If fpsPrintReceipt.Sheets(0).ActiveColumnIndex = ePrintReceipt.MenuM Then
            // 'SendKeys.Send("%{DOWN}")
            // End If
            // End If

            if (e.KeyCode == Keys.End & (mAction == eAction.ActionInsert | mAction == eAction.ActionUpdate) & blnformChange)
                btnSave_Click(null, null);
        }

        //private void FrmBedCheckIN_KeyDown(System.Object sender, System.Windows.Forms.KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.End & (mAction == eAction.ActionInsert | mAction == eAction.ActionUpdate) & blnformChange)
        //        btnSave_Click(null, null);
        //}
        private void FrmBedCheckIN_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End & (mAction == eAction.ActionInsert | mAction == eAction.ActionUpdate) & blnformChange)
                btnSave_Click(null, null);
        }



        private void FrmBedCheckIN_Load(System.Object sender, System.EventArgs e)
        {
            cf.setControlsonForm(this, CtrlArr, btnArr);
            cf.SetUserScreenActions(this, UserInfo.UserId, (Int64)mScreenID, btnArr, null, mBlnEdit);
            ScreenToCenter();
            txtTotalAmt.Enabled = false;
            nudAdvance.Enabled = false;

            txtUser.Text = UserInfo.UserName;
            FillCounter();

            if (txtCounter.Tag == null || txtCounter.Tag.ToString() == "0")
            {
                MessageBox.Show("User/Counter/Machine ID assign conflict. Please contact system administrator.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            FillItemMaster();

            OpenPreviewWindow2();
            if (btnNew.Enabled)
                btnNew_Click(null, null);
        }
        private void ScreenToCenter()
        {
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }

        private void FormClear()
        {
            txtScan.Clear();
            txtCheckIN.Clear();
            imgVideo.ImageLocation = "";
            imgVideo_1.ImageLocation = "";
            imgVideo_1.Image = null;
            PictureBox_Bhakt.Image = null;
            txtImagePath.Text = "";
            txtVchNo.Text = "";
            txtDays.Text = "1";
            txtmobno.Text = "";
            txtName.Text = "";
            txtNoOfPerson.Text = "";
            txtPlace.Text = "";
            nudAdvance.Value = 0;
            nudRent.Value = 0;
            txtTotalAmt.Value = 0;
            fpsPrintReceipt.Rows.Clear();
            txtVchNo.Tag = null;

            System.Drawing.Image IMG = null;
            imgVideo_1.BackgroundImage = IMG;
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
                dr = BedReceiptDALobj.GetDrMaxSrNo(Convert.ToInt64(txtCounter.Tag), UserInfo.CompanyID, PrintReceiptLocId, 0, UserInfo.fy_id);
                if (dr.Rows.Count > 0)
                    txtVchNo.Text = (Convert.ToInt32(dr.Rows[0]["SerialNo"]) + 1).ToString();
                else
                    txtVchNo.Text = "1";
            }
            catch (Exception ex)
            {
            }
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

        private void btnNew_Click(System.Object sender, System.EventArgs e)
        {
            System.Data.DataTable dr;
            FormClear();
            cf.fncSetDateAndRange(dtpCheckIn);
            cf.fncSysTime(dtpCheckInTime);

            dtpCheckIn.Enabled = bkDateEntry;
            mAction = eAction.ActionInsert;

            FillItemMaster();
            btnSave.Enabled = btnNew.Enabled;
            GetmaxNo();
            blnformChange = false;
        }
        public void SetTextBoxBackAndForeColor(System.Windows.Forms.TextBox txtTextBox)
        {
            txtTextBox.BackColor = Color.FromArgb(87, 86, 128);
            txtTextBox.ForeColor = Color.White;
        }

        public void ReSetTextBoxBackAndForeColor(System.Windows.Forms.TextBox txtTextBox)
        {
            txtTextBox.BackColor = Color.White;
            txtTextBox.ForeColor = Color.Black;
        }
        private void FillItemMaster()
        {
            string strDate;
            dsItemMaster = null;
            dsItemMaster = new System.Data.DataSet();
            lstItemMaster.Items.Clear();
            strDate = FormatDateToString(dtpCheckIn.Value);
            try
            {
                dsItemMaster = cf.GetDsProductMenu(UserInfo.UserId);
            }
            catch (Exception ex)
            {
            }
            if (dsItemMaster.Tables[0].Rows.Count > 0)
                cf.FillComboWithDataSet(lstItemMaster, dsItemMaster.Tables[0], "ItemTitle", "ItemTitle", "ItemId", "ItemCode", "");

            DataGridViewComboBoxCell cboItem = new DataGridViewComboBoxCell();
            foreach (var item in lstItemMaster.Items)
            {
                cboItem.Items.Add(item.ToString());
            }
            ((DataGridViewComboBoxColumn)fpsPrintReceipt.Columns[(int)ePrintReceipt.ProductN]).DataSource = cboItem.Items;
        }

        private void fpsPrintReceipt_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
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

                        withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.TotAdv].Value = Math.Round(Convert.ToDecimal(withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.Advance].Value) * Convert.ToDecimal(withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.Qty].Value), 2);
                        withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.TotNidhi].Value = Math.Round(Convert.ToDecimal(withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.Nidhi].Value) * Convert.ToDecimal(withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.Qty].Value), 2);
                        break;
                    }
                }
            }
            else if (e.ColumnIndex == (int)ePrintReceipt.Qty)
            {
                withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.TotAdv].Value = Convert.ToDecimal(withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.Advance].Value) * Convert.ToDecimal(withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.Qty].Value);
                withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.TotNidhi].Value = Convert.ToDecimal(withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.Nidhi].Value) * Convert.ToDecimal(withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.Qty].Value);
            }
            blnformChange = true;
            CalculateAmt();
        }

        private void fpsPrintReceipt_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            try
            {
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
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("fpsPrintReceipt_KeyDown: " + ex.Message);
            }
        }
        private void fpsPrintReceipt_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (fpsPrintReceipt.CurrentCell.ColumnIndex == 0 && e.Control is ComboBox)
            {
                ComboBox comboBox = e.Control as ComboBox;
                comboBox.SelectedIndexChanged -= LastColumnComboSelectionChanged;
                comboBox.SelectedIndexChanged += LastColumnComboSelectionChanged;
            }
        }

        private void LastColumnComboSelectionChanged(object sender, EventArgs e)
        {
            Int32 lngItemId;
            {
                var withBlock = fpsPrintReceipt;
                if (withBlock.CurrentCell.ColumnIndex == (int)ePrintReceipt.ProductN)
                {
                    lngItemId = Convert.ToInt32(withBlock.Rows[withBlock.CurrentCell.RowIndex].Cells[withBlock.CurrentCell.ColumnIndex].Tag);
                    if (lngItemId > 0)
                        withBlock.CurrentCell = fpsPrintReceipt.Rows[withBlock.CurrentCell.RowIndex].Cells[(int)ePrintReceipt.Qty];
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
        //        if (withBlock.CurrentCell.ColumnIndex == (int)ePrintReceipt.ProductN)
        //        {
        //            lngItemId = Convert.ToInt32(withBlock.Rows[withBlock.CurrentCell.RowIndex].Cells[withBlock.CurrentCell.ColumnIndex].Tag);
        //            if (lngItemId > 0)
        //                withBlock.CurrentCell = fpsPrintReceipt.Rows[withBlock.CurrentCell.RowIndex].Cells[(int)ePrintReceipt.Qty];
        //        }
        //    }
        //}
        private void btnSave_Click(System.Object sender, System.EventArgs e)
        {
            long lngError = -1;
            if (blnformChange == false)
                return;
            btnSave.Enabled = false;
            if (fncSave())
            {
                blnformChange = false;
                btnNew_Click(null, null);
                MessageBox.Show("Record Saved Successfully.");
            }
            btnSave.Enabled = true;
        }

        private bool fncSave()
        {
            IDataObject data;
            System.Drawing.Image bmap;
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
            lngSerialNo = Convert.ToInt64(txtVchNo.Text);
            PrintRcptMst = GetPrnRcptDtMst();
            coll = GetPrnRcptDtDetColl();
            string s = item_name;
            if (mAction == eAction.ActionInsert)
            {
                lngError = BedReceiptDALobj.Insert(PrintRcptMst, coll, UserInfo.UserName, UserInfo.Machine_Name, lngSerialNo, dtEnteredOn);
                if (lngError > 0)
                    txtVchNo.Text = lngError.ToString();
            }
            else if (mAction == eAction.ActionUpdate)
            {
            }
            dgScanPrievew dlgSP = new dgScanPrievew();
            dlgSP.ImgPath = System.Configuration.ConfigurationManager.AppSettings.Get("BedScannerPath") + Interaction.IIf(Information.IsDBNull(PrintRcptMst.Image), PrintRcptMst.ScanDoc + ".bmp", PrintRcptMst.Image);
            dlgSP.ShowDialog();
            setCursor(this, true);
            if (lngError < 0)
            {
                strErr = ProcErr(lngError);
                col = new string[1];
                col[0] = "";
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
                setCursor(this, true);
                flag = true;

                dtpCheckIn.Tag = 1;
                blnformChange = false;
                if (mAction == eAction.ActionInsert)
                    PrintOrigReceipt();
                else
                    btnPrint_Click(null, null);
            }
            return flag;
        }

        private BedCheckInMst GetPrnRcptDtMst()
        {
            BedCheckInMst PrnRcptDtMst = new BedCheckInMst();
            double dblRentAmt = 0;
            double dblAdvAmt = 0;
            int Qty = 0;
            Int16 ctr;
            string strImagePath;
            string strScanPath;
            if (txtImagePath.Text != "")
                strImagePath = txtImagePath.Text;
            else
                strImagePath = "";

            if (txtScanDoc.Text != "")
                strScanPath = txtScanDoc.Text;
            else
                strScanPath = "";

            PrnRcptDtMst.ScanDoc = Scan();

            if ((Helper.final == ""))
                PrnRcptDtMst.Image = val1.Replace(@"\", "");
            else
                PrnRcptDtMst.Image = Helper.final;
            PrnRcptDtMst.CheckIn_ID = txtCheckIN.Text;
            PrnRcptDtMst.CheckInMstId = Convert.ToInt64(txtVchNo.Tag);
            PrnRcptDtMst.ComId = UserInfo.CompanyID;
            PrnRcptDtMst.LocId = PrintReceiptLocId;  // UserInfo.LocationID
            PrnRcptDtMst.DeptId = PrintReceiptDeptID; // UserInfo.DepartmentID
            PrnRcptDtMst.FyId = UserInfo.fy_id;
            PrnRcptDtMst.UserId = UserInfo.UserId;
            PrnRcptDtMst.CtrMachId = Convert.ToInt64(txtCounter.Tag);
            PrnRcptDtMst.InDate = dtpCheckIn.Value;
            PrnRcptDtMst.InTime = dtpCheckInTime.Value;
            for (ctr = 0; ctr <= fpsPrintReceipt.RowCount - 2; ctr++)
            {
                dblRentAmt = dblRentAmt + Math.Round(Convert.ToDouble(fpsPrintReceipt.Rows[ctr].Cells[(int)ePrintReceipt.TotNidhi].Value), 0);
                dblAdvAmt = dblAdvAmt + Math.Round(Convert.ToDouble(fpsPrintReceipt.Rows[ctr].Cells[(int)ePrintReceipt.TotAdv].Value), 0);
                Qty = Qty + Convert.ToInt32(Math.Round(Convert.ToDouble(fpsPrintReceipt.Rows[ctr].Cells[(int)ePrintReceipt.Qty].Value), 0));
            }
            PrnRcptDtMst.NoOfBeds = Qty;
            PrnRcptDtMst.Advance = dblAdvAmt;
            PrnRcptDtMst.Rent = dblRentAmt;
            PrnRcptDtMst.Place = txtPlace.Text;
            PrnRcptDtMst.Name = txtName.Text;
            PrnRcptDtMst.mob_no = txtmobno.Text;
            PrnRcptDtMst.Days = Convert.ToInt32(txtDays.Text);
            PrnRcptDtMst.NoOfPersons = Convert.ToInt32(txtNoOfPerson.Text);
            PrnRcptDtMst.UserId = UserInfo.UserId;
            PrnRcptDtMst.Barcode = GenerateRandomString(4) + txtVchNo.Text;
            txtmobno.Tag = PrnRcptDtMst.Barcode;
            PrnRcptDtMst.OutDate = dtpCheckOut.Value;
            PrnRcptDtMst.OutTime = dtpCheckOutTime.Value;
            if (rbFlag == 0)
                PrnRcptDtMst.BhaktTypeId = (int)eBhaktaType.Bhkta;
            else if (rbFlag == 1)
                PrnRcptDtMst.BhaktTypeId = (int)eBhaktaType.Trip;
            else if (rbFlag == 2)
                PrnRcptDtMst.BhaktTypeId = (int)eBhaktaType.Gorup;
            PrnRcptDtMst.RecordModifiedCount = Convert.ToInt64(dtpCheckOut.Tag) + 1;
            PrnRcptDtMst.ImagePath = strScanPath;
            PrnRcptDtMst.ServerName = UserInfo.serverName;
            return PrnRcptDtMst;
        }

        public static string val1 = "";
        private string Scan()
        {
            string Scan = "";
            string tempfile;
            WIA.Device mydevice;
            WIA.Item item;
            WIA.ImageFile F;
            System.Windows.Forms.Label Err_btnTakePicture_click;
            WIA.CommonDialog CommonDialogBox = new WIA.CommonDialog();
            WIA.CommonDialog Commondialog1 = new WIA.CommonDialog();
            string F1 = System.Configuration.ConfigurationManager.AppSettings.Get("BedScannerPath");
            string s;
            DialogResult answer;


            try
            {
                WIA.DeviceManager DeviceManager1 = new WIA.DeviceManager();//Interaction.CreateObject("WIA.DeviceManager");
                int i = 0;
                mydevice = CommonDialogBox.ShowSelectDevice(WIA.WiaDeviceType.ScannerDeviceType, true, false);

                if (DeviceManager1.DeviceInfos[1].Type == WIA.WiaDeviceType.ScannerDeviceType)
                {
                    WIA.Device Scanner = DeviceManager1.DeviceInfos[1].Connect();
                    if (Information.IsNothing(Scanner))
                    {
                        Interaction.MsgBox("Could not connect to scanner please check attached Properly.");
                        return null;
                    }
                    else
                        try
                        {
                            s = txtName.Text + txtVchNo.Text;
                            {
                                Scanner.Items[1].Properties["6146"].set_Value(WIA.WiaImageIntent.ColorIntent);  // 4 is Black-white,gray is 2, color 1 (Color Intent)
                            }
                            F = (WIA.ImageFile)Scanner.Items[1].Transfer("{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}");

                            if (Information.IsNothing(F))
                            {
                                answer = MessageBox.Show("There is no file in scanner do you want to scan blank image", "Yes/no sample", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (answer == DialogResult.Yes)
                                {
                                    return null;
                                }
                            }
                            val1 = s + "." + F.FileExtension;
                            F1 = F1 + val1;
                            Scan = s;
                            if (File.Exists(F1))
                                FileSystem.Kill(F1);
                            F.SaveFile(F1);
                            txtScan.Text = s;
                            txtScanDoc.Text = F1;
                        }
                        catch (Exception ex)
                        {
                            Interaction.MsgBox(ex.Message);
                            Scan = "No Images scan";
                        }
                        finally
                        {
                            Scanner = null;
                        }
                }
                else
                    Interaction.MsgBox("Scanner is not attached checked it");

                DeviceManager1 = null;
            }
            catch (Exception ex)
            {
                answer = MessageBox.Show("There is no file in scanner or scanner not attached do you want to scan blank image", "Yes/no sample", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer == DialogResult.Yes)
                {
                    return "";
                }
            }
            return Scan;
        }

        private Collection GetPrnRcptDtDetColl()
        {
            Collection coll = new Collection();
            BedCheckInDet PrnRcptDtDet = new BedCheckInDet();

            item_name = "";
            {
                var withBlock = fpsPrintReceipt;
                for (int ctr = 0; ctr <= withBlock.RowCount - 2; ctr++)
                {
                    PrnRcptDtDet.CheckInMstId = Convert.ToInt64(txtVchNo.Tag);
                    PrnRcptDtDet.ProdId = Convert.ToInt32(withBlock.Rows[ctr].Cells[(int)ePrintReceipt.ProductN].Tag);
                    PrnRcptDtDet.Qty = Convert.ToInt32(withBlock.Rows[ctr].Cells[(int)ePrintReceipt.Qty].Value);
                    PrnRcptDtDet.Rent = Convert.ToDouble(withBlock.Rows[ctr].Cells[(int)ePrintReceipt.Nidhi].Value);
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
                    if (withBlock.Rows[withBlock.RowCount - 2].Cells[(int)ePrintReceipt.ProductN].Value.ToString() == "" & Convert.ToInt32(withBlock.Rows[withBlock.RowCount - 2].Cells[(int)ePrintReceipt.Qty].Value) == 0)
                        withBlock.Rows.RemoveAt(withBlock.RowCount - 2);
                }

                if (withBlock.RowCount == 1)
                {
                    MessageBox.Show("Please enter Item Details to save.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (withBlock.RowCount > 2)
                {
                    MessageBox.Show("Please Select only one Product.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                for (i = 0; i <= withBlock.RowCount - 2; i++)
                {
                    int MaxQty = dsItemMaster.Tables[0].AsEnumerable().Where(r => r.Field<int>("ItemId") == Convert.ToInt32(withBlock.Rows[i].Cells[(int)ePrintReceipt.ProductN].Tag)).Select(h => h.Field<int>("ProductCount")).FirstOrDefault();
                    if (Convert.ToInt32(withBlock.Rows[i].Cells[(int)ePrintReceipt.ProductN].Tag) == 0)
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
                    if (Convert.ToInt32(withBlock.Rows[i].Cells[(int)ePrintReceipt.Qty].Value) > MaxQty)
                    {
                        col[0] = "Qty";
                        col[1] = (i + 1).ToString();
                        MessageBox.Show("Quantity should not greater than " + withBlock.Rows[i].Cells[(int)ePrintReceipt.ProductN].Tag + "!", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            if (nudRent.Value > UserInfo.BedCheckInMaxAmount)
            {
                MessageBox.Show("Please Check The Total Dengi Amount.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                nudRent.Focus();
                return false;
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
            frmSearchNew form1 = new frmSearchNew("BED_RECEIPT_MST_FIND_V", false, eModType.BedSystem);
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
            {
                mAction = eAction.ActionUpdate;
                txtName.Enabled = false;
                GroupBox1.Enabled = false;
                txtmobno.Enabled = false;
                txtCheckIN.Enabled = false;
                txtPlace.Enabled = false;
                txtDays.Enabled = false;
                txtNoOfPerson.Enabled = false;
            }
            else
            {
                mAction = eAction.ActionLocked;
                txtName.Enabled = false;
                GroupBox1.Enabled = false;
                txtmobno.Enabled = false;
                txtCheckIN.Enabled = false;
                txtPlace.Enabled = false;
                txtDays.Enabled = false;
                txtNoOfPerson.Enabled = false;
            }
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
                dr = BedReceiptDALobj.GetDsBedCheckInMst(lngSearchId);
                if (dr.Rows.Count > 0)
                {
                    txtCheckIN.Text = dr.Rows[0]["CheckIn_ID"].ToString();
                    dtpCheckIn.Value = Convert.ToDateTime(dr.Rows[0]["InDate"]);
                    txtmobno.Text = dr.Rows[0]["MOB_NO"].ToString();
                    dtpCheckInTime.Value = Convert.ToDateTime(dr.Rows[0]["InTime"]);
                    txtVchNo.Text = dr.Rows[0]["SerialNo"].ToString();
                    txtVchNo.Tag = lngSearchId;
                    dtpCheckIn.Tag = dr.Rows[0]["RecordModifiedCount"];
                    txtName.Text = dr.Rows[0]["Name"].ToString();
                    txtPlace.Text = dr.Rows[0]["Place"].ToString();
                    txtScan.Text = dr.Rows[0]["ScanImageName"].ToString();
                    imgVideo.ImageLocation = System.Configuration.ConfigurationManager.AppSettings.Get("BedScannerPath") + Interaction.IIf(Information.IsDBNull(dr.Rows[0]["Image"]), dr.Rows[0]["ScanImageName"].ToString() + ".bmp", dr.Rows[0]["Image"].ToString());
                    txtDays.Text = dr.Rows[0]["Days"].ToString();
                    dtpCheckOut.Value = Convert.ToDateTime(dr.Rows[0]["OutDate"]);
                    dtpCheckOutTime.Value = Convert.ToDateTime(dr.Rows[0]["OutTime"]);
                    txtNoOfPerson.Text = dr.Rows[0]["NoOfPersons"].ToString();

                    BarcodeRet = dr.Rows[0]["Barcode"].ToString();
                    nudAdvance.Text = dr.Rows[0]["Advance"].ToString();
                    nudRent.Text = dr.Rows[0]["Rent"].ToString();
                    dtEnteredOn = Convert.ToDateTime(dr.Rows[0]["EnteredOn"]);

                    bhaktype = Convert.ToInt32(dr.Rows[0]["BHAKT_TYPE"]);
                    txtTotalAmt.Value = Convert.ToDecimal(dr.Rows[0]["Advance"]) + Convert.ToDecimal(dr.Rows[0]["Rent"]);
                }
                //dr.Close();
                if (bhaktype == (int)eBhaktaType.Bhkta)
                    rdBhakta.Checked = true;
                else if (bhaktype == (int)eBhaktaType.Gorup)
                    rdGroup.Checked = true;
                else if (bhaktype == (int)eBhaktaType.Trip)
                    rdTrip.Checked = true;
                dr = BedReceiptDALobj.GetDrPrintRcptDet(lngSearchId, UserInfo.UserId);
                {
                    var withBlock = fpsPrintReceipt;
                    withBlock.RowCount = 0;
                    withBlock.RowCount = 1;
                    foreach (DataRow drrow in dr.Rows)
                    {
                        withBlock.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.ProductN].Value = drrow["ItemTitle"];
                        withBlock.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.ProductC].ReadOnly = true;
                        withBlock.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.ProductN].Tag = drrow["ItemId"];
                        withBlock.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.ProductC].Value = drrow["ItemCode"];
                        withBlock.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.Qty].Value = (drrow["Qty"]);
                        withBlock.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.Nidhi].Value = (drrow["PRICE"]);
                        withBlock.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.Advance].Value = (drrow["ADVANCE"]);
                        withBlock.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.TotAdv].Value = (drrow["TOTAL_ADVANCE"]);
                        withBlock.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.TotNidhi].Value = (drrow["TOTAL_RENT"]);
                        PrnRcptDtDet.CheckInDetId = Convert.ToInt64(drrow["PrintRcptDetId"]);
                        mDelColl.Add(PrnRcptDtDet);
                        withBlock.RowCount = withBlock.RowCount + 1;
                    }
                }

                FillItemMaster();

                mAction = eAction.ActionUpdate;
                blnformChange = true;

                dtpCheckIn.Enabled = false;
                fpsPrintReceipt.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load Transaction failed.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnNew_Click(null, null);
            }
        }
        private void btnClose_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void FrmBedCheckIN_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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
                MessageBox.Show(ex.Message, PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                txtTotalAmt.Value = Convert.ToDecimal(dblAmt + dblAmtAdv);
            }
        }



        private void PrintOrigReceipt()
        {
            if (Convert.ToInt32(dtpCheckIn.Tag) == 0 | blnformChange)
                return;
            if (mScreenID == eScreenID.PrintReceiptSup)
                return;
            string strReportName;
            string strReportName1;
            Form sForm;
            Collection pColl = new Collection();
            System.Data.DataSet ds;
            setCursor(this, false);
            Int16 ctr;
            // strReportName = "BedePrintReceipt.rpt"
            strReportName1 = "BedCheckInDengi1.rdlc";
            try
            {
                {
                    var withBlock = fpsPrintReceipt;
                    if (withBlock.RowCount >= 2)
                    {
                        if (withBlock.Rows[withBlock.RowCount - 2].Cells[(int)ePrintReceipt.ProductN].Value.ToString() == "" & Convert.ToInt32(withBlock.Rows[withBlock.RowCount - 2].Cells[(int)ePrintReceipt.Qty].Value) == 0)
                            withBlock.Rows.RemoveAt(withBlock.RowCount - 2);
                    }
                    for (ctr = 0; ctr <= withBlock.RowCount - 2; ctr++)
                    {
                        ds = FillDataInDataset(ctr, txtmobno.Tag.ToString());

                        sForm = new frmCrystalViewer(UserInfo.ReportPath + strReportName1, null, ds, null, pColl, eScreenID.BedCheckIn, true);
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
            ds = null;
            setCursor(this, true);
        }

        private void btnPrint_Click(System.Object sender, System.EventArgs e)
        {
            if (Convert.ToInt32(dtpCheckIn.Tag) == 0 | blnformChange)
                return;
            if (mScreenID == eScreenID.PrintReceiptSup)
                return;
            string strReportName;
            string strReportName1;
            Form sForm;
            Collection pColl = new Collection();
            System.Data.DataSet ds;
            setCursor(this, false);
            Int16 ctr;
            strReportName = "BedPrintReceiptDup1.rdlc";
            strReportName1 = "BedCheckInDengi1Dup.rdlc";
            try
            {
                {
                    var withBlock = fpsPrintReceipt;
                    if (withBlock.RowCount >= 2)
                    {
                        if (withBlock.Rows[withBlock.RowCount - 2].Cells[(int)ePrintReceipt.ProductN].Value.ToString() == "" && Convert.ToInt32(withBlock.Rows[withBlock.RowCount - 2].Cells[(int)ePrintReceipt.Qty].Value) == 0)
                            withBlock.Rows.RemoveAt(withBlock.RowCount - 2);
                    }
                    for (ctr = 0; ctr <= withBlock.RowCount - 2; ctr++)
                    {
                        ds = FillDataInDataset(ctr, BarcodeRet);

                        sForm = new frmCrystalViewer(UserInfo.ReportPath + strReportName, null, ds, null, pColl, eScreenID.BedCheckIn, true);
                        sForm.Text = "Print Receipt : " + eReportID.BedCheckIn;
                        sForm.Show();
                        System.Threading.Thread.Sleep(500);
                        sForm.Close();


                        sForm = new frmCrystalViewer(UserInfo.ReportPath + strReportName1, null, ds, null, pColl, eScreenID.BedCheckIn, true);
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
            ds = null;
            setCursor(this, true);
        }

        private System.Data.DataSet FillDataInDataset(Int16 srno, string Barcode)
        {
            System.Data.DataTable TempTable1;
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
            TempTable1.Columns.Add("ID", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("BARCODE", System.Type.GetType("System.Byte[]"));
            TempTable1.Columns.Add("ENTERED_BY", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("ENTERED_ON", System.Type.GetType("System.DateTime"));
            TempTable1.Columns.Add("PRINT_TIME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("BED", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("BED_T", System.Type.GetType("System.String"));

            ds.Tables.Add(TempTable1);
            TempTable1.Rows.Clear();

            try
            {
                var withBlock = fpsPrintReceipt;
                MyRow = TempTable1.NewRow();
                MyRow["PRINT_RECEIPT_MST_ID"] = srno + 1; // Val(dtpPrnRcptDt.Tag & vbNullString)
                MyRow["PR_DATE"] = dtpCheckIn.Value;
                MyRow["SERIAL_NO"] = txtVchNo.Text;
                MyRow["LOC_SH_NAME"] = PrintReceiptLocName;
                MyRow["DEPT_SH_NAME"] = PrintReceiptDeptName;
                MyRow["COUNTER"] = mStrCounterMachineShortName;
                MyRow["AMOUNT"] = txtTotalAmt.Value;
                MyRow["AMTINWORDS"] = cf.getNumbersInWords(txtTotalAmt.Value.ToString(), eCurrencyType.Rupees);
                MyRow["NAME"] = txtName.Text + "";
                MyRow["PLACE"] = txtPlace.Text + "";
                MyRow["MOBILE"] = txtmobno.Text + "";
                MyRow["ENTERED_BY"] = txtUser.Text;
                MyRow["ENTERED_ON"] = DateTime.Now; // dtEnteredOn
                MyRow["PRINT_TIME"] = cf.GetComSysPrintTimeRpt(); // dtEnteredOn
                MyRow["ID"] = txtCheckIN.Text;
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
                MessageBox.Show("FillDataInDataset : " + ex.ToString(), PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                ds = null;
            }
            return ds;
        }


        private void fpsPrintReceipt_Enter(object sender, System.EventArgs e)
        {
            var withBlock = fpsPrintReceipt;
            if (withBlock.CurrentCell == null)
            {
                return;
            }
            fpsPrintReceipt.Rows[withBlock.CurrentCell.RowIndex].Cells[withBlock.CurrentCell.ColumnIndex].Style.BackColor = Color.White;
            DisableSendKeys = true;
        }

        private void fpsPrintReceipt_Leave(object sender, System.EventArgs e)
        {
            var withBlock = fpsPrintReceipt;
            withBlock.Rows[withBlock.CurrentCell.RowIndex].Cells[withBlock.CurrentCell.ColumnIndex].Style.BackColor = Color.White;
            if (withBlock.RowCount >= 2)
            {
                if (withBlock.Rows[withBlock.RowCount - 2].Cells[(int)ePrintReceipt.ProductN].Value == null && Convert.ToInt32(withBlock.Rows[withBlock.RowCount - 2].Cells[(int)ePrintReceipt.Qty].Value) == 0)
                    withBlock.Rows.RemoveAt(withBlock.RowCount - 2);
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
                        if (((withBlock.Rows[i].Cells[index].Tag == withBlock.Rows[j].Cells[index].Tag) || (withBlock.Rows[i].Cells[index].Value == withBlock.Rows[j].Cells[index].Value)) && withBlock.Rows[i].Cells[index].Value.ToString() != "" && Convert.ToInt32(withBlock.Rows[i].Cells[index].Tag + "") != 0)
                        {
                            MessageBox.Show("Menu Item : " + withBlock.Rows[j].Cells[index].Value + " selected more than once. ", PrjMsgBoxTitle, MessageBoxButtons.OK);
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
            var withBlock = fpsPrintReceipt;
            int ctr;
            long lngItemId;
            int ctr1 = withBlock.CurrentCell.RowIndex;
            bool blnFlag = false;
            {
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
            if (fpsPrintReceipt.CurrentCell == null)
            {
                return;
            }
            var withBlock = fpsPrintReceipt;
            // End If
            withBlock.Rows[withBlock.CurrentCell.RowIndex].Cells[withBlock.CurrentCell.ColumnIndex].Style.BackColor = Color.Cyan;
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
            if (fpsPrintReceipt.CurrentCell == null)
            {
                return;
            }
            fpsPrintReceipt.Rows[fpsPrintReceipt.CurrentCell.RowIndex].Cells[fpsPrintReceipt.CurrentCell.ColumnIndex].Style.BackColor = Color.Cyan;
            if (fpsPrintReceipt.CurrentCell.ColumnIndex == (int)ePrintReceipt.ProductN & mScreenID == eScreenID.PrintReceiptSup)
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



        private void txtmobno_TextChanged(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
        }


        private void txtCheckIN_GotFocus(object sender, System.EventArgs e)
        {
            System.Windows.Forms.TextBox t1 = (System.Windows.Forms.TextBox)sender;
            SetTextBoxBackAndForeColor(t1);
        }
        private void txtChec(object sender, System.EventArgs e)
        {
            System.Windows.Forms.TextBox t1 = (System.Windows.Forms.TextBox)sender;
            ReSetTextBoxBackAndForeColor(t1);
        }

        private void txtNoOfPerson_GotFocus(object sender, System.EventArgs e)
        {
            System.Windows.Forms.TextBox t1 = (System.Windows.Forms.TextBox)sender;
            SetTextBoxBackAndForeColor(t1);
        }

        private void txtNoOfPerson_LostFocus(object sender, System.EventArgs e)
        {
            System.Windows.Forms.TextBox t1 = (System.Windows.Forms.TextBox)sender;
            ReSetTextBoxBackAndForeColor(t1);
            fpsPrintReceipt.CurrentCell = fpsPrintReceipt.Rows[0].Cells[(int)ePrintReceipt.ProductN];
            fpsPrintReceipt.Focus();
            SendKeys.Send("%{DOWN}");
        }


        private void txtName_TextChanged(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
        }

        private void txtPlace_TextChanged(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
        }

        private void txtNoOfPerson_TextChanged(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
        }


        private void txtCheckIN_TextChanged(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
        }

        private void OpenPreviewWindow()
        {
            int iHeight = imgVideo.Height;
            int iWidth = imgVideo.Width;

            hHwnd = capCreateCaptureWindowA(iDevice.ToString(), WS_VISIBLE | WS_CHILD, 0, 0, 640, 480, imgVideo.Handle.ToInt32(), 0);

            if (SendMessage(hHwnd, WM_CAP_DRIVER_CONNECT, iDevice, 0) == 1)
            {
                SendMessage(hHwnd, WM_CAP_SET_SCALE, 1, 0);

                SendMessage(hHwnd, WM_CAP_SET_PREVIEWRATE, 66, 0);

                SendMessage(hHwnd, WM_CAP_SET_PREVIEW, 1, 0);

                SetWindowPos(hHwnd, HWND_BOTTOM, 0, 0, imgVideo.Width, imgVideo.Height, SWP_NOMOVE | SWP_NOZORDER);
            }
            else
                DestroyWindow(hHwnd);
        }


        private void OpenPreviewWindow2()
        {
            int iHeight = imgVideo_1.Height;
            int iWidth = imgVideo_1.Width;

            IDataObject data;
            System.Drawing.Image bmap;

            string strFileName;
            hHwnd = capCreateCaptureWindowA(iDevice.ToString(), WS_VISIBLE | WS_CHILD, 0, 0, 640, 480, imgVideo_1.Handle.ToInt32(), 0);

            if (SendMessage(hHwnd, WM_CAP_DRIVER_CONNECT, iDevice, 0) == 1)
            {
                SendMessage(hHwnd, WM_CAP_SET_SCALE, 1, 0);

                SendMessage(hHwnd, WM_CAP_SET_PREVIEWRATE, 50, 0);

                SendMessage(hHwnd, WM_CAP_SET_PREVIEW, 1, 0);

                SetWindowPos(hHwnd, HWND_BOTTOM, 0, 0, imgVideo_1.Width, imgVideo_1.Height, SWP_NOMOVE | SWP_NOZORDER);


                if (SendMessage(hHwnd, WM_CAP_DRIVER_CONNECT, iDevice, 0) == 1)
                    SendMessage(hHwnd, WM_CAP_EDIT_COPY, 0, 0);
            }
            else
            {
                DestroyWindow(hHwnd);

                btnSave.Enabled = false;
            }
        }

        private void ClosePreviewWindow()
        {
            SendMessage(hHwnd, WM_CAP_DRIVER_DISCONNECT, iDevice, 0);

            DestroyWindow(hHwnd);
        }

        private void BhaktImgCap_Click(System.Object sender, System.EventArgs e)
        {
            int iHeight = imgVideo_1.Height;
            int iWidth = imgVideo_1.Width;

            IDataObject data;
            System.Drawing.Image bmap;

            string strFileName;

            if (txtName.Text != "")
            {
                if (SendMessage(hHwnd, WM_CAP_DRIVER_CONNECT, iDevice, 0) == 1)
                {
                    SendMessage(hHwnd, WM_CAP_EDIT_COPY, 0, 0);

                    data = Clipboard.GetDataObject();

                    if (data.GetDataPresent(typeof(System.Drawing.Bitmap)))
                    {
                        bmap = (System.Drawing.Image)data.GetData(typeof(System.Drawing.Bitmap));
                        imgVideo_1.Image = bmap;
                        ClosePreviewWindow();
                        txtImagePath.Text = (txtName.Text) + System.DateTime.Now.ToString("dd/MM/yyyy");
                        strFileName = ObjImageFilePath.Final_SaveImageCapture(bmap, txtImagePath.Text);
                        txtImagePath.Text = strFileName;
                        PictureBox_Bhakt.Image = System.Drawing.Image.FromFile(strFileName);
                    }
                    OpenPreviewWindow2();
                }
            }
            else
                MessageBox.Show("Enter Bhakt Name and Please try again!");
        }


        private void imgVideo_1_Click(System.Object sender, System.EventArgs e)
        {
            if (txtName.Text != "")
                OpenPreviewWindow2();
            else
                MessageBox.Show("Enter Bhakt Name and Please try again!");
        }

        private void btnScan_Click(System.Object sender, System.EventArgs e)
        {
            Scan_Document();
        }

        public void Scan_Document()
        {
            string tempfile;
            WIA.Device mydevice;
            WIA.Item item;
            WIA.ImageFile F;
            System.Windows.Forms.Label Err_btnTakePicture_click;
            WIA.CommonDialog CommonDialogBox = new WIA.CommonDialog();
            WIA.CommonDialog Commondialog1 = new WIA.CommonDialog();
            string F1 = System.Configuration.ConfigurationManager.AppSettings.Get("BedScannerPath");
            string s;
            DialogResult answer;


            try
            {
                WIA.DeviceManager DeviceManager1 = new WIA.DeviceManager();//= Interaction.CreateObject("WIA.DeviceManager");
                int i = 0;
                mydevice = CommonDialogBox.ShowSelectDevice(WIA.WiaDeviceType.ScannerDeviceType, true, false);

                if (DeviceManager1.DeviceInfos[1].Type == WIA.WiaDeviceType.ScannerDeviceType)
                {
                    WIA.Device Scanner = DeviceManager1.DeviceInfos[1].Connect();
                    if (Information.IsNothing(Scanner))
                    {
                        Interaction.MsgBox("Could not connect to scanner please check attached Properly.");
                        return;
                    }
                    else
                        try
                        {
                            s = txtName.Text + txtVchNo.Text;
                            {
                                var withBlock = Scanner.Items[1];
                                withBlock.Properties["6146"].set_Value(WIA.WiaImageIntent.ColorIntent);  // 4 is Black-white,gray is 2, color 1 (Color Intent)
                            }
                            F = (WIA.ImageFile)Scanner.Items[1].Transfer("{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}");
                            //F = (WIA.ImageFile)Scanner.Items[1].Transfer(WIA.FormatID.wiaFormatJPEG);

                            if (Information.IsNothing(F))
                            {
                                answer = MessageBox.Show("There is no file in scanner do you want to scan blank image", "Yes/no sample", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (answer == DialogResult.Yes)
                                    return;
                            }
                            val1 = s + "." + F.FileExtension;
                            F1 = F1 + val1;
                            txtScanDoc.Text = s;
                            if (File.Exists(F1))
                                FileSystem.Kill(F1);
                            F.SaveFile(F1);
                            txtScan.Text = s;
                        }
                        catch (Exception ex)
                        {
                            Interaction.MsgBox(ex.Message);
                        }
                        finally
                        {
                            Scanner = null;
                        }
                }
                else
                    Interaction.MsgBox("Scanner is not attached checked it");

                imgVideo.ImageLocation = F1;
                DeviceManager1 = null;
            }

            catch (Exception ex)
            {
                answer = MessageBox.Show("There is no file in scanner or scanner not attached do you want to scan blank image", "Yes/no sample", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer == DialogResult.Yes)
                    return;
            }
        }

        private void txtNoOfPerson_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}
