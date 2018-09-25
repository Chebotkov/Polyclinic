using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PolyclinicBL;

namespace PolyclinicView
{
    public interface IListRecordingView
    {
        event EventHandler MedicalCardOpen; 
        event EventHandler ListRecordingFormLoad;
        event EventHandler<DateSelectedEventArgs> DateChange;

        IShowMedicalCard showMedicalCard { get; }

        void SetDoctors(IEnumerable doctors);
        void SetPatients(IEnumerable patients);
    }

    public partial class ListRecordingForm : Form, IListRecordingView
    {
        public event EventHandler MedicalCardOpen;
        public event EventHandler ListRecordingFormLoad;
        public event EventHandler<DateSelectedEventArgs> DateChange;
        
        public IShowMedicalCard showMedicalCard { get; private set; }

        public ListRecordingForm()
        {
            InitializeComponent();
            monthCalendar1.MinDate = DateTime.Today;
            label1.Visible = false;
            button2.Enabled = false;
            monthCalendar1.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ListRecordingForm_Load(object sender, EventArgs e)
        {
            ListRecordingFormLoad?.Invoke(this, EventArgs.Empty);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                ShowMedicalCard SMC = new ShowMedicalCard(Editor.GetId(listBox1.SelectedItem.ToString()));
                showMedicalCard = SMC;
                MedicalCardOpen?.Invoke(this, EventArgs.Empty);
                SMC.Show();
            }
            else MessageBox.Show("Выберите пациента из списка ниже!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            //This important for something. But I forgot why.
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;
            monthCalendar1.Visible = true;
            label1.Visible = true;
            listBox1.Text = "";
            monthCalendar1.DateSelected += new DateRangeEventHandler(monthCalendar1_DateChanged);
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0 && comboBox1.SelectedIndex != -1) MessageBox.Show("В данный день у вас нет пациентов", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            listBox1.Text = "";
            string ChosenDate = e.Start.ToShortDateString();

            DateChange?.Invoke(this, new DateSelectedEventArgs(Editor.GetId(comboBox1.SelectedItem.ToString()), ChosenDate));
        }

        public void SetDoctors(IEnumerable doctors)
        {
            if (doctors is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(doctors)));
            }

            comboBox1.DataSource = doctors;
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
