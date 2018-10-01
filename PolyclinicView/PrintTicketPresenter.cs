using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyclinicBL;

namespace PolyclinicView
{
    public class PrintTicketPresenter
    {
        private IPrintTicketView iPrintTicketView;
        private IPrintTicketModel printTicketModel;

        public PrintTicketPresenter(IPrintTicketView iPrintTicketView, IPrintTicketModel printTicketModel)
        {
            if (iPrintTicketView is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(iPrintTicketView)));
            }

            if (printTicketModel is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(printTicketModel)));
            }

            this.iPrintTicketView = iPrintTicketView;
            this.printTicketModel = printTicketModel;
            iPrintTicketView.PrintTicketLoad += IPrintTicketView_PrintTicketLoad;
            iPrintTicketView.PatientChoise += IPrintTicketView_PatientChoise;
            iPrintTicketView.ShowTicketOnScreenOpen += IPrintTicketView_ShowTicketOnScreenOpen;
            iPrintTicketView.TicketChoise += IPrintTicketView_TicketChoise;
        }

        private void IPrintTicketView_TicketChoise(object sender, DoctorEventArgs e)
        {
            iPrintTicketView.printedTicket = printTicketModel.GetFullTicket(e.DoctorsId);
        }

        private void IPrintTicketView_ShowTicketOnScreenOpen(object sender, EventArgs e)
        {
            ShowTicketOnScreenPresenter showTicketOnScreenPresenter = new ShowTicketOnScreenPresenter(iPrintTicketView.iShowTicketOnScreen, new MSWordWorker());
        }

        private void IPrintTicketView_PatientChoise(object sender, DoctorEventArgs e)
        {
            iPrintTicketView.SetTickets(printTicketModel.GetTicketsByPatientsId(e.DoctorsId));
        }

        private void IPrintTicketView_PrintTicketLoad(object sender, EventArgs e)
        {
            iPrintTicketView.SetPatients(printTicketModel.GetPatients());
        }
    }
}
