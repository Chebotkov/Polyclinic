using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyclinicBL;

namespace PolyclinicView
{
    public class MainFormPresenter
    {
        private readonly IMainView view;
        private readonly IMessageService messageService;
        private readonly IMainFormModel mainFormModel;
        private readonly ITicketCreator ticketCreator;

        public MainFormPresenter(IMainView view, IMessageService messageService, IMainFormModel mainFormModel, ITicketCreator ticketCreator)
        {
            if (view is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(view)));
            }

            if (messageService is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(messageService)));
            }

            if (mainFormModel is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(mainFormModel)));
            }

            if (ticketCreator is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(ticketCreator)));
            }

            this.view = view;
            this.messageService = messageService;
            this.mainFormModel = mainFormModel;
            this.ticketCreator = ticketCreator;

            view.Enter_Click += View_Enter_Click;
            view.Registrator_Click += View_Registrator_Click;
            view.Doctor_Click += View_Doctor_Click;
            view.MainFormLoad += View_MainFormLoad;
        }

        private void View_MainFormLoad(object sender, EventArgs e)
        {
            ticketCreator.RemoveTickets(mainFormModel.GetOldPrintedTickets());
            mainFormModel.DeleteOldTickets();
            mainFormModel.CheckStatistics(31);
        }

        private void View_Doctor_Click(object sender, EventArgs e)
        {
            DoctorFormPresenter doctorFormPresenter = new DoctorFormPresenter(view.IDoctorViewRef);
        }

        private void View_Registrator_Click(object sender, EventArgs e)
        {
            RegistratorFormPresenter registratorFormPresenter = new RegistratorFormPresenter(view.IRegistratorViewRef);
        }

        private void View_Enter_Click(object sender, EventArgs e)
        {
            view.EntityChoice(mainFormModel.LoginChecker(view.EnteredLogin));
        }
        
    }
}
