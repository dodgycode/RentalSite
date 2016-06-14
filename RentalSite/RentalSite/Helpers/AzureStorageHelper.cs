using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types
using System;
using System.IO;

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
        /// Uploads image to blob container. Returns blob reference.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>Blob reference</returns>
        public static string UploadBlob(string filePath)
        {
            string blobRef = Guid.NewGuid().ToString();
            CloudBlockBlob blob = _blobContainer.GetBlockBlobReference(blobRef);
            blob.UploadFromFileAsync(filePath);
            return blobRef;
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
            CloudBlobContainer container = blobClient.GetContainerReference("propertyimages");

            _blobContainer = container;
        }
        #endregion
    }
}