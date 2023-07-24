using AutoMapper;
using System.Net;
using TastingClubBLL.DTOs.UserDrinkReviewDTOs;
using TastingClubBLL.Exceptions;
using TastingClubBLL.Interfaces.IServices;
using TastingClubBLL.ViewModels.UserDrinkReviewViewModels;
using TastingClubDAL.Interfaces;
using TastingClubDAL.Models;

namespace TastingClubBLL.Services
{
    public class UserDrinkReviewService : IUserDrinkReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserDrinkReviewService(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> CreateUserDrinkReviewAsync(UserDrinkReviewDtoForCreate userDrinkReviewDto)
        {
            var mappedUserDrinkReview = _mapper.Map<UserDrinkReview>(userDrinkReviewDto);
            await _unitOfWork.UserDrinkReviews.CreateAsync(mappedUserDrinkReview);
            await _unitOfWork.SaveAsync();
            return mappedUserDrinkReview.Id;
        }

        public async Task DeleteUserDrinkReviewAsync(int id)
        {
            if (!await _unitOfWork.UserDrinkReviews.EntityExistsAsync(id))
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, "UserDrinkReview not found");
            }
            await _unitOfWork.UserDrinkReviews.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<UserDrinkReviewGeneralViewModel>> GetAllUserDrinkReviewsAsync()
        {
            var userDrinkReviews = await _unitOfWork.UserDrinkReviews.GetAllAsync();
            return _mapper.Map<List<UserDrinkReviewGeneralViewModel>>(userDrinkReviews);
        }

        public async Task<UserDrinkReviewDetailViewModel> GetUserDrinkReviewAsync(int id)
        {
            var entity = await _unitOfWork.UserDrinkReviews.GetAsync(id);
            if (entity == null)
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, "UserDrinkReview not found");
            }
            return _mapper.Map<UserDrinkReviewDetailViewModel>(entity);
        }

        public async Task UpdateUserDrinkReviewAsync(UserDrinkReviewDtoForUpdate userDrinkReviewDto)
        {
            if (!await _unitOfWork.UserDrinkReviews.EntityExistsAsync(userDrinkReviewDto.Id))
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, "UserDrinkReview not found");
            }
            await _unitOfWork.UserDrinkReviews.UpdateAsync(_mapper.Map<UserDrinkReview>(userDrinkReviewDto));
            await _unitOfWork.SaveAsync();
        }
    }
}
