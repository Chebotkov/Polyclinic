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
    public interface INewDoctorView
    {
        event EventHandler NewDoctorLoad;
        event EventHandler<EntityIdEventArgs> SpecializationSelect;
        event EventHandler RoomsRegisterOpen;
        event EventHandler<EntityIdEventArgs> IsRoomFree;
        event EventHandler<NewDoctorEventArgs> DoctorCreate;
        void SetRegionsAndSpecializations(IEnumerable regions, IEnumerable specializations);
        void SetRooms(IEnumerable rooms);
        void SetInformationAboutRoom(bool isFree);
        void RefreshRooms();

        IRoomsRegisterView RoomsRegisterView { get; }
    }

    public partial class NewDoctor : Form, INewDoctorView
    {
        List<TextBox> TextBoxes = new List<TextBox>();

        public event EventHandler NewDoctorLoad;
        public event EventHandler<EntityIdEventArgs> SpecializationSelect;
        public event EventHandler<EntityIdEventArgs> IsRoomFree;
        public event EventHandler RoomsRegisterOpen;
        public event EventHandler<NewDoctorEventArgs> DoctorCreate;
        public IRoomsRegisterView RoomsRegisterView { get; private set; }

        private bool isRoomFree = false;
        private bool IsOpenedFromRF = false;

        public NewDoctor()
        {
            InitializeComponent();
        }

        public NewDoctor(bool b)
        {
            InitializeComponent();
            IsOpenedFromRF = b;
        }

        #region Actions 
        private void NewDoctor_Load(object sender, EventArgs e)
        {
            NewDoctorLoad?.Invoke(this, EventArgs.Empty);

            TextBoxes.Add(textBox1);
            TextBoxes.Add(textBox2);
            TextBoxes.Add(textBox3);
            comboBox2.Enabled = false;
            comboBox3.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            bool save = true;
            foreach (TextBox TB in TextBoxes)
            {
                if (String.IsNullOrEmpty(TB.Text))
                {
                    errorProvider1.SetError(TB, "Поле должно быть заполнено!");
                    save = false;
                }
            }

            if (comboBox1.SelectedItem == null)
            {
                errorProvider1.SetError(comboBox1, "Выберите специализацию!");
                save = false;
            }
            else if (comboBox2.SelectedItem == null && comboBox1.Text == "Терапевт")
            {
                errorProvider1.SetError(comboBox2, "Выберите участок!");
                save = false;
            }

            if (comboBox3.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите кабинет", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errorProvider1.SetError(comboBox3, "Выберите кабинет!");
                save = false;
            }
            else
            {
                IsRoomFree?.Invoke(this, new EntityIdEventArgs(Int32.Parse(comboBox3.SelectedItem.ToString())));
                if (!isRoomFree)
                {
                    MessageBox.Show("Данный кабинет уже закреплён за другим врачом", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    save = false;
                }
            }

            if (save)
            {
                PolyclinicBL.Doctor doctor = new Doctor
                {
                    LastName = textBox1.Text,
                    FirstName = textBox2.Text,
                    Patronymic = textBox3.Text,
                    Specialization = Editor.GetId(comboBox1.SelectedItem.ToString()),
                    Room = Convert.ToInt32(comboBox3.SelectedItem),
                    Schedule = "00:00-00:00",
                    Interval = 0
                };

                if (Editor.GetSpecialization(comboBox1.SelectedItem.ToString()) == "Терапевт")
                {
                    doctor.Region = Editor.GetId(comboBox2.SelectedItem.ToString());
                }

                DoctorCreate?.Invoke(this, new NewDoctorEventArgs(doctor));
                Close();
                MessageBox.Show("Данные о враче успешно добавлены.", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SpecializationSelect?.Invoke(this, new EntityIdEventArgs(Editor.GetId(comboBox1.SelectedItem.ToString())));

            comboBox3.Enabled = true;

            if (Editor.GetSpecialization(comboBox1.SelectedItem.ToString()) == "Терапевт") comboBox2.Enabled = true;
            else
            {
                comboBox2.Text = "";
                comboBox2.Enabled = false;
            }

            if (comboBox3.Items.Count == 0)
            {
                MessageBox.Show("за данной специальностью нет закреплённых кабинетов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                RoomsRegister RR = new RoomsRegister(true);
                RoomsRegisterView = RR;
                RR.Owner = this;
                RoomsRegisterOpen?.Invoke(this, EventArgs.Empty);
                RR.ShowDialog();
            }
        }

        private void NewDoctor_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (IsOpenedFromRF)
            {
                RegistersForm RF = this.Owner as RegistersForm;
                RF.RefreshComboboxes('d');
            }
        }
        #endregion

        #region Interface implementation
        public void RefreshRooms()
        {
            SpecializationSelect?.Invoke(this, new EntityIdEventArgs(Editor.GetId(comboBox1.SelectedItem.ToString())));
        }

        public void SetRooms(IEnumerable rooms)
        {
            if (rooms is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(rooms)));
            }

            comboBox3.DataSource = rooms;
        }

        public void SetRegionsAndSpecializations(IEnumerable regions, IEnumerable specializations)
        {
            if (regions is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(regions)));
            }

            if (specializations is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(specializations)));
            }

            comboBox2.DataSource = regions;
            comboBox1.DataSource = specializations;

            comboBox2.Text = "";
        }

        public void SetInformationAboutRoom(bool isFree)
        {
            isRoomFree = isFree;
        }
        #endregion
    }
}
