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
    public interface IDoctorView
    {
        IListRecordingView IListRecordingViewRef { get; set; }
        IMedicalCardView IMedicalCardViewRef { get; set; }
        IReferenceBookView IReferenceBookViewRef { get; set; }
        IShowStatistic IShowStatisticRef { get; set; }
        event EventHandler RecordedPatients_Click;
        event EventHandler ReferenceBook_Click;
        event EventHandler OpenPatientsCard_Click;
        event EventHandler Statistics_Click;
    }

    public partial class DoctorForm : Form, IDoctorView
    {
        private static int h = 0;
        private static int m = 0;
        private static int s = 0;
        private Form parentForm;

        public IListRecordingView IListRecordingViewRef { get; set; }
        public IMedicalCardView IMedicalCardViewRef { get; set; }
        public IReferenceBookView IReferenceBookViewRef { get; set; }
        public IShowStatistic IShowStatisticRef { get; set; }

        public event EventHandler ReferenceBook_Click;
        public event EventHandler RecordedPatients_Click;
        public event EventHandler OpenPatientsCard_Click;
        public event EventHandler Statistics_Click;

        public DoctorForm()
        {
            InitializeComponent();
        }

        public DoctorForm(Form parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
            timer1.Start();
        }

        private void DoctorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            parentForm.Visible = true;
            h = m = s = 0;
        }

        #region Actions
        private void OpenPatientsCardBtn_Click(object sender, EventArgs e)
        {
            MedicalCardForm MC = new MedicalCardForm();
            MC.Show();
            IMedicalCardViewRef = MC;
            OpenPatientsCard_Click?.Invoke(this, EventArgs.Empty);
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ReferenceBookBtn_Click(object sender, EventArgs e)
        {
            ReferenceBook RB = new ReferenceBook();
            RB.Show();
            IReferenceBookViewRef = RB;
            ReferenceBook_Click?.Invoke(this, EventArgs.Empty);
        }              

        private void RecordedPatientsBtn_Click(object sender, EventArgs e)
        {
            ListRecordingForm LRF = new ListRecordingForm();
            LRF.Show();
            IListRecordingViewRef = LRF;
            RecordedPatients_Click?.Invoke(this, EventArgs.Empty);
        }

        private void StatisticsBtn_Click(object sender, EventArgs e)
        {
            ShowStatistics SS = new ShowStatistics();
            SS.Show();
            IShowStatisticRef = SS;
            Statistics_Click?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region Timer
        private void timer1_Tick(object sender, EventArgs e)
        {
            s++;
            if (s == 60)
            {
                m++;
                s = 0;
            }
            if (m == 60)
            {
                h++;
                m = 0;
            }
            toolStripStatusLabel2.Text = $"({h.ToString("00")}:{m.ToString("00")}:{s.ToString("00")})";
        }
        #endregion
    }
}
