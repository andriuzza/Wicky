using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoftwareHouse.Contract.Interfaces;
using SoftwareHouse.Contract.Services;
using SoftwareHouse.Web.ViewModels.Search;

namespace SoftwareHouse.Web.Controllers
{
    public class HomeController : Controller
    {
        private IPersonManagementService _personService;

        public HomeController(IPersonManagementService personService)
        {
            _personService = personService;
        }
        public async Task<ActionResult> Index()
        {
         
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(SearchViewModel searchField)
        {
           // var result = await _personService.GetAllUsers();

            return RedirectToAction("GetUsers", "Users");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
