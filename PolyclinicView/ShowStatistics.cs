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
using PolyclinicDBManager;

namespace PolyclinicView
{
    public interface IShowStatistic
    {
        IShowMedicalCard iShowMedicalCard { get; }

        event EventHandler<DateChangedEventArgs> DateChange;
        event EventHandler MedicalCardOpen;
        event EventHandler ShowStatisticsLoad;
        event EventHandler<DoctorEventArgs> ShowDoctorsStatistic;

        void SetDoctors(IEnumerable doctors);
        void SetDoctorsStatistic(IEnumerable statistic);
        void SetPatients(IEnumerable patients);
    }

    public partial class ShowStatistics : Form, IShowStatistic
    {
        public event EventHandler<DateChangedEventArgs> DateChange;
        public event EventHandler MedicalCardOpen;
        public event EventHandler ShowStatisticsLoad;
        public event EventHandler<DoctorEventArgs> ShowDoctorsStatistic;

        public IShowMedicalCard iShowMedicalCard { get; private set; }
        private IEnumerable Statistics;

        public ShowStatistics()
        {
            InitializeComponent();
        }

        private void ShowStatistics_Load(object sender, EventArgs e)
        {
            ShowStatisticsLoad?.Invoke(this, EventArgs.Empty);

            monthCalendar1.Enabled = false;
            listBox1.Enabled = false;
            button1.Hide();

            monthCalendar1.MinDate = DateTime.Today.AddDays(-31);
            monthCalendar1.MaxDate = DateTime.Today;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label7.Text = "Явившиеся пациенты: 0";
            label6.Text = "Неявившиеся пациенты: 0";
            ShowDoctorsStatistic?.Invoke(this, new DoctorEventArgs(Editor.GetId(comboBox1.SelectedItem.ToString())));

            listBox1.Items.Clear();
            listBox1.Enabled = false;
            monthCalendar1.Enabled = true;
            button1.Hide();
            monthCalendar1.DateSelected += new DateRangeEventHandler(monthCalendar1_DateChanged);

            label8.Text = "Статистика посещений на сегодня: ";

            int Arrived = 0;
            int NonArrived = 0;
            foreach (VisitorStatistics statistic in Statistics)
            {
                if (statistic.Date.ToShortDateString() == DateTime.Today.ToShortDateString())
                {
                    label7.Text = "Явившиеся пациенты: " + statistic.ArrivedPatients;
                    label6.Text = "Неявившиеся пациенты: " + statistic.NonArrivedPatients;
                }

                Arrived += statistic.ArrivedPatients.Value;
                NonArrived += statistic.NonArrivedPatients.Value;
            }

            label4.Text = "Явившиеся пациенты: " + Arrived;
            label5.Text = "Неявившиеся пациенты: " + NonArrived;
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

            label7.Text = "Явившиеся пациенты: 0";
            label6.Text = "Неявившиеся пациенты: 0";
            foreach (VisitorStatistics statistic in Statistics)
            {
                if (statistic.Date.ToShortDateString() == e.Start.ToShortDateString())
                {
                    label7.Text = "Явившиеся пациенты: " + statistic.ArrivedPatients;
                    label6.Text = "Неявившиеся пациенты: " + statistic.NonArrivedPatients;
                    break;
                }
            }

            DateChange?.Invoke(this, new DateChangedEventArgs(Editor.GetId(comboBox1.SelectedItem.ToString()), e.Start.Date));
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex != -1)
            {
                ShowMedicalCard SMC = new ShowMedicalCard(Convert.ToInt32(listBox1.SelectedItem.ToString().Substring(0, listBox1.SelectedItem.ToString().IndexOf('.'))));
                iShowMedicalCard = SMC;
                MedicalCardOpen?.Invoke(this, EventArgs.Empty);
                SMC.ShowDialog();
            }
        }

        public void SetDoctors(IEnumerable doctors)
        {
            if (doctors is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(doctors)));
            }

            comboBox1.DataSource = doctors;
        }

        public void SetDoctorsStatistic(IEnumerable statistic)
        {
            if (statistic is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(statistic)));
            }

            Statistics = statistic;
        }

        public void SetPatients(IEnumerable patients)
        {
            if (patients is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(patients)));
            }

            listBox1.DataSource = patients;
        }
    }
}
