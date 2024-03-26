using SGMOSOL.ADMIN;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using Microsoft.Reporting.WinForms;

using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using System.Reflection;
using System.Drawing.Printing;



namespace SGMOSOL.SCREENS
{
    public partial class frmCalculation : Form
    {
        CommonFunctions cm;
        private DataTable tempItemTable;
        public frmCalculation()
        {
            InitializeComponent();

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void dtpPrnRcptDt_ValueChanged(object sender, EventArgs e)
        {

        }

        private void frmCalculation_Load(object sender, EventArgs e)
        {
            txtCounter.Text = UserInfo.Counter_Name;
            txtUser.Text = UserInfo.UserName;
            fillCurrency();
            createItemTable();
            this.reportViewer1.RefreshReport();
            reportViewer1.Visible = false;
        }
        public void fillCurrency()
        {
            cm = new CommonFunctions();
            DataTable dt = new DataTable();
            dt = cm.getCurrency();
            DataRow newRow = dt.NewRow();
            newRow["Lookup_Value_Name"] = "Select";
            newRow["Lookup_Value_Order"] = 0;
            dt.Rows.InsertAt(newRow, 0);
            cboCurrency.DataSource = dt;
            cboCurrency.DisplayMember = "Lookup_Value_Name";
            cboCurrency.ValueMember = "Lookup_Value_Order";
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            int intCurrency = 0;
            int intQuantity = 0;
            if (cboCurrency.Text != "Select" && txtQuantity.Text != "")
            {
                intCurrency = Convert.ToInt32(cboCurrency.Text);
                intQuantity = Convert.ToInt32(txtQuantity.Text);
                txtAmount.Text = (intCurrency * intQuantity).ToString();
            }
        }
        private void createItemTable()
        {
            dgvItemDetails.Columns.Clear();
            tempItemTable = new DataTable("ItemTable");
            tempItemTable.Columns.Add("ID", typeof(int));
            tempItemTable.Columns.Add("Currency", typeof(string));
            tempItemTable.Columns.Add("Quantity", typeof(string));
            tempItemTable.Columns.Add("Amount", typeof(decimal));

            tempItemTable.Columns["ID"].AutoIncrement = true;
            tempItemTable.Columns["ID"].AutoIncrementSeed = 1;
            tempItemTable.Columns["ID"].AutoIncrementStep = 1;
            dgvItemDetails.DataSource = tempItemTable;
            DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn();
            editButtonColumn.Name = "EditButton";
            editButtonColumn.HeaderText = "Edit";
            editButtonColumn.Text = "Edit";
            editButtonColumn.UseColumnTextForButtonValue = true;
            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
            deleteButtonColumn.Name = "DeleteButton";
            deleteButtonColumn.HeaderText = "Delete";
            deleteButtonColumn.Text = "Delete";
            deleteButtonColumn.UseColumnTextForButtonValue = true;
            dgvItemDetails.Columns.Add(editButtonColumn);
            dgvItemDetails.Columns.Add(deleteButtonColumn);
            foreach (DataGridViewColumn column in dgvItemDetails.Columns)
            {
                if (column.Name != "Quantity")
                    column.ReadOnly = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cboCurrency.Text == "Select")
            {
                lblCurrency.Text = "Please select Currency";
            }
            else
            {
                lblCurrency.Text = "";
            }
            if (txtQuantity.Text == "")
            {
                lblQuntity.Text = "Please add Quantity";
            }
            else
            {
                lblQuntity.Text = "";
            }
            if (txtQuantity.Text == "0")
            {
                lblQuntity.Text = "Quantity Can not be 0";
            }
            if (btnAdd.Text == "Add")
            {
                if (txtQuantity.Text != "" && cboCurrency.Text != "Select" && txtQuantity.Text != "0")
                {
                    //lblQuantity.Text = "";
                    DataRow[] duplicateRow = tempItemTable.Select($"[Currency] = '{cboCurrency.Text}'");
                    if (duplicateRow.Length > 0)
                    {
                        MessageBox.Show("Record already Exist");

                        //foreach (DataRow row in duplicateRow)
                        //{
                        //    int currentQuantity = Convert.ToInt32(row["Quantity"]);
                        //    int currentAmount = Convert.ToInt32(row["Amount"]);
                        //    row["Quantity"] = Convert.ToInt32(txtQuantity.Text) + currentQuantity;
                        //    row["Amount"] = Convert.ToDecimal(txtAmount.Text) + currentAmount;
                        //}
                    }
                    else
                    {
                        DataRow newRow = tempItemTable.NewRow();
                        newRow["Currency"] = cboCurrency.Text.ToString();
                        newRow["Quantity"] = txtQuantity.Text.ToString();
                        newRow["Amount"] = Convert.ToDecimal(txtAmount.Text);
                        tempItemTable.Rows.Add(newRow);
                    }
                    txtTotalAmount.Text = getTotalAmount();
                    clear();
                }
                else
                {
                    // lblQuantity.Text = "Please add Details";
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
                    row["Currency"] = cboCurrency.Text.ToString();
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
        private void clear()
        {
            cboCurrency.SelectedValue = 0;
            txtAmount.Text = "";
            //txtPrice.Text = "0";
            txtQuantity.Text = "";
        }


        private void dgvItemDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvItemDetails.Columns["EditButton"].Index)
            {
                ClearRowSelection();
                dgvItemDetails.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightPink;
                DataGridViewRow selectedRow = dgvItemDetails.Rows[e.RowIndex];
                int id = Convert.ToInt32(selectedRow.Cells["ID"].Value);
                cboCurrency.Text = Convert.ToString(selectedRow.Cells["Currency"].Value);
                txtQuantity.Text = Convert.ToString(selectedRow.Cells["Quantity"].Value);
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

        private void btnNew_Click(object sender, EventArgs e)
        {
            clear();
            btnAdd.Text = "Add";
            //dgvItemDetails.DataSource = null;
            createItemTable();

        }
        public bool CheckQuantityValue()
        {
            bool foundZeroQuantity = false;
            foreach (DataGridViewRow row in dgvItemDetails.Rows)
            {
                if (!row.IsNewRow && row.Cells["Quantity"].Value != null)
                {
                    if (row.Cells["Quantity"].Value.ToString() == "0")
                    {
                        foundZeroQuantity = true;
                    }
                }
            }
            return foundZeroQuantity;
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (CheckQuantityValue())
            {
                lblAlertMsg.Text = "Quantity Can not 0 !!!";
            }
            else
            {
                lblAlertMsg.Text = "";
                cm = new CommonFunctions();
                DataTable dataGridViewData = new DataTable();
                foreach (DataGridViewColumn column in dgvItemDetails.Columns)
                {
                    dataGridViewData.Columns.Add(column.HeaderText);
                }
                foreach (DataGridViewRow row in dgvItemDetails.Rows)
                {
                    DataRow dataRow = dataGridViewData.NewRow();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dataRow[cell.ColumnIndex] = cell.Value;
                    }
                    dataGridViewData.Rows.Add(dataRow);
                }
                ReportParameter[] parameters = new ReportParameter[5];
                parameters[0] = new ReportParameter("CounterName", txtCounter.Text);
                parameters[1] = new ReportParameter("UserName", txtUser.Text);
                parameters[2] = new ReportParameter("Date", dtpPrnRcptDt.Text);
                parameters[3] = new ReportParameter("TotalAmount", txtTotalAmount.Text);
                parameters[4] = new ReportParameter("TotalAmountInWords", cm.words(Convert.ToDouble(txtTotalAmount.Text)));
                reportViewer1.LocalReport.SetParameters(parameters);
                ReportDataSource reportDataSource = new ReportDataSource("DataSet1", dataGridViewData);
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(reportDataSource);
                this.reportViewer1.RefreshReport();
                printReport("TotalFundReport");
            }
        }
        public void printReport(string docName)
        {
            string printerName = System.Configuration.ConfigurationManager.AppSettings["Printer_name"].ToString();

            byte[] renderedBytes = reportViewer1.LocalReport.Render("Image");
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(renderedBytes))
            {
                using (System.Drawing.Image image = System.Drawing.Image.FromStream(stream))
                {
                    try
                    {
                        PrintDocument printDoc = new PrintDocument();
                        printDoc.PrinterSettings.PrinterName = printerName;
                        printDoc.DocumentName = docName;
                        PaperSize paperSize = new PaperSize("Custom", (int)(4.84 * 100), (int)(5.70 * 100));
                        printDoc.DefaultPageSettings.PaperSize = paperSize;
                        printDoc.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
                        printDoc.PrintPage += (s, args) =>
                        {
                            args.Graphics.DrawImage(image, args.MarginBounds);
                        };
                        printDoc.Print();
                    }
                    catch (Exception ex)
                    {
                        cm.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                    }
                }
            }

        }

        private void txtTotalAmount_TextChanged(object sender, EventArgs e)
        {
            if (txtTotalAmount.Text != "")
            {
                lblAmountInWords.Text = cm.words(Convert.ToDouble(txtTotalAmount.Text));
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvItemDetails_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvItemDetails.Columns["Quantity"].Index && e.RowIndex >= 0)
            {
                // Get the quantity and price values
                int quantity = Convert.ToInt32(dgvItemDetails.Rows[e.RowIndex].Cells["Quantity"].Value);
                decimal Currency = Convert.ToDecimal(dgvItemDetails.Rows[e.RowIndex].Cells["Currency"].Value);

                // Calculate the amount
                decimal amount = quantity * Currency;

                // Update the "Amount" cell
                dgvItemDetails.Rows[e.RowIndex].Cells["Amount"].Value = amount;
            }
        }
    }
}
