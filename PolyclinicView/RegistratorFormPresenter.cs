using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PolyclinicDBManager;
using PolyclinicBL;

namespace PolyclinicView
{
    public class RegistratorFormPresenter
    {
        private IRegistratorView registratorView;
        public RegistratorFormPresenter(IRegistratorView registratorView)
        {
            if (registratorView is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(registratorView)));
            }

            this.registratorView = registratorView;

            registratorView.PrintTicket_Click += RegistratorView_PrintTicket_Click;
            registratorView.ReferenceBook_Click += RegistratorView_ReferenceBook_Click;
            registratorView.Registers_Click += RegistratorView_Registers_Click;
            registratorView.Registration_Click += RegistratorView_Registration_Click;
            registratorView.RoomsRegister_Click += RegistratorView_RoomsRegister_Click;
            registratorView.TicketOrder_Click += RegistratorView_TicketOrder_Click;
        }

        private void RegistratorView_TicketOrder_Click(object sender, EventArgs e)
        {
            TicketOrderPresenter ticketOrderPresenter = new TicketOrderPresenter(registratorView.ITicketOrderViewRef, new TicketOrderModel());
        }

        private void RegistratorView_RoomsRegister_Click(object sender, EventArgs e)
        {
            RoomsRegisterPresenter roomsRegisterPresenter = new RoomsRegisterPresenter(registratorView.IRoomsRegisterViewRef);
        }

        private void RegistratorView_Registration_Click(object sender, EventArgs e)
        {
            RegistrationPresenter registrationPresenter = new RegistrationPresenter(registratorView.IRegistrationViewRef, new RegistrationModel(), new MedicalCardManager());
        }

        private void RegistratorView_PrintTicket_Click(object sender, EventArgs e)
        {
            PrintTicketPresenter printTicketPresenter = new PrintTicketPresenter(registratorView.IPrintTicketViewRef);
        }

        private void RegistratorView_Registers_Click(object sender, EventArgs e)
        {
            RegistersFormPresenter registersFormPresenter = new RegistersFormPresenter(registratorView.IRegistersViewRef, new RegistersModel());
        }

        private void RegistratorView_ReferenceBook_Click(object sender, EventArgs e)
        {
            ReferenceBookPresenter referenceBookPresenter = new ReferenceBookPresenter(registratorView.IReferenceBookViewRef);
        }
    }
}
