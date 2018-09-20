using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolyclinicView
{
    public interface IReferenceBookView
    {

    }

    public partial class ReferenceBook : Form, IReferenceBookView
    {
        //Database1DataSetTableAdapters.DiagnosesTableAdapter diagnosesTableAdapter = new Database1DataSetTableAdapters.DiagnosesTableAdapter();
        //Database1DataSetTableAdapters.DrugsTableAdapter drugsTable = new Database1DataSetTableAdapters.DrugsTableAdapter();

        //public List<Diagnoses> DiagnosesList = new List<Diagnoses>();
        //public List<Drug> Drugs = new List<Drug>();
        public bool OpenFromMedicalCardForm = false;

        public ReferenceBook()
        {
            InitializeComponent();
        }

        public ReferenceBook(bool b)
        {   
            InitializeComponent();
            OpenFromMedicalCardForm = b;
        }
        
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

            label1.Text = "Добавьте диагноз:";
            label2.Text = "Выберите диагноз:";
            ClearAll();
            /*foreach (Diagnoses d in DiagnosesList)
            {
                comboBox1.Items.Add(d.Diagnosis);
            }*/
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
            /*foreach (Drug d in Drugs)
            {
                comboBox1.Items.Add(d.Medicines);
            }*/
            
        }
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            /*if (radioButton1.Checked)
            {
               textBox1.Text = DiagnosesList[comboBox1.SelectedIndex].Description;
            }
            if (radioButton2.Checked)
            {
                textBox1.Text = Drugs[comboBox1.SelectedIndex].Description;
            }*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label3.Hide();
            try
            {
                /*if (radioButton2.Checked)
                {
                    Drugs[comboBox1.SelectedIndex].Description = textBox1.Text;
                    drugsTable.Update(Drugs[comboBox1.SelectedIndex].Medicines, Drugs[comboBox1.SelectedIndex].Description, Drugs[comboBox1.SelectedIndex].Medicines);
                }
                if (radioButton1.Checked)
                {
                    DiagnosesList[comboBox1.SelectedIndex].Description = textBox1.Text;
                    diagnosesTableAdapter.Update(DiagnosesList[comboBox1.SelectedIndex].Diagnosis, DiagnosesList[comboBox1.SelectedIndex].Description, DiagnosesList[comboBox1.SelectedIndex].Diagnosis);
                }*/
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
                   /* Drug D = new Drug();
                    D.Medicines = textBox2.Text;
                    Drugs.Add(D);
                    drugsTable.Insert(D.Medicines, D.Description);*/
                    MessageBox.Show("Лекарство усешно добавлено", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    /*Diagnoses D = new Diagnoses();
                    D.Diagnosis = textBox2.Text;
                    DiagnosesList.Add(D);
                    diagnosesTableAdapter.Insert(D.Diagnosis, D.Description);*/
                    MessageBox.Show("Диагноз усешно добавлен", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            errorProvider1.Clear();
            comboBox1.Items.Clear();
        }

        private void ReferenceBook_Load(object sender, EventArgs e)
        {
            /*Filling F = new Filling();
            F.DiagnosesFilling(DiagnosesList);
            F.DrugsFilling(Drugs);*/

            label3.Text = "";
            textBox1.ReadOnly = true;
            label3.Hide();
        }

        private void ReferenceBook_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (OpenFromMedicalCardForm)
            {
                //MedicalCardForm MF = this.Owner as MedicalCardForm;
                //MF.RefreshDrugsAndDiagnosis();
            }
        }
    }
}
