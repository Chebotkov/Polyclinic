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

    public class SpecializationEventArgs : EventArgs
    {
        public string SpecializationName { get; private set; }

        public SpecializationEventArgs(string specializationName)
        {
            SpecializationName = specializationName;
        }
    }

    public class ScheduleEventArgs : EventArgs
    {
        public int SpecializationId { get; private set; }
        public string Schedule { get; private set; }
        public int Interval { get; private set; }

        public ScheduleEventArgs(int specializationId, string schedule, int interval)
        {
            SpecializationId = specializationId;
            Schedule = schedule;
            Interval = interval;
        }
    }

    public class DoctorsTimeEventArgs : EventArgs
    {
        public int DoctorsId { get; private set; }
        public string Schedule { get; private set; }
        public int Interval { get; private set; }
        public DoctorsTimeEventArgs(int doctorsId, string schedule, int interval)
        {
            DoctorsId = doctorsId;
            Schedule = schedule;
            Interval = interval;
        }
    }

    public class StreetsEventHandler : EventArgs
    {
        public int RegionsId { get; private set; }
        public string StreetName { get; private set; }

        public StreetsEventHandler(int regionsId)
        {
            RegionsId = regionsId;
        }

        public StreetsEventHandler (int regionsId, string streetName)
        {
            RegionsId = regionsId;
            StreetName = streetName;
        }
    }

    public class RegionsEventHandler : EventArgs
    {
        public int RegionNumber { get; private set; }
        public string RegionName { get; private set; } 

        public RegionsEventHandler(int regionNumber, string regionName)
        {
            RegionNumber = regionNumber;
            RegionName = RegionName;
        }
    }

    public class DateChangedEventArgs : EventArgs
    {
        public DateTime Date { get; private set; }
        public int DocId { get; private set; }

        public DateChangedEventArgs(int docId, DateTime date)
        {
            DocId = docId;
            Date = date;
        }
    }

    public class DateSelectedEventArgs : EventArgs
    {
        public int DocId { get; private set; }
        public string Date { get; private set; }

        public DateSelectedEventArgs(int docid, string date)
        {
            DocId = docid;
            Date = date;
        }
    }

    public class DrugOrDiagnosisEventArgs : EventArgs
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public DrugOrDiagnosisEventArgs(string Name, string Description)
        {
            this.Name = Name;
            this.Description = Description;
        }
    }

    public class MedicalCardEventAgs : EventArgs
    {
        public byte[] MedicalCardRecords { get; private set; }
        public int PatientsId { get; private set; }

        public MedicalCardEventAgs(int patientsId, byte[] medicalCardRecords)
        {
            PatientsId = patientsId;
            MedicalCardRecords = medicalCardRecords;
        }
    }

    public class PatientsArrivalEventArgs : EventArgs
    {
        public int PatientId { get; private set; }
        public bool IsPatientArrived { get; private set; }
        public int DoctorId { get; private set; }
        public string Date { get; private set; }
        public string Time { get; private set; }

        public PatientsArrivalEventArgs(int patientId, int doctorId, bool isPatientArrived, string date, string time)
        {
            PatientId = patientId;
            IsPatientArrived = isPatientArrived;
            DoctorId = doctorId;
            Date = date;
            Time = time;
        }
    }
}
