using TastingClubDAL.Models;
using AutoMapper;
using TastingClubBLL.DTOs.DrinkDTOs;
using TastingClubBLL.ViewModels.DrinkViewModels;
using TastingClubBLL.ViewModels.EventViewModels;
using TastingClubBLL.DTOs.EventDTOs;
using TastingClubBLL.ViewModels.UserDrinkReviewViewModels;
using TastingClubBLL.DTOs.UserDrinkReviewDTOs;
using TastingClubBLL.ViewModels.ApplicationUserViewModels;
using TastingClubBLL.DTOs.ApplicationUserDTOs;
using TastingClubBLL.ViewModels.GroupViewModels;
using TastingClubBLL.DTOs.GroupDTOs;
using TastingClubBLL.ViewModels.DrinkTypeViewModels;
using TastingClubBLL.DTOs.DrinkTypeDTOs;
using TastingClubBLL.ViewModels.ProducingCountryViewModels;
using TastingClubBLL.DTOs.ProducingCountryDTOs;
using TastingClubBLL.DTOs.ProducerDTOs;
using TastingClubBLL.ViewModels.ProducerViewModels;
using TastingClubBLL.DTOs.DrinkBrandDTOs;
using TastingClubBLL.ViewModels.DrinkBrandViewModels;
using TastingClubBLL.ViewModels.DrinkSuitableProductViewModels;
using TastingClubBLL.ViewModels.PhotoViewModels;

namespace TastingClubBLL.Helpers
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles()
        {
            CreateMap<Drink, DrinkDetailViewModel>();
                //.ForMember(
                //    dest => dest,
                //    opt => opt.MapFrom(src => src.)
                //);
            CreateMap<Drink, DrinkGeneralViewModel>();
            CreateMap<DrinkDtoForCreate, Drink>();
            CreateMap<DrinkDtoForUpdate, Drink>();

            CreateMap<Group, GroupDetailViewModel>();
            CreateMap<Group, GroupGeneralViewModel>();
            CreateMap<GroupDtoForCreate, Group>();
            CreateMap<GroupDtoForUpdate, Group>();

            CreateMap<Event, EventGeneralViewModel>();
            CreateMap<Event, EventDetailViewModel>();
            CreateMap<EventDtoForUpdate, Event>();
            CreateMap<EventDtoForCreate, Event>();

            //CreateMap<EventDrink, EventDrinkDetailViewModel>();
            //CreateMap<EventDrinkDtoForCreate, EventDrink>();
            //CreateMap<EventDrinkDtoForUpdate, EventDrink>();


            CreateMap<UserDrinkReview, UserDrinkReviewDetailViewModel>();
            CreateMap<UserDrinkReview, UserDrinkReviewGeneralViewModel>();
            CreateMap<UserDrinkReviewDtoForCreate, UserDrinkReview>();
            CreateMap<UserDrinkReviewDtoForUpdate, UserDrinkReview>();


            CreateMap<ApplicationUser, ApplicationUserDetailViewModel>();
            CreateMap<ApplicationUser, ApplicationUserGeneralViewModel>();
            CreateMap<ApplicationUserDtoForUpdate, ApplicationUser>();
            CreateMap<ApplicationUserDtoForRegister, ApplicationUser>();

            CreateMap<DrinkType, DrinkTypeDetailViewModel>();
            CreateMap<DrinkType, DrinkTypeGeneralViewModel>();
            CreateMap<DrinkTypeDtoForUpdate, DrinkType>();
            CreateMap<DrinkTypeDtoForCreate, DrinkType>();


            CreateMap < ProducingCountry, ProducingCountryDetailViewModel > ();
            CreateMap < ProducingCountry, ProducingCountryGeneralViewModel > ();
            CreateMap < ProducingCountryDtoForUpdate, ProducingCountry> ();
            CreateMap < ProducingCountryDtoForCreate, ProducingCountry> ();


            //CreateMap < Producer, ProducerDetailViewModel> ();
            CreateMap < Producer, ProducerGeneralViewModel> ();
            CreateMap < ProducerDtoForUpdate, Producer> ();
            CreateMap < ProducerDtoForCreate, Producer> ();


            //CreateMap <DrinkBrand, DrinkBrandDetailViewModel> ();
            CreateMap <DrinkBrand, DrinkBrandGeneralViewModel> ();
            //CreateMap <DrinkBrandDtoForUpdate, DrinkBrand> ();
            CreateMap <DrinkBrandDtoForCreate, DrinkBrand> ();


            //CreateMap < SuitableProduct, SuitableProductDetailViewModel > ();
            CreateMap < SuitableProduct, SuitableProductGeneralViewModel > ();
            //CreateMap < SuitableProductDtoForUpdate, SuitableProduct> ();
            //CreateMap < SuitableProductDtoForCreate, SuitableProduct> ();

            CreateMap <DrinkPhoto, DrinkPhotoViewModel> ();

            //CreateMap < ~, ~DetailViewModel > ();
            //CreateMap < ~, ~GeneralViewModel > ();
            //CreateMap < ~DtoForUpdate, ~> ();
            //CreateMap < ~DtoForCreate, ~> ();


            //CreateMap < ~, ~DetailViewModel > ();
            //CreateMap < ~, ~GeneralViewModel > ();
            //CreateMap < ~DtoForUpdate, ~> ();
            //CreateMap < ~DtoForCreate, ~> ();


            //CreateMap < ~, ~DetailViewModel > ();
            //CreateMap < ~, ~GeneralViewModel > ();
            //CreateMap < ~DtoForUpdate, ~> ();
            //CreateMap < ~DtoForCreate, ~> ();



        }

        public static List<T> ConcatenateLists<T>(List<List<T>> listOfLists)
        {
            List<T> concatenatedList = new List<T>();

            foreach (List<T> innerList in listOfLists)
            {
                concatenatedList.AddRange(innerList);
            }

            return concatenatedList;
        }
    }
}
