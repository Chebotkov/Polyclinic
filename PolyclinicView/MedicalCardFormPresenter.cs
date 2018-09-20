using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicView
{
    public class MedicalCardFormPresenter
    {
        private IMedicalCardView iMedicalCardView;
        public MedicalCardFormPresenter(IMedicalCardView iMedicalCardView)
        {
            if (iMedicalCardView is null)
            {
                throw new ArgumentNullException("{0} is null", nameof(iMedicalCardView));
            }

            this.iMedicalCardView = iMedicalCardView;
            iMedicalCardView.ReferenceBook_Click += IMedicalCardView_ReferenceBook_Click;
        }

        private void IMedicalCardView_ReferenceBook_Click(object sender, EventArgs e)
        {
            ReferenceBookPresenter referenceBookPresenter = new ReferenceBookPresenter(iMedicalCardView.IReferenceBookViewRef);
        }
    }
}
