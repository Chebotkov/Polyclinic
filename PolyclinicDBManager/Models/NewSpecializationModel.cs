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
                var doctors = from d in context.Doctor
                              where d.DocId == doctorsId
                              select d;

                Doctor doctor = doctors.ToList()[0];

                doctor.Shedule = schedule;
                doctor.Interval = interval;
                context.Entry(doctor).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }

        }

        public IEnumerable GetDoctors(int specializationId)
        {
            return iCRUDMethods.GetDoctorsBySpecializationsId(specializationId);
        }

        public IEnumerable GetDoctorsSchedule(int specializationId)
        {
            List<string> Schedules = new List<string>();

            using (var context = new PolyclinicDBContext())
            {
                var schedules = from d in context.DoctorsTimeTable.AsNoTracking()
                                where d.SpecId == specializationId
                                select d;

                foreach (DoctorsTimeTable docTT in schedules)
                {
                    Schedules.Add(docTT.Shedule);
                }
            }

            return Schedules;
        }

        public IEnumerable GetDoctorsInterval(int specializationId)
        {
            List<int> Intervals = new List<int>();

            using (var context = new PolyclinicDBContext())
            {
                var intervals = from d in context.DoctorsTimeTable.AsNoTracking()
                                where d.SpecId == specializationId
                                select d;

                foreach (DoctorsTimeTable docTT in intervals)
                {
                    Intervals.Add(docTT.Interval);
                }
            }

            return Intervals;
        }
    }
}
