using CountryInfoService.Models;
using CountryInfoService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CountryInfoService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Country> Countries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>().HasData(
            new Country { Id = 1, Name = "Sample", ISOCode = "XX" }
        );
    }
}