using Microsoft.Office.Interop.Excel;
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
using SGMOSOL.DAL;
using System.Data.SqlClient;
using static SGMOSOL.BAL.LockerBAL;


namespace SGMOSOL.SCREENS.Locker
{
    public partial class frmDamagedLokers : Form
    {
        private long mScreenID;
        private bool mBlnEdit = false;
        private eAction mAction;        // Reference to Enum of Type Action for ActionView,ActionNew,ActionUpdate and ActionDelete
        private ArrayList CtrlArr = new ArrayList(); // To insert Form Control in Array List
        private ArrayList btnArr = new ArrayList();
        private CommonFunctions cf = new CommonFunctions();
        private LockerMasterDAL objDsLockerMst = new LockerMasterDAL();
        private Int16 flag;

        public frmDamagedLokers()
        {
            InitializeComponent();
        }

        private void frmDamagedLokers_Load(object sender, System.EventArgs e)
        {
            // Me.WindowState = FormWindowState.Maximized
            dtpDate.Value = DateTime.Now;
            cf.setControlsonForm(this, CtrlArr, btnArr);
            cf.SetUserScreenActions(this, UserInfo.UserId, (int)eScreenID.LockerCheckIn, btnArr, null, mBlnEdit);
            ScreenToCenter();
            flag = 0;
            txtUser.Text = UserInfo.UserName;
            FillCounter();
            FillAvailableLockers();
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
            System.Data.DataTable dr;
            dr = cf.GetDrCounterMachId(UserInfo.UserId, SystemHDDModelNo, SystemHDDSerialNo, SystemMacID, Convert.ToInt16(eModType.Locker));
            if (dr.Rows.Count > 0)
            {
                txtCounter.Text = dr.Rows[0]["CounterMachineTitle"].ToString();

                txtCounter.Tag = dr.Rows[0]["LocId"];
            }
            //dr.Close();
        }



        private void btnSave_Click(System.Object sender, System.EventArgs e)
        {
            frmDmLockerList objfrmDmLockerList = new frmDmLockerList();
            DamagedLockers objDamagedLkrs;
            // OSOL_CONNECTION.clsConnection.glbTransaction = OSOL_CONNECTION.clsConnection.glbCon.BeginTransaction
            try
            {
                long lngErrNo;

                for (int i = 0; i <= LockerListBox.Items.Count - 1; i++)
                {
                    if (LockerListBox.GetItemChecked(i))
                    {
                        objDamagedLkrs = GetData(i);
                        if (flag == 0)
                        {
                            lngErrNo = objDsLockerMst.UpdateLockerMst(objDamagedLkrs.LockerId, (int)eTokenDetail.StatusInActive, (int)eTokenDetail.Damaged);
                            if (lngErrNo >= 0)
                                lngErrNo = objDsLockerMst.Insert(objDamagedLkrs);
                        }
                        else if (flag == 1)
                        {
                            lngErrNo = objDsLockerMst.UpdateLockerMst(objDamagedLkrs.LockerId, (int)eTokenDetail.StatusActive, (int)eTokenDetail.StatusYes);
                            if (lngErrNo >= 0)
                                lngErrNo = objDsLockerMst.Delete(objDamagedLkrs);
                        }
                    }
                }




                MessageBox.Show("Locker Updated Successfully!", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            finally
            {
                LockerListBox.Refresh();
            }
        }

        private void btnClose_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void FillAvailableLockers()
        {
            System.Data.DataTable dr = null;
            try
            {
                dr = objDsLockerMst.GetDrLockerDetails(Convert.ToInt32(txtCounter.Tag), (int)eTokenDetail.StatusActive, (int)eTokenDetail.StatusYes);

                cf.FillListBox(LockerListBox, dr, "LockerName", "LockerId", "RecordModifiedCount");
            }
            catch (Exception ex)
            {
                //if (dr != null)
                //    dr.Close();
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
        }

        private void cbReparedLkr_Click(System.Object sender, System.EventArgs e)
        {
            if (cbReparedLkr.Checked)
            {
                flag = 1;
                txtReason.Visible = false;
                lbReason.Visible = false;
                Label1.Text = "Select Repaired Lockers";
                FillDamagedLockers();
            }
            else
            {
                flag = 0;
                txtReason.Visible = true;
                lbReason.Visible = true;
                Label1.Text = "Select Damaged Lockers";
                FillAvailableLockers();
            }
        }

        private void FillDamagedLockers()
        {
            System.Data.DataTable dr = null;
            try
            {
                dr = objDsLockerMst.GetDrLockerDetails(Convert.ToInt32(txtCounter.Tag), (int)eTokenDetail.StatusInActive, (int)eTokenDetail.Damaged);

                cf.FillListBox(LockerListBox, dr, "LockerName", "LockerId", "RecordModifiedCount");
            }
            catch (Exception ex)
            {
                //if (dr != null)
                //    dr.Close();
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
        }

        private DamagedLockers GetData(int i)
        {
            DamagedLockers objDmLkrs = new DamagedLockers();
            
            objDmLkrs.LockerId = cf.lsbItemData(LockerListBox, i);
            objDmLkrs.Reason = txtReason.Text;
            objDmLkrs.sDate = FormatDateToString(dtpDate.Value);

            return objDmLkrs;
        }








        private void btnRefresh_Click(System.Object sender, System.EventArgs e)
        {
            if (flag == 0)
                FillAvailableLockers();
            else
                FillDamagedLockers();
            txtReason.Text = "";
        }
    }

}
