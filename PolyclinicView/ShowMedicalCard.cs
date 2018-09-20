using System;
using System.Drawing;
using System.Windows.Forms;

namespace PolyclinicView
{
    public partial class ShowMedicalCard : Form
    {
        //Methods M = new Methods();
        public ShowMedicalCard(int id)
        {
            InitializeComponent();
            //string pathToCard = PathInfo.PathToProject + "\\MedicalCards\\" + id.ToString() + ".txt";

            //M.ReadFromMedicalCard(pathToCard, textBox1);
            
            textBox1.SelectionStart = textBox1.TextLength;
        }

        private void ShowMedicalCard_ClientSizeChanged(object sender, EventArgs e)
        {
            textBox1.Bounds = new Rectangle(0, 0, this.Width, this.Height);
        }
    }
}
