using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaxMind.GeoIP2;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _accessor;


        public EmployeesController(IPersonManagementService personService,
            IUrlHelper uriHelper,
            IHostingEnvironment hostingEnvironment,
            IHttpContextAccessor accessor)
        {
            _personService = personService;
            _uriHelper = uriHelper;
            _hostingEnvironment = hostingEnvironment;
            _accessor = accessor;
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
             //   city = GetUserLocation()
        };
            
            Response.Headers.Add("Pagination", JsonConvert.SerializeObject(metadata));

            if (!result.Any())
            {
                return NoContent();
            }   

            return Ok(result);
        }

        private string GetUserLocation()
        {
            using (var reader = new DatabaseReader(_hostingEnvironment.ContentRootPath + "\\GeoLite2-City_20180403" + "\\GeoLite2-City.mmdb"))
            {
                // Determine the IP Address of the request
                var ipAddress = _accessor.HttpContext.Connection.RemoteIpAddress;
                var res = ipAddress.ToString();
                // Get the city from the IP Address
                var city = reader.City(ipAddress);
                return city.City.Name.ToString();
            }
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