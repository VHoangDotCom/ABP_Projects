using Quartz;

namespace BackgroundJob_AspNET.Triggers
{
    public static class CronTrigger
    {
        //See explaination of Cron trigger here: https://www.freeformatter.com/cron-expression-generator-quartz.html
        //Make Crontab here: http://www.cronmaker.com/

        //Build a trigger that will fire every other minute, between 8am and 5pm, every day:
        public static ITrigger Sample1Trigger(string jobName)
        {
            var jobKey = new JobKey(jobName);

            var trigger = TriggerBuilder.Create()
                .WithIdentity($"{jobName}-trigger1", "group1")
                .StartAt(DateBuilder.EvenMinuteDate(null))
                .WithCronSchedule("0 0/2 8-17 * * ?")
                .ForJob(jobKey)
                .Build();

            return trigger;
        }
        //or
        public static ITrigger Sample1Trigger_V2(string jobName, int inputMinute, int inputHour, int inputDayOfWeek)
        {
            var jobKey = new JobKey(jobName);

            var trigger = TriggerBuilder.Create()
                .WithIdentity($"{jobName}-trigger1", "group1")
                .StartAt(DateBuilder.EvenMinuteDate(null))
                .WithCronSchedule($"0 {inputMinute}/2 {inputHour}-17 * * {inputDayOfWeek}")
                .ForJob(jobKey)
                .Build();

            return trigger;
        }
        //var trigger = Sample1Trigger_V2("jobName", 0, 8, 3); // Runs every 2 minutes from 8 AM to 5 PM on Wednesdays

        //Build a trigger that will fire daily at 10:42 am:
        public static ITrigger Sample2Trigger(string jobName)
        {
            var jobKey = new JobKey(jobName);

            var trigger = TriggerBuilder.Create()
                .WithIdentity($"{jobName}-trigger1", "group1")
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(10, 42))
                .ForJob(jobKey)
                .Build();

            return trigger;
        }
        //or
        public static ITrigger Sample2Trigger_V2(string jobName)
        {
            var jobKey = new JobKey(jobName);

            var trigger = TriggerBuilder.Create()
                .WithIdentity($"{jobName}-trigger1", "group1")
                .WithCronSchedule("0 42 10 * * ?")
                .ForJob(jobKey)
                .Build();

            return trigger;
        }

        //Build a trigger that will fire on Wednesdays at 10:42 am, in a TimeZone other than the system's default:
        public static ITrigger Sample3Trigger(string jobName)
        {
            var jobKey = new JobKey(jobName);

            var trigger = TriggerBuilder.Create()
                .WithIdentity($"{jobName}-trigger1", "group1")
                .WithSchedule(CronScheduleBuilder
                .WeeklyOnDayAndHourAndMinute(DayOfWeek.Wednesday, 10, 42)
                .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time")))
                .ForJob(jobKey)
                .Build();

            return trigger;
        }
        //or
        public static ITrigger Sample3Trigger_V2(string jobName)
        {
            var jobKey = new JobKey(jobName);

            var trigger = TriggerBuilder.Create()
                .WithIdentity($"{jobName}-trigger1", "group1")
                .WithCronSchedule("0 42 10 ? * WED", x => x
                    .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time")))
                .ForJob(jobKey)
                .Build();

            return trigger;
        }
    }
}
