using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolyclinicView
{
    public interface IMessageService
    {
        void ShowError(string error);
        void ShowExclamation(string exclamation);
        void ShowWarning(string warning);
        void ShowQuestion(string question);
    }

    public class MessageService : IMessageService
    {
        public void ShowError(string error)
        {
            MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowExclamation(string exclamation)
        {
            MessageBox.Show(exclamation, "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public void ShowWarning(string warning)
        {
            MessageBox.Show(warning, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void ShowQuestion(string question)
        {
            MessageBox.Show(question, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        public void ShowInfo(string info)
        {
            MessageBox.Show(info, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
