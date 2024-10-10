using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSUFunctionsDemoProject.FunctionsBlob
{
    public interface IFileRepository
    {
        Task<string> UploadFileAsync(string fileName, byte[] fileContent);
        Task<string> UploadFileAsync(string fileName, string fileContent);
        Task<string> DownloadFile(string fileName);
    }
}



