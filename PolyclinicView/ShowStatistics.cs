﻿using PolyclinicBL;
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
        event EventHandler<EntityIdEventArgs> ShowDoctorsStatistic;

        void SetDoctors(IEnumerable doctors);
        void SetDoctorsStatistic(IEnumerable statistic);
        void SetPatients(IEnumerable patients);
    }

    public partial class ShowStatistics : Form, IShowStatistic
    {
        public event EventHandler<DateChangedEventArgs> DateChange;
        public event EventHandler MedicalCardOpen;
        public event EventHandler ShowStatisticsLoad;
        public event EventHandler<EntityIdEventArgs> ShowDoctorsStatistic;

        public IShowMedicalCard iShowMedicalCard { get; private set; }
        private IEnumerable Statistics;
        private DateTime chosenDate = DateTime.Today;

        private bool areDoctorsSets = false;
        private bool isStatisticSets = false;

        public ShowStatistics()
        {
            InitializeComponent();
        }

        #region Actions
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
            if (!areDoctorsSets)
            {
                listBox1.DataSource = null;
                listBox1.Enabled = false;
                monthCalendar1.Enabled = true;
                button1.Hide();
                monthCalendar1.DateSelected += new DateRangeEventHandler(monthCalendar1_DateChanged);
                label7.Text = "Явившиеся пациенты: 0";
                label6.Text = "Неявившиеся пациенты: 0";

                ShowDoctorsStatistic?.Invoke(this, new EntityIdEventArgs(Editor.GetId(comboBox1.SelectedItem.ToString())));


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

                    Arrived += statistic.ArrivedPatients;
                    NonArrived += statistic.NonArrivedPatients;
                }

                label4.Text = "Явившиеся пациенты: " + Arrived;
                label5.Text = "Неявившиеся пациенты: " + NonArrived;
            }

            areDoctorsSets = false;
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            if (!isStatisticSets)
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

            isStatisticSets = false;
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
        #endregion

        #region Interface implementation.
        public void SetDoctors(IEnumerable doctors)
        {
            if (doctors is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(doctors)));
            }

            areDoctorsSets = true;
            comboBox1.DataSource = doctors;
            comboBox1.Text = "";
        }

        public void SetDoctorsStatistic(IEnumerable statistic)
        {
            if (statistic is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(statistic)));
            }

            isStatisticSets = true;
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
        #endregion

    }
}
