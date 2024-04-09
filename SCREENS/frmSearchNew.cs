using Microsoft.Reporting.Map.WebForms.BingMaps;
using SGMOSOL.ADMIN;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SGMOSOL.ADMIN.CommonFunctions;

namespace SGMOSOL.SCREENS
{
    public partial class frmSearchNew : Form
    {
        CommonFunctions CF = new CommonFunctions();
        private string mStrTableName;
        public long mLngSearchId;
        public int mIntCtrMachId;
        private int mscreenid;
        private int mscreenid2;
        private string PrintReceiptLocName;
        private int PrintReceiptLocId;
        //private System.Text.StringBuilder strSQL = new System.Text.StringBuilder();
        //private OSOL_ADMIN.clsDsCommon mClsDsCom = new OSOL_ADMIN.clsDsCommon();
        private string _p1;
        private int _p2;

        public frmSearchNew()
        {
            InitializeComponent();
        }
        public frmSearchNew(string strTableName, bool txtNameView, eModType BedSystem)
        {
            InitializeComponent();
            mStrTableName = strTableName;
            txtName.Visible = txtNameView;//IF FRMSearch then Ans False
        }
        private void frmSearchNew_Load(System.Object sender, System.EventArgs e)
        {
            //SetGridScreen();
            CF.fncSetDateAndRange(dtpFromDate);
            CF.fncSetDateAndRange(dtpToDate);
            FillCounter();
            btnLoad_Click(null, null);
        }
        private void btnLoad_Click(System.Object sender, System.EventArgs e)
        {
            //OSOL_CONNECTION.clsConnection objcon = new OSOL_CONNECTION.clsConnection();
            //DataSet ds;
            string strFromDate;
            string strToDate;
            string strName;
            int i;
            mscreenid = (int)eScreenID.SaleItemReceipt;
            strFromDate = FormatDateToString(dtpFromDate.Value);
            strToDate = FormatDateToString(dtpToDate.Value);
            strName = txtName.Text;

            DataTable ds = CF.GetDsSearchDataNew(mStrTableName, strFromDate, strToDate, strName, mIntCtrMachId, PrintReceiptLocId, UserInfo.UserId);


            fpsSearch.DataSource = ds;
            if (fpsSearch.DataSource != null)
            {
                //var withBlock = fpsSearch;
                fpsSearch.Columns[0].Visible = false;
                fpsSearch.Columns[1].Visible = false;
                fpsSearch.Columns[2].Visible = false;
                fpsSearch.Columns[3].Visible = false;
                fpsSearch.Columns[4].Visible = false;
                fpsSearch.Columns[5].Visible = false;
                for (i = 0; i <= fpsSearch.Columns.Count - 1; i++)
                    // .Columns(i).Width = 100 '--.Columns(i).GetPreferredWidth()
                    // .Columns(i).AllowAutoFilter = True
                    fpsSearch.Columns[i].Name = fpsSearch.Columns[i].Name.Replace("_", " ");
            }
            if (ds != null && ds.Rows.Count > 0)
            {
                CF.LockUnLockGrid(0, fpsSearch.RowCount - 1, 0, fpsSearch.ColumnCount - 1, fpsSearch, true);
            }

        }
        private void FillCounter()
        {
            DataTable dr;
            dr = CF.GetDrCounterMachId(UserInfo.UserId, SystemHDDModelNo, SystemHDDSerialNo, SystemMacID, 0, mIntCtrMachId);
            if (dr.Rows.Count > 0)
            {
                PrintReceiptLocName = dr.Rows[0]["LocName"].ToString();
                PrintReceiptLocId = (int)dr.Rows[0]["LocId"];
            }
            //dr.Close();
        }
        private void btnOK_Click(System.Object sender, System.EventArgs e)
        {
                    var withBlock = fpsSearch;
            if (withBlock.Rows.Count > 0)
            {
                {
                    try
                    {
                        mLngSearchId = Convert.ToInt64(withBlock.Rows[withBlock.CurrentCell.RowIndex].Cells[0].Value);
                    }
                    catch (Exception ex)
                    {
                        mLngSearchId = 0;
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select atlist one record!.");
            }
        }

        private void fpsSearch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (fpsSearch.Rows[e.RowIndex].Cells[0].Value != DBNull.Value)
            {
                mLngSearchId = Convert.ToInt64(fpsSearch.Rows[e.RowIndex].Cells[0].Value);
                this.Close();
            }
        }

        //private void SetGridScreen()
        //{
        //    FarPoint.Win.Spread.InputMap inputmap1 = new FarPoint.Win.Spread.InputMap();
        //    FarPoint.Win.Spread.FpSpread FpSpread;
        //    // -- Set object equal to existing map
        //    FpSpread = fpsSearch;
        //    inputmap1 = FpSpread.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
        //    // -- Map Enter key
        //    inputmap1.Put(new FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap);
        //    // -- Create InputMap object
        //    FarPoint.Win.Spread.InputMap inputmap2 = new FarPoint.Win.Spread.InputMap();
        //    // -- Set object equal to existing map
        //    inputmap2 = FpSpread.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused);
        //    // -- Map Enter key
        //    inputmap2.Put(new FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap);
        //    // -- Map Tab key 
        //    inputmap1.Put(new FarPoint.Win.Spread.Keystroke(Keys.Tab, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
        //    // -- set Tab key to move to next control
        //    inputmap1 = FpSpread.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
        //    // -- set shift + Tab key to move to previous control
        //    inputmap1.Put(new FarPoint.Win.Spread.Keystroke(Keys.Tab, Keys.Shift), FarPoint.Win.Spread.SpreadActions.None);
        //    // -- Create InputMap object
        //    FarPoint.Win.Spread.InputMap inputmap3 = new FarPoint.Win.Spread.InputMap();
        //    // -- Set object equal to existing map
        //    inputmap3 = FpSpread.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused);
        //    // -- Map F2 key to edit the enabled cells
        //    inputmap3.Put(new FarPoint.Win.Spread.Keystroke(Keys.F2, Keys.None), FarPoint.Win.Spread.SpreadActions.StartEditing);
        //}
    }
}
