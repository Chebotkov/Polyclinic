using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyclinicBL;

namespace PolyclinicDBManager
{
    public class ReferenceBookModel : IReferenceBookModel
    {
        public IEnumerable<PolyclinicBL.Drug> GetDrugs()
        {
            List<PolyclinicBL.Drug> Drugs = new List<PolyclinicBL.Drug>();
            using (var context = new PolyclinicDBContext())
            {
                IQueryable<Drug> query = context.Drug.AsNoTracking();
                var DrugList = query.ToList();

                foreach (Drug drug in DrugList)
                {
                    PolyclinicBL.Drug pdrug = new PolyclinicBL.Drug(drug.DrugName, drug.Description);

                    Drugs.Add(pdrug);
                }
            }

            return Drugs;
        }

        public IEnumerable<PolyclinicBL.Diagnoses> GetDiagnoses()
        {
            List<PolyclinicBL.Diagnoses> Diagnoses = new List<PolyclinicBL.Diagnoses>();
            using (var context = new PolyclinicDBContext())
            {
                IQueryable<Diagnosis> query = context.Diagnosis.AsNoTracking();
                var DiagnosisList = query.ToList();

                foreach (Diagnosis diagnosis in DiagnosisList)
                {
                    PolyclinicBL.Diagnoses pdiagnoses = new PolyclinicBL.Diagnoses(diagnosis.DiagnosisName, diagnosis.Description);

                    Diagnoses.Add(pdiagnoses);
                }
            }

            return Diagnoses;
        }
        
        public void DrugAdd(string drugName, string description)
        {
            if (description is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(description)));
            }

            if (drugName is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(drugName)));
            }

            using (var context = new PolyclinicDBContext())
            {
                IQueryable query = context.Drug;

                Drug drug = new Drug()
                {
                    DrugName = drugName,
                    Description = description,
                };

                context.Drug.Add(drug);
                context.SaveChanges();
            }
        }

        public void DiagnosisAdd(string diagnosisName, string description)
        {
            if (description is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(description)));
            }

            if (diagnosisName is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(diagnosisName)));
            }

            using (var context = new PolyclinicDBContext())
            {
                IQueryable query = context.Diagnosis;

                Diagnosis diagnosis = new Diagnosis()
                {
                    DiagnosisName = diagnosisName,
                    Description = description,
                };

                context.Diagnosis.Add(diagnosis);
                context.SaveChanges();
            }
        }

        public void DrugDescriptionChanged(string drugName, string description)
        {
            if (description is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(description)));
            }

            if (drugName is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(drugName)));
            }

            using (var context = new PolyclinicDBContext())
            {
                IQueryable query = context.Drug;

                var drugs = from d in context.Drug
                            where d.DrugName == drugName
                            select d;

                var list = drugs.ToList();
                list[0].Description = description;

                context.SaveChanges();
            }
        }

        public void DiagnosisDescriptionChanged(string diagnosisName, string description)
        {
            if (description is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(description)));
            }

            if (diagnosisName is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(diagnosisName)));
            }

            using (var context = new PolyclinicDBContext())
            {
                IQueryable query = context.Diagnosis;

                var diagnosis = from d in context.Diagnosis
                                      where d.DiagnosisName == diagnosisName
                                      select d;
                var list = diagnosis.ToList();
                list[0].Description = description;

                context.SaveChanges();
            }
        }
    }
}
