using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicView
{
    public class NewSpecializationPresenter
    {
        private INewSpecialization iNewSpecialization;

        public NewSpecializationPresenter(INewSpecialization iNewSpecialization)
        {
            if (iNewSpecialization is null)
            {
                throw new ArgumentNullException("{0} is null", nameof(iNewSpecialization));
            }

            this.iNewSpecialization = iNewSpecialization;
        }
    }
}
