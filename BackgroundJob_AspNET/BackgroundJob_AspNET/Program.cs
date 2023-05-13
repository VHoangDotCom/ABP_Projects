using BackgroundJob_AspNET;
using BackgroundJob_AspNET.Configuration;
using BackgroundJob_AspNET.Jobs;
using BackgroundJob_AspNET.Triggers;
using Microsoft.Extensions.Hosting;
using Quartz;

/*
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddHostedService<BackgroundWorkerService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add Quartz Service
builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();
});
builder.Services.AddQuartzHostedService(opt =>
{
    opt.WaitForJobsToComplete = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();*/


var builder = Host.CreateDefaultBuilder()
    .ConfigureServices((cxt, services) =>
    {
        // Add services to the container.
        services.AddQuartz(async q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();

            //Configure the job inside the builder - Run job config crontab in Program.cs
            // Create a "key" for the job
            /* var jobKey = new JobKey("HelloWorldJob");

             // Register the job with the DI container
             q.AddJob<HelloWorldJob>(opts => opts.WithIdentity(jobKey));

             // Create a trigger for the job
             q.AddTrigger(opts => opts
                 .ForJob(jobKey) // link to the HelloWorldJob
                 .WithIdentity("HelloWorldJob-trigger") // give the trigger a unique name
                 .WithCronSchedule("0/5 * * * * ?")); // run every 5 seconds*/

            // Register the job, loading the schedule from configuration
            //Run job config crontab in app.setting - recommend
            //await q.AddJobAndTriggerWithConfiguration<HelloWorldJob>(cxt.Configuration);
            //await q.AddJobAndTriggerWithConfiguration<HelloJob>(cxt.Configuration);
            //await q.AddJobAndTrigger<HelloJob>(jobName => SimpleTrigger.SimpleSpecificMomentTrigger(jobName, 10, 10));
            //await q.AddJobAndTrigger<HelloJob>(jobName => SimpleTrigger.FireInSpecificTimeSetTrigger(jobName, 2, IntervalUnit.Minute));//fail
            //await q.AddJobAndTrigger<HelloJob>(jobName => SimpleTrigger.FireEveryTimeSpanUntilEndDateTrigger(jobName, 10, 14, 0, 10, 16, 0));//start date fail
            await q.AddJobAndTrigger<HelloJob>(jobName => SimpleTrigger.FireEveryMinuteAfterFirstTimeSetTrigger(jobName, 1));//cannot stop execute immediately
        });
        services.AddQuartzHostedService(opt =>
        {
            opt.WaitForJobsToComplete = true;
        });
        services.AddControllers();
        //services.AddHostedService<BackgroundWorkerService>();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }).Build();

await builder.RunAsync();

//Configure the job outside the builder
/*var schedulerFactory = builder.Services.GetRequiredService<ISchedulerFactory>();
var scheduler = await schedulerFactory.GetScheduler();

// define the job and tie it to our HelloJob class
var job = JobBuilder.Create<HelloJob>()
    .WithIdentity("myJob", "group1")
    .Build();

// Trigger the job to run now, and then every 40 seconds
var trigger = TriggerBuilder.Create()
    .WithIdentity("myTrigger", "group1")
    .StartNow()
    .WithSimpleSchedule(x => x
        .WithIntervalInSeconds(10)
        .RepeatForever())
    .Build();

await scheduler.ScheduleJob(job, trigger);
*/
// will block until the last running job completes



