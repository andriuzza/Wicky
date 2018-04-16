using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftwareHouse.Contract.DataContracts;
using SoftwareHouse.Contract.Services;

namespace SoftwareHouse.Web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/Employees")]
    public class EmployeesController : Controller
    {
        private readonly IPersonManagementService _personService;

        public EmployeesController(IPersonManagementService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var result = await _personService.GetAllUsers();

            if (!result.Any())
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployee(string id)
        {
            var result = await _personService.GetUser(id);

            if (!result.IsSuccess)
            {
                return NoContent();
            }

            return Ok(result.ErrorMessage);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewEmployee(ApplicationUserDto userDto)
        {
           var user = await _personService.Add(userDto);

            if (user.IsSuccess)
            {
                return Ok(user);
            }

            return BadRequest(user.ErrorMessage);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(ApplicationUserDto userDto)
        {
            var user = await _personService.Update(userDto);

            if (user.IsSuccess)
            {
                return Ok(user);
            }

            return BadRequest(user.ErrorMessage);
        }

       

    }
}