using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoftwareHouse.Contract.DataContracts;
using SoftwareHouse.Contract.Services;

namespace SoftwareHouse.Web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/qualifications")]
    public class QualificationsController : Controller
    {
        private readonly IQualificationsService _qualificationsService;

        public QualificationsController(IQualificationsService qualificationsService)
        {
            _qualificationsService = qualificationsService;
        }

      

        [HttpPost("")]
        public async Task<IActionResult> AddNewEmployee(QualificationDto qualificationDto)
        {
            var user = await _qualificationsService.Add(qualificationDto);

            if (user.IsSuccess)
            {
                return Ok(user);
            }

            return BadRequest(user.ErrorMessage);
        }

        [HttpPut(Name = "updateQualification")]
        public async Task<IActionResult> UpdateQualification(QualificationDto qualificationDto)
        {
            var user = await _qualificationsService.Update(qualificationDto);

            if (user.IsSuccess)
            {
                return Ok(user);
            }

            return BadRequest(user.ErrorMessage);
        }
    }
}