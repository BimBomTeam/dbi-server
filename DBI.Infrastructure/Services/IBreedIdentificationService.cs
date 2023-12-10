using DBI.Infrastructure.Dto;

namespace DBI.Infrastructure.Services
{
    public interface IBreedIdentificationService
    {
        public Task<DogBreedDto> IdentifyAsync(string base64);
    }
}
