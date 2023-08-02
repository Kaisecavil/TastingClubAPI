using TastingClubDAL.Models;
using TastingClubDAL.Models.Base;

namespace TastingClubDAL.Interfaces
{
    public interface IUnitOfWork
    {
        IBaseRepository<DrinkPhoto> DrinkPhotos { get; }
        IBaseRepository<Drink> Drinks { get; }
        IBaseRepository<DrinkBrand> DrinkBrands { get; }
        IBaseRepository<Producer> Producers { get; }
        IBaseRepository<ProducingCountry> ProducingCountries { get; }
        IBaseRepository<EventDrink> EventDrinks { get; }
        IBaseRepository<EventParticipant> EventParticipants { get; }
        IBaseRepository<Event> Events { get; }
        IBaseRepository<GroupPhoto> GroupPhotos { get; }
        IBaseRepository<Group> Groups { get; }
        IBaseRepository<UserDrinkReview> UserDrinkReviews { get; }
        IBaseRepository<UserGroup> UserGroups { get; }
        IBaseRepository<DrinkSuitableProduct> DrinkSuitableProducts { get; }
        IBaseRepository<SuitableProduct> SuitableProducts { get; }
        IBaseRepository<DrinkType> DrinkTypes { get; }
        IBaseRepository<Photo> Photos { get; }

        void Dispose();
        void Dispose(bool disposing);
        int Save();
        Task<int> SaveAsync();
    }
}
