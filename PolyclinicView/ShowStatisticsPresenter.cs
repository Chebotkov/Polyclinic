using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyclinicBL;

namespace PolyclinicView
{
    public class ShowStatisticsPresenter
    {
        private IShowStatistic iShowStatistic;
        private IShowStatisticsModel iShowStatisticsModel;

        public ShowStatisticsPresenter(IShowStatistic iShowStatistic, IShowStatisticsModel iShowStatisticsModel)
        {
            if (iShowStatistic is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(iShowStatistic)));
            }
            if (iShowStatisticsModel is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(iShowStatisticsModel)));
            }

            this.iShowStatistic = iShowStatistic;
            this.iShowStatisticsModel = iShowStatisticsModel;

            iShowStatistic.ShowStatisticsLoad += IShowStatistic_ShowStatisticsLoad;
            iShowStatistic.ShowDoctorsStatistic += IShowStatistic_ShowDoctorsStatistic;
            iShowStatistic.MedicalCardOpen += IShowStatistic_MedicalCardOpen;
            iShowStatistic.DateChange += IShowStatistic_DateChange;
        }

        private void IShowStatistic_DateChange(object sender, DateChangedEventArgs e)
        {
            iShowStatistic.SetPatients(iShowStatisticsModel.GetRecordedPatients(e.DocId, e.Date));
        }
        
        private void IShowStatistic_MedicalCardOpen(object sender, EventArgs e)
        {
            ShowMedicalCardPresenter showMedicalCardPresenter = new ShowMedicalCardPresenter(iShowStatistic.iShowMedicalCard, new MedicalCardManager());
        }

        private void IShowStatistic_ShowDoctorsStatistic(object sender, DoctorEventArgs e)
        {
            iShowStatistic.SetDoctorsStatistic(iShowStatisticsModel.GetDoctorsStatistic(e.DoctorsId));
        }

        private void IShowStatistic_ShowStatisticsLoad(object sender, EventArgs e)
        {
            iShowStatistic.SetDoctors(iShowStatisticsModel.GetDoctors());
        }
    }
}
