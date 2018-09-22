using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PolyclinicBL;
using PolyclinicDBManager;

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
                throw new ArgumentNullException(String.Format("{0} is null", nameof(iTicketOrderView)));
            }

            if (iTicketOrderFormModel is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(iTicketOrderFormModel)));
            }

            this.iTicketOrderView = iTicketOrderView;
            this.iTicketOrderFormModel = iTicketOrderFormModel;

            iTicketOrderView.TicketOrderFormLoad += ITicketOrderView_TicketOrderFormLoad;
            iTicketOrderView.SpecializationChoise += ITicketOrderView_SpecializationChoise;
            iTicketOrderView.DoctorsSheduleCheck += ITicketOrderView_DoctorsSheduleCheck;
            iTicketOrderView.TicketOrder += ITicketOrderView_TicketOrder;
            iTicketOrderView.NewSpecializationOpen += ITicketOrderView_NewSpecializationOpen;
        }

        private void ITicketOrderView_NewSpecializationOpen(object sender, EventArgs e)
        {
            NewSpecializationPresenter newSpecializationPresenter = new NewSpecializationPresenter(iTicketOrderView.INewSpecializationRef, new NewSpecializationModel());
        }

        private void ITicketOrderView_TicketOrder(object sender, TicketEventArgs e)
        {
            iTicketOrderFormModel.AddTicketToStorage(e.PatientsId, e.DoctorsId, Editor.ParseToDateTime(e.ChosenDate, e.ChosenTime));
        }

        private void ITicketOrderView_DoctorsSheduleCheck(object sender, DoctorEventArgs e)
        {
            iTicketOrderView.SetChosenDoctor(iTicketOrderFormModel.GetDoctorById(e.DoctorsId));
            iTicketOrderView.SetOrderedTickets(iTicketOrderFormModel.GetOrderedTickets());
        }

        private void ITicketOrderView_SpecializationChoise(object sender, PatientsIdAndSpecializationNameEventArgs e)
        {
            int patientsRegion = iTicketOrderFormModel.GetPatientsRegion(e.PatientsId);
            iTicketOrderView.FillDoctorsList(iTicketOrderFormModel.GetDoctorsByCriterion(patientsRegion, e.SpecializationId));
        }

        private void ITicketOrderView_TicketOrderFormLoad(object sender, EventArgs e)
        {
            iTicketOrderView.FillFormWithSP(iTicketOrderFormModel.GetSpecializationsNames(), iTicketOrderFormModel.GetPatientsFullNames());   
        }
    }
}
