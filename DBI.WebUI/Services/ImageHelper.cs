namespace DBI.WebUI.Services
{
    public static class ImageHelper
    {
        private const int SIZE = 300;
        public static string ScaleImage(string base64Image)
        {
            
            // Convert base64 to Image
            byte[] imageBytes = Convert.FromBase64String(base64Image);
            using (var inputStream = new MemoryStream(imageBytes))
            using (var image = Image.Load(inputStream))
            {
                // Resize the image
                image.Mutate(x => x.Resize(SIZE, SIZE));

                // Convert back to base64
                using (var outputStream = new MemoryStream())
                {
                    image.SaveAsJpeg(outputStream);
                    return Convert.ToBase64String(outputStream.ToArray());
                }
            }
        }
    }
}
