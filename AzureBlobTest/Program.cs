using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureBlobTest
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Start sample\n");

            // Quick start for all container functions
            ContainerQuickStart.ProcessAsync().GetAwaiter().GetResult();

            //Quick demo for upload file to Azure Blob Storage
            UploadFileToStorage.UploadFileAsync().GetAwaiter().GetResult();            

            Console.WriteLine("\n Press any key for exit");

            Console.ReadKey();

        }        
    }
}
