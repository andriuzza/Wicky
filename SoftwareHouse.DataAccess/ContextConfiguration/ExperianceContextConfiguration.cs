using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftwareHouse.DataAccess.Models.UserInformation;

namespace SoftwareHouse.DataAccess.ContextConfiguration
{
    public class ExperianceContextConfiguration : IEntityTypeConfiguration<Experiance>
    {
        public void Configure(EntityTypeBuilder<Experiance> builder)
        {
            builder.HasKey(x => x.Id);


            builder.Property(s => s.ExperianceType)
            .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Experiances)
                .HasForeignKey(x => x.UserId);
        }
    }
}
