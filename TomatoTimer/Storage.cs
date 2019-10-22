using System;
using System.IO;
using System.Threading.Tasks;

using Microsoft.VisualBasic;

namespace TomatoTimer
{
    public class Storage
    {
        private const string FileName = "done.xml";

        private AllWorkDone allWorkDone;

        public Storage()
        {
            this.Load();
        }

        private void Load()
        {
            string rawText = File.ReadAllText(FileName);
            this.allWorkDone = rawText.FromXml<AllWorkDone>();
        }

        private async Task Save()
        {
            string rawText = this.allWorkDone.ToXml();
            await File.WriteAllTextAsync(FileName, rawText);
        }

        public WorkDone GetWorkDone(DateTime dateTime)
        {
            return this.Find(dateTime) ?? new WorkDone { Day = DateTime.Today };
        }

        private WorkDone Find(DateTime dateTime)
        {
            return this.allWorkDone.WorkDones.Find(wd => wd.Day.Year == dateTime.Year && wd.Day.DayOfYear == dateTime.DayOfYear);
        }

        public void Store(WorkDone workDone)
        {
            var internalWorkDone = this.Find(workDone.Day);
            if (internalWorkDone != null)
            {
                internalWorkDone.Tomato = workDone.Tomato;
                internalWorkDone.Interruption = workDone.Interruption;
                internalWorkDone.LostFocus = workDone.LostFocus;
            }
            else
            {
                this.allWorkDone.WorkDones.Add(workDone);
            }

            this.Save();
        }
    }
}