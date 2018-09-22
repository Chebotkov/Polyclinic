using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyclinicDBManager;

namespace PolyclinicView
{
    public class RegistersFormPresenter
    {
        private IRegistersView iRegistersView;

        public RegistersFormPresenter(IRegistersView iRegistersView)
        {
            if (iRegistersView is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(iRegistersView)));
            }

            this.iRegistersView = iRegistersView;

            iRegistersView.NewDoctor_Click += IRegistersView_NewDoctor_Click;
            iRegistersView.NewSpecialization_Click += IRegistersView_NewSpecialization_Click;
        }

        private void IRegistersView_NewSpecialization_Click(object sender, EventArgs e)
        {
            NewSpecializationPresenter newSpecializationPresenter = new NewSpecializationPresenter(iRegistersView.INewSpecializationRef, new NewSpecializationModel());
        }

        private void IRegistersView_NewDoctor_Click(object sender, EventArgs e)
        {
            NewDoctorPresenter newDoctorPresenter = new NewDoctorPresenter(iRegistersView.INewDoctorViewRef);
        }
    }
}
