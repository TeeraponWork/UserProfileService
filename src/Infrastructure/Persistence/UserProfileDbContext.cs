using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class UserProfileDbContext : DbContext
    {
        public UserProfileDbContext(DbContextOptions<UserProfileDbContext> options) : base(options) { }
        public DbSet<UserProfileAggregate> Profiles => Set<UserProfileAggregate>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var e = modelBuilder.Entity<UserProfileAggregate>();
            e.ToTable("UserProfiles");
            e.HasKey(x => x.Id);
            e.HasIndex(x => x.UserId).IsUnique();

            e.Property(x => x.DisplayName).HasMaxLength(200);
            e.Property(x => x.BloodType).HasMaxLength(10);

            // ChronicConditions as owned collection
            e.OwnsMany(x => x.ChronicConditions, nav =>
            {
                nav.ToTable("UserProfileChronicConditions");
                nav.WithOwner().HasForeignKey("ProfileId");
                nav.Property<int>("Id");
                nav.HasKey("Id");
                nav.Property(x => x.Name).IsRequired().HasMaxLength(200);
                nav.Property(x => x.Notes).HasMaxLength(1000);
            });

            // Allergies as owned collection
            e.OwnsMany(x => x.Allergies, nav =>
            {
                nav.ToTable("UserProfileAllergies");
                nav.WithOwner().HasForeignKey("ProfileId");
                nav.Property<int>("Id");
                nav.HasKey("Id");
                nav.Property(x => x.Name).IsRequired().HasMaxLength(200);
                nav.Property(x => x.Severity).HasMaxLength(50);
            });
        }
    }
}
