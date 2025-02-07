using Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Repository.Config;

public class ParticipantsConfig : IEntityTypeConfiguration<Participant>
{
    public void Configure(EntityTypeBuilder<Participant> builder)
    {
        builder.HasData(
            new Participant
            {
                Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                DateOfBirth = DateOnly.FromDateTime(new DateTime(2003, 5, 12)),
                Email = "johnDoe@test.com",
                Name = "John",
                Surname = "Doe",
                RegistrationTime = new DateTime(2023, 10, 10),
                EventId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
            },
            new Participant
            {
                Id = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
                DateOfBirth = DateOnly.FromDateTime(new DateTime(1998, 8, 22)),
                Email = "rbSmth@test.com",
                Name = "Robert",
                Surname = "Smith",
                RegistrationTime = new DateTime(2023, 10, 12),
                EventId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3")
            },
            new Participant
            {
                Id = new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"),
                DateOfBirth = DateOnly.FromDateTime(new DateTime(2007, 3, 5)),
                Email = "Zaza@test.com",
                Name = "Damn",
                Surname = "Sheesh",
                RegistrationTime = new DateTime(2023, 10, 14),
                EventId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3")
            }
        );
    }
}