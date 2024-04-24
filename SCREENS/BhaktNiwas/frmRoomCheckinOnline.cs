using CrystalDecisions.ReportAppServer;
using IDAutomation.Windows.Forms.LinearBarCode;
using Microsoft.VisualBasic;
using SGMOSOL.ADMIN;
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
using WIA;
using SGMOSOL.BAL;

namespace SGMOSOL.SCREENS.BhaktNiwas
{
    public partial class frmRoomCheckinOnline : Form
    {
        private bool mBlnEdit = false;
        private eAction mAction;        // Reference to Enum of Type Action for ActionView,ActionNew,ActionUpdate and ActionDelete
        private ArrayList CtrlArr = new ArrayList(); // To insert Form Control in Array List
        private ArrayList btnArr = new ArrayList();
        private CommonFunctions cf = new CommonFunctions();
        //private OSOL_BLSDS.clsBlsRoomCheckIn objDsRoomCheckInMst = new OSOL_BLSDS.clsBlsRoomCheckIn();
        private RoomCheckInDAL objDsRoomCheckInMst = new RoomCheckInDAL();
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
        public string BarcodeRet = "";
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
        private string BookingID = Constants.vbNullString;
        private int iDevice = 0; // Current device ID
        private int hHwnd; // Handle to preview window

        [System.Runtime.InteropServices.DllImport("user32")]
        static extern int SendMessage(int hwnd, int wMsg, int wParam, [MarshalAs(UnmanagedType.AsAny)] object lParam);

        [System.Runtime.InteropServices.DllImport("user32")]
        static extern int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);

        [System.Runtime.InteropServices.DllImport("user32")]
        static extern bool DestroyWindow(int hndw);

        [System.Runtime.InteropServices.DllImport("avicap32.dll")]
        static extern int capCreateCaptureWindowA(string lpszWindowName, int dwStyle, int x, int y, int nWidth, short nHeight, int hWndParent, int nID);

        [System.Runtime.InteropServices.DllImport("avicap32.dll")]
        static extern bool capGetDriverDescriptionA(short wDriver, string lpszName, int cbName, string lpszVer, int cbVer);

        public frmRoomCheckinOnline()
        {
            InitializeComponent();
        }

