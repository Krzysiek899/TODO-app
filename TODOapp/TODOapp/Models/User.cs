using Microsoft.AspNetCore.Identity;

namespace TODOapp.Models;

public class User : IdentityUser {
    public ICollection<TodoTask> TodoTasks { get; } = new List<TodoTask>();
    public ICollection<Tag> Tags { get; } = new List<Tag>();
}