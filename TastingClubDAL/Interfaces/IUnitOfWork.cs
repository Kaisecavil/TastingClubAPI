using TastingClubDAL.Models;

namespace TastingClubDAL.Interfaces
{
    public interface IUnitOfWork
    {
        IBaseRepository<DrinkPhoto> DrinkPhotos { get; }
        IBaseRepository<Drink> Drinks { get; }
        IBaseRepository<EventDrink> EventDrinks { get; }
        IBaseRepository<EventParticipant> EventParticipants { get; }
        IBaseRepository<Event> Events { get; }
        IBaseRepository<GroupPhoto> GroupPhotos { get; }
        IBaseRepository<Group> Groups { get; }
        IBaseRepository<UserDrinkReview> UserDrinkReviews { get; }
        IBaseRepository<UserGroup> UserGroups { get; }

        void Dispose();
        void Dispose(bool disposing);
        int Save();
        Task<int> SaveAsync();
    }
}
