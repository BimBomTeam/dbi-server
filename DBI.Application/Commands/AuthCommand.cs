using DBI.Domain.Entities.Core;
using DBI.Infrastructure.Commands;

namespace DBI.Application.Commands
{
    public class AuthCommand : BaseCommand<User, string>, IAuthCommand
    {
        public AuthCommand(ApplicationDbContext context) : base(context) { }
    }
}
