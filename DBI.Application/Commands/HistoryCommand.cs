using DBI.Domain.Entities.Core;
using DBI.Infrastructure.Commands;

namespace DBI.Application.Commands
{
    public class HistoryCommand : BaseCommand<SearchHistoryEntity, int>, IHistoryCommand
    {
        public HistoryCommand(ApplicationDbContext context) : base(context) 
        {
        }
    }
}
