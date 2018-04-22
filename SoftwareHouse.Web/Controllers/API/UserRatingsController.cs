using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftwareHouse.Contract.DataContracts;
using SoftwareHouse.Contract.Services;

namespace SoftwareHouse.Web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/ratings")]
    public class UserRatingsController : Controller
    {
        private readonly IUserRatingsService _ratingsService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRatingsController(IUserRatingsService ratingsService,
            IHttpContextAccessor httpContextAccessor)
        {
            _ratingsService = ratingsService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRating(int id)
        {
            var result = await _ratingsService.GetById(id);

            if (!result.IsSuccess)
            {
                return NoContent();
            }

            return Ok(result.ErrorMessage);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddRating(UserRatingDto ratingDto)
        {
            ratingDto.UserAssessorId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _ratingsService.Add(ratingDto);

            if (user.IsSuccess)
            {
                return Ok(user);
            }

            return BadRequest(user.ErrorMessage);
        }

        [HttpPut(Name = "updateRating")]
        public async Task<IActionResult> UpdateRating(UserRatingDto ratingDto)
        {
            var user = await _ratingsService.Update(ratingDto);

            if (user.IsSuccess)
            {
                return Ok(user);
            }
            return BadRequest(user.ErrorMessage);
        }
    }
}