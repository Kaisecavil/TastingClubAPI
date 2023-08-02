using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using TastingClubBLL.Interfaces.IHelper;
using TastingClubBLL.Interfaces.IServices;
using TastingClubDAL.Interfaces;
using TastingClubDAL.Interfaces.IModel;
using TastingClubDAL.Models;
using TastingClubDAL.Models.Base;
using TastingClubDAL.Repositories;

namespace TastingClubBLL.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileHelper _fileHelper;

        public PhotoService(IUnitOfWork unitOfWork,
            IFileHelper fileHelper)
        {
            _unitOfWork = unitOfWork;
            _fileHelper = fileHelper;
        }

        public async Task<int> CreatePhotoAsync(IFormFile file)
        {
            var filePath = await _fileHelper.SavePhotoAsync(file);
            var photo = new Photo { PhotoPath = filePath };
            await _unitOfWork.Photos.CreateAsync(photo);
            await _unitOfWork.SaveAsync();
            return photo.Id;
        }

        public async Task DeletePhotoAsync(int id)
        {
            await _unitOfWork.Photos.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeletePhotoRangeAsync(List<int> ids)
        {
            _unitOfWork.Photos.DeleteRange(ids);
            await _unitOfWork.SaveAsync();
        }
        
    }
}
