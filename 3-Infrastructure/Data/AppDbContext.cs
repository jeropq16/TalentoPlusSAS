using _2_Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace _3_Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Department>()
            .ToTable("Departments");

        modelBuilder.Entity<Employee>()
            .ToTable("Employees");
    }
}