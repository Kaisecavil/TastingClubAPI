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
        public async Task SeedApplicationContextAsync()
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
                    DrinkTypeId = 1, // Replace with appropriate DrinkType ID
                    ProducingCountryId = 1, // Replace with appropriate ProducingCountry ID
                    ProducerId = 1, // Replace with appropriate Producer ID
                    DrinkBrandId = 1, // Replace with appropriate DrinkBrand ID
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
                    DrinkTypeId = 2, // Replace with appropriate DrinkType ID
                    ProducingCountryId = 2, // Replace with appropriate ProducingCountry ID
                    ProducerId = 2, // Replace with appropriate Producer ID
                    DrinkBrandId = 2, // Replace with appropriate DrinkBrand ID
                }
                // Add more sample drinks as needed
            };
            #endregion Drinks

            #region drinkSuitableProduct
            var drinkSuitableProducts = new List<DrinkSuitableProduct>
            {
                new DrinkSuitableProduct
                {
                    DrinkId= 1,
                    SuitableProductId= 1
                }
            };
            #endregion drinkSuitableProduct

            #region eventDrink
            var eventDrinks = new List<EventDrink>
            {
                new EventDrink
                {
                    DrinkId = 1,
                    EventId = 1
                }
            };
            #endregion eventDrink

            #region events
            var events = new List<Event>
            {
                new Event
                {
                    Date= new DateTime(),
                    Description = "",
                    GruopId = 1,
                    Title = "",
                    Status = Enums.EventStatus.Planned
                }
            };
            #endregion events

            #region eventPartisipants
            //?
            #endregion eventPartisipants

            #region userGroups
            //?
            #endregion userDrinkReviews
        }
    }
}
