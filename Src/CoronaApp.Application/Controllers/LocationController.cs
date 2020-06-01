using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoronaApp.Services;

using Microsoft.EntityFrameworkCore;
using CoronaApp.Dal;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoronaApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {

        public ILocationRepository LocationRepository;
        public LocationController(ILocationRepository locationRepository)
        {
            LocationRepository = locationRepository;
        }
        // GET: api/<LocationController>

        [HttpGet]
       [Route("{action}")]
        public IActionResult GetAllList()
        {
            var listPatient = new List<Location>();
            LocationRepository.GetAllList(listPatient);
            return Ok(listPatient);
        }
        [HttpGet]
        [Route("{action}")]
        public IEnumerable<Location> Get([FromQuery] LocationSearch locationSearch=null)
        {
            var listDemo = (List<Location>)LocationRepository.SearchBy(locationSearch);
            return listDemo;
        }
        
       

    }
}
