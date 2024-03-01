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
            lblName.Text = name;
        }
        public void Setmobile(string mobile)
        {
            lblMobile.Text = mobile;
        }
        public void SetGotra(string gotra)
        {
            if (gotra != "Select")
                lblGotra.Text = gotra;
            else lblGotra.Text = "";
        }
        public void SetAddress(string add)
        {
            lblAddress.Text = add;
        }
        public void SetAmount(string strAmount)
        {
            lblAmount.Text = strAmount;
        }
        public void SetDocDetail(string strDocDetail)
        {
            lblDocDetail.Text = strDocDetail;
        }

        public void SetDocType(string strDocType)
        {
            lblDocType.Text = strDocType;
        }

        public void SetTaluka(string strTaluka)
        {
            lblTaluka.Text = strTaluka;
        }
        public void SetPincode(string strPincode)
        {
            lblPincode.Text = strPincode;
        }
        public void SetState(string strState)
        {
            lblState.Text = strState;
        }

        public void SetDistrict(string strDistrict)
        {
            lblDistrict.Text = strDistrict;
        }

        public void SetAmtInWord(string strAmtInWord)
        {
            lblAmtWords.Text = strAmtInWord;
        }

        public void SetDengiType(string strDengiType)
        {
            lblDengiHead.Text = strDengiType;
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
            Font LabelFont = new Font("Microsoft Sans Serif", 16);
            //String strLabel = "";
            lblAmount.Font = LabelFont;
            lblAddress.Font= LabelFont;
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

            lblAddress1.Text  = lblAddress1.Text + ": ";
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
    }
}
