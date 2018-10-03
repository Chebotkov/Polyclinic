using PolyclinicBL;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PolyclinicDBManager
{
    public class NewDoctorModel : INewDoctorModel
    {
        private ICRUDMethods iCRUDMethods = new CRUDMethods();

        public NewDoctorModel()
        {

        }

        public NewDoctorModel(ICRUDMethods crudMethods)
        {
            if (crudMethods is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(crudMethods)));
            }

            iCRUDMethods = crudMethods;
        }

        public IEnumerable GetSpecializations()
        {
            return iCRUDMethods.GetSpecializationsNames();
        }

        public IEnumerable GetRegions()
        {
            return iCRUDMethods.GetRegions();
        }

        public IEnumerable GetRooms(int specializationId)
        {
            List<int> Rooms;

            using (var context = new PolyclinicDBContext())
            {
                var query = from r in context.Room.AsNoTracking()
                            where r.SpecId == specializationId
                            select r;

                Rooms = query.ToList<int>();
            }

            return Rooms;
        }

        public void DoctorCreate(PolyclinicBL.Doctor doctor)
        {
            using (var context = new PolyclinicDBContext())
            {
                Doctor newDoctor = new Doctor();
                newDoctor.LastName = doctor.LastName;
                newDoctor.FirstName = doctor.FirstName;
                newDoctor.Patronymic = doctor.Patronymic;
                newDoctor.Specialization = doctor.Specialization;
                newDoctor.Region = doctor.Region;
                newDoctor.Room = doctor.Room;
                newDoctor.Shedule = doctor.Schedule;
                newDoctor.Interval = doctor.Interval;

                context.Doctor.Add(newDoctor);
                context.SaveChanges();
            }
        }

        public bool IsRoomFree(int room)
        {
            bool isFree = true;
            using (var context = new PolyclinicDBContext())
            {
                Doctor doctor = new Doctor();
                var doctors = from d in context.Doctor.AsNoTracking()
                              where d.Room == room
                              select d;

                if (doctors.ToList().Count > 0) isFree = false;
            }

            return isFree;
        }
    }
}
