using NummyApi.Entitites.Generic;

namespace NummyApi.Entitites;

public class RequestLog : Auditable
{
    public required Guid HttpLogId { get; set; }
    public required string TraceIdentifier { get; set; }
    public required string Body { get; set; }
    public required string Method { get; set; }
    public required string Path { get; set; }
    public string? RemoteIp { get; set; }
}