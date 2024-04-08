using SGMOSOL.ADMIN;
using SGMOSOL.BAL;
using SGMOSOL.DAL.Locker;
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
        public MDI()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            cm = new CommonFunctions();
            this.Text = cm.getFormTitle() + " / " + Application.ProductVersion;
        }

        private void MDI_Load(object sender, EventArgs e)
        {
            InitAppParam();
            LoadBedCheckInMaxAmount();
        }
        public void InitAppParam()
        {
            //UserInfo.Rounding = "N";
            //UserInfo.TodaysDate = cm.GetDsSERVERDATE();
            //UserInfo.MaxRowsOfFindGrid = 300;
            //UserInfo.DengiAlertAmt = 5000;
            //UserInfo.InstrumentDateDays = 30;
            UserInfo.ReportPath = Application.StartupPath.Replace("bin\\Debug","") + "Reports\\";
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
        private void dengiToolStripMenuItem_Click(object sender, EventArgs e)
        {

            
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChnagePassword = Application.OpenForms.OfType<frmChnagePassword>().FirstOrDefault();
            if (frmChnagePassword == null)
            {
                frmChnagePassword = new frmChnagePassword();
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
            Application.Exit();
            System.Diagnostics.Process.Start(Application.ExecutablePath);
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
    }
}
