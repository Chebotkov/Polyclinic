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
        INewDoctorView INewDoctorViewRef { get; }
        IRegistrationView registrationView { get; }
        INewSpecialization INewSpecializationRef { get; }
        event EventHandler<RegionsEventHandler> AddNewRegion;
        event EventHandler NewDoctor_Click;
        event EventHandler Registration_Click;
        event EventHandler NewSpecialization_Click;
        event EventHandler GetEntities;
        event EventHandler DoctorsGet;
        event EventHandler PatientsGet;
        event EventHandler SpecializationsGet;
        event EventHandler<StreetsEventHandler> FillStreets;
        event EventHandler<EntityIdEventArgs> DoctorsListGet;
        event EventHandler<EntityIdEventArgs> PatientsInfoGet;
        event EventHandler<EntityIdEventArgs> DoctorsListGetBySpecialization;
        event EventHandler<EntityIdEventArgs> RegionInfoGet;

        void SetEntities(IEnumerable Patients, IEnumerable Doctors, IEnumerable Specializations, IEnumerable Regions);
        void SetStreets(IEnumerable Streets);
        void SetRegions(IEnumerable regions);
        void SetEntity(IEnumerable entities, char entity);
        void SetData(string data);
    }

    public partial class RegistersForm : Form, IRegistersView
    {
        private IEnumerable Patients;
        private IEnumerable Doctors;
        private IEnumerable Regions;
        private IEnumerable Specializations;
        private IEnumerable Streets;

        public INewDoctorView INewDoctorViewRef { get; private set; }
        public IRegistrationView registrationView { get; private set; }
        public INewSpecialization INewSpecializationRef { get; private set; }
        public event EventHandler<RegionsEventHandler> AddNewRegion;
        public event EventHandler NewDoctor_Click;
        public event EventHandler NewSpecialization_Click;
        public event EventHandler GetEntities;
        public event EventHandler DoctorsGet;
        public event EventHandler PatientsGet;
        public event EventHandler SpecializationsGet;
        public event EventHandler Registration_Click;
        public event EventHandler<StreetsEventHandler> FillStreets;
        public event EventHandler<EntityIdEventArgs> DoctorsListGet;
        public event EventHandler<EntityIdEventArgs> PatientsInfoGet;
        public event EventHandler<EntityIdEventArgs> DoctorsListGetBySpecialization;
        public event EventHandler<EntityIdEventArgs> RegionInfoGet;

        public RegistersForm()
        {
            InitializeComponent();
        }

        #region Actions
        private void RegistersForm_Load(object sender, EventArgs e)
        {
            GetEntities?.Invoke(this, EventArgs.Empty);
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
                registrationView = RF;
                RF.Owner = this;
                Registration_Click?.Invoke(this, EventArgs.Empty);
                RF.Show();
            }
            if (radioButton1.Checked)
            {
                NewSpecialization NS = new NewSpecialization(false, true);
                INewSpecializationRef = NS;
                NS.Owner = this;
                NewSpecialization_Click?.Invoke(this, EventArgs.Empty);
                NS.ShowDialog();
            }

            if (radioButton2.Checked)
            {
                save = true;
                string regionName = "";
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

                else if (!(Editor.GetRegion(maskedTextBox1.Text, out regionName)))
                {
                    MessageBox.Show("Укажите имя участка", "Прошу тебя, уважаемый", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    save = false;
                }

                else if (!Editor.IsEnteredRegionCorrect(maskedTextBox1.Text, Regions))
                {
                    save = false;
                    MessageBox.Show("Название районов не совпадает. Если вы хотели добавить новый участок, укажите номер, несодержащийся в списке.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    FillStreets?.Invoke(this, new StreetsEventHandler(PolyclinicBL.Editor.GetNum(maskedTextBox1.Text)));

                    foreach (string street in Streets)
                    {
                        if (street.ToLower() == (Editor.GetStreet(maskedTextBox2.Text)).ToLower())
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
                    string RegName = regionName;
                    string street = (Editor.GetStreetForRegion(maskedTextBox2.Text));
                    MessageBox.Show("Участок с адресом успешно добавлены!", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        
                    AddNewRegion?.Invoke(this, new RegionsEventHandler(RegNum, RegName, street));                    
                }

                radioButton2.Select();
                radioButton1.Select();
                maskedTextBox1.Text = "";
                maskedTextBox2.Text = "";
            }
            if (radioButton3.Checked)
            {
                NewDoctor ND = new NewDoctor(true);
                ND.Owner = this;
                INewDoctorViewRef = ND;
                NewDoctor_Click?.Invoke(this, EventArgs.Empty);
                ND.ShowDialog();
            }

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //Regions
            comboBox1.DataSource = Regions;

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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Clear();
            if (radioButton3.Checked)
            {
                DoctorsListGet?.Invoke(this, new EntityIdEventArgs(Editor.GetId(comboBox1.SelectedItem.ToString())));
            }

            if (radioButton2.Checked)
            {
                RegionInfoGet?.Invoke(this, new EntityIdEventArgs(Editor.GetId(comboBox1.SelectedItem.ToString())));
            }

            if (radioButton1.Checked)
            {
                DoctorsListGetBySpecialization?.Invoke(this, new EntityIdEventArgs(Editor.GetId(comboBox1.SelectedItem.ToString())));
            }

            if (radioButton5.Checked)
            {
                PatientsInfoGet?.Invoke(this, new EntityIdEventArgs(Editor.GetId(comboBox1.SelectedItem.ToString())));
            }
        }
        #endregion

        #region Necessary methods
        /// <summary>
        /// Refreshes collection by parameter "c".
        /// </summary>
        /// <param name="entity">Certain entity: d - Doctor, p - Patient, s - Specialization.</param>
        /// <exception cref="ArgumentException">Throws when <paramref name="entity"/> isn't valid.</exception>
        public void RefreshComboboxes(char entity)
        {
            switch (entity)
            {
                case 'd':
                    {
                        DoctorsGet?.Invoke(this, EventArgs.Empty);
                        break;
                    }
                case 'p':
                    {
                        PatientsGet?.Invoke(this, EventArgs.Empty);
                        break;
                    }
                case 's':
                    {
                        SpecializationsGet?.Invoke(this, EventArgs.Empty);
                        break;
                    }
                default:
                    throw new ArgumentException(String.Format("{0} is not valid. There is only 'd', 'p' and 'c' parametres.", nameof(entity)));
            }
            radioButton2.Select();
            radioButton1.Select();
        }

        private void RefreshFields()
        {
            comboBox1.Text = "";
            errorProvider1.Clear();
            textBox1.Clear();
            textBox2.Clear();
        }
        #endregion

        #region Interface implementation
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

        public void SetRegions(IEnumerable regions)
        {
            if (regions is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(regions)));
            }

            Regions = regions;
        }

        public void SetData(string data)
        {
            if (data is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(data)));
            }
            
            textBox2.Text = data;
        }

        /// <summary>
        /// Sets collection by parameter "entity".
        /// </summary>
        /// <param name="entities">Collection of entities.</param>
        /// <param name="entity">Certain entity: d - Doctor, p - Patient, s - Specialization.</param>
        /// <exception cref="ArgumentNullException">Throws when <paramref name="entities"/> is null.</exception>
        /// <exception cref="ArgumentException">Throws when <paramref name="entity"/> isn't valid.</exception>
        public void SetEntity(IEnumerable entities, char entity)
        {
            if (entities is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(entities)));
            }

            switch (entity)
            {
                case 'd':
                    {
                        Doctors = entities;
                        break;
                    }
                case 'p':
                    {
                        Patients = entities;
                        break;
                    }
                case 's':
                    {
                        Streets = entities;
                        break;
                    }
                default:
                    throw new ArgumentException(String.Format("{0} is not valid. There is only 'd', 'p' and 'c' parametres.", nameof(entity)));
            }
        }
        #endregion
    }
}
