using Microsoft.EntityFrameworkCore;
using TODOapp.Models;

namespace TODOapp.Data;

public class TodoTaskService {
    private readonly DataContext _context;

    public TodoTaskService(DataContext dataContext) {
        _context = dataContext;
    }

    public async Task<List<TodoTask>> GetTodoTasks() {
        return await _context.TodoTasks.ToListAsync();
    }
    public async Task AddTodoTaskAsync(TodoTask todoTask) {
        _context.TodoTasks.Add(todoTask);
        await _context.SaveChangesAsync();
    }
}