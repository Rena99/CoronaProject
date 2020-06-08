using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using CoronaApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoronaApp.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService PatientService;
        public PatientController(IPatientService patientService)
        {
            PatientService = patientService;
        }
        // GET api/<PatientController>/5

        [AllowAnonymous]
        [HttpGet("{id}/{password}/{name}")]
        public Patient Get([Range(100000000,999999999)] int id, int password, string name)
        {
            //return PatientService.Get(new Patient() { Id = id, Age = age });
            return PatientService.Authenticate(id, password, name);
        }
         
        [AllowAnonymous]
        [HttpGet("Authenticate/{id}/{password}")]
        
        //public JwtSecurityToken Authenticate(int id,int password)
        //{
        //    return PatientService.Authenticate(id,password);
        //}
        // POST api/<PatientController>
        [HttpPost]
        public void Post([FromBody] Patient value)
        {
            
            PatientService.Save(value);
        }

        // PUT api/<PatientController>/5
        [HttpPut("{id}")]
        public Object Put(int id, [FromBody] Location location)
        {
            return PatientService.Add(id, location);
        }

        [HttpDelete("{id}")]
        public void Delete(int id, [FromBody] string location)
        {
            PatientService.Delete(id, int.Parse(location));

        }


    }
}







