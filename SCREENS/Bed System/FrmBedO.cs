using Microsoft.Office.Interop.Excel;
using Microsoft.Reporting.Map.WebForms.BingMaps;
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
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using Microsoft.VisualBasic;
using static SGMOSOL.ADMIN.CommonFunctions;
using SGMOSOL.ADMIN;
using static SGMOSOL.BAL.BadBAL;
using System.Web.Services.Description;
using SGMOSOL.DAL;

namespace SGMOSOL.SCREENS.Bed_System
{
    public partial class FrmBedO : Form
    {
        private eScreenID mScreenID;
        private int rbFlag;
        private bool mBlnEdit = false;
        private eAction mAction;        // Reference to Enum of Type Action for ActionView,ActionNew,ActionUpdate and ActionDelete
        private ArrayList CtrlArr = new ArrayList(); // To insert Form Control in Array List
        private ArrayList btnArr = new ArrayList();
        //private OSOL_ADMIN.clsDsCommon mClsDsCom = new OSOL_ADMIN.clsDsCommon();
        //private OSOL_BLSDS.clsBlsBedPrintReceipt objBlsInvAlloc = new OSOL_BLSDS.clsBlsBedPrintReceipt();
        //private OSOL_BLSDS.clsDsBedReceipt objDsPrintReceiptationMst = new OSOL_BLSDS.clsDsBedReceipt();
        //private OSOL_BLSDS.clsDsBedReceiptDet objDsMessPrintReceiptDet = new OSOL_BLSDS.clsDsBedReceiptDet();
        private bool DisableSendKeys;

