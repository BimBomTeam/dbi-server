namespace DBI.Infrastructure.Providers
{
    public interface IJwtProvider
    {
        Task<string> GetForCredentialsAsync(string email, string password);
    }
}
