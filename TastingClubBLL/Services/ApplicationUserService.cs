using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TastingClubBLL.ViewModels.ApplicationUserViewModels;
using TastingClubDAL.Interfaces;
using TastingClubDAL.Models;
using TastingClubBLL.Exceptions;
using Microsoft.IdentityModel.Tokens;

namespace TastingClubBLL.Services
{
    public class ApplicationUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserService(IUnitOfWork unitOfWork,
            IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<List<ApplicationUserDetailViewModel>> GetApplicationUsersByNameAsync(string nameToSearch)
        {
            if(nameToSearch.IsNullOrEmpty())
            {
                throw new HttpStatusException(System.Net.HttpStatusCode.BadRequest, "Searched name can't be empty or null");
            }

            var serchedUsers = _userManager.Users.Where(u => string.Concat(u.FirstName, " ", u.LastName).Contains(nameToSearch)).ToList();
            var mappedUsers = _mapper.Map<List<ApplicationUserDetailViewModel>>(serchedUsers);
            return mappedUsers;
        }
    }
}
