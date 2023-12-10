using DBI.Domain.Entities.Core;

namespace DBI.Infrastructure.Commands
{
    public interface IHistoryCommand : IBaseCommand<SearchHistoryEntity, int>
    {
    }
}
