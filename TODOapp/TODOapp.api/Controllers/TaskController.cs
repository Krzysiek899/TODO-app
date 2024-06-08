using Microsoft.AspNetCore.Mvc;
using TODOapp.Data;
using TODOapp.Models;

namespace TODOapp.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoTasksController : ControllerBase {
    private readonly TodoTaskRepository _todoRepository;

    public TodoTasksController(TodoTaskRepository todoTaskRepository) {
        _todoRepository = todoTaskRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<TodoTask>>> Get() {
        var todoTasks = await _todoRepository.GetTodoTasksAsync();
        return Ok(todoTasks);
    }

    [HttpGet("{guid}")]
    public async Task<ActionResult<TodoTask>> Get(Guid guid) {
        var todoTask = await _todoRepository.GetTodoTaskAsync(guid);
        if (todoTask == null)
            return NotFound();
        return Ok(todoTask);
    }

    [HttpPost]
    public async Task<ActionResult> Post(TodoTask todoTask) {
        await _todoRepository.AddTodoTaskAsync(todoTask);
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