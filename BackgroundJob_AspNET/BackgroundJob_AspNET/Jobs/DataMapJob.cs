using Quartz;

namespace BackgroundJob_AspNET.Jobs
{
    public class DataMapJob : IJob
    {
        public DataMapJob()
        {
            // define the job and tie it to our DumbJob class
            IJobDetail job = JobBuilder.Create<DataMapJob>()
             .WithIdentity("myJob", "group1") // name "myJob", group "group1"
             .UsingJobData("jobSays", "Hello World!")
             .UsingJobData("myFloatValue", 3.141f)
             .Build();
        }
        public async Task Execute(IJobExecutionContext context)
        {
            JobKey key = context.JobDetail.Key;

            JobDataMap dataMap = context.MergedJobDataMap;  // Note the difference from the previous example

            string jobSays = dataMap.GetString("jobSays");
            float myFloatValue = dataMap.GetFloat("myFloatValue");
            IList<DateTimeOffset> state = (IList<DateTimeOffset>)dataMap["myStateData"];
            state.Add(DateTimeOffset.UtcNow);

            await Console.Error.WriteLineAsync("Instance " + key + " of DumbJob says: " + jobSays + ", and val is: " + myFloatValue);
        }
    }
}
