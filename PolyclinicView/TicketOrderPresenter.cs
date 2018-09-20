using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicView
{
    public class TicketOrderPresenter
    {
        private ITicketOrderView iTicketOrderView;

        public TicketOrderPresenter(ITicketOrderView iTicketOrderView)
        {
            if (iTicketOrderView is null)
            {
                throw new ArgumentNullException("{0} is null", nameof(iTicketOrderView));
            }

            this.iTicketOrderView = iTicketOrderView;
        }
    }
}
