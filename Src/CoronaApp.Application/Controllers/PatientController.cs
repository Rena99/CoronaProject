using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using CoronaApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
        public async Task<Patient> Get([Range(100000000,999999999)] int id, int password, string name)
        {
            //return PatientService.Get(new Patient() { Id = id, Age = age });
            return await PatientService.Authenticate(id, password, name);
        }
         
        //[AllowAnonymous]
        //[HttpGet("Authenticate/{id}/{password}")]

        //public JwtSecurityToken Authenticate(int id,int password)
        //{
        //    return PatientService.Authenticate(id,password);
        //}
        // POST api/<PatientController>

        [AllowAnonymous]
        [HttpPost]
        public void Post([FromBody] Patient value)
        {
            PatientService.Save(value);
        }

        // PUT api/<PatientController>/5
        [HttpPut("{token}")]
        public async Task<Object> Put(string token, [FromBody] Location location)
        {
            
            //var stream = "[encoded jwt]"; 
            var handler = new JwtSecurityTokenHandler(); 
            var jsonToken = handler.ReadToken(token);
            var tokenS = handler.ReadToken(token) as JwtSecurityToken;
           
            var jti = tokenS.Claims.First(claim => claim.Type == "role"). Value;
            return await PatientService.Add(Int32.Parse(jti), location);
        }

        [HttpDelete("{token}")]
        public void Delete(string token, [FromBody] string location)
        {

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = handler.ReadToken(token) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "role").Value;
            PatientService.Delete(Int32.Parse(jti), int.Parse(location));

        }


    }
}







