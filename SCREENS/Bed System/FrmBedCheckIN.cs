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
            //SetGridScreen();
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
                //this.Close();
                return;
            }

            FillItemMaster();

            // commented by girish
            OpenPreviewWindow2();
            // objThread.Start()
            if (btnNew.Enabled)
                btnNew_Click(null, null);
        }
        private void ScreenToCenter()
        {
            //Int32 ctr;
            //this.Width = this.Width + 200;
            //ctr = (MDI.Size.Width - this.Size.Width) / (double)2;
            //this.Location = new System.Drawing.Point(ctr, 0);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }
        //private void SetGridScreen()
        //{
        //    FarPoint.Win.Spread.InputMap inputmap1 = new FarPoint.Win.Spread.InputMap();
        //    FarPoint.Win.Spread.FpSpread FpSpread;
        //    // -- Set object equal to existing map
        //    Int16 ctr;
        //    // For ctr = 1 To 2
        //    // If ctr = 1 Then
        //    // FpSpread = fpsPrintReceipt
        //    // Else
        //    // FpSpread = fpsMinLevel
        //    // End If
        //    FpSpread = fpsPrintReceipt;
        //    inputmap1 = FpSpread.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
        //    // -- Map Enter key
        //    inputmap1.Put(new FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap);
        //    // -- Create InputMap object
        //    FarPoint.Win.Spread.InputMap inputmap2 = new FarPoint.Win.Spread.InputMap();
        //    // -- Set object equal to existing map
        //    inputmap2 = FpSpread.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused);
        //    // -- Map Enter key
        //    inputmap2.Put(new FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap);
        //    // -- Map Tab key 
        //    inputmap1.Put(new FarPoint.Win.Spread.Keystroke(Keys.Tab, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
        //    // -- set Tab key to move to next control
        //    inputmap1 = FpSpread.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
        //    // -- set shift + Tab key to move to previous control
        //    inputmap1.Put(new FarPoint.Win.Spread.Keystroke(Keys.Tab, Keys.Shift), FarPoint.Win.Spread.SpreadActions.None);
        //    // -- Create InputMap object
        //    FarPoint.Win.Spread.InputMap inputmap3 = new FarPoint.Win.Spread.InputMap();
        //    // -- Set object equal to existing map
        //    inputmap3 = FpSpread.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused);
        //    // -- Map F2 key to edit the enabled cells
        //    inputmap3.Put(new FarPoint.Win.Spread.Keystroke(Keys.F2, Keys.None), FarPoint.Win.Spread.SpreadActions.StartEditing);

        //    // Dim sfir As New FarPoint.Win.Spread.SolidFocusIndicatorRenderer(Color.Blue, 2)

        //    // fpsPrintReceipt.FocusRenderer = sfir

        //    // fpsPrintReceipt.FocusRenderer = New MyIndicator()

        //    fpsPrintReceipt.Sheets(0).Columns(ePrintReceipt.ProductC).Width = 0;
        //    this.fpsPrintReceipt_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
        //}

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
            //fpsPrintReceipt.Sheets(0).RowCount = 0;
            //fpsPrintReceipt.Sheets(0).RowCount = 1;
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
            //dr.Close();
        }

        // Private Sub Fillitem_names()
        // Dim dr As SqlClient.SqlDataReader
        // dr = cf.getitem_names(UserInfo.UserId, SystemHDDModelNo, SystemHDDSerialNo, SystemMacID)
        // If dr.Read Then

        // PrintReceiptitem_names = dr("item_names")

        // End If
        // dr.Close()
        // End Sub
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
                //dr.Close();
            }
            catch (Exception ex)
            {

                //if (!dr == null)
                //    dr.Close();
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
            // OpenPreviewWindow2()
            btnSave.Enabled = btnNew.Enabled;
            GetmaxNo();
            // If txtVchNo.Text = "" Then
            // System.Threading.Thread.Sleep(500)
            // GetmaxNo()
            // End If

            // fpsPrintReceipt.Focus()
            // fpsPrintReceipt.Sheets(0).Cells(0, ePrintReceipt.ProductN).BackColor = Color.Cyan
            // fpsPrintReceipt.Sheets(0).SetActiveCell(0, ePrintReceipt.ProductN)
            // SendKeys.Send("%{DOWN}")

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
            //OSOL_ADMIN.clsDsCommon mObjDsCommon = new OSOL_ADMIN.clsDsCommon();
            string strTime = "";

            dsItemMaster = null;
            dsItemMaster = new System.Data.DataSet();
            lstItemMaster.Items.Clear();
            strDate = FormatDateToString(dtpCheckIn.Value);
            try
            {
                // Code change filter added - 19/12/2019 
                // dsItemMaster = cf.GetDsProductMenu()
                dsItemMaster = cf.GetDsProductMenu(UserInfo.UserId);
            }
            catch (Exception ex)
            {
            }
            if (dsItemMaster.Tables[0].Rows.Count > 0)
                cf.FillComboWithDataSet(lstItemMaster, dsItemMaster.Tables[0], "ItemTitle", "ItemTitle", "ItemId", "ItemCode", "");

            //FarPoint.Win.Spread.CellType.ComboBoxCellType cboItem = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            //cboItem.ListControl = lstItemMaster;
            //cboItem.Editable = true;
            //fpsPrintReceipt.Columns[(int)ePrintReceipt.ProductN].CellType = cboItem;

            DataGridViewComboBoxCell cboItem = new DataGridViewComboBoxCell();
            cboItem.DataSource = lstItemMaster;
            cboItem.ReadOnly = true;

            fpsPrintReceipt.Columns[(int)ePrintReceipt.ProductN].CellTemplate = cboItem;
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

                        withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.TotAdv].Value = Math.Round(Convert.ToDecimal(withBlock.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) * Convert.ToDecimal(withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.Qty].Value), 2);
                        withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.TotNidhi].Value = Math.Round(Convert.ToDecimal(withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.Nidhi].Value) * Convert.ToDecimal(withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.Qty].Value), 2);
                        break;
                    }
                }
            }
            else if (e.ColumnIndex == (int)ePrintReceipt.Qty)
            {

                // .Cells(e.Row, ePrintReceipt.Amount).Text = RoundIt(.Cells(e.Row, ePrintReceipt.Price).Value * .Cells(e.Row, ePrintReceipt.Qty).Value, 2)
                withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.TotAdv].Value = Convert.ToDecimal(withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.Advance].Value) * Convert.ToDecimal(withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.Qty].Value);
                withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.TotNidhi].Value = Convert.ToDecimal(withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.Nidhi].Value) * Convert.ToDecimal(withBlock.Rows[e.RowIndex].Cells[(int)ePrintReceipt.Qty].Value);
            }
            blnformChange = true;
            CalculateAmt();
        }

        private void fpsPrintReceipt_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            // Dim ctr As Int16
            // Dim ctr1 As Int16
            // Dim lngItemId As Long
            // Dim blnFlag As Boolean = False
            // Dim dblQty As Double

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
            // Call Scan_Document()
            if (fncSave())
            {
                // commented by girish
                // LoadItemMinLevel()
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
            string a;

            // If SendMessage(hHwnd, WM_CAP_DRIVER_CONNECT, iDevice, 0) Then
            // SendMessage(hHwnd, WM_CAP_EDIT_COPY, 0, 0)
            // '
            // ' Get image from clipboard and convert it to a bitmap
            // '
            // data = Clipboard.GetDataObject()
            // If data.GetDataPresent(GetType(System.Drawing.Bitmap)) Then
            // bmap = CType(data.GetData(GetType(System.Drawing.Bitmap)), Image)
            // imgVideo.Image = bmap
            // ClosePreviewWindow()
            // '  If sfdImage.ShowDialog = DialogResult.OK Then
            // ' bmap.Save("d:\a.png", Imaging.ImageFormat.Png)0
            // Helper.SaveImageCapture(bmap, "Person-Img")
            // 'End If
            // End If
            // End If

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
                dtpCheckIn.Tag = 1;
                blnformChange = false;
                // 'btnPrint_Click(Nothing, Nothing)
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

            // PrnRcptDtMst.ScanDoc = Scan(PrnRcptDtMst)
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
            // PrnRcptDtMst.Amount = nudAmount.Value
            PrnRcptDtMst.Advance = dblAdvAmt;
            PrnRcptDtMst.Rent = dblRentAmt;
            PrnRcptDtMst.Place = txtPlace.Text;
            PrnRcptDtMst.Name = txtName.Text;
            PrnRcptDtMst.mob_no = txtmobno.Text;
            PrnRcptDtMst.Days = Convert.ToInt32(txtDays.Text);
            PrnRcptDtMst.NoOfPersons = Convert.ToInt32(txtNoOfPerson.Text);
            PrnRcptDtMst.UserId = UserInfo.UserId;
            // 'PrnRcptDtMst.Barcode = txtName.Text.Substring(0, 4) & txtVchNo.Text
            PrnRcptDtMst.Barcode = GenerateRandomString(4) + txtVchNo.Text;
            txtmobno.Tag = PrnRcptDtMst.Barcode;
            // PrnRcptDtMst.NoOfPersons = Val(txtNoOfPnersons.Text)
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

        // Private Function Scan(ByVal Collection As OSOL_CONNECTION.BedCheckInMst) As String
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
            // Dim BedCheckInMst As New OSOL_CONNECTION.BedCheckInMst
            string F1 = System.Configuration.ConfigurationManager.AppSettings.Get("BedScannerPath");
            string s;
            DialogResult answer;


            try
            {
                // If ((mydevice) Is Nothing) Then
                // MsgBox("Scanner is not attached.")
                // Return Nothing
                // Exit Function
                // End If

                // mydevice = CommonDialogBox.ShowSelectDevice(WIA.WiaDeviceType.UnspecifiedDeviceType, True, False)
                // F = CommonDialogBox.ShowAcquireImage(WIA.WiaDeviceType.CameraDeviceType, WIA.WiaImageIntent.ColorIntent, WIA.WiaImageBias.MinimizeSize, , True, True)
                WIA.DeviceManager DeviceManager1 = new WIA.DeviceManager();//Interaction.CreateObject("WIA.DeviceManager");
                int i = 0;
                // For i = 1 To DeviceManager1.DeviceInfos.Count
                mydevice = CommonDialogBox.ShowSelectDevice(WIA.WiaDeviceType.ScannerDeviceType, true, false);

                //if (DeviceManager1.DeviceInfos[1].Type == 1)
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
                            // s = txtName.Text & txtAppNo.Text
                            s = txtName.Text + txtVchNo.Text;
                            // F.SaveFile(F1)
                            // F = Scanner.Items(1).Transfer()
                            // F = CommonDialogBox.ShowAcquireImage(WIA.WiaDeviceType.ScannerDeviceType, WIA.WiaImageIntent.ColorIntent, WIA.WiaImageBias.MinimizeSize, , False, False, True)
                            {
                                Scanner.Items[1].Properties["6146"].set_Value(WIA.WiaImageIntent.ColorIntent);  // 4 is Black-white,gray is 2, color 1 (Color Intent)
                            }
                            F = (WIA.ImageFile)Scanner.Items[1].Transfer("{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}");
                            //F = (WIA.ImageFile)Scanner.Items[1].Transfer(WIA.FormatID.wiaFormatJPEG);

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
                            // Collection.ScanDoc = s
                            Scan = s;
                            //var filesystemobject = Interaction.CreateObject("Scripting.FileSystemObject");
                            if (File.Exists(F1))
                                FileSystem.Kill(F1);
                            F.SaveFile(F1);
                            txtScan.Text = s;
                            txtScanDoc.Text = F1;
                        }
                        // Dim obj As OSOL_BLSDS.clsBlsRoomCheckIn = New OSOL_BLSDS.clsBlsRoomCheckIn()
                        // i = obj.UpdateScan(CheckInMst.ScanDoc, txtAppNo.Text)
                        // If i = -1 Then
                        // MsgBox("Document Scan successfully")
                        // Else
                        // MsgBox("Document not Scan successfully")
                        // End If
                        catch (Exception ex)
                        {
                            Interaction.MsgBox(ex.Message);
                            Scan = "No Images scan";
                        }
                        // Return Collection.ScanDoc = "No Images scan"
                        finally
                        {
                            Scanner = null/* TODO Change to default(_) if this is not a reference type */;
                        }
                }
                else
                    Interaction.MsgBox("Scanner is not attached checked it");

                // Return Collection.ScanDoc

                // Next
                DeviceManager1 = null;
            }
            catch (Exception ex)
            {
                answer = MessageBox.Show("There is no file in scanner or scanner not attached do you want to scan blank image", "Yes/no sample", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer == DialogResult.Yes)
                {
                    return null;
                }
            }
            return Scan;

        }

        private Collection GetPrnRcptDtDetColl()
        {
            Collection coll = new Collection();
            BedCheckInDet PrnRcptDtDet = new BedCheckInDet();

            double dblAmt = 0;
            // Dim item_name As String = ""
            item_name = "";
            string item_name1;
            Int32 i;
            {
                var withBlock = fpsPrintReceipt;
                for (int ctr = 0; ctr <= withBlock.RowCount - 2; ctr++)
                {
                    PrnRcptDtDet.CheckInMstId = Convert.ToInt64(txtVchNo.Tag);
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
                    if (Convert.ToInt32(withBlock.Rows[i].Cells[(int)ePrintReceipt.Qty].Value) > 50)
                    {
                        col[0] = "Qty";
                        col[1] = (i + 1).ToString();
                        MessageBox.Show("Quantity should not greater than 50!", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        withBlock.CurrentCell = fpsPrintReceipt.Rows[i].Cells[(int)ePrintReceipt.Qty];
                        return false;
                    }
                }
            }

            // If IsDuplicate(fpsPrintReceipt, ePrintReceipt.MenuM) Then
            // ReDim col(1)
            // col(0) = "Code"
            // col(1) = "Print Receipt"
            // MessageBox.Show(GetErrorMessage(col, 77), PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
            // fpsPrintReceipt.Focus()
            // IsValidForm = False
            // Exit Function
            // End If


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

            // If Val(nudCash.Text & vbNullString) < Val(nudAmount.Text & vbNullString) And chkGuestNo.Checked And Val(nudCash.Text & vbNullString) > 0 Then
            // col(0) = "Cash"
            // col(1) = (i + 1).ToString
            // MessageBox.Show("Please enter cash amount of atleast total amount.", PrjMsgBoxTitle, MessageBoxButtons.OK)
            // nudCash.Focus()
            // IsValidForm = False
            // Exit Function
            // End If

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
                    //pControlObject.ActiveRowIndex = intRow;
                    //pControlObject.ActiveRowIndex = intcol;
                    //pControlObject.SetActiveCell(intRow, intcol);
                    pControlObject.CurrentCell = fpsPrintReceipt.Rows[intRow].Cells[intcol];
                }
                // If pControlObject.name <> "fps_qlty" Then blnEnterNavigation = False
                pControlObject.Focus();
                return false;
            }
            catch (Exception ex)
            {
                // blnEnterNavigation = False
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
                // code change - location filter added - 19/12/2019
                dr = BedReceiptDALobj.GetDrPrintRcptDet(lngSearchId, UserInfo.UserId);
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
                        withBlock.Rows[withBlock.RowCount - 1].Cells[(int)ePrintReceipt.Nidhi].Value = (drrow["PRICE"]);
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
                blnformChange = true;

                dtpCheckIn.Enabled = false;
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
            // objThread.Abort()
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CalculateAmt()
        {
            double dblAmt = 0;
            double dblAmtAdv = 0;
            // 'If mScreenID = eScreenID.PrintReceiptSup Then
            // '    nudAmount.Value = 0
            // '    Exit Sub
            // 'End If
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
            // Commented by Amit 09-04-2018
            // strReportName = "BedePrintReceipt.rpt"
            strReportName1 = "BedCheckInDengi1.rpt";
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

                        // ds.Tables(0).Columns.Add(New DataColumn("Barcode", GetType(Byte())))
                        // LinearCrystal(Barcode = New LinearCrystal())


                        // Commented by Amit 09-04-2018

                        // sForm = New frmCrystalViewer(UserInfo.ReportPath & strReportName, , ds, , pColl, ReportID.BedCheckIn, True)
                        // sForm.Text = "Print Receipt : " & ReportID.BedCheckIn
                        // sForm.Show()
                        // System.Threading.Thread.Sleep(500)
                        // sForm.Close()

                        // added by payal
                        //sForm = new frmCrystalViewer(UserInfo.ReportPath + strReportName1, null, ds, null, pColl, eReportID.BedCheckIn, true);
                        //sForm.Text = "Print Receipt : " + eReportID.BedCheckIn;
                        //sForm.Show();
                        //System.Threading.Thread.Sleep(500);
                        //sForm.Close();
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
            strReportName = "BedPrintReceiptDup1.rpt";
            strReportName1 = "BedCheckInDengi1Dup.rpt";
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

                        // ds.Tables(0).Columns.Add(New DataColumn("Barcode", GetType(Byte())))
                        // LinearCrystal(Barcode = New LinearCrystal())



                        //sForm = new frmCrystalViewer(UserInfo.ReportPath + strReportName, null, ds, null, pColl, eReportID.BedCheckIn, true);
                        //sForm.Text = "Print Receipt : " + eReportID.BedCheckIn;
                        //sForm.Show();
                        //System.Threading.Thread.Sleep(500);
                        //sForm.Close();


                        //// added by payal
                        //sForm = new frmCrystalViewer(UserInfo.ReportPath + strReportName1, null, ds, null, pColl, eReportID.BedCheckIn, true);
                        //sForm.Text = "Print Receipt : " + eReportID.BedCheckIn;
                        //sForm.Show();
                        //System.Threading.Thread.Sleep(500);
                        //sForm.Close();
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

        private System.Data.DataSet FillDataInDataset(Int16 srno, string Barcode)
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
            TempTable1.Columns.Add("ID", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("BARCODE", System.Type.GetType("System.Byte[]"));
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
                    MyRow["AMTINWORDS"] = cf.getNumbersInWords(txtTotalAmt.Value, eCurrencyType.Rupees);
                    // ''MyRow("GUEST") = chkGuestYes.Checked
                    // ''MyRow("CASH") = nudCash.Value
                    // ''MyRow("CHANGE") = nudChange.Value
                    // 'code for print barcode


                    // comment
                    // Dim _tempByte() As Byte = Nothing
                    // Dim NewBarcode As IDAutomation.Windows.Forms.LinearBarCode.Barcode = New Barcode()
                    // Dim imageName As String = "img" & txtVchNo.Text & Date.Now().Millisecond & ".Jpeg"

                    // ' NewBarcode.DataToEncode = txtName.Text.Substring(0, 4) & txtVchNo.Text 'Input of textbox to generate barcode 
                    // NewBarcode.DataToEncode = Barcode 'Input of textbox to generate barcode 
                    // NewBarcode.SymbologyID = Symbologies.Code39
                    // NewBarcode.Code128Set = Code128CharacterSets.A
                    // NewBarcode.RotationAngle = RotationAngles.Zero_Degrees
                    // NewBarcode.RefreshImage()
                    // NewBarcode.Resolution = Resolutions.Screen
                    // NewBarcode.ResolutionCustomDPI = 96
                    // NewBarcode.RefreshImage()
                    // NewBarcode.ShowText = False

                    // NewBarcode.SaveImageAs(System.Configuration.ConfigurationSettings.AppSettings.Get("BARCODE_PATH") & "\" & imageName, System.Drawing.Imaging.ImageFormat.Jpeg)
                    // NewBarcode.Resolution = Resolutions.Printer

                    // 'Image(img = Image.FromFile(Application.StartupPath & "\" & "SavedBarcode.Jpeg"))


                    // Dim _fileInfo As New IO.FileInfo(Application.StartupPath & "\" & "Barcode1.Jpeg")

                    // Commented By Roshan For Stop Savaing the Barcode image 
                    // Dim _fileInfo As New IO.FileInfo(System.Configuration.ConfigurationSettings.AppSettings.Get("BARCODE_PATH") & "\" & imageName)
                    // Dim FileName As String = System.Configuration.ConfigurationSettings.AppSettings.Get("BARCODE_PATH") & "\" & imageName
                    // Dim _NumBytes As Long = _fileInfo.Length
                    // Dim _FStream As New IO.FileStream(System.Configuration.ConfigurationSettings.AppSettings.Get("BARCODE_PATH") & "\" & imageName, IO.FileMode.Open, IO.FileAccess.Read)
                    // Dim _BinaryReader As New IO.BinaryReader(_FStream)
                    // _tempByte = _BinaryReader.ReadBytes(Convert.ToInt32(_NumBytes))
                    // _fileInfo = Nothing
                    // _NumBytes = 0
                    // _FStream.Close()
                    // _FStream.Dispose()
                    // _BinaryReader.Close()
                    // MyRow("BARCODE") = _tempByte


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
            }
            catch (Exception ex)
            {
                MessageBox.Show("FillDataInDataset : " + ex.ToString(), PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                ds = null/* TODO Change to default(_) if this is not a reference type */;
            }
            return ds;
        }


        private void fpsPrintReceipt_Enter(object sender, System.EventArgs e)
        {
            var withBlock = fpsPrintReceipt;
            fpsPrintReceipt.Rows[withBlock.CurrentCell.RowIndex].Cells[withBlock.CurrentCell.ColumnIndex].Style.BackColor = Color.White;
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
            int ctr1 = withBlock.CurrentCell.RowIndex;
            long lngItemId;
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
            // If fpsPrintReceipt.Sheets(0).ActiveColumnIndex <> ePrintReceipt.MenuM Then
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
            // fpsPrintReceipt.Focus()
            // With fpsPrintReceipt.Sheets(0)
            ReSetTextBoxBackAndForeColor(t1);

            //fpsPrintReceipt.ActiveSheet.SetActiveCell(1, ePrintReceipt.ProductN);
            fpsPrintReceipt.CurrentCell = fpsPrintReceipt.Rows[1].Cells[(int)ePrintReceipt.ProductN];
            // chkGuestNo.Checked = True
            fpsPrintReceipt.Focus();
            // fpsPrintReceipt.Sheets(0).Cells(0, ePrintReceipt.MenuM).BackColor = Color.Cyan
            // fpsPrintReceipt.Sheets(0).SetActiveCell(0, ePrintReceipt.MenuM)
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

            // 
            // Open Preview window in picturebox
            // 
            hHwnd = capCreateCaptureWindowA(iDevice.ToString(), WS_VISIBLE | WS_CHILD, 0, 0, 640, 480, imgVideo.Handle.ToInt32(), 0);

            // 
            // Connect to device
            // 
            if (SendMessage(hHwnd, WM_CAP_DRIVER_CONNECT, iDevice, 0) == 1)
            {
                // 
                // Set the preview scale
                // 
                SendMessage(hHwnd, WM_CAP_SET_SCALE, 1, 0);

                // 
                // Set the preview rate in milliseconds
                // 
                SendMessage(hHwnd, WM_CAP_SET_PREVIEWRATE, 66, 0);

                // 
                // Start previewing the image from the camera
                // 
                SendMessage(hHwnd, WM_CAP_SET_PREVIEW, 1, 0);

                // 
                // Resize window to fit in picturebox
                // 
                SetWindowPos(hHwnd, HWND_BOTTOM, 0, 0, imgVideo.Width, imgVideo.Height, SWP_NOMOVE | SWP_NOZORDER);
            }
            else
                // 
                // Error connecting to device close window
                // 
                DestroyWindow(hHwnd);
        }


        private void OpenPreviewWindow2()
        {
            int iHeight = imgVideo_1.Height;
            int iWidth = imgVideo_1.Width;

            IDataObject data;
            System.Drawing.Image bmap;

            string strFileName;
            // 
            // Open Preview window in picturebox
            // 
            hHwnd = capCreateCaptureWindowA(iDevice.ToString(), WS_VISIBLE | WS_CHILD, 0, 0, 640, 480, imgVideo_1.Handle.ToInt32(), 0);

            // 
            // Connect to device
            // 
            if (SendMessage(hHwnd, WM_CAP_DRIVER_CONNECT, iDevice, 0) == 1)
            {
                // 
                // Set the preview scale
                // 
                SendMessage(hHwnd, WM_CAP_SET_SCALE, 1, 0);

                // 
                // Set the preview rate in milliseconds
                // 
                SendMessage(hHwnd, WM_CAP_SET_PREVIEWRATE, 50, 0);

                // 
                // Start previewing the image from the camera
                // 
                SendMessage(hHwnd, WM_CAP_SET_PREVIEW, 1, 0);

                // 
                // Resize window to fit in picturebox
                // 
                SetWindowPos(hHwnd, HWND_BOTTOM, 0, 0, imgVideo_1.Width, imgVideo_1.Height, SWP_NOMOVE | SWP_NOZORDER);


                if (SendMessage(hHwnd, WM_CAP_DRIVER_CONNECT, iDevice, 0) == 1)
                    SendMessage(hHwnd, WM_CAP_EDIT_COPY, 0, 0);
            }
            else
            {
                // 
                // Error connecting to device close window
                // 
                DestroyWindow(hHwnd);

                btnSave.Enabled = false;
            }
        }

        private void ClosePreviewWindow()
        {
            // 
            // Disconnect from device
            // 
            SendMessage(hHwnd, WM_CAP_DRIVER_DISCONNECT, iDevice, 0);

            // 
            // close window
            // 

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

                    // 
                    // Get image from clipboard and convert it to a bitmap
                    // 
                    data = Clipboard.GetDataObject();

                    if (data.GetDataPresent(typeof(System.Drawing.Bitmap)))
                    {
                        bmap = (System.Drawing.Image)data.GetData(typeof(System.Drawing.Bitmap));
                        imgVideo_1.Image = bmap;
                        ClosePreviewWindow();
                        // If sfdImage.ShowDialog = DialogResult.OK Then
                        // bmap.Save("d:\a.png", Imaging.ImageFormat.Png)
                        txtImagePath.Text = (txtName.Text) + System.DateTime.Now.ToString("dd/MM/yyyy");
                        strFileName = ObjImageFilePath.Final_SaveImageCapture(bmap, txtImagePath.Text);
                        txtImagePath.Text = strFileName;
                        // End If
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
            // Dim BedCheckInMst As New OSOL_CONNECTION.BedCheckInMst
            string F1 = System.Configuration.ConfigurationManager.AppSettings.Get("BedScannerPath");
            string s;
            DialogResult answer;


            try
            {
                // If ((mydevice) Is Nothing) Then
                // MsgBox("Scanner is not attached.")
                // Return Nothing
                // Exit Function
                // End If

                // mydevice = CommonDialogBox.ShowSelectDevice(WIA.WiaDeviceType.UnspecifiedDeviceType, True, False)
                // F = CommonDialogBox.ShowAcquireImage(WIA.WiaDeviceType.CameraDeviceType, WIA.WiaImageIntent.ColorIntent, WIA.WiaImageBias.MinimizeSize, , True, True)
                WIA.DeviceManager DeviceManager1 = new WIA.DeviceManager();//= Interaction.CreateObject("WIA.DeviceManager");
                int i = 0;
                // For i = 1 To DeviceManager1.DeviceInfos.Count
                mydevice = CommonDialogBox.ShowSelectDevice(WIA.WiaDeviceType.ScannerDeviceType, true, false);

                if (DeviceManager1.DeviceInfos[1].Type == WIA.WiaDeviceType.ScannerDeviceType)
                {
                    WIA.Device Scanner = DeviceManager1.DeviceInfos[1].Connect();
                    if (Information.IsNothing(Scanner))
                    {
                        Interaction.MsgBox("Could not connect to scanner please check attached Properly.");
                        // Return Nothing
                        return;
                    }
                    else
                        try
                        {
                            // s = txtName.Text & txtAppNo.Text
                            s = txtName.Text + txtVchNo.Text;
                            // F.SaveFile(F1)
                            // F = Scanner.Items(1).Transfer()
                            // F = CommonDialogBox.ShowAcquireImage(WIA.WiaDeviceType.ScannerDeviceType, WIA.WiaImageIntent.ColorIntent, WIA.WiaImageBias.MinimizeSize, , False, False, True)
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
                                    // Return Nothing
                                    return;
                            }
                            val1 = s + "." + F.FileExtension;
                            F1 = F1 + val1;
                            txtScanDoc.Text = s;
                            // Collection.ScanDoc = s
                            //var filesystemobject = Interaction.CreateObject("Scripting.FileSystemObject");
                            if (File.Exists(F1))
                                FileSystem.Kill(F1);
                            F.SaveFile(F1);
                            txtScan.Text = s;
                        }
                        // Dim obj As OSOL_BLSDS.clsBlsRoomCheckIn = New OSOL_BLSDS.clsBlsRoomCheckIn()
                        // i = obj.UpdateScan(CheckInMst.ScanDoc, txtAppNo.Text)
                        // If i = -1 Then
                        // MsgBox("Document Scan successfully")
                        // Else
                        // MsgBox("Document not Scan successfully")
                        // End If
                        catch (Exception ex)
                        {
                            Interaction.MsgBox(ex.Message);
                        }
                        // Return Collection.ScanDoc = "No Images scan"
                        finally
                        {
                            Scanner = null/* TODO Change to default(_) if this is not a reference type */;
                        }
                }
                else
                    Interaction.MsgBox("Scanner is not attached checked it");

                imgVideo.ImageLocation = F1;
                // Return Collection.ScanDoc

                // Next
                DeviceManager1 = null;
            }

            catch (Exception ex)
            {
                answer = MessageBox.Show("There is no file in scanner or scanner not attached do you want to scan blank image", "Yes/no sample", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer == DialogResult.Yes)
                    // Return Nothing
                    return;
            }
        }

    }
}
