using DBI.Domain.Entities.Core;

namespace DBI.Infrastructure.Queries
{
    public interface IDogBreedQuery : IBaseQuery<DogBreed, int>
    {
        Task<DogBreed?> GetBreedByTrainingIdAsync(int trainingId);
    }
}
