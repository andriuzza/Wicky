using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SoftwareHouse.Web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/photos")]
    public class UserWorkPhotosController : Controller
    {
        public UserWorkPhotosController()
        {
            
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}