using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types
using System;
using System.Threading.Tasks;
using System.Web;
using RentalSite.Models;
using System.Data.Entity;

namespace RentalSite.Helpers
{
    public static class AzureStorageHelper
    {
        #region Module level variables
        private static CloudBlobContainer _blobContainer;
        #endregion

        #region Shared methods
     
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
            string fullPath = uriBuilder.ToString();

            return fullPath;
        }

        /// <summary>
        /// If adding property fails, delete photos from storage
        /// </summary>
        /// <param name="propertyImages"></param>
        internal static void DeletePhotos(DbSet<PropertyImage> propertyImages)
        {
            foreach (var img in propertyImages)
            {
                var url = img.ImageURL.Split('/');
                string blobRef = url[url.Length - 1];

                DeleteBlob(blobRef);
            }
        }


        #endregion

        #region Private methods
        /// <summary>
        /// Delete blob from Azure storage by blob reference
        /// </summary>
        /// <param name="blobRef"></param>
        private static void DeleteBlob(string blobRef)
        {
            CloudBlockBlob currBlob = GetBlob(blobRef);
            currBlob.DeleteIfExists();
        }

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

        /// <summary>
        /// Returns reference to blob when passed blob reference
        /// </summary>
        /// <param name="blobRef"></param>
        /// <returns></returns>
        private static CloudBlockBlob GetBlob(string blobRef)
        {
            if (_blobContainer == null) { GetBlobContainer(); }
            return _blobContainer.GetBlockBlobReference(blobRef);
        }
        #endregion
    }
}