using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PolyclinicBL;

namespace PolyclinicDBManager
{
    public class TicketOrderModel : ITicketOrderFormModel
    {
        public IEnumerable GetDoctors()
        {
            List<Doctor> doctors;
            using (var context = new PolyclinicDBContext())
            {
                IQueryable<Doctor> query = context.Doctor.AsNoTracking();
                doctors = query.ToList();
            }

            return doctors;
        }

        public object GetDoctorById(int id)
        {
            Doctor doctor;
            using (var context = new PolyclinicDBContext())
            {
                var query = context.Doctor.Where(d => d.DocId == id);
                doctor = query.ToList()[0];
            }

            return doctor;
        }

        public IEnumerable GetDoctorsByCriterion(int patientsRegionId, int SpecializationId)
        {
            var chosenDoctors = new List<string>();
            var doctors = GetDoctors();
            foreach (Doctor doctor in doctors)
            {
                if (SpecializationId == doctor.Specialization && doctor.Specialization == GetTherapistId() && patientsRegionId == doctor.Region)
                {
                    chosenDoctors.Add(doctor.DocId + "." + doctor.LastName + " " + doctor.FirstName + " " + doctor.Patronymic);
                }
                else
                {
                    if (doctor.Specialization == SpecializationId && doctor.Specialization != GetTherapistId())
                    {
                        chosenDoctors.Add(doctor.DocId + "." + doctor.LastName + " " + doctor.FirstName + " " + doctor.Patronymic);
                    }
                }
            }

            return chosenDoctors;
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

        public List<PolyclinicBL.Ticket> GetOrderedTickets()
        {
            List<PolyclinicBL.Ticket> orderedTickets = new List<PolyclinicBL.Ticket>();

            using (var context = new PolyclinicDBContext())
            {
                IQueryable<Ticket> query = context.Ticket.AsNoTracking();
                var tickets = query.ToList();
                PolyclinicBL.Ticket ticket;


                foreach (Ticket t in tickets)
                {
                    ticket = new PolyclinicBL.Ticket(t.PatientsId, t.DoctorsId, t.VisitingDateAndTime.ToShortDateString(), t.VisitingDateAndTime.ToShortTimeString());
                    orderedTickets.Add(ticket);
                }
            }

            return orderedTickets;
        }
    }
}
