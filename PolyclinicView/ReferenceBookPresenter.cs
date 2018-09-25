using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyclinicBL;

namespace PolyclinicView
{
    public class ReferenceBookPresenter
    {
        private IReferenceBookView referenceBookView;
        private IReferenceBookModel referenceBookModel;

        public ReferenceBookPresenter(IReferenceBookView referenceBookView, IReferenceBookModel referenceBookModel)
        {
            if (referenceBookView is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(referenceBookView)));
            }

            if (referenceBookModel is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(referenceBookModel)));
            }

            this.referenceBookView = referenceBookView;
            this.referenceBookModel = referenceBookModel;

            referenceBookView.ReferenceBookLoad += ReferenceBookView_ReferenceBookLoad;
            referenceBookView.DrugAdd += ReferenceBookView_DrugAdd;
            referenceBookView.DiagnosisAdd += ReferenceBookView_DiagnosisAdd;
            referenceBookView.DrugDescriptionChange += ReferenceBookView_DrugDescriptionChange;
            referenceBookView.DiagnosisDescriptionChange += ReferenceBookView_DiagnosisDescriptionChange;
        }

        private void ReferenceBookView_DiagnosisDescriptionChange(object sender, DrugOrDiagnosisEventArgs e)
        {
            referenceBookModel.DiagnosisDescriptionChanged(e.Name, e.Description);
        }

        private void ReferenceBookView_DrugDescriptionChange(object sender, DrugOrDiagnosisEventArgs e)
        {
            referenceBookModel.DrugDescriptionChanged(e.Name, e.Description);
        }

        private void ReferenceBookView_DiagnosisAdd(object sender, DrugOrDiagnosisEventArgs e)
        {
            referenceBookModel.DiagnosisAdd(e.Name, e.Description);
        }

        private void ReferenceBookView_DrugAdd(object sender, DrugOrDiagnosisEventArgs e)
        {
            referenceBookModel.DrugAdd(e.Name, e.Description);
        }

        private void ReferenceBookView_ReferenceBookLoad(object sender, EventArgs e)
        {
            this.referenceBookView.SetData(referenceBookModel.GetDrugs(), referenceBookModel.GetDiagnoses());
        }
    }
}
