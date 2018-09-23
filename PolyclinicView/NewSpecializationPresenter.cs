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
            this.iNewSpecialization.AddNewSpecialization += INewSpecialization_AddNewSpecialization;
            this.iNewSpecialization.AddNewSchedule += INewSpecialization_AddNewSchedule;
            this.iNewSpecialization.AddNewDoctorsSchedule += INewSpecialization_AddNewDoctorsSchedule;
            this.iNewSpecialization.DoctorsFill += INewSpecialization_DoctorsFill;
        }

        private void INewSpecialization_DoctorsFill(object sender, EventArgs e)
        {
            iNewSpecialization.SetDoctors(iNewSpecializationModel.GetDoctors());
        }

        private void INewSpecialization_AddNewDoctorsSchedule(object sender, DoctorsTimeEventArgs e)
        {
            iNewSpecializationModel.AddNewDoctorsSchedule(e.DoctorsId, e.Schedule, e.Interval);
        }

        private void INewSpecialization_AddNewSchedule(object sender, ScheduleEventArgs e)
        {
            iNewSpecializationModel.AddNewSchedule(e.SpecializationId, e.Schedule, e.Interval);
        }

        private void INewSpecialization_AddNewSpecialization(object sender, SpecializationEventArgs e)
        {
            iNewSpecializationModel.AddNewSpecialization(e.SpecializationName);
        }

        private void INewSpecialization_NewSpecializationLoad(object sender, EventArgs e)
        {
            iNewSpecialization.SetSpecializations(iNewSpecializationModel.GetSpecializationsNames());
        }
    }
}
