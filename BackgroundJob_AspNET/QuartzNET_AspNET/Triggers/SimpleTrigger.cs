using Quartz;
using QuartzNET_AspNET.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Quartz.Logging.OperationName;

namespace QuartzNET_AspNET.Triggers
{
    public class SimpleTrigger
    {
        //To trigger the job later, simply call TriggerJob like below:
        public async Task DoSomething(IScheduler scheduler, CancellationToken ct)
        {
            await scheduler.TriggerJob(new JobKey("name", "group"), ct);
        }

        //Build a trigger for a specific moment in time, with no repeats:
        public async Task SpecificMomentTrigger()
        {
            // trigger builder creates simple trigger by default, actually an ITrigger is returned
            ISimpleTrigger trigger = (ISimpleTrigger)TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartAt(DateTime.Now) // some Date - now
                .ForJob("job1", "group1") // identify job with name, group strings
                .Build();
        }

        //Build a trigger for a specific moment in time, then repeating every ten seconds ten times:
        public async Task SpecificMomentAndRepeatTrigger()
        {
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger3", "group1")
                .StartAt(DateTimeOffset.Now) // if a start time is not given (if this line were omitted), "now" is implied
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .WithRepeatCount(10)) // note that 10 repeats will give a total of 11 firings
                        .ForJob("job1", "group1") // identify job with handle to its JobDetail itself                   
                .Build();
        }

        //Build a trigger that will fire once, five minutes in the future:
        public async Task FireOnceInFutureTrigger()
        {
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger5", "group1")
                .StartAt(DateBuilder.FutureDate(5, IntervalUnit.Minute)) // use DateBuilder to create a date in the future
                .ForJob("job1", "group1") // identify job with its JobKey
                .Build();
        }

        //Build a trigger that will fire now, then repeat every five minutes, until the hour 22:00:
        public async Task FireUntilSpecificEndTimeTrigger()
        {
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger7", "group1")
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(5)
                    .RepeatForever())
                .EndAt(DateBuilder.DateOf(22, 0, 0))
                .Build();
        }

        //Build a trigger that will fire at the top of the next hour, then repeat every 2 hours, forever:
        public async Task FireAtTheStartPointThenRepeatTrigger()
        {
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger8") // because group is not specified, "trigger8" will be in the default group
                .StartAt(DateBuilder.EvenHourDate(null)) // get the next even-hour (minutes and seconds zero ("00:00"))
                .WithSimpleSchedule(x => x
                    .WithIntervalInHours(2)
                    .RepeatForever())
                // note that in this example, 'forJob(..)' is not called 
                //  - which is valid if the trigger is passed to the scheduler along with the job  
                .Build();
        }
    }
}
