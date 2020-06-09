
using CoronaApp.Services;
using CoronaApp.Services.Helpers;
using Lucene.Net.Analysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoronaApp.Dal
{
    public class PatientRepository : IPatientRepository
    {
        private readonly CoronaContext context;
        private readonly AppSettings appSettings;
        public PatientRepository(CoronaContext context, IOptions<AppSettings> appSettings)
        {
            this.context = context;
            this.appSettings = appSettings.Value;
        }


        public async Task<Location> Add(int id, Location location)
        {
            Patient p = await context.Patient.Include(p => p.Path).FirstOrDefaultAsync(p => p.Id == id);
            if (p != null)
            {
                p.Path.Add(location);
            }
            context.SaveChanges();
            return p.Path.Last();
        }

        public async void Delete(int id, int location)
        {
            Patient p = await context.Patient.Include(p => p.Path).FirstOrDefaultAsync(p => p.Id == id);
            if (p != null)
            {
                Location l = p.Path.FirstOrDefault(p => p.Id == location);
                p.Path.Remove(l);
            }
            context.SaveChanges();
        }

        public async Task<Patient> Get(Patient patient)
        {
            var user = await context.Patient.Include(p=>p.Path).SingleOrDefaultAsync(x => x.Id == patient.Id 
            && x.PasswordPatient == patient.PasswordPatient);
            if (user == null)
                return null;
            //authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, patient.PatientName),
                    new Claim(ClaimTypes.Role, patient.Id.ToString())

                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityToken token = (JwtSecurityToken)tokenHandler.CreateToken(tokenDescriptor);
            user.Token=tokenHandler.WriteToken(token);
            return user;
            //return (JwtSecurityToken)
            //user.Token = 

            //    //context.SaveChanges();
            //    //return user;
            //}
            //Patient result = new Patient();
            //if (patient.Id != 0 )
            //{
            //    Patient patient1 = context.Patient.Include(p => p.Path).FirstOrDefault(p => p.Id == patient.Id
            //  );
            //    if (patient1 == null)
            //    {
            //        return null;
            //    }
            //    result = new Patient() { Id = patient1.Id, Age = patient1.Age};
            //    foreach (var item in patient1.Path)
            //    {
            //        result.Path.Add(item);
            //    }
            //}
            //if (patient.Age != 0 && patient.Id == 0)
            //{
            //    List<Patient> patients = context.Patient.Include(p => p.Path).Where(p => p.Age == patient.Age).ToList();
            //    result = new Patient() { Id = 0, Age = 0 };
            //    foreach (var item in patients)
            //    {
            //        foreach (var path in item.Path)
            //        {
            //            result.Path.Add(path);
            //        }
            //    }
            //}
            //return result;
        }

        public void Save(Patient patient)
        {
            context.Patient.Add(patient);
            context.SaveChanges();
        }

    }
}
