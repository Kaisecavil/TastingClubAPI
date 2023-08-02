using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TastingClubBLL.Interfaces.IHelper
{
    public interface IFileHelper
    {
        string GetExhibitionReportFilePath(string exhibitionName);
        Task<FileContentResult> GetFileContentResultAsync(string filePath, string contentType, bool deleteFileAfter = false);
        string GetUniqueFileName(string fileName);
        string GetUserReportFilePath(string userName);
        Task<string> SavePhotoAsync(IFormFile file);
        Task<string> SavePhotoAsync(IFormFile file, string directoryPath);
    }
}