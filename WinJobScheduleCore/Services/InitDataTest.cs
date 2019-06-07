using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinJobScheduleCore.Services;

namespace WinJobScheduleCore
{
    public class InitDataTest
    {
        //This is job config, will be get from db
        public static List<JobMaster> ListJobMaster { get; set; } = new List<JobMaster>
        {
            new JobMaster
            {
                JobCode = AppEnum.JobCode.WriteText.ToString(),
                IsActive = true,
                WorkerGroup =AppEnum.WorkerGroup.WorkerTest.ToString(),
                CronSchedule = @"0 * * ? * *"
            }
        };
    }
}
