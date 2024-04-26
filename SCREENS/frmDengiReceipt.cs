
using SGMOSOL.ADMIN;
using SGMOSOL.Custom_User_Contols;
using SGMOSOL.DAL;
using SGMOSOL.DataModel;
using System;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WIA;
using System.IO;
using CommonDialog = WIA.CommonDialog;
using Microsoft.VisualBasic;
using System.Xml.Linq;
using System.Linq;
using static SGMOSOL.ADMIN.CommonFunctions;
using System.Collections;
using System.Drawing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

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
        private string mStrCounterMachineShortName;
        private int PrintReceiptDeptID;
        private string PrintReceiptDeptName;
        private string PrintReceiptLocName;
        private int PrintReceiptLocId;
        private ArrayList CtrlArr = new ArrayList();
        private ArrayList btnArr = new ArrayList();
        private bool mBlnEdit = false;
        private eAction mAction;
        private SessionManager sessionManager;
        public frmDengiReceipt()
        {
            InitializeComponent();
            CenterToParent();
            this.KeyDown -= frmDengiReceipt_KeyDown;
            this.KeyDown += new KeyEventHandler(frmDengiReceipt_KeyDown);
            this.KeyPreview = true;
            userDengi = Application.OpenForms.OfType<frmUserDengi>().FirstOrDefault();
            if (userDengi == null)
            {
                userDengi = new frmUserDengi();
            }
            else
            {

            }
            this.commonFunctions = new CommonFunctions();
            dt = new DataTable();
            txtname.TextChanged += txtname_TextChanged;
            txtmob.TextChanged += txtmob_TextChanged;
            txtAmount.TextChanged += txtAmount_TextChanged;
            txtPANNo.TextChanged += txtPANNo_TextChanged;
            txtaddr.TextChanged += txtaddr_TextChanged;
            // btnSave.Click += btnSave_Click;
            cboState.SelectedIndexChanged += cboState_SelectedIndexChanged;
            UserInfo.module = "Dengi";
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
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                return false;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void frmDengiReceipt_Load(object sender, EventArgs e)
        {
            try
            {
                commonFunctions.setControlsonForm(this, CtrlArr, btnArr);
                commonFunctions.SetUserScreenActions(this, UserInfo.UserId, (int)eScreenID.DengiReceipt, btnArr, null, mBlnEdit);
                int centerX = (ClientSize.Width - flowLayoutPanel1.Width) / 2;
                int centerY = (ClientSize.Height - flowLayoutPanel1.Height) / 2;
                flowLayoutPanel1.Location = new System.Drawing.Point(centerX, centerY);
                userDengi.Show();
                txtAmount.Focus();
                txtUser.Text = UserInfo.UserName;
                if (isPrint == false)
                {
                    FillCounter();
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
               // sessionManager = new SessionManager(this);
               // sessionManager.StartTimer();
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }

        }
        private void FillCounter()
        {
            System.Data.DataTable dr;
            dr = commonFunctions.GetDrCounterMachId(UserInfo.UserId, SystemHDDModelNo, SystemHDDSerialNo, SystemMacID, Convert.ToInt16(eModType.Dengi));
            if (dr.Rows.Count > 0)
            {
                txtCounter.Text = dr.Rows[0]["CounterMachineShortName"].ToString();
                txtCounter.Tag = dr.Rows[0]["CtrMachId"];
                UserInfo.Counter_Name = txtCounter.Text;
                UserInfo.ctrMachID = Convert.ToInt32(txtCounter.Tag);
                UserInfo.Dept_id = Convert.ToInt32(dr.Rows[0]["DeptId"]);
                PrintReceiptDeptName = dr.Rows[0]["DepartmentName"].ToString();
                PrintReceiptLocName = dr.Rows[0]["LocName"].ToString();
                UserInfo.Loc_id = Convert.ToInt32(dr.Rows[0]["LocId"]);
                mStrCounterMachineShortName = dr.Rows[0]["CounterMachineShortName"].ToString();
                this.Text = PrintReceiptLocName + " /" + PrintReceiptDeptName + " /" + mStrCounterMachineShortName + " /" + UserInfo.version; ;
            }
            //dr.Close();
        }
        private void getDengiNo()
        {
            try
            {
                dengiReceiptDAL = new DengiReceiptDAL();
                int dengiID = dengiReceiptDAL.getDenigNumber();
                txtdengireceiptNo.Text = dengiID.ToString();
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
        }
        private void FillDocumentType()
        {
            try
            {
                dt = commonFunctions.GetDocumentType();
                cboDoctype.DataSource = dt;
                cboDoctype.DisplayMember = "DocumentName";
                cboDoctype.ValueMember = "DocumentID";
                txtdocDetail.Enabled = false;
            }

            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
        }
        private void fillCountry()
        {
            try
            {
                dt = commonFunctions.getCountry();
                cboCountry.DataSource = dt;
                cboCountry.DisplayMember = "CountryName";
                cboCountry.ValueMember = "CountryID";
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
        }
        private void fillTId()
        {
            try
            {
                dt = commonFunctions.fillTID();
                cobTid.DataSource = dt;
                cobTid.DisplayMember = "tidNo";
                cobTid.ValueMember = "Tid";
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
        }
        private void fillStateByCountryId()
        {
            try
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
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
        }
        private void fillDistrictbyStateId(int stateId)
        {
            try
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
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
        }
        private void fillPaymentMode()
        {
            try
            {
                dt = commonFunctions.getPaymentMode();
                cboPaymentType.DataSource = dt;
                cboPaymentType.DisplayMember = "Token_Detail_Name";
                cboPaymentType.ValueMember = "Token_Detail_Id";
                cboPaymentType.SelectedValue = 8;
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
        }
        private void fillDengiType()
        {
            try
            {
                dt = commonFunctions.getDengiType();
                cboDengiType.DataSource = dt;
                cboDengiType.DisplayMember = "TYPE";
                cboDengiType.ValueMember = "DENGI_MST_ID";
                cboDengiType.SelectedValue = 4;
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
        }
        private void fillGotra()
        {
            try
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
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
        }
        private void cboPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataOnPaymentMode();
            lblPaymentMode.Text = "";
            userDengi.SetMode(cboPaymentType.Text);
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
            cobTid.Enabled = false;

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
            //  txtdocDetail.Enabled = true;
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
            isPrint = false;
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
            cboPaymentType.SelectedValue = 8;
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
            cboDoctype.SelectedValue = 0;
            txtdocDetail.Enabled = false;
            txtdocDetail.Text = "";
            lblDistrict.Text = "";
            imgVideo.Image = null;
        }
        private void txtname_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtname.Text != "")
                {
                    lblName.Text = "";
                }
                userDengi.SetText(txtname.Text);
                userDengi.Show();
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
        }

        private void txtmob_TextChanged(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            int value;
            try
            {
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
                if (int.TryParse(txtAmount.Text, out value) && value >= 500 && cboDoctype.Text == "Select")
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
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
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
            if (userDengi.Visible)
            {
                userDengi.Show();
            }
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            resetAllFields();
            getDengiNo();
            unLockControls();
        }
        public void validationCheck()
        {
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
                else
                {
                    if (cboDoctype.Text == "Select" && txtdocDetail.Text != "")
                    {
                        lbldoctype_err.Text = "";
                    }
                    else
                    {
                        lbldoctype_err.Text = "";
                        lbldocdetailerr.Text = "";
                    }
                }
            }
            if (cboPaymentType.Text == "Swipe" && cobTid.Text == "")
            {
                lblPaymentMode.Text = "Tid not found, Please change paymenttype.";
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            validationCheck();
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
                txtAddGotra.Text = "";
            }
            userDengi.SetGotra(cboGotra.Text);
            if (userDengi.Visible)
            {
                userDengi.Show();
            }
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
            try
            {
                int AmountAboove = 0;
                int AmountBelow = 0;
                int Amount = 0;
                string lstEnteredName = null;
                int lstEnteredAmount = 0;
                DialogResult result;
                dengiReceiptModel = new dengiReceiptModel();
                loginDAL login = new loginDAL();
                // dengiReceiptModel.dr_Date = dtpPrnRcptDt.Value;
                dengiReceiptModel.dr_Date = commonFunctions.ParseDateTimeInAnyFormat(dtpPrnRcptDt.Text);
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
                dengiReceiptModel.userId = UserInfo.UserId;
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
                // dengiReceiptModel.Doc_type = cboDoctype.Text;
                dengiReceiptModel.Doc_type = cboDoctype.SelectedValue.ToString();
                dengiReceiptModel.Doc_Detail = txtdocDetail.Text;
                dengiReceiptModel.netbankname = txtNetBankName.Text;
                dengiReceiptModel.netbankrefnumber = txtNetRefNo.Text;
                dengiReceiptModel.Invoiceno = txtInvoice.Text;
                if (txtScan.Text != "")
                {
                    dengiReceiptModel.ScanImage = txtScan.Text;
                }
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
                        lstEnteredAmount = Convert.ToInt32(row["AMOUNT"]);
                    }
                }
                if (Convert.ToString(lstEnteredAmount) == txtAmount.Text && lstEnteredName == txtname.Text.Trim())
                {
                    InputBox inputbox = new InputBox();
                    inputbox.MessageText = "Do you want to save Duplicate Record? Please insert Key";
                    DialogResult result1 = inputbox.ShowDialog();
                    if (result1 == DialogResult.OK)
                    {
                        dengiReceiptModel.IsDuplicate = Convert.ToInt32(inputbox.keyValue);
                    }
                    if (result1 == DialogResult.Cancel)
                    {
                        // inputbox.Close();
                        return;
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
                        }

                        if (status != 0 && status != -1)
                        {
                            resetAllFields();
                            getDengiNo();
                            unLockControls();
                            string receptID = status.ToString();
                            frmReportViewer report = new frmReportViewer("PRINT", receptID);
                            commonFunctions.AppendToFile("");
                            commonFunctions.AppendToFile("Creating Report:-" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            report.createReport("Dengi");
                            commonFunctions.AppendToFile("Done Report:-" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            report.Show();
                        }
                        if (chkDeclaration.Checked == true)
                        {
                            string receptID = status.ToString();
                            frmReportViewer report = new frmReportViewer("DECLARATION", receptID);
                            report.createReport("Dengi");
                            report.Show(); 
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
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
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
            try
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
                    if (obj.Doc_type != null && obj.Doc_type != "")
                        cboDoctype.SelectedValue = obj.Doc_type.ToString();
                    if (obj.Doc_Detail != null && obj.Doc_Detail != "")
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
                }
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
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
            if (userDengi.Visible)
            {
                userDengi.Show();
            }
        }

        private void txttal_TextChanged(object sender, EventArgs e)
        {
            if (txttal.Text != "")
            {
                lblTaluka.Text = "";
            }
            userDengi.SetTaluka(txttal.Text);
            if (userDengi.Visible)
            {
                userDengi.Show();
            }
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
            // sessionManager.ResetSession();
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            string receptID = txtdengireceiptNo.Tag.ToString();
            // Code to save Duplicate Print

            int status = dengiReceiptDAL.DupPrintInsert(receptID);


            frmReportViewer report = new frmReportViewer("PRINT", receptID, "D");
            commonFunctions.AppendToFile(" ");
            commonFunctions.AppendToFile("Creating Report:-" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            report.createReport("Dengi");
            commonFunctions.AppendToFile("Done Report:-" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            report.Show();
        }
        private void btnAcknowledge_Click(object sender, EventArgs e)
        {
            validationCheck();
            if (lblAdd.Text == "" && lblMobile.Text == "" && lbldoctype_err.Text == "" && lbldocdetailerr.Text == "" && lblName.Text == "" && lblTaluka.Text == "" && lblDistrict.Text == "")
            {
                if (isPrint)
                {
                    string receptID = txtdengireceiptNo.Tag.ToString();

                    // Code to save Duplicate Print

                    int status = dengiReceiptDAL.DupPrintDeclaration(receptID);

                    frmReportViewer report = new frmReportViewer("DECLARATION", receptID, "D");
                    report.createReport("Dengi");
                    //  report.Show();
                }
                else
                {
                    //createTempTableforDeclaration();
                    frmReportViewer report = new frmReportViewer("DECLARATION");
                    report.printDeclarationwithoutSave(createTempTableforDeclaration());
                    // report.Show();
                }
            }
            // report.Show();
        }
        private void txtdocDetail_TextChanged(object sender, EventArgs e)
        {
            if (cboDoctype.Text == "Adhar Card")
            {
                txtdocDetail.MaxLength = 12;
            }

            if (cboDoctype.Text != "Select")
            {
                if (txtdocDetail.Text != "")
                {
                    lbldocdetailerr.Text = "";
                    userDengi.SetDocDetail(txtdocDetail.Text);
                }
                else
                {
                    lbldocdetailerr.Text = "Please enter Document Details";
                    userDengi.SetDocDetail("");
                }
            }
        }
        public DataTable createTempTableforDeclaration()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DR_DATE", typeof(string));
            dt.Columns.Add("NAME", typeof(string));
            dt.Columns.Add("ADDRESS", typeof(string));
            dt.Columns.Add("CONTACT", typeof(string));
            dt.Columns.Add("DOC_TYPE", typeof(string));
            dt.Columns.Add("DOC_DETAIL", typeof(string));
            dt.Columns.Add("AMOUNT", typeof(string));
            dt.Columns.Add("AMOUNT_IN_WORDS", typeof(string));
            dt.Columns.Add("PINCODE", typeof(string));
            dt.Columns.Add("TYPE", typeof(string));
            dt.Rows.Add(dtpPrnRcptDt.Text, txtname.Text, txtaddr.Text, txtmob.Text, cboDoctype.Text != "Select" ? cboDoctype.Text : "", txtdocDetail.Text != "" ? txtdocDetail.Text : "", txtAmount.Text, commonFunctions.words(Convert.ToDouble(txtAmount.Text)), txtPincode.Text, cboDengiType.Text);
            return dt;
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
            if (cboDoctype.Text == "Select")
            {
                txtdocDetail.Enabled = false;
                txtdocDetail.Text = "";
                lbldocdetailerr.Text = "";
            }
            else
            {
                txtdocDetail.Enabled = true;
            }
            if (cboDoctype.SelectedIndex > 0)
            {
                lbldoctype_err.Text = "";
                if (txtdocDetail.Text == "")
                {
                    lbldocdetailerr.Text = "Please enter Document number";
                }
                else
                {
                    txtdocDetail.Text = "";
                }
                userDengi.SetDocType(cboDoctype.Text);
            }
            else
            {
                userDengi.SetDocType("Document");
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
            if (cboDistrict.SelectedIndex != 0)
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
            frmSearch = Application.OpenForms.OfType<frmSearchDengi>().FirstOrDefault();
            if (frmSearch == null)
            {
                frmSearch = new frmSearchDengi();
                frmSearch.Show();
            }
            else
            {
                frmSearch.BringToFront();
                if (frmSearch.WindowState == FormWindowState.Minimized)
                {
                    frmSearch.WindowState = FormWindowState.Normal;
                }
            }
        }
        private void btnScan_Click(object sender, EventArgs e)
        {
            //ScanDocument();
            Scan_Document();
        }
        public static string val1 = "";
        public void Scan_Document()
        {
            string tempfile;
            WIA.Device mydevice;
            WIA.Item item;
            WIA.ImageFile F;
            System.Windows.Forms.Label Err_btnTakePicture_click;
            WIA.CommonDialog CommonDialogBox = new WIA.CommonDialog();
            WIA.CommonDialog Commondialog1 = new WIA.CommonDialog();
            string F1 = System.Configuration.ConfigurationManager.AppSettings.Get("ScannerPath");
            string s;
            DialogResult answer;


            try
            {
                Cursor.Current = Cursors.WaitCursor;
                lblScanner.Text = "Scanning !!! Please Wait...";
                WIA.DeviceManager DeviceManager1 = new WIA.DeviceManager();//= Interaction.CreateObject("WIA.DeviceManager");
                int i = 0;
                mydevice = CommonDialogBox.ShowSelectDevice(WIA.WiaDeviceType.ScannerDeviceType, true, false);

                if (DeviceManager1.DeviceInfos[1].Type == WIA.WiaDeviceType.ScannerDeviceType)
                {
                    WIA.Device Scanner = DeviceManager1.DeviceInfos[1].Connect();
                    if (Information.IsNothing(Scanner))
                    {
                        Interaction.MsgBox("Could not connect to scanner please check attached Properly.");
                        Cursor.Current = Cursors.Default;
                        lblScanner.Text = "";
                        return;
                    }
                    else
                        try
                        {
                            s = txtname.Text + txtdengireceiptNo.Text;
                            {
                                var withBlock = Scanner.Items[1];
                                withBlock.Properties["6146"].set_Value(WIA.WiaImageIntent.ColorIntent);  // 4 is Black-white,gray is 2, color 1 (Color Intent)
                            }
                            F = (WIA.ImageFile)Scanner.Items[1].Transfer("{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}");
                            //F = (WIA.ImageFile)Scanner.Items[1].Transfer(WIA.FormatID.wiaFormatJPEG);

                            if (Information.IsNothing(F))
                            {
                                answer = MessageBox.Show("There is no file in scanner do you want to scan blank image", "Yes/no sample", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (answer == DialogResult.Yes)
                                    return;
                            }
                            val1 = s + "." + F.FileExtension;
                            F1 = F1 + val1;
                            txtScan.Text = s;
                            if (File.Exists(F1))
                                FileSystem.Kill(F1);
                            F.SaveFile(F1);
                            lblScanner.Text = "Successfully scanned" + F1;
                            txtScan.Text = s;
                        }
                        catch (Exception ex)
                        {
                            Interaction.MsgBox(ex.Message);
                        }
                        finally
                        {
                            Scanner = null;
                        }
                }
                else
                {
                    Interaction.MsgBox("Scanner is not attached checked it");
                    Cursor.Current = Cursors.Default;
                    lblScanner.Text = ""; ;
                }

                imgVideo.ImageLocation = F1;
                DeviceManager1 = null;
            }

            catch (Exception ex)
            {
                answer = MessageBox.Show("There is no file in scanner or scanner not attached do you want to scan blank image", "Yes/no sample", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer == DialogResult.Yes)
                    return;
            }
        }
        //public void ScanDocument()
        //{
        //    try
        //    {
        //        string scannerPath = System.Configuration.ConfigurationManager.AppSettings.Get("ScannerPath");
        //        string tempFile;
        //        string s = txtname.Text + txtCounter.Text; // Assuming txtname and txtCounter are TextBox controls in your form

        //        CommonDialog commonDialog = new CommonDialog();
        //        Device myDevice = commonDialog.ShowSelectDevice(WiaDeviceType.ScannerDeviceType, true, false);

        //        if (myDevice == null)
        //        {
        //            MessageBox.Show("Scanner failed to transfer an Image");
        //            return;
        //        }

        //        Item scannerItem = myDevice.Items[1];
        //        scannerItem.Properties["6146"].set_Value(WiaImageIntent.ColorIntent); // Color Intent
        //        WIA.ImageFile imageFile = null;
        //        imageFile = (WIA.ImageFile)scannerItem.Transfer(scannerItem.Properties["6147"].get_Value().ToString());
        //        if (imageFile == null)
        //        {
        //            DialogResult answer = MessageBox.Show("There is no file in the scanner. Do you want to scan a blank image?", "Yes/No", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //            if (answer == DialogResult.Yes)
        //                return;
        //        }

        //        string fileName = $"{s}.{imageFile.FileExtension}";
        //        string filePath = Path.Combine(scannerPath, fileName);

        //        if (File.Exists(filePath))
        //            File.Delete(filePath);

        //        imageFile.SaveFile(filePath);
        //        txtScan.Text = s;

        //        imgVideo.ImageLocation = filePath;
        //    }
        //    catch (Exception ex)
        //    {
        //        DialogResult answer = MessageBox.Show("There is no file in the scanner or the scanner is not attached. Do you want to scan a blank image?", "Yes/No", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //        if (answer == DialogResult.Yes)
        //            return;
        //        MessageBox.Show(ex.Message);
        //    }
        //}


        private void frmDengiReceipt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
              //  sessionManager.ResetSession();
                dengiReceiptDAL = new DengiReceiptDAL();
                DataTable dt = new DataTable();
                if (e.Control && e.KeyCode == Keys.P)
                {
                    btnPrint.PerformClick();
                    e.Handled = true;
                }
                if (e.Control && e.KeyCode == Keys.D)
                {
                    btnAcknowledge.PerformClick();
                    e.Handled = true;
                }
                if (e.KeyCode == Keys.F10)
                {
                    string lastName = "";
                    string lastAmount = "";
                    dt = dengiReceiptDAL.getLastEnteredRecord(UserInfo.ctrMachID);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            txtname.Text = row["Name"].ToString();
                            lastName = row["Name"].ToString();
                            txtAmount.Text = row["AMOUNT"].ToString();
                            lastAmount = row["AMOUNT"].ToString();
                            if (row["Address"] != null)
                                txtaddr.Text = row["Address"].ToString();
                            if (row["DOC_TYPE"] != null)
                                cboDoctype.SelectedValue = row["DOC_TYPE"].ToString();
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
                            if (row["GOTRA_NAME"] != null)
                                cboGotra.Text = row["GOTRA_NAME"].ToString();

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
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you wish to clear Scan document?", "Acknowledgement Form", MessageBoxButtons.YesNo);
            if ((result == DialogResult.Yes))
            {
                txtScan.Text = "";
                imgVideo.Image = null;
                imgVideo.BackgroundImage = null;
                lblScanner.Text = "";
            }
        }

        private void frmDengiReceipt_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.OfType<frmUserDengi>().Any())
            {
                Application.OpenForms.OfType<frmUserDengi>().First().Close();
            }
        }

        private void frmDengiReceipt_MouseClick(object sender, MouseEventArgs e)
        {
           // sessionManager.ResetSession();
        }

        private void chkScanDoc_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cboState_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                e.Handled = true;
            }
        }

        private void cboGotra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                e.Handled = true;
            }
        }

        private void cboDistrict_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                e.Handled = true;
            }
        }

        private void cboCountry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                e.Handled = true;
            }
        }

        private void cboDoctype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                e.Handled = true;
            }
        }

        private void cboDengiType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                e.Handled = true;
            }
        }

        private void cboPaymentType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                e.Handled = true;
            }
        }

        private void txtAddGotra_TextChanged(object sender, EventArgs e)
        {
            if (txtAddGotra.Text!="")
            {
                userDengi.SetGotra(txtAddGotra.Text); 
            }
            else
            {
                userDengi.SetGotra("");
            }
        }
    }
}
