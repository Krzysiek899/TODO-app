using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TODOapp.Models;

namespace TODOapp.Data;

public class UserService {
    private readonly DataContext _context;
    private readonly UserManager<User> _userManager;
    private readonly HttpContextAccessor _httpContextAccessor;

    public UserService(DataContext context, UserManager<User> userManager, HttpContextAccessor httpContextAccessor) {
        _context = context;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<User>> GetUsersAsync() {
        if (_httpContextAccessor.HttpContext == null) {
            throw new Exception("Unauthorized");
        }
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        if (user == null) {
            throw new Exception("User does not exist");
        }
        return await _context.Users.Where(x => x.Id != user.Id).ToListAsync();
    }

    public async Task DeleteUserAsync(Guid userId) {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null) {
            throw new Exception("User does not exist.");
        }
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}