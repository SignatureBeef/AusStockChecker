using System;

namespace AusStockChecker
{
    abstract class MonitorItem
    {
        public string Title { get; set; }
        public string Url { get; set; }

        public string FormattedUrl => Url.Replace("{invalidator}", DateTime.Now.Ticks.ToString());

        public abstract bool Process(string result, out string status);

        public abstract string TaskCategory { get; set; }

        public string LastStatus { get; set; }
    }
}
