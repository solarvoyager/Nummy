namespace NummyUi.Data.Entitites;

public class NummyCodeLog
{
    public int NummyCodeLogId { get; set; }
    public required string LogGuid { get; set; }
    public required NummyCodeLogLevel LogLevel { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? StackTrace { get; set; }
    public string? InnerException { get; set; }
    public string? ExceptionType { get; set; }
    public required DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
}