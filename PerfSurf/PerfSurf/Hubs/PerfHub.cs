using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using PerfSurf.Counters;

namespace PerfSurf.Hubs
{
    public class PerfHub : Hub
    {
        public PerfHub()
        {
            StartCounterCollection();
        }

        private void StartCounterCollection()
        {
            var task = Task.Factory.StartNew(async () =>
            {
                var perfService = new PerfCounterService();
                while (true)
                {
                    var results = perfService.GetResults();
                    Clients.All.newCounters(results);
                    await Task.Delay(2000);
                }
            }, TaskCreationOptions.LongRunning);            
        }

        public void Send(string message)
        {
            Clients.All.newMessage(string.Format("{0} говорит \"{1}\"", Context.User.Identity.Name, message));
        }
    }
}