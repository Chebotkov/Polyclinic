using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicBL
{
    public enum Entities
    {
        Administrator,
        Registrator,
        Doctor,
        Empty
    }

    public class Patient
    {
        public int id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public DateTime Birth { get; set; }
        public bool Gender { get; set; }
        public int Region { get; set; }
        public string Address { get; set; }
        public DateTime? RegistrationDate { get; set; }

        public Patient()
        {

        }

        public Patient(int id, string lastName, string firstName, string patronymic, DateTime birth, bool gender, int region, string address, DateTime registrationDate)
        {
            this.id = id;
            LastName = lastName;
            FirstName = firstName;
            Patronymic = patronymic;
            Birth = birth;
            Gender = gender;
            Region = region;
            Address = address;
            RegistrationDate = registrationDate;
        }
    }

    public class Ticket
    {
        public int PatientsId { get; private set; }
        public int DoctorsId { get; private set; }
        public string VisitingDate { get; private set; }
        public string VisitingTime { get; private set; }

        public Ticket(int patientsId, int doctorsId, string visitingDate, string visitingTime)
        {
            PatientsId = patientsId;
            DoctorsId = doctorsId;
            VisitingDate = visitingDate;
            VisitingTime = visitingTime;
        }
    }

    public class Drug
    {
        public string Medicines { get; private set; }
        public string Description { get; private set; }

        public Drug(string Medicines, string Description)
        {
            this.Medicines = Medicines;
            this.Description = Description;
        }
    }

    public class Diagnoses
    {
        public string Diagnosis { get; private set; }
        public string Description { get; private set; }

        public Diagnoses(string Diagnosis, string Description)
        {
            this.Diagnosis = Diagnosis;
            this.Description = Description;
        }
    }

    public struct PrintedTicket
    {
        public string PatientFullName { get; set; }
        public string DocSpecialization { get; set; }
        public string DocFullName { get; set; }
        public int DocRoom { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
    }

    public struct Doctor
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public int Specialization { get; set; }
        public int? Region { get; set; }
        public int Room { get; set; }
        public string Schedule { get; set; }
        public int Interval { get; set; }
    }
}
