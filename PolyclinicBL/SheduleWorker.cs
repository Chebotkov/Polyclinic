using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicBL
{
    public static class SheduleWorker
    {
        public static List<string> GetAvailableTime(int patientsId, int doctorsId, int doctorsInterval, string doctorsShedule, string ChosenDate, List<Ticket> OrderedTickets)
        {
            if (ChosenDate is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(ChosenDate)));
            }

            if (doctorsShedule is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(doctorsShedule)));
            }

            if (OrderedTickets is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(OrderedTickets)));
            }

            List<string> AvailableTime = new List<string>();
            int interval = doctorsInterval;
            string AT = doctorsShedule;
            string TodaysDate = DateTime.Today.ToShortDateString();
            string currentTime;

            GetTime(AT, out int Begin, out int End);

            int m = 0;
            if ((End - Begin) % interval == 0)
                m = (End - Begin) / interval;
            else m = (End - Begin) / interval + 1;

            for (int i = 0; i < m; i++)
            {
                bool exists = false;
                sbyte H = (sbyte)((Begin + interval * i) / 60);
                sbyte M = (sbyte)((Begin + interval * i) % 60);

                if (ChosenDate == TodaysDate && (Convert.ToInt32(TodaysDate.Substring(0, 2)) > H || (Convert.ToInt32(TodaysDate.Substring(0, 2)) == H && Convert.ToInt32(TodaysDate.Substring(3, 2)) > M)))
                    continue;

                currentTime = $"{H.ToString("00")}:{M.ToString("00")}";
                foreach (Ticket ticket in OrderedTickets)
                {
                    if (ticket.VisitingTime == currentTime  && ticket.VisitingDate == ChosenDate && (doctorsId == Convert.ToInt32(ticket.DoctorsId) || patientsId == ticket.PatientsId))
                    {
                        exists = true;
                        break;
                    }

                }

                if (!exists) AvailableTime.Add(currentTime);
            }

            return AvailableTime;
        }

        public static void GetTime(string AT, out int Begin, out int End)
        {
            if (AT is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(AT)));
            }

            Begin = Convert.ToInt32(AT.Substring(0, 2)) * 60 + Convert.ToInt32(AT.Substring(3, 2));
            End = Convert.ToInt32(AT.Substring(6, 2)) * 60 + Convert.ToInt32(AT.Substring(9, 2));
        }
    }
}
