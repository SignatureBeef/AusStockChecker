using System;
using System.Text.RegularExpressions;

namespace AusStockChecker.Stores
{
    class ComputerAlliance : MonitorItem
    {
        public override string TaskCategory { get; set; } = "Mwave";

        public override bool Process(string html, out string status)
        {
            status = "";
            var matches = Regex.Matches(html, "<a class\\=\"preorder\" .*?></i> (.*?)</a>");
            foreach (Match match in matches)
            {
                status = match.Groups[1].Value;
            }

            return !status.Equals("Out of Stock", StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
