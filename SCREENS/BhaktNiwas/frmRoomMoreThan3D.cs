using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using Microsoft.VisualBasic;
using SGMOSOL.ADMIN;
using SGMOSOL.DAL.BhaktNiwas;
using static SGMOSOL.ADMIN.CommonFunctions;

namespace SGMOSOL.SCREENS.BhaktNiwas
{
    public partial class frmRoomMoreThan3D : Form
    {
        private string mStrTableName;
        public long mLngSearchId;
        public Int16 mIntCtrMachId;
        private Int32 mscreenid;
        private Int32 mscreenid2;
        private int RoomLocID;
        private System.Text.StringBuilder strSQL = new System.Text.StringBuilder();
        private CommonFunctions cf = new CommonFunctions();
        private RoomMasterDAL objDsRoomMst = new RoomMasterDAL();

        public frmRoomMoreThan3D()
        {
            InitializeComponent();
        }

        private void frmRoomMoreThan3D_Load(System.Object sender, System.EventArgs e)
        {
            double TotalAmt = 0;
            cf.fncSetDateAndRange(dtpDate);
            ScreenToCenter();
            txtUser.Text = UserInfo.UserName;
            FillCounter();
            FillGridView();
            for (var i = 0; i <= gvOccRooms.RowCount - 1; i++)
                TotalAmt += Convert.ToDouble(gvOccRooms.Rows[i].Cells[8].Value);
            txtTotal.Text = TotalAmt.ToString();
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

        private void ScreenToCenter()
        {
            //Int32 ctr;
            //ctr = (MDI.Size.Width - this.Size.Width) / (double)2;
            //this.Location = new Point(ctr, 0);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }



        private void FillGridView()
        {
            var strDate = FormatDateToString(dtpDate.Value);
            System.Data.DataSet ds;
            try
            {
                ds = objDsRoomMst.GetMoreThan3Days(Convert.ToInt32(txtCounter.Tag), strDate, RoomLocID);
                gvOccRooms.DataSource = ds.Tables[0];

                gvOccRooms.Columns[0].HeaderText = "Rec.No.";
                gvOccRooms.Columns[1].HeaderText = "BN. No.";
                gvOccRooms.Columns[2].HeaderText = "Room No.";
                gvOccRooms.Columns[3].HeaderText = "Bhakt Name";
                gvOccRooms.Columns[4].HeaderText = "MOBILE ";
                gvOccRooms.Columns[5].HeaderText = "IN Date ";
                gvOccRooms.Columns[6].HeaderText = "IN Time ";
                gvOccRooms.Columns[7].HeaderText = "DAYS ";
                gvOccRooms.Columns[8].HeaderText = "Amount";
            }
            catch (Exception ex)
            {
            }
        }

        private void gvOccRooms_CellContentClick(System.Object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
        }
    }

}
