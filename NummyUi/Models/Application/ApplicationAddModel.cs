using System.ComponentModel.DataAnnotations;

namespace NummyUi.Models.Application;

public class ApplicationAddModel
{
    [Required] [MaxLength(120)] [MinLength(5)] public string Name { get; set; }
    [Required] [MaxLength(300)] [MinLength(5)] public string Description { get; set; }
}