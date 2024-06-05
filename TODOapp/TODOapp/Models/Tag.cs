using System.ComponentModel.DataAnnotations;

namespace TODOapp.Models;

public class Tag {
    [Key]
    public Guid TagId { get; set; }
    public string Name { get; set; }

    public ICollection<TodoTask> TodoTasks { get; } = new List<TodoTask>();
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}