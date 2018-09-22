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

namespace PolyclinicView
{
    public interface INewSpecialization
    {
        event EventHandler NewSpecializationLoad;

        void SetSpecializations(IEnumerable specializations);
    }

    public partial class NewSpecialization : Form, INewSpecialization
    {
        public event EventHandler NewSpecializationLoad;

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
            /*F.DoctorsListFilling(Doctors);
            F.SpecializationsFilling(Specializations);
            */

            NewSpecializationLoad?.Invoke(this, EventArgs.Empty);

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
            /*else
            {
                foreach (Specialization s in Specializations)
                {
                    if (s.SpecializationName == textBox1.Text)
                    {
                        MessageBox.Show("Данная специальность уже имеется", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        save = false;
                        break;
                    }
                }
            }
            if (save)
            {
                comboBox1.Items.Add(textBox1.Text);
                comboBox3.Items.Add(textBox1.Text);
                SpecializationsTableAdapter.Insert(textBox1.Text);
                F.SpecializationsFilling(Specializations);
                textBox1.Clear();
                MessageBox.Show("Специальность успешно добавлена!", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }*/
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
            /*if (save)
            {
                {
                    int id=0;
                    foreach(Specialization s in Specializations)
                    {
                        if(s.SpecializationName == comboBox1.SelectedItem.ToString())
                        {
                            id = s.SpecId;
                            break;
                        }
                    }

                    Schedule sc = new Schedule()
                    {
                        SpecId = id,
                        AppointmentTime = maskedTextBox1.Text,
                        Interval = Convert.ToInt32(maskedTextBox2.Text)
                    };
                    doctorsTimeTableTable.Insert(id, sc.AppointmentTime, sc.Interval);
                    F.SpecializationsFilling(Specializations);
                    MessageBox.Show("Данные успешно добавлены", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    radioButton1.Select(); //
                    radioButton2.Select(); // Clear
                }
            }*/
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

            //M.SetInteravalsAndAppointmentTime(Specializations, comboBox3, comboBox4, comboBox5);

            if (comboBox4.Items.Count == 0)
            {
                MessageBox.Show("Для этой специальности не указан режим рыботы", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox2.Enabled = false;
            }

            /*foreach (Doctor d in Doctors)
            {
                if(d.Specialization == comboBox3.SelectedItem.ToString())
                {
                    comboBox2.Items.Add(d.id + ". " + d.LastName + " " + d.Name + " " + d.Patronymic);
                }
            }*/

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
            /*foreach(Doctor d in Doctors)
            {
                if(d.id == M.GetId(comboBox2))
                {
                    d.AppointmentTime = comboBox4.SelectedItem.ToString();
                    d.Interval = Convert.ToInt32(comboBox5.Text);
                    doctorsTableAdapter.UpdateQuery(d.Interval, d.AppointmentTime , d.id);
                    if(isOpenedFromTO)
                    {
                        TicketOrderForm ns = this.Owner as TicketOrderForm;
                        ns.RefreshList(d.AppointmentTime, d.Interval);
                    }
                    break;
                }
            }*/
            MessageBox.Show("Время работы врача успешно установлено", "Готово!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (isOpenedFromTO)
            {
                TicketOrderForm ticketOrderForm = this.Owner as TicketOrderForm;
                ticketOrderForm.RefreshList(comboBox4.SelectedItem.ToString(), Convert.ToInt32(comboBox5.Text));
                Close();
            }
            radioButton1.Select();
        }

        private void NewSpecialization_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(IsOpenedFromRF)
            {
                RegistersForm RF = this.Owner as RegistersForm;
                RF.RefreshComboboxes('s');
            }
        }

        public void SetSpecializations(IEnumerable specializations)
        {
            comboBox1.DataSource = specializations;
            comboBox3.DataSource = specializations;
        }
    }
}
