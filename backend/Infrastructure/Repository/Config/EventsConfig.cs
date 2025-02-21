using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Repository.Config;

public class EventsConfig : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Id)
            .HasColumnName("EventId")
            .ValueGeneratedOnAdd();
        
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("Name"); 
        
        builder.Property(e => e.Description)
            .HasMaxLength(255)
            .HasColumnName("Description"); 
        
        builder.Property(e => e.StartDate)
            .IsRequired()
            .HasColumnName("StartDate"); 

        builder.Property(e => e.Location)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("Location"); 

        builder.Property(e => e.Category)
            .IsRequired()
            .HasColumnName("Category"); 

        builder.Property(e => e.MaxParticipants)
            .IsRequired()
            .HasColumnName("MaxParticipants"); 

        builder.Property(e => e.Image)
            .HasMaxLength(255)
            .HasColumnName("Image"); 
        
        builder.HasMany(e => e.Participants)
            .WithOne(ep => ep.Event)
            .HasForeignKey(ep => ep.EventId);
    }
}