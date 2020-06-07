using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace CoronaApp.Services
{
    public interface IPatientService
    {
        JwtSecurityToken Authenticate(int id, int password);
        Patient Get(Patient patient);

        void Save(Patient patient);

        Location Add(int id, Location location);

        void Delete(int id, int location);
    }
}
