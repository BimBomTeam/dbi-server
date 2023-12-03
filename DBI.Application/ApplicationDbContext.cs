using DBI.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;

namespace DBI.Application
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public virtual DbSet<DogBreed> DogBreeds { get; set; }
        public virtual DbSet<BreedTrainingProps> BreedTrainingProps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DogBreed>()
                .HasOne(x => x.BreedTrainingProps)
                .WithOne(x => x.DogBreed)
                .HasForeignKey<BreedTrainingProps>(x => x.DogBreedId)
                .IsRequired();

            modelBuilder.Entity<DogBreed>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<BreedTrainingProps>().HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
