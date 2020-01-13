using iText.Html2pdf;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaGeneration
{
    public class Program
    {
        public static String BASEURI = "src/main/resources/html/";
        public static String SRC = String.Format("{0}hello.html", BASEURI);
        public static String TARGET = "target/results/ch01/";
        public static String DEST = String.Format("{0}test.pdf", TARGET);
       // public static String HTML = "<h1>Test</h1><p>Hello World</p>";
        /// <exception cref="System.IO.IOException"/>
        public static void Main(String[] args)
        {
            try
            {
                FileInfo file = new FileInfo(DEST);
                file.Directory.Create();                
                Directory.CreateDirectory(BASEURI);              
                string htmlString = "";
                using (FileStream fs = File.Open(BASEURI + "/Sample.html", FileMode.Open, FileAccess.ReadWrite))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        htmlString = sr.ReadToEnd();
                    }
                }
                new Program().createPdf(BASEURI, htmlString, DEST);               

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message+"/n"+ex.StackTrace);
                Console.ReadKey();
            }
            

        }

        /// <exception cref="System.IO.IOException"/>
        public virtual void CreatePdf(String dest)
        {
            //Initialize PDF writer
            PdfWriter writer = new PdfWriter(dest);
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(writer);
            // Initialize document
            Document document = new Document(pdf);
            //Add paragraph to the document
            document.Add(new Paragraph("Hello World!"));
            //Close document
            document.Close();
        }


        public void createPdf(string html, string dest)
        {
            HtmlConverter.ConvertToPdf(html, new FileStream(dest, FileMode.Create));
        }

        public void createPdf(string baseUri, string html, string dest)
        {
            ConverterProperties properties = new ConverterProperties();
            properties.SetBaseUri(baseUri);
            HtmlConverter.ConvertToPdf(html, new FileStream(dest, FileMode.Create), properties);
        }
    }
}
