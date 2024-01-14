using AutoMapper;
using DBI.Domain.Entities.Core;
using DBI.Infrastructure.Commands;
using DBI.Infrastructure.Dto;
using DBI.Infrastructure.Queries;

namespace DBI.Application.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly IMapper mapper;

        IHistoryQuery historyQuery;
        IHistoryCommand historyCommand;

        public HistoryService(IHistoryQuery historyQuery, IMapper mapper, IHistoryCommand historyCommand)
        {
            this.mapper = mapper;
            this.historyQuery = historyQuery;
            this.historyCommand = historyCommand;
        }

        public List<HistoryDto> GetSearchHistory()
        {
            var historyEntities = historyQuery.GetAll();
            var result = historyEntities.Select(x => mapper.Map<HistoryDto>(x)).ToList();
            return result;
        }

        public async Task<HistoryDto> AddSearchHistory(HistoryDto historyEntityDto)
        {

            var historyEntity = mapper.Map<SearchHistoryEntity>(historyEntityDto);
            historyEntity.Date = DateTime.Now;
            historyEntity.UserUid = historyEntityDto.UserUid;
            historyEntityDto = mapper.Map<HistoryDto>(await historyCommand.AddAsync(historyEntity));
            await historyCommand.SaveChangesAsync();

            return historyEntityDto;
        }

        public async void DeleteSearchHistory(int id)
        {
            historyCommand.Delete(id);
            await historyCommand.SaveChangesAsync();
        }
    }
}
