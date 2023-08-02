using TastingClubDAL.Database;
using TastingClubDAL.Interfaces;
using TastingClubDAL.Repositories;
using TastingClubDAL.Models;
using TastingClubDAL.Models.Base;

namespace TastingClubDAL.UnitOfWork
{

    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private ApplicationContext _db;
        private IBaseRepository<Drink> _drinkRepository;
        private IBaseRepository<DrinkPhoto> _drinkPhotoRepository;
        private IBaseRepository<UserDrinkReview> _userDrinkReviewRepository;
        private IBaseRepository<Event> _eventRepository;
        private IBaseRepository<EventDrink> _eventDrinkRepository;
        private IBaseRepository<Group> _groupRepository;
        private IBaseRepository<GroupPhoto> _groupPhotoRepository;
        private IBaseRepository<UserGroup> _userGroupRepository;
        private IBaseRepository<EventParticipant> _eventParticipantRepository;
        private IBaseRepository<DrinkSuitableProduct> _drinkSuitableProductRepository;
        private IBaseRepository<Photo> _photoRepository;
        private IBaseRepository<ProducingCountry> _producingCountryRepository;
        private IBaseRepository<DrinkBrand> _brinkRepository;

        public UnitOfWork(ApplicationContext db)
        {
            _db = db;
        }

        public IBaseRepository<Drink> Drinks
        {
            get
            {
                if (_drinkRepository == null)
                    _drinkRepository = new BaseRepository<Drink>(_db);
                return _drinkRepository;
            }
        }
        public IBaseRepository<Event> Events
        {
            get
            {
                if (_eventRepository == null)
                    _eventRepository = new BaseRepository<Event>(_db);
                return _eventRepository;
            }
        }
        public IBaseRepository<EventDrink> EventDrinks
        {
            get
            {
                if (_eventDrinkRepository == null)
                    _eventDrinkRepository = new BaseRepository<EventDrink>(_db);
                return _eventDrinkRepository;
            }
        }
        public IBaseRepository<UserDrinkReview> UserDrinkReviews
        {
            get
            {
                if (_userDrinkReviewRepository == null)
                    _userDrinkReviewRepository = new BaseRepository<UserDrinkReview>(_db);
                return _userDrinkReviewRepository;
            }
        }
        public IBaseRepository<DrinkPhoto> DrinkProducingCountries
        {
            get
            {
                if (_drinkPhotoRepository == null)
                    _drinkPhotoRepository = new BaseRepository<DrinkPhoto>(_db);
                return _drinkPhotoRepository;
            }
        }
        public IBaseRepository<GroupPhoto> GroupProducingCountries
        {
            get
            {
                if (_groupPhotoRepository == null)
                    _groupPhotoRepository = new BaseRepository<GroupPhoto>(_db);
                return _groupPhotoRepository;
            }
        }
        public IBaseRepository<Group> Groups
        {
            get
            {
                if (_groupRepository == null)
                    _groupRepository = new BaseRepository<Group>(_db);
                return _groupRepository;
            }
        }
        public IBaseRepository<UserGroup> UserGroups
        {
            get
            {
                if (_userGroupRepository == null)
                    _userGroupRepository = new BaseRepository<UserGroup>(_db);
                return _userGroupRepository;
            }
        }

        public IBaseRepository<EventParticipant> EventParticipants
        {
            get
            {
                if (_eventParticipantRepository == null)
                    _eventParticipantRepository = new BaseRepository<EventParticipant>(_db);
                return _eventParticipantRepository;
            }
        }

        public IBaseRepository<DrinkSuitableProduct> DrinkSuitableProducts
        {
            get
            {
                if (_drinkSuitableProductRepository == null)
                    _drinkSuitableProductRepository = new BaseRepository<DrinkSuitableProduct>(_db);
                return _drinkSuitableProductRepository;
            }
        }

        public IBaseRepository<Photo> ProducingCountries
        {
            get
            {
                if (_photoRepository == null)
                    _photoRepository = new BaseRepository<Photo>(_db);
                return _photoRepository;
            }
        }

        public IBaseRepository<ProducingCountry> ProducingCountries
        {
            get
            {
                if (_producingCountryRepository == null)
                    _producingCountryRepository = new BaseRepository<ProducingCountry>(_db);
                return _producingCountryRepository;
            }
        }

        public IBaseRepository<DrinkBrand> DrinkBrands
        {
            get
            {
                if (_drinkBrandRepository == null)
                    _drinkBrandRepository = new BaseRepository<DrinkBrand>(_db);
                return _drinkBrandRepository;
            }
        }

        public int Save()
        {
            return _db.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _db.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
