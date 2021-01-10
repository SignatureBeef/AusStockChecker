using System;
using System.Text.RegularExpressions;

namespace AusStockChecker.Stores
{
    class CPLOnline : MonitorItem
    {
        public override string TaskCategory { get; set; } = "CPL";

        public override bool Process(string html, out string status)
        {
            status = "";
            var matches = Regex.Matches(html, "<td class=\"stock-value\">(.*?)</td>", RegexOptions.Singleline);
            foreach (Match match in matches)
            {
                status = match.Groups[1].Value.Trim();
                if (!status.Equals("Pre Order", StringComparison.CurrentCultureIgnoreCase))
                    return true;
            }

            return false;
        }
    }
}
