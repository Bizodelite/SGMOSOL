using SGMOSOL.ADMIN;
using SGMOSOL.BAL;
using SGMOSOL.DAL;
using SGMOSOL.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using System.Web.UI.WebControls;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using System.Xml.Linq;
using static SGMOSOL.ADMIN.CommonFunctions;
using CrystalDecisions.ReportAppServer.DataDefModel;

namespace SGMOSOL.SCREENS
{
    public partial class frmBhojnalayaPrintReceipt : Form
    {
        BhojnalayPrintReceiptBAL bhojnalayprintReceiptBAL;
        frmUserDengi user;
        CommonFunctions cm;
        private DataTable tempItemTable;
        public bool isPrint = false;
        CommonFunctions commonFunctions;
        DataTable dt;
        private string mStrCounterMachineShortName;
        private int PrintReceiptDeptID;
        private string PrintReceiptDeptName;
        private string PrintReceiptLocName;
        private int PrintReceiptLocId;
        private List<DataRow> filteredItems;

        public frmBhojnalayaPrintReceipt()
        {
            InitializeComponent();
            CenterToParent();
            txtName.Focus();
            this.KeyDown += new KeyEventHandler(frmBhojnalayaPrintReceipt_KeyDown);
            this.KeyPreview = true;
            user = Application.OpenForms.OfType<frmUserDengi>().FirstOrDefault();
            if (user == null)
            {
                user = new frmUserDengi("Mess");
                user.Show();
            }
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            try
            {
                if (keyData == Keys.Enter)
                {
                    if (ActiveControl is System.Windows.Forms.Button btnSave)
                    {
                        if (btnSave.Name == "btnSave")
                        {
                            btnSave.PerformClick();
                            return true;
                        }
                    }
                    if (ActiveControl is System.Windows.Forms.Button btnNew)
                    {
                        if (btnNew.Name == "btnNew")
                        {
                            btnNew.PerformClick();
                            return true;
                        }
                    }
                    if (ActiveControl is System.Windows.Forms.Button btnClose)
                    {
                        if (btnClose.Name == "btnClose")
                        {
                            btnClose.PerformClick();
                            return true;
                        }
                    }
                    if (ActiveControl is System.Windows.Forms.Button btnSearch)
                    {
                        if (btnSearch.Name == "btnSearch")
                        {
                            btnSearch.PerformClick();
                            return true;
                        }
                    }
                    if (ActiveControl is System.Windows.Forms.Button btnPrint)
                    {
                        if (btnPrint.Name == "btnPrint")
                        {
                            btnPrint.PerformClick();
                            return true;
                        }
                    }
                    if (ActiveControl is System.Windows.Forms.TextBox txtQuantity)
                    {
                        if (txtQuantity.Name == "txtQuantity")
                        {
                            //addItemIngrid();
                            return false;
                        }
                    }
                    SendKeys.Send("{TAB}");
                    return true;
                }
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return false;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void frmBhojnalayaPrintReceipt_Load(object sender, EventArgs e)
        {
            dt = new DataTable();
            user = new frmUserDengi("Mess");
            int centerX = (ClientSize.Width - pnlMaster.Width) / 2;
            int centerY = (ClientSize.Height - pnlMaster.Height) / 2;
            pnlMaster.Location = new System.Drawing.Point(centerX, centerY);
            SystemModel.Computer_Name.Comp_Name = System.Environment.MachineName;
            bhojnalayprintReceiptBAL = new BhojnalayPrintReceiptBAL();
            commonFunctions = new CommonFunctions();
            dtpPrnRcptDt.Value = System.DateTime.Now;
            if (isPrint == false)
            {
                FillCounter();
                fillItemCode();
                createItemTable();
                fillDocumentType();
                getMasterReceiptNumber();
            }
            txtUser.Text = UserInfo.UserName;
            txtPrice.Text = "0";

        }
        public void getMasterReceiptNumber()
        {
            try
            {
                bhojnalayprintReceiptBAL = new BhojnalayPrintReceiptBAL();
                int receiptID = bhojnalayprintReceiptBAL.getMasterReceiptNumber();
                txtReceiptno.Text = receiptID.ToString();
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
        }
        private void FillCounter()
        {
            commonFunctions = new CommonFunctions();
            System.Data.DataTable dr;
            dr = commonFunctions.GetDrCounterMachId(UserInfo.UserId, SystemHDDModelNo, SystemHDDSerialNo, SystemMacID, Convert.ToInt16(eModType.Bhojnalay));
            if (dr.Rows.Count > 0)
            {
                UserInfo.Counter_Name = dr.Rows[0]["CounterMachineTitle"].ToString();
                txtCounter.Text = dr.Rows[0]["CounterMachineShortName"].ToString();
                txtCounter.Tag = dr.Rows[0]["CtrMachId"];
                UserInfo.ctrMachID = Convert.ToInt32(txtCounter.Tag);
                UserInfo.Dept_id = Convert.ToInt32(dr.Rows[0]["DeptId"]);
                PrintReceiptDeptName = dr.Rows[0]["DepartmentName"].ToString();
                PrintReceiptLocName = dr.Rows[0]["LocName"].ToString();
                UserInfo.Loc_id = Convert.ToInt32(dr.Rows[0]["LocId"]);
                mStrCounterMachineShortName = dr.Rows[0]["CounterMachineShortName"].ToString();
                this.Text = PrintReceiptLocName + " /" + PrintReceiptDeptName + " /" + mStrCounterMachineShortName + " /" + UserInfo.version;
            }
            //dr.Close();
        }
        private void fillItemCode()
        {
            cboItemCode.TextChanged -= cboItemCode_TextChanged;
            dt = bhojnalayprintReceiptBAL.getItemCodeAssignToCounter();
            cboItemCode.DataSource = dt;
            cboItemCode.DisplayMember = "ITEM_CODE";
            cboItemCode.ValueMember = "ITEM_ID";
            cboItemCode.SelectedValue = 0;
            cboItemCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboItemCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
            UpdateSuggestions();
        }
        private void UpdateSuggestions()
        {
            // Clear existing suggestions
            cboItemCode.AutoCompleteCustomSource.Clear();

            // Extract column data from the DataTable
            List<string> suggestions = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                suggestions.Add(row["Item_Code"].ToString());
            }

            // Filter suggestions based on user input
            string input = cboItemCode.Text;
            List<string> filteredSuggestions = suggestions.FindAll(s => s.StartsWith(input, StringComparison.OrdinalIgnoreCase));

            // Add filtered suggestions to the auto-complete source
            cboItemCode.AutoCompleteCustomSource.AddRange(filteredSuggestions.ToArray());
        }
        public void fillDocumentType()
        {
            cm = new CommonFunctions();
            DataTable dt = new DataTable();
            dt = cm.GetDocumentType();
            DataRow newRow = dt.NewRow();
            cboDocName.DataSource = dt;
            cboDocName.DisplayMember = "DocumentName";
            cboDocName.ValueMember = "DocumentID";
            cboDocName.SelectedValue = 0;
        }

        private void cboItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ItemName = null;
            decimal price = 0;
            ItemName = bhojnalayprintReceiptBAL.getItemName("ITEM_CODE", cboItemCode.Text);
            txtitemName.Text = ItemName;
            if (cboItemCode.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)cboItemCode.SelectedItem;

                // Retrieve the value from the DataRowView
                int itemId = Convert.ToInt32(selectedRow["ITEM_ID"]);
                price = bhojnalayprintReceiptBAL.getItemPrice(itemId);
                txtPrice.Text = price.ToString();
            }
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            decimal price = 0;
            decimal quantity = 0;
            decimal amount = 0;
            if (txtQuantity.Text != "" && txtPrice.Text != "")
            {
                price = Convert.ToDecimal(txtPrice.Text);
                quantity = Convert.ToDecimal(txtQuantity.Text);
                amount = price * quantity;
                txtAmount.Text = amount.ToString();
                if (Convert.ToInt32(txtQuantity.Text) > 99)
                {
                    lblQuantity.Text = "Quantity should not greater than 99";
                }
            }
        }
        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }
        private void clear()
        {
            cboItemCode.SelectedValue = 0;
            //  cboItemName.SelectedValue = 0;
            txtAmount.Text = "";
            txtPrice.Text = "0";
            txtQuantity.Text = "";
        }

