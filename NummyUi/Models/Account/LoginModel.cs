using System.ComponentModel.DataAnnotations;

namespace NummyUi.Models;

public class LoginModel
{
    [Required] public string Email { get; set; }
    [Required] public string Password { get; set; }
    public bool AutoLogin { get; set; }
}