using System.ComponentModel.DataAnnotations;

namespace _1_Application.DTOs;

public class LoginDto
{
    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}