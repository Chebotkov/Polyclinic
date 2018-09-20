using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyclinicBL;

namespace PolyclinicDBManager
{
    public class MainFormModel : IMainFormModel
    {
        public MainFormModel()
        {
        }

        public Entities LoginChecker(string enteredLogin)
        {
            if (enteredLogin is null)
            {
                throw new ArgumentNullException("{0} is null", nameof(enteredLogin));
            }
            
            using (PolyclinicDBContext polyclinicDBContext = new PolyclinicDBContext())
            {
                var logins = polyclinicDBContext.Login.AsNoTracking().ToList();

                foreach (Login login in logins)
                {
                    if (login.PolyclinicUser == "Administrator" && enteredLogin == login.UsersLogin)
                        return Entities.Administrator;

                    if (login.PolyclinicUser == "Registrator" && enteredLogin == login.UsersLogin)
                        return Entities.Registrator;

                    if (login.PolyclinicUser == "Doctor" && enteredLogin == login.UsersLogin)
                        return Entities.Doctor;
                }

                return Entities.Empty;
            }
        }
    }
}
