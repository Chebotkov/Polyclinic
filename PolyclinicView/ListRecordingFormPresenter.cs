using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicView
{
    public class ListRecordingFormPresenter
    {
        private IListRecordingView iListRecordingView;

        public ListRecordingFormPresenter(IListRecordingView iListRecordingView)
        {
            if (iListRecordingView is null)
            {
                throw new ArgumentNullException("{0} is null", nameof(iListRecordingView));
            }

            this.iListRecordingView = iListRecordingView;
        }
    }
}
