using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyclinicBL;

namespace PolyclinicView
{
    public class ShowTicketOnScreenPresenter
    {
        private IShowTicketOnScreen showTicketOnScreen;
        private ITicketCreator ticketCreator;

        public ShowTicketOnScreenPresenter(IShowTicketOnScreen showTicketOnScreen, ITicketCreator ticketCreator)
        {
            if (showTicketOnScreen is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(showTicketOnScreen)));
            }

            if (ticketCreator is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(ticketCreator)));
            }

            this.showTicketOnScreen = showTicketOnScreen;
            this.ticketCreator = ticketCreator;
            showTicketOnScreen.CreateTicket += ShowTicketOnScreen_CreateTicket;
        }

        private void ShowTicketOnScreen_CreateTicket(object sender, ShowTicketOnScreenEventArgs e)
        {
            ticketCreator.WriteToWordFile(e.printedTicket);
        }
    }
}
