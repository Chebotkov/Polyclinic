using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyclinicBL;

namespace PolyclinicDBManager
{
    public class NewSpecializationModel : INewSpecializationModel
    {
        private ICRUDMethods iCRUDMethods = new CRUDMethods();

        public NewSpecializationModel()
        {

        }

        public NewSpecializationModel(ICRUDMethods crudMethods)
        {
            if (crudMethods is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(crudMethods)));
            }

            iCRUDMethods = crudMethods;
        }

        public IEnumerable GetSpecializationsNames()
        {
            return iCRUDMethods.GetSpecializationsNames();
        }

        public void AddNewSpecialization(string specializationName)
        {
            if (specializationName is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(specializationName)));
            }

            using (var context = new PolyclinicDBContext())
            {
                Specialization specialization = new Specialization
                {
                    SpecializationName = specializationName
                };
                context.Specialization.Add(specialization);

                context.SaveChanges();
            }
        }

        public void AddNewSchedule(int specializationId, string schedule, int interval)
        {
            if (schedule is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(schedule)));
            }

            using (var context = new PolyclinicDBContext())
            {
                DoctorsTimeTable doctorsTimeTable = new DoctorsTimeTable()
                {
                    SpecId = specializationId,
                    Shedule = schedule,
                    Interval = interval
                };
                context.DoctorsTimeTable.Add(doctorsTimeTable);

                context.SaveChanges();
            }
        }

        public void AddNewDoctorsSchedule(int doctorsId, string schedule, int interval)
        {
            if (schedule is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(schedule)));
            }

            using (var context = new PolyclinicDBContext())
            {
                IQueryable<Doctor> query = context.Doctor;

                foreach (Doctor doctor in query.ToList())
                {
                    if (doctor.DocId == doctorsId)
                    {
                        doctor.Shedule = schedule;
                        doctor.Interval = interval;
                        break;
                    }
                }

                context.SaveChanges();
            }
        }

        public IEnumerable GetDoctors()
        {
            return iCRUDMethods.GetDoctors();
        }
    }
}
