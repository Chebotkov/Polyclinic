using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyclinicBL;

namespace PolyclinicView
{
    public class ListRecordingFormPresenter
    {
        private IListRecordingView iListRecordingView;
        private IListRecordingModel iListRecordingModel;

        public ListRecordingFormPresenter(IListRecordingView iListRecordingView, IListRecordingModel iListRecordingModel)
        {
            if (iListRecordingView is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(iListRecordingView)));
            }

            if (iListRecordingModel is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(iListRecordingModel)));
            }
            
            this.iListRecordingView = iListRecordingView;
            this.iListRecordingModel = iListRecordingModel;

            iListRecordingView.ListRecordingFormLoad += IListRecordingView_ListRecordingFormLoad;
            iListRecordingView.DateChange += IListRecordingView_DateChange;
            iListRecordingView.MedicalCardOpen += IListRecordingView_MedicalCardOpen;
        }

        private void IListRecordingView_MedicalCardOpen(object sender, EventArgs e)
        {
            ShowMedicalCardPresenter showMedicalCardPresenter = new ShowMedicalCardPresenter(iListRecordingView.showMedicalCard, new MedicalCardManager());
        }

        private void IListRecordingView_DateChange(object sender, DateSelectedEventArgs e)
        {
            iListRecordingView.SetPatients(iListRecordingModel.GetPatientsByCriterion(e.DocId, e.Date));
        }

        private void IListRecordingView_ListRecordingFormLoad(object sender, EventArgs e)
        {
            iListRecordingView.SetDoctors(iListRecordingModel.GetDoctors());
        }
    }
}
