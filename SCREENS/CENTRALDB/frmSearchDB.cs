using iTextSharp.text.xml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGMOSOL.SCREENS.CENTRALDB
{
    public partial class frmSearchDB : Form
    {
        public frmSearchDB()
        {
            InitializeComponent();
        }

        private void frmSearchDB_Load(object sender, EventArgs e)
        {
            cboSearch.Text = "BARCODE";
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {
                if (cboSearch.Text == "BARCODE")
                { 
                
                }
            }
        }
    }
}
