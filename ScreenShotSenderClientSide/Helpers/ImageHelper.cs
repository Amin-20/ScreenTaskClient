using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ScreenShotSenderClientSide.Helpers
{
    public class ImageHelper
    {
        public string FolderPath { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"CLIENT{NetworkHelper.client.Client.LocalEndPoint.ToString().Replace(":", ".")}.Images");
        public void CreateFolder()
        {
            int number = 0;
            while (Directory.Exists(FolderPath))
            {
                if (number != 0)
                {
                    FolderPath.Replace((number - 1).ToString(), "");
                    FolderPath += number;
                }
                number++;
            }

            Directory.CreateDirectory(FolderPath);
        }

        public byte[] GetBytesOfImage(string path)
        {
            var image = new Bitmap(path);
            ImageConverter imageconverter = new ImageConverter();
            var imagebytes = (byte[])imageconverter.ConvertTo(image, typeof(byte[]));
            return imagebytes;
        }

        public string TakeScreenShot()
        {
            Bitmap bmp = new Bitmap(1920, 1080);
            var id = Guid.NewGuid().ToString();
            var source = Path.Combine(FolderPath, "screenshot" + id + ".png");

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(0, 0, 0, 0, new System.Drawing.Size(1920, 1080));
                bmp.Save(source);  
            }
            return source;
        }
    }
}
