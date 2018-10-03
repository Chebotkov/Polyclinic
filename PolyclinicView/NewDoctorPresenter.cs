using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyclinicBL;
using PolyclinicDBManager.Models;

namespace PolyclinicView
{
    public class NewDoctorPresenter
    {
        private INewDoctorView iNewDoctorView;
        private INewDoctorModel newDoctorModel;

        public NewDoctorPresenter(INewDoctorView iNewDoctorView, INewDoctorModel newDoctorModel)
        {
            if (iNewDoctorView is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(iNewDoctorView)));
            }

            if (newDoctorModel is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(newDoctorModel)));
            }

            this.iNewDoctorView = iNewDoctorView;
            this.newDoctorModel = newDoctorModel;
            iNewDoctorView.NewDoctorLoad += INewDoctorView_NewDoctorLoad;
            iNewDoctorView.SpecializationSelect += INewDoctorView_SpecializationSelect;
            iNewDoctorView.RoomsRegisterOpen += INewDoctorView_RoomsRegisterOpen;
            iNewDoctorView.IsRoomFree += INewDoctorView_IsRoomFree;
            iNewDoctorView.DoctorCreate += INewDoctorView_DoctorCreate;
        }

        private void INewDoctorView_DoctorCreate(object sender, NewDoctorEventArgs e)
        {
            newDoctorModel.DoctorCreate(e.doctor);
        }

        private void INewDoctorView_IsRoomFree(object sender, EntityIdEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void INewDoctorView_SpecializationSelect(object sender, EntityIdEventArgs e)
        {
            iNewDoctorView.SetRooms(newDoctorModel.GetRooms(e.DoctorsId));
        }

        private void INewDoctorView_RoomsRegisterOpen(object sender, EventArgs e)
        {
            RoomsRegisterPresenter roomsRegisterPresenter = new RoomsRegisterPresenter(iNewDoctorView.RoomsRegisterView, new RoomsRegisterModel());
        }

        private void INewDoctorView_NewDoctorLoad(object sender, EventArgs e)
        {
            iNewDoctorView.SetRegionsAndSpecializations(newDoctorModel.GetRegions(), newDoctorModel.GetSpecializations());
        }
    }
}
