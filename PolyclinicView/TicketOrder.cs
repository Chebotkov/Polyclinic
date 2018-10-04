using PolyclinicDBManager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using PolyclinicBL;

namespace PolyclinicView
{
    public interface ITicketOrderView
    {
        event EventHandler TicketOrderFormLoad;
        event EventHandler<TicketEventArgs> TicketOrder;
        event EventHandler<PatientsIdAndSpecializationNameEventArgs> SpecializationChoise;
        event EventHandler<EntityIdEventArgs> DoctorsSheduleCheck;
        event EventHandler NewSpecializationOpen;

        void FillFormWithSP(IEnumerable specializations, IEnumerable patients);
        void FillDoctorsList(IEnumerable doctors);
        void SetChosenDoctor(object doctor);
        void SetOrderedTickets(object tickets);

        PolyclinicDBManager.Doctor ChosenDoctor { get; }
        List<PolyclinicBL.Ticket> OrderedTickets { get; }
        INewSpecialization INewSpecializationRef { get; }
    }

    public partial class TicketOrderForm : Form, ITicketOrderView
    {
        public event EventHandler<TicketEventArgs> TicketOrder;
        public event EventHandler TicketOrderFormLoad;
        public event EventHandler<PatientsIdAndSpecializationNameEventArgs> SpecializationChoise;
        public event EventHandler<EntityIdEventArgs> DoctorsSheduleCheck;
        public event EventHandler NewSpecializationOpen;

        public PolyclinicDBManager.Doctor ChosenDoctor { get; private set; }
        public List<PolyclinicBL.Ticket> OrderedTickets { get; private set; }
        public INewSpecialization INewSpecializationRef { get; private set; }
        string chosenDate;

        public TicketOrderForm()
        {
            InitializeComponent();
        }

        #region Actions
        private void TicketOrderForm_Load(object sender, EventArgs e)
        {
            TicketOrderFormLoad?.Invoke(this, EventArgs.Empty);
            
            monthCalendar1.MinDate = DateTime.Today;
            button1.Enabled = false;
            monthCalendar1.Enabled = false;
            comboBox3.Enabled = false;
            comboBox4.Enabled = false;
            domainUpDown1.Enabled = false;
            comboBox1.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            errorProvider1.Clear();
        }

        /// <summary>
        /// Triggered when patient was chosen.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">EventArgs</param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Enabled = true;
            comboBox4.Enabled = false;
            monthCalendar1.Enabled = false;
            domainUpDown1.Enabled = false;
            button1.Enabled = false;
            comboBox3.Text = "";
            comboBox4.Text = "";
            domainUpDown1.Text = "";
            domainUpDown1.Items.Clear();
        }

        /// <summary>
        /// Triggered when specialization was chosen.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">EventArgs</param>
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

            SpecializationChoise?.Invoke(this, new PolyclinicBL.PatientsIdAndSpecializationNameEventArgs(PolyclinicBL.Editor.GetId(comboBox1.SelectedItem.ToString()), PolyclinicBL.Editor.GetId(comboBox3.SelectedItem.ToString())));

            monthCalendar1.Enabled = false;
            domainUpDown1.Enabled = false;
            button1.Enabled = false;
            comboBox4.Text = "";
            domainUpDown1.Text = "";
            domainUpDown1.Items.Clear();
            
            if(comboBox4.Items.Count == 0)
            {
                MessageBox.Show("Не найдено врачей с данной специализацией", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBox4.Enabled = false;
            }
            else comboBox4.Enabled = true;
        }

        /// <summary>
        /// Triggered when date was chosen.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">EventArgs</param>
        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            chosenDate = e.Start.ToShortDateString();

            domainUpDown1.Text = "";
            domainUpDown1.Items.Clear();

            var AvailableTime = SheduleWorker.GetAvailableTime(Editor.GetId(comboBox1.SelectedItem.ToString()), Editor.GetId(comboBox4.SelectedItem.ToString()), ChosenDoctor.Interval, ChosenDoctor.Shedule, chosenDate, OrderedTickets);

            if (AvailableTime.Count <= 0)
            {
                domainUpDown1.Enabled = false;
                MessageBox.Show("Талон не может быть заказан на выбранный день", "Извините", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                domainUpDown1.Items.AddRange(AvailableTime);
                domainUpDown1.Enabled = true;
                domainUpDown1.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Triggered when ticket was ordered.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">EventArgs</param>
        private void button1_Click(object sender, EventArgs e)
        {
            bool save = true;
            if (String.IsNullOrEmpty(chosenDate) || domainUpDown1.SelectedIndex == -1)
            {
                MessageBox.Show("Назначьте дату/время!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                save = false;
            }

            if (save)
            {
                TicketOrder?.Invoke(this, new TicketEventArgs(Editor.GetId(comboBox1.SelectedItem.ToString()), Editor.GetId(comboBox4.SelectedItem.ToString()), chosenDate, domainUpDown1.Text));
                MessageBox.Show("Талон Заказан.", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
        }

        /// <summary>
        /// Changes doctors schedule. Called from "NewSpecialization" form.
        /// </summary>
        /// <param name="schedule">Doctors schedule.</param>
        /// <param name="interval">Interval between doctors visiting.</param>
        public void RefreshList(string schedule, int interval)
        {
            ChosenDoctor.Shedule = schedule;
            ChosenDoctor.Interval = interval;
        }


        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        /// <summary>
        /// Triggered when doctor was chosen. 
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">EventArgs</param>
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool order = true;

            DoctorsSheduleCheck?.Invoke(this, new EntityIdEventArgs(Editor.GetId(comboBox4.SelectedItem.ToString())));

            if (ChosenDoctor.Shedule == "00:00-00:00" || ChosenDoctor.Interval == 0)
            {
                if (DialogResult.OK == MessageBox.Show("Для данного врача не назначено время приёма", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error))
                {
                    NewSpecialization NS = new NewSpecialization(true, false);
                    NS.Owner = this;
                    INewSpecializationRef = NS;
                    NewSpecializationOpen?.Invoke(this, EventArgs.Empty);
                    NS.ShowDialog();
                }
                order = false;
            }

            if (order)
            {
                monthCalendar1.DateSelected += monthCalendar1_DateChanged;
                monthCalendar1.Enabled = true;
                domainUpDown1.Text = "";
                domainUpDown1.Enabled = false;
                domainUpDown1.Items.Clear();
                button1.Enabled = false;
            }
        }
        #endregion

        #region Interface implementation

        public void FillFormWithSP(IEnumerable specializations, IEnumerable patients)
        {
            comboBox1.DataSource = patients;
            comboBox1.Text = "";
            comboBox3.DataSource = specializations;
            comboBox3.Text = "";
        }
        
        public void FillDoctorsList(IEnumerable doctors)
        {
            comboBox4.DataSource = doctors;
            comboBox4.Text = "";
        }

        public void SetChosenDoctor(object doctor)
        {
            if (doctor is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(doctor)));
            }
            
            ChosenDoctor = doctor as PolyclinicDBManager.Doctor;

            if (ChosenDoctor == null)
            {
                throw new InvalidCastException(String.Format("Can't cast {0} to {1}", nameof(doctor), typeof(PolyclinicDBManager.Doctor)));
            }
        }

        public void SetOrderedTickets(object tickets)
        {
            if(tickets is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(tickets)));
            }

            OrderedTickets = tickets as List<PolyclinicBL.Ticket>;

            if (OrderedTickets == null)
            {
                throw new InvalidCastException(String.Format("Can't cast {0} to {1}", nameof(tickets), typeof(List<PolyclinicBL.Ticket>)));
            }
        }

        #endregion
    }
}
