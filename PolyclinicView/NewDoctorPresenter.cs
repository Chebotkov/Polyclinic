using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicView
{
    public class NewDoctorPresenter
    {
        private INewDoctorView iNewDoctorView;

        public NewDoctorPresenter(INewDoctorView iNewDoctorView)
        {
            if (iNewDoctorView is null)
            {
                throw new ArgumentNullException("{0} is null", nameof(iNewDoctorView));
            }

            this.iNewDoctorView = iNewDoctorView;
        }
    }
}
