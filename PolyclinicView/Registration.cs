using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using PolyclinicDBManager;

namespace PolyclinicView
{
    public interface IRegistrationView
    {
        event EventHandler RegistrationFormLoad;
        event EventHandler SaveChanges;

        PolyclinicBL.Patient GetNewPatient();
        void SetRegions(IEnumerable regions);
    }

    public partial class RegistrationForm : Form, IRegistrationView
    {
        public event EventHandler RegistrationFormLoad;
        public event EventHandler SaveChanges;

        private MessageService messageService = new MessageService();
        List<TextBox> TextBoxes = new List<TextBox>();
        bool save = true;
        bool IsOpenedFromRF = false;

        public RegistrationForm()
        {
            InitializeComponent();
        }

        public RegistrationForm(bool b)
        {
            InitializeComponent();
            IsOpenedFromRF = b;
        }

        #region Actions
        private void RegistrationForm_Load(object sender, EventArgs e)
        {
            TextBoxes.Add(textBox1);
            TextBoxes.Add(textBox2);
            TextBoxes.Add(textBox3);
            domainUpDown1.Items.AddRange(new string[] { "Женский", "Мужской"});
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            
            RegistrationFormLoad?.Invoke(this, EventArgs.Empty);
        }

        private void Save_Click(object sender, EventArgs e)
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

            if (comboBox1.SelectedItem == null)
            {
                errorProvider1.SetError(comboBox1, "Выберите участок!");
                save = false;
            }
            else
            {
                if (String.IsNullOrEmpty(textBox4.Text))
                {
                    errorProvider1.SetError(textBox4, "Введите улицу!");
                    save = false;
                }

                if (String.IsNullOrEmpty(textBox5.Text))
                {
                    errorProvider1.SetError(textBox5, "Введите номер дома!");
                    save = false;
                }

                if (!maskedTextBox1.MaskFull)
                {
                    errorProvider1.SetError(maskedTextBox1, "Введите дату!");
                    save = false;
                }
                else
                {
                    if (!DateTime.TryParse(maskedTextBox1.Text, out DateTime datetime))
                    {
                        messageService.ShowError("Неверная дата");
                        save = false;
                    }
                }

                if (domainUpDown1.SelectedItem == null)
                {
                    errorProvider1.SetError(domainUpDown1, "Выберите пол!");
                    save = false;
                }
            }

            if (save)
            {
                SaveChanges?.Invoke(this, EventArgs.Empty);
                messageService.ShowInfo("Данные о пациенте успешно добавлены.");
                Close();
            }
        }
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            textBox6.Enabled = true;
        }

        private void RegistrationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(IsOpenedFromRF)
            {
                RegistersForm RF = this.Owner as RegistersForm;
                RF.RefreshComboboxes('p');
            }
        }
        #endregion

        #region Interface Implementation

        public void SetRegions(IEnumerable regions)
        {
            comboBox1.DataSource = regions;
            comboBox1.SelectedIndex = -1;
        }

        public PolyclinicBL.Patient GetNewPatient()
        {
            PolyclinicBL.Patient patient = new PolyclinicBL.Patient();
            patient.LastName = textBox2.Text;
            patient.FirstName = textBox1.Text;
            patient.Patronymic = textBox3.Text;
            patient.Birth = DateTime.Parse(maskedTextBox1.Text);
            patient.Gender = domainUpDown1.SelectedIndex == 0 ? false : true;
            patient.Region = comboBox1.SelectedIndex + 1;
            patient.Address = String.Format("ул. {0}, д. {1} {2}", textBox4.Text, textBox5.Text, (!String.IsNullOrEmpty(textBox6.Text) ? (", кв." + textBox6.Text) : " "));
            patient.RegistrationDate = DateTime.Today;

            return patient;
        }

        #endregion
    }
}
