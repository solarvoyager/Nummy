﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NummyApi.DataContext;
using NummyApi.Dtos;
using NummyApi.Dtos.Domain;
using NummyApi.Dtos.Generic;
using NummyApi.Entitites;
using NummyApi.Services.Abstract;

namespace NummyApi.Services.Concrete;

public class CodeLogService(NummyDataContext dataContext, IMapper mapper) : ICodeLogService
{
    public async Task Add(CodeLogToAddDto dto)
    {
        var mapped = mapper.Map<CodeLog>(dto);

        await dataContext.AddAsync(mapped);
        await dataContext.SaveChangesAsync();
    }

    public async Task<PaginatedListDto<CodeLogToListDto>> Get(GetCodeLogsDto dto)
    {
        var skip = (dto.PageIndex - 1) * dto.PageSize;

        var query = dataContext.CodeLogs
            .Where(l => dto.Levels.Contains(l.LogLevel));

        if (!string.IsNullOrWhiteSpace(dto.Query))
            query = query.Where(l =>
                EF.Functions.Like(l.TraceIdentifier!.ToLower(), $"%{dto.Query.ToLower()}%") ||
                EF.Functions.Like(l.Title.ToLower(), $"%{dto.Query.ToLower()}%") ||
                EF.Functions.Like(l.Description!.ToLower(), $"%{dto.Query.ToLower()}%") ||
                EF.Functions.Like(l.ExceptionType!.ToLower(), $"%{dto.Query.ToLower()}%"));

        var totalCount = await query.CountAsync();

        if (dto.SortType is not null && dto.SortOrder is not null)
            query = dto.SortType switch
            {
                CodeLogSortType.TraceIdentifier => dto.SortOrder == SortOrder.Descending
                    ? query.OrderByDescending(q => q.TraceIdentifier)
                    : query.OrderBy(q => q.TraceIdentifier),
                CodeLogSortType.LogLevel => dto.SortOrder == SortOrder.Descending
                    ? query.OrderByDescending(q => q.LogLevel)
                    : query.OrderBy(q => q.LogLevel),
                CodeLogSortType.Title => dto.SortOrder == SortOrder.Descending
                    ? query.OrderByDescending(q => q.Title)
                    : query.OrderBy(q => q.Title),
                CodeLogSortType.Description => dto.SortOrder == SortOrder.Descending
                    ? query.OrderByDescending(q => q.Description)
                    : query.OrderBy(q => q.Description),
                CodeLogSortType.ExceptionType => dto.SortOrder == SortOrder.Descending
                    ? query.OrderByDescending(q => q.ExceptionType)
                    : query.OrderBy(q => q.ExceptionType),
                CodeLogSortType.CreatedAt => dto.SortOrder == SortOrder.Descending
                    ? query.OrderByDescending(q => q.CreatedAt)
                    : query.OrderBy(q => q.CreatedAt),
                _ => query
            };

        query = query
            .Skip(skip)
            .Take(dto.PageSize);

        var mapped = mapper.Map<IEnumerable<CodeLogToListDto>>(await query.ToListAsync());

        return new PaginatedListDto<CodeLogToListDto>(totalCount, mapped);
    }

    public async Task<bool> Delete(DeleteCodeLogsDto dto)
    {
        await dataContext.CodeLogs.Where(l => dto.Ids.Contains(l.Id)).ExecuteDeleteAsync();

        return true;
    }
}