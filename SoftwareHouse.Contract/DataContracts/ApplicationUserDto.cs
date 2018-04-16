using System;

namespace SoftwareHouse.Contract.DataContracts
{
    public class ApplicationUserDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDayDateTime { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
