using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyclinicBL;

namespace PolyclinicView
{
    public class RoomsRegisterPresenter
    {
        private IRoomsRegisterView iRoomsRegister;
        private IRoomsRegisterModel roomsRegisterModel;

        public RoomsRegisterPresenter(IRoomsRegisterView iRoomsRegister, IRoomsRegisterModel roomsRegisterModel)
        {
            if (iRoomsRegister is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(iRoomsRegister)));
            }

            if (roomsRegisterModel is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(roomsRegisterModel)));
            }

            this.iRoomsRegister = iRoomsRegister;
            this.roomsRegisterModel = roomsRegisterModel;
            iRoomsRegister.RoomsRegisterLoad += IRoomsRegister_RoomsRegisterLoad;
            iRoomsRegister.SpecializationChoise += IRoomsRegister_SpecializationChoise;
            iRoomsRegister.RoomsAdd += IRoomsRegister_RoomsAdd;
        }

        private void IRoomsRegister_RoomsAdd(object sender, RoomsEventArgs e)
        {
            roomsRegisterModel.SetRooms(e.Rooms, e.SpecializationId);
        }

        private void IRoomsRegister_SpecializationChoise(object sender, EntityIdEventArgs e)
        {
            iRoomsRegister.SetAvailableRooms(roomsRegisterModel.GetRooms(e.DoctorsId));
        }

        private void IRoomsRegister_RoomsRegisterLoad(object sender, EventArgs e)
        {
            iRoomsRegister.SetSpecializations(roomsRegisterModel.GetSpecializations());
        }
    }
}
