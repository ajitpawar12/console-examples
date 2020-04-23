using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureBlobTest
{
    public class UploadFileToStorage
    {
        public static async Task UploadFileAsync()
        {
            //example for upload file to blob storage

            try
            {
                Console.WriteLine("\n Start Upload file to storage !\n");

                //connection string of Azure storage account 
                string storageConnectionString = "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;";

                string localPath = @"D:\BlobTest\";
                string localFileName = "TestImage.png";
                string sourceFile = Path.Combine(localPath, localFileName);
                string extension = Path.GetExtension(sourceFile);

                // Get file and coverted into file stream
                FileStream fs = new FileStream(sourceFile, FileMode.Open, FileAccess.Read);

                CloudStorageAccount cloudStotageAcoount;

                // connect to azure storage account using connectio string
                if (CloudStorageAccount.TryParse(storageConnectionString, out cloudStotageAcoount))
                {
                    //Sucessfully connect to azure storage account

                    //create blobclient
                    CloudBlobClient cloudBlobClient = cloudStotageAcoount.CreateCloudBlobClient();

                    //container name
                    string containerName = "mediafilescontainer";

                    //create reference for container
                    CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);

                    //check for container exists, if container not exists then create new container on azure blob storage
                    if (await cloudBlobContainer.CreateIfNotExistsAsync())
                    {
                        await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
                    }

                    //create cloudblock blob for upload or delete blob
                    CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(localFileName);

                    //set content type for blob which is whatever the extension file has
                    cloudBlockBlob.Properties.ContentType = extension;

                    //upload file from file stream to azure blob storage against current container
                    await cloudBlockBlob.UploadFromStreamAsync(fs, fs.Length);

                    //Get url for uploaded file
                    var path = cloudBlockBlob.Uri.AbsoluteUri;
                    
                    MemoryStream memStream = new MemoryStream();

                    // download the uploaded file to memory stream
                    cloudBlockBlob.DownloadToStream(memStream);

                    string destinationFile = sourceFile.Replace(".png", "_DOWNLOAD.png");

                    //download the uploaded file and stored on destination folder
                    await cloudBlockBlob.DownloadToFileAsync(destinationFile, FileMode.Create);

                    //delete the file from azure blob storage
                    await cloudBlockBlob.DeleteIfExistsAsync();
                }
                else
                {
                    Console.WriteLine("Azure connection failed !");
                }

                Console.WriteLine("\n End Upload file to storage !\n");
            }
            
            catch (Exception ex)
            {
                Console.WriteLine("Upload file to storage exception !\n");

                Console.WriteLine(ex.InnerException);
            }
        }
    }
}
