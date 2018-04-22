using SoftwareHouse.Contract.DataContracts;

namespace SoftwareHouse.DataAccess.Models.UserInformation
{
    public class Experiance
    {
        public int Id { get; set; }
        public ExperianceType ExperianceType { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }

    }

 
}