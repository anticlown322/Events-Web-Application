using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Repository.Config;

public class ParticipantsConfig : IEntityTypeConfiguration<Participant>
{
    public void Configure(EntityTypeBuilder<Participant> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(e => e.Id)
            .HasColumnName("ParticipantId") 
            .ValueGeneratedOnAdd(); 

        builder.Property(e => e.Name)
            .IsRequired() 
            .HasMaxLength(100) 
            .HasColumnName("Name"); 

        builder.Property(e => e.Surname)
            .IsRequired() 
            .HasMaxLength(100) 
            .HasColumnName("Surname"); 

        builder.Property(e => e.DateOfBirth)
            .IsRequired() 
            .HasColumnName("DateOfBirth"); 

        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(320) 
            .HasColumnName("Email"); 
        
        builder.Property(e => e.RegistrationTime)
            .IsRequired() 
            .HasColumnName("RegistrationTime"); 
        
        builder.HasOne(p => p.Event)
            .WithMany(ep => ep.Participants)
            .HasForeignKey(ep => ep.EventId);
    }
}