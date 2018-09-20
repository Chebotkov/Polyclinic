using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PolyclinicView
{
    public interface IRoomsRegisterView
    {

    }

    public partial class RoomsRegister : Form, IRoomsRegisterView
    {
        /*Database1DataSetTableAdapters.RoomsTableAdapter roomsTableAdapter = new Database1DataSetTableAdapters.RoomsTableAdapter();
        public List<Room> Rooms = new List<Room>();
        public List<Specialization> Specializations = new List<Specialization>();
        Filling F = new Filling();*/
        bool IsOpenedFromND = false;

        public RoomsRegister()
        {
            InitializeComponent();
        }

        public RoomsRegister(bool b)
        {
            InitializeComponent();
            IsOpenedFromND = b;
        }

        private void RoomsRegister_Load(object sender, EventArgs e)
        {
            /*F.SpecializationsFilling(Specializations);
            F.RoomsFilling(Rooms);

            radioButton3.Select();
            foreach (Specialization s in Specializations)
            {
                comboBox1.Items.Add(s.SpecializationName);
                comboBox2.Items.Add(s.SpecializationName);
            }*/
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
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
            else if (Convert.ToInt32(textBox3.Text) - Convert.ToInt32(textBox2.Text) < 0)
            {
                MessageBox.Show("Указан неверный интервал", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                save = false;
            }

            if (save)
            {
                int AddedRooms = Convert.ToInt32(textBox3.Text) - Convert.ToInt32(textBox2.Text);
                /*for (int i = Convert.ToInt32(textBox2.Text); i <= Convert.ToInt32(textBox3.Text); i++)
                {
                    bool exists = false;
                    foreach (Room r in Rooms)
                    {
                        if (r.RoomNum == i)
                        {
                            exists = true;
                            AddedRooms--;
                            break;
                        }
                    }

                    if (!exists)
                    {
                        foreach (Specialization s in Specializations)
                        {
                            if (s.SpecializationName == comboBox1.SelectedItem.ToString())
                            {
                                roomsTableAdapter.InsertQuery(i, s.SpecId);
                                break;
                            }
                        }
                    }
                }
                if (AddedRooms == -1) MessageBox.Show("Выбранные кабинеты уже закреплены за какими-либо специализациями", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else MessageBox.Show("Кабинеты успешно добавлены", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox2.Clear();
                textBox3.Clear();
                Rooms.Clear();
                F.RoomsFilling(Rooms);*/
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool exists = false;
            if (String.IsNullOrEmpty(textBox1.Text))
                errorProvider1.SetError(textBox1, "Укажите номер кабинета");
            /*else
            {
                foreach (Room r in Rooms)
                {
                    if (r.RoomNum == Convert.ToInt32(textBox1.Text))
                    {
                        MessageBox.Show("Данный кабинет уже есть", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    foreach (Specialization s in Specializations)
                    {
                        if (s.SpecializationName == comboBox1.SelectedItem.ToString())
                        {
                            roomsTableAdapter.InsertQuery(Convert.ToInt32(textBox1.Text), s.SpecId);
                            MessageBox.Show("Кабинет успешно добавлен", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            textBox1.Clear();
                            Rooms.Clear();
                            F.RoomsFilling(Rooms);
                            break;
                        }
                    }
                }
            }*/
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            listBox1.Items.Clear();
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
            listBox1.Items.Clear();
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
            listBox1.Items.Clear();
            listBox1.Enabled = true;

            /*foreach (Specialization s in Specializations)
            {
                if (s.SpecializationName == comboBox2.SelectedItem.ToString())
                {
                    foreach (Room r in Rooms)
                    {
                        if (r.SpecId == s.SpecId)
                            listBox1.Items.Add(r.RoomNum);
                    }
                }
            }*/
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
            /*if(IsOpenedFromND)
            {
                NewDoctor ND = this.Owner as NewDoctor;
                ND.RefreshRooms();
            }*/
        }
    }
}
