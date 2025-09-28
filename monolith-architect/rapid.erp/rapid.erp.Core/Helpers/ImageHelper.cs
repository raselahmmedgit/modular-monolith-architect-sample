using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rapid.erp.Core.Helpers
{
    public class ImageHelper
    {
        public static bool IsImage(string fileName)
        {
            bool result = new bool();
            string targetExtension = Path.GetExtension(fileName);
            List<string> ImageExtensions = new List<string> { ".JPG", ".JPEG", ".BMP", ".GIF", ".PNG" };
            if (ImageExtensions.Contains(targetExtension.ToUpperInvariant()))
                result = true;
            return result;
        }
        public static string CropImage(string webRootPath, string originalPath, string originalFileName, string newFileName, int Width, int Height, bool needToFill)
        {
            using (var image = Image.FromFile(Path.Combine(webRootPath, originalPath + originalFileName)))
            {
                int sourceWidth = image.Width;
                int sourceHeight = image.Height;
                int sourceX = 0;
                int sourceY = 0;
                double destX = 0;
                double destY = 0;

                double nScale = 0;
                double nScaleW = 0;
                double nScaleH = 0;

                nScaleW = ((double)Width / (double)sourceWidth);
                nScaleH = ((double)Height / (double)sourceHeight);
                if (!needToFill)
                    nScale = Math.Min(nScaleH, nScaleW);
                else
                {
                    nScale = Math.Max(nScaleH, nScaleW);
                    destY = (Height - sourceHeight * nScale) / 2;
                    destX = (Width - sourceWidth * nScale) / 2;
                }

                int destWidth = (int)Math.Round(sourceWidth * nScale);
                int destHeight = (int)Math.Round(sourceHeight * nScale);

                Bitmap bmPhoto = null;
                bmPhoto = new Bitmap(destWidth + (int)Math.Round(2 * destX), destHeight + (int)Math.Round(2 * destY));

                using (Graphics grPhoto = Graphics.FromImage(bmPhoto))
                {
                    grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    grPhoto.CompositingQuality = CompositingQuality.HighQuality;
                    grPhoto.SmoothingMode = SmoothingMode.HighQuality;

                    Rectangle to = new Rectangle((int)Math.Round(destX), (int)Math.Round(destY), destWidth, destHeight);
                    Rectangle from = new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight);
                    grPhoto.DrawImage(image, to, from, GraphicsUnit.Pixel);
                    image.Dispose();
                }

                //Crops the image
                string savedPath = originalPath + newFileName;
                bmPhoto.Save(Path.Combine(webRootPath, savedPath));
                return "~/" + savedPath;
            }
        }

        public static Dictionary<string, bool> CheckFileCropApplicable(IFormFile file)
        {
            //https://github.com/CoreCompat/CoreCompat 
            //https://www.nuget.org/packages/System.Drawing.Common
            Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
            Image image = Image.FromStream(file.OpenReadStream());
            if ((image.Width < 150) && (image.Height < 150))
                dictionary.Add("Crop150", false);
            else
                dictionary.Add("Crop150", true);
            if ((image.Width < 250) && (image.Height < 250))
                dictionary.Add("Crop250", false);
            else
                dictionary.Add("Crop250", true);
            if ((image.Width < 500) && (image.Height < 500))
                dictionary.Add("Crop500", false);
            else
                dictionary.Add("Crop500", true);
            if ((image.Width < 800) && (image.Height < 600))
                dictionary.Add("Crop800X600", false);
            else
                dictionary.Add("Crop800X600", true);
            return dictionary;
        }
        public static string GetPath()
        {
            string currectYear = Convert.ToString(DateTime.Now.Year);
            string currectMonth = Convert.ToString(DateTime.Now.Month);
            string path = "upload/" + currectYear + "/" + currectMonth + "/";
            return path;
        }
    }
}
