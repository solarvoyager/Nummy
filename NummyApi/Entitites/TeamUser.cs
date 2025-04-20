using System.ComponentModel.DataAnnotations.Schema;
using NummyApi.Entitites.Generic;

namespace NummyApi.Entitites;

public class TeamUser : Auditable
{
    [ForeignKey("User")]
    public required Guid UserId { get; set; }
    public virtual User? User { get; set; }
    [ForeignKey("Team")]
    public required Guid TeamId { get; set; }
    public virtual Team? Team { get; set; }
}