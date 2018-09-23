using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicBL
{
    public interface ITicketOrderFormModel
    {
        IEnumerable GetDoctors();
        IEnumerable GetDoctorsByCriterion(int patientsRegionId, int SpecializationId);
        object GetDoctorById(int id);
        IEnumerable GetPatientsFullNames();
        int GetPatientsRegion(int patientsId);
        IEnumerable GetSpecializationsNames();
        int GetTherapistId();
        List<PolyclinicBL.Ticket> GetOrderedTickets();
        void AddTicketToStorage(int patientsId, int doctorsId, DateTime visitingDate);
    }

    public interface INewSpecializationModel
    {
        void AddNewSpecialization(string specializationName);
        void AddNewSchedule(int specializationId, string schedule, int interval);
        void AddNewDoctorsSchedule(int doctorsId, string schedule, int interval);
        IEnumerable GetDoctors();
        IEnumerable GetSpecializationsNames();
    }

    public interface IRegistersModel
    {
        IEnumerable GetDoctors();
        IEnumerable GetPatients();
        IEnumerable GetRegions();
        IEnumerable GetSpecializations();
        IEnumerable GetStreetsByRegionsId(int regionsId);
        void AddNewRegion(int regionId, string regionName);
        void AddNewStreet(int regionsId, string street);
    }

    public interface IShowStatisticsModel
    {
        IEnumerable GetDoctors();
        IEnumerable GetDoctorsStatistic(int docId);
        IEnumerable GetRecordedPatients(int docId, DateTime date);
    }
}
