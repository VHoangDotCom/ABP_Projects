using Quartz;
using QuartzNET_AspNET.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuartzNET_AspNET.Tasks
{
    public class TaskJobs
    {
        public async Task DoSomething(IScheduler scheduler, CancellationToken ct)
        {
            var job = JobBuilder.Create<HelloJob>()
                                .WithIdentity("name", "group")
                                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity("name", "group")
                .StartNow()
                .Build();

            await scheduler.ScheduleJob(job, trigger, ct);
        }
    }
}
