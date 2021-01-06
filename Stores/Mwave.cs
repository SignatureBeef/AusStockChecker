using System;
using System.Text.RegularExpressions;

namespace AusStockChecker.Stores
{
    class Mwave : MonitorItem
    {
        public override string TaskCategory { get; set; } = "Mwave";

        public override bool Process(string html, out string status)
        {
            status = "";
            var matches = Regex.Matches(html, "var\\ Instock\\ =\\ '(.*?)';");
            foreach (Match match in matches)
            {
                status = match.Value.Substring(15, match.Value.Length - 15 - 2);
            }

            return !status.Equals("Currently No Stock", StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
