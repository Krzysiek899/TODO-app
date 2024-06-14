using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
// using TODOapp.api.Controllers;
using TODOapp.Models;

namespace TODOapp.Data;

public class TodoTaskService {
    private readonly DataContext _context;
    private readonly UserManager<User> _userManager;
    private readonly HttpContextAccessor _httpContextAccessor;

    public TodoTaskService(DataContext context, UserManager<User> userManager, HttpContextAccessor httpContextAccessor) {
        _context = context;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<TodoTask>> GetPendingTodoTasksAsync() {
        if (_httpContextAccessor.HttpContext == null) {
            throw new Exception("Unauthorized");
        }
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        if (user == null) {
            throw new Exception("User does not exist");
        }
        return await _context.TodoTasks.Where(x => x.UserId == user.Id && !x.IsCompleted).Include(x => x.Tags).ToListAsync();
    }

    public async Task<List<TodoTask>> GetCompletedTodoTasksAsync() {
        if (_httpContextAccessor.HttpContext == null) {
            throw new Exception("Unauthorized");
        }
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        if (user == null) {
            throw new Exception("User does not exist");
        }
        return await _context.TodoTasks.Where(x => x.UserId == user.Id && x.IsCompleted).Include(x => x.Tags).ToListAsync();
    }

    public async Task<TodoTask?> GetTaskAsync(Guid taskId) {
        if (_httpContextAccessor.HttpContext == null) {
            throw new Exception("Unauthorized");
        }
        return await _context.TodoTasks.FindAsync(taskId);
    }

    public async Task AddTodoTaskAsync(TodoTaskDTO newTodoTask) {
        if (_httpContextAccessor.HttpContext == null) {
            throw new Exception("Unauthorized");
        }
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        if (user == null) {
            throw new Exception("User does not exist");
        }
        var todoTask = new TodoTask {
            TaskId = Guid.NewGuid(),
            Title = newTodoTask.Title,
            Description = newTodoTask.Description,
            Importance = newTodoTask.Importance,
            DueDate = newTodoTask.DueDate,
            IsCompleted = false,
            Tags = newTodoTask.Tags,
            User = user
        };
        _context.TodoTasks.Add(todoTask);
        await _context.SaveChangesAsync();
    }

    public async Task CompleteTaskAsync(Guid taskId) {
        var todoTask = await _context.TodoTasks.FindAsync(taskId);
        if (todoTask == null) {
            throw new Exception("Task does not exist");
        }
        todoTask.IsCompleted = true;
        await _context.SaveChangesAsync();
    }

    public async Task RestoreTaskAsync(Guid taskId) {
        var todoTask = await _context.TodoTasks.FindAsync(taskId);
        if (todoTask == null) {
            throw new Exception("Task does not exist");
        }
        todoTask.IsCompleted = false;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTodoTaskAsync(Guid taskId, TodoTaskDTO updatedTodoTask) {
        if (_httpContextAccessor.HttpContext == null) {
            throw new Exception("Unauthorized");
        }
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
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
        todoTask.Tags = updatedTodoTask.Tags;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTodoTaskAsync(Guid taskId) {
        var todoTask = await _context.TodoTasks.FindAsync(taskId);
        if (todoTask == null) {
            throw new Exception("Task does not exist");
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
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
}