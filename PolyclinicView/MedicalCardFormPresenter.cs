using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyclinicBL;
using PolyclinicDBManager;

namespace PolyclinicView
{
    public class MedicalCardFormPresenter
    {
        private IMedicalCardView iMedicalCardView;
        private IMedicalCardModel medicalCardModel;
        private IMedicalCardManager medicalCardManager;

        public MedicalCardFormPresenter(IMedicalCardView iMedicalCardView, IMedicalCardModel medicalCardModel, IMedicalCardManager medicalCardManager)
        {
            if (iMedicalCardView is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(iMedicalCardView)));
            }

            if (medicalCardModel is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(medicalCardModel)));
            }

            this.medicalCardModel = medicalCardModel;
            this.iMedicalCardView = iMedicalCardView;
            iMedicalCardView.ReferenceBook_Click += IMedicalCardView_ReferenceBook_Click;
            iMedicalCardView.MedicalCardFormLoad += IMedicalCardView_MedicalCardFormLoad;
            iMedicalCardView.ReadMedicalCard += IMedicalCardView_ReadMedicalCard;
        }

        private void IMedicalCardView_ReadMedicalCard(object sender, DoctorEventArgs e)
        {
            iMedicalCardView.SetPatientsCard(medicalCardManager.ReadMedicalCard(e.DoctorsId));
        }

        private void IMedicalCardView_MedicalCardFormLoad(object sender, EventArgs e)
        {
            iMedicalCardView.SetData(medicalCardModel.GetDoctors(), medicalCardModel.GetDrugs(), medicalCardModel.GetDiagnoses());
        }

        private void IMedicalCardView_ReferenceBook_Click(object sender, EventArgs e)
        {
            ReferenceBookPresenter referenceBookPresenter = new ReferenceBookPresenter(iMedicalCardView.IReferenceBookViewRef, new ReferenceBookModel());
        }
    }
}
