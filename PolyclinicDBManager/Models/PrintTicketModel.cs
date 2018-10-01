using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyclinicBL;

namespace PolyclinicDBManager.Models
{
    public class PrintTicketModel : IPrintTicketModel
    {
        private ICRUDMethods iCRUDMethods = new CRUDMethods();

        public PrintTicketModel()
        {

        }

        public PrintTicketModel(ICRUDMethods crudMethods)
        {
            if (crudMethods is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(crudMethods)));
            }

            iCRUDMethods = crudMethods;
        }

        public IEnumerable GetPatients()
        {
            return iCRUDMethods.GetPatientsFullNames();
        }

        public IEnumerable GetTicketsByPatientsId(int patientId)
        {
            List<string> tickets = new List<string>();

            using (var context = new PolyclinicDBContext())
            {
                var specialTickets = from t in context.Ticket.AsNoTracking()
                                     where t.PatientsId == patientId
                                     select t;

                foreach (Ticket ticket in specialTickets)
                {
                    var doctors = from d in context.Doctor.AsNoTracking()
                                  where d.DocId == ticket.DoctorsId
                                  select d;
                    Doctor doctor = doctors.ToList()[0];

                    tickets.Add(String.Format("{0}. {1} {2} {3} {4} {5} {6}",ticket.id, ticket.VisitingDateAndTime.ToShortDateString(), ticket.VisitingDateAndTime.ToShortTimeString(), doctor.DocId, doctor.LastName, doctor.FirstName, doctor.Patronymic));
                }
            }

            return tickets;
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
