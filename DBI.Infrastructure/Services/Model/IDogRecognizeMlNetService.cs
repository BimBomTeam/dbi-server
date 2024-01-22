namespace DBI.Infrastructure.Services.Model
{
    public interface IDogRecognizeMlNetService
    {
        public Task<bool> RecogniteDogOnImageAsync(string base64);
    }
}
