using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftwareHouse.DataAccess.Models;
using SoftwareHouse.DataAccess.Models.UserInformation;

namespace SoftwareHouse.DataAccess.ContextConfiguration
{
    public class UserRatingContextConfiguration : IEntityTypeConfiguration<UserRating>
    {

        public void Configure(EntityTypeBuilder<UserRating> builder)
        {
            builder.HasKey(sp => sp.Id);

            builder.Property(s => s.Feedback)
                .IsRequired();

            builder.HasOne(x => x.UserAssessor)
                .WithMany(x => x.Ratings)
                .HasForeignKey(x => x.UserAssessorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.UserEvaluated)
                .WithMany(x => x.Ratings)
                .HasForeignKey(x => x.UserEvaluatedId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
