using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Repository.Config;

public class RoleConfig : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Name = "Participant",
                NormalizedName = "PARTICIPANT"
            },
            new IdentityRole
            {
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            }
        );
    }
}