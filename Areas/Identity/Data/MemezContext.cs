using Memez.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Memez.Models;
namespace Memez.Data;

public class MemezContext : IdentityDbContext<MemezUser>
{
    public MemezContext(DbContextOptions<MemezContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.Entity<Meme>()
            .HasOne<MemezUser>(u => u.MemezUser)
            .WithMany(m => m.Memes)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Meme>()
            .Property(v => v);
    }

    public DbSet<Meme>? Memes { get; set; }
    public DbSet<Vote>? Votes { get; set; }
}
