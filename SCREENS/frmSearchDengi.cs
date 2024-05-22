using SGMOSOL.ADMIN;
using SGMOSOL.Custom_User_Contols;
using SGMOSOL.DAL;
using SGMOSOL.DataModel;
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

namespace SGMOSOL.SCREENS
{
    public partial class frmSearchDengi : Form
    {
        DengiReceiptDAL frmData;
        frmDengiReceipt frmDengi;
        CommonFunctions cm;
        SessionManager sessionManager;
        public frmSearchDengi()
        {
            InitializeComponent();
            // frmDengi = new frmDengiReceipt();
            this.MouseClick += frmSearchDengi_MouseClick;
            this.KeyDown += frmSearchDengi_KeyDown;
           
        }

        private void frmSearchDengi_Load(object sender, EventArgs e)
        {
            fillDengiType();
            fillDengiReceipt();
            dtFromDate.Format = DateTimePickerFormat.Custom;
            dtFromDate.CustomFormat = "dd/MM/yyyy";
            dtToDate.Format = DateTimePickerFormat.Custom;
            dtToDate.CustomFormat = "dd/MM/yyyy";
            //dtFromDate.va
           // sessionManager = new SessionManager(this);
           // sessionManager.StartTimer();
        }
        private void fillDengiReceipt()
        {
            try
            {
                frmData = new DengiReceiptDAL();
                dengiReceiptModel model = new dengiReceiptModel();
                model.receiptFno = txtFirstRecNo.Text;
                model.receiptLNo = txtLastRecNo.Text;
                model.Name = txtName.Text;
                model.receiptFDate = dtFromDate.Value; ;
                model.ReceiptLDate = dtToDate.Value;
                model.contact = txtMobile.Text;
                //model.DengiId = Convert.ToInt32(cboDengiType.SelectedValue);
                DataTable dataTable = new DataTable();
                dataTable = frmData.getDengiReceipt(model);
                if (dataTable.Rows.Count > 0)
                {
                    dgvDengiReceipt.DataSource = dataTable;
                    dgvDengiReceipt.Columns["DENGI_RECEIPT_ID"].Visible = false;
                    dgvDengiReceipt.RowHeadersVisible = true;

                    // Iterate through each row in the DataGridView
                    for (int i = 0; i < dgvDengiReceipt.Rows.Count; i++)
                    {
                        // Set the value of the row header to the sequential ID (starting from 1)
                        dgvDengiReceipt.Rows[i].HeaderCell.Value = (i + 1).ToString();
                    }
                }
                else dgvDengiReceipt.DataSource = null;
            }
            catch(Exception ex)
            {
                cm.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
        }
        private void fillDengiType()
        {
            cm = new CommonFunctions();
            DataTable dt = new DataTable();
            dt = cm.getDengiType();
            cboDengiType.DataSource = dt;
            cboDengiType.DisplayMember = "TYPE";
            cboDengiType.ValueMember = "DENGI_MST_ID";
            cboDengiType.SelectedValue = 4;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            fillDengiReceipt();
        }

        private void dgvDengiReceipt_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0)
            {
                // Set the background color for the header cell
                e.PaintBackground(e.CellBounds, true);
                e.Graphics.FillRectangle(Brushes.LightBlue, e.CellBounds);
                e.Graphics.DrawString(dgvDengiReceipt.Columns[e.ColumnIndex].HeaderText, e.CellStyle.Font, Brushes.Black, e.CellBounds, StringFormat.GenericDefault);
                e.Handled = true;
            }
        }

