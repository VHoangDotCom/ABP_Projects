using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzNET_AspNET.Triggers
{
    public class CronTriggers
    {
        //Build a trigger that will fire every other minute, between 8am and 5pm, every day:
        public async Task FireEveryMinuteBetweenTimeSpan()
        {
            ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("trigger3", "group1")
            .WithCronSchedule("0 0/2 8-17 * * ?")
            .ForJob("myJob", "group1")
            .Build();
        }

        //Build a trigger that will fire daily at 10:42 am:
        public async Task FireSpecificTimeEveryday()
        {
            ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("trigger3", "group1")
            .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(10, 42))
            .ForJob("myJob", "group1")
            .Build();
        }
        //or
        public async Task FireSpecificTimeEveryday_V1()
        {
            ITrigger trigger = TriggerBuilder.Create()
             .WithIdentity("trigger3", "group1")
             .WithCronSchedule("0 42 10 * * ?")
             .ForJob("myJob", "group1")
             .Build();
        }
    }
}
