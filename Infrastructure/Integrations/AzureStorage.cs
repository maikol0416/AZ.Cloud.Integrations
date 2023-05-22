using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using Utilidades;

namespace Infrastructure.Integration
{
    public class AzureStorage 
    {
        private IConfiguration Configuration { get; }

        private BlobContainerClient containerClient = null;

        public AzureStorage(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task<string> UploadFileStorage(byte[] file, string name, string ext, string destino)
        {
            try
            {
                if (file is null)
                {
                    return string.Empty;
                }
                await ConectionStorage(destino);

                string fileName = name + ext;

                BlobClient blobClient = containerClient.GetBlobClient(fileName);

                await blobClient.DeleteIfExistsAsync();

                using (var stream = new MemoryStream(file, writable: false))
                {
                    await blobClient.UploadAsync(stream, true);
                }

                return blobClient.Uri.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private async Task ConectionStorage(string destino)
        {
            try
            {
                string connectionString = Configuration[Constants.AzureBlobVariable];
                BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
                containerClient = blobServiceClient.GetBlobContainerClient(destino);
                await containerClient.CreateIfNotExistsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
