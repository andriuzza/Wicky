using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MaxMind.GeoIP2;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftwareHouse.DataAccess;
using SoftwareHouse.DataAccess.Models.LocationInformation;

namespace SoftwareHouse.Web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/cities")]
    public class AddCitiesController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public AddCitiesController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public IActionResult AddCities()
        {
         Collection<City> cities = new Collection<City>();

            using (var reader = new StreamReader(_hostingEnvironment.ContentRootPath + "\\GeoLite2-City_20180403" + "\\cities.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    cities.Add(new City
                    {
                        Name = line
                    });
                }
            }
            var _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "In_memory_db")
                .Options;
            try
            {
                using (var database = new ApplicationDbContext(_options))
                {
                    database.Cities.AddRange(cities);
                    database.SaveChanges();
                }

                return Ok(cities);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}