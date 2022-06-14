using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Core.Dtos;

public class RegistrationDto
{
    [Required(ErrorMessage = "UserName is required")]
    public string? UserName { get; set; }
    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

}