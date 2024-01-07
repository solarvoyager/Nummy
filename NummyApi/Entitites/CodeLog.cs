using NummyApi.Entitites.Generic;
using NummyApi.Enums;

namespace NummyApi.Entitites;

public class CodeLog : Auditable
{
    public string? TraceIdentifier { get; set; }
    public required CodeLogLevel LogLevel { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? StackTrace { get; set; }
    public string? InnerException { get; set; }
    public string? ExceptionType { get; set; }
}