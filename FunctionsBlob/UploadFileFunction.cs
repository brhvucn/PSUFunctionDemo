using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace PSUFunctionsDemoProject.FunctionsBlob
{
    public class UploadFileFunction
    {
        private readonly ILogger<UploadFileFunction> _logger;
        private readonly IFileRepository _fileRepository;

        public UploadFileFunction(ILogger<UploadFileFunction> logger, IFileRepository fileRepository)
        {
            _logger = logger;
            _fileRepository = fileRepository;
        }

        [Function("UploadFileFunction")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            _logger.LogInformation("Uploading a file to blob storage");
            string uri = await this._fileRepository.UploadFileAsync("test.txt", "This is the content");
            string uri2 = await this._fileRepository.UploadFileAsync(Guid.NewGuid().ToString(), "This is the content with guid");
            return new OkObjectResult(new {uri1 = uri, uri2 = uri2});
        }
    }
}
