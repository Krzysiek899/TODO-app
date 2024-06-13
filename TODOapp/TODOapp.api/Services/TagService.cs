using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TODOapp.Data;
using TODOapp.Models;

namespace TODOapp.api.Services;

public class TagService {
    private readonly DataContext _context;
    private readonly UserManager<User> _userManager;

    public TagService(DataContext dataContext, UserManager<User> userManager) {
        _context = dataContext;
        _userManager = userManager;
    }

    public async Task<List<Tag>> GetTagAsync(string userId) {
        return await _context.Tags.Where(x => x.UserId == userId).ToListAsync();
    }

    public async Task AddTagAsync(string userId, TagDTO newTag) {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) {
            throw new Exception("User does not exist");
        }
        var tag = new Tag() {
            TagId = Guid.NewGuid(),
            Name = newTag.Name,
            User = user
        };
        _context.Tags.Add(tag);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTagAsync(string userId, Guid tagId, TagDTO updatedTag) {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) {
            throw new Exception("User does not exist");
        }

        var tag = await _context.Tags.FindAsync(tagId);
        if (tag == null) {
            throw new Exception("Tag does not exist");
        }

        tag.Name = updatedTag.Name;
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

public class TagDTO {
    public string Name { get; set; }
}