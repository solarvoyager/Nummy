using NummyApi.Entitites.Generic;

namespace NummyApi.Entitites;

public class Header : Auditable
{
    public required string Key { get; set; }
    public required string Value { get; set; }
    public Guid? RequestLogId { get; set; }
    public Guid? ResponseLogId { get; set; }
}