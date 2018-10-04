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

            if (medicalCardManager is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(medicalCardManager)));
            }

            this.medicalCardModel = medicalCardModel;
            this.iMedicalCardView = iMedicalCardView;
            this.medicalCardManager = medicalCardManager;
            iMedicalCardView.ReferenceBook_Click += IMedicalCardView_ReferenceBook_Click;
            iMedicalCardView.MedicalCardFormLoad += IMedicalCardView_MedicalCardFormLoad;
            iMedicalCardView.ReadMedicalCard += IMedicalCardView_ReadMedicalCard;
            iMedicalCardView.DoctorSelect += IMedicalCardView_DoctorSelect;
            iMedicalCardView.WriteToMedicalCard += IMedicalCardView_WriteToMedicalCard;
            iMedicalCardView.SaveChanges += IMedicalCardView_SaveChanges;
            iMedicalCardView.Report += IMedicalCardView_Report;
        }

        private void IMedicalCardView_Report(object sender, PatientsArrivalEventArgs e)
        {
            medicalCardModel.UpdateArrivalStatistics(e.PatientId, e.DoctorId, e.IsPatientArrived, Editor.ParseToDateTime(e.Date, e.Time));
        }

        private void IMedicalCardView_SaveChanges(object sender, EntityIdEventArgs e)
        {
            PolyclinicDBManager.Doctor doctor = medicalCardModel.GetDoctorById(e.DoctorsId) as PolyclinicDBManager.Doctor;
            iMedicalCardView.SetDoctor(doctor);
            iMedicalCardView.SpecializationName = medicalCardModel.GetSpecializationName(doctor.Specialization);
        }

        private void IMedicalCardView_WriteToMedicalCard(object sender, MedicalCardEventAgs e)
        {
            medicalCardManager.WriteToMedicalCard(e.PatientsId, e.MedicalCardRecords);
        }

        private void IMedicalCardView_DoctorSelect(object sender, TicketEventArgs e)
        {
            iMedicalCardView.SetPatients(medicalCardModel.GetTickets(e.DoctorsId, e.ChosenDate));
        }

        private void IMedicalCardView_ReadMedicalCard(object sender, EntityIdEventArgs e)
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
