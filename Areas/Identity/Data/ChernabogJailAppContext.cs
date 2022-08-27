using ChernabogJailApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ChernabogJailApp.Models;

namespace ChernabogJailApp.Data;

public class ChernabogJailAppContext : IdentityDbContext<ChernabogJailAppUser>
{
    public ChernabogJailAppContext(DbContextOptions<ChernabogJailAppContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<ChernabogJailApp.Models.SpecialPower>? SpecialPower { get; set; }

    public DbSet<ChernabogJailApp.Models.BeastCategory>? BeastCategory { get; set; }

    public DbSet<ChernabogJailApp.Models.Beast>? Beast { get; set; }

    public DbSet<ChernabogJailApp.Models.BeastVariation>? BeastVariation { get; set; }
}
