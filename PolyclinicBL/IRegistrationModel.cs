using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicBL
{
    public interface IRegistrationModel
    {
        IEnumerable GetRegions();
        int AddPatient(Patient patient);
        void CheckStreets(string streetName, int regionNumber);
    }
}
