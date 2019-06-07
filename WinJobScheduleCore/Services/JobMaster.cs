using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinJobScheduleCore.Services
{
    public class JobMaster
    {
        public string JobCode { get; set; }
        public string WorkerGroup { get; set; }
        public string CronSchedule { get; set; }
        public bool IsActive { get; set; }
    }

    public interface IJobMasterService
    {
        List<JobMaster> GetAllJobsMaster();
    }

    public class JobMasterService : IJobMasterService
    {
        public List<JobMaster> GetAllJobsMaster()
        {
            return InitDataTest.ListJobMaster;
        }
    }
}
