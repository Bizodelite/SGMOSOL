using SGMOSOL.ADMIN;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static SGMOSOL.ADMIN.CommonFunctions;

namespace SGMOSOL.SCREENS
{
    public partial class frmDengiBhetvastu : Form
    {
        CommonFunctions commonFunctions = new CommonFunctions();
        DataTable dt;
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
    }
}
