﻿using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzNET_AspNET.Jobs
{
    public class SampleJob : IJob
    {
        // have a public key that is easy reference in DI configuration for example
        // group helps you with targeting specific jobs in maintenance operations, 
        // like pause all jobs in group "integration"
        public static readonly JobKey Key = new JobKey("sample-job", "examples");

        public async Task Execute(IJobExecutionContext context)
        {
            if (context.RefireCount > 10)
            {
                // we might not ever succeed!
                // maybe log a warning, throw another type of error, inform the engineer on call
                return;
            }

            try
            {
                // get data out of the MergedJobDataMap
                var value = context.MergedJobDataMap.GetString("some-value");

                // ... do work
                await Task.Delay(100);
            }
            catch (Exception ex)
            {
                // do you want the job to refire?
                throw new JobExecutionException(msg: "", refireImmediately: true, cause: ex);
            }
        }
    }
}
