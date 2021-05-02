using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerAPI.Identity;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private ApplicationDbContext db;

        public CountriesController(ApplicationDbContext dbContext)
        {
            this.db = dbContext;
        }

        [HttpGet]
        [Route("GetCountries")]
        public IActionResult GetCountries()
        {
            List<Country> countries = this.db.Countries.OrderBy(temp => temp.CountryName).ToList();
            return Ok(countries);
        }

    }
}
