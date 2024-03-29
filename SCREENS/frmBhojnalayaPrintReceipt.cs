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

namespace SGMOSOL.SCREENS
{
    public partial class frmBhojnalayaPrintReceipt : Form
    {
        BhojnalayPrintReceiptBAL bhojnalayprintReceiptBAL;
        frmUserDengi user;
        CommonFunctions cm;
        private DataTable tempItemTable;
        public bool isPrint = false;
        public frmBhojnalayaPrintReceipt()
        {
            InitializeComponent();
            CenterToParent();
            txtName.Focus();
            this.KeyDown += new KeyEventHandler(frmBhojnalayaPrintReceipt_KeyDown);
            this.KeyPreview = true;

            // cboItemCode.SelectedIndexChanged += cboItemCode_SelectedIndexChanged;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmBhojnalayaPrintReceipt_Load(object sender, EventArgs e)
        {
            user = new frmUserDengi();
            int centerX = (ClientSize.Width - pnlMaster.Width) / 2;
            int centerY = (ClientSize.Height - pnlMaster.Height) / 2;
            pnlMaster.Location = new System.Drawing.Point(centerX, centerY);
            SystemModel.Computer_Name.Comp_Name = System.Environment.MachineName;
            bhojnalayprintReceiptBAL = new BhojnalayPrintReceiptBAL();
            if (isPrint == false)
            {
                fillItemCode();
                fillItemName();
                createItemTable();
                fillDocumentType();
                txtReceiptno.Text = bhojnalayprintReceiptBAL.getMasterReceiptNumber().ToString();
            }
            txtCounter.Text = UserInfo.Counter_Name;
            txtUser.Text = UserInfo.UserName;
           
        }
        private void fillItemCode()
        {
            DataTable dt = new DataTable();
            dt = bhojnalayprintReceiptBAL.getItemCode();
            DataRow newRow = dt.NewRow();
            newRow["ITEM_CODE"] = "Select";
            newRow["ITEM_ID"] = 0;
            dt.Rows.InsertAt(newRow, 0);
            cboItemCode.DataSource = dt;
            cboItemCode.DisplayMember = "ITEM_CODE";
            cboItemCode.ValueMember = "ITEM_ID";
            cboItemCode.SelectedValue = 0;
        }
        private void fillItemName()
        {
            //DataTable dt = new DataTable();
            //dt = bhojnalayprintReceiptBAL.getItemName();
            //DataRow newRow = dt.NewRow();
            //newRow["ITEM_TITLE"] = "Select";
            //newRow["ITEM_ID"] = 0;
            //dt.Rows.InsertAt(newRow, 0);
            //cboItemName.DataSource = dt;
            //cboItemName.DisplayMember = "ITEM_TITLE";
            //cboItemName.ValueMember = "ITEM_ID";
            //cboItemName.SelectedValue = 0;
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
        

        //private void cboItemName_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int ItemID = 0;
        //    decimal price = 0;
        //    ItemID = bhojnalayprintReceiptBAL.getItemID("ITEM_TITLE", txtitemName.Text);
        //    cboItemCode.SelectedValue = ItemID;
        //    price = bhojnalayprintReceiptBAL.getItemPrice((Convert.ToInt32(cboItemCode.SelectedValue)));
        //    txtPrice.Text = price.ToString();
        //}\


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
            //txtPrice.Text = "0";
            txtQuantity.Text = "";
        }

        public void addItemIngrid()
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
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvItemDetails.Columns["EditButton"].Index)
            {
                ClearRowSelection();
                dgvItemDetails.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightPink;
                DataGridViewRow selectedRow = dgvItemDetails.Rows[e.RowIndex];
                int id = Convert.ToInt32(selectedRow.Cells["ID"].Value);
                cboItemCode.Text = Convert.ToString(selectedRow.Cells["Item Code"].Value);
                //  cboItemName.Text = Convert.ToString(selectedRow.Cells["Item Name"].Value);
                txtQuantity.Text = Convert.ToString(selectedRow.Cells["Quantity"].Value);
                txtPrice.Text = Convert.ToString(selectedRow.Cells["Price"].Value);
                txtAmount.Text = Convert.ToString(selectedRow.Cells["Amount"].Value);
                btnAdd.Text = "Update";
            }
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
            int Status = 0;
            bhojnalayPrintReceiptModel bhojnalayModel = new bhojnalayPrintReceiptModel();
            bhojnalayModel.ItemName = getSelectedItems().ToString();
            bhojnalayModel.Name = txtName.Text;
            bhojnalayModel.Address = txtAddress.Text;
            bhojnalayModel.Taluka = txtTaluka.Text;
            bhojnalayModel.PR_Date = Convert.ToDateTime(dtpPrnRcptDt.Text);
            bhojnalayModel.DocType = cboDocName.Text;
            bhojnalayModel.DocTypeDetail = txtDocumentName.Text;
            bhojnalayModel.Mobile = txtMobile.Text;
            bhojnalayModel.TotalAmount = Convert.ToDecimal(txtTotalAmount.Text);

            Status = bhojnalayprintReceiptBAL.InsertBhojnalayReceipt(bhojnalayModel);
            if (Status != 0)
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
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtMobile.Text == "")
            {
                lblMobile.Text = "Please Enter Number";
            }
            else
            {
                lblMobile.Text = "";
            }
            if (txtName.Text == "")
            {
                lblName.Text = "Please Enter Name";
            }
            else
            {
                lblName.Text = "";
            }
            if (txtTaluka.Text == "")
            {
                lblTaluka.Text = "Please Enter taluka";
            }
            else
            {
                lblTaluka.Text = "";
            }
            if (cboDocName.Text != "Select" && txtDocumentName.Text == "")
            {
                lblDocDetail.Text = "Please enter Document details";
            }
            else
            {
                lblDocDetail.Text = "";
            }
            if (txtAddress.Text == "")
            {
                lblAdd.Text = "Please Enter Address";
            }
            else
            {
                lblAdd.Text = "";
            }
            if (lblDocDetail.Text == "" && lblName.Text == "" && lblMobile.Text == "" && lblTaluka.Text == "" && lblAdd.Text == "")
            {
                insertBhojnalayReceiptMaster();
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
            cboDocName.Text = "";
            createItemTable();
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
            if (cboDocName.Text != "Select")
            {
                lblDocDetail.Text = "Please enter Document Detail";
            }
            else
            {
                lblDocDetail.Text = "";
            }
            user.SetDocType(cboDocName.Text);
            user.Show();
        }

        private void txtTotalAmount_TextChanged(object sender, EventArgs e)
        {
            CommonFunctions cm = new CommonFunctions();
            lblamouwords.Text = cm.words(Convert.ToDouble(txtTotalAmount.Text));
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
                cboDocName.Text = obj.DocType;
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
                btnAcknowledge.Enabled = true;
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
            if (txtName.Text != "")
            {
                user.SetText(txtName.Text);
                user.Show();
            }
        }

        private void txtTaluka_TextChanged(object sender, EventArgs e)
        {
            if (txtTaluka.Text != "")
            {
                user.SetTaluka(txtTaluka.Text);
                user.Show();
            }
        }

        private void txtDocumentName_TextChanged(object sender, EventArgs e)
        {
            if (txtDocumentName.Text != "")
            {
                user.SetDocDetail(txtDocumentName.Text);
                user.Show();
            }
        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {
            if (txtAddress.Text != "")
            {
                user.SetAddress(txtAddress.Text);
                user.Show();
            }

        }

        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                addItemIngrid();
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
        }
    }
}
