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
        //public ILocationRepository ILocationRepository;
        //public LocationController(ILocationRepository locationRepository)
        //{
        //    ILocationRepository = locationRepository;
        //}
        private readonly ILocationService locationService;
            public LocationController(ILocationService locationService)
        {
            this.locationService = locationService;
        }
        // GET: api/<LocationController>
       [HttpGet]
       [Route("{action}")]
        public IActionResult GetAllList()
        {
            var listPatient = new List<Location>();
            ICollection<Location> a;
            //try
            //{

            listPatient = (List<Location>)locationService.GetAllList(listPatient);
            //}
            //catch (Exception e)
            //{

            //    throw;
            //}
            return Ok(listPatient);
        }
        [HttpGet]
        [Route("{action}")]
        public async Task<IEnumerable<Location>> Get([FromQuery] LocationSearch locationSearch=null)
        {
            var listDemo = await locationService.Get(locationSearch);
            return listDemo;
        }
    }
}
