using Microsoft.AspNetCore.Mvc;
using TODOapp.Data;
using TODOapp.Models;

namespace TODOapp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoTasksController : ControllerBase {
    private readonly TodoTaskService _todoService;

    public TodoTasksController(TodoTaskService todoTaskService) {
        _todoService = todoTaskService;
    }

    [HttpGet]
    public async Task<ActionResult<List<TodoTask>>> Get() {
        var todoTasks = await _todoService.GetTodoTasksAsync();
        return Ok(todoTasks);
    }

    [HttpGet("{guid}")]
    public async Task<ActionResult<TodoTask>> Get(Guid guid) {
        var todoTask = await _todoService.GetTodoTaskAsync(guid);
        if (todoTask == null)
            return NotFound();
        return Ok(todoTask);
    }

    [HttpPost]
    public async Task<ActionResult> Post(TodoTask todoTask) {
        await _todoService.AddTodoTaskAsync(todoTask);
        return NoContent();
    }

    [HttpPut("{guid}")]
    public ActionResult Put(Guid guid, TodoTask todoTask) {
        if (guid != todoTask.TaskId) {
            return BadRequest();
        }

        return NoContent();
    }

    [HttpDelete]
    public ActionResult Delete(string guid) {
        return Ok("delete task by guid");
    }
}