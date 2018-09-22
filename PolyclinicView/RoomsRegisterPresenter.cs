using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicView
{
    public class RoomsRegisterPresenter
    {
        private IRoomsRegisterView iRoomsRegister;

        public RoomsRegisterPresenter(IRoomsRegisterView iRoomsRegister)
        {
            if (iRoomsRegister is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(iRoomsRegister)));
            }

            this.iRoomsRegister = iRoomsRegister;
        }
    }
}
