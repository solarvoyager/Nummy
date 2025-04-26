using System.ComponentModel.DataAnnotations.Schema;
using NummyApi.Entitites.Generic;

namespace NummyApi.Entitites;

public class TeamApplication : Auditable
{
    [ForeignKey("Team")] public required Guid TeamId { get; set; }

    public virtual Team? Team { get; set; }

    [ForeignKey("Application")] public required Guid ApplicationId { get; set; }

    public virtual Application? Application { get; set; }
}