namespace NummyShared.Dtos.Domain;

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
    Method,
    Path,
    RemoteIp,
    CreatedAt
}

public enum ResponseLogSortType
{
    StatusCode
}