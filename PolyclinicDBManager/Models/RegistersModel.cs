using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyclinicBL;

namespace PolyclinicDBManager
{
    public class RegistersModel : IRegistersModel
    {
        private ICRUDMethods iCRUDMethods = new CRUDMethods();

        public RegistersModel()
        {

        }

        public RegistersModel(ICRUDMethods crudMethods)
        {
            if (crudMethods is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(crudMethods)));
            }

            iCRUDMethods = crudMethods;
        }

        public IEnumerable GetDoctors()
        {
            return iCRUDMethods.GetDoctors();
        }

        public IEnumerable GetPatients()
        {
            return iCRUDMethods.GetPatientsFullNames();
        }

        public IEnumerable GetRegions()
        {
            return iCRUDMethods.GetRegions();
        }

        public IEnumerable GetSpecializations()
        {
            return iCRUDMethods.GetSpecializationsNames();
        }

        public IEnumerable GetStreetsByRegionsId(int regionsId)
        {
            List<string> streets = new List<string>();
            using (var context = new PolyclinicDBContext())
            {
                IQueryable<Street> query = context.Street.AsNoTracking();

                foreach (Street street in query.ToList())
                {
                    if (street.RegionNumber == regionsId)
                    {
                        streets.Add(street.StreetName);
                    }
                }
            }

            return streets;
        }

        public void AddNewRegion(int regionId, string regionName, string streetName)
        {
            using (var context = new PolyclinicDBContext())
            {
                var regions = from r in context.Region
                              where r.RegionNumber == regionId
                              select r;

                if (regions.ToList().Count == 0)
                {
                    Region region = new Region
                    {
                        RegionNumber = regionId,
                        RegionName = regionName
                    };
                    context.Region.Add(region);
                }

                var streets = from s in context.Street
                              where s.RegionNumber == regionId
                              where s.StreetName == streetName
                              select s;

                if (streets.ToList().Count == 0)
                {
                    Street street = new Street
                    {
                        RegionNumber = regionId,
                        StreetName = streetName,
                    };
                    context.Street.Add(street);
                }
                
                context.SaveChanges();
            }
        }

        public List<string> GetDoctorsByCriterion(int doctorSpecialization)
        {
            List<string> Doctors = new List<string>();

            using (var context = new PolyclinicDBContext())
            {
                int therapistId = iCRUDMethods.GetTherapistId();
                var selectedDoctors = from d in context.Doctor.AsNoTracking()
                                      where d.Specialization == doctorSpecialization
                                      select d;

                foreach (Doctor doctor in selectedDoctors)
                {
                    if (doctor.Specialization != therapistId)
                    {
                        Doctors.Add(String.Format("{0}.{1} {2} {3}; {4}; № кабинета {5}; Время приёма: {6}", doctor.DocId, doctor.LastName, doctor.FirstName, doctor.Patronymic, iCRUDMethods.GetSpecializationName(doctorSpecialization), doctor.Room, doctor.Shedule));
                    }
                    else
                    {
                        Doctors.Add(String.Format("{0}.{1} {2} {3}; {4}; № кабинета {5}; № участка: {6}; Время приёма: {7}", doctor.DocId, doctor.LastName, doctor.FirstName, doctor.Patronymic, iCRUDMethods.GetSpecializationName(doctorSpecialization), doctor.Room, GetRegionName(doctor.Region.Value), doctor.Shedule));
                    }
                }
            }

            return Doctors;
        }

        private string GetRegionName(int regionId)
        {
            string regionName;
            using (var context = new PolyclinicDBContext())
            {
                var regions = from r in context.Region.AsNoTracking()
                              where r.RegionNumber == regionId
                              select r;

                regionName = regions.ToList()[0].RegionName;
            }

            return regionName;
        }

        public string GetPatientsFullInfo(int patientId)
        {
            string patientInfo;
            using (var context = new PolyclinicDBContext())
            {
                var patients = from p in context.Patient.AsNoTracking()
                               where p.id == patientId
                               select p;
                Patient patient = patients.ToList()[0];
                patientInfo = String.Format("ФИО: {0} {1} {2} {8}Пол: {3}{8}Дата рождения: {4}{8}№ участка: {5}, Адрес: {6}{8}Дата регистрации: {7}", patient.LastName, patient.FirstName, patient.Patronymic, patient.Gender == false ? "Женский" : "Мужской", patient.Birth.ToShortDateString(), patient.Region, patient.Address, patient.RegistrationDate.Value.ToShortDateString(), Environment.NewLine);
            }

            return patientInfo;
        }

        public List<string> GetDoctorsBySpecialization(int specializationId)
        {
            List<string> Doctors = new List<string>();

            using (var context = new PolyclinicDBContext())
            {
                int therapistId = iCRUDMethods.GetTherapistId();

                var chosenDoctors = from d in context.Doctor.AsNoTracking()
                                    where d.Specialization == specializationId
                                    select d;

                foreach (Doctor doctor in chosenDoctors)
                {
                    if (doctor.Specialization == therapistId)
                    {
                        Doctors.Add(String.Format("{0} {1} {2}; {3}; № кабинета: {4}; Время приёма: {5}; Обслуживаемый участок: {6}", doctor.LastName, doctor.FirstName, doctor.Patronymic, iCRUDMethods.GetSpecializationName(doctor.Specialization), doctor.Room, doctor.Shedule, GetRegionName(doctor.Region.Value)));
                    }
                    else
                    {
                        Doctors.Add(String.Format("{0} {1} {2}; {3}; № кабинета: {4}; Время приёма: {5}", doctor.LastName, doctor.FirstName, doctor.Patronymic, iCRUDMethods.GetSpecializationName(doctor.Specialization), doctor.Room, doctor.Shedule));
                    }
                }
            }

            return Doctors;
        }

        public string GetDoctorInfo(int doctorId)
        {
            using (var context = new PolyclinicDBContext())
            {
                int therapistId = iCRUDMethods.GetTherapistId();
                var selectedDoctors = from d in context.Doctor.AsNoTracking()
                                      where d.DocId == doctorId
                                      select d;
                Doctor doctor = selectedDoctors.ToList()[0];

                if (doctor.Specialization != therapistId)
                {
                    return (String.Format("{0}.{1} {2} {3}; {4}; № кабинета {5}; Время приёма: {6}", doctor.DocId, doctor.LastName, doctor.FirstName, doctor.Patronymic, iCRUDMethods.GetSpecializationName(doctor.Specialization), doctor.Room, doctor.Shedule));
                }
                else
                {
                    return (String.Format("{0}.{1} {2} {3}; {4}; № кабинета {5}; № участка: {6}; Время приёма: {7}", doctor.DocId, doctor.LastName, doctor.FirstName, doctor.Patronymic, iCRUDMethods.GetSpecializationName(doctor.Specialization), doctor.Room, GetRegionName(doctor.Region.Value), doctor.Shedule));
                }
            }
        }

        public List<string> GetRegionInfo(int regionId)
        {
            List<string> RegionInfo = new List<string>();

            using (var context = new PolyclinicDBContext())
            {
                int therapistId = iCRUDMethods.GetTherapistId();

                var theraphists = from d in context.Doctor.AsNoTracking()
                                  where d.Specialization == therapistId
                                  select d;

                int i = 0;
                foreach (Doctor doctor in theraphists)
                {
                    if (doctor.Region.Value == regionId)
                    {
                        if (i == 0)
                        {
                            RegionInfo.Add("Врачи-терапевты, закреплённые за этим учатском:");
                            i++;
                        }
                        RegionInfo.Add(String.Format("{0} {1} {2}; № кабинета: {3}", doctor.LastName, doctor.FirstName, doctor.Patronymic, doctor.Room));
                    }
                }

                if (i == 0) RegionInfo.Add("За этим учатском нет закреплённых терапевтов");

                var addresses = from s in context.Street.AsNoTracking()
                                where s.RegionNumber == regionId
                                select s;

                RegionInfo.Add("Адреса при участке:");

                foreach (Street street in addresses)
                {
                    RegionInfo.Add(String.Format("Участок №{0}, {1}", street.RegionNumber, street.StreetName));
                }
            }

            return RegionInfo;
        }
    }
}
