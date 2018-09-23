using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicDBManager
{
    public interface ICRUDMethods
    {
        int GetPatientsRegion(int patientsId);
        IEnumerable GetRegions();
        IEnumerable GetDoctors();
        IEnumerable GetPatientsFullNames();
        IEnumerable GetSpecializationsNames();
        Doctor GetDoctorById(int id);
    }

    public class CRUDMethods : ICRUDMethods
    {
        public IEnumerable GetRegions()
        {
            List<string> regionsNames = new List<string>();
            using (var context = new PolyclinicDBContext())
            {
                var regions = context.Region.AsNoTracking();

                foreach (var region in regions)
                {
                    regionsNames.Add(region.RegionNumber + "." + region.RegionName);
                }
            }

            return regionsNames;
        }

        public int GetPatientsRegion(int patientsId)
        {
            int patientsRegion = 0;

            using (var context = new PolyclinicDBContext())
            {
                var patient = context.Patient.Where(p => p.id == patientsId);
                patientsRegion = patient.ToList()[0].Region;
            }

            return patientsRegion;
        }
        
        public IEnumerable GetPatients()
        {
            List<Patient> patients;
            using (var context = new PolyclinicDBContext())
            {
                IQueryable<Patient> query = context.Patient.AsNoTracking();
                patients = query.ToList();
            }

            return patients;
        }

        public IEnumerable GetPatientsFullNames()
        {
            var patients = GetPatients();

            List<string> patientsFullNames = new List<string>();

            foreach (Patient patient in patients)
            {
                patientsFullNames.Add(String.Format("{0}.{1} {2} {3}", patient.id, patient.LastName, patient.FirstName, patient.Patronymic));
            }

            return patientsFullNames;
        }

        public IEnumerable GetSpecializations()
        {
            List<Specialization> specializations;
            using (var context = new PolyclinicDBContext())
            {
                IQueryable<Specialization> query = context.Specialization.AsNoTracking();
                specializations = query.ToList();
            }

            return specializations;
        }

        public IEnumerable GetSpecializationsNames()
        {
            var specializations = GetSpecializations();

            List<string> specializationsNames = new List<string>();

            foreach (Specialization specialization in specializations)
            {
                specializationsNames.Add(specialization.id + "." + specialization.SpecializationName);
            }

            return specializationsNames;
        }

        public IEnumerable GetDoctors()
        {
            List<string> doctors = new List<string>();
            using (var context = new PolyclinicDBContext())
            {
                IQueryable<Doctor> query = context.Doctor.AsNoTracking();
                var docs = query.ToList();

                foreach (var doctor in docs)
                {
                    doctors.Add(String.Format("{0}.{1} {2} {3}", doctor.DocId, doctor.LastName, doctor.FirstName, doctor.Patronymic));
                }
            }

            return doctors;
        }

        public Doctor GetDoctorById(int id)
        {
            Doctor doctor;
            using (var context = new PolyclinicDBContext())
            {
                var query = context.Doctor.Where(d => d.DocId == id);
                doctor = query.ToList()[0];
            }

            return doctor;
        }

    }
}
