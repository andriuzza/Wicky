using System;

namespace SoftwareHouse.Contract.DataContracts
{
    public class QualificationDto
    {
        public int Id { get; set; }

        public string QualificationField { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUserDto ApplicationUser { get; set; }
    }
}
