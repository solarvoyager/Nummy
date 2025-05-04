using System.ComponentModel.DataAnnotations;

namespace NummyUi.Models.Team;

public class TeamAddModel
{
    [Required] [MaxLength(120)] [MinLength(5)] public string Name { get; set; }
    [Required] [MaxLength(300)] [MinLength(5)] public string Description { get; set; }
    [Required] public IEnumerable<Guid> SelectedUserIds { get; set; } = [];
}