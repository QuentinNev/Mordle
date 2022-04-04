using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.IO;

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
        if (File.Exists("words.json"))
        {

        }
        else
        {
            builder.Entity<Word>().HasData(new Word("Bonjour"));
            builder.Entity<Word>().HasData(new Word("Voiture"));
            builder.Entity<Word>().HasData(new Word("Raquette"));
            builder.Entity<Word>().HasData(new Word("Anticonstitutionnellement"));
            builder.Entity<Word>().HasData(new Word("Repas"));
            builder.Entity<Word>().HasData(new Word("Escalade"));
            builder.Entity<Word>().HasData(new Word("Poule"));
            builder.Entity<Word>().HasData(new Word("Recherche"));
            builder.Entity<Word>().HasData(new Word("Soigner"));
            builder.Entity<Word>().HasData(new Word("Trompe"));
            builder.Entity<Word>().HasData(new Word("Balancer"));
        }

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite(_configuration.GetConnectionString("DefaultConnection"));
    }
}
