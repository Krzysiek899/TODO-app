using Microsoft.EntityFrameworkCore;
using TODOapp.Models;

namespace TODOapp;

public class TaskContext : DbContext {

    public TaskContext(DbContextOptions<TaskContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
}