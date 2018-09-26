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
    public interface IReferenceBookView
    {
        event EventHandler ReferenceBookLoad;
        event EventHandler<DrugOrDiagnosisEventArgs> DrugAdd;
        event EventHandler<DrugOrDiagnosisEventArgs> DiagnosisAdd;
        event EventHandler<DrugOrDiagnosisEventArgs> DrugDescriptionChange;
        event EventHandler<DrugOrDiagnosisEventArgs> DiagnosisDescriptionChange;

        void SetData(IEnumerable<PolyclinicBL.Drug> Drugs, IEnumerable<PolyclinicBL.Diagnoses> Diagnoses);
    }

    public partial class ReferenceBook : Form, IReferenceBookView
    {
        public event EventHandler ReferenceBookLoad;
        public event EventHandler<DrugOrDiagnosisEventArgs> DrugAdd;
        public event EventHandler<DrugOrDiagnosisEventArgs> DiagnosisAdd;
        public event EventHandler<DrugOrDiagnosisEventArgs> DrugDescriptionChange;
        public event EventHandler<DrugOrDiagnosisEventArgs> DiagnosisDescriptionChange;

        public bool IsOpenedFromMedicalCardForm = false;
        private List<PolyclinicBL.Drug> Drugs;
        private List<PolyclinicBL.Diagnoses> Diagnoses;

        public ReferenceBook()
        {
            InitializeComponent();
        }

        public ReferenceBook(bool IsOpenedFromMedicalCardForm)
        {   
            InitializeComponent();
            this.IsOpenedFromMedicalCardForm = IsOpenedFromMedicalCardForm;
        }
        
        private void ReferenceBook_Load(object sender, EventArgs e)
        {
            ReferenceBookLoad?.Invoke(this, EventArgs.Empty);

            label3.Text = "";
            textBox1.ReadOnly = true;
            label3.Hide();

            radioButton1.Select();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

            label1.Text = "Добавьте диагноз:";
            label2.Text = "Выберите диагноз:";
            ClearAll();
            
            foreach (Diagnoses diagnosis in Diagnoses)
            {
                comboBox1.Items.Add(diagnosis.Diagnosis);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                label3.Show();
                label3.Text = "Внесите изменения в поле ниже:";
                if (comboBox1.SelectedIndex != -1) textBox1.ReadOnly = false;
            }
            else
                if(radioButton1.Checked) MessageBox.Show("Выберите диагноз из списка слева!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else MessageBox.Show("Выберите лекарство из списка слева!", "Ошибка!", MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

            label1.Text = "Добавьте лекарство:";
            label2.Text = "Выберите лекарство:";
            ClearAll();

            foreach (Drug drug in Drugs)
            {
                comboBox1.Items.Add(drug.Medicines);
            }

        }
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                textBox1.Text = Diagnoses[comboBox1.SelectedIndex].Description;
            }
            if (radioButton2.Checked)
            {
                textBox1.Text = Drugs[comboBox1.SelectedIndex].Description;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label3.Hide();
            try
            {
                if (radioButton2.Checked)
                {
                    int drugId = comboBox1.SelectedIndex;
                    Drug drug = new Drug(Drugs[drugId].Medicines, textBox1.Text);
                    Drugs[drugId] = drug;

                    DrugDescriptionChange?.Invoke(this, new DrugOrDiagnosisEventArgs(drug.Medicines, drug.Description));
                }
                if (radioButton1.Checked)
                {
                    int diagnosisId = comboBox1.SelectedIndex;
                    Diagnoses diagnosis = new Diagnoses(Diagnoses[diagnosisId].Diagnosis, textBox1.Text);
                    Diagnoses[diagnosisId] = diagnosis;

                    DiagnosisDescriptionChange?.Invoke(this, new DrugOrDiagnosisEventArgs(diagnosis.Diagnosis, diagnosis.Description));
                }

                MessageBox.Show("Данные успешно сохранены", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.ReadOnly = true;
            }
            catch (System.ArgumentOutOfRangeException) {}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                if (String.IsNullOrEmpty(textBox2.Text))
                {
                    errorProvider1.SetError(textBox2, "Введите новое лекарство!");
                }
                else
                {
                    Drug drug = new Drug(textBox2.Text, "");
                    Drugs.Add(drug);

                    DrugAdd?.Invoke(this, new DrugOrDiagnosisEventArgs(drug.Medicines, drug.Description));

                    MessageBox.Show("Лекарство успешно добавлено", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    radioButton1.PerformClick();
                    radioButton2.PerformClick();
                }
            }
            if (radioButton1.Checked)
            {
                if (String.IsNullOrEmpty(textBox2.Text))
                {
                    errorProvider1.SetError(textBox2, "Введите новый диагноз!");
                }
                else
                {
                    Diagnoses diagnosis = new Diagnoses(textBox2.Text, "");
                    Diagnoses.Add(diagnosis);

                    DiagnosisAdd?.Invoke(this, new DrugOrDiagnosisEventArgs(diagnosis.Diagnosis, diagnosis.Description));

                    MessageBox.Show("Диагноз успешно добавлен", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    radioButton2.PerformClick();
                    radioButton1.PerformClick();
                }
            }
        }

        private void ClearAll()
        {
            comboBox1.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox1.ReadOnly = true;
            comboBox1.Items.Clear();
            errorProvider1.Clear();
        }

        private void ReferenceBook_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (IsOpenedFromMedicalCardForm)
            {
                MedicalCardForm MF = this.Owner as MedicalCardForm;
                MF.RefreshDrugsAndDiagnosis(Drugs, Diagnoses);
            }
        }

        public void SetData(IEnumerable<PolyclinicBL.Drug> Drugs, IEnumerable<PolyclinicBL.Diagnoses> Diagnoses)
        {
            if (Drugs is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(Drugs)));
            }

            if (Diagnoses is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(Diagnoses)));
            }

            this.Drugs = Drugs.ToList();
            this.Diagnoses = Diagnoses.ToList();
        }
    }
}
