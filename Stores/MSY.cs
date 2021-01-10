using System;
using System.Text.RegularExpressions;

namespace AusStockChecker.Stores
{
    class MSY : MonitorItem
    {
        public override string TaskCategory { get; set; } = "MSY";

        public override bool Process(string html, out string status)
        {
            status = "";
            var matches = Regex.Matches(html, "<div class=\"product-collateral\">.*?<table>.*?<tr>.*?<td class=\"spec-name title ui-table-summary\"><strong>Delivery</strong></td>.*?<td class=\".*?\"><b>(.*?)</b></td>", RegexOptions.Singleline);
            foreach (Match match in matches)
            {
                status = match.Groups[1].Value.Trim();
                if (!status.Equals("Out of Stock", StringComparison.CurrentCultureIgnoreCase))
                    return true;
            }

            return false;
        }
    }
}
