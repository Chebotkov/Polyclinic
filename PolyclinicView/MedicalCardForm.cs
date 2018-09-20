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
    public interface IMedicalCardView
    {
        IReferenceBookView IReferenceBookViewRef { get; set; }
        event EventHandler ReferenceBook_Click;
    }

    public partial class MedicalCardForm : Form, IMedicalCardView
    {
        public IReferenceBookView IReferenceBookViewRef { get; set; }
        public event EventHandler ReferenceBook_Click;

        //Doctor doctor = new Doctor();
        int LastLineIndex = 0;
        string pathToCard, TextBoxContent;
        //Methods M = new Methods();

        //Database1DataSetTableAdapters.VisitorStatisticsTableAdapter visitorAdapter = new Database1DataSetTableAdapters.VisitorStatisticsTableAdapter();
       // Database1DataSetTableAdapters.TicketsTableAdapter ticketTableAdapter = new Database1DataSetTableAdapters.TicketsTableAdapter();

        /*public List<Patient> Patients = new List<Patient>();
        public List<Doctor> Doctors = new List<Doctor>();
        public List<Ticket> OrderedTickets = new List<Ticket>();
        public List<Diagnoses> DiagnosesList = new List<Diagnoses>();
        public List<Drug> Drugs = new List<Drug>();*/

        public MedicalCardForm()
        {
            InitializeComponent();
        }

        private void MedicalCardForm_Load(object sender, EventArgs e)
        {
            /*Filling F = new Filling();
            F.PatientsListFilling(Patients);
            F.DoctorsListFilling(Doctors);
            F.TicketsFilling(Patients, Doctors, OrderedTickets);
            F.DiagnosesFilling(DiagnosesList);
            F.DrugsFilling(Drugs);

            M.AddDoctorsToComboBox(Doctors, comboBox2);*/

            comboBox1.Enabled = false;
            button4.Enabled = false;
            SetTrueFalse(true, 1);

            //M.AddDrugsToComboBox(Drugs, comboBox4);
           // M.AddDiagnosisToComboBox(DiagnosesList, comboBox3);

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
                /*foreach(Doctor D in Doctors)
                {
                    if(D.id == M.GetId(comboBox2))
                        textBox1.Text += Environment.NewLine + DateTime.Now.ToString() + Environment.NewLine + "Врач: " + comboBox2.Text + "." + " Специальность: " + D.Specialization + Environment.NewLine;
                }*/
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

                //M.WriteToMedicalCard(pathToCard, textBox1, LastLineIndex);

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
                    //M.WriteToMedicalCard(pathToCard, textBox1, LastLineIndex);
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
            comboBox1.Enabled = false;
            comboBox1.Text = "";
            comboBox1.Items.Clear();
            /*foreach (Ticket t in OrderedTickets)
            {
                if (M.GetId(comboBox2) == Convert.ToInt32(t.DoctorsFullName) && t.DateOfRecord == DateTime.Today.ToShortDateString() && !t.IsArrived)
                {
                    foreach (Patient p in Patients)
                    {
                        if (p.id == t.PatientsId) comboBox1.Items.Add(p.id + "." + p.LastName + " " + p.Name + " " + p.PatronymicName + " " + t.VisitingTime);
                    }
                }
            }*/
            if (comboBox1.Items.Count == 0)
            {
                MessageBox.Show("У вас нет записанных на сегодня пациентов", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBox1.Enabled = false;
            }
            else comboBox1.Enabled = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button4.Enabled = true;
            textBox1.Clear();
            LastLineIndex = 0;
            //pathToCard = PathInfo.PathToProject + "\\MedicalCards\\" + M.GetId(comboBox1).ToString() + ".txt";
            //M.ReadFromMedicalCard(pathToCard, textBox1);
            TextBoxContent = textBox1.Text;
            LastLineIndex = textBox1.Lines.Length;
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
        

        private void button3_Click(object sender, EventArgs e)
        {
            ReferenceBook RB = new ReferenceBook(true);
            RB.Owner = this;
            RB.ShowDialog();
            IReferenceBookViewRef = RB;
            ReferenceBook_Click?.Invoke(this, EventArgs.Empty);
        }

        public void RefreshDrugsAndDiagnosis()
        {
            comboBox3.Items.Clear();
            //M.AddDiagnosisToComboBox(DiagnosesList, comboBox3);

            comboBox4.Items.Clear();
            //M.AddDrugsToComboBox(Drugs, comboBox4);
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

        private void button4_Click(object sender, EventArgs e)
        {
            //if (M.IncrementOfAttendance(Doctors, OrderedTickets, comboBox2)) Close();
        }
    }
}
