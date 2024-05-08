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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using SGMOSOL.DAL;
using SGMOSOL.DataSet;
using Microsoft.Identity.Client;
using SGMOSOL.ADMIN;

using System.Diagnostics;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using System.IO;




namespace SGMOSOL.SCREENS
{
    public partial class frmReportViewer : Form
    {
        private string receiptID = null;
        private string printType = null;
        public string flag { get; set; }
        public DataTable DataTableDT { get; set; }
        DengiReceiptDAL da;
        BhojnalayPrintReceiptDAL bhojnalayDAL;
        CommonFunctions cm = new CommonFunctions();
        string strSerialNumberPrint = null;
        string filePath = null;
        public enum PrinterNames
        {
            DengiDeclaration,
            DengiPrint,
            BhojnalayReceipt,
            BhojnalayDec,
            Locker
        }

        public frmReportViewer(string flag, string value = null, string PrintType = null, DataTable DT = null)
        {
            InitializeComponent();
            this.receiptID = value;
            this.flag = flag;
            this.printType = PrintType;
            this.DataTableDT = DT;
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
            try
            {
                cm.AppendToFile("Report content is ready:-" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                string printerName = null;
                if (PrinterType == PrinterNames.DengiDeclaration)
                {
                    printerName = System.Configuration.ConfigurationManager.AppSettings["DengiDec_Printer_name"].ToString();
                }
                if (PrinterType == PrinterNames.DengiPrint)
                {
                    printerName = System.Configuration.ConfigurationManager.AppSettings["DengiReceipt_Printer_name"].ToString();
                }
                if (PrinterType == PrinterNames.BhojnalayReceipt)
                {
                    printerName = System.Configuration.ConfigurationManager.AppSettings["BhojnalayReceipt"].ToString();
                }
                if (PrinterType == PrinterNames.BhojnalayDec)
                {
                    printerName = System.Configuration.ConfigurationManager.AppSettings["BhojnalayDec"].ToString();
                }
                if (PrinterType == PrinterNames.Locker)
                {
                    printerName = System.Configuration.ConfigurationManager.AppSettings["LockerReceipt"].ToString();
                }
                byte[] renderedBytes = reportViewer2.LocalReport.Render("Image");

                using (System.IO.MemoryStream stream = new System.IO.MemoryStream(renderedBytes))
                {
                    try
                    {
                        using (System.Drawing.Image image = System.Drawing.Image.FromStream(stream))
                        {

                            PrintDocument printDoc = new PrintDocument();
                            printDoc.PrinterSettings.PrinterName = printerName;
                            printDoc.DocumentName = docName;
                            PaperSize paperSize = new PaperSize("Custom", (int)(4.84 * 100), (int)(5.70 * 100));
                            printDoc.DefaultPageSettings.PaperSize = paperSize;
                            printDoc.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
                            DialogResult result = MessageBox.Show("The Receipt with  serial number " + strSerialNumberPrint + " being sent to the printer. Do you want to continue?", "Print", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (result == DialogResult.Yes)
                            {

                                //add watermark
                                cm.AppendToFile("saving file in folder " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


                                // Add watermark for saving file
                                using (Bitmap bitmapWithWatermark = new Bitmap(image.Width, image.Height))
                                {
                                    using (Graphics graphics = Graphics.FromImage(bitmapWithWatermark))
                                    {
                                        graphics.DrawImage(image, new Point(0, 0));

                                        using (StringFormat stringFormat = new StringFormat())
                                        using (Font watermarkFont = new Font("Arial", 100))
                                        using (SolidBrush watermarkBrush = new SolidBrush(Color.FromArgb(35, Color.Red))) // Adjust opacity and color as needed
                                        {
                                            string watermarkText = "SSGMSS"; // Your watermark text
                                            SizeF textSize = graphics.MeasureString(watermarkText, watermarkFont);

                                            // Position the watermark diagonally across the receipt
                                            float centerX = (bitmapWithWatermark.Width - textSize.Width) / 2;
                                            float centerY = (bitmapWithWatermark.Height - textSize.Height) / 2;

                                            // Apply rotation for diagonal watermark
                                            Matrix matrix = new Matrix();
                                            matrix.RotateAt(-45, new PointF(centerX + textSize.Width / 2, centerY + textSize.Height / 2));
                                            graphics.Transform = matrix;
                                            graphics.DrawString(watermarkText, watermarkFont, watermarkBrush, new PointF(centerX, centerY), stringFormat);

                                            // Save the file with watermark
                                            bitmapWithWatermark.Save(filePath, ImageFormat.Png);
                                            cm.AppendToFile("Saved file in folder " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                        }
                                    }
                                }

                                //ends here 
                                printDoc.PrintPage += (s, args) =>
                                {
                                    cm.AppendToFile("It is getting ready to print page (checking page..)  " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                    args.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                    args.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                                    args.Graphics.DrawImage(image, args.MarginBounds);
                                    cm.AppendToFile("It is setting margine " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                };
                                cm.AppendToFile("It is  printing content on page " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                printDoc.Print();
                                cm.AppendToFile("Successfully done with printing process " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        cm.AppendToFile("Failed Report In Print Function " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        cm.InsertErrorLog(ex.Message, "while printing Dengi receipt ", UserInfo.version);
                    }
                }
            }
            catch (Exception ex)
            {
                cm.AppendToFile("Failed Report while Printing " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cm.InsertErrorLog(ex.Message, "while printing Dengi receipt ", UserInfo.version);
            }

        }


        public void createReport(string form)
        {

            try
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
                        foreach (DataRow row in dt1.Rows)
                        {
                            strSerialNumberPrint = row["SERIAL_NO"].ToString();
                            cm.AppendToFile("Serial_Number:-" + strSerialNumberPrint);
                        }
                        // MessageBox.Show("Printing Dengi Receipt for Serial number " + strSerialNumberPrint);
                        filePath = System.Configuration.ConfigurationManager.AppSettings["DENGI_PRINTS"].ToString() + "\\DengiReceipt_" + strSerialNumberPrint + ".png"; //
                        printReport(DocumentName, PrinterNames.DengiPrint);

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
                        foreach (DataRow row in dt1.Rows)
                        {
                            strSerialNumberPrint = row["SERIAL_NO"].ToString();
                        }
                        MessageBox.Show("Printing Dengi Declaration for Serial number " + strSerialNumberPrint);
                        printReport(DocumentName, PrinterNames.DengiDeclaration);
                    }
                }
                if (form == "Bhojnalaya")
                {
                    createBhojnalayRepoort();
                }
                if (form == "LockerCheckIN")
                {
                    LockerCheckInReport();
                }
                if (form == "LockerCheckOut")
                {
                    LockerCheckOutReport();
                }
            }
            catch (Exception ex)
            {
                cm.AppendToFile("Failed Report while Creating :-Serial No " + strSerialNumberPrint + " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cm.InsertErrorLog(ex.Message, "While creating receipt", UserInfo.version);
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
                    foreach (DataRow row1 in dt1.Rows)
                    {
                        strSerialNumberPrint = row1["ITEM_PRINT_RECEIPT_ID"].ToString();
                    }
                  
                    cm.AppendToFile("Serial_Number:-" + strSerialNumberPrint);
                    filePath = System.Configuration.ConfigurationManager.AppSettings["MESS_PRINTS"].ToString() + "\\MessReceipt_" + strSerialNumberPrint + ".png"; //
                    printReport(DocumentName, PrinterNames.BhojnalayReceipt);
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
                printReport(DocumentName, PrinterNames.BhojnalayDec);
            }
        }
        public void printDeclarationwithoutSave(DataTable dt, string formType = null)
        {
            try
            {
                string reportPath = null;
                string reportFileName = null;
                string reportsFolder = "Reports";
                string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                ReportDataSource reportDataSource = null;
                string DocumentName = null;
                reportFileName = "DengiDeclaration.rdlc";
                DocumentName = "DengiDeclaration";

                reportPath = System.IO.Path.Combine(appDirectory, reportsFolder, reportFileName);
                reportViewer2.LocalReport.ReportPath = reportPath;
                reportDataSource = new ReportDataSource("DataSet1", dt);
                reportViewer2.LocalReport.DataSources.Add(reportDataSource);
                DataTable dt1 = (DataTable)reportDataSource.Value;
                // addCustomField(dt1);
                reportViewer2.RefreshReport();
                printReport(DocumentName, PrinterNames.DengiDeclaration);
            }
            catch (Exception ex)
            {
                cm.InsertErrorLog(ex.Message, UserInfo.module, UserInfo.version);
            }


        }
        public void LockerCheckInReport()
        {
            bhojnalayDAL = new BhojnalayPrintReceiptDAL();
            DataTable dt = new DataTable();
            string reportPath = null;
            string reportFileName = null;
            string reportsFolder = "Reports";
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            appDirectory = appDirectory.Replace("bin\\Debug\\", "");
            ReportDataSource reportDataSource = null;
            string DocumentName = null;
            reportViewer2.LocalReport.DataSources.Clear();
            if (flag == "PRINT")
            {
                dt = DataTableDT.Copy();// bhojnalayDAL.getMessItemDataForReport(receiptID);
                reportFileName = "LockerCheckInReceipt_New.rdlc";   
                reportPath = System.IO.Path.Combine(appDirectory, reportsFolder, reportFileName);
                reportViewer2.LocalReport.ReportPath = reportPath;
                foreach (DataRow row in dt.Rows)
                {
                    DataTable datatable = dt.Clone();
                    datatable.ImportRow(row);
                    reportDataSource = new ReportDataSource("DataSet1", datatable);
                    reportViewer2.LocalReport.DataSources.Add(reportDataSource);
                    DataTable dt1 = (DataTable)reportDataSource.Value;
                    addCustomField(dt1);
                    foreach (DataRow row1 in dt1.Rows)
                    {
                        strSerialNumberPrint = row1["SERIAL_NO"].ToString();
                    }
                    reportViewer2.RefreshReport();
                    DocumentName = "LockerCheckInReceipt";
                    filePath = System.Configuration.ConfigurationManager.AppSettings["LOCKER_PRINTS"].ToString() + "\\LockerPrints" + strSerialNumberPrint + ".png"; //
                    printReport(DocumentName, PrinterNames.Locker);
                }
            }
        }
        public void LockerCheckOutReport()
        {
            bhojnalayDAL = new BhojnalayPrintReceiptDAL();
            DataTable dt = new DataTable();
            string reportPath = null;
            string reportFileName = null;
            string reportsFolder = "Reports";
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            appDirectory = appDirectory.Replace("bin\\Debug\\", "");
            ReportDataSource reportDataSource = null;
            string DocumentName = null;
            reportViewer2.LocalReport.DataSources.Clear();
            if (flag == "PRINT")
            {
                dt = DataTableDT.Copy();// bhojnalayDAL.getMessItemDataForReport(receiptID);
                reportFileName = "LockerCheckOutReceipt.rdlc";
                reportPath = System.IO.Path.Combine(appDirectory, reportsFolder, reportFileName);
                reportViewer2.LocalReport.ReportPath = reportPath;
                foreach (DataRow row in dt.Rows)
                {
                    DataTable datatable = dt.Clone();
                    datatable.ImportRow(row);
                    reportDataSource = new ReportDataSource("DataSet1", datatable);
                    reportViewer2.LocalReport.DataSources.Add(reportDataSource);
                    DataTable dt1 = (DataTable)reportDataSource.Value;
                    addCustomField(dt1);
                    reportViewer2.RefreshReport();
                    DocumentName = "LockerCheckOutReceipt";
                    printReport(DocumentName, PrinterNames.Locker);
                }
            }
        }
    }
}

