using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PainlessHttp.Serializer.JsonNet;
using SoftwareHouse.Contract.DataContracts;
using SoftwareHouse.Contract.DataContracts.QueryClass;
using SoftwareHouse.Contract.Helpers;
using SoftwareHouse.Contract.Services;
using Newtonsoft.Json;

namespace SoftwareHouse.Web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/Employees")]
    public class EmployeesController : Controller
    {
        private readonly IPersonManagementService _personService;
        private readonly IUrlHelper _uriHelper;

        public EmployeesController(IPersonManagementService personService,
            IUrlHelper uriHelper)
        {
            _personService = personService;
            _uriHelper = uriHelper;
        }

        [HttpGet(Name = "GetEmployees")]
        public async Task<IActionResult> GetEmployees(EmployeesResourceParameter employeesResourceParameter)
        {
                
            var result = await _personService.GetAllUsers(employeesResourceParameter);

            var previousPage = result.HasPrevious
                ? GetEmployeesUri(employeesResourceParameter, ResourceUriType.PreviousPage)
                : null;

            var nextPage = result.HasNext
                ? GetEmployeesUri(employeesResourceParameter, ResourceUriType.NextPage)
                : null;

            var metadata = new
            {
                totalCount = result.TotalCount,
                pageSize = result.PageSize,
                totalPages = result.TotalPages,
                currentPage = result.CurrentPage,
                nextPage = nextPage,
                previousPage = previousPage,
            };
            
            Response.Headers.Add("Pagination", JsonConvert.SerializeObject(metadata));

            if (!result.Any())
            {
                return NoContent();
            }   

            return Ok(result);
        }

        private string GetEmployeesUri(EmployeesResourceParameter employeesResourceParameter,
            ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return _uriHelper.Link("GetEmployees", new
                    {
                        pageNumber = employeesResourceParameter.pageNumber - 1,
                        pageSize = employeesResourceParameter.PageSize
                    });

                case ResourceUriType.NextPage:
                    return _uriHelper.Link("GetEmployees", new
                    {
                        pageNumber = employeesResourceParameter.pageNumber + 1,
                        pageSize = employeesResourceParameter.PageSize
                    });

                default:
                    return _uriHelper.Link("GetEmployees", new
                    {
                        pageNumber = employeesResourceParameter.pageNumber,
                        pageSize = employeesResourceParameter.PageSize
                    });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(string id)
        {
            var result = await _personService.GetUser(id);

            if (!result.IsSuccess)
            {
                return NoContent();
            }

            return Ok(result.ErrorMessage);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddNewEmployee(ApplicationUserDto userDto)
        {
           var user = await _personService.Add(userDto);

            if (user.IsSuccess)
            {
                return Ok(user);
            }

            return BadRequest(user.ErrorMessage);
        }

        [HttpPut(Name = "update")]
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