using Microsoft.EntityFrameworkCore;
using TODOapp.Models;

namespace TODOapp.Data;

public class TagService {
    private readonly DataContext _context;

    public TagService(DataContext dataContext) {
        _context = dataContext;
    }

    public async Task<List<Tag>> GetTagsAsync() {
        return await _context.Tags.ToListAsync();
    }
}