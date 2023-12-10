using DBI.Domain.Entities.Core;
using DBI.Infrastructure.Dto;

namespace DBI.Infrastructure.Services
{
    public interface IDogBreedService
    {
        List<DogBreedDto> GetAllBreeds();
        Task<DogBreedDto> AddBreed(DogBreedDto dogDto);
        void DeleteBreed(int id);
        void UpdateBreed(DogBreedDto dogDto);
    }
}
