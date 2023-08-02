using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TastingClubBLL.DTOs.ProducingCountryDTOs;
using TastingClubBLL.Exceptions;
using TastingClubBLL.ViewModels.ProducingCountryViewModels;
using TastingClubDAL.Interfaces;

namespace TastingClubBLL.Services
{
    public class ProducingCountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProducingCountryService(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> CreateProducingCountryAsync(ProducingCountryDtoForCreate producingCountryDto)
        {
            var mappedProducingCountry = _mapper.Map<ProducingCountry>(producingCountryDto);
            await _unitOfWork.ProducingCountries.CreateAsync(mappedProducingCountry);
            await _unitOfWork.SaveAsync();
            return mappedProducingCountry.Id;
        }

        public async Task DeleteProducingCountryAsync(int id)
        {
            if (!await _unitOfWork.ProducingCountries.EntityExistsAsync(id))
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, "ProducingCountry not found");
            }
            await _unitOfWork.ProducingCountries.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<ProducingCountryGeneralViewModel>> GetAllProducingCountriesAsync()
        {
            var producingCountries = await _unitOfWork.ProducingCountries.GetAllAsync();
            return _mapper.Map<List<ProducingCountryGeneralViewModel>>(producingCountries);
        }

        public async Task<ProducingCountryDetailViewModel> GetProducingCountryAsync(int id)
        {
            var entity = await _unitOfWork.ProducingCountries.GetAsync(id);
            if (entity == null)
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, "ProducingCountry not found");
            }
            return _mapper.Map<ProducingCountryDetailViewModel>(entity);
        }
    }
}
