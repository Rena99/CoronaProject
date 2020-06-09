using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace CoronaApp.Services
{
    public interface IPatientService
    {
        Task<Patient> Authenticate(int id, int password, string name);
        Task<Patient> Get(Patient patient);

        void Save(Patient patient);

        Task<Location> Add(int id, Location location);

        void Delete(int id, int location);
    }
}
