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
        
        
        [HttpGet("{id}/{age}")]
        public Patient Get([Range(100000000,999999999)] int id, [Range(0, 120)] int age)
        {
            return PatientService.Get(new Patient() { Id = id, Age = age });
        }
         
        [AllowAnonymous]
        [HttpGet("{id}/{password}")]
        public JwtSecurityToken Authenticate(int id,int password)
        {
            return PatientService.Authenticate(id,password);
        }
        // POST api/<PatientController>
        [HttpPost]
        public void Post([FromBody] Patient value)
        {
            //var user = PatientService.Authenticate(value.Id, value.PasswordPatient);
            //if (user == null)
            //    return BadRequest(new { message = "Username or password is incorrect" });
            //return Ok(user);
            PatientService.Save(value);
        }

        //[AllowAnonymous]
        //[HttpPost("authenticate")]
        //public IActionResult Authenticate([FromBody] AuthenticateModel model)
        //{
        //    var user = _userService.Authenticate(model.Username, model.Password);

        //    if (user == null)
        //        return BadRequest(new { message = "Username or password is incorrect" });

        //    return Ok(user);
        //}

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







