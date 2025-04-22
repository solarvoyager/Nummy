using System.ComponentModel.DataAnnotations;

namespace NummyUi.Models;

public class RegisterModel
{
    [Required]
    public string Name { get; set; }
        
    [Required]
    public string Surname { get; set; }
        
    [Required]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }

    [Required]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    //[Required]
    public string? Phone { get; set; }

    //[Required]
    public string Captcha { get; set; }
}