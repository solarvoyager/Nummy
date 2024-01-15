using NummyUi.Dtos.Enums;

namespace NummyUi.Dtos.Domain;

public record GetCodeLogsRequestDto
(
    int PageSize,
    int PageIndex, 
    string? Query, 
    CodeLogSortType? SortType,
    SortOrder? SortOrder,
    ICollection<CodeLogLevel> Levels
);