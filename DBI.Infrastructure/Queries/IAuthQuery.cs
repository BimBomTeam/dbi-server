using DBI.Domain.Entities.Core;

namespace DBI.Infrastructure.Queries
{
    public interface IAuthQuery : IBaseQuery<User, string>
    {
    }
}
