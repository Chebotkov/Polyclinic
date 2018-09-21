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
            List<string> AvaileableTime = new List<string>();
            int interval = doctorsInterval;
            string AT = doctorsShedule;
            string TodaysDate = DateTime.Today.ToShortDateString();

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

                foreach (Ticket ticket in OrderedTickets)
                {
                    if (ticket.VisitingTime == $"{H.ToString("00")}:{M.ToString("00")}" && ticket.VisitingDate == ChosenDate && (doctorsId == Convert.ToInt32(ticket.DoctorsId) || patientsId == ticket.PatientsId))
                    {
                        exists = true;
                        break;
                    }

                }

                if (!exists) AvaileableTime.Add($"{H.ToString("00")}:{M.ToString("00")}");
            }

            return AvaileableTime;
        }

        public static void GetTime(string AT, out int Begin, out int End)
        {
            Begin = Convert.ToInt32(AT.Substring(0, 2)) * 60 + Convert.ToInt32(AT.Substring(3, 2));
            End = Convert.ToInt32(AT.Substring(6, 2)) * 60 + Convert.ToInt32(AT.Substring(9, 2));
        }
    }
}
