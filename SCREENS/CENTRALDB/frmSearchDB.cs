using iTextSharp.text.xml;
using SGMOSOL.ADMIN;
using SGMOSOL.DAL.CENTRALDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGMOSOL.SCREENS.CENTRALDB
{
    public partial class frmSearchDB : Form
    {
        frmSearchDAL obj;
        CommonFunctions commonFunctions;
        private DataTable selectedRowData = new DataTable();
        frmDengiReceipt frmDengi = null;
        string Barcode = null;
        string TableName = null;
        public frmSearchDB()
        {
            InitializeComponent();
        }

        private void frmSearchDB_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Central Form";
            obj = new frmSearchDAL();
            cboSearch.Text = "BARCODE";
            commonFunctions = new CommonFunctions();
            txtSearch.KeyDown += new KeyEventHandler(txtSearch_KeyDown);
            btnShow.Click += new EventHandler(btnShow_Click);
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (txtSearch.Text != "")
            {
                dt = obj.checkRecordinCB(txtSearch.Text, cboSearch.Text);
            }
            if (dt.Rows.Count > 0)
            {
                dgvDataLoad.DataSource = dt;
                dgvDataLoad.Columns["BHAKT_TABLE"].Visible = false;
                dgvDataLoad.RowHeadersVisible = true;

                // Iterate through each row in the DataGridView
                for (int i = 0; i < dgvDataLoad.Rows.Count; i++)
                {
                    // Set the value of the row header to the sequential ID (starting from 1)
                    dgvDataLoad.Rows[i].HeaderCell.Value = (i + 1).ToString();
                }
                if (dgvDataLoad.Rows.Count > 0)
                {
                    // Select the first row
                    dgvDataLoad.CurrentCell = dgvDataLoad.Rows[0].Cells[0];
                    dgvDataLoad.Rows[0].Selected = true;
                    var selectedRow = dgvDataLoad.Rows[0];
                    var dataRowView = selectedRow.DataBoundItem as DataRowView;

                    if (dataRowView != null)
                    {
                        selectedRowData = dataRowView.Row.Table.Clone();
                        selectedRowData.ImportRow(dataRowView.Row);
                        btnLoad.Focus();
                    }
                }
            }
            else
            {
                lblAlert.Text = "NO RECORD FOUND";
                dgvDataLoad.DataSource = null;
                btnClose.Focus();
            }
        }

        private void cboSearch_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            // string barcode = null;
            //  string tableName = null;
            DataTable dt = new DataTable();
            if (selectedRowData != null && selectedRowData.Rows.Count > 0)
            {
                // Loop through each row in selectedRowData
                foreach (DataRow row in selectedRowData.Rows)
                {
                    // Check if the column name is "barcode"
                    if (row.Table.Columns.Contains("BARCODE")) // Assuming "barcode" is the column name
                    {
                        Barcode = row["BARCODE"].ToString();

                    }
                    if (row.Table.Columns.Contains("BHAKT_TABLE")) // Assuming "barcode" is the column name
                    {

                        TableName = row["BHAKT_TABLE"].ToString();

                    }
                }
                dt = obj.LoadTransaction(TableName, Barcode);

                frmDengi = Application.OpenForms.OfType<frmDengiReceipt>().FirstOrDefault();
                if (frmDengi != null)
                {
                    //frmDengi.isPrint = true;
                    frmDengi.getLoadTransaction(dt, Barcode);
                    this.Close();
                }
            }
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            try
            {
                if (keyData == Keys.Enter)
                {
                    if (ActiveControl is System.Windows.Forms.Button btnLoad)
                    {
                        if (btnLoad.Name == "btnLoad")
                        {
                            btnLoad.PerformClick();
                            return true;
                        }
                    }
                    if (ActiveControl is System.Windows.Forms.Button btnNew)
                    {
                        if (btnShow.Name == "btnShow")
                        {
                            btnShow.PerformClick();
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
                    if (ActiveControl is System.Windows.Forms.Button btnClear)
                    {
                        if (btnClear.Name == "btnClear")
                        {
                            btnClear.PerformClick();
                            return true;
                        }
                    }
                    if (ActiveControl is System.Windows.Forms.TextBox txtSearch)
                    {
                        if (txtSearch.Name == "txtSearch")
                        {
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

        private void dgvDataLoad_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dgvDataLoad.Rows[e.RowIndex];
                selectedRowData = ((DataRowView)selectedRow.DataBoundItem).Row.Table.Clone();
                selectedRowData.ImportRow(((DataRowView)selectedRow.DataBoundItem).Row);
            }
        }

        private void btnNewBhakt_Click(object sender, EventArgs e)
        {
            Barcode = "NEW";
            // TableName = "NEW";
            DataTable dt = new DataTable();
            frmDengi = Application.OpenForms.OfType<frmDengiReceipt>().FirstOrDefault();
            if (frmDengi != null)
            {
                //frmDengi.isPrint = true;
                frmDengi.getLoadTransaction(dt = null, Barcode);
                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnShow.PerformClick();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (cboSearch.Text == "MOBILE")
            {
                string mobileNumber = txtSearch.Text.Trim();
                string pattern = @"^\d{10}$";
                if (Regex.IsMatch(mobileNumber, pattern))
                {
                    lblAlert.Text = "";
                }
                else
                {
                    lblAlert.Text = "Please enter a valid 10-digit mobile number.";
                }
            }
        }
    }
}
