using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinJobScheduleCore.Jobs;
using WinJobScheduleCore.Services;

namespace WinJobScheduleCore.Workers
{
    public class WorkerTest : WorkerBase
    {
        private List<JobMaster> JobMasters;
        private readonly IJobMasterService _jobMasterService;

        public WorkerTest()
        {
            _jobMasterService = new JobMasterService();
            JobMasters = _jobMasterService.GetAllJobsMaster();
        }

        public override string GroupWorker
        {
            get
            {
                return AppEnum.WorkerGroup.WorkerTest.ToString();
            }
        }

        protected override void RegisterJob()
        {
            //Add job
            AddJob<JobWriteText>(JobMasters.FirstOrDefault(i => i.JobCode == AppEnum.JobCode.WriteText.ToString()));

            //Add other job
        }
    }
}