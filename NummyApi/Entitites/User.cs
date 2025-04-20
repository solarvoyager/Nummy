using NummyApi.Entitites.Generic;

namespace NummyApi.Entitites;

public class User : Auditable
{
    public required string Name { get; set; } 
    public required string Surname { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public required string PasswordSalt { get; set; }
    public string? Phone { get; set; }
    public DateTimeOffset LastLoginDate { get; set; }
    public virtual ICollection<TeamUser>? Teams { get; set; }
}