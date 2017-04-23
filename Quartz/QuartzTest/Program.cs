using System;
using System.Net;
using System.Threading;
using Quartz;
using Topshelf;
using Topshelf.Quartz;

namespace QuartzTest
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(c =>
            {
                c.Service<JobExample>(s =>
                {
                    s.ConstructUsing(processor => new JobExample());
                    s.WhenStarted(service => service.Start());
                    s.WhenStopped(service => service.Stop());

                    s.ScheduleQuartzJob(q =>
                    {
                        q.WithJob(() => JobBuilder.Create<JobExample>().WithIdentity("Alex", "HisGroup").Build())
                            .AddTrigger(() =>
                                    TriggerBuilder.Create()
                                        .WithSimpleSchedule(builder => builder
                                            .WithIntervalInSeconds(2)
                                            .RepeatForever())                                            
                                        .Build());
                    });

                    c.SetDescription("Test quartz service");
                    c.SetDisplayName("Test quartz service");
                    c.SetServiceName("TestQuartzService");
                });
            });
        }
    }

    [DisallowConcurrentExecution]
    public class JobExample : IJob
    {
        public void Start()
        {
            Console.WriteLine("Job example started.");
        }

        public void Stop()
        {
            Console.WriteLine("Job example stopped.");
        }

        public void Execute(IJobExecutionContext context)
        {
            var key = context.JobDetail.Key;

            Thread.Sleep(10000);
            Console.WriteLine($"triggered! ThreadId: {Thread.CurrentThread.ManagedThreadId} JobKey: {key.Name}, {key.Group}");
        }
    }
}

