using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoftwareHouse.Contract.Services;

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
        public async Task<ActionResult> GetUsers()
        {
            var result = await _personService.GetAllUsers();

            if (!result.Any())
            {
                return View(null);
            }

            return View(result);
        }
    }
}