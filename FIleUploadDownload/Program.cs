using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReadWrite
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "E:\\Documents\\MUSA ePDI\\MassDefectUpload.xlsx";

            string extension = Path.GetExtension(path);

            string filename = Path.GetFileName(path);

            string filenameNoextension = Path.GetFileNameWithoutExtension(path);           

            string root = Path.GetPathRoot(path);           

        }
    }
}
