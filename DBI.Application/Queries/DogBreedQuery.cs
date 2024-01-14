using DBI.Domain.Entities.Core;
using DBI.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace DBI.Application.Queries
{
    public class DogBreedQuery : BaseQuery<DogBreed, int>, IDogBreedQuery
    {
        public DogBreedQuery(ApplicationDbContext context) : base(context) { }

        public async Task<DogBreed?> GetBreedByTrainingIdAsync(int trainingId)
        {
            var result = await this.context.DogBreeds.Include(nameof(DogBreed.BreedTrainingProps)).FirstOrDefaultAsync(x => x.BreedTrainingProps.IdInTrainingDataset == trainingId);
            if (result == null)
                return null;

            return result;
        }
    }
}
