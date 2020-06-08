using CoronaApp.Dal.Helpers;
using CoronaApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;


namespace CoronaApp.Dal
{
    public class PatientRepository : IPatientRepository
    {
        private readonly CoronaContext context;
        public PatientRepository(CoronaContext context, IOptions<AppSettings> appSettings)
        {
            this.context = context;
            _appSettings = appSettings.Value;
        }

   
        public Location Add(int id, Location location)
        {
            Patient p = context.Patient.Include(p=> p.Path).FirstOrDefault(p => p.Id == id);
            if (p != null)
            {
                p.Path.Add(location);
            }
            context.SaveChanges();
            return p.Path.Last();
        }

        public void Delete(int id, int location)
        {
            Patient p = context.Patient.Include(p => p.Path).FirstOrDefault(p => p.Id == id);
            if (p != null)
            {
                Location l = p.Path.FirstOrDefault(p=>p.Id==location);
                p.Path.Remove(l);
            }
            context.SaveChanges();
        }

        public Patient Get(Patient patient)
        {
            Patient result=new Patient();
            if (patient.Id != 0&& patient.PasswordPatient!=0)
            {
                Patient patient1 = context.Patient.Include(p => p.Path).FirstOrDefault(p => p.Id == patient.Id
                && p.PasswordPatient==patient.PasswordPatient);
                if (patient1 == null)
                {
                    return null;
                }
                result = new Patient() { Id = patient1.Id, Age = patient1.Age,PasswordPatient=patient1.PasswordPatient };
                foreach (var item in patient1.Path)
                {
                    result.Path.Add(item);
                }
            }
            //if (patient.Age != 0 && patient.Id==0)
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
            return result;
        }

        public void Save(Patient patient)
        {
            context.Patient.Add(patient);
            context.SaveChanges();
        }
        private readonly AppSettings _appSettings;

        public Patient Authenticate(int id, int password)
        {
            var user = context.Patient./*Include(p=>p.Path).*/SingleOrDefault(x => x.Id==id && x.PasswordPatient == password);
            // return null if user not found
            if (user == null)
                return null;
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            //context.SaveChanges();
            return user;
        }
    }
}
