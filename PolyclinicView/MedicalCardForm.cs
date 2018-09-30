using PolyclinicBL;
using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PolyclinicView
{
    public interface IMedicalCardView
    {
        IReferenceBookView IReferenceBookViewRef { get; set; }
        event EventHandler<DoctorEventArgs> SaveChanges;
        event EventHandler<PatientsArrivalEventArgs> Report;
        event EventHandler ReferenceBook_Click;
        event EventHandler MedicalCardFormLoad;
        event EventHandler<DoctorEventArgs> ReadMedicalCard;
        event EventHandler<TicketEventArgs> DoctorSelect;
        event EventHandler<MedicalCardEventAgs> WriteToMedicalCard;
        void RefreshDrugsAndDiagnosis(IEnumerable Drugs, IEnumerable Diagnoses);
        void SetData(IEnumerable Doctors, IEnumerable Drugs, IEnumerable Diagnoses);
        void SetPatientsCard(TextReader reader);
        void SetPatients(IEnumerable patients);
        void SetDoctor(object doctor);
        string SpecializationName { get; set; }
    }

    public partial class MedicalCardForm : Form, IMedicalCardView
    {
        public IReferenceBookView IReferenceBookViewRef { get; set; }
        public event EventHandler<DoctorEventArgs> SaveChanges;
        public event EventHandler<PatientsArrivalEventArgs> Report;
        public event EventHandler ReferenceBook_Click;
        public event EventHandler MedicalCardFormLoad;
        public event EventHandler<DoctorEventArgs> ReadMedicalCard;
        public event EventHandler<TicketEventArgs> DoctorSelect;
        public event EventHandler<MedicalCardEventAgs> WriteToMedicalCard;
        public string SpecializationName { get; set; }

        private int LastLineIndex = 0;
        private PolyclinicDBManager.Doctor doctor;
        private string TextBoxContent;

        public MedicalCardForm()
        {
            InitializeComponent();
        }

        #region Actions
        private void MedicalCardForm_Load(object sender, EventArgs e)
        {
            MedicalCardFormLoad?.Invoke(this, EventArgs.Empty);

            comboBox1.Enabled = false;
            button4.Enabled = false;
            SetTrueFalse(true, 1);
            comboBox2.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //change
            button4.Enabled = false;
            textBox1.Text += Environment.NewLine;
            if (comboBox1.SelectedIndex != -1 && comboBox2.SelectedIndex != -1)
            {
                SetTrueFalse(false, 1);
                SetTrueFalse(false, 2);

                SaveChanges?.Invoke(this, new DoctorEventArgs(Editor.GetId(comboBox2.SelectedItem.ToString())));
                textBox1.Text += Environment.NewLine + Environment.NewLine + DateTime.Now.ToString() + Environment.NewLine + String.Format("Врач: {0} {1} {2}. Специальность: {3}.", doctor.FirstName, doctor.LastName, doctor.Patronymic, SpecializationName) + Environment.NewLine;

                textBox1.SelectionStart = textBox1.TextLength;
                textBox1.ScrollToCaret();
                textBox1.Focus();
            }
            if (comboBox1.SelectedIndex == -1) errorProvider1.SetError(comboBox1, "Выберите пациента!");
            if (comboBox2.SelectedIndex == -1) errorProvider1.SetError(comboBox2, "Выберите врача!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("Вы уверены, что хотите применить изменения?", "Сохранить?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                SetTrueFalse(true, 1);
                SetTrueFalse(true, 2);

                WriteToMedicalCard?.Invoke(this, new MedicalCardEventAgs(Editor.GetId(comboBox1.SelectedItem.ToString()), Editor.GetByteRepresentation(textBox1.Lines, LastLineIndex)));

                TextBoxContent = textBox1.Text;
                LastLineIndex = textBox1.Lines.Length;
                textBox1.ReadOnly = true;
                Cancel.Enabled = false;
                comboBox3.Text = "";
                comboBox4.Text = "";
            }
            button4.Enabled = true;

        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox1.Text = TextBoxContent;
            SetTrueFalse(true, 2);
            SetTrueFalse(true, 1);
            textBox1.SelectionStart = textBox1.TextLength;
            textBox1.ScrollToCaret();
            textBox1.Focus();
            button4.Enabled = true;
        }

        private void MedicalCardForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!textBox1.ReadOnly)
            {
                DialogResult dr = MessageBox.Show("Сохранить изменения?", "Сохранить?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    WriteToMedicalCard?.Invoke(this, new MedicalCardEventAgs(Editor.GetId(comboBox1.SelectedItem.ToString()), Editor.GetByteRepresentation(textBox1.Lines, LastLineIndex)));
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dialogresult = MessageBox.Show("Пациент прибыл на приём?", "Подтвердите", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            bool isArrived = false;
            if (DialogResult.Yes == dialogresult)
            {
                isArrived = true;
            }

            if (DialogResult.Cancel != dialogresult)
            {
                int patientId = Editor.GetId(comboBox1.SelectedItem.ToString());
                int doctorId = Editor.GetId(comboBox2.SelectedItem.ToString());
                string date = DateTime.Today.ToShortDateString();
                string time = Editor.GetTime(comboBox1.SelectedItem.ToString());

                Report?.Invoke(this, new PatientsArrivalEventArgs(patientId, doctorId, isArrived, date, time));
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex != -1)
            {
                textBox1.Clear();
                comboBox1.Enabled = false;
                comboBox1.Text = "";

                DoctorSelect?.Invoke(this, new TicketEventArgs(0, Editor.GetId(comboBox2.SelectedItem.ToString()), DateTime.Today.ToShortDateString(), ""));

                if (comboBox1.Items.Count == 0)
                {
                    MessageBox.Show("У вас нет записанных на сегодня пациентов", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    comboBox1.Enabled = false;
                }
                else comboBox1.Enabled = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button4.Enabled = true;
            textBox1.Clear();
            LastLineIndex = 0;

            ReadMedicalCard?.Invoke(this, new DoctorEventArgs(Editor.GetId(comboBox1.SelectedItem.ToString())));

            TextBoxContent = textBox1.Text;
            LastLineIndex = textBox1.Lines.Length;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ReferenceBook RB = new ReferenceBook(true);
            RB.Owner = this;
            IReferenceBookViewRef = RB;
            ReferenceBook_Click?.Invoke(this, EventArgs.Empty);
            RB.ShowDialog();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cancel.Enabled == true)
            {
                textBox1.Text += Environment.NewLine + "Диагноз: " + comboBox3.Text + Environment.NewLine;
                textBox1.SelectionStart = textBox1.TextLength;
                textBox1.ScrollToCaret();
                textBox1.Focus();
                comboBox4.Enabled = true;
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cancel.Enabled == true)
            {
                textBox1.Text += Environment.NewLine + "Назначенные лекарства: " + comboBox4.Text + Environment.NewLine + "Рекомендации: ";
                textBox1.SelectionStart = textBox1.TextLength;
                textBox1.ScrollToCaret();
                textBox1.Focus();
            }

        }
        #endregion

        #region Necessary methods
        public void RefreshDrugsAndDiagnosis(IEnumerable Drugs, IEnumerable Diagnoses)
        {
            foreach (PolyclinicBL.Diagnoses d in Diagnoses)
            {
                comboBox3.Items.Add(d.Diagnosis);
            }
            comboBox3.Text = "";

            foreach (PolyclinicBL.Drug drug in Drugs)
            {
                comboBox4.Items.Add(drug.Medicines);
            }
            comboBox4.Text = "";
        }

        private void SetTrueFalse(bool b, int i)
        {
            switch (i)
            {
                case 1:
                    {
                        comboBox3.Enabled = !b;
                        comboBox4.Enabled = !b;
                        textBox1.ReadOnly = b;
                        button2.Enabled = !b;
                        Cancel.Enabled = !b;
                        break;
                    }
                case 2:
                    {
                        comboBox1.Enabled = b;
                        comboBox2.Enabled = b;
                        break;
                    }
            }
        }
        #endregion

        #region Interface implementation.
        public void SetData(IEnumerable Doctors, IEnumerable Drugs, IEnumerable Diagnoses)
        {
            //nullReference exc.
            comboBox2.DataSource = Doctors;
            comboBox2.SelectedIndex = -1;

            RefreshDrugsAndDiagnosis(Drugs, Diagnoses);
        }

        public void SetPatients(IEnumerable patients)
        {
            if (patients is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(patients)));
            }

            comboBox1.DataSource = patients;
        }

        public void SetPatientsCard(TextReader reader)
        {
            StreamReader streamReader = reader as StreamReader;
            string line;
            while ((line = streamReader.ReadLine()) != null)
            {
                textBox1.Text += line + Environment.NewLine;
            }
            reader.Close();
        }

        public void SetDoctor(object doctor)
        {
            if (doctor is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(doctor)));
            }

            this.doctor = doctor as PolyclinicDBManager.Doctor;

            if (this.doctor is null)
            {
                throw new InvalidCastException(String.Format("{0} is not {1}", nameof(doctor), typeof(PolyclinicDBManager.Doctor)));
            }
        }
        #endregion
    }
}
