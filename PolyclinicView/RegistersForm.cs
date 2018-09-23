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
    public interface IRegistersView
    {
        INewDoctorView INewDoctorViewRef { get; set; }
        INewSpecialization INewSpecializationRef { get; set; }
        event EventHandler<RegionsEventHandler> AddNewRegion;
        event EventHandler<StreetsEventHandler> AddNewStreet;
        event EventHandler NewDoctor_Click;
        event EventHandler NewSpecialization_Click;
        event EventHandler GetEntities;
        event EventHandler<StreetsEventHandler> FillStreets;

        void SetEntities(IEnumerable Patients, IEnumerable Doctors, IEnumerable Specializations, IEnumerable Regions);
        void SetStreets(IEnumerable Streets);
    }

    public partial class RegistersForm : Form, IRegistersView
    {
        private IEnumerable Patients;
        private IEnumerable Doctors;
        private IEnumerable Regions;
        private IEnumerable Specializations;
        private IEnumerable Streets;

        public INewDoctorView INewDoctorViewRef { get; set; }
        public INewSpecialization INewSpecializationRef { get; set; }
        public event EventHandler<RegionsEventHandler> AddNewRegion;
        public event EventHandler<StreetsEventHandler> AddNewStreet;
        public event EventHandler NewDoctor_Click;
        public event EventHandler NewSpecialization_Click;
        public event EventHandler GetEntities;
        public event EventHandler<StreetsEventHandler> FillStreets;

        public RegistersForm()
        {
            InitializeComponent();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            //Doctors
            label1.Hide();
            textBox1.Hide();
            maskedTextBox1.Hide();

            maskedTextBox2.Hide();
            label2.Text = "Информация о враче: ";
            label3.Text = "Выберите врача:";
            label4.Show();
            label4.Text = "Добавить врача: ";

            textBox1.Location = new Point(19, 254);
            textBox1.Size = new Size(400, 20);
            label4.Location = new Point(160, 249);
            button1.Location = new Point(281, 238);
            button1.Size = new Size(83, 37);
            button1.Text = "Добавить";
            textBox1.Enabled = false;
            RefreshFields();

            comboBox1.DataSource = Doctors;
        }

        private void RegistersForm_Load(object sender, EventArgs e)
        {
            GetEntities?.Invoke(this, EventArgs.Empty);
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            //Patients
            label1.Hide();
            textBox1.Hide();
            maskedTextBox1.Hide();
            maskedTextBox2.Hide();
            label2.Text = "Информация о пациенте: ";
            label3.Text = "Выберите пациента:";
            label4.Show();
            label4.Text = "Зарегистрировать пациента: ";
            textBox1.Size = new Size(400, 20);
            textBox1.Location = new Point(19, 254);
            label4.Location = new Point(130, 249);
            button1.Location = new Point(321, 238);
            button1.Size = new Size(83, 37);
            button1.Text = "Добавить";
            textBox1.Enabled = false;
            RefreshFields();

            comboBox1.DataSource = Patients;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool save;
            if (radioButton5.Checked)
            {
                RegistrationForm RF = new RegistrationForm(true);
                RF.Owner = this;
                RF.Show();
            }
            if (radioButton1.Checked)
            {
                NewSpecialization NS = new NewSpecialization(false, true);
                NS.Owner = this;
                INewSpecializationRef = NS;
                NS.ShowDialog();
                NewSpecialization_Click?.Invoke(this, EventArgs.Empty);
            }

            if (radioButton2.Checked)
            {
                save = true;
                if (maskedTextBox2.Text == "ул.")
                {
                    MessageBox.Show("Введите улицу", "Прошу тебя, уважаемый", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    save = false;
                }
                else if (maskedTextBox1.Text == "№  .")
                {
                    MessageBox.Show("Укажите № участка", "Прошу тебя, уважаемый", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    save = false;
                }

                else if (PolyclinicBL.Editor.GetRegion(maskedTextBox1.Text) == "666")
                {
                    MessageBox.Show("Укажите имя участка", "Прошу тебя, уважаемый", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    save = false;
                }

                else if (PolyclinicBL.Editor.IsEnteredRegionCorrect(maskedTextBox1.Text, Regions) == -1)
                {
                    save = false;
                    MessageBox.Show("Название районов не совпадает. Если вы хотели добавить новый участок, укажите номер, несодержащийся в списке.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    FillStreets?.Invoke(this, new StreetsEventHandler(PolyclinicBL.Editor.GetNum(maskedTextBox1.Text)));

                    foreach (string street in Streets)
                    {
                        if (street.ToLower() == ("ул. " + Editor.GetStreet(maskedTextBox2.Text)).ToLower())
                        {
                            MessageBox.Show("Данный адрес уже имеется", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            save = false;
                            break;
                        }
                    }
                }
                if (save)
                {
                    int checkReg = PolyclinicBL.Editor.GetNum(maskedTextBox1.Text);
                    if (checkReg < 0)
                    {
                        MessageBox.Show("Не введён номер участка, либо указан неверный номер", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        save = false;
                    }
                }

                if (save)
                {
                    int RegNum = Editor.GetNum(maskedTextBox1.Text);
                    string RegName = Editor.GetRegion(maskedTextBox1.Text);
                    string street = ("ул. " + Editor.GetStreet(maskedTextBox2.Text));

                    bool exists = false;
                    foreach (var r in Regions)
                    {
                        if (RegNum == Editor.GetId(r.ToString()))
                        {
                            AddNewStreet?.Invoke(this, new StreetsEventHandler(RegNum, street));
                            exists = true;
                            break;
                        }
                    }

                    if (!exists)
                    {
                        comboBox1.Items.Add(RegNum + ". " + RegName);
                        AddNewRegion?.Invoke(this, new RegionsEventHandler(RegNum, RegName));
                    }
                    
                    textBox2.Text += "Участок №" + RegNum + ", ул. " + street + Environment.NewLine;
                    maskedTextBox2.Clear();
                    maskedTextBox1.Clear();
                    MessageBox.Show("Участок с адресом успешно добавлены!", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            if (radioButton3.Checked)
            {
                NewDoctor ND = new NewDoctor(true);
                ND.Owner = this;
                INewDoctorViewRef = ND;
                ND.ShowDialog();
                NewDoctor_Click?.Invoke(this, EventArgs.Empty);
            }

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //Regions
            label1.Show();
            label4.Show();
            textBox1.Hide();
            maskedTextBox1.Show();
            maskedTextBox2.Show();
            label1.Text = "Добавьте адрес: ";
            label1.Location = new Point(150, 233);
            label2.Text = "Адреса при участке, а также врачи-терапевты, закреплённые участком:";
            label3.Text = "Выберите № участка:";
            label4.Location = new Point(23, 233);
            label4.Text = "№ и имя участка:";

            textBox1.Location = new Point(150, 254);
            textBox1.Size = new Size(269, 20);
            button1.Location = new Point(433, 244);
            button1.Size = new Size(83, 37);
            button1.Text = "Добавить";
            textBox1.Enabled = true;
            RefreshFields();

            comboBox1.DataSource = Regions;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //Specializations
            label1.Hide();
            label1.Location = new Point(23, 233);
            textBox1.Hide();
            maskedTextBox2.Hide();
            maskedTextBox1.Hide();
            label1.Text = "Добавьте врачебную специальность: ";
            label2.Text = "Список врачей данной специальности:";
            label3.Text = "Выберите специальность:";
            label4.Hide();

            textBox1.Location = new Point(19, 254);
            textBox1.Size = new Size(400, 20);
            button1.Location = new Point(19, 244);
            button1.Size = new Size(497, 37);
            button1.Text = "Добавить/настроить специализацию";
            textBox1.Enabled = true;
            RefreshFields();

            comboBox1.DataSource = Specializations;
        }

        private void RefreshFields()
        {
            comboBox1.Text = "";
            errorProvider1.Clear();
            comboBox1.Items.Clear();
            textBox1.Clear();
            textBox2.Clear();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            textBox2.Clear();
            if (radioButton3.Checked && comboBox1.SelectedIndex != -1)
            {
                if (Doctors[comboBox1.SelectedIndex].Specialization != "Терапевт") textBox2.Text = Doctors[comboBox1.SelectedIndex].Name + " " + Doctors[comboBox1.SelectedIndex].LastName + " " + Doctors[comboBox1.SelectedIndex].Patronymic + "; " + Doctors[comboBox1.SelectedIndex].Specialization + "; № кабинета " + Doctors[comboBox1.SelectedIndex].Room + "; Время приёма: " + Doctors[comboBox1.SelectedIndex].AppointmentTime;
                else textBox2.Text = Doctors[comboBox1.SelectedIndex].Name + " " + Doctors[comboBox1.SelectedIndex].LastName + " " + Doctors[comboBox1.SelectedIndex].Patronymic + "; " + Doctors[comboBox1.SelectedIndex].Specialization + "; № кабинета " + Doctors[comboBox1.SelectedIndex].Room + "; № участка:" + Doctors[comboBox1.SelectedIndex].Region + "; Время приёма: " + Doctors[comboBox1.SelectedIndex].AppointmentTime;
            }
            if (radioButton2.Checked)
            {
                int i = 0;
                foreach (Doctor D in Doctors)
                {
                    if (D.Region == Convert.ToInt32(comboBox1.Text.Substring(0, comboBox1.Text.IndexOf('.'))))
                    {
                        if (i == 0)
                        {
                            textBox2.Text += "Врачи-терапевты, закреплённые за этим учатском:" + Environment.NewLine;
                            i++;
                        }
                        textBox2.Text += D.Name + " " + D.LastName + " " + D.Patronymic + "; № кабинета " + D.Room + Environment.NewLine;
                    }
                }
                if (i == 0) textBox2.Text += "За этим учатском нет закреплённых терапевтов" + Environment.NewLine;

                textBox2.Text += Environment.NewLine + "Адреса при участке:" + Environment.NewLine;
                foreach (Region r in Regions)
                {
                    if (r.RegNum == Convert.ToInt32(comboBox1.Text.Substring(0, comboBox1.Text.IndexOf('.'))))
                    {
                        foreach (string s in r.Addresses)
                        {
                            textBox2.Text += "Участок №" + r.RegNum + ", " + s + Environment.NewLine;
                        }
                        break;
                    }
                }
            }
            if (radioButton1.Checked)
            {
                foreach (Doctor D in Doctors)
                {
                    if (D.Specialization == comboBox1.SelectedItem.ToString())
                    {
                        if (comboBox1.SelectedItem.ToString() == "Терапевт") textBox2.Text += D.Name + " " + D.LastName + " " + D.Patronymic + "; " + D.Specialization + "; № кабинета: " + D.Room + "; Время приёма: " + D.AppointmentTime + "; Обслуживаемый участок:" + D.Region + Environment.NewLine;
                        else textBox2.Text += D.Name + " " + D.LastName + " " + D.Patronymic + "; " + D.Specialization + "; № кабинета " + D.Room + "; Время приёма: " + D.AppointmentTime + Environment.NewLine;
                    }
                }
            }
            if (radioButton5.Checked)
            {
                textBox2.Text = "ФИО: " + Patients[comboBox1.SelectedIndex].LastName + " " + Patients[comboBox1.SelectedIndex].Name + " " + Patients[comboBox1.SelectedIndex].PatronymicName + Environment.NewLine + "Пол:" + Patients[comboBox1.SelectedIndex].Gender + Environment.NewLine + "Дата рождения: " + Patients[comboBox1.SelectedIndex].Birth + Environment.NewLine + "№ участка:" + Patients[comboBox1.SelectedIndex].Region + ", Адрес:" + Patients[comboBox1.SelectedIndex].Address + Environment.NewLine + "Дата регистрации: " + Patients[comboBox1.SelectedIndex].RegistrationDate;
            }*/

        }

        public void RefreshComboboxes(char c)
        {
            comboBox1.Items.Clear();
            /*switch (c)
            {
                case 'd':
                    {
                        Doctors.Clear();
                        F.DoctorsListFilling(Doctors);
                        Doctors.Sort();
                        foreach (Doctor d in Doctors)
                        {
                            comboBox1.Items.Add(d.id + ". " + d.LastName + " " + d.Name + " " + d.Patronymic);
                        }
                        break;
                    }
                case 'p':
                    {
                        Patients.Clear();
                        F.PatientsListFilling(Patients);
                        foreach (Patient p in Patients)
                        {
                            comboBox1.Items.Add(p.id + ". " + p.LastName + " " + p.Name + " " + p.PatronymicName);
                        }
                        break;
                    }
                case 's':
                    {
                        Specializations.Clear();
                        F.SpecializationsFilling(Specializations);
                        foreach (Specialization s in Specializations)
                        {
                            comboBox1.Items.Add(s.SpecializationName);
                        }
                        break;
                    }
            }*/
        }

        public void SetEntities(IEnumerable Patients, IEnumerable Doctors, IEnumerable Specializations, IEnumerable Regions)
        {
            //Empty references
            this.Patients = Patients;
            this.Doctors = Doctors;
            this.Specializations = Specializations;
            this.Regions = Regions;
        }

        public void SetStreets(IEnumerable streets)
        {
            if (streets is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(streets)));
            }

            Streets = streets;
        }
    }
}
