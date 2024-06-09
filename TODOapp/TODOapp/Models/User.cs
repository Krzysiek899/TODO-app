using Microsoft.AspNetCore.Identity;

namespace TODOapp.Models;

public class User : IdentityUser<Guid> {
    public ICollection<TodoTask> TodoTasks { get; } = new List<TodoTask>();
    public ICollection<Tag> Tags { get; } = new List<Tag>();
}