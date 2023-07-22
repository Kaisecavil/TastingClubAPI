using TastingClubDAL.Models.Base;
using TastingClubDAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TastingClubDAL.Database
{

    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Group> Groups { get; set; }
        //public DbSet<DrinkType> DrinkTypes { get; set; }
        public DbSet<UserDrinkReview> UserDrinkReviews { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();

        }

        public override int SaveChanges()
        {
            ProcessEntityNavigationProperties();
            HandleEntityDelete();
            return base.SaveChanges();
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await ProcessEntitiesAsync();
            HandleEntityDelete();
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //many to many with join table
            modelBuilder.Entity<Drink>()
                .HasMany(e => e.SuitableProducts)
                .WithMany(e => e.Drinks)
                .UsingEntity<DrinkSuitableProduct>();

            //modelBuilder.Entity<Drink>()
            //    .HasO

            //No duplicate frogs on exebiton
            modelBuilder.Entity<UserDrinkReview>()
                .HasIndex(e => new { e.DrinkId, e.UserId, e.DateOfDegustation }, "UniqueDrinkId_UserId_DateOfDegustation").IsUnique(true);
        }

        private void HandleEntityDelete()
        {
            var entities = ChangeTracker.Entries()
                        .Where(e => e.State == EntityState.Deleted);
            foreach (var entity in entities)
            {
                if (entity.Entity is BaseModel)
                {
                    entity.State = EntityState.Modified;
                    var baseModel = entity.Entity as BaseModel;
                    baseModel.IsDeleted = true;
                }
            }
        }

        private void HandleDependent(EntityEntry entry)
        {
            entry.State = EntityState.Deleted;
        }

        private void ProcessEntityNavigationProperties()
        {
            foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted).ToList())
            {
                var navigations = new List<NavigationEntry>();
                foreach (var navigation in entry.Navigations)
                {
                    try
                    {
                        var navigationConvertTest = (IReadOnlyNavigation)navigation.Metadata;
                        navigations.Add(navigation);
                    }
                    catch (InvalidCastException ex)
                    {
                        continue;
                    }
                }

                foreach (var navigationEntry in navigations
                    .Where(n => !((IReadOnlyNavigation)n.Metadata).IsOnDependent))
                {
                    if (navigationEntry is CollectionEntry collectionEntry)
                    {
                        navigationEntry.Load();
                        foreach (var dependentEntry in collectionEntry.CurrentValue)
                        {
                            HandleDependent(Entry(dependentEntry));
                        }
                    }
                    else
                    {
                        var dependentEntry = navigationEntry.CurrentValue;
                        if (dependentEntry != null)
                        {
                            HandleDependent(Entry(dependentEntry));
                        }
                    }
                }
            }
        }

        private async Task ProcessEntitiesAsync()
        {
            foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted).ToList())
            {
                var navigations = new List<NavigationEntry>();
                foreach (var navigation in entry.Navigations)
                {
                    try
                    {
                        var navigationConvertTest = (IReadOnlyNavigation)navigation.Metadata;
                        navigations.Add(navigation);
                    }
                    catch (InvalidCastException ex)
                    {
                        continue;
                    }
                }

                foreach (var navigationEntry in navigations
                    .Where(n => !((IReadOnlyNavigation)n.Metadata).IsOnDependent))
                {
                    if (navigationEntry is CollectionEntry collectionEntry)
                    {
                        await navigationEntry.LoadAsync();
                        foreach (var dependentEntry in collectionEntry.CurrentValue)
                        {
                            HandleDependent(Entry(dependentEntry));
                        }
                    }
                    else
                    {
                        var dependentEntry = navigationEntry.CurrentValue;
                        if (dependentEntry != null)
                        {
                            HandleDependent(Entry(dependentEntry));
                        }
                    }
                }
            }
        }
    }

}
