using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftwareHouse.DataAccess.Models;
using SoftwareHouse.DataAccess.Models.UserInformation;

namespace SoftwareHouse.DataAccess.ContextConfiguration
{
    public class UserWorkPhotoContextConfiguration : IEntityTypeConfiguration<UserWorkPhoto>
    {

        public void Configure(EntityTypeBuilder<UserWorkPhoto> builder)
        {
            builder.ToTable("UserWorkPhoto");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.PhotoUrl).IsRequired();

            builder.HasOne(x=>x.ApplicationUser)
                .WithMany(x => x.WorkPhotos)
                .HasForeignKey(x => x.ApplicationUserId);
        }
    }
}
