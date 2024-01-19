namespace NummyApi.Dtos.Domain;

public enum SortOrder
{
    Ascending,
    Descending
}

public enum CodeLogSortType
{
    TraceIdentifier,
    LogLevel,
    Title,
    Description,
    ExceptionType,
    CreatedAt
}

public enum RequestLogSortType
{
    TraceIdentifier,
    Body,
    Method,
    Path,
    RemoteIp
}

public enum ResponseLogSortType
{
    Body,
    StatusCode
}