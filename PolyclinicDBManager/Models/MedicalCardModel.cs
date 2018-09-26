using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyclinicBL;

namespace PolyclinicDBManager.Models
{
    public class MedicalCardModel : IMedicalCardModel
    {
        private ICRUDMethods iCRUDMethods = new CRUDMethods();

        public MedicalCardModel()
        {

        }

        public MedicalCardModel (ICRUDMethods crudMethods)
        {
            if (crudMethods is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(crudMethods)));
            }

            iCRUDMethods = crudMethods;
        }

        public IEnumerable GetDoctors()
        {
            return iCRUDMethods.GetDoctors();
        }

        public IEnumerable<PolyclinicBL.Drug> GetDrugs()
        {
            return iCRUDMethods.GetDrugs();
        }

        public IEnumerable<PolyclinicBL.Diagnoses> GetDiagnoses()
        {
            return iCRUDMethods.GetDiagnoses();
        }

        public IEnumerable GetTickets(int doctorsId, string visitingDate)
        {
            List<string> Patients = new List<string>();

            using (var context = new PolyclinicDBContext())
            {
                IQueryable query = context.Ticket.AsNoTracking();
                var tickets = from t in context.Ticket
                              where t.DoctorsId == doctorsId
                              join p in context.Patient on t.PatientsId equals p.id 
                              select new
                              {
                                  PatientsId = p.id,
                                  Surname = p.LastName,
                                  Name = p.FirstName,
                                  Patronymic = p.Patronymic,
                                  Date = t.VisitingDateAndTime,
                                  IsArrived = t.IsArrived,
                              };


                foreach (var ticket in tickets)
                {
                    if (ticket.Date.ToShortDateString() == visitingDate && !ticket.IsArrived)
                    {
                        Patients.Add(ticket.PatientsId + "." + ticket.Surname + " " + ticket.Name + " " + ticket.Patronymic + " " + ticket.Date.ToShortTimeString());
                    }
                }
            }
            return Patients;
        }

        public object GetDoctorById(int doctorsId)
        {
            return iCRUDMethods.GetDoctorById(doctorsId);
        }

        public void UpdateArrivalStatistics(int patientId, int doctorId, bool isPatientArrived, DateTime date)
        {
            using (var context = new PolyclinicDBContext())
            {
                IQueryable query = context.Ticket.AsNoTracking();
                var tickets = from t in context.Ticket
                              where t.PatientsId == patientId
                              where t.DoctorsId == doctorId
                              where t.IsArrived == false
                              where t.VisitingDateAndTime == date
                              select t;
                Ticket ticket = tickets.ToList()[0];
                ticket.IsArrived = isPatientArrived;
                
                var statistics = from s in context.VisitorStatistics
                                 where s.DocId == doctorId
                                 select s;
                foreach (var statistic in statistics)
                {
                    if (statistic.Date.ToShortDateString() == statistic.Date.ToShortDateString())
                    {
                        statistic.ArrivedPatients += isPatientArrived == true ? 1 : 0;
                        statistic.NonArrivedPatients += isPatientArrived == false ? 1 : 0;
                        break;
                    }
                }
                context.SaveChanges();
            }
        }
    }
}
