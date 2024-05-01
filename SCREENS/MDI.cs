using SGMOSOL.ADMIN;
using SGMOSOL.BAL;
using SGMOSOL.DAL.Locker;
using SGMOSOL.Custom_User_Contols;
using SGMOSOL.SCREENS;
using SGMOSOL.SCREENS.Bed_System;
using SGMOSOL.SCREENS.BedSystem;
using SGMOSOL.SCREENS.Locker;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SGMOSOL.ADMIN.CommonFunctions;
using SGMOSOL.SCREENS.BhaktNiwas;

namespace SGMOSOL
{
    public partial class MDI : Form
    {
        frmUserDengi frmuserDengi;
        frmDengiReceipt frmDengiReceip;
        frmChnagePassword frmChnagePassword;
        frmBhojnalayaPrintReceipt frmbhojnalayaPrintReceipt;
        frmLockerCheckIn frmLockerCheckIn;
        frmLockerCheckOut frmLockerCheckOut;
        frmLockerList frmLockerList;
        //frmOccupiedLockers frmOccupiedLockers;
        frmLockerList frmOccupiedLockers;
        frmLockerChange frmlockerExtend;
        frmLockerChange frmLockerChange;
        frmDailyVoucherEntryBed frmDailyVoucherEntryLocker;
        frmDmLockerList frmDmLockerList;
        frmLockChkOutWrng frmLockChkOutWrng;
        frmLockChkOutWrng frmMoreThan3Day;
        frmDamagedLokers frmDamagedLokers;
        frmMessReport frmLockerAdvanceVoucher;
        frmDailyVoucherEntryBed frmDailyVoucherEntryBed;
        FrmBedCheckIN FrmBedCheckIN;
        FrmBedCheckOut FrmBedCheckOut;
        FrmBedO FrmBedO;
        frmMessReport frmBedAdvanceVoucher;
        frmCalculation frmCalculation;
        CommonFunctions cm;
        SessionManager sessionManager;
        frmRoomCheckIn frmRoomCheckIn;
        frmRoomCheckOut frmRoomCheckOut;
        frmRoomOccupied frmRoomOccupied;
        frmRoomChange frmRoomChange;
        frmDailyVoucherEntryBed dailyVoucherEntryBhaktNiwas;
        frmDmRoomList frmDmRoomList;
        frmRoomDamaged frmRoomDamaged;
        frmRoomCheckOut frmRoomTimeBefore;
        frmRoomChkOutWrng frmRoomChkOutWrng;
        frmRoomMoreThan3D frmRoomMoreThan3D;
        frmRoomLocked frmRoomLocked;
        frmRoomCheckinOnline frmRoomCheckinOnline;
        frmRoomList frmRoomList;
        frmMessReport frmMessReportR1;
        frmMessReport frmMessReportR2;
        frmMessReport frmMessReportR3;
        frmMessReport frmMessReportR4;
        frmMessReport frmMessReportR5;
        frmMessReport frmMessReportR6;
        frmMessReport frmMessReportR7;

        public MDI()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            cm = new CommonFunctions();
            this.Text = cm.getFormTitle() + " / " + Application.ProductVersion;
        }

