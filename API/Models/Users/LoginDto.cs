using System.ComponentModel.DataAnnotations;

namespace API.Models.Users;

public class LoginDto
{
    [Required]
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(15, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
        MinimumLength = 6)]
    public string Password { get; set; }
}
