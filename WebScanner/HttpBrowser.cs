using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Console = SimpleConsole.SimpleConsole;
using SysConsole = System.Console;

namespace WebScanner
{
    public static class HttpBrowser
    {
        static Dictionary<string, string> Headers = new Dictionary<string, string>() {
            { "User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:44.0) Gecko/20100101 Firefox/44.0"},
            { "Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8"},
            { "Accept-Language", "en-US;q=0.5,en;q=0.3"},
        };
        static CookieContainer Cookies = new CookieContainer();
        static HttpClient Client { get; } = new HttpClient(new HttpClientHandler() { CookieContainer = Cookies }, true);

        static int _requests = 0;
        static int _browserInstances = 0;

        public static int Requests => _requests;
        public static int BrowserInstances => _browserInstances;

        static HttpBrowser()
        {
            foreach (var header in Headers)
                Client.DefaultRequestHeaders.Add(header.Key, header.Value);
        }

        public static async Task<string> GetHtmlAsync(string url)
        {
            var response = await FetchHtmlAsync(url);
            if (response == null)
            {
                // open browser, try to get it that way. 
                // handle the capture if possible
                await OpenBrowser(url);

                // retry
                response = await FetchHtmlAsync(url);
            }

            return response;
        }


        static async Task<string> FetchHtmlAsync(string url)
        {
            System.Threading.Interlocked.Increment(ref _requests);
            try
            {
                using HttpResponseMessage response = await Client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                using HttpContent content = response.Content;
                string result = await content.ReadAsStringAsync();

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        static bool Headless { get; set; } = true;

        static async Task<bool> OpenBrowser(string url)
        {
            Console.LocalTime(ConsoleColor.DarkGray).DarkCyan($" Chrome").DarkGray($"> ").WriteLine($"Initialising", ConsoleColor.White);

            System.Threading.Interlocked.Increment(ref _browserInstances);

            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
            Browser browser = null;
            try
            {

                browser = await Puppeteer.LaunchAsync(new LaunchOptions
                {
                    Headless = Headless,
                    //Devtools = true
                }).ConfigureAwait(false);

                var page = await browser.NewPageAsync();
                await page.SetUserAgentAsync(Headers["User-Agent"]);
                await page.SetExtraHttpHeadersAsync(Headers);

                await page.SetViewportAsync(new ViewPortOptions()
                {
                    Width = 980 + new Random().Next(50),
                    Height = 1000 + new Random().Next(200),
                });

                await page.GoToAsync(url, WaitUntilNavigation.Load);

                string title = await page.GetTitleAsync();
                while (title == "Security Challenge")
                {
                    await Task.Delay(1000);
                    Console.LocalTime(ConsoleColor.DarkGray).DarkCyan($" Chrome").DarkGray($"> ").WriteLine($"Waiting on cloudflare challenge", ConsoleColor.White);

                    title = await page.GetTitleAsync();

                    // completed?

                    Headless = false;
                }

                Headless = true;

                // clear existing cookies
                var uri = new Uri(url);
                var existing = Cookies.GetCookies(uri);
                foreach (Cookie co in existing)
                {
                    co.Expires = DateTime.Now.Subtract(TimeSpan.FromDays(1));
                }

                // register the new ones
                var cookies = await page.GetCookiesAsync(url);
                foreach (var cookie in cookies)
                {
                    Cookies.Add(new Cookie(cookie.Name, cookie.Value, cookie.Path, cookie.Domain)
                    {
                        HttpOnly = cookie.HttpOnly == true,
                    });
                }

                Console.LocalTime(ConsoleColor.DarkGray).DarkCyan($" Chrome").DarkGray($"> ").WriteLine($"Challenge completed", ConsoleColor.White);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (browser != null)
                {
                    await browser.CloseAsync();
                    await browser.DisposeAsync();
                }
            }
        }
    }
}
