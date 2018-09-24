using PolyclinicBL;
using PolyclinicDBManager;
using System;
using System.Collections;
using System.Windows.Forms;

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
        private DateTime chosenDate = DateTime.Today;

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
            listBox1.DataSource = null;
            listBox1.Enabled = false;
            monthCalendar1.Enabled = true;
            button1.Hide();
            monthCalendar1.DateSelected += new DateRangeEventHandler(monthCalendar1_DateChanged);
            label7.Text = "Явившиеся пациенты: 0";
            label6.Text = "Неявившиеся пациенты: 0";

            ShowDoctorsStatistic?.Invoke(this, new DoctorEventArgs(Editor.GetId(comboBox1.SelectedItem.ToString())));


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

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            chosenDate = e.Start.Date;
            button1.Hide();
            listBox1.DataSource = null;
            listBox1.Enabled = true;

            DateChange?.Invoke(this, new DateChangedEventArgs(Editor.GetId(comboBox1.SelectedItem.ToString()), chosenDate));
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
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex != -1)
            {
                ShowMedicalCard SMC = new ShowMedicalCard(Editor.GetId(listBox1.SelectedItem.ToString()));
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
