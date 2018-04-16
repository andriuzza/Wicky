using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftwareHouse.DataAccess.Models;

namespace SoftwareHouse.DataAccess.ContextConfiguration
{

    public class UserContextConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(sp => sp.Id);

            builder.Property(s => s.BirthDayDateTime)
                .IsRequired();

            builder.Property(s => s.Address)
                .IsRequired();

            builder.Property(s => s.Name)
                .IsRequired();

            builder.Property(s => s.LastName)
                .IsRequired();
        }
    }
}
