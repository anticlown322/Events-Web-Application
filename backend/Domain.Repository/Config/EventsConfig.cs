using Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Repository.Config;

public class EventsConfig : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasData(
            new Event
            {
                Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                Category = Category.Carnival,
                Description = "It is a carnival",
                Image = "",
                Location = "London",
                MaxParticipants = 1000,
                Name = "LondonCarnival",
                StartDate = new DateTime(2023, 10, 15, 10, 0, 0),
            },
            new Event
            {
                Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                Category = Category.Competition,
                Description = "It is a competition",
                Image = "",
                Location = "Grodno",
                MaxParticipants = 100,
                Name = "GrodnoCompetition",
                StartDate = new DateTime(2023, 11, 1, 14, 30, 0),
            }
        );
    }
}