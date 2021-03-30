using AusStockChecker.Stores;
using SimpleConsole;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Timers;
using Console = SimpleConsole.SimpleConsole;
using SysConsole = System.Console;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using System.IO;
using System.Runtime.InteropServices;

namespace AusStockChecker
{
    class UserDetails
    {
        public string FromAddress { get; set; }
        public string FromAddressPassword { get; set; }
        public string ToAddress { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public Item[] Items { get; set; }

    }

    class Item
    {
        public string Title { get; set; }
        public string URL { get; set; }
        public string TaskCategory { get; set; }
    }

    static class Program
    {
        static StatusBar UptimeStatusBar { get; set; } = new StatusBar()
        {
            Text = "[0s]"
        };
        static DateTime Started = DateTime.Now;
        static int Requests = 0;
        static int BrowserInstances = 0;

        static UserDetails userDetails;

        static void Main()
        {
            // Get user details
            try
            {
                using (var sr = new StreamReader("UserDetails.yaml"))
                {

                    var deserializer = new DeserializerBuilder().Build();
                    userDetails = deserializer.Deserialize<UserDetails>(sr.ReadToEnd());
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("User details could not be read:");
                Console.WriteLine(e.Message);
            }

            // Get items desired
            foreach (Item item in userDetails.Items)
            {
                if (String.IsNullOrWhiteSpace(item.TaskCategory))
                {
                    if (item.URL.IndexOf("mwave.com.au", StringComparison.CurrentCultureIgnoreCase) > -1)
                        item.TaskCategory = "Mwave";
                    else if (item.URL.IndexOf("pccasegear.com", StringComparison.CurrentCultureIgnoreCase) > -1)
                        item.TaskCategory = "PCCaseGear";
                    else if (item.URL.IndexOf("umart.com.au", StringComparison.CurrentCultureIgnoreCase) > -1)
                        item.TaskCategory = "Umart";
                    else if (item.URL.IndexOf("scorptec.com.au", StringComparison.CurrentCultureIgnoreCase) > -1)
                        item.TaskCategory = "Scorptec";
                    else if (item.URL.IndexOf("ple.com.au", StringComparison.CurrentCultureIgnoreCase) > -1)
                        item.TaskCategory = "Ple";
                    else if (item.URL.IndexOf("computeralliance.com.au", StringComparison.CurrentCultureIgnoreCase) > -1)
                        item.TaskCategory = "ComputerAlliance";
                    else if (item.URL.IndexOf("msy.com.au", StringComparison.CurrentCultureIgnoreCase) > -1)
                        item.TaskCategory = "MSY";
                    else if (item.URL.IndexOf("cplonline.com.au", StringComparison.CurrentCultureIgnoreCase) > -1)
                        item.TaskCategory = "CPL";
                    else if (item.URL.IndexOf("bigw.com.au", StringComparison.CurrentCultureIgnoreCase) > -1)
                        item.TaskCategory = "BigW";
                }

                switch (item.TaskCategory)
                {
                    case "Mwave":
                        Items.Add(new Mwave() { Title = item.Title, Url = item.URL });
                        break;
                    case "Umart":
                        Items.Add(new Umart() { Title = item.Title, Url = item.URL });
                        break;
                    case "ComputerAlliance":
                        Items.Add(new ComputerAlliance() { Title = item.Title, TaskCategory = item.TaskCategory, Url = item.URL });
                        break;
                    case "PCCaseGear":
                        Items.Add(new PCCaseGear() { Title = item.Title, Url = item.URL });
                        break;
                    case "CPL":
                        Items.Add(new CPLOnline() { Title = item.Title, Url = item.URL });
                        break;
                    case "MSY":
                        Items.Add(new MSY() { Title = item.Title, Url = item.URL });
                        break;
                    case "BigW":
                        Items.Add(new BigW() { Title = item.Title, Url = item.URL });
                        break;
                    default:
                        Items.Add(new SchemaItem() { Title = item.Title, TaskCategory = item.TaskCategory, Url = item.URL });
                        break;
                }
            }

            using var tmr = new Timer(1000);
            tmr.Elapsed += UptimeTimer_Elapsed;
            tmr.Start();
            Console.Gradient("Stock Checker!", gradient: new[] { ConsoleColor.DarkGray, ConsoleColor.DarkBlue, }, pattern: GradientPattern.Word);

            //Console.AddStatusBar(ProgressStatusBar);
            Console.AddStatusBar(UptimeStatusBar);

            Run().Wait();

            tmr.Stop();
        }

        static void Beep()
        {
            // custom beeps only work on windows.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                SysConsole.Beep(5000, 5000);
            }
            else
            {
                SysConsole.Beep();
            }
        }

