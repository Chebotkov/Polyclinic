using System;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using PolyclinicBL;

namespace PolyclinicView
{
    public interface IMainView
    {
        string EnteredLogin { get; }
        IRegistratorView IRegistratorViewRef { get; set; }
        IDoctorView IDoctorViewRef { get; set; }

        void EntityChoice(Entities entity);
        event EventHandler MainFormLoad;
        event EventHandler Enter_Click;
        event EventHandler Doctor_Click;
        event EventHandler Registrator_Click;
    }

    partial class MainForm : Form, IMainView
    {
        private Thread MelodyThread;
        private MessageService MessageService = new MessageService();

        public event EventHandler MainFormLoad;
        public event EventHandler Enter_Click;
        public event EventHandler Doctor_Click;
        public event EventHandler Registrator_Click;

        public string EnteredLogin { get; set; }

        public IRegistratorView IRegistratorViewRef { get; set; }
        public IDoctorView IDoctorViewRef { get; set; }


        public MainForm()
        {
            InitializeComponent();
        }

        #region
        private void MainForm_Load(object sender, EventArgs e)
        {
            MelodyThread = new Thread(new ThreadStart(Music.MissionImpossible));
            MelodyThread.Start();

            MainFormLoad?.Invoke(this, EventArgs.Empty);

            label1.Hide();
            Registrator.Hide();
            Doctor.Hide();
            button3.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label1.Hide();
            Registrator.Hide();
            Doctor.Hide();
            button3.Hide();
            label2.Show();
            enter.Show();
            textBox1.Show();
            textBox1.Clear();
        }

        private void Enter_Button_Click(object sender, EventArgs e)
        {
            EnteredLogin = textBox1.Text;
            Enter_Click?.Invoke(this, EventArgs.Empty);
        }

        private void Doctor_Form_Button_Click(object sender, EventArgs e)
        {
            DoctorForm doctorForm = new DoctorForm(this);
            IDoctorViewRef = doctorForm;
            doctorForm.Owner = this;
            doctorForm.Show();
            Visible = false;

            Doctor_Click?.Invoke(this, EventArgs.Empty);
        }

        private void Registrator_Form_Button_Click(object sender, EventArgs e)
        {
            RegistratorForm RegistratorFormRef = new RegistratorForm(this);
            IRegistratorViewRef = RegistratorFormRef;
            RegistratorFormRef.Owner = this;
            RegistratorFormRef.Show();
            Visible = false;

            Registrator_Click?.Invoke(this, EventArgs.Empty);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
#endregion

        #region Interface implementation
        public void EntityChoice(Entities entity)
        {
            switch (entity)
            {
                case Entities.Administrator:
                    {
                        label1.Show();
                        Doctor.Show();
                        Registrator.Show();
                        button3.Show();
                        label2.Hide();
                        enter.Hide();
                        textBox1.Hide();
                        break;
                    }

                case Entities.Registrator:
                    {
                        textBox1.Clear();
                        RegistratorForm RegistratorFormRef = new RegistratorForm(this);
                        IRegistratorViewRef = RegistratorFormRef;
                        RegistratorFormRef.Owner = this;
                        RegistratorFormRef.Show();
                        Visible = false;

                        Registrator_Click?.Invoke(this, EventArgs.Empty);
                        break;
                    }
                case Entities.Doctor:
                    {
                        textBox1.Clear();
                        DoctorForm doctorForm = new DoctorForm(this);
                        IDoctorViewRef = doctorForm;
                        doctorForm.Owner = this;
                        doctorForm.Show();
                        Visible = false;

                        Doctor_Click?.Invoke(this, EventArgs.Empty);
                        break;
                    }
                case Entities.Empty:
                    {
                        MessageService.ShowWarning("Wrong login was entered");
                        textBox1.Clear();
                        break;
                    }
            }

            MelodyThread.Suspend();
        }
        #endregion  
    }
}
