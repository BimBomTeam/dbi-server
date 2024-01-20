using DBI.Infrastructure.Dto;

namespace DBI.Infrastructure.Services.Authorization
{
    public interface IFirebaseAuthService
    {
        Task<string?> SignUpAsync(UserCredential dto);
        Task<string?> LoginAsync(UserCredential dto);

    }
}
