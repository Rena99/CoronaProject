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

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class LocationController : ControllerBase
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
       
        private readonly ILocationService locationService;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public LocationController(ILocationService locationService)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            this.locationService = locationService;
        }
        /// <summary>
        /// Gets list patient when page is loaded 
        /// </summary>
        /// <returns> List Patient</returns>

        // GET: api/<LocationController>
        [HttpGet]
        [Route("{action}")]
        public IActionResult GetAllList()
        {
            var listPatient = locationService.GetAllList();
            return Ok(listPatient);
        }
        /// <summary>
        /// Gets list of patients according to search query
        /// </summary>
        /// <returns>Filtered list of Patient</returns>
        [HttpGet]
        [Route("{action}")]
        public async Task<IEnumerable<Location>> Get([FromQuery] LocationSearch locationSearch=null)
        {
            var listDemo = await locationService.Get(locationSearch);
            return listDemo;
        }
    }
}
