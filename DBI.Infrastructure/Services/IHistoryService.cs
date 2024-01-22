using DBI.Infrastructure.Dto;

namespace DBI.Application.Services
{
    public interface IHistoryService
    {
        List<HistoryDto> GetSearchHistoryByUser(string userId);
        Task<HistoryDto> AddSearchHistory(HistoryDto historyEntityDto);
        Task DeleteSearchHistory(int id);
    }
}
