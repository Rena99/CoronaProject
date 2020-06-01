
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoronaApp.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoronaApp.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository PatientRepository;
        public PatientController(IPatientRepository patientRepository)
        {
            PatientRepository = patientRepository;
        }
        // GET api/<PatientController>/5
        [HttpGet("{id}/{age}")]
        public Patient Get(int id, int age)
        {
            return PatientRepository.Get(new Patient() { Id = id, Age = age });
        }

        // POST api/<PatientController>
        [HttpPost]
        public void Post([FromBody] Patient value)
        {
            PatientRepository.Save(value);
        }

        // PUT api/<PatientController>/5
        [HttpPut("{id}")]
        public Object Put(int id, [FromBody] Location location)
        {
            return PatientRepository.Add(id, location);

        }

        [HttpDelete("{id}")]
        public void Delete(int id, [FromBody] string location)
        {
            PatientRepository.Delete(id, int.Parse(location));

        }


    }
}






