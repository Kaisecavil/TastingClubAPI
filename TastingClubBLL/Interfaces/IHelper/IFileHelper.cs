using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TastingClubBLL.Interfaces.IHelper
{
    public interface IFileHelper
    {
        Task<FileContentResult> GetFileContentResultAsync(string filePath, string contentType, bool deleteFileAfter = false);
        string GetUniqueFileName(string fileName);
        Task<string> SavePhotoAsync(IFormFile file);
        Task<string> SavePhotoAsync(IFormFile file, string directoryPath);
    }
}