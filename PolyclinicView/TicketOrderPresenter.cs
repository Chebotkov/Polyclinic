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
        }

        private void ITicketOrderView_TicketOrderFormLoad(object sender, EventArgs e)
        {
            iTicketOrderView.FillForm(iTicketOrderFormModel.GetSpecializationsNames(), iTicketOrderFormModel.GetPatientsFullNames());   
        }
    }
}
