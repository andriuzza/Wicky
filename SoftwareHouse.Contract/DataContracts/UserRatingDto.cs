using System;

namespace SoftwareHouse.Contract.DataContracts
{
    public class UserRatingDto
    {
        public int Id { get; set; }

        public ApplicationUserDto UserAssessor { get; set; }
        public Guid UserAssessorId { get; set; }

        public ApplicationUserDto UserEvaluated { get; set; }
        public Guid UserEvaluatedId { get; set; }

        public string Feedback { get; set; }
    }
}
