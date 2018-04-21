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
    [Route("api/photos")]
    public class UserWorkPhotosController : Controller
    {
        private readonly IPhotosOfWorkService _photosService;

        public UserWorkPhotosController(IPhotosOfWorkService photosService)
        {
            _photosService = photosService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var result = await _photosService.GetById(id);

            if (!result.IsSuccess)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddNewEmployee(UserWorkPhotoDto photoDto)
        {
            var user = await _photosService.Add(photoDto);

            if (user.IsSuccess)
            {
                return Ok(user);
            }

            return BadRequest(user.ErrorMessage);
        }

        [HttpPut(Name = "updatePhotoUrl")]
        public async Task<IActionResult> UpdatePhotoUrl(UserWorkPhotoDto photoDto)
        {
            var user = await _photosService.Update(photoDto);

            if (user.IsSuccess)
            {
                return Ok(user);
            }

            return BadRequest(user.ErrorMessage);
        }
    }
}