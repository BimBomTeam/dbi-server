﻿using DBI.Domain.Entities.Core;

namespace DBI.Infrastructure.Queries
{
    public interface IHistoryQuery : IBaseQuery<SearchHistoryEntity, int>
    {
        IEnumerable<SearchHistoryEntity> GetHistoryByUser(string userId);
    }
}
