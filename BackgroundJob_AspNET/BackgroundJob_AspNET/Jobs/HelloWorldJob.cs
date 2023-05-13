using Quartz;

namespace BackgroundJob_AspNET.Jobs
{
    [DisallowConcurrentExecution]
    public class HelloWorldJob : IJob
    {
        // [DisallowConcurrentExecution] prevents Quartz.NET from trying to run the same job concurrently.
        private readonly ILogger<HelloWorldJob> _logger;
        public HelloWorldJob(ILogger<HelloWorldJob> logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Hello world!");
            return Task.CompletedTask;
        }
    }
}
