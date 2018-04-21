using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoftwareHouse.Contract.DataContracts.QueryClass;
using SoftwareHouse.Contract.Services;
using X.PagedList;

namespace SoftwareHouse.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IPersonManagementService _personService;

        public UsersController(IPersonManagementService personService)
        {
            _personService = personService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GetUsers(int? page)
        {
           /* var result = await _personService.GetAllUsers(new EmployeesResourceParameter());

            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfProducts = result.ToPagedList(pageNumber, 25); // will only contain 25 products max because of the pageSize



            ViewBag.OnePageOfProducts = onePageOfProducts;*/

            return View();
        }
    }
}