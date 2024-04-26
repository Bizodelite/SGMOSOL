using CrystalDecisions.ReportAppServer;
using Microsoft.VisualBasic;
using SGMOSOL.ADMIN;
using SGMOSOL.SCREENS.BedSystem;
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
using SGMOSOL.DAL.BhaktNiwas;
using static SGMOSOL.BAL.BhaktNiwasBAL;
using SGMOSOL.BAL;
using IDAutomation.Windows.Forms.LinearBarCode;
//using IDAutomation.Windows.Forms.LinearBarCode;


namespace SGMOSOL.SCREENS.BhaktNiwas
{
    public partial class frmRoomCheckIn : Form
    {
        private int Break = 0;
        private bool mBlnEdit = false;
        private eAction mAction;        // Reference to Enum of Type Action for ActionView,ActionNew,ActionUpdate and ActionDelete
        private ArrayList CtrlArr = new ArrayList(); // To insert Form Control in Array List
        private ArrayList btnArr = new ArrayList();
        //private OSOL_ADMIN.clsDsCommon cf = new OSOL_ADMIN.clsDsCommon();
        //private OSOL_BLSDS.clsDsGamEntryGate objDsEntryGateDet = new OSOL_BLSDS.clsDsGamEntryGate();
        //private OSOL_BLSDS.clsBlsRoomCheckIn objBlsRoomCheckIn = new OSOL_BLSDS.clsBlsRoomCheckIn();
        private RoomCheckInDAL RoomCheckInDALobj = new RoomCheckInDAL();
        //private OSOL_BLSDS.clsDsBNRoomCheckInDet objDsRoomCheckInDet = new OSOL_BLSDS.clsDsBNRoomCheckInDet();
        private RoomMasterDAL objDsRoomMst = new RoomMasterDAL();
        private bool DisableSendKeys;
        private int srchFlag = 0;
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
        private double Advance;
        private double rent;
        private int DnrAllowedDays;
        private int flag1;
        private string mStrCounterMachineShortName;
        private int RoomCheckInDeptID;
        private int RoomCheckInLockID;
        private Int64 sublocid;
        private string RoomCheckInDeptName;
        private string RoomCheckInLocName;
        private int rbFlag = 0;
        private int SaveFlag = 0;
        const short WM_CAP = 0x400;
        private int RoomSubloId = 0;
        public string BarcodeRet = "";
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

        private System.Data.DataTable dsCountry;
        private System.Data.DataSet dsState;
        private System.Data.DataSet dsDistrict;
        private bool Eprint;

        CommonFunctions cf = new CommonFunctions();
        public frmRoomCheckIn()
        {
            InitializeComponent();
            this.Closing += new CancelEventHandler(this.frmRoomCheckIn_Closing);
        }

        private void btnNew_Click(System.Object sender, System.EventArgs e)
        {
            srchFlag = 0;
            DataTable dr;
            FormClear();
            cf.fncSetDateAndRange(dtpCheckIn);
            cf.fncSysTime(dtpCheckInTime);
            cf.fncSysTime(dtpCheckOutTime);
            // fncSysTimePlus2(dtpCheckOutTime)
            // fncSysDateTimePlus2(dtpCheckOut, dtpCheckOutTime)
            dtpCheckOut.Value = (dtpCheckIn.Value.AddDays(Convert.ToDouble(txtDays.Text)));

            // dtpCheckOutTime.Value = dtpCheckInTime.Value
            // dtpCheckOutTime.Value = dtpCheckInTime.Value
            dtpCheckIn.Enabled = bkDateEntry;
            mAction = eAction.ActionInsert;
            // subLockForm(False, CtrlArr, False)
            btnSave.Enabled = btnNew.Enabled;
            try
            {
                dr = RoomCheckInDALobj.GetMaxSerialNo(UserInfo.CompanyID, RoomCheckInLockID, 0, UserInfo.fy_id);
                if (dr.Rows.Count > 0)
                    txtVchNo.Text = (Convert.ToInt64(dr.Rows[0]["SerialNo"]) + 1).ToString();
                else
                    txtVchNo.Text = "1";
            }
            catch (Exception ex)
            {

            }
            // FillAvailableRooms()
            FillDonners(4, RoomCheckInLockID);
            FillDonners(5, RoomCheckInLockID);
            FillAuthPersons();
            FillSublocations();
            chkRooms.Enabled = true;
            // txtAppNo.Focus()
            blnformChange = false;
            if (mAction == eAction.ActionInsert)
                btnPrint.Enabled = false;
            else
                btnPrint.Enabled = Convert.ToBoolean(Interaction.IIf(Eprint, true, false));
        }

        private void FillAvailableRooms()
        {
            flag1 = 0;
            var str = txtRoomSrch.Text;
            DataTable dr;
            try
            {
                if (cboSublocation.SelectedIndex < 0)
                    sublocid = 0;
                else
                    sublocid = cf.cmbItemdata(cboSublocation, cboSublocation.SelectedIndex);

                dr = objDsRoomMst.GetDrRoomDetails1(str, sublocid, (int)eTokenDetail.StatusYes, RoomCheckInLockID);
                cf.FillListBox(chkRooms, dr, "RoomName", "RoomId", "RecordModifiedCount");
            }
            catch (Exception ex)
            {

            }
            txtLkrAvlbleCt.Text = chkRooms.Items.Count.ToString();
        }

        private void FillAuthPersons()
        {
            DataTable dr;
            try
            {
                dr = objDsRoomMst.GetDrAuthPersons();
                cf.FillCombo(cboAuthPer, dr, "Name", "id");
            }
            catch (Exception ex)
            {
            }
        }

        private void FillSublocations()
        {
            DataTable dr;
            try
            {
                dr = objDsRoomMst.GetDrSublocations(RoomCheckInLockID, (int)eModType.BhaktaNiwas);
                cf.FillCombo(cboSublocation, dr, "Name", "DeptId");
            }
            catch (Exception ex)
            {
            }
        }

        private void FillDonners(int bhaktid, int LocId)
        {
            // Dim dr As SqlClient.SqlDataReader
            // Try
            // dr = objDsRoomMst.GetDrDonners(bhaktid)
            // FillCombo(cboDonner, dr, "Name", "DonnerId")
            // Catch ex As Exception
            // If Not dr Is Nothing Then dr.Close()
            // End Try

            DataTable dataTable = new DataTable("Donners");
            System.Data.DataSet ds;


            if (bhaktid == 4)
            {
                ds = objDsRoomMst.GetDonners(bhaktid, LocId);
                MultiColumnComboBox1.DataSource = ds.Tables[0];
                MultiColumnComboBox1.DisplayMember = "Donner Id";
                MultiColumnComboBox1.ValueMember = "ROOM_NAME";
            }
            else
            {
                ds = objDsRoomMst.GetAnnadan(bhaktid, LocId);
                MultiColumnComboBox2.DataSource = ds.Tables[0];
                MultiColumnComboBox2.DisplayMember = "Donner Id";
                MultiColumnComboBox2.ValueMember = "Name";
            }
        }

        private void frmRoomCheckIn_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter & !DisableSendKeys)
                SendKeys.Send("{tab}");
            if (e.KeyCode == Keys.End & (mAction == eAction.ActionInsert | mAction == eAction.ActionUpdate) & blnformChange)
                btnSave_Click(null, null);

