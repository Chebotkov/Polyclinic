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
    public interface IShowStatistic
    {

    }

    public partial class ShowStatistics : Form, IShowStatistic
    {
        /*public List<Patient> Patients = new List<Patient>();
        public List<Doctor> Doctors = new List<Doctor>();
        public List<Ticket> Tickets = new List<Ticket>(); */

        public ShowStatistics()
        {
            InitializeComponent();
        }

        private void ShowStatistics_Load(object sender, EventArgs e)
        {
            /*Filling F = new Filling();
            F.DoctorsListFilling(Doctors);
            F.TicketsFilling(Patients, Doctors, Tickets);
            F.PatientsListFilling(Patients);*/

            monthCalendar1.Enabled = false;
            listBox1.Enabled = false;
            button1.Hide();

            /*foreach (Doctor d in Doctors)
            {
                comboBox1.Items.Add(d.id + "." + d.LastName + " " + d.Name + " " + d.Patronymic);
            }*/
            monthCalendar1.MinDate = DateTime.Today.AddDays(-31);
            monthCalendar1.MaxDate = DateTime.Today;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Enabled = false;
            monthCalendar1.Enabled = true;
            button1.Hide();
            monthCalendar1.DateSelected += new DateRangeEventHandler(monthCalendar1_DateChanged);

            label8.Text = "Статистика посещений на сегодня: ";
            /*foreach (Doctor d in Doctors)
            {
                if (d.id == GetId(comboBox1))
                {
                    label7.Text = "Явившиеся пациенты: " + d.TodaysArrivedPatients;
                    label6.Text = "Неявившиеся пациенты: " + d.TodaysNonArrivedPatients;
                    int Arrived = 0;
                    int NonArrived = 0;
                    foreach (DoctorsStatistic ds in d.DSList)
                    {
                        Arrived += ds.ArrivedPatients;
                        NonArrived += ds.Non_ArrivedPatients;
                    }
                    label4.Text = "Явившиеся пациенты: " + Arrived;
                    label5.Text = "Неявившиеся пациенты: " + NonArrived;
                    break;
                }
            }*/
        }

        private int GetId(ComboBox comboBox)
        {
            return Convert.ToInt32(comboBox.SelectedItem.ToString().Substring(0, comboBox.SelectedItem.ToString().IndexOf(".")));
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            button1.Hide();
            listBox1.Items.Clear();
            listBox1.Enabled = true;
            label8.Text = "Статистика посещений на: " + e.Start.ToShortDateString();
            /*foreach (Doctor d in Doctors)
            {
                if (d.id == GetId(comboBox1))
                {
                    foreach (DoctorsStatistic ds in d.DSList)
                    {
                        if (ds.StatisticsDay == e.Start.ToShortDateString())
                        {
                            label7.Text = "Явившиеся пациенты: " + ds.ArrivedPatients;
                            label6.Text = "Неявившиеся пациенты: " + ds.Non_ArrivedPatients;
                            break;
                        }
                        else
                        {
                            label7.Text = "Явившиеся пациенты: 0";
                            label6.Text = "Неявившиеся пациенты: 0";
                        }
                    }
                    foreach(Ticket t in Tickets)
                    {
                        if(e.Start.ToShortDateString() == t.DateOfRecord && d.id == Convert.ToInt32(t.DoctorsFullName))
                        {
                            foreach (Patient p in Patients)
                            {
                                if (p.id == t.PatientsId)
                                {
                                    listBox1.Items.Add(p.id + ". " + p.LastName + " " + p.Name + " " + p.PatronymicName + "; Время приёма: " + t.VisitingTime);
                                    break;
                                }
                            }
                        }
                    }
                    break;
                }
            }*/
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*if(listBox1.SelectedIndex != -1)
            {
                ShowMedicalCard SMC = new ShowMedicalCard(Convert.ToInt32(listBox1.SelectedItem.ToString().Substring(0, listBox1.SelectedItem.ToString().IndexOf('.'))));
                SMC.ShowDialog();
            }*/
        }
    }
}
