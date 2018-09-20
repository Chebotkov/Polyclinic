namespace PolyclinicView
{
    partial class RegistratorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegistratorForm));
            this.PatientRegistrationBtn = new System.Windows.Forms.Button();
            this.TicketOrderBtn = new System.Windows.Forms.Button();
            this.ReferenceBookBtn = new System.Windows.Forms.Button();
            this.PrintTicketBtn = new System.Windows.Forms.Button();
            this.ExitBtn = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.GoToRegisterBtn = new System.Windows.Forms.Button();
            this.RoomsRegisterBtn = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PatientRegistrationBtn
            // 
            this.PatientRegistrationBtn.Location = new System.Drawing.Point(12, 19);
            this.PatientRegistrationBtn.Name = "PatientRegistrationBtn";
            this.PatientRegistrationBtn.Size = new System.Drawing.Size(117, 44);
            this.PatientRegistrationBtn.TabIndex = 0;
            this.PatientRegistrationBtn.Text = "Зарегистрировать пациента";
            this.PatientRegistrationBtn.UseVisualStyleBackColor = true;
            this.PatientRegistrationBtn.Click += new System.EventHandler(this.PatientRegistrationBtn_Click);
            // 
            // TicketOrderBtn
            // 
            this.TicketOrderBtn.Location = new System.Drawing.Point(12, 205);
            this.TicketOrderBtn.Name = "TicketOrderBtn";
            this.TicketOrderBtn.Size = new System.Drawing.Size(117, 44);
            this.TicketOrderBtn.TabIndex = 1;
            this.TicketOrderBtn.Text = "Заказ талона";
            this.TicketOrderBtn.UseVisualStyleBackColor = true;
            this.TicketOrderBtn.Click += new System.EventHandler(this.TicketOrderBtn_Click);
            // 
            // ReferenceBookBtn
            // 
            this.ReferenceBookBtn.Location = new System.Drawing.Point(243, 87);
            this.ReferenceBookBtn.Name = "ReferenceBookBtn";
            this.ReferenceBookBtn.Size = new System.Drawing.Size(117, 44);
            this.ReferenceBookBtn.TabIndex = 2;
            this.ReferenceBookBtn.Text = "Открыть справочник";
            this.ReferenceBookBtn.UseVisualStyleBackColor = true;
            this.ReferenceBookBtn.Click += new System.EventHandler(this.ReferenceBookBtn_Click);
            // 
            // PrintTicketBtn
            // 
            this.PrintTicketBtn.Location = new System.Drawing.Point(12, 271);
            this.PrintTicketBtn.Name = "PrintTicketBtn";
            this.PrintTicketBtn.Size = new System.Drawing.Size(117, 44);
            this.PrintTicketBtn.TabIndex = 3;
            this.PrintTicketBtn.Text = "Печать талона";
            this.PrintTicketBtn.UseVisualStyleBackColor = true;
            this.PrintTicketBtn.Click += new System.EventHandler(this.PrintTicketBtn_Click);
            // 
            // ExitBtn
            // 
            this.ExitBtn.Location = new System.Drawing.Point(243, 271);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Size = new System.Drawing.Size(117, 44);
            this.ExitBtn.TabIndex = 7;
            this.ExitBtn.Text = "Выход";
            this.ExitBtn.UseVisualStyleBackColor = true;
            this.ExitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 328);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(372, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(140, 17);
            this.toolStripStatusLabel1.Text = "Время текущего сеанса:";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(57, 17);
            this.toolStripStatusLabel2.Text = "(00:00:00)";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // GoToRegisterBtn
            // 
            this.GoToRegisterBtn.Location = new System.Drawing.Point(243, 19);
            this.GoToRegisterBtn.Name = "GoToRegisterBtn";
            this.GoToRegisterBtn.Size = new System.Drawing.Size(117, 44);
            this.GoToRegisterBtn.TabIndex = 9;
            this.GoToRegisterBtn.Text = "Перейти к реестрам";
            this.GoToRegisterBtn.UseVisualStyleBackColor = true;
            this.GoToRegisterBtn.Click += new System.EventHandler(this.GoToRegister_Click);
            // 
            // RoomsRegisterBtn
            // 
            this.RoomsRegisterBtn.Location = new System.Drawing.Point(243, 156);
            this.RoomsRegisterBtn.Name = "RoomsRegisterBtn";
            this.RoomsRegisterBtn.Size = new System.Drawing.Size(117, 44);
            this.RoomsRegisterBtn.TabIndex = 10;
            this.RoomsRegisterBtn.Text = "Реестр кабинетов";
            this.RoomsRegisterBtn.UseVisualStyleBackColor = true;
            this.RoomsRegisterBtn.Click += new System.EventHandler(this.RoomsRegisterBtn_Click);
            // 
            // RegistratorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ClientSize = new System.Drawing.Size(372, 350);
            this.Controls.Add(this.RoomsRegisterBtn);
            this.Controls.Add(this.GoToRegisterBtn);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.ExitBtn);
            this.Controls.Add(this.PrintTicketBtn);
            this.Controls.Add(this.ReferenceBookBtn);
            this.Controls.Add(this.TicketOrderBtn);
            this.Controls.Add(this.PatientRegistrationBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(388, 389);
            this.Name = "RegistratorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Поликлиника";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RegistratorForm_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button PatientRegistrationBtn;
        private System.Windows.Forms.Button TicketOrderBtn;
        private System.Windows.Forms.Button ReferenceBookBtn;
        private System.Windows.Forms.Button PrintTicketBtn;
        private System.Windows.Forms.Button ExitBtn;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Button GoToRegisterBtn;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button RoomsRegisterBtn;
    }
}