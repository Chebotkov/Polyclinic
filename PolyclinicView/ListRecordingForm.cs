using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolyclinicView
{
    public interface IListRecordingView
    {

    }

    public partial class ListRecordingForm : Form, IListRecordingView
    {

        /*public List<Patient> Patients = new List<Patient>();
        public List<Doctor> Doctors = new List<Doctor>();
        public List<Ticket> OrderedTickets = new List<Ticket>();
        Methods M = new Methods();*/

        string ChosenDate;

        public ListRecordingForm()
        {
            /*Filling F = new Filling();
            F.PatientsListFilling(Patients);
            F.DoctorsListFilling(Doctors);
            F.TicketsFilling(Patients, Doctors, OrderedTickets);*/

            InitializeComponent();
            monthCalendar1.MinDate = DateTime.Today;
            label1.Visible = false;
            button2.Enabled = false;
            monthCalendar1.Visible = false;

            //M.AddDoctorsToComboBox(Doctors, comboBox1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ListRecordingForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                /*ShowMedicalCard SMC = new ShowMedicalCard(Convert.ToInt32(listBox1.Text.Substring(0, listBox1.Text.IndexOf("."))));
                SMC.Show();*/
            }
            else MessageBox.Show("Выберите пациента из списка ниже!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;
            monthCalendar1.Visible = true;
            label1.Visible = true;
            listBox1.Items.Clear();
            monthCalendar1.DateSelected += new DateRangeEventHandler(monthCalendar1_DateChanged);
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0 && comboBox1.SelectedIndex != -1) MessageBox.Show("В данный день у вас нет пациентов", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            listBox1.Items.Clear();
            ChosenDate = e.Start.ToShortDateString();
            /*{
                foreach (Ticket t in OrderedTickets)
                {
                    if (Convert.ToInt32(t.DoctorsFullName) == M.GetId(comboBox1) && t.DateOfRecord == ChosenDate)
                    {
                        foreach (Patient p in Patients)
                        {
                            if (p.id == t.PatientsId) listBox1.Items.Add(p.id + "." + p.LastName + " " + p.Name + " " + p.PatronymicName + "\t\t" + t.DateOfRecord + "\t" + t.VisitingTime);
                        }
                    }
                }
            }*/
        }
    }
}
