using NummyApi.Entitites.Generic;
using NummyShared.DTOs.Enums;

namespace NummyApi.Entitites;

public class CodeLog : Auditable
{
    public string? TraceIdentifier { get; set; }
    public Guid ApplicationId { get; set; }
    public virtual Application? Application { get; set; }
    public required CodeLogLevel LogLevel { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? StackTrace { get; set; }
    public string? InnerException { get; set; }
    public string? ExceptionType { get; set; }
}