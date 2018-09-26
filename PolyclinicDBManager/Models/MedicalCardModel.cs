using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyclinicBL;

namespace PolyclinicDBManager.Models
{
    public class MedicalCardModel : IMedicalCardModel
    {
        private ICRUDMethods iCRUDMethods = new CRUDMethods();

        public MedicalCardModel()
        {

        }

        public MedicalCardModel (ICRUDMethods crudMethods)
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

        public IEnumerable<PolyclinicBL.Drug> GetDrugs()
        {
            return iCRUDMethods.GetDrugs();
        }

        public IEnumerable<PolyclinicBL.Diagnoses> GetDiagnoses()
        {
            return iCRUDMethods.GetDiagnoses();
        }
    }
}
