using NummyApi.Enums;

namespace NummyApi.Dtos.Domain;

public record GetCodeLogsRequestDto(int PageSize, int PageIndex, ICollection<CodeLogLevel> Levels);