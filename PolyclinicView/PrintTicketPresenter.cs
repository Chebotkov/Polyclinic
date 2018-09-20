using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicView
{
    public class PrintTicketPresenter
    {
        private IPrintTicketView iPrintTicketView; 

        public PrintTicketPresenter(IPrintTicketView iPrintTicketView)
        {
            if (iPrintTicketView is null)
            {
                throw new ArgumentNullException("{0} is null", nameof(iPrintTicketView));
            }

            this.iPrintTicketView = iPrintTicketView;
        }
    }
}