        private void dgvDengiReceipt_SelectionChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (dgvDengiReceipt.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvDengiReceipt.SelectedRows[0];
                dengiReceiptModel dengimodel = new dengiReceiptModel();
                dengimodel.serailId = Convert.ToInt32(selectedRow.Cells["SERIAL NUMBER"].Value);
                dt = fetchDengiReceipt(dengimodel.serailId.ToString());
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    dengimodel.Receipt_Id = row["DENGI_RECEIPT_ID"].ToString();
                    dengimodel.amount = Convert.ToDecimal(row["AMOUNT"]);
                    dengimodel.paymentTypeId = Convert.ToInt32(row["Payment_Type_id"]);
                    dengimodel.PanNo = row["PANNumber"].ToString();
                    dengimodel.PinCode = row["PINCODE"].ToString();
                    dengimodel.Name = row["NAME"].ToString();
                    dengimodel.contact = row["CONTACT"].ToString();
                    dengimodel.Address = row["ADDRESS"].ToString();
                    dengimodel.Taluka = row["TALUKA"].ToString();
                    dengimodel.chqbankname = row["CHQ_BANK_NAME"].ToString();
                    dengimodel.chno = row["CHQ_NO"].ToString();
                    dengimodel.ddbankname = row["DD_BANK_NAME"].ToString();
                    dengimodel.ddno = row["DD_NO"].ToString();
                    dengimodel.netbankname = row["NET_BANK_NAME"].ToString();
                    dengimodel.cardbankname = row["CARD_BANK_NAME"].ToString();
                    dengimodel.netbankrefnumber = row["NET_BANK_REF_NO"].ToString();
                    dengimodel.cardbankrefnumber = row["CARD_BANK_REF_NO"].ToString();
                    dengimodel.tidId = Convert.ToInt32(row["TID_ID"]);
                    dengimodel.Invoiceno = row["INVOICE_NO"].ToString();
                    dengimodel.dr_Date = Convert.ToDateTime(row["DR_DATE"]);
                    dengimodel.DengiId = Convert.ToInt32(row["DENGI_MST_ID"]);
                    if (row["GOTRA_NAME"] != DBNull.Value)
                    {
                        dengimodel.gotra = row["GOTRA_NAME"].ToString();
                    }
                    dengimodel.gotraId = Convert.ToInt32(row["GOTRA_ID"]);
                    if (row["DD_DATE"] != DBNull.Value)
                        dengimodel.dd_date = Convert.ToDateTime(row["DD_DATE"]);
                    dengimodel.DistId = Convert.ToInt32(row["DISTRICT_ID"]);
                    dengimodel.stateId = Convert.ToInt32(row["STATE_ID"]);
                    dengimodel.countryId = Convert.ToInt32(row["COUNTRY_ID"]);
                    dengimodel.Doc_type = row["DOC_TYPE"].ToString();
                    dengimodel.Doc_Detail = row["DOC_DETAIL"].ToString();
                    //  frmDengiReceipt frmDengi = new frmDengiReceipt();
                    frmDengi = Application.OpenForms.OfType<frmDengiReceipt>().FirstOrDefault();
                    if (frmDengi != null)
                    {
                        frmDengi.isPrint = true;
                        frmDengi.getAllData(dengimodel);
                       // this.Close();
                    }
                }



                //frmDengi = new frmDengiReceipt();

                //frmDengi.getAllData(dengimodel);
            }
        }
        public DataTable fetchDengiReceipt(string ReceiptId)
        {
            DataTable dt = new DataTable();
            frmData = new DengiReceiptDAL();
            dt = frmData.fetchDengiReceipt(ReceiptId);
            return dt;
        }
        private void OpenFormDengi()
        {
            // Check if FormDengi is already open
            frmDengiReceipt formDengi = Application.OpenForms.OfType<frmDengiReceipt>().FirstOrDefault();

            // If not open, create a new instance
            if (formDengi == null)
            {
                formDengi = new frmDengiReceipt();
                MDI mDI = new MDI();
                formDengi.MdiParent = mDI.MdiParent;  // Set the same MDI parent as FormSearch
                formDengi.Show();
            }
            else
            {
                // If already open, bring it to the front
                // MDI mDI = new MDI();
                //  formDengi.MdiParent = mDI.MdiParent;
                formDengi.Activate();
            }
        }

        private void txtMobile_TextChanged(object sender, EventArgs e)
        {
            string mobileNumber = txtMobile.Text.Trim();
            string pattern = @"^\d{10}$";
            if (Regex.IsMatch(mobileNumber, pattern))
            {
                lblmobile.Text = "";
            }
            else
            {
                lblmobile.Text = "Please enter a valid 10-digit mobile number.";
            }
        }

        private void dgvDengiReceipt_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                // Calculate total amount up to the selected row
                decimal totalAmount = 0;
                foreach (DataGridViewRow row in dgvDengiReceipt.SelectedRows)
                {
                    // Assuming "Amount" column is of type decimal
                    totalAmount += Convert.ToDecimal(row.Cells["Amount"].Value);
                }
                txttotalAMount.Text = totalAmount.ToString();
                lblAmountInwords.Text = cm.words(Convert.ToDouble(totalAmount));
                
            }
        }

        private void frmSearchDengi_MouseClick(object sender, MouseEventArgs e)
        {
          //  sessionManager.ResetSession();
        }

        private void frmSearchDengi_KeyDown(object sender, KeyEventArgs e)
        {
           // sessionManager.ResetSession();
        }
    }
}
