using System.ComponentModel.DataAnnotations;

namespace NummyUi.Models.Account;

public class LoginModel
{
    [Required] [MaxLength(30)] public string Email { get; set; }
    [Required] [MaxLength(30)] public string Password { get; set; }
    public bool AutoLogin { get; set; }
}