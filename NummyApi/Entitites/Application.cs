using NummyApi.Entitites.Generic;

namespace NummyApi.Entitites;

public class Application : Auditable
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public virtual ICollection<TeamApplication> TeamApplications { get; set; } = [];
}