using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinJobScheduleCore.Services
{
    public class ExecJobLog
    {
        public long Id { get; set; }
        public string JobCode { get; set; }
        public DateTime? StartExecuteDateTime { get; set; }
        public DateTime? EndExecuteDateTime { get; set; }
        public string ResultMessage { get; set; }
        public string Status { get; set; } //SUCCESS; FAILED; IGNORE
    }

    public interface IExecJobLogService
    {
        void WriteJobLog(ExecJobLog execJobLog);
    }

    public class ExecJobLogService : IExecJobLogService
    {
        public void WriteJobLog(ExecJobLog execJobLog)
        {
            string path = @"Logs\ExecJobLog.txt";

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine("JobCode: " + execJobLog.JobCode);
                sw.WriteLine("StartExecuteDateTime: " + execJobLog.StartExecuteDateTime);
                sw.WriteLine("EndExecuteDateTime: " + execJobLog.EndExecuteDateTime);
                sw.WriteLine("Status: " + execJobLog.Status);
                sw.WriteLine("ResultMessage: " + execJobLog.ResultMessage);
                sw.WriteLine("------------------------------------------------------");
            }
        }
    }
}