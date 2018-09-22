using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicBL
{
    public class PatientsIdAndSpecializationNameEventArgs : EventArgs
    {
        public int PatientsId { get; private set; }
        public int SpecializationId { get; private set; }
        public PatientsIdAndSpecializationNameEventArgs(int patientsId, int specializationId)
        {
            PatientsId = patientsId;
            SpecializationId = specializationId;
        }
    }

    public class DoctorEventArgs : EventArgs
    {
        public int DoctorsId { get; private set; }
        public DoctorEventArgs(int doctorsId)
        {
            DoctorsId = doctorsId;
        }
    }

    public class TicketEventArgs : EventArgs
    {
        public int PatientsId { get; private set; }
        public int DoctorsId { get; private set; }
        public string ChosenDate { get; private set; }
        public string ChosenTime { get; private set; }
        
        public TicketEventArgs(int patientsId, int doctorsId, string chosenDate, string chosenTime)
        {
            PatientsId = patientsId;
            DoctorsId = doctorsId;
            ChosenDate = chosenDate;
            ChosenTime = chosenTime;
        }
    }
}
