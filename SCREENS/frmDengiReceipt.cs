using Microsoft.Reporting.WebForms;
using SGMOSOL.ADMIN;
using SGMOSOL.DAL;
using SGMOSOL.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SGMOSOL.SCREENS
{
    public partial class frmDengiReceipt : Form
    {
        private CommonFunctions commonFunctions;
        frmUserDengi userDengi;
        DengiReceiptDAL dengiReceiptDAL;
        frmSearchDengi frmSearch;
        dengiReceiptModel dengiReceiptModel;
        frmReportViewer frmreport;
        DataTable dt;
        public bool isPrint = false;
        public frmDengiReceipt()
        {
            InitializeComponent();
            CenterToParent();
            this.KeyDown -= frmDengiReceipt_KeyDown;
            this.KeyDown += new KeyEventHandler(frmDengiReceipt_KeyDown);
            this.KeyPreview = true;
            userDengi = new frmUserDengi();
            this.commonFunctions = new CommonFunctions();
            dt = new DataTable();
            txtname.TextChanged += txtname_TextChanged;
            txtmob.TextChanged += txtmob_TextChanged;
            txtAmount.TextChanged += txtAmount_TextChanged;
            txtPANNo.TextChanged += txtPANNo_TextChanged;
            txtaddr.TextChanged += txtaddr_TextChanged;
            btnSave.Click += btnSave_Click;
            cboState.SelectedIndexChanged += cboState_SelectedIndexChanged;
            UserInfo.module = "Dengi";
        }
        protected override bool ProcessDialogKey(Keys keyData)
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
                if (ActiveControl is System.Windows.Forms.Button btnAcknowledge)
                {
                    if (btnAcknowledge.Name == "btnAcknowledge")
                    {
                        btnAcknowledge.PerformClick();
                        return true;
                    }
                }
                SendKeys.Send("{TAB}");
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void frmDengiReceipt_Load(object sender, EventArgs e)
        {
            int centerX = (ClientSize.Width - flowLayoutPanel1.Width) / 2;
            int centerY = (ClientSize.Height - flowLayoutPanel1.Height) / 2;
            flowLayoutPanel1.Location = new System.Drawing.Point(centerX, centerY);
            userDengi.Show();
            txtAmount.Focus();
            txtCounter.Text = UserInfo.Counter_Name;
            txtUser.Text = UserInfo.UserName;
            if (isPrint == false)
            {
                FillDocumentType();
                fillCountry();
                fillStateByCountryId();
                fillDistrictbyStateId(Convert.ToInt32(cboState.SelectedValue));
                fillPaymentMode();
                fillDengiType();
                fillGotra();
                showPanel();
                getDengiNo();
            }
        }
        private void getDengiNo()
        {
            dengiReceiptDAL = new DengiReceiptDAL();
            int dengiID = dengiReceiptDAL.getDenigNumber();
            txtdengireceiptNo.Text = dengiID.ToString();
        }
        private void FillDocumentType()
        {
            dt = commonFunctions.GetDocumentType();
            cboDoctype.DataSource = dt;
            cboDoctype.DisplayMember = "DocumentName";
            cboDoctype.ValueMember = "DocumentID";
        }
        private void fillCountry()
        {
            dt = commonFunctions.getCountry();
            cboCountry.DataSource = dt;
            cboCountry.DisplayMember = "CountryName";
            cboCountry.ValueMember = "CountryID";
        }
        private void fillTId()
        {
            dt = commonFunctions.fillTID();
            cobTid.DataSource = dt;
            cobTid.DisplayMember = "tidNo";
            cobTid.ValueMember = "Tid";
        }
        private void fillStateByCountryId()
        {
            dt = commonFunctions.getStateById(102);
            cboState.DataSource = dt;
            cboState.DisplayMember = "StateName";
            cboState.ValueMember = "StateId";
            object selectedValue = cboCountry.SelectedValue;
            if (selectedValue != null)
            {
                string selectedValueAsString = selectedValue.ToString();
                if (selectedValueAsString == "102")
                {
                    cboState.SelectedValue = 21;
                }
            }
        }
        private void fillDistrictbyStateId(int stateId)
        {
            object selectedValue = cboState.SelectedValue;
            if (stateId != 0)
            {
                dt = commonFunctions.getDisctrictbyStateId(stateId);
                DataRow newRow = dt.NewRow();
                newRow["DistrictName"] = "Select";
                newRow["DistrictId"] = 0;
                dt.Rows.InsertAt(newRow, 0);
                cboDistrict.DataSource = dt;
                cboDistrict.DisplayMember = "DistrictName";
                cboDistrict.ValueMember = "DistrictId";
            }
        }
        private void fillPaymentMode()
        {
            dt = commonFunctions.getPaymentMode();
            cboPaymentType.DataSource = dt;
            cboPaymentType.DisplayMember = "Token_Detail_Name";
            cboPaymentType.ValueMember = "Token_Detail_Id";
            cboPaymentType.SelectedValue = 8;
        }
        private void fillDengiType()
        {
            dt = commonFunctions.getDengiType();
            cboDengiType.DataSource = dt;
            cboDengiType.DisplayMember = "TYPE";
            cboDengiType.ValueMember = "DENGI_MST_ID";
            cboDengiType.SelectedValue = 4;
        }
        private void fillGotra()
        {
            dt = commonFunctions.getGotra();
            DataRow newRow = dt.NewRow();
            DataRow row = dt.NewRow();
            newRow["gotra_name"] = "Select";
            newRow["gotra_id"] = 0;
            row["gotra_name"] = "OTHER";
            row["gotra_id"] = 9999;
            dt.Rows.InsertAt(newRow, 0);
            dt.Rows.InsertAt(row, 9999);
            cboGotra.DataSource = dt;
            cboGotra.DisplayMember = "gotra_name";
            cboGotra.ValueMember = "gotra_id";
            cboGotra.SelectedValue = 0;
        }
        private void cboPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataOnPaymentMode();
            lblPaymentMode.Text = "";
        }
        public void clearControl()
        {
            txtAddGotra.Text = "";
            txtaddr.Text = "";
            txtAmount.Text = "";
            txtChqBankname.Text = "";
            txtChqNo.Text = "";
            txtDDBankName.Text = "";
            txtDDNo.Text = "";
            txtdengireceiptNo.Text = "";
            txtdocDetail.Text = "";
            txtInvoice.Text = "";
            txtmob.Text = "";
            txtname.Text = "";
            txtNetBankName.Text = "";
            txtNetRefNo.Text = "";
            cboDoctype.Text = "";
            cboGotra.Text = "";
            dtpPrnRcptDt.Text = "";
            dtChqDt.Text = "";
            dtDDdate.Text = "";
            lblMobile.Text = "";
            lblAdd.Text = "";
        }
        public void dataOnPaymentMode()
        {
            if (cboPaymentType.Text == "Cheque")
            {
                pnlChqDtl.Visible = true;
                pnlDDDtl.Visible = false;
                pnlNetDtl.Visible = false;
                pnlswap.Visible = false;
            }
            if (cboPaymentType.Text == "Cash")
            {
                pnlChqDtl.Visible = false;
                pnlDDDtl.Visible = false;
                pnlNetDtl.Visible = false;
                pnlswap.Visible = false;
            }
            if (cboPaymentType.Text == "Net Transfer")
            {
                pnlChqDtl.Visible = false;
                pnlDDDtl.Visible = false;
                pnlNetDtl.Visible = true;
                pnlswap.Visible = false;
            }
            if (cboPaymentType.Text == "Swipe")
            {
                fillTId();
                pnlChqDtl.Visible = false;
                pnlDDDtl.Visible = false;
                pnlNetDtl.Visible = false;
                pnlswap.Visible = true;
            }
            if (cboPaymentType.Text == "Demand Draft")
            {
                pnlChqDtl.Visible = false;
                pnlDDDtl.Visible = true;
                pnlNetDtl.Visible = false;
                pnlswap.Visible = false;
            }

        }

        private void lockControls()
        {
            txtAddGotra.Enabled = false;
            txtaddr.Enabled = false;
            txtAmount.Enabled = false;
            txtChqBankname.Enabled = false;
            txtChqNo.Enabled = false;
            txtDDBankName.Enabled = false;
            txtDDNo.Enabled = false;
            txtdengireceiptNo.Enabled = false;
            txtdocDetail.Enabled = false;
            txtInvoice.Enabled = false;
            txtmob.Enabled = false;
            txtname.Enabled = false;
            txtNetBankName.Enabled = false;
            txtNetRefNo.Enabled = false;
            cboCountry.Enabled = false;
            cboDengiType.Enabled = false;
            cboPaymentType.Enabled = false;
            cboState.Enabled = false;
            cboDistrict.Enabled = false;
            cboDoctype.Enabled = false;
            cboGotra.Enabled = false;
            dtpPrnRcptDt.Enabled = false;
            dtChqDt.Enabled = false;
            dtDDdate.Enabled = false;
            txtAddGotra.Enabled = false;
            txtPincode.Enabled = false;
            txttal.Enabled = false;
            chkDeclaration.Enabled = false;
            chkScanDoc.Enabled = false;
            btnScan.Enabled = false;
            btnClear.Enabled = false;

        }

        private void unLockControls()
        {
            txtAddGotra.Enabled = true;
            txtaddr.Enabled = true;
            txtAmount.Enabled = true;
            txtChqBankname.Enabled = true;
            txtChqNo.Enabled = true;
            txtDDBankName.Enabled = true;
            txtDDNo.Enabled = true;
            txtdocDetail.Enabled = true;
            txtInvoice.Enabled = true;
            txtmob.Enabled = true;
            txtname.Enabled = true;
            txtNetBankName.Enabled = true;
            txtNetRefNo.Enabled = true;
            cboCountry.Enabled = true;
            cboDengiType.Enabled = true;
            cboPaymentType.Enabled = true;
            cboState.Enabled = true;
            cboDistrict.Enabled = true;
            cboDoctype.Enabled = true;
            cboGotra.Enabled = true;
            dtChqDt.Enabled = true;
            dtDDdate.Enabled = true;
            txtAddGotra.Enabled = true;
            txttal.Enabled = true;
            txtPincode.Enabled = true;
            chkDeclaration.Enabled = true;
            chkScanDoc.Enabled = true;
            btnScan.Enabled = true;
            btnClear.Enabled = true;
            btnSave.Enabled = true;
            btnPrint.Enabled = false;
            //btnAcknowledge.Enabled = false;
            btnAcknowledge.Enabled = true;

        }

        private void showPanel()
        {
            pnlChqDtl.Visible = false;
            pnlDDDtl.Visible = false;
            pnlNetDtl.Visible = false;
            pnlswap.Visible = false;
            pnlDetail.Location = new Point(pnlChqDtl.Location.X, pnlChqDtl.Location.Y - pnlMaster.Height);
        }
        private void resetAllFields()
        {
            txtAddGotra.Text = "";
            txtAmount.Text = "";
            txtaddr.Text = "";
            txtAddGotra.Text = "";
            txtChqBankname.Text = "";
            txtChqNo.Text = "";
            txtDDBankName.Text = "";
            txtDDNo.Text = "";
            txtInvoice.Text = "";
            txtmob.Text = "";
            txtname.Text = "";
            txtNetBankName.Text = "";
            txtNetRefNo.Text = "";
            txtPANNo.Text = "";
            txtPincode.Text = "";
            txtScan.Text = "";
            cboCountry.SelectedIndex = 0;
            cboDengiType.SelectedIndex = 1;
            cboDistrict.SelectedIndex = 0;
            cboGotra.SelectedIndex = 0;
            cboPaymentType.SelectedIndex = 1;
            cboState.SelectedValue = 21;
            txtdocDetail.Text = "";
            cboDoctype.Text = "";
            txttal.Text = "";
            txtAmount.Focus();
            lblAdd.Text = "";
            lblamount.Text = "";
            lblAmtWords.Text = "";
            lbldocdetailerr.Text = "";
            lbldoctype_err.Text = "";
            lblgotra.Text = "";
            lblMobile.Text = "";
            lblName.Text = "";
            lblpan.Text = "";
            lblPincode.Text = "";
            lblScan.Text = "";
            lblTaluka.Text = "";
            lblPincode.Text = "";
            dtpPrnRcptDt.Enabled = false;
        }
        private void txtname_TextChanged(object sender, EventArgs e)
        {
            if (txtname.Text != "")
            {
                lblName.Text = "";
            }
            userDengi.SetText(txtname.Text);
            userDengi.Show();
        }

        private void txtmob_TextChanged(object sender, EventArgs e)
        {
            string mobileNumber = txtmob.Text.Trim();
            string pattern = @"^\d{10}$";
            if (Regex.IsMatch(mobileNumber, pattern))
            {
                lblMobile.Text = "";
            }
            else
            {
                lblMobile.Text = "Please enter a valid 10-digit mobile number.";
            }
            userDengi.Setmobile(txtmob.Text);
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            int value;

            if (txtAmount.Text != "")
            {
                txtAmount.Text = Convert.ToString(Convert.ToDouble(txtAmount.Text));
                lblamount.Text = "";
            }
            if (txtAmount.Text != "")
            {
                lblAmtWords.Text = commonFunctions.words(Convert.ToInt32(txtAmount.Text));
            }
            else
            {
                lblAmtWords.Text = "Amount in Words : ";
            }
            if (int.TryParse(txtAmount.Text, out value) && value >= 500)
            {
                lbldoctype_err.Text = "Please select document type";
            }
            if (cboDoctype.Text != "")
            {
                lbldoctype_err.Text = "";
            }
            else
            {
                lblpan.Text = "";
                lbldoctype_err.Text = "";
            }
            if (txtAmount.Text == "0")
            {
                lblamount.Text = "Amount can not be 0!";
            }
            userDengi.SetAmount(txtAmount.Text);
            userDengi.SetAmtInWord(lblAmtWords.Text);
            userDengi.Show();
        }
        private void txtPANNo_TextChanged(object sender, EventArgs e)
        {
            int value;
            if (int.TryParse(txtAmount.Text, out value) && value >= 500 && txtPANNo.Text == "")
            {
                lblpan.Text = "Please enter Pan Number";
            }
            else
            {
                lblpan.Text = "";
            }
            //userDengi.SetPan(txtPANNo.Text);
            userDengi.Show();
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            resetAllFields();
            getDengiNo();
            unLockControls();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string lstEnteredName = null;
            string lstEnteredAmount = null;
            if (txtmob.Text == "")
            {
                lblMobile.Text = "Please Enter Mobile number";
            }
            if (txtaddr.Text == "")
            {
                lblAdd.Text = "Please Enter Address";
            }
            if (txtAmount.Text == "")
            {
                lblamount.Text = "Please Enter amount";
            }
            if (txtname.Text == "")
            {
                lblName.Text = "Please Enter Name";
            }
            if (txttal.Text == "")
            {
                lblTaluka.Text = "Please Enter Taluka";
            }
            if (cboDistrict.Text.ToUpper() == "SELECT")
            {
                lblDistrict.Text = "Please Select District";
            }
            if (txtAmount.Text != "")
            {
                if (Convert.ToInt32(txtAmount.Text) >= 500)
                {
                    if (cboDoctype.Text != "" && txtdocDetail.Text != "")
                    {
                        CheckValidDocs();
                    }
                    else
                    {
                        if (cboDoctype.Text == "Select")
                        {
                            lbldoctype_err.Text = "Please select Document Type";
                        }
                        else { lbldoctype_err.Text = ""; }
                        if (txtdocDetail.Text == "")
                        {
                            lbldocdetailerr.Text = "Please enter Document details";
                        }
                        else { lbldocdetailerr.Text = ""; }
                    }
                }
            }
            //if (cboDoctype.Text != "" && txtdocDetail.Text == "")
            //{
            //    lbldocdetailerr.Text = "Please enter Document details";
            //}
            //else 
            //{
            //    lbldocdetailerr.Text = "";
            //}
            if (cboPaymentType.Text == "Swipe" && cobTid.Text == "")
            {
                lblPaymentMode.Text = "Tid not found, Please change paymenttype.";
            }


            if (lblAdd.Text == "" && lblMobile.Text == "" && lbldoctype_err.Text == "" && lbldocdetailerr.Text == "" && lblName.Text == "" && lblTaluka.Text == "" && lblDistrict.Text == "")
            {
                if (txtAmount.Text != "")
                {
                    if (Convert.ToInt32(txtAmount.Text) >= 500 && Convert.ToInt32(txtAmount.Text) <= 999)
                    {
                        lblamount.Text = "Amount :- " + txtAmount.Text + "Please check carefully . Press F1";
                    }
                    else if (Convert.ToInt32(txtAmount.Text) >= 1000 && Convert.ToInt32(txtAmount.Text) <= 4999)
                    {
                        lblamount.Text = "Amount :- " + txtAmount.Text + "Please check carefully . Press F2";
                    }
                    else if (Convert.ToInt32(txtAmount.Text) >= 5000 && Convert.ToInt32(txtAmount.Text) <= 9999)
                    {
                        lblamount.Text = "Amount :- " + txtAmount.Text + "Please check carefully . Press F3";
                    }
                    else if (Convert.ToInt32(txtAmount.Text) >= 10000 && Convert.ToInt32(txtAmount.Text) <= 49999)
                    {
                        lblamount.Text = "Amount :- " + txtAmount.Text + "Please check carefully . Press F4";
                    }
                    else if (Convert.ToInt32(txtAmount.Text) >= 50000 && Convert.ToInt32(txtAmount.Text) <= 99999)
                    {
                        lblamount.Text = "Amount :- " + txtAmount.Text + "Please check carefully . Press F5";
                    }
                    else if (Convert.ToInt32(txtAmount.Text) >= 100000)
                    {
                        lblamount.Text = "Amount :- " + txtAmount.Text + "Please check carefully . Press F6";
                    }
                    else
                    {
                        SavedData();
                        btnAcknowledge.Enabled = true;
                    }
                }
            }

            //-- For Focus --//
            if (lblamount.Text != "")
                txtAmount.Focus();
            else if (lbldoctype_err.Text != "")
                cboDoctype.Focus();
            else if (lbldocdetailerr.Text != "")
                txtdocDetail.Focus();
            else if (lblName.Text != "")
                txtname.Focus();
            else if (lblMobile.Text != "")
                txtmob.Focus();
            else if (lblAdd.Text != "")
                txtaddr.Focus();
            else if (lblDistrict.Text != "")
                cboDistrict.Focus();
            else if (lblTaluka.Text != "")
                txttal.Focus();
        }
        private void cboGotra_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboGotra.Text == "OTHER")
            {
                txtAddGotra.Visible = true;
                txtAddGotra.Enabled = true;
            }
            else
            {
                txtAddGotra.Visible = false;
            }
            userDengi.SetGotra(cboGotra.Text);
            userDengi.Show();
        }
        private void txtaddr_TextChanged(object sender, EventArgs e)
        {
            userDengi.SetAddress(txtaddr.Text);
            if (txtaddr.Text == "")
            {
                lblAdd.Text = "Please Enter Address";
            }
            else
            {
                lblAdd.Text = "";
            }
        }
        private void txtmob_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtPincode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void SavedData()
        {
            int AmountAboove = 0;
            int AmountBelow = 0;
            int Amount = 0;
            string lstEnteredName = null;
            string lstEnteredAmount = null;
            DialogResult result;
            dengiReceiptModel = new dengiReceiptModel();
            loginDAL login = new loginDAL();
            dengiReceiptModel.serailId = Convert.ToDouble(txtdengireceiptNo.Text);
            dengiReceiptModel.countryId = (int)cboCountry.SelectedValue;
            dengiReceiptModel.COUNTRY_NAME = cboCountry.Text;
            // dengiReceiptModel.counter = int.Parse(txtCounter.Text);
            dengiReceiptModel.contact = txtmob.Text;
            dengiReceiptModel.Name = txtname.Text;
            dengiReceiptModel.amount = decimal.Parse(txtAmount.Text);
            Amount = Convert.ToInt32(txtAmount.Text);
            dengiReceiptModel.chno = txtChqNo.Text;
            dengiReceiptModel.chqbankname = txtChqBankname.Text;
            dengiReceiptModel.PinCode = txtPincode.Text;
            dengiReceiptModel.userId = login.getUserId(txtUser.Text);
            dengiReceiptModel.PanNo = txtPANNo.Text;
            dengiReceiptModel.Taluka = txttal.Text;
            dengiReceiptModel.gotraId = (int)cboGotra.SelectedValue;
            if (cboGotra.Text == "OTHER")
            {
                dengiReceiptModel.gotra = txtAddGotra.Text;
            }
            else
            {
                dengiReceiptModel.gotra = cboGotra.Text.Trim();
            }

            dengiReceiptModel.stateId = (int)cboState.SelectedValue;
            dengiReceiptModel.STATE = cboState.Text;
            if (txtChqNo.Text != "")
                dengiReceiptModel.chqdate = DateTime.Parse(dtChqDt.Text);
            dengiReceiptModel.Address = txtaddr.Text;
            dengiReceiptModel.ddbankname = txtDDBankName.Text;
            dengiReceiptModel.ddbankname = txtDDBankName.Text;
            dengiReceiptModel.ddno = txtDDNo.Text;
            if (txtDDNo.Text != "")
            {
                dengiReceiptModel.dd_date = DateTime.Parse(dtDDdate.Text);
            }
            dengiReceiptModel.paymentTypeId = (int)cboPaymentType.SelectedValue;
            dengiReceiptModel.DengiId = Convert.ToInt32(cboDengiType.SelectedValue);
            dengiReceiptModel.DistId = (int)cboDistrict.SelectedValue;
            dengiReceiptModel.DISTRICT = cboDistrict.Text;
            dengiReceiptModel.Doc_type = cboDoctype.Text;
            dengiReceiptModel.Doc_Detail = txtdocDetail.Text;
            dengiReceiptModel.netbankname = txtNetBankName.Text;
            dengiReceiptModel.netbankrefnumber = txtNetRefNo.Text;
            dengiReceiptModel.Invoiceno = txtInvoice.Text;
            if (cboPaymentType.Text == "Swipe")
            {
                dengiReceiptModel.tidId = (int)cobTid.SelectedValue;
            }
            else
            {
                dengiReceiptModel.tidId = 0;
            }
            dt = dengiReceiptDAL.getLastEnteredRecord(UserInfo.ctrMachID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    lstEnteredName = row["Name"].ToString();
                    lstEnteredAmount = row["AMOUNT"].ToString();
                }
            }
            if (lstEnteredAmount == txtAmount.Text && lstEnteredName == txtname.Text.Trim())
            {
                result = MessageBox.Show("Do you want to save duplicate record?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    dengiReceiptModel.IsDuplicate = 159;
                }
                else
                {
                    btnNew.PerformClick();
                }
            }
            else
            {
                dengiReceiptModel.IsDuplicate = 0;
            }

            dengiReceiptDAL = new DengiReceiptDAL();

            DataTable dtAmlountltd = new DataTable();
            dtAmlountltd = dengiReceiptDAL.getAmountLimits(cboDengiType.Text.Trim());
            if (dtAmlountltd.Rows.Count > 0)
            {
                foreach (DataRow row in dtAmlountltd.Rows)
                {
                    AmountAboove = Convert.ToInt32(row["AMOUNT_ABOVE"]);
                    AmountBelow = Convert.ToInt32(row["AMOUNT_BELOW"]);
                }
            }
            if (Amount <= AmountBelow && Amount >= AmountAboove)
            {
                result = MessageBox.Show("Are you sure to save this record", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    int status = dengiReceiptDAL.InsertDengiReceipt(dengiReceiptModel);

                    if (status >= 0)
                    {
                        long chkMissingEntry;
                        chkMissingEntry = fcheckInsert();
                        if (chkMissingEntry < 0)
                        {
                            return;
                        }
                        txtdengireceiptNo.Text = status.ToString();

                        //Refrence_Amount.Text = txtAmount.Text;
                        //Refrence_Name.Text = txtname.Text;
                    }

                    if (status != 0)
                    {
                        resetAllFields();
                        getDengiNo();
                        unLockControls();
                        string receptID = status.ToString();
                        frmReportViewer report = new frmReportViewer("PRINT", receptID);
                        report.createReport("Dengi");
                        //  report.Show();
                    }
                    if (chkDeclaration.Checked == true)
                    {
                        string receptID = status.ToString();
                        frmReportViewer report = new frmReportViewer("DECLARATION", receptID);
                        report.createReport("Dengi");
                        // report.Show();
                    }
                }
                else
                {
                    resetAllFields();
                    getDengiNo();
                    unLockControls();
                }
            }
            else
            {
                MessageBox.Show("Amount is not acceptable, It should be greater than '" + AmountAboove + " and less than " + AmountBelow + "");
                txtAmount.Focus();
            }

        }
        private int fcheckInsert()
        {
            int fcheckInsert = 0;
            try
            {
                string strLastName = "";
                string strLastReceiptNo = "";
                string strLastAmount = "";
                int lngErrorNew;

                DataTable drachk;
                drachk = dengiReceiptDAL.GetLastEnterdNameAmountSerial();
                if (drachk.Rows.Count > 0)
                {
                    foreach (DataRow dr in drachk.Rows)
                    {
                        strLastAmount = dr["AMOUNT"].ToString();
                        strLastName = dr["LastEnteredName"].ToString();
                        strLastReceiptNo = dr["SERIAL_NO"].ToString();
                        break;
                    }
                }

                if (txtAmount.Text == strLastAmount & txtname.Text == strLastName)
                {
                    MessageBox.Show("Record saved successfully.");
                }
                else
                {
                    DengiErrorLog DengiError = new DengiErrorLog();
                    DengiError.Name = txtname.Text;
                    DengiError.Amount = Convert.ToDouble(txtAmount.Text);
                    DengiError.ReceiptNo = txtdengireceiptNo.Text;
                    DengiError.LastName = strLastName;
                    DengiError.LastAmount = Convert.ToDouble(strLastAmount);
                    DengiError.LastReceiptNo = strLastReceiptNo;
                    DengiError.Mach_Id = UserInfo.ctrMachID.ToString();
                    DengiError.Username = UserInfo.UserName;
                    DengiError.ErrorDate = DateTime.Now;

                    lngErrorNew = dengiReceiptDAL.InsertError(DengiError);
                    MessageBox.Show("Error Occured. Contact System Admin. Name:" + strLastName + " Amount:" + strLastAmount + " No:" + strLastReceiptNo);
                    fcheckInsert = -1;
                    //blnformChange = false;
                    btnNew_Click(null, null); 
                    btnSave.Enabled = true;
                    txtAmount.Focus();
                    //setCursor(this, true);
                    return fcheckInsert;
                }
                fcheckInsert = 1;
                return fcheckInsert;
            }
            catch (Exception ex)
            {
                MessageBox.Show("(Error Occured in Insert Validation - 2022");
                fcheckInsert = 1;
                return fcheckInsert;
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            frmSearch = new frmSearchDengi();
            frmSearch.Show();
        }

        public void getAllData(object data)
        {
            if (data is dengiReceiptModel obj)
            {
                fillPaymentMode();
                fillGotra();
                fillDengiType();
                fillCountry();
                fillStateByCountryId();
                fillDistrictbyStateId(Convert.ToInt32(cboState.SelectedValue));
                txtdengireceiptNo.Text = obj.serailId.ToString();
                txtdengireceiptNo.Tag = obj.Receipt_Id.ToString();
                txtAmount.Text = obj.amount.ToString();
                txtaddr.Text = obj.Address.ToString();
                txtChqBankname.Text = obj.chqbankname.ToString();
                txtChqNo.Text = obj.chno.ToString();
                txtDDBankName.Text = obj.ddbankname.ToString();
                txtDDNo.Text = obj.ddno.ToString();
                //txtdengireceiptNo.Text = obj.Receipt_Id.ToString();
                txtInvoice.Text = obj.Invoiceno.ToString();
                txtmob.Text = obj.contact.ToString();
                txtname.Text = obj.Name.ToString();
                txtNetBankName.Text = obj.netbankname.ToString();
                txtNetRefNo.Text = obj.netbankrefnumber.ToString();
                txtPANNo.Text = obj.PanNo.ToString();
                txtPincode.Text = obj.PinCode.ToString();
                txttal.Text = obj.Taluka.ToString();
                cboCountry.SelectedValue = obj.countryId;
                cboDengiType.SelectedValue = Convert.ToInt32(obj.DengiId);
                cboDistrict.SelectedValue = obj.DistId;
                cboState.SelectedValue = obj.stateId;
                cboPaymentType.SelectedValue = obj.paymentTypeId;
                cboGotra.SelectedValue = obj.gotraId;
                dtpPrnRcptDt.Text = obj.dr_Date.ToString();
                if (obj.Doc_type != null)
                    cboDoctype.Text = obj.Doc_type.ToString();
                if (obj.Doc_Detail != null)
                    txtdocDetail.Text = obj.Doc_Detail.ToString();
                if (Convert.ToInt32(obj.gotraId) == 9999 && obj.gotra != null)
                {
                    txtAddGotra.Visible = true;
                    txtAddGotra.Text = obj.gotra.ToString();
                }
                dataOnPaymentMode();
                btnSave.Enabled = false;
                lockControls();
                btnAcknowledge.Enabled = true;
                btnPrint.Enabled = true;
                // this.IsMdiContainer = true;
                // this.Show();
            }
        }
        public void ShowAsMdiChild()
        {
            // MDI mdi = new MDI();
            this.MdiParent = new MDI(); // Replace with the actual instance of your MDI parent form
            this.Show();
        }


        private void txtPincode_TextChanged(object sender, EventArgs e)
        {
            string strPincode = txtPincode.Text.Trim();
            string pattern = @"^\d{6}$";
            if (Regex.IsMatch(strPincode, pattern))
            {
                lblPincode.Text = "";
            }
            else
            {
                lblPincode.Text = "Please enter a valid 6-digit pincode.";
            }
            userDengi.SetPincode(txtPincode.Text);
            userDengi.Show();
        }

        private void txttal_TextChanged(object sender, EventArgs e)
        {
            if (txttal.Text != "")
            {
                lblTaluka.Text = "";
            }
            userDengi.SetTaluka(txttal.Text);
            userDengi.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to close the application?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                this.Close();
                userDengi.Close();
            }
        }
        private void frmDengiReceipt_KeyPress(object sender, KeyPressEventArgs e)
        {
            // btnSave.PerformClick();
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            string receptID = txtdengireceiptNo.Tag.ToString();
            // Code to save Duplicate Print

            int status = dengiReceiptDAL.DupPrintInsert(receptID);


            frmReportViewer report = new frmReportViewer("PRINT", receptID, "D");
            report.createReport("Dengi");
            // report.Show();
        }
        private void btnAcknowledge_Click(object sender, EventArgs e)
        {
            string receptID = txtdengireceiptNo.Tag.ToString();

            // Code to save Duplicate Print

            int status = dengiReceiptDAL.DupPrintDeclaration(receptID);

            frmReportViewer report = new frmReportViewer("DECLARATION", receptID, "D");
            report.createReport("Dengi");
            // report.Show();
        }
        private void txtdocDetail_TextChanged(object sender, EventArgs e)
        {
            if (cboDoctype.Text == "Adhar Card")
            {
                txtdocDetail.MaxLength = 12;
            }

            if (cboDoctype.Text != "")
            {
                if (txtdocDetail.Text != "")
                {
                    lbldocdetailerr.Text = "";
                    userDengi.SetDocDetail(txtdocDetail.Text);
                }
                else
                {
                    userDengi.SetDocDetail("");
                }
            }
        }
        public void CheckValidDocs()
        {
            commonFunctions = new CommonFunctions();
            if (cboDoctype.Text == "Pan Card")
            {
                if (!commonFunctions.IsValidPan(txtdocDetail.Text))
                {
                    lbldocdetailerr.Text = "Please enter valid pan number!";
                }
                else
                {
                    lbldocdetailerr.Text = "";
                }
            }
            if (cboDoctype.Text == "Adhar Card")
            {
                if (!commonFunctions.IsValidAadhar(txtdocDetail.Text))
                {
                    lbldocdetailerr.Text = "Please enter valid adhar number!";
                }
                else
                {
                    lbldocdetailerr.Text = "";
                }
            }
            if (cboDoctype.Text == "Passport")
            {
                if (!commonFunctions.IsValidPassport(txtdocDetail.Text))
                {
                    lbldocdetailerr.Text = "Please enter valid passport number!";
                }
                else
                {
                    lbldocdetailerr.Text = "";
                }
            }
            if (cboDoctype.Text == "Driving License")
            {
                if (!commonFunctions.IsValidLicenseNumber(txtdocDetail.Text))
                {
                    lbldocdetailerr.Text = "Please enter valid driving license!";
                }
                else
                {
                    lbldocdetailerr.Text = "";
                }
            }
            if (cboDoctype.Text == "Voter ID")
            {
                if (!commonFunctions.IsValidVoterId(txtdocDetail.Text))
                {
                    lbldocdetailerr.Text = "Please enter valid Voter ID!";
                }
                else
                {
                    lbldocdetailerr.Text = "";
                }
            }
        }
        private void cboDoctype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDoctype.SelectedIndex > 0)
            {
                lbldoctype_err.Text = "";
                txtdocDetail.Text = "";
                userDengi.SetDocType(cboDoctype.Text);
            }
            else
            {
                userDengi.SetDocType("");
            }
        }
        private void cboState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboState.SelectedIndex != -1 && cboState.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)cboState.SelectedItem;
                string selectedValue = selectedRow["StateId"].ToString();
                fillDistrictbyStateId(Convert.ToInt32(selectedValue));
            }
            if (cboState.SelectedIndex != -1)
            {
                userDengi.SetState(cboState.Text);
            }
            else
            {
                userDengi.SetState("");
            }
        }

        private void cboDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDistrict.SelectedIndex != -1)
            {
                userDengi.SetDistrict(cboDistrict.Text);

                lblDistrict.Text = "";
            }
            else
            {
                userDengi.SetDistrict("");
            }
        }

        private void cboDengiType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDengiType.SelectedIndex != -1)
            {
                userDengi.SetDengiType(cboDengiType.Text);
            }
            else
            {
                userDengi.SetDengiType("");
            }
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            if (txtAmount.Text != "")
            {
                if (Convert.ToInt32(txtAmount.Text) >= 500)
                {
                    lbldoctype_err.Text = "Please select Document type";
                }
                else
                {
                    lbldoctype_err.Text = "";
                }
            }
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            frmSearch = new frmSearchDengi();
            frmSearch.Show();
        }
        private void btnScan_Click(object sender, EventArgs e)
        {

        }
        public void Scan_Document()
        {
            string tempfile = "";
        }

        private void frmDengiReceipt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                dengiReceiptDAL = new DengiReceiptDAL();
                DataTable dt = new DataTable();
                if (e.Control && e.KeyCode == Keys.P)
                {
                    btnPrint.PerformClick();
                    e.Handled = true;
                }
                if (e.KeyCode == Keys.F10)
                {
                    dt = dengiReceiptDAL.getLastEnteredRecord(UserInfo.ctrMachID);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            txtname.Text = row["Name"].ToString();
                            if (row["Address"] != null)
                                txtaddr.Text = row["Address"].ToString();
                            if (row["DOC_TYPE"] != null)
                                cboDoctype.Text = row["DOC_TYPE"].ToString();
                            if (row["DOC_DETAIL"] != null)
                                txtdocDetail.Text = row["DOC_DETAIL"].ToString();
                            if (row["TALUKA"] != null)
                                txttal.Text = row["TALUKA"].ToString();
                            if (row["PINCODE"] != null)
                                txtPincode.Text = row["PINCODE"].ToString();
                            if (row["CONTACT"] != null)
                                txtmob.Text = row["CONTACT"].ToString();
                            if (row["COUNTRY_ID"] != null)
                                cboCountry.SelectedValue = Convert.ToInt32(row["COUNTRY_ID"]);
                            if (row["STATE_ID"] != null)
                                cboState.SelectedValue = Convert.ToInt32(row["STATE_ID"]);
                            if (row["DISTRICT_ID"] != null)
                                cboDistrict.SelectedValue = Convert.ToInt32(row["DISTRICT_ID"]);

                        }
                    }
                    txtAmount.Focus();
                }
                if (txtAmount.Text != "")
                {
                    if (e.KeyCode == Keys.F1 && Convert.ToInt32(txtAmount.Text) >= 500 && Convert.ToInt32(txtAmount.Text) <= 999)
                    {
                        lblamount.Text = "";
                        txtAmount.Focus();
                        SavedData();
                    }
                    if (e.KeyCode == Keys.F2 && Convert.ToInt32(txtAmount.Text) >= 1000 && Convert.ToInt32(txtAmount.Text) <= 4999)
                    {
                        lblamount.Text = "";
                        txtAmount.Focus();
                        SavedData();
                    }
                    if (e.KeyCode == Keys.F3 && Convert.ToInt32(txtAmount.Text) >= 5000 && Convert.ToInt32(txtAmount.Text) <= 9999)
                    {
                        lblamount.Text = "";
                        txtAmount.Focus();
                        SavedData();
                    }
                    if (e.KeyCode == Keys.F4 && Convert.ToInt32(txtAmount.Text) >= 10000 && Convert.ToInt32(txtAmount.Text) <= 49999)
                    {
                        lblamount.Text = "";
                        txtAmount.Focus();
                        SavedData();
                    }
                    if (e.KeyCode == Keys.F5 && Convert.ToInt32(txtAmount.Text) >= 50000 && Convert.ToInt32(txtAmount.Text) <= 99999)
                    {
                        lblamount.Text = "";
                        txtAmount.Focus();
                        SavedData();
                    }
                    if (e.KeyCode == Keys.F6 && Convert.ToInt32(txtAmount.Text) >= 100000)
                    {
                        lblamount.Text = "";
                        txtAmount.Focus();
                        SavedData();
                    }
                }
                //if (e.KeyCode == Keys.Enter)
                //{
                //    e.SuppressKeyPress = true;
                //    this.SelectNextControl(this.ActiveControl, true, true, true, true);
                //}

                if (e.KeyCode == Keys.End)
                {
                        btnSave.PerformClick();
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private void txtdocDetail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cboDoctype.Text == "Adhar Card")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtdengireceiptNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboDoctype_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            lbldoctype_err.Text = "";
        }
    }
}
