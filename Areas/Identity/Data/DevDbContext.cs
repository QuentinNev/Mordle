using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mordle.Data;

public class DevDbContext : IdentityDbContext<IdentityUser>
{
    protected readonly IConfiguration _configuration;
    public DevDbContext(IConfiguration configuration)
    : base()
    {
        _configuration = configuration;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite(_configuration.GetConnectionString("DefaultConnection"));
    }
}
