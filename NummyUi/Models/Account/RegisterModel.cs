using System.ComponentModel.DataAnnotations;

namespace NummyUi.Models.Account;

public class RegisterModel
{
    [Required] [MaxLength(30)] public string Name { get; set; }
    [Required] [MaxLength(30)] public string Surname { get; set; }
    [Required] [MaxLength(30)]public string Email { get; set; }
    [Required] [MinLength(6)] [MaxLength(40)] public string Password { get; set; }
    [Required] [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
    //[Required]
    [MaxLength(7)]
    public string? Phone { get; set; }
    //[Required]
    public string Captcha { get; set; }
}