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

/*
 * Expression	Meaning
0 0 12 ** ?	Fire at 12pm (noon) every day
0 15 10 ? **	Fire at 10:15am every day
0 15 10 ** ?	Fire at 10:15am every day
0 15 10 ** ? *	Fire at 10:15am every day
0 15 10 ** ? 2005	Fire at 10:15am every day during the year 2005
0 14 * ?	Fire every minute starting at 2pm and ending at 2:59pm, every day
0 0/5 14 ** ?	Fire every 5 minutes starting at 2pm and ending at 2:55pm, every day
0 0/5 14,18 ** ?	Fire every 5 minutes starting at 2pm and ending at 2:55pm, AND fire every 5 minutes starting at 6pm and ending at 6:55pm, every day
0 0-5 14 ** ?	Fire every minute starting at 2pm and ending at 2:05pm, every day
0 10,44 14 ? 3 WED	Fire at 2:10pm and at 2:44pm every Wednesday in the month of March.
0 15 10 ? * MON-FRI	Fire at 10:15am every Monday, Tuesday, Wednesday, Thursday and Friday
0 15 10 15 * ?	Fire at 10:15am on the 15th day of every month
0 15 10 L * ?	Fire at 10:15am on the last day of every month
0 15 10 L-2 * ?	Fire at 10:15am on the 2nd-to-last last day of every month
0 15 10 ? * 6L	Fire at 10:15am on the last Friday of every month
0 15 10 ? * 6L	Fire at 10:15am on the last Friday of every month
0 15 10 ? * 6L 2002-2005	Fire at 10:15am on every last friday of every month during the years 2002, 2003, 2004 and 2005
0 15 10 ? * 6#3	Fire at 10:15am on the third Friday of every month
0 0 12 1/5 * ?	Fire at 12pm (noon) every 5 days every month, starting on the first day of the month.
0 11 11 11 11 ?	Fire every November 11th at 11:11am.
 */
