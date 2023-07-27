using AutoMapper;
using System.Net;
using TastingClubBLL.DTOs.DrinkSuitableProductDTOs;
using TastingClubBLL.Exceptions;
using TastingClubBLL.ViewModels.DrinkSuitableProductViewModels;
using TastingClubBLL.ViewModels.DrinkViewModels;
using TastingClubDAL.Interfaces;
using TastingClubDAL.Models;

namespace TastingClubBLL.Services
{
    public class DrinkSuitableProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DrinkSuitableProductService(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> CreateDrinkSuitableProductsAsync(List<DrinkSuitableProductDtoForCreate> drinkSuitableProductsDtos)
        {
            var mappedDrinkSuitableProducts = _mapper.Map<List<DrinkSuitableProduct>>(drinkSuitableProductsDtos);
            await _unitOfWork.DrinkSuitableProducts.CreateRangeAsync(mappedDrinkSuitableProducts);
            await _unitOfWork.SaveAsync();
            return 1;
        }

        public async Task DeleteDrinkSuitableProductsAsync(List<int> ids)
        {
            if (ids.Any(async id => await !_unitOfWork.DrinkSuitableProducts.EntityExistsAsync(id)))
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, "DrinkSuitableProduct not found");
            }
            _unitOfWork.DrinkSuitableProducts.DeleteRange(ids);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<SuitableProductGeneralViewModel>> GetAllDrinkSuitableProductsAsync(int drinkId)
        {
            var suitableProducts = _unitOfWork.DrinkSuitableProducts.GetAllQueryable(true)
                .Where(drinkSuitableProduct => drinkSuitableProduct.DrinkId == drinkId)
                .Select(drinkSuitableProduct => drinkSuitableProduct.SuitableProduct);
            return _mapper.Map<List<SuitableProductGeneralViewModel>>(suitableProducts);
        }

        public async Task<List<DrinkGeneralViewModel>> GetAllSuitableProductDrinksAsync(int suitableProductId)
        {
            var drinks = _unitOfWork.DrinkSuitableProducts.GetAllQueryable(true)
                .Where(drinkSuitableProduct => drinkSuitableProduct.SuitableProductId == suitableProductId)
                .Select(drinkSuitableProduct => drinkSuitableProduct.Drink);
            return _mapper.Map<List<DrinkGeneralViewModel>>(drinks);
        }
    }
}
