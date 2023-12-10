public static class ImageHelper
{
    private const int SIZE = 300;

    public static string ScaleImage(string base64Image)
    {
        try
        {
            byte[] imageBytes = Convert.FromBase64String(base64Image);
            //string base64Check = Convert.ToBase64String(imageBytes);
            //if (!base64Check.Equals(base64Image))
            //    throw new ArgumentException("Bad format Base64.");

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
        catch (Exception ex)
        {
            throw new ArgumentException("Bad format Base64.", ex);
        }
    }
}