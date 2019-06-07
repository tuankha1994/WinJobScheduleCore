using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinJobScheduleCore
{
    public class AppEnum
    {
        public enum ExecJobStatus
        {
            SUCCESS,
            FAILED
        }

        public enum JobCode
        {
            WriteText
        }

        public enum WorkerGroup
        {
            WorkerTest
        }
    }

    public class AppConstants
    {
        public static string StorageRootPath { get; set; }
    }
}
