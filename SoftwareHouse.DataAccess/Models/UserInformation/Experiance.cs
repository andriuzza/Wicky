using System;
using Wicky.EntityFramework.Models.PersonalInformation;

namespace SoftwareHouse.DataAccess.Models.UserInformation
{
    public class Experiance
    {
        public int Id { get; set; }
        public ExperianceType ExperianceType { get; set; }
        public ApplicationUser User { get; set; }
        public Guid UserId { get; set; }

    }

 
}