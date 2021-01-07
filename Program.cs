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

namespace AusStockChecker
{
    static class Program
    {
        static StatusBar UptimeStatusBar { get; set; } = new StatusBar()
        {
            Text = "[0s]"
        };
        static DateTime Started = DateTime.Now;
        static int Requests = 0;
        static int BrowserInstances = 0;

        static void Main()
        {
            using var tmr = new Timer(1000);
            tmr.Elapsed += UptimeTimer_Elapsed;
            tmr.Start();
            Console.Gradient("Stock Checker!", gradient: new[] { ConsoleColor.DarkGray, ConsoleColor.DarkBlue, }, pattern: GradientPattern.Word);

            //Console.AddStatusBar(ProgressStatusBar);
            Console.AddStatusBar(UptimeStatusBar);

            Run().Wait();

            tmr.Stop();
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

                var fromAddress = new MailAddress("--- INSERT EMAIL TO SEND FROM ---", "Aus Stock Checker");
                var toAddress = new MailAddress("--- INSERT EMAIL ADDRESS TO NOTIFY ---", "Customer");

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, "--- INSERT YOUR EMAIL PASSWORD TO SEND FROM ---")
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
        static List<MonitorItem> Items = new List<MonitorItem>()
        {
            new Mwave(){Title="3080 ROG STRIX OC", Url="https://www.mwave.com.au/product/asus-geforce-rtx-3080-rog-strix-oc-10gb-video-card-ac38206?vade32={invalidator}"},
            new Mwave(){Title="3090 ROG STRIX OC", Url="https://www.mwave.com.au/product/asus-geforce-rtx-3090-rog-strix-gaming-oc-24gb-video-card-ac38207?vade3w={invalidator}"},

            new Umart(){Title="3080 ROG STRIX OC", Url="https://www.umart.com.au/Asus-ROG-Strix-GeForce-RTX-3080-OC-10G-Graphics-Card_56893G.html?vade32={invalidator}"},
            new Umart(){Title="3090 ROG STRIX OC", Url="https://www.umart.com.au/Asus-ROG-Strix-GeForce-RTX-3090-OC-24G-Graphics-Card_56890G.html?vade3w={invalidator}"},

            new SchemaItem(){Title="3080 ROG STRIX OC", TaskCategory = "Scorptec", Url="https://www.scorptec.com.au/product/graphics-cards/nvidia/85382-rog-strix-rtx3080-o10g-gaming?vade3w={invalidator}"},
            new SchemaItem(){Title="3090 ROG STRIX OC", TaskCategory = "Scorptec", Url="https://www.scorptec.com.au/product/Graphics-Cards/NVIDIA/85441-ROG-STRIX-RTX3090-O24G-GAMING?vade3w={invalidator}"},

            new SchemaItem(){Title="3090 ROG STRIX OC", TaskCategory = "Ple", Url="https://www.ple.com.au/Products/643402/ASUS-GeForce-RTX-3090-ROG-Strix-Gaming-OC-24GB-GDDR6X?vade3w={invalidator}"},
            new SchemaItem(){Title="3080 ROG STRIX OC", TaskCategory = "Ple", Url="https://www.ple.com.au/Products/643403/ASUS-GeForce-RTX-3080-ROG-Strix-Gaming-OC-10GB-GDDR6X?vade3w={invalidator}"},

            new ComputerAlliance(){Title="3080 ROG STRIX OC", TaskCategory = "ComputerAlliance", Url="https://www.computeralliance.com.au/asus-rtx3080-10gb-rog-strix-oc-gaming-pcie-video-card-rog-strix-rtx3080-o10g-gaming?vade3w={invalidator}"},
            new ComputerAlliance(){Title="3090 ROG STRIX OC", TaskCategory = "ComputerAlliance", Url="https://www.computeralliance.com.au/asus-rtx3090-24gb-rog-strix-oc-pcie-video-card-rog-strix-rtx3090-o24g-gaming?vade3w={invalidator}"},

            new PCCaseGear(){Title="3080 ROG STRIX OC", Url="https://www.pccasegear.com/products/51850/asus-rog-strix-geforce-rtx-3080-oc-10gb?vade3w={invalidator}"},
            new PCCaseGear(){Title="3090 ROG STRIX OC", Url="https://www.pccasegear.com/products/51847/asus-rog-strix-geforce-rtx-3090-oc-24gb?vade3w={invalidator}"},
        };
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
                                SysConsole.Beep(5000, 5000); // scare the user into knowing the item they wanted can be purchased. a few minutes late via an email can be a problem in the case of amd/nvidia cpu/gpus etc.
                                SendNotice(task.Title, $"Stock status change detected `{status}`. If this is an in stock status get on this asap to aquire your item.<br/> - " + task.FormattedUrl);
                            }
                        }

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
