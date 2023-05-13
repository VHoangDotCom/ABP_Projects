using Quartz;
using static Quartz.Logging.OperationName;

namespace BackgroundJob_AspNET.Triggers
{
    public static class SimpleTrigger
    {
        //Build a trigger for a specific moment in time, then repeating every ten seconds ten times:
        public static ITrigger SimpleSpecificMomentTrigger(string jobName, int repeatCount, int intervalSeconds)
        {
            var jobKey = new JobKey(jobName);

            var trigger = TriggerBuilder.Create()
                .WithIdentity($"{jobName}-trigger", "group1")
                .StartAt(DateTime.Now) // if a start time is not given (if this line were omitted), "now" is implied
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(intervalSeconds)
                    .WithRepeatCount(repeatCount))
                .ForJob(jobKey) // identify job with handle to its JobDetail itself                   
                .Build();

            return trigger;
        }

        //Build a trigger that will fire once, five minutes in the future:
        public static ITrigger FireInSpecificTimeSetTrigger(string jobName, int timeSpanStart, IntervalUnit timeStyle)
        {
            var jobKey = new JobKey(jobName);

            var trigger = TriggerBuilder.Create()
                .WithIdentity($"{jobName}-trigger", "group1")
                .StartAt(DateBuilder.FutureDate(timeSpanStart, timeStyle))
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(0)
                    .WithRepeatCount(0)) // set the repeat count to 0
                .ForJob(jobKey)
                .Build();

            return trigger;
        }


        //Build a trigger that will fire now, then repeat every five seconds, until the hour 22:00:
        public static ITrigger FireEveryTimeSpanUntilEndDateTrigger(
            string jobName,
            int startHour,
            int startMinute,
            int startSecond,
            int endHour,
            int endMinute,
            int endSecond)
        {
            var jobKey = new JobKey(jobName);

            var trigger = TriggerBuilder.Create()
                .WithIdentity($"{jobName}-trigger", "group1")
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(5)
                    .RepeatForever())
                .StartAt(DateBuilder.TodayAt(startHour, startMinute, startSecond)) // set the start time
                .EndAt(DateBuilder.TodayAt(endHour, endMinute, endSecond)) // set the end time
                .ForJob (jobKey)
                .Build();

            return trigger;
        }

        //Build a trigger that will fire at the top of the next minute, then repeat every minute, forever:
        public static ITrigger FireEveryMinuteAfterFirstTimeSetTrigger(string jobName, int minuteToExecute)
        {
            var jobKey = new JobKey(jobName);

            var trigger = TriggerBuilder.Create()
                .WithIdentity($"{jobName}-trigger-fire-every-minute", "group1")
                .StartAt(DateBuilder.EvenMinuteDate(null))
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(minuteToExecute)
                    .RepeatForever()
                    .WithMisfireHandlingInstructionIgnoreMisfires())
                .ForJob(jobKey)
                .Build();

            return trigger;
        }

        /*
         * Misfire Instruction Constants for SimpleTrigger

            MisfireInstruction.IgnoreMisfirePolicy
            MisfirePolicy.SimpleTrigger.FireNow
            MisfirePolicy.SimpleTrigger.RescheduleNowWithExistingRepeatCount
            MisfirePolicy.SimpleTrigger.RescheduleNowWithRemainingRepeatCount
            MisfirePolicy.SimpleTrigger.RescheduleNextWithRemainingCount
            MisfirePolicy.SimpleTrigger.RescheduleNextWithExistingCount
         */
    }
}
