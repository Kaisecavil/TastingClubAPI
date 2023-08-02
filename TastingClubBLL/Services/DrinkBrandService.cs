using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastingClubDAL.Interfaces;
using TastingClubDAL.Models;

namespace TastingClubBLL.Services
{
    public class DrinkBrandService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DrinkBrandService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateDrinkBrandAsync(DrinkBrand drinkBrand)
        {

        }
    }
}
