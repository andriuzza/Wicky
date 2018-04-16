using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftwareHouse.DataAccess.Models;
using SoftwareHouse.DataAccess.Models.UserInformation;
using Wicky.EntityFramework.Models.PersonalInformation;

namespace SoftwareHouse.DataAccess.ContextConfiguration
{
    public class QualificationContextConfiguration : IEntityTypeConfiguration<Qualification>
    {
        public void Configure(EntityTypeBuilder<Qualification> builder)
        {

            builder.ToTable("Qualification");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.QualificationField).IsRequired();
            builder.Property(x => x.QualificationField).IsRequired();

            builder.HasOne(x => x.ApplicationUser)
                .WithMany(x => x.Qualifications)
                .HasForeignKey(x => x.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}