namespace NummyShared.Dtos.Generic;

public record PaginatedListDto<T>(
    int TotalCount,
    IEnumerable<T> Datas
);