        private bool bkDateEntry = false;
        private bool blnformChange;
        private System.Windows.Forms.ListBox lstItemMaster = new System.Windows.Forms.ListBox();
        private System.Data.DataSet dsItemMaster;
        private string[] col;
        private string[] mStrErrMsg;
        public long mSearchId;
        private Collection mDelColl = new Collection();
        private DateTime dtEnteredOn;
        private string mStrCounterMachineShortName;
        private int PrintReceiptDeptID;
        private string PrintReceiptDeptName;
        private string PrintReceiptLocName;
        private int PrintReceiptLocId;
        private string PrintReceiptitem_names;
        public static string itemnames;
        public static string item_name = "";
        // Dim t1 As New ThreadStart(AddressOf LoadItemMinLevel)
        // Dim objThread As New Thread(t1)
        CommonFunctions cf = new CommonFunctions();
        BedCheckOutDAL BedCheckOutDALobj = new BedCheckOutDAL();
        public FrmBedO(eScreenID pScreenId)
        {
            InitializeComponent();
            mScreenID = pScreenId;
        }
        public enum PrintReceipt
        {
            ProductN,
            ProductC,
            Advance,
            Nidhi,
            Qty,
            Occupied,
            Pending,
            outofbedorder,
        }
        private void FrmBedO_Load(System.Object sender, System.EventArgs e)
        {
            //SetGridScreen();
            cf.setControlsonForm(this, CtrlArr, btnArr);
            cf.SetUserScreenActions(this, UserInfo.UserId, (int)mScreenID, btnArr, null, mBlnEdit);
            cf.SetUserScreenActions(this, UserInfo.UserId, (int)mScreenID, btnArr, null, mBlnEdit);
            ScreenToCenter();


            txtUser.Text = UserInfo.UserName;
            FillCounter();

            if (txtCounter.Tag == null || Convert.ToInt32(txtCounter.Tag) == 0)
            {
                MessageBox.Show("User/Counter/Machine ID assign conflict. Please contact system administrator.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            FillItemMaster();
            LoadTransaction();
        }
        private void ScreenToCenter()
        {
            //Int32 ctr;
            //ctr = (MDI.Size.Width - this.Size.Width) / (double)2;
            //this.Location = new Point(ctr, 0);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }
        //private void SetGridScreen()
        //{
        //    FarPoint.Win.Spread.InputMap inputmap1 = new FarPoint.Win.Spread.InputMap();
        //    FarPoint.Win.Spread.FpSpread FpSpread;
        //    // -- Set object equal to existing map
        //    Int16 ctr;
        //    // For ctr = 1 To 2
        //    // If ctr = 1 Then
        //    // FpSpread = fpsPrintReceipt
        //    // Else
        //    // FpSpread = fpsMinLevel
        //    // End If
        //    FpSpread = fpsPrintReceipt;
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

        //    // Dim sfir As New FarPoint.Win.Spread.SolidFocusIndicatorRenderer(Color.Blue, 2)

        //    // fpsPrintReceipt.FocusRenderer = sfir

        //    // fpsPrintReceipt.FocusRenderer = New MyIndicator()

        //    fpsPrintReceipt.Sheets(0).Columns(PrintReceipt.ProductC).Width = 0;
        //    this.fpsPrintReceipt_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
        //}



        private void FillCounter()
        {
            System.Data.DataTable dr = cf.GetDrCounterMachId(UserInfo.UserId, SystemHDDModelNo, SystemHDDSerialNo, SystemMacID, Convert.ToInt16(eModType.BedSystem));
            if (dr.Rows.Count > 0)
            {
                txtCounter.Text = dr.Rows[0]["CounterMachineTitle"].ToString();
                txtCounter.Tag = dr.Rows[0]["CtrMachId"];
                PrintReceiptDeptID = Convert.ToInt32(dr.Rows[0]["DeptId"]);
                PrintReceiptDeptName = dr.Rows[0]["DepartmentName"].ToString();
                PrintReceiptLocName = dr.Rows[0]["LocName"].ToString();
                PrintReceiptLocId = Convert.ToInt32(dr.Rows[0]["LocId"]);
                mStrCounterMachineShortName = dr.Rows[0]["CounterMachineShortName"].ToString();
            }
        }

        private void FillItemMaster()
        {
            string strDate;
            string strTime = "";

            dsItemMaster = null;
            dsItemMaster = new System.Data.DataSet();
            lstItemMaster.Items.Clear();
            strDate = FormatDateToString(dtpCheckIn.Value);
            try
            {
                dsItemMaster = cf.GetDsProductMenu(UserInfo.UserId);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            if (dsItemMaster.Tables[0].Rows.Count > 0)
                cf.FillComboWithDataSet(lstItemMaster, dsItemMaster.Tables[0], "ItemTitle", "ItemTitle", "ItemId", "ItemCode", "");

            DataGridViewComboBoxCell cboItem = new DataGridViewComboBoxCell();
            cboItem.DataSource = lstItemMaster;
            cboItem.ReadOnly = true;
            fpsPrintReceipt.Columns[(int)PrintReceipt.ProductN].CellTemplate = cboItem;

            //FarPoint.Win.Spread.CellType.ComboBoxCellType cboItem = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            //cboItem.ListControl = lstItemMaster;
            //cboItem.Editable = true;
            //fpsPrintReceipt.Sheets(0).Columns(PrintReceipt.ProductN).CellType = cboItem;
        }
        private void LoadTransaction()
        {
            System.Data.DataTable dr;
            BedCheckInDet PrnRcptDtDet = new BedCheckInDet();
            long lngCtrMachId;
            int bhaktype;
            mDelColl = new Collection();

            try
            {
                dr = BedCheckOutDALobj.GetDrOcc(UserInfo.UserId);
                {
                    var withBlock = fpsPrintReceipt;
                    withBlock.RowCount = 0;
                    withBlock.RowCount = 1;
                    //while (dr.Read)
                    foreach (DataRow drrow in dr.Rows)
                    {
                        withBlock.Rows[withBlock.RowCount - 1].Cells[(int)PrintReceipt.ProductN].Value = drrow["ItemTitle"];
                        withBlock.Rows[withBlock.RowCount - 1].Cells[(int)PrintReceipt.ProductC].ReadOnly = true;
                        withBlock.Rows[withBlock.RowCount - 1].Cells[(int)PrintReceipt.ProductN].Tag = drrow["ItemId"];
                        withBlock.Rows[withBlock.RowCount - 1].Cells[(int)PrintReceipt.ProductC].Value = drrow["ItemCode"];
                        withBlock.Rows[withBlock.RowCount - 1].Cells[(int)PrintReceipt.Qty].Value = (drrow["Qty"]);
                        withBlock.Rows[withBlock.RowCount - 1].Cells[(int)PrintReceipt.Nidhi].Value = (drrow["RENT"]);
                        withBlock.Rows[withBlock.RowCount - 1].Cells[(int)PrintReceipt.Advance].Value = (drrow["ADVANCE"]);
                        withBlock.Rows[withBlock.RowCount - 1].Cells[(int)PrintReceipt.Occupied].Value = (drrow["occupied"]);
                        withBlock.Rows[withBlock.RowCount - 1].Cells[(int)PrintReceipt.Pending].Value = (drrow["pending"]);
                        withBlock.Rows[withBlock.RowCount - 1].Cells[(int)PrintReceipt.outofbedorder].Value = (drrow["OutOFOrderBed"]);
                        mDelColl.Add(PrnRcptDtDet);
                        withBlock.RowCount = withBlock.RowCount + 1;
                    }
                }
                FillItemMaster();


                mAction = eAction.ActionUpdate;
                blnformChange = false;
                dtpCheckIn.Enabled = false;
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                MessageBox.Show("Load Transaction failed.", PrjMsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClose_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void fpsPrintReceipt_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int ctr1;
            long lngItemId = 0;
            bool blnFlag = false;

            var withBlock = fpsPrintReceipt;
            withBlock.Rows[withBlock.CurrentCell.RowIndex].Cells[withBlock.CurrentCell.ColumnIndex].Style.BackColor = Color.White;

            if (withBlock.CurrentCell.ColumnIndex == (int)PrintReceipt.outofbedorder)
            {
                ctr1 = withBlock.CurrentCell.RowIndex;
                for (int ctr = 0; ctr <= fpsPrintReceipt.RowCount - 1; ctr++)
                {
                    if (Convert.ToInt64(withBlock.Rows[ctr].Cells[(int)PrintReceipt.outofbedorder].Tag) == lngItemId)
                    {
                        int str = Convert.ToInt32(withBlock.Rows[ctr].Cells[(int)PrintReceipt.outofbedorder].Value);
                        int ID = Convert.ToInt32(withBlock.Rows[ctr].Cells[(int)PrintReceipt.ProductN].Tag);
                        if (str == null)
                            str = 0;
                        BedCheckOutDALobj.UpdateOutofOrder(str, ID);
                    }
                }
            }
        }
    }

}
