using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoftwareHouse.Contract.DataContracts;

namespace SoftwareHouse.DataAccess.Models.UserInformation
{
    public class UserRating
    {
        public int Id  { get; set; }

      
        public string UserAssessorId { get; set; }

        public ApplicationUser UserAssessor { get; set; }

        public StarNumberType StarType { get; set; }
 
        public string UserEvaluatedId { get; set; }

        public ApplicationUser UserEvaluated { get; set; }

        public string Feedback { get; set; }
    }
}