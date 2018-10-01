using System;
using System.Windows.Forms;
using PolyclinicBL;

namespace PolyclinicView
{
    public interface IShowTicketOnScreen
    {
        event EventHandler<ShowTicketOnScreenEventArgs> CreateTicket;
    }

    public partial class ShowTicketOnScreen : Form, IShowTicketOnScreen
    {
        public event EventHandler<ShowTicketOnScreenEventArgs> CreateTicket;
        private PrintedTicket printedTicket;

        public ShowTicketOnScreen(PrintedTicket printedTicket)
        {
            InitializeComponent();

            this.printedTicket = printedTicket;

            label9.Text = printedTicket.PatientFullName;
            label10.Text = printedTicket.DocSpecialization;
            label11.Text = printedTicket.DocFullName; 
            label12.Text = printedTicket.DocRoom.ToString();
            label13.Text = printedTicket.Date;
            label14.Text = printedTicket.Time;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            PrintTicketForm PT = this.Owner as PrintTicketForm;

            CreateTicket?.Invoke(this, new ShowTicketOnScreenEventArgs(printedTicket));

            MessageBox.Show("Талон распечатан", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void ShowTicketOnScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            PrintTicketForm PT = this.Owner as PrintTicketForm;
            PT.Visible = true;
        }
    }
}
