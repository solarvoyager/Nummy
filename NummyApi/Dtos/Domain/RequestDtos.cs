using NummyApi.Dtos.Enums;

namespace NummyApi.Dtos.Domain;

public record GetCodeLogsDto(
    int PageSize,
    int PageIndex,
    string? Query,
    CodeLogSortType? SortType,
    SortOrder? SortOrder,
    ICollection<CodeLogLevel> Levels
);

public record GetRequestLogsDto(
    int PageSize,
    int PageIndex,
    string? Query,
    RequestLogSortType? SortType,
    SortOrder? SortOrder
);

public record GetResponseLogsDto(
    int PageSize,
    int PageIndex,
    string? Query,
    ResponseLogSortType? SortType,
    SortOrder? SortOrder
);

public record DeleteCodeLogsDto(
    ICollection<Guid> Ids
);

public record DeleteRequestLogsDto(
    ICollection<Guid> Ids
);

public record DeleteResponseLogsDto(
    ICollection<Guid> Ids
);