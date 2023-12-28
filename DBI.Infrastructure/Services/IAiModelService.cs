namespace DBI.Infrastructure.Services
{
    public interface IAiModelService
    {
        public Task<int> IdentifyAsync(string base64);
    }
}
