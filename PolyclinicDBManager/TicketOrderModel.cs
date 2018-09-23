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
        private ICRUDMethods iCRUDMethods = new CRUDMethods();

        public TicketOrderModel()
        {

        }

        public TicketOrderModel(ICRUDMethods crudMethods)
        {
            if (crudMethods is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(crudMethods)));
            }

            iCRUDMethods = crudMethods;
        }

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
            return iCRUDMethods.GetDoctorById(id);
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


        public IEnumerable GetSpecializationsNames()
        {
            return iCRUDMethods.GetSpecializationsNames();
        }

        public IEnumerable GetPatientsFullNames()
        {
            return iCRUDMethods.GetPatientsFullNames();
        }

        public int GetPatientsRegion(int patientsId)
        {
            return iCRUDMethods.GetPatientsRegion(patientsId);
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

        public void AddTicketToStorage(int patientsId, int doctorsId, DateTime visitingDate)
        {
            using (var context = new PolyclinicDBContext())
            {
                IQueryable query = context.Ticket;

                Ticket ticket = new Ticket();
                ticket.PatientsId = patientsId;
                ticket.DoctorsId = doctorsId;
                ticket.VisitingDateAndTime = visitingDate;
                ticket.IsArrived = false;

                context.Ticket.Add(ticket);
                context.SaveChanges();
            }
        }
    }
}
