
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using TastingClubDAL.Database;
using TastingClubDAL.Interfaces;
using TastingClubDAL.Models;
using TastingClubPL.Swashbuckle;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TastingClubDAL.UnitOfWork;
using TastingClubBLL.Interfaces.IServices;
using TastingClubBLL.Services;
using AutoMapper;
using TastingClubBLL.Helpers;
using TastingClubBLL.DTOs.ApplicationUserDTOs;
using TastingClubBLL.Constants;
using TastingClubDAL.Constants.ModelConstants.ApplicationUserConstants;
using TastingClubBLL.Interfaces.IProvider;
using TastingClubDAL.Providers;
using TastingClubDAL.Enums;

namespace TastingClubPL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.

            builder.Services.AddDbContext<ApplicationContext>(
                o =>
                {
                    o.UseLazyLoadingProxies()
                        .UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
                });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

            //builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
            
            //builder.Services.Configure<RequestLocalizationOptions>(options =>
            //{
            //    var supportedCultures = new List<CultureInfo>
            //    {
            //        new CultureInfo("en-US"),
            //        new CultureInfo("ru-RU")
            //    };

            //    options.DefaultRequestCulture = new RequestCulture("en-US");
            //    options.SupportedCultures = supportedCultures;
            //    options.SupportedUICultures = supportedCultures;
            //});

            //builder.Services.AddSingleton<IStringLocalizerFactory, ResourceManagerStringLocalizerFactory>();
            //builder.Services.AddSingleton<IStringLocalizer>(provider =>
            //    provider.GetService<IStringLocalizerFactory>().Create("SharedResources", "MyWebApp"));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IDrinkService, DrinkService>();
            builder.Services.AddScoped<IDrinkSuitableProductService, DrinkSuitableProductService>();
            builder.Services.AddScoped<IEventDrinkService, EventDrinkService>();
            builder.Services.AddScoped<IEventParticipantService, EventParticipantService>();
            builder.Services.AddScoped<IEventService, EventService>();
            builder.Services.AddScoped<IGroupService, GroupService>();
            builder.Services.AddScoped<IUserDrinkReviewService, UserDrinkReviewService>();
            builder.Services.AddScoped<IUserGroupService, UserGroupService>();
            builder.Services.AddScoped<IUserGroupService, UserGroupService>();
            builder.Services.AddScoped<IApplicationUserProvider, ApplicationUserProvider>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateActor = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
                    ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value))
                };
            }
            );

            builder.Services.AddTransient<IAuthService, AuthService>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfiles());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            builder.Services.AddControllers();

            builder.Services.AddTransient<Seed>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option =>
            {
                // ...
                option.SchemaFilter<SchemaFilter>();
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
            });

            var app = builder.Build();

            if (args.Length == 1 && args[0].ToLower() == "seeddata")
                SeedData(app);

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }

        private static async void SeedData(IHost app)
        {
            var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

            using (var scope = scopedFactory.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roles = new[] {
                        RoleConstants.AdminRole,
                        RoleConstants.UserRole
                    };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                var adminUser = new ApplicationUserDtoForRegister()
                {
                    Email = ApplicationUserDefaultValueConstants.DefaultAdminEmail,
                    Password = ApplicationUserDefaultValueConstants.DefaultAdminPassword,
                    ConfirmedPassword = ApplicationUserDefaultValueConstants.DefaultAdminPassword,
                    FirstName = "FirstNametest", LastName = "lastNameTest"
                  
                };
                var userUser = new ApplicationUserDtoForRegister() 
                {
                    Email = ApplicationUserDefaultValueConstants.DefaultUserEmail,
                    Password = ApplicationUserDefaultValueConstants.DefaultUserPassword,
                    ConfirmedPassword = ApplicationUserDefaultValueConstants.DefaultUserPassword,
                    FirstName = "FirstNametest",
                    LastName = "lastNameTest"
                };
                try
                {
                    await authService.RegisterUserAsync(adminUser);
                    await authService.RegisterUserAsync(userUser);
                }
                catch(DbUpdateException ex)
                {
                    
                }


                await userManager.AddToRoleAsync(userManager.FindByEmailAsync(adminUser.Email).Result, RoleConstants.AdminRole);
                await userManager.AddToRoleAsync(userManager.FindByEmailAsync(userUser.Email).Result, RoleConstants.UserRole);

                var userIds = userManager.Users.Select(u => u.Id).ToList();
                //var service = scope.ServiceProvider.GetService<Seed>();
                //await service.SeedApplicationContextAsync(userIds);

                #region Groups
                var groups = new List<Group>()
                    {
                        new Group
                        {
                            Title = "WineLovers",
                            Description = "If you love wine, you are welcome",
                            AccessType = GroupAccessType.Public
                        },
                        new Group
                        {
                            Title = "BeerZavrs",
                            Description = "If you love beer, you are welcome",
                            AccessType = GroupAccessType.Private
                        },

                    };
                await unitOfWork.Groups.CreateRangeAsync(groups);
                await unitOfWork.SaveAsync();
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
                await unitOfWork.SuitableProducts.CreateRangeAsync(suitableProducts);
                await unitOfWork.SaveAsync();
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
                await unitOfWork.DrinkTypes.CreateRangeAsync(drinkTypes);
                await unitOfWork.SaveAsync();
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
                await unitOfWork.ProducingCountries.CreateRangeAsync(producingCountries);
                await unitOfWork.SaveAsync();
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
                await unitOfWork.Producers.CreateRangeAsync(producers);
                await unitOfWork.SaveAsync();
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
                await unitOfWork.DrinkBrands.CreateRangeAsync(drinkBrands);
                await unitOfWork.SaveAsync();
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
                await unitOfWork.Drinks.CreateRangeAsync(drinks);
                await unitOfWork.SaveAsync();
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
                await unitOfWork.DrinkSuitableProducts.CreateRangeAsync(drinkSuitableProducts);
                await unitOfWork.SaveAsync();
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
                            Status = EventStatus.Planned
                        },
                        new Event
                        {
                            Date = DateTime.Now,
                            Description = "The best event ever 2",
                            GroupId = groups[0].Id,
                            Title = "Lidsokoe funs party",
                            Status = EventStatus.Canceled
                        },
                        new Event
                        {
                            Date = DateTime.Now,
                            Description = "The best event ever 3",
                            GroupId = groups[1].Id,
                            Title = "Garage Enjoyers",
                            Status = EventStatus.InReview
                        }
                    };
                await unitOfWork.Events.CreateRangeAsync(events);
                await unitOfWork.SaveAsync();
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
                await unitOfWork.EventDrinks.CreateRangeAsync(eventDrinks);
                await unitOfWork.SaveAsync();
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
                await unitOfWork.EventParticipants.CreateRangeAsync(eventParticipants);
                await unitOfWork.SaveAsync();
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
                await unitOfWork.UserGroups.CreateRangeAsync(userGroups);
                await unitOfWork.SaveAsync();
                #endregion userDrinkReviews

                await unitOfWork.SaveAsync();
            }
        }

        //private async Task SeedApplicationContextAsync(List<string> userIds)
        //{
        //    #region Groups
        //    var groups = new List<Group>()
        //    {
        //        new Group
        //        {
        //            Title = "WineLovers",
        //            Description = "If you love wine, you are welcome",
        //            AccessType = Enums.GroupAccessType.Public
        //        },
        //        new Group
        //        {
        //            Title = "BeerZavrs",
        //            Description = "If you love beer, you are welcome",
        //            AccessType = Enums.GroupAccessType.Private
        //        },

        //    };
        //    await _unitOfWork.Groups.CreateRangeAsync(groups);
        //    #endregion Groups

        //    #region suitableProducts
        //    var suitableProducts = new List<SuitableProduct>
        //    {
        //        new SuitableProduct
        //        {
        //            Title = "Fish"
        //        },
        //        new SuitableProduct
        //        {
        //            Title = "White meat"
        //        },
        //        new SuitableProduct
        //        {
        //            Title = "Snacks"
        //        },
        //        new SuitableProduct
        //        {
        //            Title = "Beef"
        //        },
        //        new SuitableProduct
        //        {
        //            Title = "Bread"
        //        },
        //        new SuitableProduct
        //        {
        //            Title = "Berries"
        //        },
        //        new SuitableProduct
        //        {
        //            Title = "Cheese"
        //        },
        //        new SuitableProduct
        //        {
        //            Title = "Coffee"
        //        },
        //        new SuitableProduct
        //        {
        //            Title = "Cookie"
        //        },
        //        new SuitableProduct
        //        {
        //            Title = "Dessert"
        //        },
        //        new SuitableProduct
        //        {
        //            Title = "Lamb"
        //        }
        //    };
        //    await _unitOfWork.SuitableProducts.CreateRangeAsync(suitableProducts);
        //    #endregion suitableProducts

        //    #region drinkTypes
        //    var drinkTypes = new List<DrinkType>
        //    {
        //        new DrinkType
        //        {
        //            Title = "Whiskey"
        //        },
        //        new DrinkType
        //        {
        //            Title = "Congac"
        //        },
        //        new DrinkType
        //        {
        //            Title = "Beer"
        //        },
        //        new DrinkType
        //        {
        //            Title = "Champagne"
        //        },
        //        new DrinkType
        //        {
        //            Title = "Wine"
        //        },
        //        new DrinkType
        //        {
        //            Title = "Liqueur"
        //        },
        //        new DrinkType
        //        {
        //            Title = "Vodka"
        //        }
        //    };
        //    await _unitOfWork.DrinkTypes.CreateRangeAsync(drinkTypes);
        //    #endregion drinkTypes

        //    #region producingCounties
        //    var producingCountries = new List<ProducingCountry>
        //    {
        //        new ProducingCountry
        //        {
        //            Name = "Belarus"
        //        },
        //        new ProducingCountry
        //        {
        //            Name = "Russia"
        //        },
        //        new ProducingCountry
        //        {
        //            Name = "Czech"
        //        },
        //        new ProducingCountry
        //        {
        //            Name = "France"
        //        },
        //        new ProducingCountry
        //        {
        //            Name = "Germany"
        //        },
        //        new ProducingCountry
        //        {
        //            Name = "England"
        //        },
        //        new ProducingCountry
        //        {
        //            Name = "Italy"
        //        },
        //        new ProducingCountry
        //        {
        //            Name = "Spain"
        //        }
        //    };
        //    await _unitOfWork.ProducingCountries.CreateRangeAsync(producingCountries);
        //    #endregion producingCounties

        //    #region producers
        //    var producers = new List<Producer>
        //    {
        //        new Producer
        //        {
        //            Name = "Asti-mondoro"
        //        },
        //        new Producer
        //        {
        //            Name = "Gancia"
        //        },
        //        new Producer
        //        {
        //            Name = "Laurent Perrier"
        //        },
        //        new Producer
        //        {
        //            Name = "Ruinart"
        //        },
        //        new Producer
        //        {
        //            Name = "Deutz"
        //        },
        //        new Producer
        //        {
        //            Name = "Lanson"
        //        },
        //        new Producer
        //        {
        //            Name = "Moet Chandon"
        //        },
        //        new Producer
        //        {
        //            Name = "Veuve Clicquot"
        //        }
        //    };
        //    await _unitOfWork.Producers.CreateRangeAsync(producers);
        //    #endregion producers

        //    #region drinkBrands
        //    var drinkBrands = new List<DrinkBrand>
        //    {
        //        new DrinkBrand
        //        {
        //            Name = "Abrau"
        //        },
        //        new DrinkBrand
        //        {
        //            Name = "Cinzano"
        //        },
        //        new DrinkBrand
        //        {
        //            Name = "Cristal"
        //        },
        //        new DrinkBrand
        //        {
        //            Name = "Martini"
        //        },
        //        new DrinkBrand
        //        {
        //            Name = "Asti Martini"
        //        },
        //        new DrinkBrand
        //        {
        //            Name = "Codorniu"
        //        },
        //        new DrinkBrand
        //        {
        //            Name = "Dom Perignon"
        //        },
        //        new DrinkBrand
        //        {
        //            Name = "Mondoro"
        //        }
        //    };
        //    await _unitOfWork.DrinkBrands.CreateRangeAsync(drinkBrands);
        //    #endregion drinkBrands

        //    #region Drinks
        //    var drinks = new List<Drink>
        //    {
        //        new Drink
        //        {
        //            Title = "Sample Drink 1",
        //            Description = "This is a sample drink.",
        //            Price = 5.99m,
        //            AlcoholPercentage = 5.0f,
        //            Rating = 7.5f,
        //            Color = "Red",
        //            Taste = "Sweet",
        //            Aroma = "Fruity",
        //            Gastronomy = "Pairs well with desserts.",
        //            DrinkTypeId = drinkTypes[0].Id,
        //            ProducingCountryId = producingCountries[0].Id,
        //            ProducerId = producers[0].Id,
        //            DrinkBrandId = drinkBrands[0].Id,
        //        },
        //        new Drink
        //        {
        //            Title = "Sample Drink 2",
        //            Description = "Another sample drink.",
        //            Price = 9.99m,
        //            AlcoholPercentage = 7.5f,
        //            Rating = 8.2f,
        //            Color = "Gold",
        //            Taste = "Bitter",
        //            Aroma = "Hoppy",
        //            Gastronomy = "Goes well with spicy food.",
        //            DrinkTypeId = drinkTypes[1].Id,
        //            ProducingCountryId = producingCountries[1].Id,
        //            ProducerId = producers[1].Id,
        //            DrinkBrandId = drinkBrands[1].Id,
        //        },
        //        new Drink
        //        {
        //            Title = "Sample Drink 3",
        //            Description = "Another sample drink. 3",
        //            Price = 9.99m,
        //            AlcoholPercentage = 7.5f,
        //            Rating = 8.2f,
        //            Color = "Gold",
        //            Taste = "Bitter",
        //            Aroma = "Hoppy",
        //            Gastronomy = "Goes well with sour food.",
        //            DrinkTypeId = drinkTypes[2].Id,
        //            ProducingCountryId = producingCountries[2].Id,
        //            ProducerId = producers[2].Id,
        //            DrinkBrandId = drinkBrands[2].Id,
        //        }
        //        // Add more sample drinks as needed
        //    };
        //    await _unitOfWork.Drinks.CreateRangeAsync(drinks);
        //    #endregion Drinks

        //    #region drinkSuitableProduct
        //    var drinkSuitableProducts = new List<DrinkSuitableProduct>
        //    {
        //        new DrinkSuitableProduct
        //        {
        //            DrinkId= drinks[0].Id,
        //            SuitableProductId= suitableProducts[0].Id
        //        },
        //        new DrinkSuitableProduct
        //        {
        //            DrinkId= drinks[0].Id,
        //            SuitableProductId= suitableProducts[1].Id
        //        },
        //        new DrinkSuitableProduct
        //        {
        //            DrinkId= drinks[0].Id,
        //            SuitableProductId= suitableProducts[2].Id
        //        },
        //        new DrinkSuitableProduct
        //        {
        //            DrinkId= drinks[1].Id,
        //            SuitableProductId= suitableProducts[0].Id
        //        },
        //        new DrinkSuitableProduct
        //        {
        //            DrinkId= drinks[1].Id,
        //            SuitableProductId= suitableProducts[1].Id
        //        }
        //    };
        //    await _unitOfWork.DrinkSuitableProducts.CreateRangeAsync(drinkSuitableProducts);
        //    #endregion drinkSuitableProduct

        //    #region events
        //    var events = new List<Event>
        //    {
        //        new Event
        //        {
        //            Date = DateTime.Now,
        //            Description = "The best event ever 1",
        //            GroupId = groups[0].Id,
        //            Title = "YaggerMeister Party",
        //            Status = Enums.EventStatus.Planned
        //        },
        //        new Event
        //        {
        //            Date = DateTime.Now,
        //            Description = "The best event ever 2",
        //            GroupId = groups[0].Id,
        //            Title = "Lidsokoe funs party",
        //            Status = Enums.EventStatus.Canceled
        //        },
        //        new Event
        //        {
        //            Date = DateTime.Now,
        //            Description = "The best event ever 3",
        //            GroupId = groups[0].Id,
        //            Title = "Garage Enjoyers",
        //            Status = Enums.EventStatus.InReview
        //        }
        //    };
        //    await _unitOfWork.Events.CreateRangeAsync(events);
        //    #endregion events

        //    #region eventDrink
        //    var eventDrinks = new List<EventDrink>
        //    {
        //        new EventDrink
        //        {
        //            DrinkId = drinks[0].Id,
        //            EventId = events[0].Id
        //        },
        //        new EventDrink
        //        {
        //            DrinkId = drinks[0].Id,
        //            EventId = events[1].Id
        //        },
        //        new EventDrink
        //        {
        //            DrinkId = drinks[0].Id,
        //            EventId = events[2].Id
        //        },
        //        new EventDrink
        //        {
        //            DrinkId = drinks[1].Id,
        //            EventId = events[0].Id
        //        },
        //        new EventDrink
        //        {
        //            DrinkId = drinks[1].Id,
        //            EventId = events[1].Id
        //        }
        //    };
        //    await _unitOfWork.EventDrinks.CreateRangeAsync(eventDrinks);
        //    #endregion eventDrink

        //    #region eventPartisipants
        //    var eventParticipants = new List<EventParticipant>
        //    {
        //        new EventParticipant()
        //        {
        //            UserId = userIds[0],
        //            EventId = events[0].Id,
        //            Status = EventPartisipantStatus.Approved
        //        },
        //        new EventParticipant()
        //        {
        //            UserId = userIds[0],
        //            EventId = events[1].Id,
        //            Status = EventPartisipantStatus.NotResponded
        //        },
        //        new EventParticipant()
        //        {
        //            UserId = userIds[1],
        //            EventId = events[0].Id,
        //            Status = EventPartisipantStatus.Declined
        //        },
        //        new EventParticipant()
        //        {
        //            UserId = userIds[1],
        //            EventId = events[1].Id,
        //            Status = EventPartisipantStatus.Approved
        //        },
        //    };
        //    await _unitOfWork.EventParticipants.CreateRangeAsync(eventParticipants);
        //    #endregion eventPartisipants

        //    #region userGroups
        //    var userGroups = new List<UserGroup>
        //    {
        //        new UserGroup()
        //        {
        //            GroupId = groups[0].Id,
        //            UserId = userIds[0],
        //            Role = UserGroupRole.Admin,
        //            Status = GroupMembershipStatus.Member
        //        },
        //        new UserGroup()
        //        {
        //            GroupId = groups[1].Id,
        //            UserId = userIds[1],
        //            Role = UserGroupRole.Admin,
        //            Status = GroupMembershipStatus.Member
        //        },
        //        new UserGroup()
        //        {
        //            GroupId = groups[0].Id,
        //            UserId = userIds[1],
        //            Role = UserGroupRole.Partisipant,
        //            Status = GroupMembershipStatus.Member
        //        }
        //    };
        //    await _unitOfWork.UserGroups.CreateRangeAsync(userGroups);
        //    #endregion userDrinkReviews

        //    await _unitOfWork.SaveAsync();
        //}
    }
}