using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PolyclinicDBManager;

namespace PolyclinicView
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm mainForm = new MainForm();
            MessageService messageService = new MessageService();
            MainFormModel mainFormModel = new MainFormModel();
           
            MainFormPresenter mainFormPresenter = new MainFormPresenter(mainForm, messageService, mainFormModel);

            Application.Run(mainForm);
        }
    }
}
