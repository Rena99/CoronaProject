using CoronaApp.Services.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoronaApp.Services
{
    public class PatientService : IPatientService
    {
        int Id = 000000000;
        int Password = 000000000;
        private readonly IPatientRepository PatientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            PatientRepository = patientRepository;
        }
        public async Task<Location> Add(int id, Location location)
        {
            return await PatientRepository.Add(id, location);
        }
        public void Delete(int id, int location)
        {
            PatientRepository.Delete(id, location);
        }

        public async Task<Patient> Get(Patient patient)
        {
            return await PatientRepository.Get(patient);
        }

        public void Save(Patient patient)
        {
            PatientRepository.Save(patient);
        }
        public async Task<Patient> Authenticate(int id, int password, string name)
        {
            return await PatientRepository.Get(new Patient { Id = id, PatientName = name, PasswordPatient = password });
            //if (id == Id && password == Password)
            //{
            //    var tokenHandler = new JwtSecurityTokenHandler();
            //    var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            //    var tokenDescriptor = new SecurityTokenDescriptor
            //    {
            //        Subject = new ClaimsIdentity(new Claim[]
            //        {
            //        new Claim(ClaimTypes.Name, Id.ToString())
            //        }),
            //        Expires = DateTime.UtcNow.AddDays(7),
            //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //    };
            //    return (JwtSecurityToken)tokenHandler.CreateToken(tokenDescriptor);
            //    //user.Token = tokenHandler.WriteToken(token);

            //    //context.SaveChanges();
            //    //return user;
            //}
            //else
            //{
            //    return null;
            //}
            //var user = context.Patient./*Include(p=>p.Path).*/SingleOrDefault(x => x.Id == id && x.PasswordPatient == password);
            // return null if user not found
            //if (user == null)
            //    return null;
            // authentication successful so generate jwt token
            
        }
    }
}
