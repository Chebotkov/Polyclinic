using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyclinicBL;

namespace PolyclinicView
{
    public class NewSpecializationPresenter
    {
        private INewSpecialization iNewSpecialization;
        private INewSpecializationModel iNewSpecializationModel;

        public NewSpecializationPresenter(INewSpecialization iNewSpecialization, INewSpecializationModel iNewSpecializationModel)
        {
            if (iNewSpecialization is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(iNewSpecialization)));
            }

            if (iNewSpecializationModel is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(iNewSpecializationModel)));
            }

            this.iNewSpecialization = iNewSpecialization;
            this.iNewSpecializationModel = iNewSpecializationModel;
            this.iNewSpecialization.NewSpecializationLoad += INewSpecialization_NewSpecializationLoad;
        }

        private void INewSpecialization_NewSpecializationLoad(object sender, EventArgs e)
        {
            iNewSpecialization.SetSpecializations(iNewSpecializationModel.GetSpecializationsNames());
        }
    }
}
