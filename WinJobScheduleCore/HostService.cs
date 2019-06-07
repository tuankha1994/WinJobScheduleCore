using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WinJobScheduleCore.Workers;

namespace WinJobScheduleCore
{
    internal class HostService : IHostedService, IDisposable
    {
        private List<WorkerBase> WorkerList = new List<WorkerBase>();

        public HostService(IServiceProvider serviceProvider)
        {
            IoC.Instance.SetServiceProvider(serviceProvider);
            WorkerList.Add(new WorkerTest());
        }

        bool disposing = false;
        public void Dispose()
        {
            if (!disposing)
            {
                disposing = true;             
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            foreach (var worker in WorkerList)
            {
                worker.StartWorker();
            }

            Log.Information("OnStarting method called.");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            foreach (var worker in WorkerList)
            {
                worker.StopWorker();
            }

            Log.Information("OnStopping method called.");
            return Task.CompletedTask;
        }
    }
}
