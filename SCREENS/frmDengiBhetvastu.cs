using SGMOSOL.ADMIN;
using SGMOSOL.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using WIA;
using static SGMOSOL.ADMIN.CommonFunctions;

namespace SGMOSOL.SCREENS
{
    public partial class frmDengiBhetvastu : Form
    {
        CommonFunctions commonFunctions = new CommonFunctions();
        DengiReceiptDAL obj;
        DataTable dt;
        private const int WM_CAP = 0x400;
        private const int WM_CAP_DRIVER_CONNECT = WM_CAP + 10;
        private const int WM_CAP_DRIVER_DISCONNECT = WM_CAP + 11;
        private const int WM_CAP_EDIT_COPY = WM_CAP + 30;

        // Helper function to send messages
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        public frmDengiBhetvastu()
        {
            InitializeComponent();
        }

        private void frmDengiBhetvastu_Load(object sender, EventArgs e)
        {
            dt = new DataTable();
            int centerX = (ClientSize.Width - pnlMaster.Width) / 2;
            int centerY = (ClientSize.Height - pnlMaster.Height) / 2;
            pnlMaster.Location = new System.Drawing.Point(centerX, centerY);
            commonFunctions = new CommonFunctions();
            CenterToParent();
            FillCounter();
            FillUnit();
            fillCountry();
            fillStateByCountryId();
            fillDistrictbyStateId(Convert.ToInt32(cboState.SelectedValue));
            UserInfo.module = "Dengi-BhetVastu";
            txtUser.Text = UserInfo.UserName;
            getDengiBhetvastuNumber();
        }
        private void FillCounter()
        {
            System.Data.DataTable dr;
            dr = commonFunctions.GetDrCounterMachId(UserInfo.UserId, SystemHDDModelNo, SystemHDDSerialNo, SystemMacID, Convert.ToInt16(eModType.Dengi));
            if (dr.Rows.Count > 0)
            {
                txtCounter.Text = dr.Rows[0]["CounterMachineTitle"].ToString();
                txtCounter.Tag = dr.Rows[0]["CtrMachId"];
                UserInfo.ctrMachID = Convert.ToInt32(txtCounter.Tag);
                UserInfo.Dept_id = Convert.ToInt32(dr.Rows[0]["DeptId"]);
                //  PrintReceiptDeptName = dr.Rows[0]["DepartmentName"].ToString();
                //PrintReceiptLocName = dr.Rows[0]["LocName"].ToString();
                UserInfo.Loc_id = Convert.ToInt32(dr.Rows[0]["LocId"]);
                // mStrCounterMachineShortName = dr.Rows[0]["CounterMachineShortName"].ToString();
            }
        }
        private void FillUnit()
        {
            try
            {
                dt = commonFunctions.getUnit();
                cboUnitType.DataSource = dt;
                cboUnitType.DisplayMember = "UnitName";
                cboUnitType.ValueMember = "UnitID";
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

        private void getDengiBhetvastuNumber()
        {
            try
            {
                obj = new DengiReceiptDAL();
                int dengiID = obj.getDengiBhetvastuNumber();
                txtdengireceiptNo.Text = dengiID.ToString();
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

        private void cboState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboState.SelectedIndex != -1 && cboState.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)cboState.SelectedItem;
                string selectedValue = selectedRow["StateId"].ToString();
                fillDistrictbyStateId(Convert.ToInt32(selectedValue));
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            getDengiBhetvastuNumber();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {

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
            }
            catch (Exception ex)
            {
                commonFunctions.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }
        }

        private void BhaktImgCap_Click(object sender, EventArgs e)
        {
            IntPtr hHwnd = this.Handle;
            int iDevice = 0;
            string strFileName = null;
            Helper obj = new Helper();

            // Connect to the capture device
            if (SendMessage(hHwnd, WM_CAP_DRIVER_CONNECT, new IntPtr(iDevice), IntPtr.Zero) != 0)
            {
                // Capture the image
                SendMessage(hHwnd, WM_CAP_EDIT_COPY, IntPtr.Zero, IntPtr.Zero);

                // Get image from clipboard
                IDataObject data = Clipboard.GetDataObject();
                if (data.GetDataPresent(typeof(Bitmap)))
                {
                    // Display captured image in PictureBox
                    System.Drawing.Image bmap = (System.Drawing.Image)data.GetData(typeof(Bitmap));
                    strFileName = obj.Final_SaveImageCapture(bmap, txtname.Text);

                    PictureBox_Bhakt.Image = System.Drawing.Image.FromFile(strFileName);
                   
                    // Disconnect from the capture device
                    SendMessage(hHwnd, WM_CAP_DRIVER_DISCONNECT, new IntPtr(iDevice), IntPtr.Zero);
                }
                else
                {
                    MessageBox.Show("Failed to capture image.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void ClosePreviewWindow()
        {
            IntPtr hHwnd = this.Handle;
            int iDevice = 0;
            SendMessage(hHwnd, WM_CAP_DRIVER_DISCONNECT, new IntPtr(iDevice), IntPtr.Zero);

            // 
            // close window
            // 

            //DestroyWindow(hHwnd);
        }

        private void PrasadImgCap_Click(object sender, EventArgs e)
        {
            IntPtr hHwnd = this.Handle;
            int iDevice = 0;
            string strFileName = null;
            Helper obj = new Helper();

            // Connect to the capture device
            if (SendMessage(hHwnd, WM_CAP_DRIVER_CONNECT, new IntPtr(iDevice), IntPtr.Zero) != 0)
            {
                // Capture the image
                SendMessage(hHwnd, WM_CAP_EDIT_COPY, IntPtr.Zero, IntPtr.Zero);

                // Get image from clipboard
                IDataObject data = Clipboard.GetDataObject();
                if (data.GetDataPresent(typeof(Bitmap)))
                {
                    // Display captured image in PictureBox
                    System.Drawing.Image bmap = (System.Drawing.Image)data.GetData(typeof(Bitmap));
                    strFileName = obj.Final_SaveImageCapture(bmap, txtname.Text);

                    PictureBoxPrasad.Image = System.Drawing.Image.FromFile(strFileName);

                    // Disconnect from the capture device
                    SendMessage(hHwnd, WM_CAP_DRIVER_DISCONNECT, new IntPtr(iDevice), IntPtr.Zero);
                }
                else
                {
                    MessageBox.Show("Failed to capture image.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
