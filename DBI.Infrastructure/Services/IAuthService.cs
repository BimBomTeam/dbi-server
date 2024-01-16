using DBI.Infrastructure.Dto;

namespace DBI.Infrastructure.Services
{
    public interface IAuthService
    {
        public Task<string> AddUserAsync(UserRegisterDto userRegisterDto);
        public Task<IEnumerable<UserDto>> GetAllUsers();
    }
}
