using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyclinicBL;

namespace PolyclinicView
{
    public class ShowMedicalCardPresenter
    {
        private IShowMedicalCard iShowMedicalCard;
        private IMedicalCardManager medicalCardManager;

        public ShowMedicalCardPresenter(IShowMedicalCard iShowMedicalCard, IMedicalCardManager medicalCardManager)
        {
            if (iShowMedicalCard is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(iShowMedicalCard)));
            }

            this.iShowMedicalCard = iShowMedicalCard;
            this.medicalCardManager = medicalCardManager;

            iShowMedicalCard.MedicalCardOpen += IShowMedicalCard_MedicalCardOpen;
        }

        private void IShowMedicalCard_MedicalCardOpen(object sender, PolyclinicBL.PatientsIdAndSpecializationNameEventArgs e)
        {
            iShowMedicalCard.OpenMedicalCard(medicalCardManager.ReadMedicalCard(e.PatientsId));
        }
    }
}
