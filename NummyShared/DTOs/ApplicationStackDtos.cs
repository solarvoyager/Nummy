using NummyShared.DTOs.Enums;

namespace NummyShared.DTOs;

public record ApplicationStackToListDto(
    Guid Id,
    ApplicationStackType Type,
    string IconSvg,
    string Title
);