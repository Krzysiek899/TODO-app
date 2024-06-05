using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TODOapp.Data;

public class DataContext: IdentityDbContext
{
    public DataContext(DbContextOptions options) : base(options) { }
}