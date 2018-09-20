using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolyclinicView
{
    public interface IPrintTicketView
    {

    }

    public partial class PrintTicketForm : Form, IPrintTicketView
    {
        /*public List<Patient> Patients = new List<Patient>();
        public List<Doctor> Doctors = new List<Doctor>();
        public List<Ticket> OrderedTickets = new List<Ticket>();

        Doctor Doc = new Doctor();*/

        public PrintTicketForm()
        {
            InitializeComponent();
        }

        private void PrintTicketForm_Load(object sender, EventArgs e)
        {
            /*Filling F = new Filling();
            F.PatientsListFilling(Patients);
            F.DoctorsListFilling(Doctors);
            F.TicketsFilling(Patients, Doctors, OrderedTickets);*/

            comboBox2.Enabled = false;
            /*foreach (Patient p in Patients)
            {
                comboBox1.Items.Add(p.id +"."+p.LastName + " " + p.Name + " " + p.PatronymicName);
            }*/
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
            /*if (print)
            {
                string date = comboBox2.Text.Substring(0, comboBox2.Text.IndexOf(" "));
                string time = (comboBox2.Text.Substring(date.Length+1, comboBox2.Text.Length-date.Length-1)).Substring(0, 6);
                string patient = comboBox1.Text.Substring(comboBox1.Text.IndexOf(".")+1, comboBox1.Text.Length - comboBox1.Text.IndexOf(".")-1);
                ShowTicketOnScreen STOS = new ShowTicketOnScreen(patient, Doc, date, time);
                STOS.Owner = this;
                this.Visible = false;
                STOS.ShowDialog();
            }*/
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (comboBox1.SelectedIndex != -1)
            {
                comboBox2.Items.Clear();
                /*foreach (Ticket T in OrderedTickets)
                {
                    if (Convert.ToInt32(comboBox1.Text.Substring(0, comboBox1.Text.IndexOf("."))) == T.PatientsId)
                    {
                        foreach (Doctor d in Doctors)
                        {
                            if (d.id == Convert.ToInt32(T.DoctorsFullName))
                            {
                                Doc = d;
                                comboBox2.Items.Add(T.DateOfRecord + " " + T.VisitingTime + " " + d.Specialization+  " " + d.LastName + " " + d.Name + " " + d.Patronymic);
                            }
                        }
                    }
                }*/
                if (comboBox2.Items.Count == 0) MessageBox.Show("Для данного пациента не найдено талонов", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else comboBox2.Enabled = true;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }
    }
    
}
