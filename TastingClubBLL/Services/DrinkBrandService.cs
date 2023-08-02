using TastingClubDAL.Interfaces;
using TastingClubDAL.Models;
using TastingClubBLL.DTOs.DrinkBrandDTOs;
using AutoMapper;
using TastingClubBLL.Exceptions;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using TastingClubBLL.ViewModels.DrinkBrandViewModels;

namespace TastingClubBLL.Services
{
    public class DrinkBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DrinkBrandService(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateDrinkBrandAsync(DrinkBrandDtoForCreate drinkBrand)
        {
            var mappedEntity = _mapper.Map<DrinkBrand>(drinkBrand);
            await _unitOfWork.DrinkBrands.CreateAsync(mappedEntity);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteDrinkBrandAsync(int id)
        {
            if(!await _unitOfWork.DrinkBrands.EntityExistsAsync(id))
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, "Drink brand not found");
            }
            await _unitOfWork.DrinkBrands.DeleteAsync(id);
            await _unitOfWork.SaveAsync();     

        }

        public async Task<List<DrinkBrandGeneralViewModel>> GetAllDrinkBrandsAsync()
        {
            var drinkBrands = await _unitOfWork.DrinkBrands.GetAllAsync(true);
            var mappedDrinkBrands = _mapper.Map<List<DrinkBrandGeneralViewModel>>(drinkBrands);
            return mappedDrinkBrands;
        }
    }
}
