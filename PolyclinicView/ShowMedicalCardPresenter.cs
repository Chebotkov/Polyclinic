using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicView
{
    public class ShowMedicalCardPresenter
    {
        private IShowMedicalCard iShowMedicalCard;

        public ShowMedicalCardPresenter(IShowMedicalCard iShowMedicalCard)
        {
            if (iShowMedicalCard is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(iShowMedicalCard)));
            }

            this.iShowMedicalCard = iShowMedicalCard;
        }
    }
}
