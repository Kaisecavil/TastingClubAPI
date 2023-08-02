using Microsoft.AspNetCore.Http;
using TastingClubDAL.Interfaces.IModel;
using TastingClubDAL.Models.Base;
using TastingClubDAL.Repositories;

namespace TastingClubBLL.Interfaces.IServices
{
    public interface IPhotoService
    {
        Task<int> CreatePhotoAsync(IFormFile file);
        Task DeletePhotoAsync(int id);
        Task DeletePhotoRangeAsync(List<int> ids);
    }
}