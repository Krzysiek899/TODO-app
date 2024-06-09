using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TODOapp.Models;

namespace TODOapp.Data;

public class UserNotFoundException : Exception {}

public class TodoTaskService {
    private readonly DataContext _context;
    private readonly UserManager<User> _userManager;

    public TodoTaskService(DataContext dataContext, UserManager<User> userManager) {
        _context = dataContext;
        _userManager = userManager;
    }

    public async Task<TodoTask> CreateTodoTask(string title, string description, TaskImportance taskImportance, Guid userId, DateTime? dueDate) {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null) {
            throw new UserNotFoundException();
        }

        var todoTask = new TodoTask {
            Title = title,
            Description = description,
            Importance = taskImportance,
            DueDate = dueDate,
            UserId = userId,
            User = user
        };
        return todoTask;
    }

    public async Task<List<TodoTask>> GetTodoTasksWithTagsAsync() {
        return await _context.TodoTasks.Include(x => x.Tags).ToListAsync();
    }

    public async Task<TodoTask?> GetTodoTaskAsync(Guid guid) {
        return await _context.TodoTasks.FindAsync(guid);
    }

    public async Task AddTodoTaskAsync(TodoTask todoTask) {
        _context.TodoTasks.Add(todoTask);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTodoTaskAsync(Guid guid, TodoTask todoTask) {
        var oldTask = await _context.TodoTasks.FindAsync(guid);

    }
}