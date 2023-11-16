namespace DBI.WebUI.Services
{
    public class DogBreedIdentificationService
    {
        public string Identify(string base64)
        {
            try
            {
                string scaledBase64 = ImageHelper.ScaleImage(base64);
                return scaledBase64;
            }catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
