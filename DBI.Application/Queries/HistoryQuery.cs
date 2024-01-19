using DBI.Domain.Entities.Core;
using DBI.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace DBI.Application.Queries
{
    public class HistoryQuery : BaseQuery<SearchHistoryEntity, int>, IHistoryQuery
    {
        public HistoryQuery(ApplicationDbContext context) : base(context) 
        {
        }

        public IEnumerable<SearchHistoryEntity> GetHistoryByUser(string userId)
        {
            return context.HistoryEntities.Where(x => x.UserId == userId);
        }
    }
}
