using Microsoft.AspNetCore.Authorization;
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
    public class ClientLocationsController : ControllerBase
    {
        private ApplicationDbContext db;

        public ClientLocationsController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            List<ClientLocation> clientLocations = db.ClientLocations.ToList();
            return Ok(clientLocations);
        }

        [HttpGet]
        [Route("searchbyclientlocationid/{ClientLocationID}")]
        [Authorize]
        public IActionResult GetByClientLocationID(int ClientLocationID)
        {
            ClientLocation clientLocation = db.ClientLocations.Where(temp => temp.ClientLocationID == ClientLocationID).FirstOrDefault();
            if (clientLocation != null)
            {
                return Ok(clientLocation);
            }
            else
                return NoContent();
        }

        [HttpPost]
        [Authorize]
        public ClientLocation Post([FromBody] ClientLocation clientLocation)
        {
            db.ClientLocations.Add(clientLocation);
            db.SaveChanges();

            ClientLocation existingClientLocation = db.ClientLocations.Where(temp => temp.ClientLocationID == clientLocation.ClientLocationID).FirstOrDefault();
            return clientLocation;
        }

        [HttpPut]
        [Authorize]
        public ClientLocation Put([FromBody] ClientLocation project)
        {
            ClientLocation existingClientLocation = db.ClientLocations.Where(temp => temp.ClientLocationID == project.ClientLocationID).FirstOrDefault();
            if (existingClientLocation != null)
            {
                existingClientLocation.ClientLocationName = project.ClientLocationName;
                db.SaveChanges();
                return existingClientLocation;
            }
            else
            {
                return null;
            }
        }

        [HttpDelete]
        [Authorize]
        public int Delete(int ClientLocationID)
        {
            ClientLocation existingClientLocation = db.ClientLocations.Where(temp => temp.ClientLocationID == ClientLocationID).FirstOrDefault();
            if (existingClientLocation != null)
            {
                db.ClientLocations.Remove(existingClientLocation);
                db.SaveChanges();
                return ClientLocationID;
            }
            else
            {
                return -1;
            }
        }
    }
}
