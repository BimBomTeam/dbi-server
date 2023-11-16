using System.Buffers.Text;

namespace DBI.WebUI.Services
{
    public class DogBreedIdentificationService
    {
        public string Identify(string base64)
        {
            string scaledBase64 = ImageHelper.ScaleImage(base64);
            return scaledBase64;
        }
    }
}
