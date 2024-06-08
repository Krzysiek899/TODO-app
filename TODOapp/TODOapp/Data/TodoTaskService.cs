using System.Formats.Asn1;
using Microsoft.EntityFrameworkCore;
using TODOapp.Models;

namespace TODOapp.Data;

public class TodoTaskService {
    private readonly DataContext _context;

    public TodoTaskService(DataContext dataContext) {
        _context = dataContext;
    }

    public async Task<List<TodoTask>> GetTodoTasksAsync() {
        return await _context.TodoTasks.ToListAsync();
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