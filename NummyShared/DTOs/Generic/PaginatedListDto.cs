namespace NummyShared.DTOs.Generic;

public record PaginatedListDto<T>(
    int TotalCount,
    IEnumerable<T> Datas
);