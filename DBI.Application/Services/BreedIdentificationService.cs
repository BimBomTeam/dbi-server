using DBI.Infrastructure.Dto;
using DBI.Infrastructure.Services;

namespace DBI.Application.Services
{
    public class BreedIdentificationService : IBreedIdentificationService
    {
        private readonly IAiModelService modelService;
        public BreedIdentificationService()
        {
            this.modelService = new TensorflowNetModel();
            //this.modelService = modelService;
        }
        public async Task<DogBreedDto> IdentifyAsync(string base64)
        {
            try
            {
                string scaledBase64 = ImageHelper.ScaleImage(base64);
                var result = await Task.Run(() => modelService.IdentifyAsync(scaledBase64));

                var dict = new Dictionary<int, string>()
                {
                    { 0, "Affenpisscher" },
                    { 1, "Affgan hound" },
                    { 2, "Dalmatian" }
                };

                var res = new DogBreedDto() { Name = dict[result], Description = "" };

                return res;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
