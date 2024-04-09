using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGMOSOL.SCREENS.BedSystem
{
    public partial class dgScanPrievew : Form
    {
        public string ImgPath;
        public dgScanPrievew()
        {
            InitializeComponent();
        }
        private void OK_Button_Click(System.Object sender, System.EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void dgScanPrievew_Load(System.Object sender, System.EventArgs e)
        {
            pbImgPrev.ImageLocation = ImgPath;
        }
    }
}
