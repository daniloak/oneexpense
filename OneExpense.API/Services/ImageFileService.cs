using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using OneExpense.Business.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OneExpense.API.Services
{
    public class ImageFileService : IImageFileService
    {
        private readonly IConfiguration _configuration;
        public ImageFileService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> Upload(string file, string imageName)
        {
            var azureConnectionString = _configuration.GetValue<string>("AZURE_STORAGE_CONNECTION_STRING");
            var blobContainerClient = new BlobContainerClient(azureConnectionString, "expense-images");
            var blob = blobContainerClient.GetBlobClient(imageName);
            var imageDataByteArray = Convert.FromBase64String(file);
            var stream = new MemoryStream(imageDataByteArray);

            await blob.UploadAsync(stream);

            return blob.Uri.AbsoluteUri;
        }
    }
}