        private void cboSublocation_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            FillAvailableRooms();
        }
        private void FillSublocations()
        {
            System.Data.DataTable dr;
            try
            {
                dr = objDsRoomMst.GetDrSublocations(RoomCheckInLockID, (Int64)eModType.BhaktaNiwas);
                cf.FillCombo(cboSublocation, dr, "Name", "DeptId");
            }
            catch (Exception ex)
            {
            }
        }
        private void frmRoomCheckinOnline_Load(System.Object sender, System.EventArgs e)
        {
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
            // To open webcamara in preview mode
            OpenPreviewWindow();
            //Int32 ctr;
            //ctr = (MDI.Size.Width - this.Size.Width) / (double)2;
            //this.Location = new Point(ctr, 0);
            CreateDs();
            // Dim dr As System.Data.DataTable
            // Try
            // dr = objDsRoomMst.GetDrRoomTariff()
            // If dr.Read Then
            // RoomAdvanceTariff = dr("Advance")
            // RoomRentPerday = dr("RentPerDay")
            // End If
            // dr.Close()
            // Catch ex As Exception
            // If Not dr Is Nothing Then dr.Close()
            // End Try

            if (btnNew.Enabled)
                btnNew_Click(null, null);
            else
                cf.subLockForm(true, CtrlArr, false);
        }
        private void FillCounter()
        {
            System.Data.DataTable dr;
            dr = cf.GetDrCounterMachId(UserInfo.UserId, SystemHDDModelNo, SystemHDDSerialNo, SystemMacID, Convert.ToInt16(eModType.BhaktaNiwas));
            if (dr.Rows.Count > 0)
            {
                txtCounter.Text = dr.Rows[0]["CounterMachineTitle"].ToString();
                txtCounter.Tag = dr.Rows[0]["CtrMachId"];
                RoomCheckInDeptID = Convert.ToInt32(dr.Rows[0]["DeptId"]);
                RoomCheckInDeptName = dr.Rows[0]["DepartmentName"].ToString();
                RoomCheckInLocName = dr.Rows[0]["LocName"].ToString();
                RoomCheckInLockID = Convert.ToInt32(dr.Rows[0]["LocId"]);
                mStrCounterMachineShortName = dr.Rows[0]["CounterMachineShortName"].ToString();
            }
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
        private void CreateDs()
        {
            TempTable1 = new DataTable("BN_ROOM_CHECK_IN_MST_T");
            TempTable2 = new DataTable("BN_ROOM_CHECK_IN_DET_T");

            TempTable1.Columns.Add("CHECK_IN_MST_ID", System.Type.GetType("System.Int64"));
            TempTable1.Columns.Add("LOC_SH_NAME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("DEPT_SH_NAME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("COUNTER", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("SublocationNm", System.Type.GetType("System.String"));

            TempTable1.Columns.Add("IN_DATE", System.Type.GetType("System.DateTime"));
            TempTable1.Columns.Add("IN_TIME", System.Type.GetType("System.DateTime"));
            TempTable1.Columns.Add("SERIAL_NO", System.Type.GetType("System.Int64"));
            TempTable1.Columns.Add("APP_NO", System.Type.GetType("System.String"));

            TempTable1.Columns.Add("NAME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("PLACE", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("MOB_NO", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("DAYS", System.Type.GetType("System.Int32"));

            TempTable1.Columns.Add("NO_OF_ROOMS", System.Type.GetType("System.Int32"));
            TempTable1.Columns.Add("NO_OF_PERSONS", System.Type.GetType("System.Int32"));
            TempTable1.Columns.Add("OUT_DATE", System.Type.GetType("System.DateTime"));
            TempTable1.Columns.Add("OUT_TIME", System.Type.GetType("System.DateTime"));
            TempTable1.Columns.Add("BARCODE", System.Type.GetType("System.Byte[]"));
            TempTable1.Columns.Add("ADVANCE", System.Type.GetType("System.Double"));
            TempTable1.Columns.Add("RENT", System.Type.GetType("System.Double"));

            TempTable1.Columns.Add("USER_NAME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("SERVER_NAME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("MACHINE_NAME", System.Type.GetType("System.String"));
            TempTable1.Columns.Add("AMT_IN_WORDS", System.Type.GetType("System.String"));

            TempTable2.Columns.Add("CHECK_IN_MST_ID", System.Type.GetType("System.Int64"));
            TempTable2.Columns.Add("ROOM_NAME", System.Type.GetType("System.String"));

            ds.Tables.Add(TempTable1);
            ds.Tables.Add(TempTable2);
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
            // rbBhakt.Checked = True
            // rbBhakt.PerformClick()
            cboSublocation.SelectedIndex = -1;
        }
        private void BhaktClick(System.Object sender, System.EventArgs e)
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

        private void btnNew_Click(System.Object sender, System.EventArgs e)
        {
            srchFlag = 0;
            System.Data.DataTable dr;
            FormClear();
            cf.fncSetDateAndRange(dtpCheckIn);
            cf.fncSysTime(dtpCheckInTime);
            cf.fncSysTime(dtpCheckOutTime);
            // fncSysTimePlus2(dtpCheckOutTime)
            // fncSysDateTimePlus2(dtpCheckOut, dtpCheckOutTime)
            dtpCheckOut.Value = (dtpCheckIn.Value.AddDays(Convert.ToInt32(txtDays.Text)));

            // dtpCheckOutTime.Value = dtpCheckInTime.Value
            // dtpCheckOutTime.Value = dtpCheckInTime.Value
            dtpCheckIn.Enabled = bkDateEntry;
            mAction = eAction.ActionInsert;
            cf.subLockForm(false, CtrlArr, false);
            btnSave.Enabled = btnNew.Enabled;
            try
            {
                dr = objDsRoomCheckInMst.GetMaxSerialNo(UserInfo.CompanyID, RoomCheckInLockID, 0, UserInfo.fy_id);
                if (dr.Rows.Count > 0)
                    txtVchNo.Text = (Convert.ToInt32(dr.Rows[0]["SerialNo"]) + 1).ToString();
                else
                    txtVchNo.Text = "1";
            }
            catch (Exception ex)
            {

            }
            // FillAvailableRooms()
            // FillDonners(4)
            // FillDonners(5)
            // FillAuthPersons()
            FillSublocations();
            BhaktClick(null, null);
            chkRooms.Enabled = true;
            // txtAppNo.Focus()
            blnformChange = false;
            txtBarcode.Text = "";
        }


        private void FillAvailableRooms()
        {
            flag1 = 0;
            var str = txtRoomSrch.Text;
            System.Data.DataTable dr;
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
        private void showRent()
        {
            nudRent.Value = Convert.ToDecimal(Math.Round(rent * Convert.ToDouble(txtDays.Text), 2));
            nudAdvance.Value = Convert.ToDecimal(Math.Round(Advance, 2));
        }
        private void nudAdvance_ValueChanged(System.Object sender, System.EventArgs e)
        {
            txtTotalAmt.Text = Convert.ToString(nudAdvance.Value + nudRent.Value);
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
                        System.Data.DataTable dr;
                        System.Data.DataSet drValidation;

                        try
                        {
                            drValidation = objDsRoomMst.GetValidation(id);
                            if (drValidation.Tables[0].Rows.Count > 0)
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

            // rent = RoomRentPerday
            Advance = RoomAdvanceTariff;
            showRent();
            txtNoOfRooms.Text = chkRooms.CheckedItems.Count.ToString();
        }

        private void btnSave_Click(System.Object sender, System.EventArgs e)
        {
            srchFlag = 0;
            long lngError = -1;
            if (blnformChange == false)
                return;
            if (fncSave())
            {
                if (BookingID != null)
                {
                    objDsRoomCheckInMst.DeleteRoomLocked(BookingID);
                }
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
            RoomCheckInMst = GetCheckInMst();
            System.Data.DataTable dr;
            dr = objDsRoomCheckInMst.GetDaysLimit();
            if (dr.Rows.Count > 0)
                MaxDays = Convert.ToInt32(dr.Rows[0]["EXPIRY_DAYS"]);

            if (RoomCheckInMst.Days > MaxDays | RoomCheckInMst.Days == 0)
            {
                MessageBox.Show("Days Are Not Acceptable!", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDays.Text = "";
                return false;
            }
            if (RoomCheckInMst.BhaktTypeId == 4 | RoomCheckInMst.BhaktTypeId == 5)
            {
                if (RoomCheckInMst.Days > DnrAllowedDays)
                {
                    MessageBox.Show("Days are not allowed for this doner!", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDays.Text = DnrAllowedDays.ToString();
                    return false;
                }
            }


            dr = objDsRoomCheckInMst.GetRoomLimit();
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
                lngError = objDsRoomCheckInMst.Insert(RoomCheckInMst, coll, UserInfo.UserName, UserInfo.Machine_Name, lngSerialNo, dtEnteredOn);
                if (lngError >= 0)
                {
                    dr = objDsRoomCheckInMst.GetDrRoomCheckInMstId();
                    if (dr.Rows.Count > 0)
                        lngSerialNo = Convert.ToInt64(dr.Rows[0]["SerialNo"]);

                    txtVchNo.Text = lngSerialNo.ToString();
                }
            }
            else if (mAction == eAction.ActionUpdate)
                lngError = objDsRoomCheckInMst.Update(RoomCheckInMst, coll, mDelColl, UserInfo.UserName, UserInfo.Machine_Name, lngSerialNo);
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
                // btnPrint_Click(Nothing, Nothing)
                if (mAction == eAction.ActionInsert)
                {
                    blnformChange = false;
                    MethodForPrint();
                }
                else
                    btnPrint_Click(null, null);
            }
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
            // strReportName = "RoomCheckInReceipt.rpt"


            // FillDataInDataset(txtmobno.Tag)
            // sForm = New frmCrystalViewer(UserInfo.ReportPath & strReportName, , ds, , pColl, ReportID.RoomCheckIn, True)
            // sForm.Text = "Room Check In : " & ReportID.RoomCheckIn
            // sForm.Show()
            // sForm.Close()

            strReportName = "RoomCheckInReceipt.rdlc";




            FillDataInDataset(BarcodeRet);
            sForm = new frmCrystalViewer(UserInfo.ReportPath + strReportName, null, ds, null, pColl, (long)eReportID.RoomCheckIn, true);
            sForm.Text = "Room Check In : " + eReportID.RoomCheckIn;
            sForm.Show();
            System.Threading.Thread.Sleep(850);
            sForm.Close();
            strReportName = "RoomCheckInNew.rdlc";

            sForm = new frmCrystalViewer(UserInfo.ReportPath + strReportName, null, ds, null, pColl, (long)eReportID.RoomCheckIn1, true);
            sForm.Text = "Room Check IN : " + eReportID.RoomCheckIn1;

            sForm.Show();
            System.Threading.Thread.Sleep(850);
            sForm.Close();
        }
        public static string val1 = "";
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
            CheckInMst.Sublocid = cf.cmbItemdata(cboSublocation, cboSublocation.SelectedIndex);
            CheckInMst.sublocn = cboSublocation.Text;
            CheckInMst.Barcode = GenerateRandomString(4) + txtVchNo.Text;
            CheckInMst.InDate = dtpCheckIn.Value;
            CheckInMst.InTime = dtpCheckInTime.Value;
            CheckInMst.SerialNo = Convert.ToInt64(txtVchNo.Tag);
            if (rbFlag == 2)
            {
                CheckInMst.donerId = Convert.ToInt32(((System.Data.DataRowView)MultiColumnComboBox1.SelectedItem).Row.ItemArray[6]);
                CheckInMst.DonnerRoomId = Convert.ToInt32(((System.Data.DataRowView)MultiColumnComboBox1.SelectedItem).Row.ItemArray[5]);
            }
            else if (rbFlag == 3)
            {
                CheckInMst.donerId = Convert.ToInt32(((System.Data.DataRowView)MultiColumnComboBox2.SelectedItem).Row.ItemArray[6]);
                CheckInMst.DonnerRoomId = Convert.ToInt32(((System.Data.DataRowView)MultiColumnComboBox1.SelectedItem).Row.ItemArray[5]);
            }
            CheckInMst.Name = txtName.Text;


            if ((Helper.final == ""))
                CheckInMst.Image = val1;
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
            if (cboAuthPer.Visible == true)
            {
                if (cboAuthPer.SelectedIndex != -1)
                    CheckInMst.AuthPersonId = ((clsItemData)cboAuthPer.SelectedItem).ItemData;
            }



            // If rbBhakt.Checked Then
            CheckInMst.BhaktTypeId = 1;
            // ElseIf rbGuest1.Checked Then
            // CheckInMst.BhaktTypeId = 2
            // ElseIf rbGuest2.Checked Then
            // CheckInMst.BhaktTypeId = 3
            // ElseIf rbDoner.Checked Then
            // CheckInMst.BhaktTypeId = 4
            // Else
            // CheckInMst.BhaktTypeId = 5
            // End If
            CheckInMst.ServerName = UserInfo.serverName;
            CheckInMst.MachineName = UserInfo.Machine_Name;
            CheckInMst.EnteredBy = UserInfo.UserName;
            CheckInMst.ModifiedBy = UserInfo.UserName;
            CheckInMst.RecordModifiedCount = Convert.ToInt64(dtpCheckIn.Tag) + 1;
            CheckInMst.isOnline = "1";
            CheckInMst.BookingID = BookingID;
            return CheckInMst;
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

            return true;
        }
        private bool ShowValidateError(Control myObject, int tabIndex, string[] ErrMsg, int ErrNo)
        {
            setCursor(this, true);
            MessageBox.Show(cf.GetErrorMessage(ErrMsg, ErrNo), PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            myObject.Focus();
            return false;
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
                    System.Data.DataTable dr;
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

        private void txtBarcode_KeyPress(System.Object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            string BarCode = Convert.ToString(txtBarcode.Text).Trim();
            System.Data.DataSet ds = new System.Data.DataSet();
            try
            {
                if (BarCode != Constants.vbNullString)
                {
                    if (e.KeyChar == Convert.ToChar(13))
                    {
                        ds = objDsRoomMst.GetDataForFillingFromOnline(BarCode);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            BookingID = ds.Tables[0].Rows[0]["ROOM_BOOKING_MST_ID"].ToString();
                            txtName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                            txtmobno.Text = ds.Tables[0].Rows[0]["REG_MOBILE"].ToString();
                            txtDays.Text = ds.Tables[0].Rows[0]["NO_OF_DAYS"].ToString();
                            txtNoOfPersons.Text = ds.Tables[0].Rows[0]["NO_OF_PERSON"].ToString();
                            txtNoOfRooms.Text = ds.Tables[0].Rows[0]["NO_OF_ROOM"].ToString();
                            txtPlace.Text = ds.Tables[0].Rows[0]["PER_ADDRESS_LINE1"].ToString();
                            // drValidation.Close()
                            return;
                        }
                        else
                            BookingID = "0";
                    }
                    txtBarcode.Focus();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void btnPrint_Click(System.Object sender, System.EventArgs e)
        {
            string strReportName;
            Form sForm;
            Collection pColl = new Collection();
            setCursor(this, false);
            System.Drawing.Printing.PrintDocument printDoc = new System.Drawing.Printing.PrintDocument();
            string printD1;
            long lngError = -1;
            // strReportName = "RoomCheckInReceipt.rpt"
            // ' FillDataInDataset()
            // sForm = New frmCrystalViewer(UserInfo.ReportPath & strReportName, , ds, , pColl, ReportID.RoomCheckIn, True)
            // sForm.Text = "Room Check In : " & ReportID.RoomCheckIn
            // sForm.Show()
            // sForm.Close()

            lngError = objDsRoomCheckInMst.InsertRePrint(txtVchNo.Tag, UserInfo.UserName, UserInfo.Machine_Name, UserInfo.UserId);

            strReportName = "RoomCheckInReceipt.rdlc";




            FillDataInDataset(BarcodeRet);
            sForm = new frmCrystalViewer(UserInfo.ReportPath + strReportName, null, ds, null, pColl, (long)eReportID.RoomCheckIn, true);
            sForm.Text = "Room Check In : " + eReportID.RoomCheckIn;
            sForm.Show();
            System.Threading.Thread.Sleep(850);
            sForm.Close();
            strReportName = "RoomCheckInNew.rdlc";

            sForm = new frmCrystalViewer(UserInfo.ReportPath + strReportName, null, ds, null, pColl, (long)eReportID.RoomCheckIn1, true);
            sForm.Text = "Room Check IN : " + eReportID.RoomCheckIn1;

            sForm.Show();
            System.Threading.Thread.Sleep(850);
            sForm.Close();
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
            MyRow["IN_TIME"] = dtpCheckInTime.Text;
            MyRow["SERIAL_NO"] = (txtVchNo.Text);
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

            MyRow["USER_NAME"] = txtUser.Text;
            MyRow["SERVER_NAME"] = UserInfo.serverName;
            MyRow["MACHINE_NAME"] = UserInfo.Machine_Name;
            total_val = nudAdvance.Value + nudRent.Value;
            MyRow["AMT_IN_WORDS"] = cf.getNumbersInWords(total_val, eCurrencyType.Rupees);
            // 'code for print barcode
            byte[] _tempByte = null;
            IDAutomation.Windows.Forms.LinearBarCode.Barcode NewBarcode = new Barcode();
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
            frmSearchNew form1 = new frmSearchNew("dbo.BN_RoomCheck_IN_ONLINE_V", false, eModType.BhaktaNiwas);
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
            setCursor(this, true);
        }

        private bool LoadTransaction(long lngSearchId, ref bool blnLockForm)
        {
            srchFlag = 1;
            System.Data.DataTable dr;
            // Dim CheckInDet As New OSOL_CONNECTION.LockerCheckInMst
            RoomCheckInDet CheckInDet = new RoomCheckInDet();
            int ctr = 0;
            bool blnFlag = false;
            mDelColl = new Collection();
            int bhaktype;
            string sublocation = "";
            Int64 sublocationID = 0;
            FormClear();
            try
            {
                dr = objDsRoomCheckInMst.GetDrRoomCheckInMst(lngSearchId);
                if (dr.Rows.Count > 0)
                {
                    txtAppNo.Text = dr.Rows[0]["AppNo"].ToString();
                    txtBarcode.Text = dr.Rows[0]["serialNo"].ToString();
                    dtpCheckIn.Value = Convert.ToDateTime(dr.Rows[0]["InDate"]);
                    txtmobno.Text = dr.Rows[0]["MOB_NO"].ToString();
                    dtpCheckInTime.Value = Convert.ToDateTime(dr.Rows[0]["InTime"]);
                    txtVchNo.Text = (dr.Rows[0]["SerialNo"]).ToString();
                    txtVchNo.Tag = lngSearchId;
                    dtpCheckIn.Tag = dr.Rows[0]["RecordModifiedCount"];
                    txtName.Text = dr.Rows[0]["Name"].ToString();
                    txtPlace.Text = dr.Rows[0]["Place"].ToString();
                    BarcodeRet = dr.Rows[0]["Barcode"].ToString();
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
                    sublocationID = Convert.ToInt64(dr.Rows[0]["SUBLOC_ID"]);
                    sublocation = dr.Rows[0]["SUBLOCATION"].ToString();
                    bhaktype = Convert.ToInt32(dr.Rows[0]["BHAKT_TYPE"]);
                    txtScan.Text = dr.Rows[0]["ScanImageName"].ToString();



                    if (dr.Rows[0]["NoOfRooms"] != dr.Rows[0]["PendRoomCount"])
                        blnLockForm = true;
                }
                // If bhaktype = 1 Then
                // rbBhakt.Checked = True
                // 'rbBhakt.PerformClick()
                // ElseIf bhaktype = 2 Then
                // rbGuest1.Checked = True
                // 'rbGuest1.PerformClick()
                // ElseIf bhaktype = 3 Then
                // rbGuest2.Checked = True
                // 'rbGuest2.PerformClick()
                // ElseIf bhaktype = 4 Then
                // rbDoner.Checked = True
                // 'rbDoner.PerformClick()
                // Else
                // rbAnnadan.Checked = True
                // 'rbAnnadan.PerformClick()
                // End If
                cboSublocation.Text = sublocation;
                dr = objDsRoomCheckInMst.GetDrRoomCheckInDet(lngSearchId, false, sublocationID);
                mDelColl = new Collection();
                CheckInDet = new RoomCheckInDet();
                CheckInDet.CheckInMstId = Convert.ToInt64(txtVchNo.Tag);


                //while (dr.Read)
                foreach (DataRow drItem in dr.Rows)
                {
                    ctr = ctr + 1;
                    chkRooms.Items.Add(new clsItemData(drItem["RoomName"].ToString(), Convert.ToInt32(drItem["RoomId"]), drItem["RecordModifiedCount"].ToString()));
                    for (var i = 0; i <= chkRooms.Items.Count - 1; i++)
                    {
                        if (cf.lsbItemData(chkRooms, i) == Convert.ToInt32(drItem["RoomId"]))
                        {
                            chkRooms.SetItemChecked(i, true);
                            CheckInDet.LockerId = Convert.ToInt32(drItem["RoomId"]);
                            CheckInDet.LockerAvailableStatus = (int)eTokenDetail.StatusYes;
                            CheckInDet.LockerRecordModifiedCount = Convert.ToInt64(drItem["RecordModifiedCount"]) + 1;
                            CheckInDet.CheckInDetId = Convert.ToInt64(drItem["RoomCheckInDetId"]);
                            CheckInDet.CheckInMstId = Convert.ToInt64(drItem["RoomCheckInMstId"]);
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
                // F = CommonDialogBox.ShowAcquireImage(WIA.WiaDeviceType.CameraDeviceType)
                WIA.DeviceManager DeviceManager1 = new WIA.DeviceManager();//Interaction.CreateObject("WIA.DeviceManager");
                int i = 0;
                // For i = 1 To DeviceManager1.DeviceInfos.Count
                mydevice = CommonDialogBox.ShowSelectDevice(WIA.WiaDeviceType.ScannerDeviceType, true, false);

                if (DeviceManager1.DeviceInfos[1].Type == WiaDeviceType.CameraDeviceType)
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
                            F = CommonDialogBox.ShowAcquireImage(WIA.WiaDeviceType.ScannerDeviceType);
                            if (Information.IsNothing(F))
                            {
                                answer = MessageBox.Show("There is no file in scanner do you want to scan blank image", "Yes/no sample", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (answer == DialogResult.Yes)
                                {
                                    return null;
                                }
                            }
                            F1 = F1 + s + "." + F.FileExtension;
                            Collection.ScanDoc = s;
                            var filesystemobject = Interaction.CreateObject("Scripting.FileSystemObject");
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
                            Collection.ScanDoc = "No Images scan";
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
                answer = MessageBox.Show("There is no file in scanner do you want to scan blank image", "Yes/no sample", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer == DialogResult.Yes)
                {
                    return null;
                }
                else { return null; }
            }
        }

        private void txtRoomSrch_TextChanged(object sender, EventArgs e)
        {
            FillAvailableRooms();
        }
    }

}
