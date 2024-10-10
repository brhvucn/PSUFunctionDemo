using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace PSUFunctionsDemoProject.FunctionsBlob
{
    public class DownloadFileFunction
    {
        private readonly ILogger<DownloadFileFunction> _logger;
        private readonly IFileRepository _fileRepository;

        public DownloadFileFunction(ILogger<DownloadFileFunction> logger, IFileRepository fileRepository)
        {
            _logger = logger;
            _fileRepository = fileRepository;
        }

        [Function("DownloadFileFunction")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            _logger.LogInformation("Download a file. This should normally be done directly in a browser for the client.");
            var downloadedFile = await _fileRepository.DownloadFile("test.txt");
            return new OkObjectResult(new {file = downloadedFile});
        }
    }
}
