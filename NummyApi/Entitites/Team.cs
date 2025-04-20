using NummyApi.Entitites.Generic;

namespace NummyApi.Entitites;

public class Team : Auditable
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public virtual ICollection<TeamUser>? Users { get; set; }
}