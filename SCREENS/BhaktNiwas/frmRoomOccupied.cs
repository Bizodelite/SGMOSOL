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
using System.Web.UI.WebControls.WebParts;
using SGMOSOL.ADMIN;
using static SGMOSOL.ADMIN.CommonFunctions;
using SGMOSOL.DAL.BhaktNiwas;
using System.Runtime.InteropServices;

namespace SGMOSOL.SCREENS.BhaktNiwas
{
    public partial class frmRoomOccupied : Form
    {
        private string mStrTableName;
        public long mLngSearchId;
        public Int16 mIntCtrMachId;
        private Int32 mscreenid;
        private Int32 mscreenid2;
        private Int32 RoomLocID;
        private System.Text.StringBuilder strSQL = new System.Text.StringBuilder();
        //private OSOL_ADMIN.clsDsCommon mClsDsCom = new OSOL_ADMIN.clsDsCommon();
        private RoomMasterDAL objDsRoomMst = new RoomMasterDAL();
        CommonFunctions cf = new CommonFunctions();
        public frmRoomOccupied()
        {
            InitializeComponent();
        }
        private void frmRoomOccupied_Load(System.Object sender, System.EventArgs e)
        {
            ScreenToCenter();
            txtUser.Text = UserInfo.UserName;

            FillCounter();
            FillSublocation();
            // FillRooms()
            FillGridView();
            txtCtOfRooms.Text = gvOccRooms.Rows.Count.ToString();
        }
        private void FillSublocation()
        {
            System.Data.DataTable dr;
            dr = cf.GetDrSublocation(7, RoomLocID);
            cf.FillCombo(cmbBhaktaNiwas, dr, "Department", "Department_Id");
            if (cmbBhaktaNiwas.Items.Count > 0)
            {
                cmbBhaktaNiwas.SelectedIndex = -1;
                cmbBhaktaNiwas.SelectedText = "All";
            }
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
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }

        private void FillGridView()
        {
            Int64 Deptid;
            System.Data.DataSet ds = new System.Data.DataSet();
            int ctr;
            try
            {
                Deptid = cf.cmbItemdata(cmbBhaktaNiwas, cmbBhaktaNiwas.SelectedIndex);
                ds = objDsRoomMst.GetDataForGrid(Deptid, RoomLocID);
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
                ctr = ds.Tables[0].Rows.Count;
                txtCtOfRooms.Text = ctr.ToString();
            }
            catch (Exception ex)
            {
            }
        }

        private void cmbBhaktaNiwas_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            // FillRooms()
            FillGridView();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel (.xlsx)|  *.xlsx";
            sfd.FileName = this.Text + " report.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                try
                {
                    app.Visible = false;
                    worksheet = workbook.ActiveSheet;
                    //worksheet.Name = "gridview";
                    for (int i = 1; i < gvOccRooms.Columns.Count + 1; i++)
                    {
                        worksheet.Cells[1, i] = gvOccRooms.Columns[i - 1].HeaderText;
                    }
                    for (int i = 0; i < gvOccRooms.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < gvOccRooms.Columns.Count; j++)
                        {
                            worksheet.Cells[i + 2, j + 1] = gvOccRooms.Rows[i].Cells[j].Value.ToString();
                        }
                    }
                    workbook.SaveAs(sfd.FileName);
                    app.Quit();
                }
                catch (Exception ex)
                {
                    app.Quit();
                }
            }
        }
    }

}
