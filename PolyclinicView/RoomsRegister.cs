using PolyclinicBL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PolyclinicView
{
    public interface IRoomsRegisterView
    {
        event EventHandler RoomsRegisterLoad;
        event EventHandler<DoctorEventArgs> SpecializationChoise;
        event EventHandler<RoomsEventArgs> RoomsAdd;
        void SetSpecializations(IEnumerable specializations);
        void SetAvailableRooms(List<int> rooms);
    }

    public partial class RoomsRegister : Form, IRoomsRegisterView
    {
        private bool IsOpenedFromND = false;
        private List<int> rooms;

        public event EventHandler RoomsRegisterLoad;
        public event EventHandler<RoomsEventArgs> RoomsAdd;
        public event EventHandler<DoctorEventArgs> SpecializationChoise;

        public RoomsRegister()
        {
            InitializeComponent();
        }

        public RoomsRegister(bool b)
        {
            InitializeComponent();
            IsOpenedFromND = b;
        }

        #region Actions
        private void RoomsRegister_Load(object sender, EventArgs e)
        {
            RoomsRegisterLoad?.Invoke(this, EventArgs.Empty);

            radioButton3.Select();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SpecializationChoise?.Invoke(this, new DoctorEventArgs(Editor.GetId(comboBox1.SelectedItem.ToString())));

            groupBox1.Enabled = true;
            radioButton1.Select();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            button1.Enabled = false;
            button2.Enabled = true;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            button1.Enabled = true;
            button2.Enabled = false;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool save = true;

            int firstRoom = Int32.Parse(textBox2.Text);
            int lastRoom = Int32.Parse(textBox3.Text);
            if (String.IsNullOrEmpty(textBox2.Text))
            {
                errorProvider1.SetError(textBox2, "Укажите начало кабинетов");
                save = false;
            }
            else if (String.IsNullOrEmpty(textBox3.Text))
            {
                errorProvider1.SetError(textBox3, "Укажите конец кабинетов");
                save = false;
            }
            else if (lastRoom - firstRoom < 0)
            {
                MessageBox.Show("Указан неверный интервал", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                save = false;
            }

            if (save)
            {
                int AddedRooms = lastRoom - firstRoom;
                List<int> Rooms = new List<int>(); 

                for (int i = firstRoom; i <= lastRoom; i++)
                {
                    bool exists = false;
                    foreach (int room in rooms)
                    {
                        if (room == i)
                        {
                            exists = true;
                            AddedRooms--;
                            break;
                        }
                    }

                    if (!exists)
                    {
                        Rooms.Add(i);
                    }
                }

                if (AddedRooms == -1) MessageBox.Show("Выбранные кабинеты уже закреплены за какими-либо специализациями", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else MessageBox.Show("Кабинеты успешно добавлены", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox2.Clear();
                textBox3.Clear();

                RoomsAdd?.Invoke(this, new RoomsEventArgs(Editor.GetId(comboBox1.SelectedItem.ToString()), Rooms));

                radioButton4.Select();
                radioButton3.Select();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool exists = false;
            int currentRoom = Int32.Parse(textBox1.Text);
            if (String.IsNullOrEmpty(textBox1.Text))
                errorProvider1.SetError(textBox1, "Укажите номер кабинета");
            else
            {
                foreach (int room in rooms)
                {
                    if (room == currentRoom)
                    {
                        MessageBox.Show("Данный кабинет уже есть", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                {
                    RoomsAdd?.Invoke(this, new RoomsEventArgs(Editor.GetId(comboBox1.SelectedItem.ToString()), new List<int> { currentRoom }));
                    MessageBox.Show("Кабинет успешно добавлен", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    radioButton4.Select();
                    radioButton3.Select();
                }
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            comboBox1.Enabled = true;
            comboBox2.Enabled = false;
            comboBox1.Text = "";
            comboBox2.Text = "";
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            listBox1.Enabled = true;
            comboBox1.Enabled = false;
            comboBox2.Enabled = true;
            groupBox1.Enabled = false;
            comboBox1.Text = "";
            comboBox2.Text = "";
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Enabled = true;

            SpecializationChoise?.Invoke(this, new DoctorEventArgs(Editor.GetId(comboBox2.SelectedItem.ToString())));
            listBox1.DataSource = rooms;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 44)
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 44)
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 44)
            {
                e.Handled = true;
            }
        }

        private void RoomsRegister_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(IsOpenedFromND)
            {
                throw new NotImplementedException();
                NewDoctor ND = this.Owner as NewDoctor;
                ND.RefreshRooms();
            }
        }
        #endregion

        #region Interface implementation
        public void SetSpecializations(IEnumerable specializations)
        {
            if (specializations is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(specializations)));
            }

            comboBox1.DataSource = specializations;
            comboBox2.DataSource = specializations;
        }

        public void SetAvailableRooms(List<int> rooms)
        {
            if (rooms is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(rooms)));
            }

            this.rooms = rooms;
        }
        #endregion
    }
}
