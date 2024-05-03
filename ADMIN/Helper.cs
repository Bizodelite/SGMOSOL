using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using Microsoft.VisualBasic;
using System.Runtime.InteropServices;

namespace SGMOSOL.ADMIN
{
    class Helper
    {
        public static string final;
        public static void SaveImageCapture(System.Drawing.Image image, string UNName)
        {
            SaveFileDialog s = new SaveFileDialog();
            string finalpath;
            finalpath = System.Configuration.ConfigurationSettings.AppSettings.Get("IMAGE_PATH");
            // finalpath = System.IO.Path.GetFullPath("C:\Users\priyanka\Desktop\OSOL\Images\TempImage\")
            // Dim finalpath As String = Server.MapPath("\Images\studThumbImage\")

            DateTime dt = DateTime.Now;
            string year = dt.Year.ToString();
            string month = dt.Month.ToString();
            string day = dt.Day.ToString();
            string hours = dt.Hour.ToString();
            string min = dt.Minute.ToString();
            string sec = dt.Second.ToString();
            string milisec = dt.Millisecond.ToString();
            string curdatetime = day + month + year + hours + min + sec + milisec + ".jpg";

            // final = "person-Img-" & curdatetime
            final = UNName + curdatetime;
            finalpath = finalpath + final;
            // Save Image
            string filename = finalpath;
            FileStream fstream = new FileStream(filename, FileMode.Create);
            image.Save(fstream, System.Drawing.Imaging.ImageFormat.Jpeg);
            // SaveThumbnailImages(image, filename)
            fstream.Close();
            //return filename;
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        public static bool isImageExistance(string imageName, string checkImgIn)
        {
            try
            {
                string ImagePath = string.Empty;
                ImagePath = System.Configuration.ConfigurationSettings.AppSettings.Get("IMAGE_PATH") + imageName;
                if (System.IO.File.Exists(ImagePath))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        private static void SaveThumbnailImages(System.Drawing.Image image, string fileName)
        {
            string ApplicationBase = null;
            string SavePath = "";

            ApplicationBase = AppDomain.CurrentDomain.BaseDirectory.ToString();
            System.IO.DirectoryInfo DirInfo = new System.IO.DirectoryInfo(ApplicationBase);
            ApplicationBase = DirInfo.FullName;

            SavePath = System.Configuration.ConfigurationSettings.AppSettings.Get("IMAGE_PATH") + final;
            GenerateTumbImage(image, SavePath, fileName, 50, 50);
        }

        private static void GenerateTumbImage(System.Drawing.Image image, string SubDirName, string fileName, int Width, int Height)
        {
            int newWidth;
            int newHeight;
            int originalWidth = image.Width;
            int originalHeight = image.Height;

            if ((originalWidth > Width) && (originalHeight > Height))
            {
                float percentWidth = System.Convert.ToSingle(Width) / System.Convert.ToSingle(originalWidth);
                float percentHeight = System.Convert.ToSingle(Height) / System.Convert.ToSingle(originalHeight);
                float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                newWidth = System.Convert.ToInt32(Math.Truncate(originalWidth * percent));
                newHeight = System.Convert.ToInt32(Math.Truncate(originalHeight * percent));
            }
            else
            {
                newWidth = Width;
                newHeight = Height;
            }
        }

        private static Bitmap resizeImage(System.Drawing.Image image, int newWidth, int newHeight)
        {
            Bitmap bmp1 = new Bitmap(newWidth, newHeight);
            using (Graphics graphicsHandle = Graphics.FromImage(bmp1))
            {
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsHandle.SmoothingMode = SmoothingMode.HighQuality;
                graphicsHandle.CompositingQuality = CompositingQuality.HighQuality;

                Rectangle imageRec = new Rectangle(0, 0, newWidth, newHeight);
                // graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight);

                graphicsHandle.DrawImage(image, imageRec);
            }
            return bmp1;
        }



        public string Final_SaveImageCapture(System.Drawing.Image image, string UNName)
        {
            SaveFileDialog s = new SaveFileDialog();
            string finalpath;
            finalpath = System.Configuration.ConfigurationSettings.AppSettings.Get("IMAGE_PATH");
            // finalpath = System.IO.Path.GetFullPath("C:\Users\priyanka\Desktop\OSOL\Images\TempImage\")
            // Dim finalpath As String = Server.MapPath("\Images\studThumbImage\")

            DateTime dt = DateTime.Now;
            string year = dt.Year.ToString();
            string month = dt.Month.ToString();
            string day = dt.Day.ToString();
            string hours = dt.Hour.ToString();
            string min = dt.Minute.ToString();
            string sec = dt.Second.ToString();
            string milisec = dt.Millisecond.ToString();
            string curdatetime = day + month + year + hours + min + sec + milisec + ".jpg";


            // final = "person-Img-" & curdatetime
            final = UNName + curdatetime;


            finalpath = finalpath + final;


            // Save Image
            string filename = finalpath;


            FileStream fstream = new FileStream(filename, FileMode.Create);
            image.Save(fstream, System.Drawing.Imaging.ImageFormat.Jpeg);
            // SaveThumbnailImages(image, filename)
            fstream.Close();

            return filename;
        }
    }

}
