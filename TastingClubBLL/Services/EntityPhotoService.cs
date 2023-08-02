using Microsoft.AspNetCore.Http;
using TastingClubBLL.Interfaces.IServices;
using TastingClubDAL.Interfaces;
using TastingClubDAL.Interfaces.IModel;
using TastingClubDAL.Models;
using TastingClubDAL.Models.Base;
using TastingClubDAL.Repositories;

namespace TastingClubBLL.Services
{
    public class EntityPhotoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPhotoService _photoService;

        public EntityPhotoService(IUnitOfWork unitOfWork,
            IPhotoService photoService)
        {
            _unitOfWork = unitOfWork;
            _photoService = photoService;
        }

        public async Task CreateEntityPhoto<RepositoryType>(IFormFile photo, int entityId, BaseRepository<RepositoryType> repository) where RepositoryType : BaseModel, IPhotoModel
        {
            var photoId = await _photoService.CreatePhotoAsync(photo);
            if (repository is BaseRepository<DrinkPhoto>)
            {
                await _unitOfWork.DrinkPhotos.CreateAsync(
                    new DrinkPhoto()
                    {
                        DrinkId = entityId,
                        PhotoId = photoId
                    }
                );
            }

            if (repository is BaseRepository<GroupPhoto>)
            {
                await _unitOfWork.GroupPhotos.CreateAsync(
                    new GroupPhoto()
                    {
                        GroupId = entityId,
                        PhotoId = photoId
                    }
                );
            }
        }

        public async Task DeleteEntityPhoto<RepositoryType>(int id, BaseRepository<RepositoryType> repository) where RepositoryType : BaseModel, IPhotoModel
        {
            var photoIds = repository.GetAllQueryable().Where(p => p.EntityId== id).Select(p => p.Id).ToList();
            await _photoService.DeletePhotoRangeAsync(photoIds);
            await repository.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }

    }
}
