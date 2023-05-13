using Quartz;

namespace BackgroundJob_AspNET.Jobs
{
    [DisallowConcurrentExecution]
    public class HelloJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("HelloJob is executing.");
        }
    }
}
