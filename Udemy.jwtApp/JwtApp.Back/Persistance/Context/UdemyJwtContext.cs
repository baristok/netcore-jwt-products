using JwtApp.Back.Core.Domain;
using JwtApp.Back.Persistance.Configurations;
using Microsoft.EntityFrameworkCore;

namespace JwtApp.Back.Persistance.Context;

public class UdemyJwtContext : DbContext
{
    public UdemyJwtContext(DbContextOptions<UdemyJwtContext> options) : base(options)
    {
    }
    
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<AppRole> AppRoles { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new AppUserConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}