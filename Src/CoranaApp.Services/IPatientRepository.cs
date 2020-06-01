using System;
using System.Collections.Generic;
using System.Text;

namespace CoronaApp.Services
{
    public interface IPatientRepository
    {
        Patient Get(Patient patient);

        void Save(Patient patient);

        Location Add(int id, Location location);

        void Delete(int id, int location);
    }
}