        private void MDI_Load(object sender, EventArgs e)
        {
            // Show the login form
            frmLogin loginForm = new frmLogin();
            loginForm.WindowState = FormWindowState.Maximized;

            //Show the MDI form if login is successful
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                // If login is successful, start the MDI parent form
                InitAppParam();
                LoadBedCheckInMaxAmount();
                LoadRoomCheckInMaxAmount();
                sessionManager = new SessionManager();
                sessionManager.StartTimer();
            }
            else
            {
                // If login fails or the login form is closed, exit the application
                Application.Exit();
            }
        }

            //
            //
            //sessionManager = new SessionManager();
            //sessionManager.StartTimer();
        
        public void InitAppParam()
        {
            //UserInfo.Rounding = "N";
            UserInfo.TodaysDate = cm.GetComSysdate();
            //UserInfo.MaxRowsOfFindGrid = 300;
            //UserInfo.DengiAlertAmt = 5000;
            //UserInfo.InstrumentDateDays = 30;
            UserInfo.ReportPath = Application.StartupPath.Replace("bin\\Debug","") + "Reports\\";

            bool exists = System.IO.Directory.Exists(Application.StartupPath.Replace("bin\\Debug", "") + "BarocdePath");
            if (!exists)
                System.IO.Directory.CreateDirectory(Application.StartupPath.Replace("bin\\Debug", "") + "BarocdePath");

            exists = System.IO.Directory.Exists(Application.StartupPath.Replace("bin\\Debug", "") + "ScanImages");
            if (!exists)
                System.IO.Directory.CreateDirectory(Application.StartupPath.Replace("bin\\Debug", "") + "ScanImages");

        }
        private void LoadBedCheckInMaxAmount()
        {
            try
            {
                UserInfo.BedCheckInMaxAmount = CommonFunctions.getBedCheckInMaxAmount(); ;
            }
            catch (Exception ex)
            {
                UserInfo.BedCheckInMaxAmount = 0;
            }
        }
        private void LoadRoomCheckInMaxAmount()
        {
            try
            {
                UserInfo.RoomCheckInMaxAmount = CommonFunctions.getRoomCheckInMaxAmount(); ;
            }
            catch (Exception ex)
            {
                UserInfo.RoomCheckInMaxAmount = 0;
            }
        }
        private void dengiToolStripMenuItem_Click(object sender, EventArgs e)
        {

            
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChnagePassword = Application.OpenForms.OfType<frmChnagePassword>().FirstOrDefault();
            if (frmChnagePassword == null)
            {
                frmChnagePassword = new frmChnagePassword(false);
                frmChnagePassword.StartPosition = FormStartPosition.CenterParent;
                frmChnagePassword.MdiParent = this;
                frmChnagePassword.WindowState = FormWindowState.Maximized;
                frmChnagePassword.Show();
            }
            frmuserDengi = Application.OpenForms.OfType<frmUserDengi>().FirstOrDefault();
            if (frmuserDengi != null)
            {
                frmuserDengi.Close();
            }
        }

        private void printReceiptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmbhojnalayaPrintReceipt = Application.OpenForms.OfType<frmBhojnalayaPrintReceipt>().FirstOrDefault();
            if (frmbhojnalayaPrintReceipt == null)
            {
                frmbhojnalayaPrintReceipt = new frmBhojnalayaPrintReceipt();
                frmbhojnalayaPrintReceipt.StartPosition = FormStartPosition.CenterParent;
                frmbhojnalayaPrintReceipt.MdiParent = this;
                frmbhojnalayaPrintReceipt.WindowState = FormWindowState.Maximized;
                frmbhojnalayaPrintReceipt.Show();
            }  
            frmuserDengi = Application.OpenForms.OfType<frmUserDengi>().FirstOrDefault();
            if (frmuserDengi == null)
            {
                frmuserDengi = new frmUserDengi();
                frmuserDengi.WindowState = FormWindowState.Minimized;
                frmuserDengi.Show();
            }
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginBAL loginBAL = new LoginBAL();
            loginBAL.updateUser_Login_Details();
            loginBAL.DeleteUser_Login_details();
            this.MDI_Load(null,null);

        }

        private void calculationFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCalculation = Application.OpenForms.OfType<frmCalculation>().FirstOrDefault();
            if (frmCalculation == null)
            {
                frmCalculation = new frmCalculation();
                frmCalculation.StartPosition = FormStartPosition.CenterParent;
                frmCalculation.MdiParent = this;
                frmCalculation.WindowState = FormWindowState.Maximized;
                frmCalculation.Show();
            }
            frmuserDengi = Application.OpenForms.OfType<frmUserDengi>().FirstOrDefault();
            if (frmuserDengi != null)
            {
                frmuserDengi.Close();
            }
        }

        private void MDI_FormClosing(object sender, FormClosingEventArgs e)
        {
            LoginBAL loginBAL = new LoginBAL();
            loginBAL.updateUser_Login_Details();
            loginBAL.DeleteUser_Login_details();
            // Application.Exit();
            //s System.Diagnostics.Process.Start(Application.ExecutablePath);
        }
        private void ResetApplicationState()
        {
            // Close or hide any child forms or dialogs
            List<Form> openFormsCopy = new List<Form>(Application.OpenForms.Cast<Form>());
            foreach (Form form in openFormsCopy)
            {
                if (form != this && form != null) // Exclude the MDI parent form
                {
                    form.Close(); // or form.Hide();
                }
            }
        }
        private void encryptionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void encryptionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmEncryption frm = new frmEncryption();
            frm.Show();
        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void totalDengiReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void totalDengiReportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmDengiReceip = Application.OpenForms.OfType<frmDengiReceipt>().FirstOrDefault();
            if (frmDengiReceip == null)
            {
                frmDengiReceip = new frmDengiReceipt();
                frmDengiReceip.StartPosition = FormStartPosition.CenterParent;
                frmDengiReceip.MdiParent = this;
                frmDengiReceip.WindowState = FormWindowState.Maximized;
                frmDengiReceip.Show();
            }
            frmuserDengi = Application.OpenForms.OfType<frmUserDengi>().FirstOrDefault();
            if (frmuserDengi == null)
            {
                frmuserDengi = new frmUserDengi();
                frmuserDengi.WindowState = FormWindowState.Minimized;
                frmuserDengi.Show();
            }
           
        }

        private void totalDengiReportToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmTotalDengiReport frm = new frmTotalDengiReport();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void requirementToAdminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReqToAdmin frm=new frmReqToAdmin();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void MDI_KeyPress(object sender, KeyPressEventArgs e)
        {
            sessionManager.ResetSession();
        }

        private void MDI_KeyDown(object sender, KeyEventArgs e)
        {
            sessionManager.ResetSession();
        }

        private void MDI_MouseMove(object sender, MouseEventArgs e)
        {
            sessionManager.ResetSession();
        }

        private void MDI_MouseClick(object sender, MouseEventArgs e)
        {
            sessionManager.ResetSession();
        }
        private void lockerCheckINToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLockerCheckIn = new frmLockerCheckIn();
            frmLockerCheckIn.StartPosition = FormStartPosition.CenterParent;
            frmLockerCheckIn.MdiParent = this;
            frmLockerCheckIn.WindowState = FormWindowState.Maximized;
            frmLockerCheckIn.Show();
        }

        private void checkOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLockerCheckOut = new frmLockerCheckOut(CommonFunctions.eScreenID.LockerCheckOut);
            frmLockerCheckOut.StartPosition = FormStartPosition.CenterParent;
            frmLockerCheckOut.MdiParent = this;
            frmLockerCheckOut.WindowState = FormWindowState.Maximized;
            //frmLockerCheckOut.mScreenID = CommonFunctions.eScreenID.LockerCheckOut;
            frmLockerCheckOut.Show();
        }

        private void availableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLockerList = new frmLockerList(CommonFunctions.eScreenID.LockerAvailable);
            frmLockerList.StartPosition = FormStartPosition.CenterParent;
            frmLockerList.MdiParent = this;
            frmLockerList.WindowState = FormWindowState.Maximized;
            //frmLockerList.mScreenID = CommonFunctions.eScreenID.LockerAvailable;
            frmLockerList.Show();
        }

        private void occupiedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOccupiedLockers = new frmLockerList(CommonFunctions.eScreenID.Lockeroccupied);
            frmOccupiedLockers.StartPosition = FormStartPosition.CenterParent;
            frmOccupiedLockers.MdiParent = this;
            frmOccupiedLockers.WindowState = FormWindowState.Maximized;
            frmOccupiedLockers.Show();
            //frmOccupiedLockers = new frmOccupiedLockers();
            //frmOccupiedLockers.StartPosition = FormStartPosition.CenterParent;
            //frmOccupiedLockers.MdiParent = this;
            //frmOccupiedLockers.WindowState = FormWindowState.Maximized;
            //frmOccupiedLockers.Show();
        }

        private void lockerExtendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmlockerExtend = new frmLockerChange(CommonFunctions.eScreenID.LockerExtend);
            frmlockerExtend.StartPosition = FormStartPosition.CenterParent;
            frmlockerExtend.MdiParent = this;
            frmlockerExtend.WindowState = FormWindowState.Maximized;
            //frmlockerExtend.mScreenID = CommonFunctions.eScreenID.LockerExtend;
            frmlockerExtend.Show();
        }

        private void lockerChangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLockerChange = new frmLockerChange(CommonFunctions.eScreenID.Lockerchange);
            frmLockerChange.StartPosition = FormStartPosition.CenterParent;
            frmLockerChange.MdiParent = this;
            frmLockerChange.WindowState = FormWindowState.Maximized;
            //frmLockerChange.mScreenID = CommonFunctions.eScreenID.Lockerchange;
            frmLockerChange.Show();
        }

        private void dailyVoucherEntryLockerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDailyVoucherEntryLocker = new frmDailyVoucherEntryBed(CommonFunctions.eScreenID.DailyVoucherEntryLocker);
            frmDailyVoucherEntryLocker.StartPosition = FormStartPosition.CenterParent;
            frmDailyVoucherEntryLocker.MdiParent = this;
            frmDailyVoucherEntryLocker.WindowState = FormWindowState.Maximized;
            frmDailyVoucherEntryLocker.Show();
        }

        private void outOfOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDmLockerList = new frmDmLockerList();
            frmDmLockerList.StartPosition = FormStartPosition.CenterParent;
            frmDmLockerList.MdiParent = this;
            frmDmLockerList.WindowState = FormWindowState.Maximized;
            frmDmLockerList.Show();
        }

        private void lockerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmLockChkOutWrng = new frmLockChkOutWrng(eScreenID.LockerCheckoutWarning);
            frmLockChkOutWrng.StartPosition = FormStartPosition.CenterParent;
            frmLockChkOutWrng.MdiParent = this;
            frmLockChkOutWrng.WindowState = FormWindowState.Maximized;
            frmLockChkOutWrng.Show();
        }

        private void moreThan3DayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMoreThan3Day = new frmLockChkOutWrng(eScreenID.LockMoreThen3Day);
            frmMoreThan3Day.StartPosition = FormStartPosition.CenterParent;
            frmMoreThan3Day.MdiParent = this;
            frmMoreThan3Day.WindowState = FormWindowState.Maximized;
            frmMoreThan3Day.Show();
        }

        private void addDamagedLockerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDamagedLokers = new frmDamagedLokers();
            frmDamagedLokers.StartPosition = FormStartPosition.CenterParent;
            frmDamagedLokers.MdiParent = this;
            frmDamagedLokers.WindowState = FormWindowState.Maximized;
            frmDamagedLokers.Show();
        }

        private void lockerAdvanceVoucherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLockerAdvanceVoucher = new frmMessReport(eScreenID.lockeradvancevouchure);
            frmLockerAdvanceVoucher.StartPosition = FormStartPosition.CenterParent;
            frmLockerAdvanceVoucher.MdiParent = this;
            frmLockerAdvanceVoucher.WindowState = FormWindowState.Maximized;
            frmLockerAdvanceVoucher.Show();
        }

        private void dailyVoucherEntryBedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDailyVoucherEntryBed = new frmDailyVoucherEntryBed(CommonFunctions.eScreenID.DailyVoucherEntryBed);
            frmDailyVoucherEntryBed.StartPosition = FormStartPosition.CenterParent;
            frmDailyVoucherEntryBed.MdiParent = this;
            frmDailyVoucherEntryBed.WindowState = FormWindowState.Maximized;
            frmDailyVoucherEntryBed.Show();
        }

        private void bedCheckInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBedCheckIN = new FrmBedCheckIN(CommonFunctions.eScreenID.DailyVoucherEntryBed);
            FrmBedCheckIN.StartPosition = FormStartPosition.CenterParent;
            FrmBedCheckIN.MdiParent = this;
            FrmBedCheckIN.WindowState = FormWindowState.Maximized;
            FrmBedCheckIN.Show();
        }

        private void bedCheckOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBedCheckOut = new FrmBedCheckOut(eScreenID.BedCheckOut);
            FrmBedCheckOut.StartPosition = FormStartPosition.CenterParent;
            FrmBedCheckOut.MdiParent = this;
            FrmBedCheckOut.WindowState = FormWindowState.Maximized;
            FrmBedCheckOut.Show();
        }

        private void bedOccupiedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBedO = new FrmBedO(eScreenID.BedOccupied);
            FrmBedO.StartPosition = FormStartPosition.CenterParent;
            FrmBedO.MdiParent = this;
            FrmBedO.WindowState = FormWindowState.Maximized;
            FrmBedO.Show();
        }

        private void bedAdvanceVoucherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBedAdvanceVoucher = new frmMessReport(eScreenID.BEDRoomAdvanceVoucher);
            frmBedAdvanceVoucher.StartPosition = FormStartPosition.CenterParent;
            frmBedAdvanceVoucher.MdiParent = this;
            frmBedAdvanceVoucher.WindowState = FormWindowState.Maximized;
            frmBedAdvanceVoucher.Show();
        }

        private void roomCheckInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRoomCheckIn = new frmRoomCheckIn();
            frmRoomCheckIn.StartPosition = FormStartPosition.CenterParent;
            frmRoomCheckIn.MdiParent = this;
            frmRoomCheckIn.WindowState = FormWindowState.Maximized;
            frmRoomCheckIn.Show();
        }

        private void roomOutTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRoomCheckOut = new frmRoomCheckOut(eScreenID.RoomCheckOut);
            frmRoomCheckOut.StartPosition = FormStartPosition.CenterParent;
            frmRoomCheckOut.MdiParent = this;
            frmRoomCheckOut.WindowState = FormWindowState.Maximized;
            frmRoomCheckOut.Show();
        }

        private void occupiedRoomsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRoomOccupied = new frmRoomOccupied();
            frmRoomOccupied.StartPosition = FormStartPosition.CenterParent;
            frmRoomOccupied.MdiParent = this;
            frmRoomOccupied.WindowState = FormWindowState.Maximized;
            frmRoomOccupied.Show();
        }

        private void roomChangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRoomChange = new frmRoomChange(eScreenID.RoomChange);
            frmRoomChange.StartPosition = FormStartPosition.CenterParent;
            frmRoomChange.MdiParent = this;
            frmRoomChange.WindowState = FormWindowState.Maximized;
            frmRoomChange.Show();
        }

        private void dailyVoucherEntryBhaktNiwasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dailyVoucherEntryBhaktNiwas = new frmDailyVoucherEntryBed(eScreenID.DailyVoucherEntryBhaktniwas);
            dailyVoucherEntryBhaktNiwas.StartPosition = FormStartPosition.CenterParent;
            dailyVoucherEntryBhaktNiwas.MdiParent = this;
            dailyVoucherEntryBhaktNiwas.WindowState = FormWindowState.Maximized;
            dailyVoucherEntryBhaktNiwas.Show();
        }

        private void roomOutOfOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDmRoomList = new frmDmRoomList();
            frmDmRoomList.StartPosition = FormStartPosition.CenterParent;
            frmDmRoomList.MdiParent = this;
            frmDmRoomList.WindowState = FormWindowState.Maximized;
            frmDmRoomList.Show();
        }

        private void addDamagedRoomsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRoomDamaged = new frmRoomDamaged();
            frmRoomDamaged.StartPosition = FormStartPosition.CenterParent;
            frmRoomDamaged.MdiParent = this;
            frmRoomDamaged.WindowState = FormWindowState.Maximized;
            frmRoomDamaged.Show();
        }

        private void roomOutTimeBeforeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRoomTimeBefore = new frmRoomCheckOut(eScreenID.RoomCheckOutBefore);
            frmRoomTimeBefore.StartPosition = FormStartPosition.CenterParent;
            frmRoomTimeBefore.MdiParent = this;
            frmRoomTimeBefore.WindowState = FormWindowState.Maximized;
            frmRoomTimeBefore.Show();
        }

        private void roomOutTimeWarningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRoomChkOutWrng = new frmRoomChkOutWrng();
            frmRoomChkOutWrng.StartPosition = FormStartPosition.CenterParent;
            frmRoomChkOutWrng.MdiParent = this;
            frmRoomChkOutWrng.WindowState = FormWindowState.Maximized;
            frmRoomChkOutWrng.Show();
        }

        private void moreThan3DaysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRoomMoreThan3D = new frmRoomMoreThan3D();
            frmRoomMoreThan3D.StartPosition = FormStartPosition.CenterParent;
            frmRoomMoreThan3D.MdiParent = this;
            frmRoomMoreThan3D.WindowState = FormWindowState.Maximized;
            frmRoomMoreThan3D.Show();
        }

        private void roomLockedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRoomLocked = new frmRoomLocked();
            frmRoomLocked.StartPosition = FormStartPosition.CenterParent;
            frmRoomLocked.MdiParent = this;
            frmRoomLocked.WindowState = FormWindowState.Maximized;
            frmRoomLocked.Show();
        }

        private void roomInTimeOnlineNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRoomCheckinOnline = new frmRoomCheckinOnline();
            frmRoomCheckinOnline.StartPosition = FormStartPosition.CenterParent;
            frmRoomCheckinOnline.MdiParent = this;
            frmRoomCheckinOnline.WindowState = FormWindowState.Maximized;
            frmRoomCheckinOnline.Show();
        }

        private void availableRoomsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRoomList = new frmRoomList(eScreenID.RoomAvailable);
            frmRoomList.StartPosition = FormStartPosition.CenterParent;
            frmRoomList.MdiParent = this;
            frmRoomList.WindowState = FormWindowState.Maximized;
            frmRoomList.Show();
        }

        private void checqueDengiReceiptVoucherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMessReportR1 = new frmMessReport(eScreenID.ChequeDengiReceiptVoucher);
            frmMessReportR1.StartPosition = FormStartPosition.CenterParent;
            frmMessReportR1.MdiParent = this;
            frmMessReportR1.WindowState = FormWindowState.Maximized;
            frmMessReportR1.Show();
        }

        private void dailyDengiReceiptVoucherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMessReportR2 = new frmMessReport(eScreenID.DailyDengi);
            frmMessReportR2.StartPosition = FormStartPosition.CenterParent;
            frmMessReportR2.MdiParent = this;
            frmMessReportR2.WindowState = FormWindowState.Maximized;
            frmMessReportR2.Show();
        }

        private void gameBhevastuUserwiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMessReportR3 = new frmMessReport(eScreenID.GameUserWise);
            frmMessReportR3.StartPosition = FormStartPosition.CenterParent;
            frmMessReportR3.MdiParent = this;
            frmMessReportR3.WindowState = FormWindowState.Maximized;
            frmMessReportR3.Show();
        }

        private void dengiBhetvastuUserwiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMessReportR4 = new frmMessReport(eScreenID.DengiBhetvastuUserwise);
            frmMessReportR4.StartPosition = FormStartPosition.CenterParent;
            frmMessReportR4.MdiParent = this;
            frmMessReportR4.WindowState = FormWindowState.Maximized;
            frmMessReportR4.Show();
        }

        private void gameDailyReceiptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMessReportR5 = new frmMessReport(eScreenID.GameDailyReceipt);
            frmMessReportR5.StartPosition = FormStartPosition.CenterParent;
            frmMessReportR5.MdiParent = this;
            frmMessReportR5.WindowState = FormWindowState.Maximized;
            frmMessReportR5.Show();
        }

        private void userWizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMessReportR6 = new frmMessReport(eScreenID.UserWize);
            frmMessReportR6.StartPosition = FormStartPosition.CenterParent;
            frmMessReportR6.MdiParent = this;
            frmMessReportR6.WindowState = FormWindowState.Maximized;
            frmMessReportR6.Show();
        }

        private void toyTrainUserWiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMessReportR7 = new frmMessReport(eScreenID.ToyTrainUserWise);
            frmMessReportR7.StartPosition = FormStartPosition.CenterParent;
            frmMessReportR7.MdiParent = this;
            frmMessReportR7.WindowState = FormWindowState.Maximized;
            frmMessReportR7.Show();
        }
    }
}
