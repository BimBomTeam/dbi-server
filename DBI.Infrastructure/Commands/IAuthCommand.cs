using DBI.Domain.Entities.Core;

namespace DBI.Infrastructure.Commands
{
    public interface IAuthCommand : IBaseCommand<User, string>
    {
    }
}
