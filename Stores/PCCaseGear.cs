using System;
using System.Text.RegularExpressions;

namespace AusStockChecker.Stores
{
    class PCCaseGear : MonitorItem
    {
        public override string TaskCategory { get; set; } = "Mwave";

        public override bool Process(string html, out string status)
        {
            status = "";
            var matches = Regex.Matches(html, "<button .*?class=\"add-to-cart.*?>(.*?)</button>", RegexOptions.Singleline);
            foreach (Match match in matches)
            {
                status = match.Groups[1].Value.Trim();
            }

            return !status.Equals("Sold Out", StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
