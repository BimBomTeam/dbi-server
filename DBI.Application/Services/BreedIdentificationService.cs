using DBI.Application.Services.MlNet;
using DBI.Infrastructure.Dto;
using DBI.Infrastructure.Queries;
using DBI.Infrastructure.Services;
using Tensorflow;

namespace DBI.Application.Services
{
    public class BreedIdentificationService : IBreedIdentificationService
    {
        private readonly IAiModelService modelService;
        private readonly IDogBreedQuery dogBreedQuery;
        public BreedIdentificationService(IDogBreedQuery dogBreedQuery, IAiModelService modelService)
        {
            this.modelService = modelService;
            this.dogBreedQuery = dogBreedQuery;
        }
        public async Task<DogBreedDto> IdentifyAsync(string base64)
        {
            try
            {
                var trainingIndex = await Task.Run(() => modelService.IdentifyAsync(base64));

                var dbObject = await dogBreedQuery.GetBreedByTrainingIdAsync(trainingIndex);
                if (dbObject == null)
                    throw new InvalidArgumentError();

                var result = new DogBreedDto() 
                { 
                    Name = dbObject.ShowName, 
                    Description = dbObject.ShortDescription, 
                    Id = dbObject.Id,
                    AvatarLink = dbObject.AvatarLink,
                };

                return result;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
