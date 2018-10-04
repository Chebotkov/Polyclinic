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
        TextReader ReadMedicalCard(int patientsId);
        void WriteToMedicalCard(int patientsId, byte[] medicalCardRecords);
    }

    public class MedicalCardManager : IMedicalCardManager
    {
        public string ProjectName { get; private set; } = "Polyclinic";
        public string PathToCards { get; private set; } = @"\Files\MedicalCards\";
        public string Extension { get; private set; } = ".txt";


        public MedicalCardManager() { }
        
        public MedicalCardManager(string projectName)
        {
            if (projectName is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(projectName)));
            }

            ProjectName = projectName;
        }

        public MedicalCardManager(string projectName, string PathToCards) : this(projectName)
        {
            if (PathToCards is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(PathToCards)));
            }

            this.PathToCards = PathToCards;
        }

        public MedicalCardManager(string projectName, string pathToCards, string extension) : this (projectName, pathToCards)
        {
            if (extension is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(extension)));
            }

            Extension = extension;
        }

        public void CreateMedicalCard(Patient patient)
        {
            if (patient is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(patient)));
            }

            using (StreamWriter write = new StreamWriter(GetMedicalCardPath(patient.id), false, System.Text.Encoding.Default))
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

        public string GetMedicalCardPath(int patientsId)
        {
            return String.Format(GetApplicationsPath() + PathToCards + patientsId.ToString() + Extension);
        }

        public TextReader ReadMedicalCard(int patientsId)
        {
            return new StreamReader(GetMedicalCardPath(patientsId), System.Text.Encoding.Default); 
        }
        
        public void WriteToMedicalCard(int patientsId, byte[] medicalCardRecords)
        {
            if (medicalCardRecords is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(medicalCardRecords)));
            }

            StreamWriter writer = new StreamWriter(GetMedicalCardPath(patientsId), true, System.Text.Encoding.Default);
            writer.Write(Encoding.Default.GetChars(medicalCardRecords));
            writer.Close();
        }
    }
}
