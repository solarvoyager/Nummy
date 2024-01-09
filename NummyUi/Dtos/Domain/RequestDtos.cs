using NummyApi.Enums;

namespace NummyUi.Dtos.Domain;

public record GetCodeLogsRequestDto(int PageSize, int PageIndex, ICollection<CodeLogLevel> Levels);