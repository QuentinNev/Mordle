using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Mordle.Data;

public class DevDbContext : IdentityDbContext<IdentityUser>
{
    public virtual DbSet<Word> Words { get; set; }
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
            builder.Entity<Word>().HasData(new Word(1, "Bonjour"));
            builder.Entity<Word>().HasData(new Word(2, "Voiture"));
            builder.Entity<Word>().HasData(new Word(3, "Raquette"));
            builder.Entity<Word>().HasData(new Word(4, "Anticonstitutionnellement"));
            builder.Entity<Word>().HasData(new Word(5, "Repas"));
            builder.Entity<Word>().HasData(new Word(6, "Escalade"));
            builder.Entity<Word>().HasData(new Word(7, "Poule"));
            builder.Entity<Word>().HasData(new Word(8, "Recherche"));
            builder.Entity<Word>().HasData(new Word(9, "Soigner"));
            builder.Entity<Word>().HasData(new Word(10, "Trompe"));
            builder.Entity<Word>().HasData(new Word(11, "Balancer"));
        }

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite(_configuration.GetConnectionString("DefaultConnection"));
    }
}