        public void addItemIngrid()
        {
            lblAlert.Text = "";
            if (dgvItemDetails.Rows.Count >= 5)
            {
                MessageBox.Show("Maximum limit of 5 Items reached.", "Limit Exceeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return; // Exit the method without adding the new row
            }
            if (txtitemName.Text != "" && cboItemCode.Text != "")
            {
                if (txtQuantity.Text != "" && cboItemCode.Text != "Select")
                {
                    lblQuantity.Text = "";
                    DataRow[] duplicateRow = tempItemTable.Select($"[Item Name] = '{txtitemName.Text}'");
                    if (duplicateRow.Length > 0)
                    {
                        foreach (DataRow row in duplicateRow)
                        {
                            int currentQuantity = Convert.ToInt32(row["Quantity"]);
                            int currentAmount = Convert.ToInt32(row["Amount"]);
                            row["Price"] = Convert.ToDecimal(txtPrice.Text);
                            // row["Quantity"] = Convert.ToInt32(txtQuantity.Text) + currentQuantity;
                            row["Quantity"] = Convert.ToInt32(txtQuantity.Text);
                            row["Amount"] = Convert.ToDecimal(txtAmount.Text);
                            // row["Amount"] = currentAmount;
                        }
                    }
                    else
                    {
                        DataRow newRow = tempItemTable.NewRow();
                        newRow["Item Code"] = cboItemCode.Text.ToString();
                        newRow["Item Name"] = txtitemName.Text.ToString();
                        newRow["Price"] = Convert.ToDecimal(txtPrice.Text);
                        newRow["Quantity"] = Convert.ToInt32(txtQuantity.Text);
                        newRow["Amount"] = Convert.ToDecimal(txtAmount.Text);
                        tempItemTable.Rows.Add(newRow);
                    }
                    txtTotalAmount.Text = getTotalAmount();
                    clear();
                    cboItemCode.Focus();
                }
            }
            else
            {
                MessageBox.Show("Please Select Item");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Add")
            {
                if (txtQuantity.Text != "" && cboItemCode.Text != "Select")
                {
                    lblQuantity.Text = "";
                    DataRow[] duplicateRow = tempItemTable.Select($"[Item Name] = '{txtitemName.Text}'");
                    if (duplicateRow.Length > 0)
                    {
                        foreach (DataRow row in duplicateRow)
                        {
                            int currentQuantity = Convert.ToInt32(row["Quantity"]);
                            int currentAmount = Convert.ToInt32(row["Amount"]);
                            row["Price"] = Convert.ToDecimal(txtPrice.Text);
                            row["Quantity"] = Convert.ToInt32(txtQuantity.Text) + currentQuantity;
                            row["Amount"] = Convert.ToDecimal(txtAmount.Text) + currentAmount;
                        }
                    }
                    else
                    {
                        DataRow newRow = tempItemTable.NewRow();
                        newRow["Item Code"] = cboItemCode.Text.ToString();
                        // newRow["Item Name"] = cboItemName.Text.ToString();
                        newRow["Price"] = Convert.ToDecimal(txtPrice.Text);
                        newRow["Quantity"] = Convert.ToInt32(txtQuantity.Text);
                        newRow["Amount"] = Convert.ToDecimal(txtAmount.Text);
                        tempItemTable.Rows.Add(newRow);
                    }
                    txtTotalAmount.Text = getTotalAmount();
                    clear();
                }
                else
                {
                    lblQuantity.Text = "Please add Details";
                }
            }
            if (btnAdd.Text == "Update")
            {
                int selectedRowIndex = dgvItemDetails.CurrentCell.RowIndex;
                DataGridViewRow selectedRow = dgvItemDetails.Rows[selectedRowIndex];
                int id = Convert.ToInt32(selectedRow.Cells["ID"].Value);
                DataRow[] rowsToUpdate = tempItemTable.Select($"ID = {id}");
                if (rowsToUpdate.Length > 0)
                {
                    DataRow row = rowsToUpdate[0];
                    row["Item Code"] = cboItemCode.Text.ToString();
                    // row["Item Name"] = cboItemName.Text.ToString();
                    row["Price"] = Convert.ToDecimal(txtPrice.Text);
                    row["Quantity"] = Convert.ToInt32(txtQuantity.Text);
                    row["Amount"] = Convert.ToDecimal(txtAmount.Text);
                    dgvItemDetails.Refresh();
                    txtTotalAmount.Text = getTotalAmount();
                    MessageBox.Show($"Row with ID {id} updated successfully!");
                    btnAdd.Text = "Add";
                    clear();
                }
            }
        }
        private void createItemTable()
        {
            dgvItemDetails.Columns.Clear();
            tempItemTable = new DataTable("ItemTable");
            tempItemTable.Columns.Add("ID", typeof(int));
            tempItemTable.Columns.Add("Item Code", typeof(string));
            tempItemTable.Columns.Add("Item Name", typeof(string));
            tempItemTable.Columns.Add("Price", typeof(decimal));
            tempItemTable.Columns.Add("Quantity", typeof(int));
            tempItemTable.Columns.Add("Amount", typeof(decimal));

            tempItemTable.Columns["ID"].AutoIncrement = true;
            tempItemTable.Columns["ID"].AutoIncrementSeed = 1;
            tempItemTable.Columns["ID"].AutoIncrementStep = 1;
            dgvItemDetails.DataSource = tempItemTable;
            //DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn();
            //editButtonColumn.Name = "EditButton";
            //editButtonColumn.HeaderText = "Edit";
            //editButtonColumn.Text = "Edit";
            //editButtonColumn.UseColumnTextForButtonValue = true;
            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
            deleteButtonColumn.Name = "DeleteButton";
            deleteButtonColumn.HeaderText = "Delete";
            deleteButtonColumn.Text = "Delete";
            deleteButtonColumn.UseColumnTextForButtonValue = true;
            // dgvItemDetails.Columns.Add(editButtonColumn);
            dgvItemDetails.Columns.Add(deleteButtonColumn);
        }

        private void dgvItemDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0 && e.ColumnIndex == dgvItemDetails.Columns["EditButton"].Index)
            //{
            //    ClearRowSelection();
            //    dgvItemDetails.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightPink;
            //    DataGridViewRow selectedRow = dgvItemDetails.Rows[e.RowIndex];
            //    int id = Convert.ToInt32(selectedRow.Cells["ID"].Value);
            //    cboItemCode.Text = Convert.ToString(selectedRow.Cells["Item Code"].Value);
            //    //  cboItemName.Text = Convert.ToString(selectedRow.Cells["Item Name"].Value);
            //    txtQuantity.Text = Convert.ToString(selectedRow.Cells["Quantity"].Value);
            //    txtPrice.Text = Convert.ToString(selectedRow.Cells["Price"].Value);
            //    txtAmount.Text = Convert.ToString(selectedRow.Cells["Amount"].Value);
            //    btnAdd.Text = "Update";
            //}
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvItemDetails.Columns["DeleteButton"].Index)
            {
                ClearRowSelection();
                dgvItemDetails.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightPink;
                int id = Convert.ToInt32(dgvItemDetails.Rows[e.RowIndex].Cells["ID"].Value);
                DialogResult result = MessageBox.Show($"Are you sure you want to delete the row with ID {id}?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    DataRow[] rowsToDelete = tempItemTable.Select($"ID = {id}");
                    if (rowsToDelete.Length > 0)
                    {
                        rowsToDelete[0].Delete();
                        tempItemTable.AcceptChanges();
                        dgvItemDetails.Refresh();
                        MessageBox.Show($"Row with ID {id} deleted successfully!");
                        txtTotalAmount.Text = getTotalAmount();
                    }
                }
            }
        }
        private void ClearRowSelection()
        {
            foreach (DataGridViewRow row in dgvItemDetails.Rows)
            {
                row.DefaultCellStyle.BackColor = dgvItemDetails.DefaultCellStyle.BackColor;
            }
        }
        private string getTotalAmount()
        {
            decimal sum = 0;
            if (dgvItemDetails.Columns.Contains("Amount"))
            {
                foreach (DataGridViewRow row in dgvItemDetails.Rows)
                {
                    if (row.Cells["Amount"].Value != null)
                    {
                        sum += Convert.ToDecimal(row.Cells["Amount"].Value);
                    }
                }
            }
            return sum.ToString();
        }

        public void addCustomField(DataTable dt)
        {
            string printType = "D";
            if (dt.Columns.Contains("AMOUNT"))
            {
                foreach (DataRow row in dt.Rows)
                {
                    double amount = Convert.ToDouble(row["AMOUNT"]);
                    row["AMOUNT_IN_WORDS"] = cm.words(Convert.ToInt32(amount));
                    if (printType == "D")
                    {
                        row["REPORT_TYPE"] = "Duplicate";
                    }
                    else
                    {
                        row["REPORT_TYPE"] = "ND";
                    }
                }
            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            frmReportViewer frm = new frmReportViewer("PRINT", txtReceiptno.Tag.ToString(), "D");
            frm.createReport("Bhojnalaya");
            // frm.Show();
        }
        private void insertBhojnalayReceiptMaster()
        {
            loginDAL obj = new loginDAL();
            if (obj.CheckDateTime())
            {
                int Status = 0;
                bhojnalayPrintReceiptModel bhojnalayModel = new bhojnalayPrintReceiptModel();
                bhojnalayModel.ItemName = getSelectedItems().ToString();
                bhojnalayModel.Name = txtName.Text;
                bhojnalayModel.Address = txtAddress.Text;
                bhojnalayModel.Taluka = txtTaluka.Text;
                bhojnalayModel.PR_Date = dtpPrnRcptDt.Value;
                bhojnalayModel.DocType = cboDocName.SelectedValue.ToString();
                bhojnalayModel.DocTypeDetail = txtDocumentName.Text;
                bhojnalayModel.Mobile = txtMobile.Text;
                bhojnalayModel.TotalAmount = Convert.ToDecimal(txtTotalAmount.Text);

                Status = bhojnalayprintReceiptBAL.InsertBhojnalayReceipt(bhojnalayModel);
                if (Status > 0)
                {
                    List<string> itemNameList = new List<string>(bhojnalayModel.ItemName.Split(','));
                    List<int> itemIDs = new List<int>();
                    foreach (var item in itemNameList)
                    {
                        int ItemID = bhojnalayprintReceiptBAL.getItemIdbyItemName(item.Trim());
                        bhojnalayModel.ItemId = ItemID;
                        foreach (DataGridViewRow row in dgvItemDetails.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                string itemName = row.Cells["Item Name"].Value.ToString();
                                if (itemName == item)
                                {
                                    bhojnalayModel.PRINT_MST_ID = Status;
                                    bhojnalayModel.Price = Convert.ToDecimal(row.Cells["Price"].Value);
                                    bhojnalayModel.Quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                                    bhojnalayModel.Amount = Convert.ToDecimal(row.Cells["Amount"].Value);
                                    int id = bhojnalayprintReceiptBAL.InsertMessItemData(bhojnalayModel);
                                }
                            }
                        }
                    }
                    MessageBox.Show("Record Saved Successully");
                    //Print code
                    if (Status > 0)
                    {
                        string Receipt_Id = Status.ToString();
                        frmReportViewer report = new frmReportViewer("PRINT", Receipt_Id);
                        report.createReport("Bhojnalaya");
                        //  report.Show();

                    }
                    clearControls();
                    getMasterReceiptNumber();
                    UnlockControls();
                    frmBhojnalayaPrintReceipt_Load(null, null);
                }
            }
            else {
                MessageBox.Show("Date mismatch!!!");
            }
        }
        public void CheckValidDocs()
        {
            commonFunctions = new CommonFunctions();
            if (cboDocName.Text == "Pan Card")
            {
                if (!commonFunctions.IsValidPan(txtDocumentName.Text))
                {
                    lblDocDetail.Text = "Please enter valid pan number!";
                }
                else
                {
                    lblDocDetail.Text = "";
                }
            }
            if (cboDocName.Text == "Adhar Card")
            {
                if (!commonFunctions.IsValidAadhar(txtDocumentName.Text))
                {
                    lblDocDetail.Text = "Please enter valid adhar number!";
                }
                else
                {
                    lblDocDetail.Text = "";
                }
            }
            if (cboDocName.Text == "Passport")
            {
                if (!commonFunctions.IsValidPassport(txtDocumentName.Text))
                {
                    lblDocDetail.Text = "Please enter valid passport number!";
                }
                else
                {
                    lblDocDetail.Text = "";
                }
            }
            if (cboDocName.Text == "Driving License")
            {
                if (!commonFunctions.IsValidLicenseNumber(txtDocumentName.Text))
                {
                    lblDocDetail.Text = "Please enter valid driving license!";
                }
                else
                {
                    lblDocDetail.Text = "";
                }
            }
            if (cboDocName.Text == "Voter ID")
            {
                if (!commonFunctions.IsValidVoterId(txtDocumentName.Text))
                {
                    lblDocDetail.Text = "Please enter valid Voter ID!";
                }
                else
                {
                    lblDocDetail.Text = "";
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            validation();
            if (lblDocDetail.Text == "" && lblName.Text == "" && lblAddress.Text == "")
            {
                if (dgvItemDetails.Rows.Count > 0)
                {
                    insertBhojnalayReceiptMaster();
                    lblAlert.Text = "";
                }
                else
                {
                    lblAlert.Text = "Please select at least one item";
                }
            }
        }
        public void validation()
        {
            if (txtName.Text == "")
            {
                lblName.Text = "Please Enter Name";
                txtName.Focus();
            }
            else
            {
                lblName.Text = "";
            }
            if (txtTotalAmount.Text != "")
            {
                if (Convert.ToDecimal(txtTotalAmount.Text) >= 500)
                {
                    if (cboDocName.Text != "" && txtDocumentName.Text != "")
                    {
                        CheckValidDocs();
                    }
                    else
                    {
                        if (cboDocName.Text == "Select")
                        {
                            lblDocDetail.Text = "Please select Document Type";
                        }
                        else { lblDocDetail.Text = ""; }
                        if (lblDocDetail.Text == "")
                        {
                            lblDocDetail.Text = "Please enter Document details";
                        }
                        else { lblDocDetail.Text = ""; }
                    }
                }
                else
                {
                    if (cboDocName.Text == "Select" && txtDocumentName.Text != "")
                    {
                        lblDocDetail.Text = "";
                    }
                    else
                    {
                        lblDocDetail.Text = "";
                    }
                }
            }

            if (txtAddress.Text == "")
            {
                lblAddress.Text = "Please Enter Address";
                txtAddress.Focus();
            }
            else
            {
                lblAddress.Text = "";
            }
        }
        public string getSelectedItems()
        {
            StringBuilder itemNames = new StringBuilder();
            foreach (DataGridViewRow row in dgvItemDetails.Rows)
            {
                if (!row.IsNewRow)
                {
                    object cellValue = row.Cells["Item Name"].Value;
                    if (cellValue != null)
                    {
                        itemNames.Append(cellValue.ToString());
                        if (row.Index < dgvItemDetails.Rows.Count - 1)
                        {
                            itemNames.Append(",");
                        }
                    }
                }
            }
            return itemNames.ToString();
        }
        public void clearControls()
        {
            txtName.Text = "";
            txtAddress.Text = "";
            txtMobile.Text = "";
            txtQuantity.Text = "";
            txtTaluka.Text = "";
            txtDocumentName.Text = "";
            cboDocName.SelectedValue = 0; ;
            createItemTable();
            lblMobile.Text = "";
            lblAddress.Text = "";
            lblName.Text = "";
            // lblQuantity.Text = "0";
            lblDocDetail.Text = "";
            txtDocumentName.Enabled = false;
            lblamouwords.Text = "";
            lblTaluka.Text = "";
            txtName.Focus();
            lblAdd.Text = "";
            lblAddress.Text = "";
            lblMobile.Text = "";
            txtTotalAmount.Text = "";
            txtPrice.Text = "0";
        }
        public void lockControls()
        {
            txtName.Enabled = false;
            txtAddress.Enabled = false;
            txtMobile.Enabled = false;
            txtTaluka.Enabled = false;
            txtPrice.Enabled = false;
            txtDocumentName.Enabled = false;
            txtQuantity.Enabled = false;
            txtAmount.Enabled = false;
            txtTotalAmount.Enabled = false;
            cboDocName.Enabled = false;
            cboItemCode.Enabled = false;
            // cboItemName.Enabled = false;
            btnAdd.Enabled = false;
        }
        public void UnlockControls()
        {
            txtName.Enabled = true;
            txtAddress.Enabled = true;
            txtMobile.Enabled = true;
            txtTaluka.Enabled = true;
            txtPrice.Enabled = true;
            txtDocumentName.Enabled = true;
            txtQuantity.Enabled = true;
            txtAmount.Enabled = true;
            cboDocName.Enabled = true;
            cboItemCode.Enabled = true;
            // cboItemName.Enabled = false;
            btnAdd.Enabled = false;
        }

        private void txtMobile_TextChanged(object sender, EventArgs e)
        {
            string mobileNumber = txtMobile.Text.Trim();
            string pattern = @"^\d{10}$";
            if (Regex.IsMatch(mobileNumber, pattern))
            {
                lblMobile.Text = "";
            }
            else
            {
                lblMobile.Text = "Please enter a valid 10-digit mobile number.";
            }
            user.Setmobile(mobileNumber);
            user.Show();
        }

        private void cboDocName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDocName.Text != "Select" && txtDocumentName.Text == "")
            {
                lblDocDetail.Text = "Please enter Document Detail";
            }
            else
            {
                lblDocDetail.Text = "";
            }
            if (cboDocName.Text == "Select")
            {
                txtDocumentName.Enabled = false;
                txtDocumentName.Text = "";
            }
            else
            {
                txtDocumentName.Enabled = true;
            }

            user = Application.OpenForms.OfType<frmUserDengi>().FirstOrDefault();
            if (user != null)
            {
                user.SetDocType(cboDocName.Text);
                // user.Show();
            }
        }

        private void txtTotalAmount_TextChanged(object sender, EventArgs e)
        {
            CommonFunctions cm = new CommonFunctions();
            if (txtTotalAmount.Text != "")
            {
                lblamouwords.Text = cm.words(Convert.ToDouble(txtTotalAmount.Text));
            }
            user.SetAmount(txtTotalAmount.Text);
            user.SetAmtInWord(lblamouwords.Text);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to close the application?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            clearControls();
            UnlockControls();
            getMasterReceiptNumber();
            btnSave.Enabled = true;
            btnPrint.Enabled = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            frmBhojnalaySearch frmsearch = new frmBhojnalaySearch();
            frmsearch.Show();
        }
        public void getAllData(object data)
        {
            BhojnalayPrintReceiptBAL ba = new BhojnalayPrintReceiptBAL();
            if (data is bhojnalayPrintReceiptModel obj)
            {
                txtName.Text = obj.Name;
                txtAddress.Text = obj.Address;
                txtMobile.Text = obj.Mobile;
                txtTotalAmount.Text = obj.TotalAmount.ToString();
                txtTaluka.Text = obj.Taluka;
                txtDocumentName.Text = obj.DocTypeDetail;
                cboDocName.SelectedValue = obj.DocType;
                txtReceiptno.Text = obj.SerialNo.ToString();
                txtReceiptno.Tag = obj.PRINT_MST_ID.ToString();
                // code for creating Item table
                DataTable dtItem = new DataTable();
                dtItem = ba.getItemDetailbyMasterId(obj.PRINT_MST_ID.ToString());
                if (dtItem != null)
                {
                    dgvItemDetails.Columns.Clear();
                    dgvItemDetails.DataSource = dtItem;
                }
                lockControls();
                btnSave.Enabled = false;
                btnPrint.Enabled = true;
                dgvItemDetails.Enabled = false;
            }
        }
        private void txtMobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            user = Application.OpenForms.OfType<frmUserDengi>().FirstOrDefault();
            if (user != null)
            {
                if (txtName.Text != "")
                {
                    user.SetText(txtName.Text);
                    user.Show();
                }
            }
        }
        private void txtTaluka_TextChanged(object sender, EventArgs e)
        {
            user = Application.OpenForms.OfType<frmUserDengi>().FirstOrDefault();
            if (user != null)
            {
                if (txtTaluka.Text != "")
                {
                    user.SetTaluka(txtTaluka.Text);
                    user.Show();
                }
            }
        }
        private void txtDocumentName_TextChanged(object sender, EventArgs e)
        {
            //if (cboDocName.Text == "Adhar Card")
            //{
            //    txtDocumentName.MaxLength = 12;
            //}
            //if (this.Text != "")
            //{
            //    lblDocDetail.Text = "";
            //}

            CheckValidDocs();
            user = Application.OpenForms.OfType<frmUserDengi>().FirstOrDefault();
            if (user != null)
            {
                if (txtDocumentName.Text != "")
                {
                    user.SetDocDetail(txtDocumentName.Text);
                    user.Show();
                }
            }
        }
        private void txtAddress_TextChanged(object sender, EventArgs e)
        {
            user = Application.OpenForms.OfType<frmUserDengi>().FirstOrDefault();
            if (user != null)
            {
                if (txtAddress.Text != "")
                {
                    user.SetAddress(txtAddress.Text);
                    user.Show();
                }
            }

        }
        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            bool itemFound = false;
            if (e.KeyCode == Keys.Enter)
            {
                if (txtAmount.Text != "")
                {
                    if (Convert.ToDecimal(txtAmount.Text) > 500)
                    {
                        lblamount.Text = "Amount should not greater than 500";
                    }
                    else
                    {
                        lblamount.Text = "";
                    }
                }
                if (txtQuantity.Text != "0" && lblamount.Text == "")
                {
                    addItemIngrid();
                }
                if (txtQuantity.Text == "0")
                {
                    // Get the item name
                    string itemName = txtitemName.Text;

                    // Search for the item in the DataGridView
                    foreach (DataGridViewRow row in dgvItemDetails.Rows)
                    {
                        if (row.Cells["Item Name"].Value.ToString() == itemName)
                        {
                            // Remove the row from the DataGridView
                            dgvItemDetails.Rows.Remove(row);
                            txtTotalAmount.Text = getTotalAmount();
                            itemFound = true;
                            clear();
                            cboItemCode.Focus();
                            break;
                        }
                    }
                    if (itemFound == false)
                    {
                        MessageBox.Show("Invalid Item !!!");
                    }
                }
            }
        }
        private void frmBhojnalayaPrintReceipt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                btnSave.PerformClick();
            }
            if (e.Control && e.KeyCode == Keys.P)
            {
                btnPrint.PerformClick();
            }
            if (e.Alt && e.KeyCode == Keys.N)
            {
                txtName.Focus();
            }
            if (e.Alt && e.KeyCode == Keys.M)
            {
                txtMobile.Focus();
            }
            if (e.Alt && e.KeyCode == Keys.T)
            {
                txtTaluka.Focus();
            }
            if (e.Alt && e.KeyCode == Keys.A)
            {
                txtAddress.Focus();
            }
            if (e.KeyCode == Keys.End)
            {
                btnSave.PerformClick();
            }
        }
        private void cboItemCode_TextChanged(object sender, EventArgs e)
        {
            UpdateSuggestions();
        }

        private void txtDocumentName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cboDocName.Text == "Adhar Card")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
                txtDocumentName.MaxLength = 12;
            }
        }
    }
}
