using System;
using System.Text.RegularExpressions;

namespace AusStockChecker.Stores
{
    class SchemaItem : MonitorItem
    {
        public override string TaskCategory { get; set; }

        public override bool Process(string html, out string status)
        {
            status = "";

            var matches = Regex.Matches(html, "<script.*? type=\"application\\/ld\\+json\">(.*?)</script>", RegexOptions.Singleline);
            foreach (Match match in matches)
            {
                var json = match.Groups[1].Value; // match.Value.Substring(35, match.Value.Length - 9 - 35);
                try
                {
                    var obj = (dynamic)Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                    if (obj["@type"] == "Product")
                    {
                        status = obj.offers.availability;
                    }
                }
                catch (Exception ex)
                {

                }
            }

            if (matches.Count == 0 || status.Length == 0)
            {
                matches = Regex.Matches(html, "<span class=\"availability\">(.*?)</span>", RegexOptions.Singleline);
                foreach (Match match in matches)
                {
                    status = match.Groups[1].Value;
                }
            }

            return !status.Equals("https://schema.org/PreOrder", StringComparison.CurrentCultureIgnoreCase)
                && !status.Equals("https://schema.org/OutOfStock", StringComparison.CurrentCultureIgnoreCase)
                && !status.Equals("OutOfStock", StringComparison.CurrentCultureIgnoreCase)
                && !string.IsNullOrWhiteSpace(status);
        }
    }
}
