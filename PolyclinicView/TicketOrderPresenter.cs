using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PolyclinicBL;

namespace PolyclinicView
{
    public class TicketOrderPresenter
    {
        private ITicketOrderView iTicketOrderView;
        private ITicketOrderFormModel iTicketOrderFormModel;

        public TicketOrderPresenter(ITicketOrderView iTicketOrderView, ITicketOrderFormModel iTicketOrderFormModel)
        {
            if (iTicketOrderView is null)
            {
                throw new ArgumentNullException("{0} is null", nameof(iTicketOrderView));
            }

            if (iTicketOrderFormModel is null)
            {
                throw new ArgumentNullException("{0} is null", nameof(iTicketOrderFormModel));
            }

            this.iTicketOrderView = iTicketOrderView;
            this.iTicketOrderFormModel = iTicketOrderFormModel;

            iTicketOrderView.TicketOrderFormLoad += ITicketOrderView_TicketOrderFormLoad;
            iTicketOrderView.SpecializationChoise += ITicketOrderView_SpecializationChoise;
            iTicketOrderView.DoctorsSheduleCheck += ITicketOrderView_DoctorsSheduleCheck;
        }

        private void ITicketOrderView_DoctorsSheduleCheck(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ITicketOrderView_SpecializationChoise(object sender, EventArgs e)
        {
            PatientsIdAndSpecializationNameEventArgs piasne = (PatientsIdAndSpecializationNameEventArgs)e;
            int patientsRegion = iTicketOrderFormModel.GetPatientsRegion(piasne.PatientsId);
            iTicketOrderView.FillDoctorsList(iTicketOrderFormModel.GetDoctorsByCriterion(patientsRegion, piasne.SpecializationId));
        }

        private void ITicketOrderView_TicketOrderFormLoad(object sender, EventArgs e)
        {
            iTicketOrderView.FillFormWithSP(iTicketOrderFormModel.GetSpecializationsNames(), iTicketOrderFormModel.GetPatientsFullNames());   
        }
    }
}
