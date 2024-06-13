using System.ComponentModel.DataAnnotations;

namespace TODOapp.Models;

public class UserManagerModel
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Role { get; set; }
}