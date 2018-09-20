using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicBL
{
    public interface IMainFormModel
    {
        Entities LoginChecker(string enteredLogin);
    }
}
