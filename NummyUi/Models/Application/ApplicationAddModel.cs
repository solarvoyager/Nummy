using System.ComponentModel.DataAnnotations;
using NummyShared.DTOs.Enums;

namespace NummyUi.Models.Application;

public class ApplicationAddModel
{
    [Required] [MaxLength(120)] [MinLength(5)] public string Name { get; set; }
    [Required] [MaxLength(300)] [MinLength(5)] public string Description { get; set; }
    [Required] public Guid StackTypeId { get; set; }
}