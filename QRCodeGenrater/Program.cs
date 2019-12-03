using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRCodeGenrater
{
    class Program
    {
        static void Main(string[] args)
        {
            try {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode("The text which should be encoded.", QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            var bitmapBytes = BitmapToBytes(qrCodeImage); //Convert bitmap into a byte array
                
            string filePath = HttpContext.Current.Server.MapPath("~/DefectImages") + "/" + 1234 + ".Jpeg";

                using (MemoryStream ms = new MemoryStream(bitmapBytes))
                {
                    using (FileStream file = new FileStream(filePath, FileMode.Create, System.IO.FileAccess.Write))
                    {
                        ms.Read(bitmapBytes, 0, (int)ms.Length);
                        file.Write(bitmapBytes, 0, bitmapBytes.Length); ms.Close();
                    }
                }


                Console.WriteLine("");

            Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
        private static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

    }
}
