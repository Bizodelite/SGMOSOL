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
using SGMOSOL.DAL.BhaktNiwas;

namespace SGMOSOL.SCREENS.BhaktNiwas
{
    public partial class frmDmRoomList : Form
    {
        private long mScreenID;
        private bool mBlnEdit = false;
        private eAction mAction;        // Reference to Enum of Type Action for ActionView,ActionNew,ActionUpdate and ActionDelete
        private ArrayList CtrlArr = new ArrayList(); // To insert Form Control in Array List
        private ArrayList btnArr = new ArrayList();
        //private OSOL_ADMIN.clsDsCommon mClsDsCom = new OSOL_ADMIN.clsDsCommon();
        private RoomMasterDAL objDsLockerMst = new RoomMasterDAL();
        private int RoomLocID;
        private Int16 flag;
        CommonFunctions cf = new CommonFunctions();
        public frmDmRoomList()
        {
            InitializeComponent();
        }

        private void frmDmRoomList_Load(object sender, System.EventArgs e)
        {
            // Me.WindowState = FormWindowState.Maximized
            cf.setControlsonForm(this, CtrlArr, btnArr);
            cf.SetUserScreenActions(this, UserInfo.UserId, (int)eScreenID.RoomCheckIn, btnArr, null, mBlnEdit);
            ScreenToCenter();
            flag = 0;
            txtUser.Text = UserInfo.UserName;
            FillCounter();
            FillGridView();
            txtRoomsCt.Text = gvDamagedRooms.RowCount.ToString();
        }

        public void FillGridView()
        {
            System.Data.DataSet ds;
            try
            {
                ds = objDsLockerMst.GetDmgedRoomsForGrid(Convert.ToInt32(txtCounter.Tag), RoomLocID);
                gvDamagedRooms.DataSource = ds.Tables[0];

                gvDamagedRooms.Columns[0].Width = 150;
                gvDamagedRooms.Columns[1].Width = 100;
                gvDamagedRooms.Columns[2].Width = 100;
                gvDamagedRooms.Columns[3].Width = 100;
                gvDamagedRooms.Columns[4].Width = 300;

                gvDamagedRooms.Columns[0].HeaderText = "Room Name";
                gvDamagedRooms.Columns[1].HeaderText = "Date";
                gvDamagedRooms.Columns[2].HeaderText = "Time";
                gvDamagedRooms.Columns[3].HeaderText = "User";
                gvDamagedRooms.Columns[4].HeaderText = "Reason ";
            }
            catch (Exception ex)
            {
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
            dr = cf.GetDrCounterMachId(UserInfo.UserId, SystemHDDModelNo, SystemHDDSerialNo, SystemMacID, Convert.ToInt16(eModType.BhaktaNiwas));
            if (dr.Rows.Count > 0)
            {
                txtCounter.Text = dr.Rows[0]["CounterMachineTitle"].ToString();

                txtCounter.Tag = dr.Rows[0]["CtrMachId"];
                RoomLocID = Convert.ToInt32(dr.Rows[0]["LocId"]);
            }
        }
    }

}
