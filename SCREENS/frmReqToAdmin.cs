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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static SGMOSOL.ADMIN.CommonFunctions;

namespace SGMOSOL.SCREENS
{
    public partial class frmReqToAdmin : Form
    {
        BhojnalayPrintReceiptBAL obj = new BhojnalayPrintReceiptBAL();
        private DataTable tempItemTable;
        CommonFunctions cm = new CommonFunctions();
        DataTable dt;
        private string mStrCounterMachineShortName;
        private int PrintReceiptDeptID;
        private string PrintReceiptDeptName;
        private string PrintReceiptLocName;
        private int PrintReceiptLocId;
        private List<DataRow> filteredItems;
        public frmReqToAdmin()
        {
            InitializeComponent();
            this.KeyPreview = true;
            CenterToParent();
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
                cm.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return false;
            }
            return base.ProcessDialogKey(keyData);
        }
        private void FillCounter()
        {
            cm = new CommonFunctions();
            System.Data.DataTable dr;
            dr = cm.GetDrCounterMachId(UserInfo.UserId, SystemHDDModelNo, SystemHDDSerialNo, SystemMacID, Convert.ToInt16(eModType.Bhojnalay));
            if (dr.Rows.Count > 0)
            {
                UserInfo.Counter_Name = dr.Rows[0]["CounterMachineShortName"].ToString();
                txtCounter.Tag = dr.Rows[0]["CtrMachId"];
                UserInfo.ctrMachID = Convert.ToInt32(txtCounter.Tag);
                UserInfo.Dept_id = Convert.ToInt32(dr.Rows[0]["DeptId"]);
                PrintReceiptDeptName = dr.Rows[0]["DepartmentName"].ToString();
                PrintReceiptLocName = dr.Rows[0]["LocName"].ToString();
                UserInfo.Loc_id = Convert.ToInt32(dr.Rows[0]["LocId"]);
                mStrCounterMachineShortName = dr.Rows[0]["CounterMachineShortName"].ToString();
                this.Text = PrintReceiptLocName + " /" + PrintReceiptDeptName + " /" + mStrCounterMachineShortName + "/ " + Application.ProductVersion;
            }
            //dr.Close();
        }
        private void frmReqToAdmin_Load(object sender, EventArgs e)
        {
            dt = new DataTable();
            int centerX = (ClientSize.Width - pnlMaster.Width) / 2;
            int centerY = (ClientSize.Height - pnlMaster.Height) / 2;
            pnlMaster.Location = new System.Drawing.Point(centerX, centerY);
            FillCounter();
            fillItemCode();
            getRequirementber();
            createItemTable();
            txtCounter.Text = UserInfo.Counter_Name;
            txtUser.Text = UserInfo.UserName;
        }
        private void ClearRowSelection()
        {
            foreach (DataGridViewRow row in dgvItemDetails.Rows)
            {
                row.DefaultCellStyle.BackColor = dgvItemDetails.DefaultCellStyle.BackColor;
            }
        }
        public void getRequirementber()
        {
            int intRequiremenumber = Convert.ToInt32(obj.getReqNumber()) + 1;
            txtReqID.Text = intRequiremenumber.ToString();
        }
        public void fillItemCode()
        {
            DataTable dt = new DataTable();
            dt = obj.getItemCode();
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
        public void clearControl()
        {
            txtQuantity.Text = "";
            txtItemName.Text = "";
            cboItemCode.SelectedValue = 0;
            createItemTable();
            getRequirementber();
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Label5_Click(object sender, EventArgs e)
        {

        }

        private void cboItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboItemCode.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)cboItemCode.SelectedItem;
                int itemId = Convert.ToInt32(selectedRow["ITEM_ID"]);
                getItemName(itemId);
            }
        }
        public void getItemName(int ItemId)
        {
            string strItemName = null;
            strItemName = obj.getItemName(ItemId);
            txtItemName.Text = strItemName;
        }
        private void createItemTable()
        {
            dgvItemDetails.Columns.Clear();
            tempItemTable = new DataTable("ItemTable");
            tempItemTable.Columns.Add("ID", typeof(int));
            tempItemTable.Columns.Add("Item Code", typeof(string));
            tempItemTable.Columns.Add("Item Name", typeof(string));
            // tempItemTable.Columns.Add("Price", typeof(decimal));
            tempItemTable.Columns.Add("Quantity", typeof(int));
            // tempItemTable.Columns.Add("Amount", typeof(decimal));

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
        public void addItemIngrid()
        {
            
            if (txtItemName.Text != "" && cboItemCode.Text != "")
            {
                if (txtQuantity.Text != "" && cboItemCode.Text != "Select")
                {
                    lblQuantity.Text = "";
                    DataRow[] duplicateRow = tempItemTable.Select($"[Item Name] = '{txtItemName.Text}'");
                    if (duplicateRow.Length > 0)
                    {
                        foreach (DataRow row in duplicateRow)
                        {
                            int currentQuantity = Convert.ToInt32(row["Quantity"]);
                            int currentAmount = Convert.ToInt32(row["Amount"]);
                           // row["Price"] = Convert.ToDecimal(txtPrice.Text);
                            // row["Quantity"] = Convert.ToInt32(txtQuantity.Text) + currentQuantity;
                            row["Quantity"] = Convert.ToInt32(txtQuantity.Text);
                            //row["Amount"] = Convert.ToDecimal(txtAmount.Text);
                            // row["Amount"] = currentAmount;
                        }
                    }
                    else
                    {
                        DataRow newRow = tempItemTable.NewRow();
                        newRow["Item Code"] = cboItemCode.Text.ToString();
                        newRow["Item Name"] = txtItemName.Text.ToString();
                       // newRow["Price"] = Convert.ToDecimal(txtPrice.Text);
                        newRow["Quantity"] = Convert.ToInt32(txtQuantity.Text);
                        //newRow["Amount"] = Convert.ToDecimal(txtAmount.Text);
                        tempItemTable.Rows.Add(newRow);
                    }
                    clear();
                    cboItemCode.Focus();
                }
            }
            else
            {
                MessageBox.Show("Please Select Item");
            }
        }
        private void clear()
        {
            cboItemCode.SelectedValue = 0;
            //  cboItemName.SelectedValue = 0;
          //  txtAmount.Text = "";
            //txtPrice.Text = "0";
            txtQuantity.Text = "";
        }

        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            bool itemFound = false;
            if (e.KeyCode == Keys.Enter)
            {
                if (cboItemCode.Text != "")
                {
                    if (txtQuantity.Text != "0")
                    {
                        addItemIngrid();
                    }
                    if (txtQuantity.Text == "0")
                    {
                        // Get the item name
                        string itemName = txtItemName.Text;

                        // Search for the item in the DataGridView
                        foreach (DataGridViewRow row in dgvItemDetails.Rows)
                        {
                            if (row.Cells["Item Name"].Value.ToString() == itemName)
                            {
                                // Remove the row from the DataGridView
                                dgvItemDetails.Rows.Remove(row);
                                // txtTotalAmount.Text = getTotalAmount();
                                itemFound = true;
                                clear();
                                break;
                            }
                        }
                        if (itemFound == false)
                        {
                            MessageBox.Show("Item not found");
                        }
                    }
                }
                else
                {
                    lblItemCode.Text = "Please select an Item";
                }
            }
        }

        private void dgvItemDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (lblQuantity.Text == "" && lblItemCode.Text == "")
            {
                saveRequirement();
            }

        }
        public void saveRequirement()
        {
            int Status = 0;
            bhojnalayPrintReceiptModel bhojnalayModel = new bhojnalayPrintReceiptModel();
            bhojnalayModel.SerialNo = txtReqID.Text;
            bhojnalayModel.ItemName = getSelectedItems().ToString();
            Status = obj.InsertReqToAdmin_MST(bhojnalayModel);
            if (Status != 0)
            {
                List<string> itemNameList = new List<string>(bhojnalayModel.ItemName.Split(','));
                List<int> itemIDs = new List<int>();
                foreach (var item in itemNameList)
                {
                    int ItemID = obj.getItemIdbyItemName(item.Trim());
                    bhojnalayModel.ItemId = ItemID;
                    foreach (DataGridViewRow row in dgvItemDetails.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            string itemName = row.Cells["Item Name"].Value.ToString();

                            if (itemName == item)
                            {
                                bhojnalayModel.PRINT_MST_ID = Status;
                                // bhojnalayModel.Price = Convert.ToDecimal(row.Cells["Price"].Value);
                                bhojnalayModel.Quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                                //bhojnalayModel.Amount = Convert.ToDecimal(row.Cells["Amount"].Value);
                                int id = obj.InsertRquToAdmin_DET(bhojnalayModel);
                            }
                        }
                    }
                }
                MessageBox.Show("Record Saved Successully");
                txtQuantity.Text = "";
                clearControl();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            clearControl();
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            decimal price = 0;
            decimal quantity = 0;
            decimal amount = 0;
            if (txtQuantity.Text != "")
            {
                quantity = Convert.ToDecimal(txtQuantity.Text);
                amount = price * quantity;
                if (Convert.ToInt32(txtQuantity.Text) > 99)
                {
                    lblQuantity.Text = "Quantity should not greater than 99";
                }
            }
        }

        private void pnlMaster_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmReqToAdmin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                btnSave.PerformClick();
            }
            if (e.Control && e.KeyCode == Keys.N)
            {
                btnNew.PerformClick();
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void txtItemName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        private void cboItemCode_TextChanged(object sender, EventArgs e)
        {
            UpdateSuggestions();
        }
    }
}
