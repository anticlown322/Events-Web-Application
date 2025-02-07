using Domain.Entities.Models;
using Domain.Repository.Config;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repository;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions options)
        : base(options)
    {}
    
    public DbSet<Event>? Events { get; set; }
    public DbSet<Participant>? Participants { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EventsConfig());
        modelBuilder.ApplyConfiguration(new ParticipantsConfig());
    }
}