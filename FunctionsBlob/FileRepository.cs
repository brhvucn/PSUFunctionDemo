using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PSUFunctionsDemoProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSUFunctionsDemoProject.FunctionsBlob
{
    public class FileRepository : IFileRepository
    {
        private readonly string containerName = "files";
        private readonly BlobServiceClient blobServiceClient;
        public FileRepository(IConfiguration config)
        {
            string connectionString = config["AzureWebJobsStorage"].ToString();
            blobServiceClient = new BlobServiceClient(connectionString);            
            // Create the container if it doesn't exist
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            containerClient.CreateIfNotExists();
        }

        public async Task<string> DownloadFile(string fileName)
        {
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(fileName);

            var response = await blobClient.DownloadAsync();
            using (var reader = new StreamReader(response.Value.Content, Encoding.UTF8))
            {
                return await reader.ReadToEndAsync();
            }
        }

        /// <summary>
        /// Forwarding the content to the UploadFileAsync method
        /// </summary>
        /// <param name="filename">Name of the file, this is a part of the uri, consider using a Guid for this.</param>
        /// <param name="content">The string content to opload, converted to byte[] and passed to UploadFileAsync</param>
        /// <returns></returns>
        public async Task<string> UploadFileAsync(string filename, string content)
        {
            var data = Encoding.UTF8.GetBytes(content);
            return await this.UploadFileAsync(filename, data);
        }

        /// <summary>
        /// Uploads a file to the blob storage
        /// </summary>
        /// <param name="filename">Name of the file, this is a part of the uri, consider using a Guid for this.</param>
        /// <param name="content">The content of the file, a byte[]</param>
        /// <returns></returns>
        public async Task<string> UploadFileAsync(string filename, byte[] fileContent)
        {
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(filename);
            using (var stream = new MemoryStream(fileContent))
            {
                await blobClient.UploadAsync(stream, overwrite: true);
            }
            return blobClient.Uri.ToString();
        }
    }
}
