using Quartz;
using Quartz.Impl;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using WinJobScheduleCore.Jobs;
using WinJobScheduleCore.Services;

namespace WinJobScheduleCore.Workers
{
    public abstract class WorkerBase
    {
        public abstract string GroupWorker { get; }

        protected IScheduler Scheduler { get; set; }

        public WorkerBase()
        {
            InitScheduler();
        }

        private async void InitScheduler()
        {
            NameValueCollection props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };

            StdSchedulerFactory factory = new StdSchedulerFactory(props);
            Scheduler = await factory.GetScheduler();
        }

        protected abstract void RegisterJob();

        protected async void AddJob<T>(JobMaster jobMaster) where T : JobBase
        {
            if (jobMaster != null)
            {
                // define the job and tie it to our T class
                IJobDetail job = JobBuilder.Create<T>()
                    .WithIdentity(jobMaster.JobCode, GroupWorker)
                    .Build();

                // Trigger the job
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity(jobMaster.JobCode, GroupWorker)
                    .WithCronSchedule(jobMaster.CronSchedule)
                    .ForJob(jobMaster.JobCode, GroupWorker)
                    .Build();

                // Tell quartz to schedule the job
                await Scheduler.ScheduleJob(job, trigger);

                //Log
                Log.Information(GroupWorker + " - " + jobMaster.JobCode + ": Registered.");
            }
            else
            {
                Log.Information(this.GroupWorker + " - " + typeof(T).Name + ": Job master configuration not found.");
            }
        }

        public void StartWorker()
        {
            try
            {
                Scheduler.Start();
                RegisterJob();
                Log.Information(GroupWorker + " started.");
            }
            catch (SchedulerException se)
            {
                Log.Error(se.Message, GroupWorker + " terminated.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, GroupWorker + " terminated.");
            }
        }

        public async void StopWorker()
        {
            await Scheduler.Shutdown();
            Log.Information(GroupWorker + " stopped.");
        }
    }
}
