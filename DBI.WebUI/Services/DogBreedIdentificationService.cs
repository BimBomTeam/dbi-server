namespace DBI.WebUI.Services
{
    public class DogBreedIdentificationService
    {
        private readonly ModelService modelService;
        public DogBreedIdentificationService(ModelService modelService)
        {
            this.modelService = modelService;
        }
        public string Identify(string base64)
        {
            try
            {
                string scaledBase64 = ImageHelper.ScaleImage(base64);
                var result = modelService.Identify(scaledBase64);

                var dict = new Dictionary<int, string>()
                {
                    { 0, "Affenpisscher" },                
                    { 1, "Affgan hound" },                
                    { 2, "Dalmatian" }             
                };

                return dict[result];
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
