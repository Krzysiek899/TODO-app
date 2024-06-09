using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TODOapp.Data;
using TODOapp.Models;

namespace TODOapp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoTasksController : ControllerBase {
    private readonly DataContext _context;

    private readonly TodoTaskService _todoTaskService;
    public TodoTasksController(DataContext dataContext, TodoTaskService todoTaskService) {
        _context = dataContext;
        _todoTaskService = todoTaskService;
    }

    [HttpGet]
    public async Task<ActionResult<List<TodoTask>>> Get() {
        var todoTasks = await _context.TodoTasks.ToListAsync();
        return Ok(todoTasks);
    }

    [HttpGet("{guid}")]
    public async Task<ActionResult<TodoTask>> Get(Guid guid) {
        var todoTask = await _context.TodoTasks.FindAsync(guid);
        if (todoTask == null)
            return NotFound();
        return Ok(todoTask);
    }

    [HttpPost]
    public async Task<ActionResult> Post(TodoTask todoTask) {
        _context.TodoTasks.Add(todoTask);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("{guid}")]
    public async Task<ActionResult> Put(Guid guid, TodoTask todoTask) {
        if (guid != todoTask.TaskId) {
            return BadRequest();
        }
        var oldTask = await _context.TodoTasks.FindAsync(guid);
        if (oldTask == null)
            return NotFound();

        oldTask.Title = todoTask.Title;
        oldTask.Description = todoTask.Description;
        oldTask.Importance = todoTask.Importance;
        oldTask.DueDate = todoTask.DueDate;
        oldTask.UserId = todoTask.UserId;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(string guid) {
        var todoTask = await _context.TodoTasks.FindAsync(guid);
        if (todoTask == null) {
            return NotFound();
        }

        _context.TodoTasks.Remove(todoTask);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}