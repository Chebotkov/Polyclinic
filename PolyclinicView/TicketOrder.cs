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
        event EventHandler SpecializationChoise;
        event EventHandler DoctorsSheduleCheck;

        void FillFormWithSP(IEnumerable specializations, IEnumerable patients);
        void FillDoctorsList(IEnumerable doctors);
        Doctor GetDoctorById(int DocId);
    }

    public partial class TicketOrderForm : Form, ITicketOrderView
    {
        public event EventHandler TicketOrderFormLoad;
        public event EventHandler SpecializationChoise;
        public event EventHandler DoctorsSheduleCheck;

        int ReplaceableDoc = -1;
        string ChosenDate;
        SortedDictionary<int, int> PatientsRegion = new SortedDictionary<int, int>();
        bool save = true;

        public TicketOrderForm()
        {
            InitializeComponent();
            monthCalendar1.MinDate = DateTime.Today;
        }

        private void TicketOrderForm_Load(object sender, EventArgs e)
        {
            ClearAll();
            TicketOrderFormLoad?.Invoke(this, EventArgs.Empty);
        }


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

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            monthCalendar1.Enabled = false;
            domainUpDown1.Enabled = false;
            button1.Enabled = false;
            comboBox4.Text = "";
            domainUpDown1.Text = "";
            domainUpDown1.Items.Clear();
            
            SpecializationChoise?.Invoke(this, new PolyclinicBL.PatientsIdAndSpecializationNameEventArgs(PolyclinicBL.Editor.GetId(comboBox1.SelectedItem.ToString()), PolyclinicBL.Editor.GetId(comboBox3.SelectedItem.ToString())));
            
            if(comboBox4.Items.Count == 0)
            {
                MessageBox.Show("Не найдено врачей с данной специализацией", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBox4.Enabled = false;
            }
            else comboBox4.Enabled = true;
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            ChosenDate = e.Start.ToShortDateString();

            domainUpDown1.Text = "";
            domainUpDown1.Items.Clear();

            var availaebleTime = SheduleWorker.GetAvailableTime(Editor.GetId(comboBox1.SelectedItem.ToString()), Editor.GetId(comboBox4.SelectedItem.ToString()), );

            if (availaebleTime.Count <= 0)
            {
                domainUpDown1.Enabled = false;
                MessageBox.Show("Талон не может быть заказан на выбранный день", "Извините", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                domainUpDown1.Items.AddRange(availaebleTime);
                domainUpDown1.Enabled = true;
                domainUpDown1.SelectedIndex = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(ChosenDate) || domainUpDown1.SelectedIndex == -1)
            {
                MessageBox.Show("Назначьте дату/время!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                save = false;
            }
            if (save)
            {
                //ticketTableAdapter.Insert(M.GetId(comboBox1), M.GetId(comboBox4), ChosenDate, domainUpDown1.Text, M.GetId(comboBox1), false);
                ClearAll();
                MessageBox.Show("Талон Заказан.", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
        }


        private void ClearAll()
        {
            button1.Enabled = false ;
            monthCalendar1.Enabled = false;
            comboBox3.Enabled = false;
            comboBox4.Enabled = false;
            domainUpDown1.Enabled = false;
            comboBox1.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            comboBox4.Items.Clear();
            errorProvider1.Clear();
        }

        public void RefreshList(string AT, int Interval)
        {
            ClearAll();
            //Doctors[ReplaceableDoc].AppointmentTime = AT;
            //Doctors[ReplaceableDoc].Interval = Interval;
        }


        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool order = true;

            Doctor doctor = GetDoctorById(PolyclinicBL.Editor.GetId(comboBox4.SelectedItem.ToString()));

            if (doctor.Shedule == "00:00-00:00" || doctor.Interval == 0)
            {
                if (DialogResult.OK == MessageBox.Show("Для данного врача не назначено время приёма", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error))
                {
                    //ReplaceableDoc = Doctors.FindIndex(0, FindById);
                    NewSpecialization NS = new NewSpecialization(true, false);
                    NS.Owner = this;
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

        public Doctor GetDoctorById(int DocId)
        {
            throw new NotImplementedException();
            //DoctorsSheduleCheck?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
