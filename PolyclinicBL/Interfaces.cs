using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicBL
{
    public interface IMainFormModel
    {
        Entities LoginChecker(string enteredLogin);
    }

    public interface IRegistrationModel
    {
        IEnumerable GetRegions();
        int AddPatient(Patient patient);
        void CheckStreets(string streetName, int regionNumber);
    }

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

    public interface IListRecordingModel
    {
        IEnumerable GetDoctors();
        IEnumerable GetPatientsByCriterion(int docId, string date);
    }

    public interface IReferenceBookModel
    {
        void DrugAdd(string drugName, string description);
        void DiagnosisAdd(string diagnosisName, string description);
        void DrugDescriptionChanged(string drugName, string description);
        void DiagnosisDescriptionChanged(string diagnosisName, string description);
        IEnumerable<PolyclinicBL.Diagnoses> GetDiagnoses();
        IEnumerable<PolyclinicBL.Drug> GetDrugs();
    }

    public interface IMedicalCardModel
    {
        IEnumerable GetDoctors();
        IEnumerable<PolyclinicBL.Diagnoses> GetDiagnoses();
        IEnumerable<PolyclinicBL.Drug> GetDrugs();
        IEnumerable GetTickets(int doctorsId, string visitingDate);
        object GetDoctorById(int doctorsId);
        void UpdateArrivalStatistics(int patientId, int doctorId, bool isPatientArrived, DateTime date);
    }

}
