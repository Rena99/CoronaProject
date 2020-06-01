using CoronaApp.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoronaApp.Dal
{
    public class PatientRepository : IPatientRepository
    {
        private readonly CoronaContext context;
        public PatientRepository(CoronaContext context)
        {
            this.context = context;
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
            if (patient.Id != 0)
            {
                Patient patient1 = context.Patient.Include(p => p.Path).FirstOrDefault(p => p.Id == patient.Id);
                if (patient1 == null)
                {
                    return null;
                }
                result = new Patient() { Id = patient1.Id, Age = patient1.Age };
                foreach (var item in patient1.Path)
                {
                    result.Path.Add(item);
                }
            }
            if (patient.Age != 0 && patient.Id==0)
            {
                List<Patient> patients = context.Patient.Include(p => p.Path).Where(p => p.Age == patient.Age).ToList();
                result = new Patient() { Id = 0, Age = 0 };
                foreach (var item in patients)
                {
                    foreach (var path in item.Path)
                    {
                        result.Path.Add(path);
                    }
                }
            }
            return result;
        }

        public void Save(Patient patient)
        {
            context.Patient.Add(patient);
            context.SaveChanges();
        }
    }
}
