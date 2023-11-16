namespace DBI.WebUI.Services
{
    public static class ImageHelper
    {
        private const int SIZE = 300;
        public static string ScaleImage(string base64Image)
        {
            
            byte[] imageBytes = Convert.FromBase64String(base64Image);
            using (var inputStream = new MemoryStream(imageBytes))
            using (var image = Image.Load(inputStream))
            {
                image.Mutate(x => x.Resize(SIZE, SIZE));

                using (var outputStream = new MemoryStream())
                {
                    image.SaveAsJpeg(outputStream);
                    return Convert.ToBase64String(outputStream.ToArray());
                }
            }
        }
    }
}
