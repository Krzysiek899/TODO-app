using System.ComponentModel.DataAnnotations;

namespace TODOapp.Models;

public class TodoTask {
    [Key]
    public Guid TaskId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public TaskImportance Importance { get; set; } = TaskImportance.Medium;
    public DateTime? DueDate { get; set; }

    public string UserId { get; set; }
    public User User { get; set; } = null!;
    public ICollection<Tag> Tags { get; } = new List<Tag>();

}

public enum TaskImportance {
    Critical,
    VeryHigh,
    High,
    Medium,
    Low,
    VeryLow
}