            if (e.KeyCode == Keys.F1)
            {
                DataTable dr;
                dr = RoomCheckInDALobj.GetLastEnterdName(Convert.ToInt32(txtCounter.Tag));
                if (dr.Rows.Count > 0)
                {
                    txtName.Text = dr.Rows[0]["LastEnteredName"].ToString();
                    txtPlace.Text = dr.Rows[0]["PLACE"].ToString();
                }
            }
        }


        private void frmRoomCheckIn_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End & (mAction == eAction.ActionInsert | mAction == eAction.ActionUpdate) & blnformChange)
                btnSave_Click(null, null);
        }

        private void frmRoomCheckIn_Load(System.Object sender, System.EventArgs e)
        {
            // Dim prnter = System.Configuration.ConfigurationSettings.AppSettings.Get("Printer_name")
            // myPrinters.SetDefaultPrinter(prnter)
            // Me.WindowState = FormWindowState.Maximized
            System.Data.DataSet dsPaymentType1;
            System.Data.DataSet dsTID;
            txtDays.Text = "1";
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
            OpenPreviewWindow();
            CreateDs();

            fillCountry();
            dsState = cf.GetState(102);
            int[] ptype = new int[] { (int)eTokenDetail.Swap, (int)eTokenDetail.Cash, (int)eTokenDetail.Cheque };
            dsPaymentType1 = cf.GetDsPaymentType2(ptype);
            cf.FillComboWithDataSet(cboPaymentType, dsPaymentType1.Tables[0], "Token_Detail_Name", "Token_Detail_Name", "Token_Detail_Id");
            dsTID = cf.GetDsTID(Convert.ToInt32(txtCounter.Tag));
            cf.FillComboWithDataSet(cobTid, dsTID.Tables[0], "tidNo", "tidNo", "Tid");

            if (btnNew.Enabled)
                btnNew_Click(null, null);
            else
                cf.subLockForm(true, CtrlArr, false);
            pnlChqDtl.Visible = false;

            try
            {
                DataTable rstAction = cf.GetUserTotalAction(UserInfo.UserId, (Int64)eScreenID.RoomCheckIn);
                string strAction = string.Empty;
                foreach (DataRow item in rstAction.Rows)
                {
                    strAction = Interaction.IIf(strAction == string.Empty, item["ActionId"], strAction + "," + item["ActionId"]).ToString();
                }
                if (strAction.Contains("8"))
                    Eprint = true;
                else
                    Eprint = false;
            }
            catch (Exception ex)
            {
            }
            btnPrint.Enabled = false;
        }

        private void fillCountry()
        {
            dsCountry = cf.getCountry();
            cf.FillComboWithDataSet(cboCountry, dsCountry, "CountryID", "CountryName", "CountryID");
        }

        private void FillCounter()
        {
            DataTable dr;
            dr = cf.GetDrCounterMachId(UserInfo.UserId, SystemHDDModelNo, SystemHDDSerialNo, SystemMacID, Convert.ToInt16(eModType.BhaktaNiwas));
            if (dr.Rows.Count > 0)
            {
                txtCounter.Text = dr.Rows[0]["CounterMachineTitle"].ToString();
                txtCounter.Tag = dr.Rows[0]["CtrMachId"];
                RoomCheckInDeptID = Convert.ToInt32(dr.Rows[0]["DeptId"]);
                RoomCheckInDeptName = dr.Rows[0]["DepartmentName"].ToString();
                RoomCheckInLocName = dr.Rows[0]["LOC_FNAME"].ToString();
                RoomCheckInLockID = Convert.ToInt32(dr.Rows[0]["LocId"]);
                mStrCounterMachineShortName = dr.Rows[0]["CounterMachineShortName"].ToString();
            }
        }



        private void FormClear()
        {
            txtScan.Clear();
            txtVchNo.Text = "";
            txtVchNo.Tag = null;
            txtAppNo.Text = "";
            txtName.Text = "";
            txtPlace.Text = "";
            txtDays.Text = "1";
            txtmobno.Text = "";
            txtNoOfRooms.Text = "";
            nudAdvance.Value = 0;
            nudRent.Value = 0;
            txtNoOfPersons.Text = "";
            txtRemark.Text = "";
            txtVehcleNo.Text = "";
            chkRooms.Items.Clear();
            SaveFlag = 0;
            flag1 = 0;
            rbBhakt.Checked = true;
            rbBhakt.PerformClick();
            cboSublocation.SelectedIndex = -1;
            cboPaymentType.SelectedIndex = 0;
            cobTid.SelectedIndex = -1;
            txtInvoice.Text = "";
            imgVideo.ImageLocation = "";
            lblimg.Visible = false;
            cboCountry.SelectedIndex = cboCountry.FindStringExact("INDIA");
            dsState = cf.GetState(Convert.ToInt32(cf.cmbItemdata(cboCountry, cboCountry.SelectedIndex)));
            cf.FillComboWithDataSet(cboState, dsState.Tables[0], "StateId", "StateName", "StateId");
            cboState.SelectedIndex = cboState.FindStringExact("MAHARASHTRA");
            dsDistrict = cf.GetDistrict(Convert.ToInt32(cf.cmbItemdata(cboState, cboState.SelectedIndex)));
            cf.FillComboWithDataSet(cboDistrict, dsDistrict.Tables[0], "DistrictId", "DistrictName", "DistrictId");
            txtChqBankname.Text = "";
            txtChqNo.Text = "";
            dtChqDt.Text = "";
            txtKYCDocdetails.Text = "";
            cmbKYCDoc.SelectedIndex = -1;
        }

        private void btnSave_Click(System.Object sender, System.EventArgs e)
        {
            srchFlag = 0;
            long lngError = -1;

            if (blnformChange == false)
                return;
            if (fncSave())
            {
                OpenPreviewWindow();
                blnformChange = false;
                btnNew_Click(null, null);
                blnformChange = false;
            }
        }

        private bool fncSave()
        {
            IDataObject data;
            Image bmap;
            string a;

            if (SendMessage(hHwnd, WM_CAP_DRIVER_CONNECT, iDevice, 0) == 1)
            {
                SendMessage(hHwnd, WM_CAP_EDIT_COPY, 0, 0);
                // 
                // Get image from clipboard and convert it to a bitmap
                // 
                data = Clipboard.GetDataObject();
                if (data.GetDataPresent(typeof(System.Drawing.Bitmap)))
                {
                    bmap = (Image)data.GetData(typeof(System.Drawing.Bitmap));
                    imgVideo.Image = bmap;
                    ClosePreviewWindow();
                    // If sfdImage.ShowDialog = DialogResult.OK Then
                    // bmap.Save("d:\a.png", Imaging.ImageFormat.Png)
                    Helper.SaveImageCapture(bmap, "Person-Img");
                }
            }
            long lngError = -1;
            RoomCheckInMst RoomCheckInMst;
            Collection coll;
            long lngSerialNo;
            string strErr;
            bool flag = true;
            int MaxDays = 0;
            int MaxRooms = 0;
            if (IsValidForm() == false)
            {
                return false;
            }

            setCursor(this, false);
            lngSerialNo = Convert.ToInt64(txtVchNo.Text);
            cf.fncSetDateAndRange(dtpCheckIn);
            cf.fncSysTime(dtpCheckInTime);


            RoomCheckInMst = GetCheckInMst();
            DataTable dr;
            dr = RoomCheckInDALobj.GetDaysLimit();
            if (dr.Rows.Count > 0)
                MaxDays = Convert.ToInt32(dr.Rows[0]["EXPIRY_DAYS"]);
            if (RoomCheckInMst.Days > MaxDays | RoomCheckInMst.Days == 0)
            {
                MessageBox.Show("Days Are Not Acceptable!", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDays.Text = "";
                setCursor(this, true);
                return false;
            }

            // added by Roshan/payal
            if (RoomCheckInMst.BhaktTypeId == 4 | RoomCheckInMst.BhaktTypeId == 5)
            {
                if ((DnrAllowedDays == 0))
                {
                    MessageBox.Show("Allocated Room for Donner crossed for max limit. You can not allocate room under Donner..!", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDays.Text = DnrAllowedDays.ToString();
                    setCursor(this, true);
                    return false;
                }
                if ((RoomCheckInMst.Days > DnrAllowedDays))
                {
                    MessageBox.Show("Donner has only remaning " + DnrAllowedDays.ToString() + " days", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDays.Text = DnrAllowedDays.ToString();
                    setCursor(this, true);
                    return false;
                }
            }
            dr = RoomCheckInDALobj.GetRoomLimit();
            if (dr.Rows.Count > 0)
                MaxRooms = Convert.ToInt32(dr.Rows[0]["MAX_ROOMS"]);

            if (RoomCheckInMst.NoOfRooms > MaxRooms)
            {
                MessageBox.Show("No.of Rooms are Exceeding!", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNoOfRooms.Text = "";
                for (int i = 0; i <= chkRooms.Items.Count - 1; i++)
                {
                    if (chkRooms.GetSelected(i))
                        chkRooms.SetItemChecked(i, false);
                }
                return false;
            }
            if (mAction == eAction.ActionInsert)
                coll = GetCheckInDetColl(true);
            else
                coll = GetCheckInDetColl(false);
            if (flag1 == 1)
            {
                FillAvailableRooms();
                return false;
            }
            if (SaveFlag == 1)
            {
                for (var i = 0; i <= chkRooms.Items.Count - 1; i++)
                {
                    if (chkRooms.GetSelected(i))
                        chkRooms.SetItemChecked(i, false);
                }
                return false;
            }

            if (mAction == eAction.ActionInsert)
            {
                lngError = RoomCheckInDALobj.Insert(RoomCheckInMst, coll, UserInfo.UserName, UserInfo.Machine_Name, lngSerialNo, dtEnteredOn);
                if (lngError > 0)
                    // dr = objDsRoomCheckInMst.GetDrRoomCheckInMstId()
                    // If dr.Read Then
                    // lngSerialNo = Val(dr("SerialNo") & vbNullString)
                    // End If
                    // dr.Close()
                    txtVchNo.Text = lngError.ToString();
            }
            else if (mAction == eAction.ActionUpdate)
                lngError = RoomCheckInDALobj.Update(RoomCheckInMst, coll, mDelColl, UserInfo.UserName, UserInfo.Machine_Name, lngSerialNo);
            dgScanPrievew dlgSP = new dgScanPrievew();
            dlgSP.ImgPath = System.Configuration.ConfigurationManager.AppSettings.Get("ScannerPath") + Interaction.IIf(Information.IsDBNull(RoomCheckInMst.Image), RoomCheckInMst.ScanDoc + ".bmp", RoomCheckInMst.Image);
            dlgSP.ShowDialog();
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
                if (mAction == eAction.ActionInsert)
                {
                    blnformChange = false;
                    MethodForPrint();
                }
                else
                    btnPrint_Click(null, null);
            }
            setCursor(this, true);
            return flag;
        }

        private void MethodForPrint()
        {
            string strReportName;
            Form sForm;
            Collection pColl = new Collection();
            setCursor(this, false);
            System.Drawing.Printing.PrintDocument printDoc = new System.Drawing.Printing.PrintDocument();
            string printD1;
            FillDataInDataset(txtmobno.Tag.ToString());

            strReportName = "RoomCheckInNew.rdlc";
            sForm = new frmCrystalViewer(UserInfo.ReportPath + strReportName, null, ds, null, pColl, (long)eReportID.RoomCheckIn1, true);
            sForm.Text = "Room Check IN : " + eReportID.RoomCheckOut;
            sForm.Show();
            System.Threading.Thread.Sleep(450);
            sForm.Close();

            setCursor(this, true);
        }
        public static string val1 = "";
        public string GenerateRandomString(int iLength)
        {
            Random rdm = new Random();
            char[] allowChrs = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLOMNOPQRSTUVWXYZ0123456789".ToCharArray();
            string sResult = "";
            for (int i = 0; i <= iLength - 1; i++)
                sResult += allowChrs[rdm.Next(0, allowChrs.Length)];

            return sResult;
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
            CheckInMst.ScanDoc = Scan(CheckInMst);


            if (cboDistrict.SelectedIndex != -1)
            {
                CheckInMst.DISTRICT = cboDistrict.Text;
                CheckInMst.Districtid = cf.cmbItemdata(cboDistrict, cboDistrict.SelectedIndex);
            }
            if (cboState.SelectedIndex != -1)
            {
                CheckInMst.statename = cboState.Text;
                CheckInMst.Stateid = cf.cmbItemdata(cboState, cboState.SelectedIndex);
            }
            CheckInMst.CountryId = cf.cmbItemdata(cboCountry, cboCountry.SelectedIndex);
            CheckInMst.Countryname = cboCountry.Text;

            CheckInMst.Sublocid = cf.cmbItemdata(cboSublocation, cboSublocation.SelectedIndex);
            CheckInMst.sublocn = cboSublocation.Text;
            CheckInMst.InDate = dtpCheckIn.Value;
            CheckInMst.InTime = dtpCheckInTime.Value;
            CheckInMst.SerialNo = Convert.ToInt64(txtVchNo.Tag);
            CheckInMst.tid = cf.cmbItemdata(cobTid, cobTid.SelectedIndex);
            CheckInMst.invoiceno = txtInvoice.Text;
            CheckInMst.paymenttype = cf.cmbItemdata(cboPaymentType, cboPaymentType.SelectedIndex);
            if (CheckInMst.paymenttype == (Int64)eTokenDetail.Cheque)
                CheckInMst.CHQ_DATE = dtChqDt.Value;
            else
                CheckInMst.CHQ_DATE = null;
            CheckInMst.CHQ_BANK_NAME = txtChqBankname.Text;
            CheckInMst.CHQ_NO = txtChqNo.Text;
            CheckInMst.Barcode = GenerateRandomString(4) + txtVchNo.Text;
            txtmobno.Tag = CheckInMst.Barcode;
            if (rbFlag == 2)
            {
                CheckInMst.donerId = Convert.ToInt32(((DataRowView)MultiColumnComboBox1.SelectedItem).Row.ItemArray[6]);
                CheckInMst.DonnerRoomId = Convert.ToInt32(((System.Data.DataRowView)MultiColumnComboBox1.SelectedItem).Row.ItemArray[5]);
            }
            else if (rbFlag == 3)
                CheckInMst.donerId = Convert.ToInt32(((System.Data.DataRowView)MultiColumnComboBox2.SelectedItem).Row.ItemArray[4]);
            CheckInMst.Name = txtName.Text;


            if ((Helper.final == ""))
                CheckInMst.Image = val1.Replace(@"\", "");
            else
                CheckInMst.Image = Helper.final;

            CheckInMst.AppNo = Convert.ToInt32(txtAppNo.Text);
            CheckInMst.Place = txtPlace.Text;
            CheckInMst.VehicleNo = txtVehcleNo.Text;
            CheckInMst.mob_no = txtmobno.Text;
            CheckInMst.Days = Convert.ToInt32(txtDays.Text);
            CheckInMst.NoOfRooms = Convert.ToInt32(txtNoOfRooms.Text);
            CheckInMst.NoOfPersons = Convert.ToInt32(txtNoOfPersons.Text);
            CheckInMst.OutDate = dtpCheckOut.Value;
            CheckInMst.OutTime = dtpCheckOutTime.Value;
            CheckInMst.Advance = nudAdvance.Value;
            CheckInMst.Rent = nudRent.Value;
            CheckInMst.Remark = txtRemark.Text;
            CheckInMst.UserId = UserInfo.UserId;
            CheckInMst.MachineName = UserInfo.Machine_Name;
            if (cboAuthPer.Visible == true)
            {
                if (cboAuthPer.SelectedIndex != -1)
                    CheckInMst.AuthPersonId = Convert.ToInt32(cboAuthPer.SelectedValue);
            }

            // code added 26-07-2023
            // code added for KYC Document
            CheckInMst.DOC_TYPE = cmbKYCDoc.Text;
            CheckInMst.DOC_DETAILS = txtKYCDocdetails.Text;

            if (rbBhakt.Checked)
                CheckInMst.BhaktTypeId = 1;
            else if (rbGuest1.Checked)
                CheckInMst.BhaktTypeId = 2;
            else if (rbGuest2.Checked)
                CheckInMst.BhaktTypeId = 3;
            else if (rbDoner.Checked)
                CheckInMst.BhaktTypeId = 4;
            else
                CheckInMst.BhaktTypeId = 5;
            CheckInMst.ServerName = UserInfo.serverName;
            CheckInMst.EnteredBy = UserInfo.UserName;
            CheckInMst.ModifiedBy = UserInfo.UserName;
            CheckInMst.RecordModifiedCount = Convert.ToInt64(dtpCheckIn.Tag) + 1;
            return CheckInMst;
        }


        private Collection GetCheckInDetColl(bool Flag)
        {
            int avstatus;
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
                    DataTable dr;
                    try
                    {
                        coll.Add(CheckInDet);
                        if (Flag == true)
                        {
                            dr = objDsRoomMst.checkRoomAvailability(CheckInDet.LockerId);
                            if (dr.Rows.Count > 0)
                            {
                                avstatus = Convert.ToInt32(dr.Rows[0]["AVAILABLE_STATUS"]);
                                if (avstatus == 178)
                                {
                                    flag1 = 1;
                                    MessageBox.Show("Room is Alrady Given!", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }

                        if (flag1 == 1)
                            break;
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            return coll;
        }

        private bool IsValidForm()
        {
            if (txtName.Text == "")
            {
                mStrErrMsg = new string[1];
                mStrErrMsg[0] = "Name";
                return ShowValidateError(txtName, 0, mStrErrMsg, 1);
            }

            if (txtAppNo.Text == "")
            {
                mStrErrMsg = new string[1];
                mStrErrMsg[0] = "App.No.";
                return ShowValidateError(txtAppNo, 0, mStrErrMsg, 1);
            }

            if (cboSublocation.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Sublocation.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboSublocation.Focus();
                return false;
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

            if (txtNoOfRooms.Text == "")
            {
                mStrErrMsg = new string[1];
                mStrErrMsg[0] = "No. of Rooms";
                return ShowValidateError(txtNoOfRooms, 0, mStrErrMsg, 1);
            }

            if (txtNoOfPersons.Text == "")
            {
                mStrErrMsg = new string[1];
                mStrErrMsg[0] = "No. of Persons";
                return ShowValidateError(txtNoOfPersons, 0, mStrErrMsg, 1);
            }

            if (txtNoOfPersons.Text != "")
            {
                System.Text.RegularExpressions.Regex No = new System.Text.RegularExpressions.Regex("^[0-9]");
                if (No.IsMatch(txtNoOfPersons.Text.Trim()) == false)
                {
                    MessageBox.Show("Enter Numeric Value");
                    txtNoOfPersons.Focus();
                    return false;
                }
            }

            if (chkRooms.CheckedItems.Count == 0)
            {
                mStrErrMsg = new string[1];
                mStrErrMsg[0] = "Rooms";
                return ShowValidateError(txtNoOfRooms, 0, mStrErrMsg, 37);
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

            if (Convert.ToInt32(txtNoOfRooms.Text) != chkRooms.CheckedItems.Count)
            {
                mStrErrMsg = new string[1];
                mStrErrMsg[0] = "Rooms";
                MessageBox.Show("No. of Rooms specified doesn't match with Room Selected.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                chkRooms.Focus();
                return false;
            }
            if (cboPaymentType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select payment type from the list.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboPaymentType.Focus();
                return false;
            }
            Int64 PaymentType;
            PaymentType = cf.cmbItemdata(cboPaymentType, cboPaymentType.SelectedIndex);
            if (PaymentType == (Int64)eTokenDetail.Swap)
            {
                if (txtInvoice.Text == "")
                {
                    MessageBox.Show("Please enter Invoice No.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtInvoice.Focus();
                    return false;
                }
                if (cobTid.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select TID from the list.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cobTid.Focus();
                    return false;
                }
            }
            else if (PaymentType == (Int64)eTokenDetail.Cheque)
            {
                if (txtChqBankname.Text == "")
                {
                    MessageBox.Show("Please enter bank name.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtChqBankname.Focus();
                    return false;
                }
                if (txtChqNo.Text == "")
                {
                    MessageBox.Show("Please enter cheque number.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtChqNo.Focus();
                    return false;
                }
                if (dtChqDt.Text == "")
                {
                    MessageBox.Show("Please enter cheque date.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtChqBankname.Focus();
                    return false;
                }
            }
            if (cboCountry.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Country.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboCountry.Focus();
                return false;
            }

            if (nudRent.Value > UserInfo.RoomCheckInMaxAmount)
            {
                MessageBox.Show("Please Check The Total Dengi Amount.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTotalAmt.Focus();
                return false;
            }

            if (cboCountry.Text == "INDIA")
            {
                if (cboState.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select State.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboState.Focus();
                    return false;
                }
                if (cboDistrict.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select District.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboDistrict.Focus();
                    return false;
                }
            }

            // code added 26-07-2023 
            // check for kyc document 
            if (cmbKYCDoc.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Document type.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbKYCDoc.Focus();
                return false;
            }

            if (txtKYCDocdetails.Text == "")
            {
                mStrErrMsg = new string[1];
                mStrErrMsg[0] = "Document details";
                return ShowValidateError(txtKYCDocdetails, 0, mStrErrMsg, 1);
            }

            if (cmbKYCDoc.Text.ToUpper() == "PAN CARD" & !cf.IsValidPan(txtKYCDocdetails.Text))
            {
                MessageBox.Show("Invalid PAN number.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtKYCDocdetails.Focus();
                return false;
            }
            else if (cmbKYCDoc.Text.ToUpper() == "AADHAR CARD" & !cf.IsValidAadhar(txtKYCDocdetails.Text))
            {
                MessageBox.Show("Invalid AADHAR number.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtKYCDocdetails.Focus();
                return false;
            }
            else if (txtKYCDocdetails.Text.Length < 4)
            {
                MessageBox.Show("Invalid KYC Document details.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtKYCDocdetails.Focus();
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

        private void txtRoomSrch_TextChanged(System.Object sender, System.EventArgs e)
        {
            FillAvailableRooms();
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
            frmSearchNew form1 = new frmSearchNew("BN_ROOM_CHECK_IN_MST_T_FIND_V", true, eModType.BhaktaNiwas);
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
                    btnPrint.Enabled = Convert.ToBoolean(Interaction.IIf(Eprint, true, false));
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
            btnPrint.Enabled = Convert.ToBoolean(Interaction.IIf(Eprint, true, false));
            setCursor(this, true);
        }

        private bool LoadTransaction(long lngSearchId, ref bool blnLockForm)
        {
            srchFlag = 1;
            DataTable dr;
            // Dim CheckInDet As New OSOL_CONNECTION.LockerCheckInMst
            RoomCheckInDet CheckInDet = new RoomCheckInDet();
            int ctr = 0;
            bool blnFlag = false;
            mDelColl = new Collection();
            int bhaktype = 0;
            string sublocation = "";
            long sublocationID = 0;
            System.Data.DataSet dsPaymentType1;
            string paymentname = "";
            string tidno = "";
            int paymentType = 0;
            int tid = 0;
            string invoice = "";
            System.Data.DataSet dsTID;
            string state = "";
            string country = "";
            string district = "";
            int stateid = 0;
            int countryid = 0;
            FormClear();
            try
            {
                dr = RoomCheckInDALobj.GetDrRoomCheckInMst(lngSearchId);
                if (dr.Rows.Count > 0)
                {
                    txtAppNo.Text = dr.Rows[0]["AppNo"].ToString();

                    dtpCheckIn.Value = Convert.ToDateTime(dr.Rows[0]["InDate"]);
                    txtmobno.Text = dr.Rows[0]["MOB_NO"].ToString();
                    dtpCheckInTime.Value = Convert.ToDateTime(dr.Rows[0]["InTime"]);
                    txtVchNo.Text = dr.Rows[0]["SerialNo"].ToString();
                    txtVchNo.Tag = lngSearchId;
                    dtpCheckIn.Tag = dr.Rows[0]["RecordModifiedCount"];
                    txtName.Text = dr.Rows[0]["Name"].ToString();
                    txtPlace.Text = dr.Rows[0]["Place"].ToString();
                    txtVehcleNo.Text = dr.Rows[0]["VEHICLE_NO"].ToString();
                    txtDays.Text = dr.Rows[0]["Days"].ToString();
                    dtpCheckOut.Value = Convert.ToDateTime(dr.Rows[0]["OutDate"]);
                    dtpCheckOutTime.Value = Convert.ToDateTime(dr.Rows[0]["OutTime"]);
                    txtNoOfRooms.Text = dr.Rows[0]["NoOfRooms"].ToString();
                    txtNoOfPersons.Text = dr.Rows[0]["NoOfPersons"].ToString();
                    txtRemark.Text = dr.Rows[0]["REMARK"].ToString();
                    nudAdvance.Text = dr.Rows[0]["Advance"].ToString();
                    nudRent.Text = dr.Rows[0]["Rent"].ToString();
                    dtEnteredOn = Convert.ToDateTime(dr.Rows[0]["EnteredOn"]);
                    BarcodeRet = dr.Rows[0]["Barcode"].ToString();
                    sublocationID = Convert.ToInt64(dr.Rows[0]["SUBLOC_ID"]);
                    sublocation = dr.Rows[0]["SUBLOCATION"].ToString();
                    bhaktype = Convert.ToInt32(dr.Rows[0]["BHAKT_TYPE"]);
                    txtScan.Text = dr.Rows[0]["ScanImageName"].ToString();
                    imgVideo.ImageLocation = System.Configuration.ConfigurationManager.AppSettings.Get("ScannerPath") + Interaction.IIf(Information.IsDBNull(dr.Rows[0]["Image"]), dr.Rows[0]["ScanImageName"].ToString() + ".bmp", dr.Rows[0]["Image"].ToString());
                    lblimg.Visible = true;
                    invoice = dr.Rows[0]["Invoice"].ToString();
                    paymentType = Convert.ToInt32(dr.Rows[0]["PaymentType"]);
                    paymentname = dr.Rows[0]["payment_name"].ToString();
                    tidno = dr.Rows[0]["tidNo"].ToString();
                    tid = Convert.ToInt32(dr.Rows[0]["tid"]);
                    if (dr.Rows[0]["NoOfRooms"] != dr.Rows[0]["PendRoomCount"])
                        blnLockForm = true;
                    txtChqBankname.Text = dr.Rows[0]["CHQ_BANK_NAME"].ToString();
                    txtChqNo.Text = dr.Rows[0]["CHQ_NO"].ToString();
                    if (!Information.IsDBNull(dr.Rows[0]["CHQ_DATE"]))
                        dtChqDt.Value = Convert.ToDateTime(dr.Rows[0]["CHQ_DATE"]);

                    state = dr.Rows[0]["STATE"].ToString();
                    country = dr.Rows[0]["COUNTRY_NAME"].ToString();
                    district = dr.Rows[0]["DISTRICT"].ToString();
                    stateid = Convert.ToInt32(dr.Rows[0]["STATE_ID"]);
                    countryid = Convert.ToInt32(dr.Rows[0]["COUNTRY_ID"]);

                    // Code Added 08/01/2024
                    if (dr.Rows[0]["KycDocType"].ToString() != "")
                        cmbKYCDoc.Text = dr.Rows[0]["KycDocType"].ToString();
                    if (dr.Rows[0]["KycDocDetail"].ToString() != "")
                        txtKYCDocdetails.Text = dr.Rows[0]["KycDocDetail"].ToString();
                }

                cboPaymentType.Items.Clear();
                int[] ptype = new int[] { (int)eTokenDetail.Swap, (int)eTokenDetail.Cash, (int)eTokenDetail.Cheque };
                dsPaymentType1 = cf.GetDsPaymentType2(ptype);
                cf.FillComboWithDataSet(cboPaymentType, dsPaymentType1.Tables[0], "Token_Detail_Name", "Token_Detail_Name", "Token_Detail_Id");
                if (paymentType != 0)
                    cboPaymentType.Text = paymentname;
                cboCountry.SelectedIndex = cboCountry.FindStringExact(country);
                dsState = cf.GetState(countryid);
                cf.FillComboWithDataSet(cboState, dsState.Tables[0], "StateId", "StateName", "StateId");
                cboState.SelectedIndex = cboState.FindStringExact(state);
                dsDistrict = cf.GetDistrict(stateid);
                cf.FillComboWithDataSet(cboDistrict, dsDistrict.Tables[0], "DistrictId", "DistrictName", "DistrictId");
                cboDistrict.SelectedIndex = cboDistrict.FindStringExact(district);
                dsTID = cf.GetDsTID(Convert.ToInt16(txtCounter.Tag));
                cf.FillComboWithDataSet(cobTid, dsTID.Tables[0], "tidNo", "tidNo", "Tid");
                if (tid != 0)
                    cobTid.Text = tidno;
                txtInvoice.Text = invoice;
                if (bhaktype == 1)
                    rbBhakt.Checked = true;
                else if (bhaktype == 2)
                    rbGuest1.Checked = true;
                else if (bhaktype == 3)
                    rbGuest2.Checked = true;
                else if (bhaktype == 4)
                    rbDoner.Checked = true;
                else
                    rbAnnadan.Checked = true;
                cboSublocation.Text = sublocation;

                dr = RoomCheckInDALobj.GetDrRoomCheckInDet(lngSearchId, false, sublocationID);
                mDelColl = new Collection();
                CheckInDet = new RoomCheckInDet();
                CheckInDet.CheckInMstId = Convert.ToInt64(txtVchNo.Tag);


                foreach (DataRow Item in dr.Rows)
                {
                    ctr = ctr + 1;
                    chkRooms.Items.Add(new clsItemData(Item["RoomName"].ToString(), Convert.ToInt32(Item["RoomId"]), Item["RecordModifiedCount"].ToString()));
                    for (var i = 0; i <= chkRooms.Items.Count - 1; i++)
                    {
                        if (cf.lsbItemData(chkRooms, i) == Convert.ToInt32(Item["RoomId"]))
                        {
                            chkRooms.SetItemChecked(i, true);
                            CheckInDet.LockerId = Convert.ToInt32(Item["RoomId"]);
                            CheckInDet.LockerAvailableStatus = (int)eTokenDetail.StatusYes;
                            CheckInDet.LockerRecordModifiedCount = Convert.ToInt32(Item["RecordModifiedCount"]) + 1;
                            CheckInDet.CheckInDetId = Convert.ToInt64(Item["RoomCheckInDetId"]);
                            CheckInDet.CheckInMstId = Convert.ToInt64(Item["RoomCheckInMstId"]);
                            mDelColl.Add(CheckInDet);
                        }
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
            imgVideo.Image = null;
            this.Close();
        }

        private void frmRoomCheckIn_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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
                Interaction.MsgBox(ex.Message, MsgBoxStyle.Information, PrjMsgBoxTitle);
            }
        }

        public void btnPrint_Click(System.Object sender, System.EventArgs e)
        {
            // If Val(dtpCheckIn.Tag & vbNullString) = 0 Or blnformChange Then Exit Sub
            string strReportName;
            Form sForm;
            long lngError = -1;
            Collection pColl = new Collection();
            setCursor(this, false);
            System.Drawing.Printing.PrintDocument printDoc = new System.Drawing.Printing.PrintDocument();
            string printD1;

            lngError = RoomCheckInDALobj.InsertRePrint(txtVchNo.Tag, UserInfo.UserName, UserInfo.Machine_Name, UserInfo.UserId);

            // Commented By Roshan/Amit
            FillDataInDataset(BarcodeRet);
            // strReportName = "RoomCheckInReceipt.rpt"
            // sForm = New frmCrystalViewer(UserInfo.ReportPath & strReportName, , ds, , pColl, ReportID.RoomCheckIn, True)
            // sForm.Text = "Room Check In : " & ReportID.RoomCheckIn
            // sForm.Show()
            // System.Threading.Thread.Sleep(850)
            // sForm.Close()

            strReportName = "RoomCheckInNew.rdlc";
            sForm = new frmCrystalViewer(UserInfo.ReportPath + strReportName, null, ds, null, pColl, (long)eReportID.RoomCheckIn, true, false, false,"","D");
            sForm.Text = "Room Check IN : " + eReportID.RoomCheckIn;
            sForm.Show();
            System.Threading.Thread.Sleep(850);
            sForm.Close();

            setCursor(this, true);
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
            MyRow["SERIAL_NO"] = txtVchNo.Text;
            MyRow["APP_NO"] = txtAppNo.Text;
            MyRow["SublocationNm"] = cboSublocation.Text;
            MyRow["NAME"] = txtName.Text;


            MyRow["PLACE"] = txtPlace.Text;
            MyRow["MOB_NO"] = txtmobno.Text;
            MyRow["DAYS"] = txtDays.Text;

            MyRow["NO_OF_ROOMS"] = txtNoOfRooms.Text;
            MyRow["NO_OF_PERSONS"] = txtNoOfPersons.Text;
            MyRow["OUT_DATE"] = dtpCheckOut.Value;
            MyRow["OUT_TIME"] = dtpCheckOutTime.Value;

            MyRow["ADVANCE"] = nudAdvance.Value;
            MyRow["RENT"] = nudRent.Value;
            MyRow["TID"] = "TID : " + cobTid.Text;
            MyRow["INVOICE"] = "Swipe No: " + txtInvoice.Text;
            MyRow["Payment_Type"] = cboPaymentType.Text;
            MyRow["USER_NAME"] = txtUser.Text;
            MyRow["SERVER_NAME"] = UserInfo.serverName;
            MyRow["MACHINE_NAME"] = UserInfo.Machine_Name;
            total_val = nudAdvance.Value + nudRent.Value;
            MyRow["AMT_IN_WORDS"] = cf.getNumbersInWords(total_val, eCurrencyType.Rupees);
            MyRow["COUNTRY_NAME"] = cboCountry.Text;
            MyRow["STATE"] = cboState.Text;
            MyRow["DISTRICT"] = cboDistrict.Text;
            MyRow["CHQ_BANK_NAME"] = txtChqBankname.Text;
            MyRow["CHQ_NO"] = txtChqNo.Text;
            MyRow["CHQ_DATE"] = dtChqDt.Value;

            // Code added 09/01/2024
            MyRow["KYCDocType"] = cmbKYCDoc.Text;
            MyRow["KYCDocDetail"] = txtKYCDocdetails.Text;



            // 'code for print barcode
            byte[] _tempByte = null;
            IDAutomation.Windows.Forms.LinearBarCode.Barcode NewBarcode = new IDAutomation.Windows.Forms.LinearBarCode.Barcode();
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
            TempTable1 = new DataTable("BN_ROOM_CHECK_IN_MST_T");
            TempTable2 = new DataTable("BN_ROOM_CHECK_IN_DET_T");

            TempTable1.Columns.Add("CHECK_IN_MST_ID", System.Type.GetType("System.Int64"));
            TempTable1.Columns.Add("LOC_SH_NAME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("DEPT_SH_NAME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("COUNTER", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("SublocationNm", System.Type.GetType("System.String"));

            // Code added 08/01/2024
            TempTable1.Columns.Add("KYCDocType", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("KYCDocDetail", System.Type.GetType("System.String"));


            TempTable1.Columns.Add("IN_DATE", System.Type.GetType("System.DateTime"));
            TempTable1.Columns.Add("IN_TIME", System.Type.GetType("System.DateTime"));
            TempTable1.Columns.Add("SERIAL_NO", System.Type.GetType("System.Int64"));
            TempTable1.Columns.Add("APP_NO", System.Type.GetType("System.String"));

            TempTable1.Columns.Add("NAME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("PLACE", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("MOB_NO", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("DAYS", System.Type.GetType("System.Int32"));
            TempTable1.Columns.Add("Payment_Type", System.Type.GetType("System.String"));

            TempTable1.Columns.Add("TID", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("INVOICE", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("NO_OF_ROOMS", System.Type.GetType("System.Int32"));
            TempTable1.Columns.Add("NO_OF_PERSONS", System.Type.GetType("System.Int32"));
            TempTable1.Columns.Add("OUT_DATE", System.Type.GetType("System.DateTime"));
            TempTable1.Columns.Add("OUT_TIME", System.Type.GetType("System.DateTime"));

            TempTable1.Columns.Add("ADVANCE", System.Type.GetType("System.Double"));
            TempTable1.Columns.Add("RENT", System.Type.GetType("System.Double"));
            TempTable1.Columns.Add("BARCODE", System.Type.GetType("System.Byte[]"));

            TempTable1.Columns.Add("COUNTRY_NAME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("STATE", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("DISTRICT", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("CHQ_BANK_NAME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("CHQ_NO", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("CHQ_DATE", System.Type.GetType("System.DateTime"));

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
            string strname = txtName.Text;
        }

        private void txtPlace_TextChanged(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
        }

        private void txtDays_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtDays_TextChanged(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
            // If dtpCheckInTime.Value.ToString("HH:mm:ss") > dtpCheckOutTime.Value.ToString("HH:mm:ss") Then
            dtpCheckOut.Value = dtpCheckIn.Value.AddDays(Convert.ToInt32(txtDays.Text));
            // Else
            // dtpCheckOut.Value = dtpCheckIn.Value.AddDays(Val(txtDays.Text & vbNullString))
            // End If


            if (rbFlag == 1 | rbFlag == 2 | rbFlag == 3)
                nudRent.Value = 0;
            else
                showRent();
        }

        private void txtDays_Enter(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
            dtpCheckOut.Value = dtpCheckIn.Value.AddDays(Convert.ToInt32(txtDays.Text));
            if (rbFlag == 1 | rbFlag == 2 | rbFlag == 3)
                nudRent.Value = 0;
            else
                showRent();
        }

        private void txtNoOfRooms_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtNoOfRooms_TextChanged(System.Object sender, System.EventArgs e)
        {
            if (rbFlag == 1 | rbFlag == 2 | rbFlag == 3)
            {
                nudAdvance.Value = 0;
                nudRent.Value = 0;
            }
            else
                showRent();
            blnformChange = true;
        }

        private void txtVchNo_TextChanged(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
        }

        private void chkRooms_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
            double RoomRentPerday = 0;
            double RoomAdvanceTariff = 0;
            if (rbFlag == 0)
            {
                for (var i = 0; i <= chkRooms.Items.Count - 1; i++)
                {
                    if (chkRooms.GetItemChecked(i))
                    {
                        var id = cf.lsbItemData(chkRooms, i);
                        DataTable dr;
                        System.Data.DataSet drValidation;
                        try
                        {
                            drValidation = objDsRoomMst.GetValidation(id);
                            if (drValidation != null && drValidation.Tables[0].Rows.Count > 0)
                            {
                                Interaction.MsgBox("This Room is All ready allocated to online Bhankt ....");
                                // drValidation.Close()
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                        }

                        try
                        {
                            dr = objDsRoomMst.GetRent(id);
                            if (dr.Rows.Count > 0)
                            {
                                RoomRentPerday += Convert.ToDouble(dr.Rows[0]["RentPerDay"]);
                                RoomAdvanceTariff += Convert.ToDouble(dr.Rows[0]["Advance"]);
                            }
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
            nudRent.Value = Convert.ToDecimal(Math.Round(rent * Convert.ToDouble(txtDays.Text), 2));
            nudAdvance.Value = Convert.ToDecimal(Math.Round(Advance, 2));
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
        private void txtVehcleNo_KeyPress(System.Object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (char.IsLower(e.KeyChar))
                e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void rbGuest2_Click(System.Object sender, System.EventArgs e)
        {
            rbFlag = 0;
            FillAvailableRooms();
            lbAuthPer.Visible = true;
            cboAuthPer.Visible = true;
            txtName.Visible = true;
            // cboDonner.Visible = False
            MultiColumnComboBox1.Visible = false;
            MultiColumnComboBox2.Visible = false;
            nudAdvance.ReadOnly = false;
            nudRent.ReadOnly = false;
            if (srchFlag == 0)
            {
                if (txtNoOfRooms.Text != "")
                    showRent();
            }
        }


        private void rbBhakt_Click(System.Object sender, System.EventArgs e)
        {
            rbFlag = 0;
            if (srchFlag == 0)
                FillAvailableRooms();

            lbAuthPer.Visible = false;
            cboAuthPer.Visible = false;
            txtName.Visible = true;
            // cboDonner.Visible = False
            MultiColumnComboBox1.Visible = false;
            MultiColumnComboBox2.Visible = false;
            nudAdvance.ReadOnly = true;
            nudRent.ReadOnly = true;
            if (srchFlag == 0)
            {
                if (txtNoOfRooms.Text != "")
                    showRent();
            }
        }

        private void rbAnnadan_Click(System.Object sender, System.EventArgs e)
        {
            rbFlag = 3;
            FillAvailableRooms();
            // cboDonner.Text = ""
            MultiColumnComboBox1.Text = "";
            txtName.Text = "";
            // Dim bhaktid As Integer = 5


            // FillDonners(bhaktid)
            lbAuthPer.Visible = false;
            cboAuthPer.Visible = false;
            txtName.Visible = true;
            // cboDonner.Visible = True
            MultiColumnComboBox1.Visible = false;
            MultiColumnComboBox2.Visible = true;
            nudAdvance.Value = 0;
            nudRent.Value = 0;
            nudAdvance.ReadOnly = true;
            nudRent.ReadOnly = true;
        }

        private void rbGuest1_Click(System.Object sender, System.EventArgs e)
        {
            rbFlag = 1;
            FillAvailableRooms();
            lbAuthPer.Visible = true;
            cboAuthPer.Visible = true;
            // cboDonner.Visible = False
            MultiColumnComboBox1.Visible = false;
            MultiColumnComboBox2.Visible = false;
            txtName.Visible = true;
            nudAdvance.Value = 0;
            nudRent.Value = 0;
            nudAdvance.ReadOnly = true;
            nudRent.ReadOnly = true;
        }

        private void rbDoner_Click(System.Object sender, System.EventArgs e)
        {
            rbFlag = 2;
            // cboDonner.Text = ""
            MultiColumnComboBox1.Text = "";
            txtName.Text = "";
            // Dim bhaktid As Integer = 4

            // Dim dataTable As New DataTable("Donners")
            // Dim ds As DataSet
            // ds = objDsRoomMst.GetDonners(bhaktid)


            // dataTable.Columns.Add("Employee ID", GetType([String]))
            // dataTable.Columns.Add("Name", GetType([String]))
            // dataTable.Columns.Add("Designation", GetType([String]))

            // dataTable.Rows.Add(New [String]() {"D1", "Natalia", "Developer"})
            // dataTable.Rows.Add(New [String]() {"D2", "Jonathan", "Developer"})
            // dataTable.Rows.Add(New [String]() {"D3", "Jake", "Developer"})
            // dataTable.Rows.Add(New [String]() {"D4", "Abraham", "Developer"})
            // dataTable.Rows.Add(New [String]() {"T1", "Mary", "Team Lead"})
            // dataTable.Rows.Add(New [String]() {"PM1", "Calvin", "Project Manager"})
            // dataTable.Rows.Add(New [String]() {"T2", "Sarah", "Team Lead"})
            // dataTable.Rows.Add(New [String]() {"D12", "Monica", "Developer"})
            // dataTable.Rows.Add(New [String]() {"D13", "Donna", "Developer"})

            // MultiColumnComboBox1.DataSource = ds.Tables(0)

            // MultiColumnComboBox1.DisplayMember = "Donner Id"
            // 'MultiColumnComboBox1.ValueMember = "Name"
            // FillDonners(bhaktid)
            lbAuthPer.Visible = false;
            cboAuthPer.Visible = false;
            nudAdvance.Value = 0;
            nudRent.Value = 0;
            // cboDonner.Visible = True
            MultiColumnComboBox1.Visible = true;
            MultiColumnComboBox2.Visible = false;
            nudAdvance.ReadOnly = true;
            nudRent.ReadOnly = true;
        }
        private void cboDonner_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
        }

        private void MultiColumnComboBox1_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            if (MultiColumnComboBox1.Visible == true)
            {
                txtName.Text = ((System.Data.DataRowView)MultiColumnComboBox1.SelectedItem).Row.ItemArray[1].ToString();
                txtmobno.Text = ((System.Data.DataRowView)MultiColumnComboBox1.SelectedItem).Row.ItemArray[2].ToString();
                txtPlace.Text = ((System.Data.DataRowView)MultiColumnComboBox1.SelectedItem).Row.ItemArray[3].ToString();
                RoomSubloId = Convert.ToInt32(((System.Data.DataRowView)MultiColumnComboBox1.SelectedItem).Row.ItemArray[5]);
                int donerId = Convert.ToInt32(((System.Data.DataRowView)MultiColumnComboBox1.SelectedItem).Row.ItemArray[6]);
                if (cboSublocation.SelectedIndex < 0)
                    sublocid = 0;
                else
                    sublocid = cf.cmbItemdata(cboSublocation, cboSublocation.SelectedIndex);

                DataTable dr;
                try
                {
                    dr = objDsRoomMst.GetDrRoomIds(donerId, sublocid, (int)eTokenDetail.StatusYes);
                    cf.FillListBox(chkRooms, dr, "RoomName", "RoomId", "RecordModifiedCount");
                    dr = objDsRoomMst.GetDaysForDonner(donerId, RoomSubloId);
                    if (dr.Rows.Count > 0)
                        DnrAllowedDays = Convert.ToInt32(dr.Rows[0]["NoOfDays"]);
                    //dr.Close();
                }
                catch (Exception ex)
                {
                }
                if (chkRooms.Items.Count == 0)
                    FillAvailableRooms();
                txtLkrAvlbleCt.Text = chkRooms.Items.Count.ToString();
            }
        }

        private void MultiColumnComboBox2_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            if (MultiColumnComboBox2.Visible == true)
            {
                txtName.Text =  ((System.Data.DataRowView)MultiColumnComboBox2.SelectedItem).Row.ItemArray[0].ToString();
                txtmobno.Text = ((System.Data.DataRowView)MultiColumnComboBox2.SelectedItem).Row.ItemArray[1].ToString();
                txtPlace.Text = ((System.Data.DataRowView)MultiColumnComboBox2.SelectedItem).Row.ItemArray[2].ToString();
                int donerId = Convert.ToInt32(((System.Data.DataRowView)MultiColumnComboBox2.SelectedItem).Row.ItemArray[4]);
                // RoomSubloId = DirectCast(MultiColumnComboBox2.SelectedItem, System.Data.DataRowView).Row.ItemArray(5)
                if (cboSublocation.SelectedIndex < 0)
                    sublocid = 0;
                else
                    sublocid = cf.cmbItemdata(cboSublocation, cboSublocation.SelectedIndex);
                DataTable dr;
                try
                {
                    // dr = objDsRoomMst.GetDrRoomIds(donerId, sublocid, eTokenDetail.StatusActive, eTokenDetail.StatusYes)
                    // FillListBox(chkRooms, dr, "RoomName", "RoomId", "RecordModifiedCount")
                    dr = objDsRoomMst.GetDaysForAnnadan(donerId);
                    if (dr.Rows.Count > 0)
                        DnrAllowedDays = Convert.ToInt32(dr.Rows[0]["NoOfDays"]);
                    //dr.Close();
                }
                catch (Exception ex)
                {
                }
                if (chkRooms.Items.Count == 0)
                    FillAvailableRooms();
                txtLkrAvlbleCt.Text = chkRooms.Items.Count.ToString();
            }
        }
        private void cboSublocation_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            FillAvailableRooms();
        }
        private void nudAdvance_ValueChanged(System.Object sender, System.EventArgs e)
        {
            txtTotalAmt.Text = Convert.ToString(nudAdvance.Value + nudRent.Value);
        }

        private void nudRent_ValueChanged(System.Object sender, System.EventArgs e)
        {
            txtTotalAmt.Text = Convert.ToString(nudAdvance.Value + nudRent.Value);
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

        private void dtpCheckInTime_ValueChanged(System.Object sender, System.EventArgs e)
        {
        }

        private void imgVideo_Click(System.Object sender, System.EventArgs e)
        {
            if (imgVideo.Dock == DockStyle.Fill)
            {
                imgVideo.Dock = DockStyle.None;
                lblimg.Text = "Click on Image to Enlarge";
            }
            else
            {
                imgVideo.Dock = DockStyle.Fill;
                lblimg.Text = "Click on Image to Shrink";
            }
        }


        // Sub check()
        // On Error Resume Next
        // Commondialog1 = New WIA.CommonDialog
        // mydevice = Commondialog1.ShowSelectDevice
        // MsgBox(mydevice.Type)

        // On Error GoTo Err_btnTakePicture_click

        // End Sub
        private string Scan(RoomCheckInMst Collection)
        {
            string tempfile;
            WIA.Device mydevice;
            WIA.Item item;
            WIA.ImageFile F;
            Label Err_btnTakePicture_click;
            WIA.CommonDialog CommonDialogBox = new WIA.CommonDialog();
            WIA.CommonDialog Commondialog1 = new WIA.CommonDialog();
            RoomCheckInMst CheckInMst = new RoomCheckInMst();
            string F1 = System.Configuration.ConfigurationManager.AppSettings.Get("ScannerPath");
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
                WIA.DeviceManager DeviceManager1 = new WIA.DeviceManager(); //var DeviceManager1 = Interaction.CreateObject("WIA.DeviceManager");
                int i = 0;
                // For i = 1 To DeviceManager1.DeviceInfos.Count
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
                            s = txtName.Text + txtAppNo.Text;

                            // F.SaveFile(F1)
                            // F = Scanner.Items(1).Transfer()
                            // F = CommonDialogBox.ShowAcquireImage(WIA.WiaDeviceType.ScannerDeviceType, WIA.WiaImageIntent.ColorIntent, WIA.WiaImageBias.MinimizeSize, , False, False, True)
                            {
                                var withBlock = Scanner.Items[1];
                                withBlock.Properties["6146"].set_Value(WIA.WiaImageIntent.ColorIntent);  // 4 is Black-white,gray is 2, color 1 (Color Intent)
                            }
                            //F = (WIA.ImageFile)Scanner.Items[1].Transfer(WIA.FormatID.wiaFormatJPEG);
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
                            Collection.ScanDoc = s;
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
                            return "No Images scan";
                        }
                        finally
                        {
                            Scanner = null/* TODO Change to default(_) if this is not a reference type */;
                        }
                }
                else
                    Interaction.MsgBox("Scanner is not attached checked it");

                return Collection.ScanDoc;

            }
            catch (Exception ex)
            {
                answer = MessageBox.Show("There is no file in scanner or scanner not attached do you want to scan blank image", "Yes/no sample", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer == DialogResult.Yes)
                {
                    return null;
                }
                else
                {
                    return "";
                }
            }
        }
        private void cboPaymentType_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            pnlChqDtl.Visible = false;
            blnformChange = true;
            long PaymentType;
            clearfields();
            if (cboPaymentType.SelectedIndex == -1)
                return;
            PaymentType = cf.cmbItemdata(cboPaymentType, cboPaymentType.SelectedIndex);
            if (PaymentType == (Int64)eTokenDetail.Cheque)
            {
                pnlChqDtl.Visible = true;
                cobTid.Visible = false;
                txtInvoice.Visible = false;
                Label21.Visible = false;
                Label22.Visible = false;
                pnlDetails.Location = new Point(pnlChqDtl.Location.X, pnlChqDtl.Location.Y + pnlChqDtl.Height);
            }
            else
                pnlDetails.Location = new Point(pnlChqDtl.Location.X, pnlChqDtl.Location.Y);
            if (PaymentType == (Int64)eTokenDetail.Swap)
            {
                pnlChqDtl.Visible = false;
                cobTid.Visible = true;
                txtInvoice.Visible = true;
                Label21.Visible = true;
                Label22.Visible = true;
                pnlDetails.Location = new Point(pnlChqDtl.Location.X, pnlChqDtl.Location.Y);
            }
            if (PaymentType != 8)
            {
                cobTid.Enabled = true;
                txtInvoice.Enabled = true;
            }
            else
            {
                cobTid.Visible = false;
                txtInvoice.Visible = false;
                Label21.Visible = false;
                Label22.Visible = false;
            }
        }

        private void clearfields()
        {
            txtInvoice.Text = "";
            cobTid.SelectedIndex = -1;
        }

        private void txtInvoice_TextChanged(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
        }

        private void txtmobno_TextChanged(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
        }

        //public long Insert(object CheckInReceiptId, string UserName, string MacineName, int userid)
        //{
        //    long lngMstId;
        //    long lngErrNo;
        //    try
        //    {
        //        clsConnection.glbTransaction = clsConnection.glbCon.BeginTransaction();
        //        SetError("Inserting Into  ");
        //        lngErrNo = objDsRoomCheckInMst.Insert(CheckInReceiptId, UserName, MacineName, userid);
        //        if (lngErrNo < 0)
        //        {
        //            clsConnection.glbTransaction.Rollback();
        //            return lngErrNo;
        //        }
        //        else
        //        {
        //            clsConnection.glbTransaction.Commit();
        //            return lngErrNo;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        clsConnection.glbTransaction.Rollback();
        //        SetError(ex.ToString());
        //        return -1;
        //    }
        //}

        private void cboCountry_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            Int64 countryid;

            if (cboCountry.SelectedIndex == -1)
                return;
            countryid = cf.cmbItemdata(cboCountry, cboCountry.SelectedIndex);
            dsState = cf.GetState(Convert.ToInt32(countryid));
            cf.FillComboWithDataSet(cboState, dsState.Tables[0], "StateId", "StateName", "StateId");
            if (countryid == 102)
                cboState.Text = "MAHARASHTRA";
            else
                cboDistrict.Items.Clear();
        }

        private void cboState_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            Int64 state;

            if (cboState.SelectedIndex == -1)
                return;
            state = cf.cmbItemdata(cboState, cboState.SelectedIndex);
            dsDistrict = cf.GetDistrict(Convert.ToInt32(state));
            cf.FillComboWithDataSet(cboDistrict, dsDistrict.Tables[0], "DistrictId", "DistrictName", "DistrictId");
        }

        private void rbGuest2_CheckedChanged(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
            txtDays.Enabled = true;
        }

        private void rbBhakt_CheckedChanged(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
            txtDays.Text = "1";
            txtDays.Enabled = false;
        }

        private void rbDoner_CheckedChanged(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
            txtDays.Enabled = true;
        }

        private void rbAnnadan_CheckedChanged(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
            txtDays.Enabled = true;
        }

        private void rbGuest1_CheckedChanged(System.Object sender, System.EventArgs e)
        {
            blnformChange = true;
            txtDays.Enabled = true;
        }

        private void GroupBox1_Enter(System.Object sender, System.EventArgs e)
        {
        }

    }

}
