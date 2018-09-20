using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PolyclinicBL;

namespace PolyclinicView
{
    public class RegistrationPresenter
    {
        private IRegistrationView iRegistrationView;
        private IRegistrationModel iRegistrationModel;
        private IMedicalCardManager iMedicalCardManager;

        public RegistrationPresenter(IRegistrationView iRegistrationView, IRegistrationModel iRegistrationModel, IMedicalCardManager iMedicalCardManager)
        {
            if (iRegistrationView is null)
            {
                throw new ArgumentNullException("{0} is null", nameof(iRegistrationView));
            }

            if (iRegistrationModel is null)
            {
                throw new ArgumentNullException("{0} is null", nameof(iRegistrationModel));
            }

            if (iMedicalCardManager is null)
            {
                throw new ArgumentNullException("{0} is null", nameof(iMedicalCardManager));
            }

            this.iRegistrationView = iRegistrationView;
            this.iRegistrationModel = iRegistrationModel;
            this.iMedicalCardManager = iMedicalCardManager;

            iRegistrationView.RegistrationFormLoad += IRegistrationView_RegistrationFormLoad;
            iRegistrationView.SaveChanges += IRegistrationView_SaveChanges;
        }

        private void IRegistrationView_SaveChanges(object sender, EventArgs e)
        {
            //Adding the patient to the DataBase or somewhere
            PolyclinicBL.Patient patient = iRegistrationView.GetNewPatient();
            int patientsId = iRegistrationModel.AddPatient(patient);
            patient.id = patientsId;

            //Creating patients medical card
            iMedicalCardManager.CreateMedicalCard(patient);

            //Cheking streets register
            iRegistrationModel.CheckStreets(Editor.GetStreet(patient.Address), patient.Region);
        }

        private void IRegistrationView_RegistrationFormLoad(object sender, EventArgs e)
        {
            iRegistrationView.SetRegions(iRegistrationModel.GetRegions());
        }
    }
}
