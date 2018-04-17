using System;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Identity;
using SoftwareHouse.DataAccess.Models.UserInformation;

namespace SoftwareHouse.DataAccess.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        /*PersonalInformation */
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDayDateTime { get; set; }
        public string Address { get; set; }

        /*---------------------*/
        public Collection<Qualification> Qualifications { get; set; }

        //[InverseProperty("ApplicationUser")]
        //[NotMapped]
        public Collection<UserRating> UserRatings { get; set; }

        public Collection<UserWorkPhoto> WorkPhotos { get; set; }

        public Collection<Experiance> Experiances { get; set; }

      

        public bool IsDeleted { get; set; }
    }
}