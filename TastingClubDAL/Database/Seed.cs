using TastingClubDAL.Enums;
using TastingClubDAL.Interfaces;
using TastingClubDAL.Models;

namespace TastingClubDAL.Database
{
    public class Seed
    {
        private readonly IUnitOfWork _unitOfWork;
        public Seed(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task SeedApplicationContextAsync(List<string> userIds)
        {
            #region Groups
            var groups = new List<Group>()
            {
                new Group
                {
                    Title = "WineLovers",
                    Description = "If you love wine, you are welcome",
                    AccessType = Enums.GroupAccessType.Public
                },
                new Group
                {
                    Title = "BeerZavrs",
                    Description = "If you love beer, you are welcome",
                    AccessType = Enums.GroupAccessType.Private
                },

            };
            await _unitOfWork.Groups.CreateRangeAsync(groups);
            #endregion Groups

            #region suitableProducts
            var suitableProducts = new List<SuitableProduct>
            { 
                new SuitableProduct
                {
                    Title = "Fish"
                },
                new SuitableProduct
                {
                    Title = "White meat"
                },
                new SuitableProduct
                {
                    Title = "Snacks"
                },
                new SuitableProduct
                {
                    Title = "Beef"
                },
                new SuitableProduct
                {
                    Title = "Bread"
                },
                new SuitableProduct
                {
                    Title = "Berries"
                },
                new SuitableProduct
                {
                    Title = "Cheese"
                },
                new SuitableProduct
                {
                    Title = "Coffee"
                },
                new SuitableProduct
                {
                    Title = "Cookie"
                },
                new SuitableProduct
                {
                    Title = "Dessert"
                },
                new SuitableProduct
                {
                    Title = "Lamb"
                }
            };
            await _unitOfWork.SuitableProducts.CreateRangeAsync(suitableProducts);
            #endregion suitableProducts

            #region drinkTypes
            var drinkTypes = new List<DrinkType>
            {
                new DrinkType
                {
                    Title = "Whiskey"
                },
                new DrinkType
                {
                    Title = "Congac"
                },
                new DrinkType
                {
                    Title = "Beer"
                },
                new DrinkType
                {
                    Title = "Champagne"
                },
                new DrinkType
                {
                    Title = "Wine"
                },
                new DrinkType
                {
                    Title = "Liqueur"
                },
                new DrinkType
                {
                    Title = "Vodka"
                }
            };
            await _unitOfWork.DrinkTypes.CreateRangeAsync(drinkTypes);
            #endregion drinkTypes

            #region producingCounties
            var producingCountries = new List<ProducingCountry>
            {
                new ProducingCountry
                {
                    Name = "Belarus"
                },
                new ProducingCountry
                {
                    Name = "Russia"
                },
                new ProducingCountry
                {
                    Name = "Czech"
                },
                new ProducingCountry
                {
                    Name = "France"
                },
                new ProducingCountry
                {
                    Name = "Germany"
                },
                new ProducingCountry
                {
                    Name = "England"
                },
                new ProducingCountry
                {
                    Name = "Italy"
                },
                new ProducingCountry
                {
                    Name = "Spain"
                }
            };
            await _unitOfWork.ProducingCountries.CreateRangeAsync(producingCountries);
            #endregion producingCounties

            #region producers
            var producers = new List<Producer>
            {
                new Producer
                {
                    Name = "Asti-mondoro"
                },
                new Producer
                {
                    Name = "Gancia"
                },
                new Producer
                {
                    Name = "Laurent Perrier"
                },
                new Producer
                {
                    Name = "Ruinart"
                },
                new Producer
                {
                    Name = "Deutz"
                },
                new Producer
                {
                    Name = "Lanson"
                },
                new Producer
                {
                    Name = "Moet Chandon"
                },
                new Producer
                {
                    Name = "Veuve Clicquot"
                }
            };
            await _unitOfWork.Producers.CreateRangeAsync(producers);
            #endregion producers

            #region drinkBrands
            var drinkBrands = new List<DrinkBrand>
            {
                new DrinkBrand
                {
                    Name = "Abrau"
                },
                new DrinkBrand
                {
                    Name = "Cinzano"
                },
                new DrinkBrand
                {
                    Name = "Cristal"
                },
                new DrinkBrand
                {
                    Name = "Martini"
                },
                new DrinkBrand
                {
                    Name = "Asti Martini"
                },
                new DrinkBrand
                {
                    Name = "Codorniu"
                },
                new DrinkBrand
                {
                    Name = "Dom Perignon"
                },
                new DrinkBrand
                {
                    Name = "Mondoro"
                }
            };
            await _unitOfWork.DrinkBrands.CreateRangeAsync(drinkBrands);
            #endregion drinkBrands


            #region Drinks
            var drinks = new List<Drink>
            {
                new Drink
                {
                    Title = "Sample Drink 1",
                    Description = "This is a sample drink.",
                    Price = 5.99m,
                    AlcoholPercentage = 5.0f,
                    Rating = 7.5f,
                    Color = "Red",
                    Taste = "Sweet",
                    Aroma = "Fruity",
                    Gastronomy = "Pairs well with desserts.",
                    DrinkTypeId = drinkTypes[0].Id,
                    ProducingCountryId = producingCountries[0].Id, 
                    ProducerId = producers[0].Id, 
                    DrinkBrandId = drinkBrands[0].Id, 
                },
                new Drink
                {
                    Title = "Sample Drink 2",
                    Description = "Another sample drink.",
                    Price = 9.99m,
                    AlcoholPercentage = 7.5f,
                    Rating = 8.2f,
                    Color = "Gold",
                    Taste = "Bitter",
                    Aroma = "Hoppy",
                    Gastronomy = "Goes well with spicy food.",
                    DrinkTypeId = drinkTypes[1].Id,
                    ProducingCountryId = producingCountries[1].Id,
                    ProducerId = producers[1].Id,
                    DrinkBrandId = drinkBrands[1].Id,
                },
                new Drink
                {
                    Title = "Sample Drink 3",
                    Description = "Another sample drink. 3",
                    Price = 9.99m,
                    AlcoholPercentage = 7.5f,
                    Rating = 8.2f,
                    Color = "Gold",
                    Taste = "Bitter",
                    Aroma = "Hoppy",
                    Gastronomy = "Goes well with sour food.",
                    DrinkTypeId = drinkTypes[2].Id,
                    ProducingCountryId = producingCountries[2].Id,
                    ProducerId = producers[2].Id,
                    DrinkBrandId = drinkBrands[2].Id,
                }
                // Add more sample drinks as needed
            };
            await _unitOfWork.Drinks.CreateRangeAsync(drinks);
            #endregion Drinks

            #region drinkSuitableProduct
            var drinkSuitableProducts = new List<DrinkSuitableProduct>
            {
                new DrinkSuitableProduct
                {
                    DrinkId= drinks[0].Id,
                    SuitableProductId= suitableProducts[0].Id
                },
                new DrinkSuitableProduct
                {
                    DrinkId= drinks[0].Id,
                    SuitableProductId= suitableProducts[1].Id
                },
                new DrinkSuitableProduct
                {
                    DrinkId= drinks[0].Id,
                    SuitableProductId= suitableProducts[2].Id
                },
                new DrinkSuitableProduct
                {
                    DrinkId= drinks[1].Id,
                    SuitableProductId= suitableProducts[0].Id
                },
                new DrinkSuitableProduct
                {
                    DrinkId= drinks[1].Id,
                    SuitableProductId= suitableProducts[1].Id
                }
            };
            await _unitOfWork.DrinkSuitableProducts.CreateRangeAsync(drinkSuitableProducts);
            #endregion drinkSuitableProduct

            #region events
            var events = new List<Event>
            {
                new Event
                {
                    Date = DateTime.Now,
                    Description = "The best event ever 1",
                    GroupId = groups[0].Id,
                    Title = "YaggerMeister Party",
                    Status = Enums.EventStatus.Planned
                },
                new Event
                {
                    Date = DateTime.Now,
                    Description = "The best event ever 2",
                    GroupId = groups[0].Id,
                    Title = "Lidsokoe funs party",
                    Status = Enums.EventStatus.Canceled
                },
                new Event
                {
                    Date = DateTime.Now,
                    Description = "The best event ever 3",
                    GroupId = groups[0].Id,
                    Title = "Garage Enjoyers",
                    Status = Enums.EventStatus.InReview
                }
            };
            await _unitOfWork.Events.CreateRangeAsync(events);
            #endregion events

            #region eventDrink
            var eventDrinks = new List<EventDrink>
            {
                new EventDrink
                {
                    DrinkId = drinks[0].Id,
                    EventId = events[0].Id
                },
                new EventDrink
                {
                    DrinkId = drinks[0].Id,
                    EventId = events[1].Id
                },
                new EventDrink
                {
                    DrinkId = drinks[0].Id,
                    EventId = events[2].Id
                },
                new EventDrink
                {
                    DrinkId = drinks[1].Id,
                    EventId = events[0].Id
                },
                new EventDrink
                {
                    DrinkId = drinks[1].Id,
                    EventId = events[1].Id
                }
            };
            await _unitOfWork.EventDrinks.CreateRangeAsync(eventDrinks);
            #endregion eventDrink

            #region eventPartisipants
            var eventParticipants = new List<EventParticipant>
            {
                new EventParticipant()
                {
                    UserId = userIds[0],
                    EventId = events[0].Id,
                    Status = EventPartisipantStatus.Approved
                },
                new EventParticipant()
                {
                    UserId = userIds[0],
                    EventId = events[1].Id,
                    Status = EventPartisipantStatus.NotResponded
                },
                new EventParticipant()
                {
                    UserId = userIds[1],
                    EventId = events[0].Id,
                    Status = EventPartisipantStatus.Declined
                },
                new EventParticipant()
                {
                    UserId = userIds[1],
                    EventId = events[1].Id,
                    Status = EventPartisipantStatus.Approved
                },
            };
            await _unitOfWork.EventParticipants.CreateRangeAsync(eventParticipants);
            #endregion eventPartisipants

            #region userGroups
            var userGroups = new List<UserGroup>
            {
                new UserGroup()
                {
                    GroupId = groups[0].Id,
                    UserId = userIds[0],
                    Role = UserGroupRole.Admin,
                    Status = GroupMembershipStatus.Member
                },
                new UserGroup()
                {
                    GroupId = groups[1].Id,
                    UserId = userIds[1],
                    Role = UserGroupRole.Admin,
                    Status = GroupMembershipStatus.Member
                },
                new UserGroup()
                {
                    GroupId = groups[0].Id,
                    UserId = userIds[1],
                    Role = UserGroupRole.Partisipant,
                    Status = GroupMembershipStatus.Member
                }   
            };
            await _unitOfWork.UserGroups.CreateRangeAsync(userGroups);
            #endregion userDrinkReviews

            await _unitOfWork.SaveAsync();
        }
    }
}
