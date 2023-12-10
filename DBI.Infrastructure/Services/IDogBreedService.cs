using DBI.Infrastructure.Dto;

namespace DBI.Infrastructure.Services
{
    public interface IDogBreedService
    {
        List<DogBreedDto> GetAllBreeds();
        Task<DogBreedDto> AddBreed(DogBreedDto dogDto);
        void DeleteBreed(int id);
        void EditBreed(DogBreedDto dogDto);
    }
}
