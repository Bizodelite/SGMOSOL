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
using System.Data.SqlClient;
using SGMOSOL.ADMIN;
using SGMOSOL.DAL;

namespace SGMOSOL.SCREENS.Locker
{
    public partial class frmDmLockerList : Form
    {
        private long mScreenID;
        private bool mBlnEdit = false;
        private eAction mAction;        // Reference to Enum of Type Action for ActionView,ActionNew,ActionUpdate and ActionDelete
        private ArrayList CtrlArr = new ArrayList(); // To insert Form Control in Array List
        private ArrayList btnArr = new ArrayList();
        private CommonFunctions cf = new CommonFunctions();
        private LockerMasterDAL objDsLockerMst = new LockerMasterDAL();
        private Int16 flag;

        public frmDmLockerList()
        {
            InitializeComponent();
        }

        private void frmDmLockerList_Load(object sender, System.EventArgs e)
        {
            // Me.WindowState = FormWindowState.Maximized
            cf.setControlsonForm(this, CtrlArr, btnArr);
            cf.SetUserScreenActions(this, UserInfo.UserId, (int)eScreenID.LockerCheckIn, btnArr, null, mBlnEdit);
            ScreenToCenter();
            flag = 0;
            txtUser.Text = UserInfo.UserName;
            FillCounter();
            FillGridView();
            txtLockersCt.Text = gvDamagedLkrs.RowCount.ToString();
        }

        public void FillGridView()
        {
            System.Data.DataSet ds;
            try
            {
                ds = objDsLockerMst.GetDmgedLkrsForGrid(txtCounter.Tag);
                gvDamagedLkrs.DataSource = ds.Tables[0];

                gvDamagedLkrs.Columns[0].Width = 150;
                gvDamagedLkrs.Columns[1].Width = 100;
                gvDamagedLkrs.Columns[2].Width = 300;
                gvDamagedLkrs.Columns[0].HeaderText = "Locker Name";
                gvDamagedLkrs.Columns[1].HeaderText = "Date";
                gvDamagedLkrs.Columns[2].HeaderText = "Reason ";
            }

            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
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

                txtCounter.Tag = dr.Rows[0]["CtrMachId"];
            }
            //dr.Close();
        }
    }

}
