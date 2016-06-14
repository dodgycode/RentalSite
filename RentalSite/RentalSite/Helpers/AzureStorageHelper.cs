using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types
using System;
using System.Threading.Tasks;
using System.Web;

namespace RentalSite.Helpers
{
    public static class AzureStorageHelper
    {
        #region Module level variables
        private static CloudBlobContainer _blobContainer;
        #endregion

        #region Shared methods
        /// <summary>
        /// Returns reference to blob when passed blob reference
        /// </summary>
        /// <param name="blobRef"></param>
        /// <returns></returns>
        public static CloudBlockBlob GetBlob(string blobRef)
        {
            if (_blobContainer == null) { GetBlobContainer(); }
            return _blobContainer.GetBlockBlobReference(blobRef);
        }

        /// <summary>
        /// Uploads image to blob container. Returns safe URL.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>Image URL</returns>
        public async static Task<string> UploadPhotoAsync(HttpPostedFileBase photoToUpload)
        {
            // Upload image to Blob Storage
            string blobRef = Guid.NewGuid().ToString();
            CloudBlockBlob blockBlob = GetBlob(blobRef);
            blockBlob.Properties.ContentType = photoToUpload.ContentType;
            await blockBlob.UploadFromStreamAsync(photoToUpload.InputStream);

            // Convert to be HTTP based URI (default storage path is HTTPS)
            var uriBuilder = new UriBuilder(blockBlob.Uri);
            uriBuilder.Scheme = "http";
            string fullPath = uriBuilder.ToString();

            return fullPath;
        }

        #endregion

        #region Private methods
        /// <summary>
        /// Finds the blob container for property images
        /// </summary>
        private static void GetBlobContainer()
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container.
            _blobContainer = blobClient.GetContainerReference("propertyimages");
        }
        #endregion
    }
}