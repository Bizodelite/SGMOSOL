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




namespace SGMOSOL.SCREENS
{
    public partial class frmReportViewer : Form
    {
        private string receiptID = null;
        private string printType = null;
        public string flag { get; set; }
        DengiReceiptDAL da;
        BhojnalayPrintReceiptDAL bhojnalayDAL;
        CommonFunctions cm = new CommonFunctions();

        public enum PrinterNames
        {
            DengiDeclaration,
            DengiPrint,
            BHDeclaration,
            BHPrint
        }

        public frmReportViewer(string flag, string value = null, string PrintType = null)
        {
            InitializeComponent();
            this.receiptID = value;
            this.flag = flag;
            this.printType = PrintType;
            // reportViewer2.Size = new System.Drawing.Size(Size.Width, Size.Height);
            // reportViewer2.Size = new System.Drawing.Size((int)(5 * 100), (int)(6 * 100));
        }

        private void frmReportViewer_Load(object sender, EventArgs e)
        {
            //frmReportViewer frmReport = new frmReportViewer();
            // this.reportViewer3.RefreshReport();
        }

        public void printReport(string docName, PrinterNames PrinterType)
        {
            string printerName = null;
            if (PrinterType == PrinterNames.DengiDeclaration)
            {
                printerName = System.Configuration.ConfigurationManager.AppSettings["DengiDec_Printer_name"].ToString();
            }
            if (PrinterType == PrinterNames.DengiPrint)
            {
                printerName = System.Configuration.ConfigurationManager.AppSettings["DengiPrint_Printer_name"].ToString();
            }
            //else
            //{
            //    printerName = System.Configuration.ConfigurationManager.AppSettings["Printer_name"].ToString();
            //}

            byte[] renderedBytes = reportViewer2.LocalReport.Render("Image");
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(renderedBytes))
            {
                using (System.Drawing.Image image = System.Drawing.Image.FromStream(stream))
                {
                    try
                    {
                        PrintDocument printDoc = new PrintDocument();
                        printDoc.PrinterSettings.PrinterName = printerName;
                        printDoc.DocumentName = docName;
                        PaperSize paperSize = new PaperSize("Custom", (int)(4.84 * 100), (int)(5.70 * 100));
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
        public void createReport(string form)
        {
            if (form == "Dengi")
            {
                da = new DengiReceiptDAL();
                DataTable dt = new DataTable();
                string reportPath = null;
                string reportFileName = null;
                string reportsFolder = "Reports";
                string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                ReportDataSource reportDataSource = null;
                string DocumentName = null;
                reportViewer2.LocalReport.DataSources.Clear();
                if (flag == "PRINT")
                {
                    dt = da.getDengiReceiptDataForReport(receiptID);
                    reportFileName = "DengiReceipt.rdlc";
                    reportPath = System.IO.Path.Combine(appDirectory, reportsFolder, reportFileName);
                    reportViewer2.LocalReport.ReportPath = reportPath;
                    reportDataSource = new ReportDataSource("DataSet1", dt);
                    reportViewer2.LocalReport.DataSources.Add(reportDataSource);
                    DataTable dt1 = (DataTable)reportDataSource.Value;
                    addCustomField(dt1);
                    reportViewer2.RefreshReport();
                    DocumentName = "DengiReceipt";
                    printReport(DocumentName,PrinterNames.DengiPrint);

                }
                if (flag == "DECLARATION")
                {
                    dt = da.getDengiReceiptDataForReport(receiptID);
                    reportFileName = "DengiDeclaration.rdlc";
                    reportPath = System.IO.Path.Combine(appDirectory, reportsFolder, reportFileName);
                    reportViewer2.LocalReport.ReportPath = reportPath;
                    reportDataSource = new ReportDataSource("DataSet1", dt);
                    reportViewer2.LocalReport.DataSources.Add(reportDataSource);
                    DataTable dt1 = (DataTable)reportDataSource.Value;
                    addCustomField(dt1);
                    reportViewer2.RefreshReport();
                    DocumentName = "DengiDeclaration";
                    printReport(DocumentName,PrinterNames.DengiDeclaration);
                }
            }
            if (form == "Bhojnalaya")
            {
                createBhojnalayRepoort();
            }
        }

        private void reportViewer2_Load(object sender, EventArgs e)
        {

        }
        public void addCustomField(DataTable dt)
        {
            if (dt.Columns.Contains("AMOUNT"))
            {
                foreach (DataRow row in dt.Rows)
                {
                    double amount = Convert.ToDouble(row["AMOUNT"]);
                    row["AMOUNT_IN_WORDS"] = cm.words(Convert.ToInt32(amount));
                    if (printType == "D")
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
        public void createBhojnalayRepoort()
        {
            bhojnalayDAL = new BhojnalayPrintReceiptDAL();
            DataTable dt = new DataTable();
            string reportPath = null;
            string reportFileName = null;
            string reportsFolder = "Reports";
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            ReportDataSource reportDataSource = null;
            string DocumentName = null;

            if (flag == "PRINT")
            {
                dt = bhojnalayDAL.getMessItemDataForReport(receiptID);
                reportFileName = "mess_item_receipt.rdlc";
                reportPath = System.IO.Path.Combine(appDirectory, reportsFolder, reportFileName);
                reportViewer2.LocalReport.ReportPath = reportPath;
                foreach (DataRow row in dt.Rows)
                {
                    reportViewer2.LocalReport.DataSources.Clear();
                    DataTable datatable = dt.Clone();
                    datatable.ImportRow(row);
                    reportDataSource = new ReportDataSource("DataSet1", datatable);
                    reportViewer2.LocalReport.DataSources.Add(reportDataSource);
                    DataTable dt1 = (DataTable)reportDataSource.Value;
                    addCustomField(dt1);
                    reportViewer2.RefreshReport();
                    DocumentName = "BhojnalayReceipt";
                    printReport(DocumentName, PrinterNames.BHPrint);
                }

            }
            if (flag == "DECLARATION")
            {
                dt = da.getDengiReceiptDataForReport(receiptID);
                reportFileName = "DengiDeclaration.rdlc";
                reportPath = System.IO.Path.Combine(appDirectory, reportsFolder, reportFileName);
                reportViewer2.LocalReport.ReportPath = reportPath;
                reportDataSource = new ReportDataSource("DataSet1", dt);
                reportViewer2.LocalReport.DataSources.Add(reportDataSource);
                DataTable dt1 = (DataTable)reportDataSource.Value;
                addCustomField(dt1);
                reportViewer2.RefreshReport();
                DocumentName = "BhojnalayDeclaration" +
                    "";
                printReport(DocumentName, PrinterNames.BHDeclaration);
            }
        }
        public void printDeclarationwithoutSave(DataTable dt)
        {
            string reportPath = null;
            string reportFileName = null;
            string reportsFolder = "Reports";
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            ReportDataSource reportDataSource = null;
            string DocumentName = null;
            reportFileName = "DengiDeclaration.rdlc";
            reportPath = System.IO.Path.Combine(appDirectory, reportsFolder, reportFileName);
            reportViewer2.LocalReport.ReportPath = reportPath;
            reportDataSource = new ReportDataSource("DataSet1", dt);
            reportViewer2.LocalReport.DataSources.Add(reportDataSource);
            DataTable dt1 = (DataTable)reportDataSource.Value;
            // addCustomField(dt1);
            reportViewer2.RefreshReport();
            DocumentName = "DengiDeclaration";
            printReport(DocumentName, PrinterNames.DengiDeclaration);
        }
    }
}

