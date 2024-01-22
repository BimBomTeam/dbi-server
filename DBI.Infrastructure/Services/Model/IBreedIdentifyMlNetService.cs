namespace DBI.Infrastructure.Services.Model
{
    public interface IBreedIdentifyMlNetService
    {
        public Task<int> IdentifyAsync(string base64);
    }
}
