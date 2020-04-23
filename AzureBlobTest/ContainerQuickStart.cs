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
    public class ContainerQuickStart
    {
        public static async Task ProcessAsync()
        {
            try
            {

            Console.WriteLine("\n Start Container Quick start !\n");
            // Azure storage account connection string
            string storageConnectionString = "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;";

            CloudStorageAccount storageAccount;

            //check for valid connection string using tryparse method
            if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
            {
                //sucessfully connect with storage account and stores connection in to storageAcoount variable

                //create blobclient 
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                //create container refernce from  current blob client
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("quickstartblobs" + Guid.NewGuid().ToString());

                //create container in azure blob storage
                await cloudBlobContainer.CreateAsync();

                //create public access permissions to container
                BlobContainerPermissions permissions = new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                };

                //set public access permissions to container
                await cloudBlobContainer.SetPermissionsAsync(permissions);
                
                //local folder path
                string localPath = @"D:\BlobTest";
                
                //create local file name appending guid id
                string localFileName = "QuickStart_" + Guid.NewGuid().ToString() + ".txt";

                //bind path to local folder
                string sourceFile = Path.Combine(localPath, localFileName);

                //create file and write text to newly created .txt file
                File.WriteAllText(sourceFile, "Welocme Test !\n");

                //for uploading file on blob storage need to create oject for cloudblock blob using name as localfile name
                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(localFileName);

                //Upload file from local folder location directly to newly created container
                await cloudBlockBlob.UploadFromFileAsync(sourceFile);

                #region This block get the list of all blob files contains in current container
                BlobContinuationToken blobContinuationToken = null;
                do
                {
                    var results = await cloudBlobContainer.ListBlobsSegmentedAsync(null, blobContinuationToken);

                    blobContinuationToken = results.ContinuationToken;

                    foreach (IListBlobItem item in results.Results)
                    {
                       // Console.WriteLine(item.Uri.ToString()+"\n");
                    }

                } while (blobContinuationToken != null);

                #endregion

                // destination file name for download file
                string destinationFile = sourceFile.Replace(".txt", "_DOWNLOAD.txt");

                //download file from blob storage and stored into destination file path
                await cloudBlockBlob.DownloadToFileAsync(destinationFile, FileMode.Create);

                //delete current blob from azure blob storage
                await cloudBlockBlob.DeleteIfExistsAsync();

                //delete current container from azure blob storage
                if (cloudBlobContainer != null)
                {
                    await cloudBlobContainer.DeleteIfExistsAsync();
                }

                //delete files from local folder
                File.Delete(sourceFile);
                File.Delete(destinationFile);

               
            }
            else
            {
                Console.WriteLine("Connection invalid");
            }

            Console.WriteLine("\n End Container Quick start !\n");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Container Quick start exception !\n");

                Console.WriteLine(ex.InnerException);
            }
        }
    }
}
