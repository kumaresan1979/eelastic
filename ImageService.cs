using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace appathon_component
{
    public class ImageService
    {
        static string strContainer = CloudConfigurationManager.GetSetting("ContainerName");
        static string strLocalPath = CloudConfigurationManager.GetSetting("LocalPath");

        public async Task<string> UploadImageAsync(HttpPostedFileBase imageToUpload)
        {
            string imageFullPath = null;
            if (imageToUpload == null || imageToUpload.ContentLength == 0)
            {
                return null;
            }
            try
            {
                CloudStorageAccount cloudStorageAccount = ConnectionString.GetConnectionString();
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(strContainer);

                if (await cloudBlobContainer.CreateIfNotExistsAsync())
                {
                    await cloudBlobContainer.SetPermissionsAsync(
                        new BlobContainerPermissions
                        {
                            PublicAccess = BlobContainerPublicAccessType.Blob
                        }
                        );
                }
                string imageName = imageToUpload.FileName;
                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(imageName);
                cloudBlockBlob.Properties.ContentType = imageToUpload.ContentType;
                await cloudBlockBlob.UploadFromStreamAsync(imageToUpload.InputStream);
                imageFullPath = cloudBlockBlob.Uri.ToString().Substring(cloudBlockBlob.Uri.ToString().LastIndexOf("/")+1);
            }
            catch (Exception ex)
            {

            }
            return imageFullPath;
        }

        public List<appathon_component.Models.ImageBlob> ListAll()
        {
            CloudStorageAccount cloudStorageAccount = ConnectionString.GetConnectionString();
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(strContainer);

            if (cloudBlobContainer.CreateIfNotExists())
            {
                  cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions
                   {
                        PublicAccess = BlobContainerPublicAccessType.Blob
                    }
                    );
            }

            List<appathon_component.Models.ImageBlob> blobs = new List<appathon_component.Models.ImageBlob>();

            foreach (CloudBlockBlob blobItem in cloudBlobContainer.ListBlobs())
            {
                appathon_component.Models.ImageBlob itemblob = new appathon_component.Models.ImageBlob();
                itemblob.Uri = blobItem.Uri;
                itemblob.StorageUri = blobItem.StorageUri;
                itemblob.Container = blobItem.Container;
                itemblob.Parent = blobItem.Parent;
                blobs.Add(itemblob);
            }
            return blobs;
        }

        public string DeleteFile(string Name)
        {
            CloudStorageAccount cloudStorageAccount = ConnectionString.GetConnectionString();
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(strContainer);
            CloudBlockBlob myBlockBlob = cloudBlobContainer.GetBlockBlobReference(Name);
            myBlockBlob.Delete();
            return "File Deleted.";
        }


        public void AzureFileDownload(string FileName)
        {
            CloudStorageAccount cloudStorageAccount = ConnectionString.GetConnectionString();
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(strContainer);
            CloudBlockBlob myBlockBlob = cloudBlobContainer.GetBlockBlobReference(FileName);

            // path on local machine.
            using (var fileStream = System.IO.File.Open(@strLocalPath + "\\" + FileName, FileMode.OpenOrCreate))
            {
                myBlockBlob.DownloadToStream(fileStream);
            }
        }
        
    }
}


