using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareHouse.DataAccess.Models.UserInformation
{
    public class UserRating
    {
        public int Id  { get; set; }

        [ForeignKey("UserAssessorId")]
        [InverseProperty("UserAssessorUserRatings")]
        public ApplicationUser UserAssessor { get; set; }
        public string UserAssessorId { get; set; }

        [ForeignKey("UserEvaluatedId")]
        public ApplicationUser UserEvaluated { get; set; }
        public string UserEvaluatedId { get; set; }

        public string Feedback { get; set; }
    }
}