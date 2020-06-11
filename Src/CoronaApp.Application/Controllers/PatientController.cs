using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoronaApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoronaApp.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class PatientController : ControllerBase
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        private readonly IPatientService PatientService;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public PatientController(IPatientService patientService)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            PatientService = patientService;
        }
        /// <summary>
        /// Gets patient
        /// </summary>
        /// <param name="id">Patient id</param>
        /// <param name="name">Patient name</param>
        /// <param name="password">Patient password</param>
        /// <returns> Specific Patient</returns>

        // GET api/<PatientController>/5

        [AllowAnonymous]
        [HttpGet("{id}/{password}/{name}")]
        public async Task<Patient> Get([Range(100000000,999999999)] int id, int password, string name)
        {
            var claims = new List<Claim>
           {
               new Claim(ClaimTypes.Name, name),
               new Claim(ClaimTypes.Role, id.ToString()),
               new Claim(ClaimTypes.Authentication, password.ToString()),
           };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {

                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),

                IssuedUtc = DateTimeOffset.UtcNow
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
            return await PatientService.Get(new Patient() { Id = id, PasswordPatient = password, PatientName = name });
        }

        //[AllowAnonymous]
        //[HttpGet("Authenticate/{id}/{password}")]

        //public JwtSecurityToken Authenticate(int id,int password)
        //{
        //    return PatientService.Authenticate(id,password);
        //}

        /// <summary>
        /// Add patient
        /// </summary>
        ///<param name="value">Patient to add</param>
        // POST api/<PatientController>

        [AllowAnonymous]
        [HttpPost]
        public void Post([FromBody] Patient value)
        {
            PatientService.Save(value);
        }
        /// <summary>
        /// Add location to patient
        /// </summary>
        /// <param name="token">Authenticate</param>
        /// <param name="location">location to add</param>
        /// <returns> List Patient</returns>
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
        /// <summary>
        /// Delete patient
        /// </summary>
        /// <param name="token">Authenticate</param>
        /// <param name="location">location to add</param>
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







