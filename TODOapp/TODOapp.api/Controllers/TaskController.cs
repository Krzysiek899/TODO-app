using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TODOapp.Data;
using TODOapp.Models;

namespace TODOapp.api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "User, Admin")]
public class TodoTasksController : ControllerBase {
    private readonly TodoTaskService _todoService;
    public TodoTasksController(TodoTaskService todoTaskService, UserManager<User> userManager) {
        _todoService = todoTaskService;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<ActionResult<List<TodoTask>>> Get() {
        var userId = User.FindFirstValue("uid");
        if (userId == null) {
            return Unauthorized();
        }

        var todoTasks = await _todoService.GetTodoTasksAsync(userId);
        return Ok(todoTasks);
    }

    [HttpPost]
    public async Task<ActionResult> Post(TodoTaskDTO newTodoTask) {
        var userId = User.FindFirstValue("uid");
        if (userId == null) {
            return Unauthorized();
        }

        await _todoService.AddTodoTaskAsync(userId, newTodoTask);
        return Ok();
    }

    [HttpPut("{taskId}")]
    public async Task<ActionResult> Put(Guid taskId, TodoTaskDTO updatedTodoTask) {
        var userId = User.FindFirstValue("uid");
        if (userId == null) {
            return Ok();
        }

        await _todoService.UpdateTodoTaskAsync(userId, taskId, updatedTodoTask);
        return Ok();
    }

    [HttpDelete("{taskId}")]
    public async Task<ActionResult> Delete(Guid taskId) {
        var userId = User.FindFirstValue("uid");
        if (userId == null) {
            return Unauthorized();
        }

        await _todoService.DeleteTodoTaskAsync(userId, taskId);
        return Ok();
    }
}
