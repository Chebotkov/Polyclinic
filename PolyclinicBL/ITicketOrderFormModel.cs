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
        IEnumerable GetSpecializationsNames();
        IEnumerable GetPatientsFullNames();
        IEnumerable GetDoctors();
    }
}
