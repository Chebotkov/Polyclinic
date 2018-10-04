using PolyclinicBL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace PolyclinicView
{
    public interface IRegistratorView
    {
        IPrintTicketView IPrintTicketViewRef { get; set; }
        IReferenceBookView IReferenceBookViewRef { get; set; }
        IRegistersView IRegistersViewRef { get; set; }
        IRegistrationView IRegistrationViewRef { get; set; }
        IRoomsRegisterView IRoomsRegisterViewRef { get; set; }
        ITicketOrderView ITicketOrderViewRef { get; set; }
        event EventHandler PrintTicket_Click;
        event EventHandler ReferenceBook_Click;
        event EventHandler Registers_Click;
        event EventHandler Registration_Click;
        event EventHandler RoomsRegister_Click;
        event EventHandler TicketOrder_Click;
    }

    partial class RegistratorForm : Form, IRegistratorView
    {
        private static int h = 0;
        private static int m = 0;
        private static int s = 0;
        private Form parentForm;
        
        private MessageService MessageService = new MessageService();

        public IPrintTicketView IPrintTicketViewRef { get; set; }
        public IReferenceBookView IReferenceBookViewRef { get; set; }
        public IRegistersView IRegistersViewRef { get; set; }
        public IRegistrationView IRegistrationViewRef { get; set; }
        public IRoomsRegisterView IRoomsRegisterViewRef { get; set; }
        public ITicketOrderView ITicketOrderViewRef { get; set; }
        public event EventHandler PrintTicket_Click;
        public event EventHandler ReferenceBook_Click;
        public event EventHandler Registers_Click;
        public event EventHandler Registration_Click;
        public event EventHandler RoomsRegister_Click;
        public event EventHandler TicketOrder_Click;

        public RegistratorForm()
        {
            InitializeComponent();
        }

        public RegistratorForm(Form parentForm)
        {
            if (parentForm is null)
            {
                throw new ArgumentNullException("{0} is null", nameof(parentForm));
            }

            this.parentForm = parentForm;
            InitializeComponent();
            timer1.Start();
        }
        
        #region Actions
        private void ReferenceBookBtn_Click(object sender, EventArgs e)
        {
            ReferenceBook RB = new ReferenceBook();
            RB.Owner = this;
            IReferenceBookViewRef = RB;
            ReferenceBook_Click?.Invoke(this, EventArgs.Empty);
            RB.Show();
        }

        private void TicketOrderBtn_Click(object sender, EventArgs e)
        {
            TicketOrderForm TO = new TicketOrderForm();
            ITicketOrderViewRef = TO;
            TO.Owner = this;
            TicketOrder_Click?.Invoke(this, EventArgs.Empty);
            TO.Show();
        }


        private void ExitBtn_Click(object sender, EventArgs e)
        {
            parentForm.Visible = true;
            Close();
        }

        private void PatientRegistrationBtn_Click(object sender, EventArgs e)
        {
            RegistrationForm RF = new RegistrationForm();
            IRegistrationViewRef = RF;
            Registration_Click?.Invoke(this, EventArgs.Empty);
            RF.Show();
        }

        private void PrintTicketBtn_Click(object sender, EventArgs e)
        {
            PrintTicketForm PT = new PrintTicketForm();
            PT.Owner = this;
            IPrintTicketViewRef = PT;
            PrintTicket_Click?.Invoke(this, EventArgs.Empty);
            PT.Show();
        }

        private void GoToRegister_Click(object sender, EventArgs e)
        {
            RegistersForm RegF = new RegistersForm();
            RegF.Owner = this;
            IRegistersViewRef = RegF;
            Registers_Click?.Invoke(this, EventArgs.Empty);
            RegF.Show();
        }

        private void RoomsRegisterBtn_Click(object sender, EventArgs e)
        {
            RoomsRegister RR = new RoomsRegister();
            IRoomsRegisterViewRef = RR;
            RoomsRegister_Click?.Invoke(this, EventArgs.Empty);
            RR.Show();
        }

        private void RegistratorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            parentForm.Visible = true;
            h = m = s = 0;
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
