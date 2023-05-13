using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuartzNET_AspNET.Jobs
{
    public class JobWithMutilpleTrigger:IJob
    {
        public static readonly JobKey Key = new JobKey("customer-process", "group");

        public async Task Execute(IJobExecutionContext context)
        {
            var customerId = context.MergedJobDataMap.GetString("CustomerId");
            var batchSize = context.MergedJobDataMap.GetString("batch-size");

            await Console.Out.WriteLineAsync($"CustomerId={customerId} batch-size={batchSize}");
        }

      /*  public async Task DoSomething(IScheduler schedule, CancellationToken ct)
        {
            var job = JobBuilder.Create<JobWithMutilpleTrigger>()
                                .WithIdentity(HelloJob.Key)
                                .UsingJobData("batch-size", "50")
                                .Build();

            await schedule.AddJob(job, replace: true, storeNonDurableWhileAwaitingScheduling: true, ct);

            // Trigger 1
            var jobData1 = new JobDataMap { { "CustomerId", 1 } };
            await scheduler.TriggerJob(HelloJob.Key), jobData1, ct);

            // Trigger 2
            var jobData2 = new JobDataMap { { "CustomerId", 2 } };
            await scheduler.TriggerJob(HelloJob.Key), jobData2, ct);
        }*/
    }
}
