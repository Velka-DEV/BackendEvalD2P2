using BackendEvalD2P2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendEvalD2P2.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Event> Events { get; set; } = default!;
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}