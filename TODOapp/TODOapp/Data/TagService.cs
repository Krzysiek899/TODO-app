using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TODOapp.Models;

namespace TODOapp.Data;

public class TagService {
    private readonly DataContext _context;
    private readonly UserManager<User> _userManager;
    private readonly HttpContextAccessor _httpContextAccessor;

    public TagService(DataContext context, UserManager<User> userManager, HttpContextAccessor httpContextAccessor) {
        _context = context;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<Tag>> GetTagsAsync() {
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        if (user == null) {
            throw new Exception("User does not exist");
        }
        return await _context.Tags.Where(x => x.UserId == user.Id).ToListAsync();
    }

    public async Task AddTagAsync(TagDTO newTag) {
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        if (user == null) {
            throw new Exception("User does not exist");
        }
        var tag = new Tag {
            TagId = Guid.NewGuid(),
            Name = newTag.Name,
            User = user
        };
        _context.Tags.Add(tag);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTagAsync(Guid tagId) {
        var tag = await _context.Tags.FindAsync(tagId);
        if (tag == null) {
            throw new Exception("Tag does not exist");
        }
        _context.Tags.Remove(tag);
        await _context.SaveChangesAsync();
    }
}

public class TagDTO {
    public string Name { get; set; }
}