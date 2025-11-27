using NummyApi.Entitites.Generic;

namespace NummyApi.Entitites;

public class ResponseLog : Auditable
{
    public required Guid HttpLogId { get; set; }
    public required string Body { get; set; }
    public required int StatusCode { get; set; }
    public required long DurationMs { get; set; }
    public string? Headers { get; set; }
    
}