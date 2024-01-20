using DBI.Domain.Entities.Core;
using DBI.Infrastructure.Queries;

namespace DBI.Application.Queries
{
    public class AuthQuery : BaseQuery<User, string>, IAuthQuery
    {
        public AuthQuery(ApplicationDbContext context) : base(context) { }
    }
}
