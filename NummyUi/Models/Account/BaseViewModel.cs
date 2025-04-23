using System.ComponentModel.DataAnnotations;

namespace NummyUi.Models;

public class BaseViewModel
{
    [Required] public string Name { get; set; } 
    [Required] public string Surname { get; set; }
    public string Avatar { get; set; }
    [Required] public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    [MaxLength(7)] public string Phone { get; set; }
}