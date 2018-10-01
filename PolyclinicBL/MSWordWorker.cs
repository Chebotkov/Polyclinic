using System;
using System.IO;
using Word = Microsoft.Office.Interop.Word;

namespace PolyclinicBL
{
    public interface ITicketCreator
    {
        void WriteToWordFile(PrintedTicket printedTicket);
    }

    public class MSWordWorker : ITicketCreator
    {
        public string ProjectName { get; private set; } = "Polyclinic";
        public string PathToTickets { get; private set; } = @"\Files\Tickets\";
        public string TicketTemplateName { get; private set; } = @"\Files\Ticket.docx";
        
        public MSWordWorker() { }

        public MSWordWorker(string projectName)
        {
            if (projectName is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(projectName)));
            }

            ProjectName = projectName;
        }

        public MSWordWorker(string projectName, string PathToTickets) : this(projectName)
        {
            if (PathToTickets is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(PathToTickets)));
            }

            this.PathToTickets = PathToTickets;
        }

        public MSWordWorker(string projectName, string PathToTickets, string TicketTemplateName) : this(projectName, PathToTickets)
        {
            if (TicketTemplateName is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(TicketTemplateName)));
            }

            this.TicketTemplateName = TicketTemplateName;
        }

        public void WriteToWordFile(PrintedTicket printedTicket)
        {
            string Path = String.Concat(GetApplicationsPath() + PathToTickets,  printedTicket.PatientFullName, printedTicket.DocSpecialization, printedTicket.DocFullName, printedTicket.DocRoom, printedTicket.Date + "." + printedTicket.Time.Split(':')[0], ".docx");
            var wordApp = new Word.Application();
            wordApp.Visible = false;
            var wordDoc = wordApp.Documents.Open(GetApplicationsPath() + TicketTemplateName);
            ReplaceWord("{Patient}", printedTicket.PatientFullName, wordDoc);
            ReplaceWord("{Doctor}", printedTicket.DocSpecialization, wordDoc);
            ReplaceWord("{DocFullName}", printedTicket.DocFullName, wordDoc);
            ReplaceWord("{Room}", printedTicket.DocRoom.ToString(), wordDoc);
            ReplaceWord("{Day}", printedTicket.Date, wordDoc);
            ReplaceWord("{Time}", printedTicket.Time, wordDoc);

            wordDoc.SaveAs2(FileName: Path);

            wordApp.Quit(SaveChanges: Word.WdSaveOptions.wdDoNotSaveChanges);
        }

        private void ReplaceWord(string ReplacedWord, string text, Microsoft.Office.Interop.Word.Document document)
        {
            var range = document.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: ReplacedWord, ReplaceWith: text);
        }

        private string GetApplicationsPath()
        {
            string pathToAssembly = Directory.GetCurrentDirectory();

            string currentPath = Directory.GetParent(Path.GetDirectoryName(pathToAssembly)).ToString();

            while (currentPath.Substring(currentPath.Length - ProjectName.Length) != ProjectName)
            {
                currentPath = Directory.GetParent(currentPath).ToString();
            }

            return currentPath;
        }
    }
}
