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
    public partial class frmUserDengi : Form
    {
        frmDengiReceipt dengiReceipt;
        public frmUserDengi()
        {
            InitializeComponent();
            Screen[] screens = Screen.AllScreens;
            if (screens.Length <= 1)
            {
                this.Location = Screen.AllScreens[0].WorkingArea.Location;
            }
            else
            {
                bool maximised = false;
                if (WindowState == FormWindowState.Maximized)
                {
                    WindowState = FormWindowState.Normal;
                    maximised = true;
                }

                if (maximised)
                {
                    WindowState = FormWindowState.Maximized;
                }
                this.Location = Screen.AllScreens[1].WorkingArea.Location;
            }
            // dengiReceipt = frmdengiReceipt;
        }


        public void SetText(string name)
        {
            lblValue.Text = name;
            lblName.Text = name;
        }
        public void Setmobile(string mobile)
        {
            lblValue.Text = mobile;
            lblMobile.Text = mobile;
        }
        public void SetGotra(string gotra)
        {
            if (gotra != "Select")
            {
                lblGotra.Text = gotra;
                lblValue.Text = gotra;
            }
            else
            {
                lblGotra.Text = "";
                lblValue.Text = "";
            }
        }
        public void SetAddress(string add)
        {
            lblValue.Text = add;
            lblAddress.Text = add;
        }
        public void SetAmount(string strAmount)
        {
            lblValue.Text = strAmount;
            lblAmount.Text = strAmount;
        }
        public void SetDocDetail(string strDocDetail)
        {
            lblValue.Text = strDocDetail;
            lblDocDetail.Text = strDocDetail;
        }

        public void SetDocType(string strDocType)
        {
            lblValue.Text = strDocType;
            lblDocType.Text = strDocType;
        }

        public void SetTaluka(string strTaluka)
        {
            lblValue.Text = strTaluka;
            lblTaluka.Text = strTaluka;
        }
        public void SetPincode(string strPincode)
        {
            lblValue.Text = strPincode;
            lblPincode.Text = strPincode;
        }
        public void SetState(string strState)
        {
            lblValue.Text = strState;
            lblState.Text = strState;
        }

        public void SetDistrict(string strDistrict)
        {
            lblValue.Text = strDistrict;
            lblDistrict.Text = strDistrict;
        }

        public void SetAmtInWord(string strAmtInWord)
        {
           // lblValue.Text = lblAmount.Text + " : " + strAmtInWord;
            lblAmtWords.Text = strAmtInWord;
        }

        public void SetDengiType(string strDengiType)
        {
            lblValue.Text = strDengiType;
            lblDengiHead.Text = strDengiType;
        }
        public void SetMode(string strMode)
        {
            lblValue.Text = strMode;
            lblMode.Text = strMode;
        }
        private void btnconvert_Click(object sender, EventArgs e)
        {
            if (btnconvert.Text.ToUpper() == "CONVERT TO ENGLISH")
            {
                btnconvert.Text = "मराठी रूपांतर";

            }
            else
            {
                btnconvert.Text = "Convert to English";
            }

        }

        private void frmUserDengi_Load(object sender, EventArgs e)
        {
            //08/03/24 
            lblValue.Text = "";
            lblValue.Font = new Font("Microsoft Sans Serif", 40);
            Font LabelFont = new Font("Microsoft Sans Serif", 20);
            //String strLabel = "";
            lblAmount.Font = LabelFont;
            lblAddress.Font = LabelFont;
            lblAmtWords.Font = LabelFont;
            lblDengiHead.Font = LabelFont;
            lblDistrict.Font = LabelFont;
            lblDocDetail.Font = LabelFont;
            lblGotra.Font = LabelFont;
            lblMobile.Font = LabelFont;
            lblName.Font = LabelFont;
            lblPincode.Font = LabelFont;
            lblState.Font = LabelFont;
            lblTaluka.Font = LabelFont;

            lblAddress1.Font = LabelFont;
            lblAmount1.Font = LabelFont;
            lblDistrict1.Font = LabelFont;
            lblDocType.Font = LabelFont;
            lblGotra1.Font = LabelFont;
            lblMobile1.Font = LabelFont;
            lblName1.Font = LabelFont;
            lblPincode1.Font = LabelFont;
            lblState1.Font = LabelFont;
            lblTaluka1.Font = LabelFont;

            lblAddress1.Text = lblAddress1.Text + ": ";
            lblAmount1.Text = lblAmount1.Text + ": ";
            lblDistrict1.Text = lblDistrict1.Text + ": ";
            lblDocType.Text = lblDocType.Text + ": ";
            lblGotra1.Text = lblGotra1.Text + ": ";
            lblMobile1.Text = lblMobile1.Text + ": ";
            lblName1.Text = lblName1.Text + ": ";
            lblPincode1.Text = lblPincode1.Text + ": ";
            lblState1.Text = lblState1.Text + ": ";
            lblTaluka1.Text = lblTaluka1.Text + ": ";


        }

        private void flowLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblValue_Click(object sender, EventArgs e)
        {

        }

        private void frmUserDengi_FormClosing(object sender, FormClosingEventArgs e)
        {
            dengiReceipt = Application.OpenForms.OfType<frmDengiReceipt>().FirstOrDefault();
            if (dengiReceipt != null)
            {
                dengiReceipt.Close();
            }
        }
    }
}
