using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicView
{
    public class ShowStatisticsPresenter
    {
        private IShowStatistic iShowStatistic;

        public ShowStatisticsPresenter(IShowStatistic iShowStatistic)
        {
            if (iShowStatistic is null)
            {
                throw new ArgumentNullException("{0} is null", nameof(iShowStatistic));
            }

            this.iShowStatistic = iShowStatistic;
        }
    }
}
