using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using PolyclinicBL;

namespace PolyclinicView
{
    public interface IShowMedicalCard
    {
        event EventHandler<PatientsIdAndSpecializationNameEventArgs> MedicalCardOpen; 

        void OpenMedicalCard(TextReader reader);
    }
        
    public partial class ShowMedicalCard : Form, IShowMedicalCard
    {
        public event EventHandler<PatientsIdAndSpecializationNameEventArgs> MedicalCardOpen;
        private int PatientsId;

        public ShowMedicalCard(int id)
        {
            InitializeComponent();

            PatientsId = id;
            textBox1.SelectionStart = textBox1.TextLength;
        }

        #region Actions
        private void ShowMedicalCard_Load(object sender, EventArgs e)
        {
            MedicalCardOpen?.Invoke(this, new PatientsIdAndSpecializationNameEventArgs(PatientsId, 0));
        }

        private void ShowMedicalCard_ClientSizeChanged(object sender, EventArgs e)
        {
            textBox1.Bounds = new Rectangle(0, 0, this.Width, this.Height);
        }
        #endregion

        #region Interface implementation
        public void OpenMedicalCard(TextReader reader)
        {
            StreamReader streamReader = reader as StreamReader;
            string line;
            while ((line = streamReader.ReadLine()) != null)
            {
                textBox1.Text += line + Environment.NewLine;
            }
            streamReader.Close();
        }
        #endregion
    }

}
