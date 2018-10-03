using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PolyclinicBL;

namespace PolyclinicView
{
    public interface IPrintTicketView
    {
        event EventHandler PrintTicketLoad;
        event EventHandler<EntityIdEventArgs> PatientChoise;
        event EventHandler ShowTicketOnScreenOpen;
        event EventHandler<EntityIdEventArgs> TicketChoise; 
        void SetPatients(IEnumerable Patients);
        void SetTickets(IEnumerable Tickets);
        IShowTicketOnScreen iShowTicketOnScreen { get; }
        PrintedTicket printedTicket { get; set; }
    }

    public partial class PrintTicketForm : Form, IPrintTicketView
    {
        public event EventHandler PrintTicketLoad;
        public event EventHandler<EntityIdEventArgs> PatientChoise;
        public event EventHandler ShowTicketOnScreenOpen;
        public event EventHandler<EntityIdEventArgs> TicketChoise;
        public IShowTicketOnScreen iShowTicketOnScreen { get; private set; }
        public PrintedTicket printedTicket { get; set; }

        private bool isLoad = false;

        public PrintTicketForm()
        {
            InitializeComponent();
        }

        #region Actions
        private void PrintTicketForm_Load(object sender, EventArgs e)
        {
            PrintTicketLoad?.Invoke(this, EventArgs.Empty);

            comboBox2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool print = true;
            errorProvider1.Clear();
            if (comboBox1.SelectedIndex == -1)
            {
                errorProvider1.SetError(comboBox1, "Выберите пациента");
                print = false;
            }
            if (comboBox2.SelectedIndex == -1)
            {
                errorProvider1.SetError(label2, "Выберите талон");
                print = false;
            }
            
            if (print)
            {
                TicketChoise?.Invoke(this, new EntityIdEventArgs(Editor.GetId(comboBox2.SelectedItem.ToString())));
                
                ShowTicketOnScreen STOS = new ShowTicketOnScreen(printedTicket);
                STOS.Owner = this;
                iShowTicketOnScreen = STOS;
                ShowTicketOnScreenOpen?.Invoke(this, EventArgs.Empty);
                this.Visible = false;
                STOS.ShowDialog();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isLoad)
            {
                errorProvider1.Clear();

                PatientChoise?.Invoke(this, new EntityIdEventArgs(Editor.GetId(comboBox1.SelectedItem.ToString())));

                if (comboBox2.Items.Count == 0) MessageBox.Show("Для данного пациента не найдено талонов", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else comboBox2.Enabled = true;
            }

            isLoad = false;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }
        #endregion

        #region Interface implementation
        public void SetPatients(IEnumerable Patients)
        {
            if (Patients is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(Patients)));
            }

            isLoad = true;
            comboBox1.DataSource = Patients;
        }

        public void SetTickets(IEnumerable Tickets)
        {
            if (Tickets is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(Tickets)));
            }

            comboBox2.DataSource = Tickets;
            comboBox2.SelectedIndex = -1;
            comboBox2.Text = "";
        }
        #endregion
    }
}
