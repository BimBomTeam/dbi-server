using DBI.Application.Services.MlNet;
using DBI.Infrastructure.Dto;
using DBI.Infrastructure.Queries;
using DBI.Infrastructure.Services;
using DBI.Infrastructure.Services.Model;
using Tensorflow;

namespace DBI.Application.Services
{
    public class BreedIdentificationService : IBreedIdentificationService
    {
        private readonly IBreedIdentifyMlNetService identificationService;
        private readonly IDogRecognizeMlNetService recognitionService;
        private readonly IDogBreedQuery dogBreedQuery;
        public BreedIdentificationService(IDogBreedQuery dogBreedQuery, IBreedIdentifyMlNetService identificationService, IDogRecognizeMlNetService recognitionService)
        {
            this.recognitionService = recognitionService;
            this.identificationService = identificationService;
            this.dogBreedQuery = dogBreedQuery;
        }
        public async Task<DogBreedDto> IdentifyAsync(string base64)
        {
            try
            {
                var isDog = await Task.Run(() => recognitionService.RecogniteDogOnImageAsync(base64));
                var result = new DogBreedDto() { Id = -1 };

                if (isDog)
                {
                    var trainingIndex = await Task.Run(() => identificationService.IdentifyAsync(base64));

                    var dbObject = await dogBreedQuery.GetBreedByTrainingIdAsync(trainingIndex);
                    if (dbObject == null)
                        throw new InvalidArgumentError();

                    result = new DogBreedDto()
                    {
                        Name = dbObject.ShowName,
                        Description = dbObject.ShortDescription,
                        Id = dbObject.Id,
                        AvatarLink = dbObject.AvatarLink,
                    };

                }

                return result;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
