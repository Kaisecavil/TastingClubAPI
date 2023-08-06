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

namespace TastingClubBLL.Helpers
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles()
        {
            CreateMap<Drink, DrinkDetailViewModel>();
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
