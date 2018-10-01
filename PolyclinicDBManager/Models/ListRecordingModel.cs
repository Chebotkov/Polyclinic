using PolyclinicBL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PolyclinicDBManager
{
    public class ListRecordingModel : IListRecordingModel
    {
        private ICRUDMethods iCRUDMethods = new CRUDMethods();

        public ListRecordingModel()
        {

        }

        public ListRecordingModel(ICRUDMethods crudMethods)
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

        public IEnumerable GetPatientsByCriterion(int docId, string date)
        {
            List<string> recordedPatients = new List<string>();

            using (var context = new PolyclinicDBContext())
            {
                var patients = from t in context.Ticket
                               where t.DoctorsId == docId
                               join p in context.Patient on t.PatientsId equals p.id
                               select new
                               {
                                   Id = p.id,
                                   LastName = p.LastName,
                                   FirstName = p.FirstName,
                                   Patronymic = p.Patronymic,
                                   Date = t.VisitingDateAndTime
                               };

                foreach (var p in patients)
                {
                    if (p.Date.ToShortDateString() == date)
                    {
                        recordedPatients.Add(p.Id + "." + p.LastName + " " + p.FirstName + " " + p.Patronymic + "\t\t" + p.Date.ToShortDateString() + "\t" + p.Date.ToShortTimeString());
                    }
                }
            }

            return recordedPatients;
        }
    }
}
