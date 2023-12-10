using DBI.Domain.Entities.Core;
using DBI.Infrastructure.Queries;

namespace DBI.Application.Queries
{
    public class HistoryQuery : BaseQuery<SearchHistoryEntity, int>, IHistoryQuery
    {
        public HistoryQuery(ApplicationDbContext context) : base(context) 
        {
        }
    }
}
