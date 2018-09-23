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
    public interface INewSpecialization
    {
        event EventHandler NewSpecializationLoad;
        event EventHandler DoctorsFill;
        event EventHandler<SpecializationEventArgs> AddNewSpecialization;
        event EventHandler<ScheduleEventArgs> AddNewSchedule;
        event EventHandler<DoctorsTimeEventArgs> AddNewDoctorsSchedule;

        void SetSpecializations(IEnumerable specializations);
        void SetDoctors(IEnumerable doctors);
    }

    public partial class NewSpecialization : Form, INewSpecialization
    {
        public event EventHandler NewSpecializationLoad;
        public event EventHandler DoctorsFill;
        public event EventHandler<SpecializationEventArgs> AddNewSpecialization;
        public event EventHandler<ScheduleEventArgs> AddNewSchedule;
        public event EventHandler<DoctorsTimeEventArgs> AddNewDoctorsSchedule;

        private IEnumerable Doctors;
        private IEnumerable Specializations;

        private bool isOpenedFromTO = false;
        private bool IsOpenedFromRF = false;

        public NewSpecialization()
        {
            InitializeComponent();
        }

        public NewSpecialization(bool isOpenedFromTO, bool IsOpenedFromRF)
        {
            InitializeComponent();
            this.isOpenedFromTO = isOpenedFromTO;
            this.IsOpenedFromRF = IsOpenedFromRF;
        }

        private void NewSpecialization_Load(object sender, EventArgs e)
        {
            NewSpecializationLoad?.Invoke(this, EventArgs.Empty);

            comboBox1.DataSource = Specializations;
            comboBox3.DataSource = Specializations;
            SetEnable(true);
            radioButton1.Select();
        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            SetEnable(true);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            SetEnable(false);
            maskedTextBox1.Enabled = false;
            maskedTextBox2.Enabled = false;
            button2.Enabled = false;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            bool save = true;
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "Поле должно быть заполнено");
                MessageBox.Show("Введите наименование специальности", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                save = false;
            }
            else
            {
                foreach (var specialization in Specializations)
                {
                    if (PolyclinicBL.Editor.GetSpecialization(specialization.ToString()) == textBox1.Text)
                    {
                        MessageBox.Show("Данная специальность уже имеется", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        save = false;
                        break;
                    }
                }
            }
            if (save)
            {
                AddNewSpecialization?.Invoke(this, new SpecializationEventArgs(textBox1.Text));
                NewSpecializationLoad?.Invoke(this, EventArgs.Empty);

                comboBox1.DataSource = Specializations;
                comboBox3.DataSource = Specializations;

                textBox1.Clear();
                MessageBox.Show("Специальность успешно добавлена!", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            maskedTextBox1.Enabled = true;
            maskedTextBox2.Enabled = true;
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool save = true;
            if (maskedTextBox1.Text == $"00:00-00:00")
            {
                MessageBox.Show("Введите режим работы!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                save = false;
            }
            else if (maskedTextBox2.Text == $"00")
            {
                MessageBox.Show("Введите интервал работы!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                save = false;
            }
            if (save)
            {
                AddNewSchedule?.Invoke(this, new ScheduleEventArgs(Editor.GetId(comboBox1.SelectedItem.ToString()), maskedTextBox1.Text, Int32.Parse(maskedTextBox2.Text)));
                
                MessageBox.Show("Данные успешно добавлены", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                radioButton1.Select(); //
                radioButton2.Select(); // Clear
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            SetEnableR3(true);
        }


        private void SetEnable(bool b)
        {
            textBox1.Clear();
            maskedTextBox1.Text = $"00:00-00:00";
            maskedTextBox2.Text = $"00";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            comboBox5.Text = "";
            button1.Enabled = b;
            textBox1.Enabled = b;
            comboBox1.Enabled = !b;
            button2.Enabled = !b;
            maskedTextBox1.Enabled = !b;
            maskedTextBox2.Enabled = !b;
            button3.Enabled = false;
            comboBox2.Enabled = false;
            comboBox3.Enabled = false;
            comboBox4.Enabled = false;
            comboBox5.Enabled = false;

        }
        private void SetEnableR3(bool b)
        {
            textBox1.Clear();
            maskedTextBox1.Text = $"00:00-00:00";
            maskedTextBox2.Text = $"00";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            comboBox5.Text = "";
            button1.Enabled = !b;
            textBox1.Enabled = !b;
            comboBox1.Enabled = !b;
            button2.Enabled = !b;
            maskedTextBox1.Enabled = !b;
            maskedTextBox2.Enabled = !b;
            button3.Enabled = !b;
            comboBox2.Enabled = !b;
            comboBox3.Enabled = b;
            comboBox4.Enabled = !b;
            comboBox5.Enabled = !b;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetEnableR3(true);

            comboBox2.Enabled = true;
            comboBox2.Items.Clear();
            comboBox4.Items.Clear();
            comboBox5.Items.Clear();
            
            if (comboBox4.Items.Count == 0)
            {
                MessageBox.Show("Для этой специальности не указан режим работы", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox2.Enabled = false;
            }

            DoctorsFill?.Invoke(this, EventArgs.Empty);

            if (comboBox3.Items.Count == 0)
            {
                MessageBox.Show("Не найдено врачей с данной специальностью", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox2.Enabled = false;
            }
            else comboBox2.Enabled = true;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox5.Enabled = false;
            comboBox4.Enabled = true;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox5.Enabled = true;
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            button3.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string schedule = comboBox4.SelectedItem.ToString();
            int interval = Convert.ToInt32(comboBox5.Text);

            AddNewDoctorsSchedule?.Invoke(this, new DoctorsTimeEventArgs(Editor.GetId(comboBox2.SelectedItem.ToString()), schedule, interval));
                    
            MessageBox.Show("Время работы врача успешно установлено", "Готово!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (isOpenedFromTO)
            {
                TicketOrderForm ticketOrderForm = this.Owner as TicketOrderForm;
                ticketOrderForm.RefreshList(schedule, interval);
                Close();
            }

            radioButton1.Select();
        }

        private void NewSpecialization_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (IsOpenedFromRF)
            {
                RegistersForm RF = this.Owner as RegistersForm;
                RF.RefreshComboboxes('s');
            }
        }

        #region Interface implementation
        public void SetSpecializations(IEnumerable specializations)
        {
            if(specializations is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(specializations)));
            }

            Specializations = specializations;
        }

        public void SetDoctors(IEnumerable doctors)
        {
            if(doctors is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(doctors)));
            }

            Doctors = doctors;
        }
        #endregion
    }
}
