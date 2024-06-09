using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TODOapp.Models;

namespace TODOapp.Data;

public class DataContext: IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public DataContext(DbContextOptions options) : base(options) {}

    public DbSet<TodoTask> TodoTasks { get; set;}
    public DbSet<Tag> Tags { get; set; }
}