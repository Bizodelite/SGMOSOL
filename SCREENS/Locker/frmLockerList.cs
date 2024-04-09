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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using Microsoft.VisualBasic;
using SGMOSOL.ADMIN;
using System.Data.SqlClient;
using SGMOSOL.DAL;

namespace SGMOSOL.SCREENS.Locker
{
    public partial class frmLockerList : Form
    {
        private eScreenID mScreenID;
        private int lngLocid;
        private bool mBlnEdit = false;
        private Action mAction;        // Reference to Enum of Type Action for ActionView,ActionNew,ActionUpdate and ActionDelete
        private ArrayList CtrlArr = new ArrayList(); // To insert Form Control in Array List
        private ArrayList btnArr = new ArrayList();
        private CommonFunctions cf = new CommonFunctions();
        private LockerMasterDAL objDsLockerMst = new LockerMasterDAL();

        public frmLockerList(eScreenID ScreenID)
        {
            InitializeComponent();
            mScreenID = ScreenID;
            if (mScreenID == eScreenID.LockerAvailable)
                this.Text = "Locker Available";
            else
                this.Text = "Locker Occupied";
        }

        private void frmLockerList_Load(object sender, System.EventArgs e)
        {
            // Me.WindowState = FormWindowState.Maximized
            cf.setControlsonForm(this, CtrlArr, btnArr);
            cf.SetUserScreenActions(this, UserInfo.UserId, (int)eScreenID.LockerCheckIn, btnArr, null, mBlnEdit);
            ScreenToCenter();

            txtUser.Text = UserInfo.UserName;
            FillCounter();
            FillLockers();
        }

        private void ScreenToCenter()
        {
            //Int32 ctr;
            //ctr = (MDI.Size.Width - this.Size.Width) / (double)2;
            //this.Location = new Point(ctr, 0);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;// Windows.Forms.FormBorderStyle.FixedToolWindow;
        }
        private void FillCounter()
        {
            try
            {
                DataTable dr;
                dr = cf.GetDrCounterMachId(UserInfo.UserId, SystemHDDModelNo, SystemHDDSerialNo, SystemMacID, Convert.ToInt16(eModType.Locker));
                if (dr.Rows.Count > 0)
                {
                    txtCounter.Text = dr.Rows[0]["CounterMachineTitle"].ToString();
                    txtCounter.Tag = dr.Rows[0]["CtrMachId"];
                    lngLocid = Convert.ToInt32(dr.Rows[0]["LocId"]);
                }
                //dr.Close();
            }catch (Exception ex) { cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version); }
        }

        private void FillLockers()
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            Int32 ctr;
            Int32 row = 0;
            Int32 col = 0;
            DataView Dv;
            string Filtercriteria = "";
            try
            {
                if (mScreenID == eScreenID.LockerAvailable)
                {
                    ds = objDsLockerMst.GetDsLockerDetails(lngLocid, (int)eTokenDetail.StatusActive, (int)eTokenDetail.StatusYes);
                }
                else
                {
                    lbCtOfLkrs.Text = "No.Of Occupied Lockers";
                    ds = objDsLockerMst.GetDsLockerDetails(lngLocid, (int)eTokenDetail.StatusActive, (int)eTokenDetail.StatusNo);
                }
                ctr = ds.Tables[0].Rows.Count;
                txtCtOfLockers.Text = ctr.ToString();
                if (ctr == 0)
                    return;
                ctr = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(ds.Tables[0].Rows.Count) / (decimal)4));
                Dv = new DataView(ds.Tables[0], Filtercriteria, "", DataViewRowState.CurrentRows);
                fpsLockers.RowCount = ctr;
                {
                    var withBlock = fpsLockers;
                    row = 0;
                    foreach (DataRowView Drv in Dv)
                    {
                        withBlock.Rows[row].Cells[col].Value = Drv["LockerName"];
                        row = row + 1;
                        if (row >= ctr)
                        {
                            row = 0;
                            col = col + 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
        }

    }
}
