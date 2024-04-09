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
            ProductN = 0,
            ProductC = 1,
            Advance = 2,
            Nidhi = 3,
            Qty = 4,
            Occupied = 5,
            Pending = 6,
            outofbedorder = 7,
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
                dsItemMaster = cf.GetDsProductMenu(UserInfo.UserId,0);
            }
            catch (Exception ex)
            {
                cf.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
            if (dsItemMaster.Tables[0].Rows.Count > 0)
                cf.FillComboWithDataSet(lstItemMaster, dsItemMaster.Tables[0], "ItemTitle", "ItemTitle", "ItemId", "ItemCode", "");

            DataGridViewComboBoxCell cboItem = new DataGridViewComboBoxCell();
            foreach (var item in lstItemMaster.Items)
            {
                cboItem.Items.Add(item.ToString());
            }
            ((DataGridViewComboBoxColumn)fpsPrintReceipt.Columns[(int)ePrintReceipt.ProductN]).DataSource = cboItem.Items;
        }
        private void LoadTransaction()
        {
            System.Data.DataTable dr;
            BedCheckInDet PrnRcptDtDet = new BedCheckInDet();
            mDelColl = new Collection();

            try
            {
                dr = BedCheckOutDALobj.GetDrOcc(UserInfo.UserId);
                {
                    var withBlock = fpsPrintReceipt;
                    //while (dr.Read)
                    foreach (DataRow drrow in dr.Rows)
                    {
                        withBlock.RowCount = withBlock.RowCount + 1;
                    }
                    int i = 0;
                    foreach (DataRow drrow in dr.Rows)
                    {
                        withBlock.Rows[i].Cells[(int)PrintReceipt.ProductN].Value = drrow["ItemTitle"];
                        withBlock.Rows[i].Cells[(int)PrintReceipt.ProductC].ReadOnly = true;
                        withBlock.Rows[i].Cells[(int)PrintReceipt.ProductN].Tag = drrow["ItemId"];
                        withBlock.Rows[i].Cells[(int)PrintReceipt.ProductC].Value = drrow["ItemCode"];
                        withBlock.Rows[i].Cells[(int)PrintReceipt.Qty].Value = (drrow["Qty"]);
                        withBlock.Rows[i].Cells[(int)PrintReceipt.Nidhi].Value = (drrow["RENT"]);
                        withBlock.Rows[i].Cells[(int)PrintReceipt.Advance].Value = (drrow["ADVANCE"]);
                        withBlock.Rows[i].Cells[(int)PrintReceipt.Occupied].Value = (drrow["occupied"]);
                        withBlock.Rows[i].Cells[(int)PrintReceipt.Pending].Value = (drrow["pending"]);
                        withBlock.Rows[i].Cells[(int)PrintReceipt.outofbedorder].Value = (drrow["OutOFOrderBed"]);
                        mDelColl.Add(PrnRcptDtDet);
                        i++;
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
