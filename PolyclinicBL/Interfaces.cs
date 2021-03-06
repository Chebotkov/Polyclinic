﻿using System;
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
        void DeleteOldTickets();
        void CheckStatistics(int limitDays);
        List<PrintedTicket> GetOldPrintedTickets();
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
        IEnumerable GetDoctors(int specializationId);
        IEnumerable GetSpecializationsNames();
        IEnumerable GetDoctorsInterval(int specializationId);
        IEnumerable GetDoctorsSchedule(int specializationId);
    }

    public interface IRegistersModel
    {
        IEnumerable GetDoctors();
        IEnumerable GetPatients();
        IEnumerable GetRegions();
        IEnumerable GetSpecializations();
        IEnumerable GetStreetsByRegionsId(int regionsId);
        void AddNewRegion(int regionId, string regionName, string streetName);
        List<string> GetDoctorsByCriterion(int doctorSpecialization);
        List<string> GetDoctorsBySpecialization(int specializationId);
        string GetDoctorInfo(int doctorId);
        List<string> GetRegionInfo(int regionId);
        string GetPatientsFullInfo(int patientId);
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
        string GetSpecializationName(int specializationId);
    }

    public interface IPrintTicketModel
    {
        IEnumerable GetPatients();
        IEnumerable GetTicketsByPatientsId(int patientId);
        PrintedTicket GetFullTicket(int ticketId);
    }

    public interface IRoomsRegisterModel
    {
        List<int> GetRooms(int specializationId);
        IEnumerable GetSpecializations();
        void SetRooms(List<int> rooms, int specializationId);
    }

    public interface INewDoctorModel
    {
        IEnumerable GetSpecializations();
        IEnumerable GetRegions();
        IEnumerable GetRooms(int specializationId);
        void DoctorCreate(PolyclinicBL.Doctor doctor);
        bool IsRoomFree(int room);
    }
}
