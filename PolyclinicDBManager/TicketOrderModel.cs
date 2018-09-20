using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PolyclinicBL;

namespace PolyclinicDBManager
{
    public class TicketOrderModel : ITicketOrderFormModel
    {
        public IEnumerable GetDoctors()
        {
            throw new NotImplementedException();
        }


        public List<Specialization> GetSpecializations()
        {
            List<Specialization> specializations;
            using (var context = new PolyclinicDBContext())
            {
                specializations = context.Specialization.AsNoTracking().ToList();
            }

            return specializations;
        }

        public IEnumerable GetSpecializationsNames()
        {
            var specializations = GetSpecializations();

            List<string> specializationsNames = new List<string>();

            foreach (Specialization specialization in specializations)
            {
                specializationsNames.Add(specialization.id + ". " + specialization.SpecializationName);
            }

            return specializationsNames;
        }

        public IEnumerable GetPatientsFullNames()
        {
            MessageBox.Show("zz");
            List<string> patientsFullNames = new List<string>();
            using (var context = new PolyclinicDBContext())
            {
                var patients = context.Patient.AsNoTracking().ToList();

                foreach (Patient patient in patients)
                {
                    patientsFullNames.Add(String.Format("{0}. {1} {2} {3}", patient.id, patient.LastName, patient.FirstName, patient.Patronymic));
                }
            }
            return patientsFullNames;
        }
    }
}
