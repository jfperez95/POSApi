using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infraestructure.FileStorage
{
    public class AzureStorage : IAzureStorage
    {
        private readonly string _connectionString;
        public AzureStorage(IConfiguration configuratio)
        {
            _connectionString = configuratio.GetConnectionString("AzureStorage")!;
        }
        public async Task<string> EditFile(string container, IFormFile file, string route)
        {
            await RemoveFile(route, container);
            return await SaveFile(container, file);
        }

        public async Task RemoveFile(string route, string container)
        {
            if (string.IsNullOrEmpty(route))
            {
                return ;
            }

            var client = new BlobContainerClient(_connectionString, container);

            await client.CreateIfNotExistsAsync();

            var file = Path.GetExtension(route);
            var blob = client.GetBlobClient(file);

            await blob.DeleteIfExistsAsync();
        }

        public async Task<string> SaveFile(string container, IFormFile file)
        {
            var client = new BlobContainerClient(_connectionString, container);

            await client.CreateIfNotExistsAsync();

            await client.SetAccessPolicyAsync(PublicAccessType.Blob);

            var extension = Path.GetExtension(file.FileName);

            var fileName = $"{Guid.NewGuid()}{extension}";

            var blob = client.GetBlobClient(fileName);

            await blob.UploadAsync(file.OpenReadStream());

            return blob.Uri.ToString();
        }
    }
}
