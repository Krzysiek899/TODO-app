using System.ComponentModel.DataAnnotations;

namespace TODOapp.Models;

public class User {
    [Key]
    public Guid UserId { get; set;}
    public string Email { get; set; }
    public string Password { get; set; }

    public ICollection<TodoTask> TodoTasks { get; } = new List<TodoTask>();
    public ICollection<Tag> Tags { get; } = new List<Tag>();
}