using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SGMOSOL.ADMIN.CommonFunctions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
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


namespace SGMOSOL.SCREENS.Locker
{
    public partial class frmOccupiedLockers : Form
    {
        private string mStrTableName;
        public long mLngSearchId;
        public Int16 mIntCtrMachId;
        private Int32 mscreenid;
        private Int32 mscreenid2;
        private int lngLocid;
        private System.Text.StringBuilder strSQL = new System.Text.StringBuilder();
        private CommonFunctions mClsDsCom = new CommonFunctions();
        private LockerMasterDAL objDsLockerMst = new LockerMasterDAL();

        public frmOccupiedLockers()
        {
            InitializeComponent();
        }
        private void frmOccupiedLockers_Load(System.Object sender, System.EventArgs e)
        {
            ScreenToCenter();
            txtUser.Text = UserInfo.UserName;
            FillCounter();
            FillLockers();
            FillGridView();
        }

        private void FillCounter()
        {
            try
            {
                DataTable dr;
                dr = mClsDsCom.GetDrCounterMachId(UserInfo.UserId, SystemHDDModelNo, SystemHDDSerialNo, SystemMacID, Convert.ToInt16(eModType.Locker));
                if (dr.Rows.Count > 0)
                {
                    txtCounter.Text = dr.Rows[0]["CounterMachineTitle"].ToString();
                    txtCounter.Tag = dr.Rows[0]["CtrMachId"];
                    lngLocid = Convert.ToInt32(dr.Rows[0]["LocId"]);
                }
                //dr.Close();
            }catch (Exception ex) { mClsDsCom.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version); }
        }

        private void ScreenToCenter()
        {
            //Int32 ctr;
            //ctr = (MDI.Size.Width - this.Size.Width) / (double)2;
            //this.Location = new Point(ctr, 0);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }

        private void FillLockers()
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            Int32 ctr;
            Int32 row = 0;
            Int32 col = 0;
            string Filtercriteria = "";
            try
            {
                ds = objDsLockerMst.GetDsLockerDetails(lngLocid, (int)eTokenDetail.StatusActive, (int)eTokenDetail.StatusNo);

                ctr = ds.Tables[0].Rows.Count;
                txtCtOfLockers.Text = ctr.ToString();
            }
            catch (Exception ex)
            {
                mClsDsCom.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
        }

        private void FillGridView()
        {
            System.Data.DataSet ds;
            try
            {
                ds = objDsLockerMst.GetDataForGrid(lngLocid);
                gvOccLockers.DataSource = ds.Tables[0];




                gvOccLockers.Columns[0].Width = 80;
                gvOccLockers.Columns[1].Width = 100;
                gvOccLockers.Columns[2].Width = 200;
                gvOccLockers.Columns[3].Width = 100;
                gvOccLockers.Columns[4].Width = 100;
                gvOccLockers.Columns[5].Width = 100;
                gvOccLockers.Columns[6].Width = 100;


                gvOccLockers.Columns[0].HeaderText = "Rec.No.";
                gvOccLockers.Columns[1].HeaderText = "Locker";
                gvOccLockers.Columns[2].HeaderText = "Bhakt Name ";
                gvOccLockers.Columns[3].HeaderText = "In Date ";
                gvOccLockers.Columns[4].HeaderText = "Out Date";
                gvOccLockers.Columns[5].HeaderText = "Mobile No.";
                gvOccLockers.Columns[6].HeaderText = "City";
            }
            catch (Exception ex)
            {
                mClsDsCom.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
        }
    }

}
