using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfSurf.Counters
{
    public class PerfCounterService
    {
        private List<PerfCounterWrapper> _counters;

        public PerfCounterService()
        {
            _counters = new List<PerfCounterWrapper>();

            _counters.Add(new PerfCounterWrapper("Процессор", "Processor", "% Processor Time", "_Total"));
            _counters.Add(new PerfCounterWrapper("Память", "Memory", "Pages/sec"));
            _counters.Add(new PerfCounterWrapper("Диск", "PhysicalDisk", "% Disk Time", "_Total"));
        }

        public dynamic GetResults()
        {
            return _counters.Select(c => new { name = c.Name, value = c.Value });
        }
    }
}