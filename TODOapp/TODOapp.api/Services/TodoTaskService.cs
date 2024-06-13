using System.Formats.Asn1;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TODOapp.Models;

namespace TODOapp.Data;

public class TodoTaskService {
    private readonly DataContext _context;
    private readonly UserManager<User> _userManager;

    public TodoTaskService(DataContext dataContext, UserManager<User> userManager) {
        _context = dataContext;
        _userManager = userManager;
    }

    public async Task<List<TodoTask>> GetTodoTasksAsync(string userId) {
        return await _context.TodoTasks.Where(x => x.UserId == userId).ToListAsync();
    }

    public async Task AddTodoTaskAsync(string userId, TodoTaskDTO newTodoTask) {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) {
            throw new Exception("User does not exist");
        }
        var todoTask = new TodoTask {
            TaskId = Guid.NewGuid(),
            Title = newTodoTask.Title,
            Description = newTodoTask.Description,
            Importance = newTodoTask.Importance,
            DueDate = newTodoTask.DueDate,
            IsCompleted = newTodoTask.IsCompleted,
            User = user
        };
        _context.TodoTasks.Add(todoTask);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTodoTaskAsync(string userId, Guid taskId, TodoTaskDTO updatedTodoTask) {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) {
            throw new Exception("User does not exist");
        }

        var todoTask = await _context.TodoTasks.FindAsync(taskId);
        if (todoTask == null) {
            throw new Exception("Task does not exist");
        }
        todoTask.Title = updatedTodoTask.Title;
        todoTask.Description = updatedTodoTask.Description;
        todoTask.Importance = updatedTodoTask.Importance;
        todoTask.DueDate = updatedTodoTask.DueDate;
        todoTask.IsCompleted = updatedTodoTask.IsCompleted;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTodoTaskAsync(string userId, Guid taskId) {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) {
            throw new Exception("User does not exist");
        }
        var todoTask = await _context.TodoTasks.FindAsync(taskId);
        if (todoTask == null) {
            throw new Exception("Task does not exist");
        }
        if (todoTask.User != user) {
            throw new Exception("Unathorized");
        }
        _context.TodoTasks.Remove(todoTask);
        await _context.SaveChangesAsync();
    }
}

public class TodoTaskDTO {
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public TaskImportance Importance { get; set; } = TaskImportance.Medium;
    public DateTime? DueDate { get; set; }
    public bool IsCompleted { get; set; } = false;
}