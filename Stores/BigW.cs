using System;
using System.Text.RegularExpressions;

namespace AusStockChecker.Stores
{
    class BigW : MonitorItem
    {
        public override string TaskCategory { get; set; } = "BigW";

        public override bool Process(string html, out string status)
        {
            status = "";

            var matches = Regex.Matches(html, "<button id=\"addToCartButtonNew\" type=\"submit\" class=\"addToCartButtonNew btn btn-orange\".*?>(.*?)</button>", RegexOptions.Singleline);

            foreach (Match match in matches)
            {
                if (match.Groups[1].Value.Trim().Equals("Add to cart", StringComparison.CurrentCultureIgnoreCase))
                {
                    status = "In stock";
                }
            }

            if (matches.Count == 0)
            {
                matches = Regex.Matches(html, "<button id=\"addToCartButtonNew\" type=\"submit\" class=\"addToCartButtonNew btn btn-IDisabled\".*?>(.*?)</button>", RegexOptions.Singleline);

                foreach (Match match in matches)
                {
                    if (match.Groups[1].Value.Trim().Equals("Add to cart", StringComparison.CurrentCultureIgnoreCase))
                    {
                        status = "Not available";
                    }
                }
            }

            return status.Equals("In stock", StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
