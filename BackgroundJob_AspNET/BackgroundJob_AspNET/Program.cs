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




