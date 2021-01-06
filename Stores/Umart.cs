using System.Text.RegularExpressions;

namespace AusStockChecker.Stores
{
    class Umart : MonitorItem
    {
        public override string TaskCategory { get; set; } = "Umart";

        public override bool Process(string html, out string status)
        {
            var matches = Regex.Matches(html, "<button type=\"button\" onClick=\"javascript:addToCart(.*?)\" name=\"bi_addToCart\" class=\"addtocart_btn graphik-bold\">Add To Cart</button>");
            if (matches.Count > 0)
            {
                status = "In stock";
                return true;
            }
            else
            {
                status = "No stock";
                return false;
            }
        }
    }
}
