using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicView
{
    public class ReferenceBookPresenter
    {
        private IReferenceBookView referenceBookView;

        public ReferenceBookPresenter(IReferenceBookView referenceBookView)
        {
            if (referenceBookView is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(referenceBookView)));
            }

            this.referenceBookView = referenceBookView;
        }
    }
}
