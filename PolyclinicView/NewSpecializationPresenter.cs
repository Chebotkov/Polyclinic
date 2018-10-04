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
            iNewSpecialization.NewSpecializationLoad += INewSpecialization_NewSpecializationLoad;
            iNewSpecialization.AddNewSpecialization += INewSpecialization_AddNewSpecialization;
            iNewSpecialization.AddNewSchedule += INewSpecialization_AddNewSchedule;
            iNewSpecialization.AddNewDoctorsSchedule += INewSpecialization_AddNewDoctorsSchedule;
            iNewSpecialization.SpecializationSelect += INewSpecialization_SpecializationSelect;
        }

        private void INewSpecialization_SpecializationSelect(object sender, EntityIdEventArgs e)
        {
            iNewSpecialization.SetDoctors(iNewSpecializationModel.GetDoctors(e.DoctorsId));
            iNewSpecialization.SetScheduleAndIntervals(iNewSpecializationModel.GetDoctorsSchedule(e.DoctorsId), iNewSpecializationModel.GetDoctorsInterval(e.DoctorsId));
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
