using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Azure.KeyVault;
using System;
using System.IO;
using ProductoInvent.Models;
using System.Threading.Tasks;

namespace ProductoInvent.Shared
{
    public class AzureStorageBlob
    {

        private CloudStorageAccount _storageAccount;
        private CloudBlobContainer _container;
        private CloudBlobClient _blobClient;
        private CloudBlockBlob blob;
        const string blobPath = "/product/images/";
        private string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=azuredemorstorage;AccountKey=IsD6J8IlF56FEozAKZPoNXFWSNYKWcL/Gvciq0qTFgKiZGB5U64nXCYDlArAvg88nP6cbyPNg6CXs0FOLfbNbw==;EndpointSuffix=core.windows.net";
public AzureStorageBlob()
        {            

            _storageAccount = CreateStorageAccountFromConnectionString(storageConnectionString);


            _blobClient = _storageAccount.CreateCloudBlobClient();
            
             _container = _blobClient.GetContainerReference("productinventcontainer");

             _container.CreateIfNotExistsAsync();
            
        }

        
        public async Task<int> UploadToBlobAsync(ProductCollectionModel productCollectionModel)
        {
            try
            {
                blob = _container.GetBlockBlobReference(blobPath + productCollectionModel.FileName);
               await blob.UploadFromByteArrayAsync(productCollectionModel.ProductImage, 0, productCollectionModel.ProductImage.Length);
                return 1;
            }
            catch (Exception)
            {

                return 0;
            }
           
        }
      
        public async Task<byte[]> DownloadFromBlob(string blobName)
        {
            try
            {
                blob = _container.GetBlockBlobReference(blobPath+blobName);
                byte[] file = new byte[blob.StreamWriteSizeInBytes];
                await blob.DownloadToByteArrayAsync(file, 0);
                return file;
            }
            catch (StorageException ex)
            {

                return null;
            }
            
        }


        private static CloudStorageAccount CreateStorageAccountFromConnectionString(string storageConnectionString)
        {
            CloudStorageAccount storageAccount;
            try
            {
                storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            }
            catch (FormatException)
            {                
                throw;
            }
            catch (ArgumentException)
            {                
                throw;
            }

            return storageAccount;
        }


    }
}
