using System;

namespace SoftwareHouse.Contract.DataContracts
{
    public class UserRatingDto
    {
        public int Id { get; set; }

        public ApplicationUserDto UserAssessor { get; set; }
        public string UserAssessorId { get; set; }

        public ApplicationUserDto UserEvaluated { get; set; }
        public string UserEvaluatedId { get; set; }

        public string Feedback { get; set; }
    }
}
