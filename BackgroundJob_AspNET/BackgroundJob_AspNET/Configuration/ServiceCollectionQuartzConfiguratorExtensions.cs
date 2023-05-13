using BackgroundJob_AspNET.Triggers;
using Quartz;

namespace BackgroundJob_AspNET.Configuration
{
    public static class ServiceCollectionQuartzConfiguratorExtensions
    {
        //Execute by Cron-tab in config file
        public static async Task AddJobAndTriggerWithConfiguration<T>(
        this IServiceCollectionQuartzConfigurator quartz,
        IConfiguration config)
        where T : IJob
        {
            // Use the name of the IJob as the appsettings.json key
            string jobName = typeof(T).Name;

            // Try and load the schedule from configuration
            var configKey = $"Quartz:{jobName}";
            var cronSchedule = config[configKey];

            // Some minor validation
            if (string.IsNullOrEmpty(cronSchedule))
            {
                throw new Exception($"No Quartz.NET Cron schedule found for job in configuration at {configKey}");
            }

            // register the job as before
            var jobKey = new JobKey(jobName);
            quartz.AddJob<T>(opts => opts.WithIdentity(jobKey));

            quartz.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity(jobName + "-trigger")
                .WithCronSchedule(cronSchedule)); // use the schedule from configuration
        }

        public static async Task AddJobAndTrigger<J>(
         this IServiceCollectionQuartzConfigurator quartz,
         Func<string, ITrigger> triggerFactory)
         where J : IJob
        {
            var jobName = typeof(J).Name;
            var jobKey = new JobKey(jobName);

            // Create the job detail and associate it with our job type
            var jobDetail = JobBuilder.Create<J>()
                .WithIdentity(jobKey)
                .Build();

            quartz.AddJob<J>(opts => opts.WithIdentity(jobKey));

            // Create the trigger using the triggerFactory function
            var trigger = triggerFactory(jobName);
            quartz.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity(trigger.Key)
                .WithSchedule(trigger.GetScheduleBuilder()));
        }

    }
}
