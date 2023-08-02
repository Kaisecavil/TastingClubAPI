using Microsoft.AspNetCore.Http;

namespace TastingClubBLL.Interfaces.IServices
{
    public interface IPhotoService
    {
        Task<int> CreatePhotoAsync(IFormFile file);
        Task DeletePhotoAsync(int id);
        Task DeletePhotoRangeAsync(List<int> ids);
    }
}