using PolyclinicBL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PolyclinicDBManager
{
    public class ShowStatisticsModel : IShowStatisticsModel
    {
        private ICRUDMethods iCRUDMethods = new CRUDMethods();

        public ShowStatisticsModel()
        {

        }

        public ShowStatisticsModel(ICRUDMethods crudMethods)
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

        public IEnumerable GetDoctorsStatistic(int docId)
        {
            List<VisitorStatistics> list = new List<VisitorStatistics>();
            using (var context = new PolyclinicDBContext())
            {
                IQueryable<VisitorStatistics> query = context.VisitorStatistics.AsNoTracking().Where(v => v.DocId == docId);

                list = query.ToList();
            }

            return list;
        }

        public IEnumerable GetRecordedPatients(int docId, DateTime date)
        {
            List<string> patients = new List<string>();
            using (var context = new PolyclinicDBContext())
            {
                var tickets = from t in context.Ticket
                              where t.DoctorsId == docId
                              join p in context.Patient on t.PatientsId equals p.id
                              select new
                              {
                                  id = p.id,
                                  LastName = p.LastName,
                                  FirstName = p.FirstName,
                                  Patronymic = p.Patronymic,
                                  VisitingTime = t.VisitingDateAndTime
                              };
                foreach (var ticket in tickets)
                {
                    if (ticket.VisitingTime.ToShortDateString() == date.ToShortDateString())
                    {
                        patients.Add(ticket.id + ". " + ticket.LastName + " " + ticket.FirstName + " " + ticket.Patronymic + "; Время приёма: " + ticket.VisitingTime.ToShortTimeString());
                    }
                }
            }

            return patients;
        }
    }
}
