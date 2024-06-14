using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TODOapp.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace TODOapp.Data;

public class UserService {
    private readonly DataContext _context;
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(DataContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor) {
        _context = context;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<UserWithRole>> GetUsersAsync() {
        if (_httpContextAccessor.HttpContext == null) {
            throw new Exception("Unauthorized");
        }
        var currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        if (currentUser == null) {
            throw new Exception("User does not exist");
        }

        var users = await _context.Users.Where(x => x.Id != currentUser.Id).ToListAsync();
        var userWithRoles = new List<UserWithRole>();

        foreach (var user in users) {
            var roles = await _userManager.GetRolesAsync(user);
            userWithRoles.Add(new UserWithRole {
                UserName = user.UserName,
                UserId = user.Id,
                Role = roles.FirstOrDefault() ?? "User"
            });
        }

        return userWithRoles;
    }

    public async Task DeleteUserAsync(Guid userId) {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null) {
            throw new Exception("User does not exist.");
        }
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task SetUserAsAdminAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null) {
            throw new Exception("User does not exist.");
        }
        if (!await _userManager.IsInRoleAsync(user, "ADMIN")) {
            await _userManager.RemoveFromRoleAsync(user, "USER");
            await _userManager.AddToRoleAsync(user, "ADMIN");
        }
    }

    public async Task SetAdminAsUserAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null) {
            throw new Exception("User does not exist.");
        }

        if (!await _userManager.IsInRoleAsync(user, "USER")) {
            await _userManager.RemoveFromRoleAsync(user, "ADMIN");
            await _userManager.AddToRoleAsync(user, "USER");
        }
        
    }
}

public class UserWithRole
{
    public string UserName { get; set; }
    public string Role { get; set; }
    
    public Guid UserId { get; set; }
}
