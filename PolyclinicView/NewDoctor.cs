using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolyclinicView 
{
    public interface INewDoctorView
    {

    }

    public partial class NewDoctor : Form, INewDoctorView
    {
        /*Database1DataSetTableAdapters.DoctorsTableAdapter doctorsTableAdapter = new Database1DataSetTableAdapters.DoctorsTableAdapter();
        public List<Doctor> Doctors = new List<Doctor>();
        public List<Specialization> Specializations = new List<Specialization>();
        public List<Region> Regions = new List<Region>();
        public List<Room> Rooms = new List<Room>();

        Methods M = new Methods();
        Filling F = new Filling();*/
        List<TextBox> TextBoxes = new List<TextBox>();
        bool save = true;
        bool IsOpenedFromRF = false;

        public NewDoctor()
        {
            InitializeComponent();
        }

        public NewDoctor(bool b)
        {
            InitializeComponent();
            IsOpenedFromRF = b;
        }

        private void NewDoctor_Load(object sender, EventArgs e)
        {
            /*F.DoctorsListFilling(Doctors);
            F.SpecializationsFilling(Specializations);
            F.RegionsFilling(Regions);
            F.RoomsFilling(Rooms);*/

            TextBoxes.Add(textBox1);
            TextBoxes.Add(textBox2);
            TextBoxes.Add(textBox3);
            comboBox2.Enabled = false;
            /*foreach (Specialization s in Specializations)
            {
                comboBox1.Items.Add(s.SpecializationName);
            }
            foreach (Region r in Regions)
            {
                comboBox2.Items.Add(r.RegNum + ". " + r.RegName);
            }*/
        }

        private void button1_Click(object sender, EventArgs e)
        {

            errorProvider1.Clear();
            save = true;
            foreach (TextBox TB in TextBoxes)
            {
                if (String.IsNullOrEmpty(TB.Text))
                {
                    errorProvider1.SetError(TB, "Поле должно быть заполнено!");
                    save = false;
                }
            }

            /*foreach(Doctor D in Doctors)
            {
                try
                {
                    if (D.Room == Convert.ToInt32(comboBox3.SelectedItem))
                    {
                        MessageBox.Show("Данный кабинет уже закреплён за другим врачом", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        save = false;
                    }
                }
                catch (System.FormatException) { }
            }*/

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
            /*if (save)
            {
                if (comboBox1.SelectedItem.ToString() == "Терапевт")
                    doctorsTableAdapter.InsertDoctor(textBox1.Text, textBox2.Text, textBox3.Text, comboBox1.Text, Convert.ToInt32(comboBox3.SelectedItem), M.GetNum(comboBox2.SelectedItem.ToString()), "00:00-00:00", 0);
                else doctorsTableAdapter.InsertNotTherapist(textBox1.Text, textBox2.Text, textBox3.Text, comboBox1.Text, Convert.ToInt32(comboBox3.SelectedItem), "00:00-00:00", 0);
                Close();
                MessageBox.Show("Данные о враче успешно добавлены.", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();

            if (comboBox1.SelectedItem.ToString() == "Терапевт") comboBox2.Enabled = true;
            else
            {
                comboBox2.Text = "";
                comboBox2.Enabled = false;
            }

            RefreshRooms();

            if(comboBox3.Items.Count == 0)
            {
                MessageBox.Show("за данной специальностью нет закреплённых кабинетов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                RoomsRegister RR = new RoomsRegister(true);
                RR.Owner = this;
                RR.ShowDialog();
            }
        }

        public void RefreshRooms()
        {
            /*F.RoomsFilling(Rooms);
            foreach (Specialization s in Specializations)
            {
                if (s.SpecializationName == comboBox1.SelectedItem.ToString())
                {
                    foreach (Room r in Rooms)
                    {
                        if (s.SpecId == r.SpecId)
                        {
                            comboBox3.Items.Add(r.RoomNum);
                        }
                    }
                    break;
                }
            }*/
        }

        private void NewDoctor_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(IsOpenedFromRF)
            {
                RegistersForm RF = this.Owner as RegistersForm;
                RF.RefreshComboboxes('d');
            }
        }
    }
}
