using System.ComponentModel.DataAnnotations;

namespace NummyUi.Models;

public class LoginModel
{
    [Required] public string Email { get; set; }

    [Required] public string Password { get; set; }

    public string Mobile { get; set; }

    public string Captcha { get; set; }

    public string LoginType { get; set; }

    public bool AutoLogin { get; set; }
}