using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageConversion
{
    class Program
    {
        static void Main(string[] args)
        {
            string imagePath = @"D:\Ajit\25415.jpg";
            string imgBase64String = GetBase64StringForImage(imagePath);
            Console.WriteLine(imgBase64String);
            Console.ReadKey();
        }

        protected static string GetBase64StringForImage(string imgPath)
        {
            byte[] imageBytes = System.IO.File.ReadAllBytes(imgPath);
            string base64String = Convert.ToBase64String(imageBytes);
            return base64String;
        }
    }
}
