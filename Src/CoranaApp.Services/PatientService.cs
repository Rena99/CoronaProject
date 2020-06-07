using System;
using System.Collections.Generic;
using System.Text;

namespace CoronaApp.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository PatientRepository;
        public PatientService(IPatientRepository patientRepository)
        {
            PatientRepository = patientRepository;
        }
        public Location Add(int id, Location location)
        {
            return PatientRepository.Add(id, location);
        }

        public Patient Authenticate(int id, int password)
        {
            return PatientRepository.Authenticate(id, password);
        }

        public void Delete(int id, int location)
        {
            PatientRepository.Delete(id, location);
        }

        public Patient Get(Patient patient)
        {
            return PatientRepository.Get(patient);
        }

        public void Save(Patient patient)
        {
            PatientRepository.Save(patient);
        }
    }
}
