using PolyclinicBL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PolyclinicDBManager
{
    public class MainFormModel : IMainFormModel
    {
        private ICRUDMethods iCRUDMethods = new CRUDMethods();

        public MainFormModel()
        {
        }

        public MainFormModel(ICRUDMethods crudMethods)
        {
            if (crudMethods is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(crudMethods)));
            }

            iCRUDMethods = crudMethods;
        }

        public Entities LoginChecker(string enteredLogin)
        {
            if (enteredLogin is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(enteredLogin)));
            }

            using (PolyclinicDBContext polyclinicDBContext = new PolyclinicDBContext())
            {
                var logins = polyclinicDBContext.Login.AsNoTracking().ToList();

                foreach (Login login in logins)
                {
                    if (login.PolyclinicUser == "Administrator" && enteredLogin == login.UsersLogin)
                        return Entities.Administrator;

                    if (login.PolyclinicUser == "Registrator" && enteredLogin == login.UsersLogin)
                        return Entities.Registrator;

                    if (login.PolyclinicUser == "Doctor" && enteredLogin == login.UsersLogin)
                        return Entities.Doctor;
                }

                return Entities.Empty;
            }
        }

        public void CheckStatistics(int limitDays)
        {
            bool exists = false;

            using (var context = new PolyclinicDBContext())
            {
                var Doctors = context.Doctor;

                foreach (Doctor doctor in Doctors)
                {
                    var Statistics = from s in context.VisitorStatistics
                                     where s.DocId == doctor.DocId
                                     select s;

                    exists = false;
                    foreach (VisitorStatistics day in Statistics)
                    {
                        if (day.Date.ToShortDateString() == DateTime.Today.ToShortDateString())
                        {
                            exists = true;
                            break;
                        }
                    }

                    if (!exists)
                    {
                        VisitorStatistics visitorStatistics = new VisitorStatistics
                        {
                            Date = DateTime.Today,
                            ArrivedPatients = 0,
                            NonArrivedPatients = 0,
                            DocId = doctor.DocId
                        };
                    }

                    //Delete Previous Days
                    int NumberOfDaysInStatistics = Statistics.ToList().Count;
                    VisitorStatistics DeletedDay = null;
                    foreach (VisitorStatistics day in Statistics)
                    {
                        string StatisticsDay = day.Date.ToShortDateString();
                        string TodaysDay = DateTime.Today.ToShortDateString();

                        if (Convert.ToInt32(StatisticsDay.Substring(0, 2)) == Convert.ToInt32(TodaysDay.Substring(0, 2)))
                        {
                            if (Convert.ToInt32(StatisticsDay.Substring(3, 2)) < Convert.ToInt32(TodaysDay.Substring(3, 2)))
                            {
                                if (Convert.ToInt32(StatisticsDay.Substring(6, 4)) <= Convert.ToInt32(TodaysDay.Substring(6, 4)) && StatisticsDay != TodaysDay)
                                {
                                    if (NumberOfDaysInStatistics > limitDays)
                                    {
                                        DeletedDay = day;
                                    }
                                    
                                }
                            }
                        }
                    }

                    if (!(DeletedDay is null))
                    {
                        context.VisitorStatistics.Remove(DeletedDay);
                    }
                }
            }
        }
        
        public List<PrintedTicket> GetOldPrintedTickets()
        {
            List<Ticket> OldTickets = GetOldTickets();
            List<PrintedTicket> OldPrintedTickets = new List<PrintedTicket>();

            using (var context = new PolyclinicDBContext())
            {
                foreach (Ticket ticket in OldTickets)
                {
                    OldPrintedTickets.Add(iCRUDMethods.GetFullTicket(ticket.id));
                }
            }

            return OldPrintedTickets;
        }
        
        private List<Ticket> GetOldTickets()
        {
            DateTime Date = DateTime.Today;
            DateTime PM = Date.AddDays(-31);
            List<Ticket> OldTickets;
            using (var context = new PolyclinicDBContext())
            {
                IQueryable<Ticket> oldTickets = from t in context.Ticket.AsNoTracking()
                                                where t.VisitingDateAndTime.CompareTo(PM) < 0
                                                select t;

                OldTickets = oldTickets.ToList<Ticket>();
            }

            return OldTickets;
        }

        public void DeleteOldTickets()
        {
            List<Ticket> OldTickets = GetOldTickets();

            using (var context = new PolyclinicDBContext())
            {
                foreach (Ticket ticket in OldTickets)
                {
                    context.Ticket.Attach(ticket);
                    context.Ticket.Remove(ticket);
                }

                context.SaveChanges();
            }
        }
    }
}
