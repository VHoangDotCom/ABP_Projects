using Quartz;

namespace BackgroundJob_AspNET.Jobs
{
    public class SampleJob : IJob
    {
        public static readonly JobKey Key = new JobKey("job-name", "group-name");

        public async Task Execute(IJobExecutionContext context) {
            await Console.Out.WriteLineAsync("Sample Job is executing.");
        }

        public async Task DoSomething(IScheduler schedule, CancellationToken ct)
        {
            var trigger = TriggerBuilder.Create()
                        .WithIdentity("a-trigger", "a-group")
                        .ForJob(SampleJob.Key)
                        .StartNow()
                        .Build();

            await schedule.ScheduleJob(trigger, ct);
        }
    }
}
