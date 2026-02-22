using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NummyApi.DataContext;
using NummyApi.Entitites;
using NummyApi.Services.Abstract;
using NummyShared.DTOs;
using NummyShared.DTOs.Domain;

namespace NummyApi.Services.Concrete;

public class ResponseLogService(NummyDataContext dataContext, IMapper mapper) : IResponseLogService
{
    public async Task<bool> Delete(DeleteResponseLogsDto dto, CancellationToken cancellationToken = default)
    {
        await dataContext.ResponseLogs
            .Where(l => dto.Ids.Contains(l.Id))
            .ExecuteDeleteAsync(cancellationToken);

        return true;
    }

    public async Task Add(ResponseLogToAddDto dto, CancellationToken cancellationToken = default)
    {
        var mapped = mapper.Map<ResponseLog>(dto);

        var headers = dto.Headers.Select(h => new Header
        {
            Key = h.Key,
            Value = h.Value,
        }).ToList();

        mapped.Headers = headers;

        await dataContext.ResponseLogs.AddAsync(mapped, cancellationToken);
        await dataContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<HttpLogDto?> Get(Guid httpLogId, CancellationToken cancellationToken = default)
    {
        using var transaction = await dataContext.Database.BeginTransactionAsync(
            System.Data.IsolationLevel.ReadCommitted, cancellationToken);

        var request = await dataContext.RequestLogs
            .Include(l => l.Headers)
            .FirstOrDefaultAsync(l => l.HttpLogId == httpLogId, cancellationToken);

        if (request == null)
            return null;

        var response = await dataContext.ResponseLogs
            .Include(l => l.Headers)
            .FirstOrDefaultAsync(l => l.HttpLogId == httpLogId, cancellationToken);

        await transaction.CommitAsync(cancellationToken);

        return new HttpLogDto(
            response?.Id ?? Guid.Empty,
            request.HttpLogId,
            request.TraceIdentifier,
            request.Body,
            request.Headers.Select(h => new HeaderDto(h.Key, h.Value)).ToList(),
            response?.Body,
            response?.Headers.Select(h => new HeaderDto(h.Key, h.Value)).ToList(),
            response?.StatusCode,
            response?.DurationMs,
            response?.CreatedAt
        );
    }
}
