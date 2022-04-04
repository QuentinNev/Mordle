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
            builder.Entity<Word>().HasData(new Word(1, "PAPILLON"));
            builder.Entity<Word>().HasData(new Word(2, "TROUPEAU"));
            builder.Entity<Word>().HasData(new Word(3, "ADORABLE"));
            builder.Entity<Word>().HasData(new Word(4, "ENTREPOT"));
            builder.Entity<Word>().HasData(new Word(5, "GOURMAND"));
            builder.Entity<Word>().HasData(new Word(6, "IRONIQUE"));
            builder.Entity<Word>().HasData(new Word(7, "LAPEREAU"));
            builder.Entity<Word>().HasData(new Word(8, "LOCUTEUR"));
            builder.Entity<Word>().HasData(new Word(9, "MARECAGE"));
            builder.Entity<Word>().HasData(new Word(10, "NETTOYER"));
            builder.Entity<Word>().HasData(new Word(11, "ORNEMENT"));
        }

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite(_configuration.GetConnectionString("DefaultConnection"));
    }
}
