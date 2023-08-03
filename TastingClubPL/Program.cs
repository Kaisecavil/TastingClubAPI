
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
                    Password = ApplicationUserDefaultValueConstants.DefaultAdminPassword
                };
                var userUser = new ApplicationUserDtoForRegister() 
                {
                    Email = ApplicationUserDefaultValueConstants.DefaultUserEmail,
                    Password = ApplicationUserDefaultValueConstants.DefaultUserPassword
                };
                await authService.RegisterUserAsync(adminUser);
                await authService.RegisterUserAsync(userUser);


                await userManager.AddToRoleAsync(await userManager.FindByEmailAsync(adminUser.Email), RoleConstants.AdminRole);
                await userManager.AddToRoleAsync(await userManager.FindByEmailAsync(userUser.Email), RoleConstants.UserRole);
                var service = scope.ServiceProvider.GetService<Seed>();
                service.SeedApplicationContextAsync();

            }
        }
    }
}