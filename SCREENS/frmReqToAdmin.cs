using SGMOSOL.ADMIN;
using SGMOSOL.BAL;
using SGMOSOL.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGMOSOL.SCREENS
{
    public partial class frmReqToAdmin : Form
    {
        ReqToAdminBAL obj = new ReqToAdminBAL();
        private DataTable tempItemTable;
        public frmReqToAdmin()
        {
            InitializeComponent();
        }

        private void frmReqToAdmin_Load(object sender, EventArgs e)
        {
            txtCounter.Text = UserInfo.Counter_Name;
            getRequirementber();
            txtUser.Text = UserInfo.UserName;
            fillItemCode();
            createItemTable();
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
            DataRow newRow = dt.NewRow();
            newRow["ITEM_CODE"] = "Select";
            newRow["ITEM_ID"] = 0;
            dt.Rows.InsertAt(newRow, 0);
            cboItemCode.DataSource = dt;
            cboItemCode.DisplayMember = "ITEM_CODE";
            cboItemCode.ValueMember = "ITEM_ID";
            cboItemCode.SelectedValue = 0;
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
            if (txtQuantity.Text != "" && cboItemCode.Text != "Select")
            {
               // lblQuantity.Text = "";
                DataRow[] duplicateRow = tempItemTable.Select($"[Item Name] = '{txtItemName.Text}'");
                if (duplicateRow.Length > 0)
                {
                    foreach (DataRow row in duplicateRow)
                    {
                        int currentQuantity = Convert.ToInt32(row["Quantity"]);
                        //int currentAmount = Convert.ToInt32(row["Amount"]);
                       // row["Price"] = Convert.ToDecimal(txtPrice.Text);
                        row["Quantity"] = Convert.ToInt32(txtQuantity.Text) + currentQuantity;
                       // row["Amount"] = Convert.ToDecimal(txtAmount.Text) + currentAmount;
                    }
                }
                else
                {
                    DataRow newRow = tempItemTable.NewRow();
                    newRow["Item Code"] = cboItemCode.Text.ToString();
                    newRow["Item Name"] = txtItemName.Text.ToString();
                   // newRow["Price"] = Convert.ToDecimal(txtPrice.Text);
                    newRow["Quantity"] = Convert.ToInt32(txtQuantity.Text);
                   // newRow["Amount"] = Convert.ToDecimal(txtAmount.Text);
                    tempItemTable.Rows.Add(newRow);
                }
                //txtTotalAmount.Text = getTotalAmount();
               // clear();
                cboItemCode.Focus();
            }
        }

        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                addItemIngrid();
            }
        }

        private void dgvItemDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvItemDetails.Columns["DeleteButton"].Index)
            {
               // ClearRowSelection();
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
    }
}
