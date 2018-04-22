using System;

namespace SoftwareHouse.Contract.DataContracts
{
    public class ExperianceDto
    {
        public int Id { get; set; }
        public ExperianceType ExperianceType { get; set; }
        public ApplicationUserDto User { get; set; }
        public string UserId { get; set; }
    }
}
