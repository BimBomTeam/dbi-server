// DBI.Application
using DBI.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;

namespace DBI.Application
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<DogBreed> DogBreeds { get; set; }
        public DbSet<BreedTrainingProps> BreedTrainingProps { get; set; }
        public DbSet<SearchHistoryEntity> HistoryEntities { get; set; }
        public DbSet<ApplicationUserEntity> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DogBreed>()
                .HasOne(x => x.BreedTrainingProps)
                .WithOne(x => x.DogBreed)
                .HasForeignKey<BreedTrainingProps>(x => x.DogBreedId);

            modelBuilder.Entity<BreedTrainingProps>()
                .HasOne(x => x.DogBreed)
                .WithOne(x => x.BreedTrainingProps)
                .HasForeignKey<DogBreed>(x => x.BreedTrainingPropsId);


            modelBuilder.Entity<SearchHistoryEntity>()
                .HasOne(x => x.DogBreed)
                .WithMany(x => x.SearchHistories)
                .HasForeignKey(x => x.DogBreedId);

            modelBuilder.Entity<DogBreed>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<BreedTrainingProps>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<SearchHistoryEntity>().HasQueryFilter(e => !e.IsDeleted);

            modelBuilder.Entity<ApplicationUserEntity>()
                .HasKey(u => u.Id);
        }
    }
}
