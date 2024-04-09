using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Drawing.Printing;
using SGMOSOL.DAL;
using SGMOSOL.DataSet;
using Microsoft.Identity.Client;
using SGMOSOL.ADMIN;
using System.Diagnostics;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.Shared;
using Microsoft.VisualBasic;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using CrystalDecisions.CrystalReports.Engine;
using System.Resources;
using static SGMOSOL.ADMIN.CommonFunctions;
using Microsoft.Reporting.WebForms;

namespace SGMOSOL.SCREENS
{
    public partial class frmCrystalViewer : Form
    {
        private string mReportName;
        private string mSelectCriteria;
        private string mSubRptSelectCriteria;
        private eScreenID mReportID;
        private Database crDatabase;
        private Tables crTables;
        private Table crTable;
        private TableLogOnInfo crTableLogOnInfo;
        private ConnectionInfo crConnectionInfo;
        private System.Data.DataSet mDataSet;
        private Collection mCollParam;
        private System.Configuration.ConfigurationSettings sPath;
        private bool blnDirectToPrinter;
        private string mstrStockRptv;
        private ArrayList objCtl;
        private ArrayList buttonArray;
        private bool mBlnEdit = false;
        string sprintType = "";

        CommonFunctions cm = new CommonFunctions();

        public frmCrystalViewer(string ReportName, string SelectCriteria = "", System.Data.DataSet ObjDataSet = null, Hashtable objHash = null, Collection collParameter = null, eScreenID pReportId = 0, bool DirectToPrinter = false, bool DS_SCONNECT = false, bool DB_DS_CONNECT = false, string strStockRptv = "", string printType = "") : base()
        {
            mReportName = ReportName;
            mSelectCriteria = SelectCriteria;
            mDataSet = ObjDataSet;
            mCollParam = collParameter;
            mReportID = pReportId;
            blnDirectToPrinter = DirectToPrinter;
            mstrStockRptv = strStockRptv;
            sprintType = printType;
            //crReportDocument.Load(mReportName);

            InitializeComponent();
        }

        private void frmCrystalViewer_Load(System.Object sender, System.EventArgs e)
        {
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource = null;
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = mReportName;
            int i = 1;
            foreach (DataTable dt in mDataSet.Tables)
            {
                reportDataSource = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet" + i + "", dt);
                reportViewer1.LocalReport.DataSources.Add(reportDataSource);
                DataTable dt1 = (DataTable)reportDataSource.Value;
                addCustomField(dt1);
                reportViewer1.RefreshReport();
                i++;
            }
            if (blnDirectToPrinter)
            {
                printReport("");
            }
        }
        public void printReport(string docName)
        {
            string printerName = System.Configuration.ConfigurationManager.AppSettings["Printer_name"].ToString();

            byte[] renderedBytes = reportViewer1.LocalReport.Render("Image");
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(renderedBytes))
            {
                using (System.Drawing.Image image = System.Drawing.Image.FromStream(stream))
                {
                    try
                    {
                        PrintDocument printDoc = new PrintDocument();
                        printDoc.PrinterSettings.PrinterName = printerName;
                        printDoc.DocumentName = docName;
                        System.Drawing.Printing.PaperSize paperSize = new System.Drawing.Printing.PaperSize("Custom", (int)(4.84 * 100), (int)(5.70 * 100));
                        printDoc.DefaultPageSettings.PaperSize = paperSize;
                        printDoc.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
                        printDoc.PrintPage += (s, args) =>
                        {
                            args.Graphics.DrawImage(image, args.MarginBounds);
                        };
                        printDoc.Print();
                    }
                    catch (Exception ex)
                    {
                        cm.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
                    }
                }
            }

        }
        public void addCustomField(DataTable dt)
        {
            if (dt.Columns.Contains("AMOUNT"))
            {
                foreach (DataRow row in dt.Rows)
                {
                    double amount = Convert.ToDouble(row["AMOUNT"]);

                    if (dt.Columns.Contains("AMOUNT_IN_WORDS"))
                        row["AMOUNT_IN_WORDS"] = cm.words(Convert.ToInt32(amount));
                    if (dt.Columns.Contains("REPORT_TYPE"))
                    {
                        if (sprintType == "D")
                        {
                            row["REPORT_TYPE"] = "Duplicate";
                        }
                        else
                        {
                            row["REPORT_TYPE"] = "ND";
                        }
                    }
                }
            }
        }
        private void frmCrystalViewer_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
    }


}