        private static void UptimeTimer_Elapsed(object sender, ElapsedEventArgs e) => UptimeTimer_Elapsed();
        private static void UptimeTimer_Elapsed()
        {
            var duration = DateTime.Now - Started;
            UptimeStatusBar.Text = $"[u:{duration.ToShortTime()} h:{Requests} a:{BrowserInstances} i:{ItemNumber}/{Items.Count}]";
        }

        static void SendNotice(string title, string notice)
        {
            try
            {
                var fromAddress = new MailAddress(userDetails.FromAddress, "Aus Stock Checker");
                var toAddress = new MailAddress(userDetails.ToAddress, "Customer");

                var smtp = new SmtpClient
                {
                    Host = userDetails.Host,
                    Port = userDetails.Port,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, userDetails.FromAddressPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = "Stock Notification: " + title,
                    Body = notice,
                    IsBodyHtml = true,
                    Priority = MailPriority.High,
                })
                {
                    message.Headers.Add("Importance", "High");
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                Console.LocalTime(ConsoleColor.DarkGray)
                            .DarkCyan($" ERR")
                            .DarkGray($"> ")
                            .WriteLine(ex, ConsoleColor.Red);

                Console.LocalTime(ConsoleColor.DarkGray)
                            .DarkCyan($" ERR")
                            .DarkGray($"> ")
                            .WriteLine("--- ENSURE YOU UPDATE THE EMAIL INFORMATION! ---", ConsoleColor.Red);
            }
        }

        /// UPDATE THE LINKS YOU WANT
        static List<MonitorItem> Items = new List<MonitorItem>();
        static int? ItemNumber { get; set; }

        static async Task Run()
        {
            ItemNumber = null;
            while (Items.Count > 0)
            {
                ItemNumber = 0;
                foreach (var task in Items.ToArray())
                {
                    ItemNumber++;
                    UptimeTimer_Elapsed();

                    await Task.Delay(500);

                    var html = await WebScanner.HttpBrowser.GetHtmlAsync(task.FormattedUrl);

                    if (!String.IsNullOrWhiteSpace(html))
                    {
                        var has_changed = task.Process(html, out string status);
                        if (has_changed)
                        {
                            if (task.LastStatus != status)
                            {
                                task.LastStatus = status;
                                Beep(); // scare the user into knowing the item they wanted can be purchased. a few minutes late via an email can be a problem in the case of amd/nvidia cpu/gpus etc.
                                SendNotice(task.Title, $"Stock status change detected `{status}`. If this is an in stock status get on this asap to aquire your item.<br/> - " + task.FormattedUrl);
                            }
                        }

                        if (String.IsNullOrWhiteSpace(status))
                            status = "*Failed to determine";

                        Console.LocalTime(ConsoleColor.DarkGray)
                            .DarkCyan($" {task.TaskCategory}")
                            .DarkGray($"> ")
                            .Write($"{task.Title} ", ConsoleColor.White)
                            .WriteLine(status, has_changed ? ConsoleColor.Green : ConsoleColor.Red);
                    }
                    else
                    {
                        Console.LocalTime(ConsoleColor.DarkGray)
                            .DarkCyan($" {task.TaskCategory}")
                            .DarkGray($"> ")
                            .Write($"{task.Title} ", ConsoleColor.White)
                            .WriteLine("Failed to fetch", ConsoleColor.Red);
                    }
                }

                await Task.Delay(1000 * 30);
            }
        }
    }
}
