using SGMOSOL.BAL;
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

namespace SGMOSOL.SCREENS
{
    public partial class frmBhojnalaySearch : Form
    {
        BhojnalayPrintReceiptBAL ba = null;
        bhojnalayPrintReceiptModel model = null;
        public frmBhojnalaySearch()
        {
            InitializeComponent();
        }

        private void frmBhojnalaySearch_Load(object sender, EventArgs e)
        {
            ba = new BhojnalayPrintReceiptBAL();
            dtFromDate.Value = DateTime.Now;
            dtToDate.Value = DateTime.Now;
            getAllData();
          //  this.reportViewer1.RefreshReport();
        }
        public void getAllData()
        {
            model = new bhojnalayPrintReceiptModel();
            model.Name = txtName.Text;
            model.receiptFDate = dtFromDate.Value;
            model.ReceiptLDate = dtToDate.Value;
            model.receiptFno = txtFirstRecNo.Text;
            model.receiptLNo = txtLastRecNo.Text;
            DataTable dt = new DataTable();
            dt = ba.getAllData(model);
            if (dt != null)
            {
                dgvbhojnalay.DataSource = dt;
                dgvbhojnalay.Columns["PRINT_RECEIPT_MST_ID"].Visible = false;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            getAllData();
        }

        private void dgvbhojnalay_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                e.Graphics.FillRectangle(Brushes.LightBlue, e.CellBounds);
                e.Graphics.DrawString(dgvbhojnalay.Columns[e.ColumnIndex].HeaderText, e.CellStyle.Font, Brushes.Black, e.CellBounds, StringFormat.GenericDefault);
                e.Handled = true;
            }

        }

        private void dgvbhojnalay_SelectionChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataTable dtItem = new DataTable();
            if (dgvbhojnalay.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvbhojnalay.SelectedRows[0];
                bhojnalayPrintReceiptModel model = new bhojnalayPrintReceiptModel();
                model.PRINT_MST_ID = Convert.ToDecimal(selectedRow.Cells["PRINT_RECEIPT_MST_ID"].Value);
                dt = ba.getDataByReceiptID(model.PRINT_MST_ID.ToString());
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    model.SerialNo = row["SERIAL_NO"].ToString();
                    model.Name = row["Name"].ToString();
                    model.Mobile = row["MOBILE"].ToString();
                    model.Address = row["ADDRESS"].ToString();
                    model.Taluka = row["TALUKA"].ToString();
                    model.DocType = row["DOC_TYPE"].ToString();
                    model.DocTypeDetail = row["DOC_DETAIL"].ToString();
                    model.TotalAmount = Convert.ToDecimal(row["AMOUNT"].ToString());
                    frmBhojnalayaPrintReceipt frm = new frmBhojnalayaPrintReceipt();
                    frm = Application.OpenForms.OfType<frmBhojnalayaPrintReceipt>().FirstOrDefault();
                    if (frm != null)
                    {
                        frm.isPrint = true;
                        frm.getAllData(model);
                        this.Close();
                    }
                    this.Close();
                }
            }
        }
    }
}
