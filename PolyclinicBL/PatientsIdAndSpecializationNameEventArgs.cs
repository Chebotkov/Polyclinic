using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicBL
{
    public class PatientsIdAndSpecializationNameEventArgs : EventArgs
    {
        public int PatientsId { get; private set; }
        public int SpecializationId { get; private set; }
        public PatientsIdAndSpecializationNameEventArgs(int patientsId, int specializationId)
        {
            PatientsId = patientsId;
            SpecializationId = specializationId;
        }
    }
}
