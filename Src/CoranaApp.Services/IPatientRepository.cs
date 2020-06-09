using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoronaApp.Services
{
    public interface IPatientRepository
    {
        Task<Patient> Get(Patient patient);

        void Save(Patient patient);

        Task<Location> Add(int id, Location location);

        void Delete(int id, int location);
    }
}
