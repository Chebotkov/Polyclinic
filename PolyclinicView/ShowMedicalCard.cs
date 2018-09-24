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

        public ShowMedicalCard(int id)
        {
            InitializeComponent();

            MedicalCardOpen?.Invoke(this, new PatientsIdAndSpecializationNameEventArgs(id, 0));
            textBox1.SelectionStart = textBox1.TextLength;
        }
    
        private void ShowMedicalCard_ClientSizeChanged(object sender, EventArgs e)
        {
            textBox1.Bounds = new Rectangle(0, 0, this.Width, this.Height);
        }

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
    }

}
