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
            return iCRUDMethods.GetFullTicket(ticketId);
        }
    }
}
