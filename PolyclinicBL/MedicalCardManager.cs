using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolyclinicBL
{
    public interface IMedicalCardManager
    {
        void CreateMedicalCard(Patient patient);
    }

    public class MedicalCardManager : IMedicalCardManager
    {
        public string ProjectName { get; set; } = "PolyclinicAPP";

        public void CreateMedicalCard(Patient patient)
        {   
            using (StreamWriter write = new StreamWriter(GetApplicationsPath() + @"\Files\MedicalCards\" + patient.id.ToString() + ".txt", false, System.Text.Encoding.Default))
            {
                write.WriteLine(String.Format("ФИО: {0} {1} {2}", patient.LastName, patient.FirstName, patient.Patronymic));
                write.WriteLine("Дата рождения: " + patient.Birth.ToShortDateString() + Environment.NewLine + "Пол: " + (patient.Gender == true ? "Мужской" : "Женский") + Environment.NewLine + "Адрес: " + patient.Address + Environment.NewLine + "Дата регистрации: " + ((DateTime)patient.RegistrationDate).ToShortDateString() + Environment.NewLine);
                write.Close();
            }

        }

        private string GetApplicationsPath()
        {
            string pathToAssembly = Directory.GetCurrentDirectory();

            string currentPath = Directory.GetParent(Path.GetDirectoryName(pathToAssembly)).ToString();

            while (currentPath.Substring(currentPath.Length - ProjectName.Length) != ProjectName)
            {
                currentPath = Directory.GetParent(currentPath).ToString();
            }

            return currentPath;
        }
    }
}
