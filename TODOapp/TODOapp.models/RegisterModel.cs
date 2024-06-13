using System.ComponentModel.DataAnnotations;

namespace TODOapp.Models;

public class RegisterModel
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string UserName { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}