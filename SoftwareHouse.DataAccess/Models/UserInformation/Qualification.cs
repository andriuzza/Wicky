using System;

namespace SoftwareHouse.DataAccess.Models.UserInformation
{
    public class Qualification
    {
        public int Id { get; set; }

        public string QualificationField { get; set; }

        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}