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
        IEnumerable GetPatients();
        IEnumerable GetPatientsFullNames();
        int GetPatientsRegion(int patientsId);
        IEnumerable GetSpecializations();
        IEnumerable GetSpecializationsNames();
        int GetTherapistId();
        List<PolyclinicBL.Ticket> GetOrderedTickets();
    }
}
