using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyclinicBL;

namespace PolyclinicDBManager
{
    public interface ICRUDMethods
    {
        int GetPatientsRegion(int patientsId);
        IEnumerable GetRegions();
        IEnumerable GetDoctors();
        IEnumerable GetPatientsFullNames();
        IEnumerable GetSpecializationsNames();
        IEnumerable GetSpecializationsNamesOnly();
        IEnumerable GetDoctorsBySpecializationsId(int specializationId);
        Doctor GetDoctorById(int id);
        IEnumerable<PolyclinicBL.Drug> GetDrugs();
        IEnumerable<PolyclinicBL.Diagnoses> GetDiagnoses();
        int GetTherapistId();
        string GetSpecializationName(int specializationId);
        PrintedTicket GetFullTicket(int ticketId);
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

        public IEnumerable GetSpecializationsNamesOnly()
        {
            List<string> specializationsList = new List<string>();
            using (var context = new PolyclinicDBContext())
            {
                IQueryable<Specialization> query = context.Specialization.AsNoTracking();

                foreach (Specialization specialization in query.ToList())
                {
                    specializationsList.Add(specialization.SpecializationName);
                }
            }

            return specializationsList;
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

        public IEnumerable GetDoctorsBySpecializationsId(int specializationId)
        {
            List<string> doctors = new List<string>();
            using (var context = new PolyclinicDBContext())
            {
                var docs = from d in context.Doctor.AsNoTracking()
                           where d.Specialization == specializationId
                           select d;

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


        public IEnumerable<PolyclinicBL.Diagnoses> GetDiagnoses()
        {
            List<PolyclinicBL.Diagnoses> Diagnoses = new List<PolyclinicBL.Diagnoses>();
            using (var context = new PolyclinicDBContext())
            {
                IQueryable<Diagnosis> query = context.Diagnosis.AsNoTracking();
                var DiagnosisList = query.ToList();

                foreach (Diagnosis diagnosis in DiagnosisList)
                {
                    PolyclinicBL.Diagnoses pdiagnoses = new PolyclinicBL.Diagnoses(diagnosis.DiagnosisName, diagnosis.Description);

                    Diagnoses.Add(pdiagnoses);
                }
            }

            return Diagnoses;
        }

        public IEnumerable<PolyclinicBL.Drug> GetDrugs()
        {
            List<PolyclinicBL.Drug> Drugs = new List<PolyclinicBL.Drug>();
            using (var context = new PolyclinicDBContext())
            {
                IQueryable<Drug> query = context.Drug.AsNoTracking();
                var DrugList = query.ToList();

                foreach (Drug drug in DrugList)
                {
                    PolyclinicBL.Drug pdrug = new PolyclinicBL.Drug(drug.DrugName, drug.Description);

                    Drugs.Add(pdrug);
                }
            }

            return Drugs;
        }

        public int GetTherapistId()
        {
            int id = 0;
            using (var context = new PolyclinicDBContext())
            {
                var specialization = context.Specialization.Where(s => s.SpecializationName == "Терапевт");
                id = specialization.ToList()[0].id;
            }

            return id;
        }

        public string GetSpecializationName(int specializationId)
        {
            string specializationName;

            using (var context = new PolyclinicDBContext())
            {
                var specializationNames = from s in context.Specialization.AsNoTracking()
                                          where s.id == specializationId
                                          select s;

                specializationName = specializationNames.ToList()[0].SpecializationName;
            }

            return specializationName;
        }

        public PrintedTicket GetFullTicket(int ticketId)
        {
            PrintedTicket printedTicket;

            using (var context = new PolyclinicDBContext())
            {
                var tickets = from t in context.Ticket.AsNoTracking()
                              where t.id == ticketId
                              join p in context.Patient.AsNoTracking() on t.PatientsId equals p.id
                              join d in context.Doctor.AsNoTracking() on t.DoctorsId equals d.DocId
                              join s in context.Specialization.AsNoTracking() on d.Specialization equals s.id
                              select new
                              {
                                  t.VisitingDateAndTime,
                                  PLN = p.LastName,
                                  PFN = p.FirstName,
                                  PP = p.Patronymic,
                                  DLN = d.LastName,
                                  DFN = d.FirstName,
                                  DP = d.Patronymic,
                                  DocRoom = d.Room,
                                  DocSpecialization = s.SpecializationName
                              };

                var ticket = tickets.ToList()[0];

                printedTicket = new PrintedTicket()
                {
                    Date = ticket.VisitingDateAndTime.ToShortDateString(),
                    Time = ticket.VisitingDateAndTime.ToShortTimeString(),
                    PatientFullName = String.Format("{0} {1} {2}", ticket.PLN, ticket.PFN, ticket.PP),
                    DocFullName = String.Format("{0} {1} {2}", ticket.DLN, ticket.DFN, ticket.DP),
                    DocSpecialization = ticket.DocSpecialization,
                    DocRoom = ticket.DocRoom,
                };
            }

            return printedTicket;
        }
    }
}
