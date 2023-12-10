using AutoMapper;
using DBI.Domain.Entities.Core;
using DBI.Infrastructure.Commands;
using DBI.Infrastructure.Dto;
using DBI.Infrastructure.Queries;
using DBI.Infrastructure.Services;

namespace DBI.Application.Services
{
    public class DogBreedService : IDogBreedService
    {
        private IMapper mapper;

        IDogBreedQuery dogBreedQuery;
        IDogBreedCommand dogBreedCommand;
        public DogBreedService(IDogBreedQuery dogBreedQuery, IMapper mapper, IDogBreedCommand dogBreedCommand)
        {
            this.mapper = mapper;
            this.dogBreedQuery = dogBreedQuery;
            this.dogBreedCommand = dogBreedCommand;
        }
        public List<DogBreedDto> GetAllBreeds()
        {
            var dogBreeds = dogBreedQuery.GetAll();

            var result = dogBreeds.Select(x => mapper.Map<DogBreedDto>(x)).ToList();

            return result;
        }
        public async Task<DogBreedDto> AddBreed(DogBreedDto dogDto)
        {
            var dogBreed = mapper.Map<DogBreed>(dogDto);
            dogDto = mapper.Map<DogBreedDto>(await dogBreedCommand.AddAsync(dogBreed));
            await dogBreedCommand.SaveChangesAsync();

            return dogDto;
        }
        public async void DeleteBreed(int id)
        {
            dogBreedCommand.Delete(id);
            await dogBreedCommand.SaveChangesAsync();
        }
        public async void UpdateBreed(DogBreedDto dogDto)
        {
            var dogBreed = mapper.Map<DogBreed>(dogDto);
            dogBreedCommand.Update(dogBreed);
            await dogBreedCommand.SaveChangesAsync();
        }
    }
}
