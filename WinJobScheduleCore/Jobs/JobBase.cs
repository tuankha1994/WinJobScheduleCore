using Quartz;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinJobScheduleCore.Services;

namespace WinJobScheduleCore.Jobs
{


    public abstract class JobBase : IJob
    {
        private readonly IExecJobLogService _execJobLogService;

        public JobBase()
        {
            _execJobLogService = new ExecJobLogService();
        }
        public abstract string JobCode { get; }

        public ExecJobLog ExecJobLog { get; set; }

        public bool IsTrackedJob = true;

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                try
                {
                    ExecJobLog = new ExecJobLog
                    {
                        JobCode = JobCode,
                        StartExecuteDateTime = DateTime.Now
                    };
                    OnBeforeExecuting();
                    OnExecuting();
                    OnAfterExecuting();
                    ExecJobLog.EndExecuteDateTime = DateTime.Now;
                }
                catch (Exception ex)
                {
                    ExecJobLog.EndExecuteDateTime = DateTime.Now;
                    ExecJobLog.Status = AppEnum.ExecJobStatus.FAILED.ToString();
                    try
                    {
                        ExecJobLog.ResultMessage = ex.Message + "   " + ex.InnerException?.Message;
                        OnError(ex);
                    }
                    catch (Exception ex1)
                    {
                        ExecJobLog.ResultMessage += " -> " + ex1.Message + "   " + ex1.InnerException?.Message;
                    }
                }
                finally
                {
                    //add when set do not tracking job then system don't log ignore status 
                    if (IsTrackedJob == true)
                    {
                        ExecJobLog.Status = AppEnum.ExecJobStatus.SUCCESS.ToString();

                    }
                    _execJobLogService.WriteJobLog(ExecJobLog);
                }
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Log.Error(JobCode, ex.Message);
                await Task.CompletedTask;
            }
        }

        public virtual void OnBeforeExecuting() { }
        public abstract void OnExecuting();
        public virtual void OnAfterExecuting() { }
        public virtual void OnError(Exception ex) { }
    }
}
