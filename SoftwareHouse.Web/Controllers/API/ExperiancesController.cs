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
    [Route("api/experiances")]
    public class ExperiancesController : Controller
    {
        private readonly IExperiancesService _experianceService;

        public ExperiancesController(IExperiancesService experiancesService)
        {
            _experianceService = experiancesService;
        }

        [HttpGet("/userexperiances/{id}")]
        public async Task<IActionResult> GetUserExperiances(string id)
        {
            var result = await _experianceService.GetUserExperiances(id);

            if (!result.IsSuccess)
            {
                return NoContent();
            }

            return Ok(result.Item);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddExperiance(ExperianceDto userDto)
        {
            var user = await _experianceService.Add(userDto);

            if (user.IsSuccess)
            {
                return Ok(user);
            }

            return BadRequest(user.ErrorMessage);
        }

        [HttpPut(Name = "updateExperiance")]
        public async Task<IActionResult> UpdateExperianc(ExperianceDto userDto)
        {
            var user = await _experianceService.Update(userDto);

            if (user.IsSuccess)
            {
                return Ok(user);
            }

            return BadRequest(user.ErrorMessage);
        }
    }
}