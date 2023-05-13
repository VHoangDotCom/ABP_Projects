using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using QuartzNET_AspNET.Jobs;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace QuartzNET_AspNET
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            /* var builder = Host.CreateDefaultBuilder()
                 .ConfigureServices((cxt, services) =>
                 {
                     services.AddQuartz(q =>
                     {
                         q.UseMicrosoftDependencyInjectionJobFactory();
                     });
                     services.AddQuartzHostedService(opt =>
                     {
                         opt.WaitForJobsToComplete = true;
                     });
                 }).Build();

             //Execute Job
             var schedulerFactory = builder.Services.GetRequiredService<ISchedulerFactory>();
             var scheduler = await schedulerFactory.GetScheduler();

             // define the job and tie it to our HelloJob class
             IJobDetail job = JobBuilder.Create<HelloJob>()
              .WithIdentity("myJob", "group1")
              .Build();

             // Trigger the job to run now, and then every 40 seconds
             ITrigger trigger = TriggerBuilder.Create()
               .WithIdentity("myTrigger", "group1")
               .StartNow()
               .WithSimpleSchedule(x => x
                .WithIntervalInSeconds(40)
                .RepeatForever())
               .Build();

             await scheduler.ScheduleJob(job, trigger);

             // will block until the last running job completes
             await builder.RunAsync();*/

            //another build
            CreateHostBuilder(args).Build().Run();
        }

        //another build
        public static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
                  .ConfigureServices((hostContext, services) =>
                  {
                      services.AddQuartz(q =>
                      {
                          q.UseMicrosoftDependencyInjectionScopedJobFactory();

                          // Create a "key" for the job
                          var jobKey = new JobKey("HelloWorldJob");

                          // Register the job with the DI container
                          q.AddJob<HelloWorldJob>(opts => opts.WithIdentity(jobKey));

                          // Create a trigger for the job
                          q.AddTrigger(opts => opts
                              .ForJob(jobKey) // link to the HelloWorldJob
                              .WithIdentity("HelloWorldJob-trigger") // give the trigger a unique name
                              .WithCronSchedule("0/5 * * * * ?")); // run every 5 seconds

                      });
                      services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
                      // ...
                  });

    }
}
