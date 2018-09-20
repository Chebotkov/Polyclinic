using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PolyclinicBL;
using PolyclinicDBManager.Properties;

namespace PolyclinicDBManager
{
    public class RegistrationModel : IRegistrationModel
    {
        public IEnumerable GetRegions()
        {
            List<string> regionsNames = new List<string>();
            using (var context = new PolyclinicDBContext())
            {
                var regions = context.Region.AsNoTracking();

                foreach (var region in regions)
                {
                    regionsNames.Add(region.RegionName);
                }
            }

            return regionsNames;
        }

        public int AddPatient(PolyclinicBL.Patient patient)
        {
            int patientsId;
            using (SqlConnection connection = new SqlConnection(Settings.Default.PolyclinicDBConnect))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Patient] ([LastName], [FirstName], [Patronymic], [Birth],[Gender],[Region],[Address],[RegistrationDate]) output INSERTED.ID VALUES( @LN, @FN, @Patronymic, @Birth, @Gender, @Region, @Address, @RegistrationDate)", connection))
                {
                    cmd.Parameters.AddWithValue("@LN", patient.LastName);
                    cmd.Parameters.AddWithValue("@FN", patient.FirstName);
                    cmd.Parameters.AddWithValue("@Patronymic", patient.Patronymic);
                    cmd.Parameters.AddWithValue("@Birth", patient.Birth);
                    cmd.Parameters.AddWithValue("@Gender", patient.Gender);
                    cmd.Parameters.AddWithValue("@Region", patient.Region);
                    cmd.Parameters.AddWithValue("@Address", patient.Address);
                    cmd.Parameters.AddWithValue("@RegistrationDate", patient.RegistrationDate);
                    connection.Open();

                    patientsId = (int)cmd.ExecuteScalar();

                    connection.Close();
                }
            }

            return patientsId;
        }

        public void CheckStreets(string streetName, int regionNumber)
        {
            bool exists = false;
            using (var context = new PolyclinicDBContext())
            {
                IQueryable<Street> streetQuery = context.Street;
                List<Street> streets = streetQuery.ToList();
                
                foreach (Street street in streets)
                {
                    if (streetName == street.StreetName && regionNumber == street.RegionNumber)
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                {
                    context.Street.Add(new Street() { RegionNumber = regionNumber, StreetName = streetName });
                }

                context.SaveChanges();
            }
        }
    }
}
