using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

        public IEnumerable<UserRatingDto> UserRatings { get; set; }

        public IEnumerable<UserWorkPhotoDto> WorkPhotos { get; set; }

        public IEnumerable<ExperianceDto> Experiances { get; set; }

        public IEnumerable<QualificationDto> Qualifications { get; set; }
    }
}
