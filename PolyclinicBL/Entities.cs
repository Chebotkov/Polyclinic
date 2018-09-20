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
}
