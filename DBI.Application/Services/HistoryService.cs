﻿using AutoMapper;
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

        public List<HistoryDto> GetSearchHistoryByUser(string userId)
        {
            var historyEntities = historyQuery.GetHistoryByUser(userId);
            var historyDtos = historyEntities.Select(x => mapper.Map<HistoryDto>(x)).ToList();
            //var result = historyEntities.Select(x => mapper.Map<HistoryDto>(x)).ToList();
            return historyDtos;
        }

        public async Task<HistoryDto> AddSearchHistory(HistoryDto historyEntityDto)
        {
            var historyEntity = mapper.Map<SearchHistoryEntity>(historyEntityDto);
            historyEntity.Date = DateTime.UtcNow;
            historyEntity.UserId = historyEntityDto.UserId;
            var addedHistory = await historyCommand.AddAsync(historyEntity);
            historyEntityDto = mapper.Map<HistoryDto>(addedHistory);
            await historyCommand.SaveChangesAsync();

            return historyEntityDto;
        }

        public async Task DeleteSearchHistory(int id)
        {
            if (await historyQuery.GetByIdAsync(id) == null)
                return;

            historyCommand.Delete(id);
            await historyCommand.SaveChangesAsync();
        }
    }
}
