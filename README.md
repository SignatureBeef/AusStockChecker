# Aus Stock Checker
A tool to monitor stock availability and notify you of items you wish to purchase from a collection of Australian retailers, such as NVIDIA and AMD GPUs/CPUs which are hard to find and when you do you only have minutes to act - every second up your sleeve counts.

Given a list of product urls from these retailers (Umart, Mwave, PC Case Gear, Computer Alliance, PLE, Scorptec) the tool will scan the urls every 30 seconds and it will determine if the item is in stock. If the status has changes to something other than out of stock (in stock, pre order etc) the application will sound a beep and send you and email to purchase your item.

This project was for personal use indended for me to snag one of the new Nvidia GPU's, however it can also be used for nearly all of the items on those retailer websites. 
Given that this could be abused and that it is not intended for anything else i've decided im not providing a compiled binary, and any use of this is at your own risk and i accept no responsibility at all whatsoever. If the tool works for you, great, if not then feel free to PR a fix or deal with it as i have no use for this code at this stage - i'm just putting it up for other people like myself.

![Demo image](https://github.com/DeathCradle/AusStockChecker/blob/main/demo.png?raw=true)

## Requirements
 - Visual Studio / IDE / compiler with .NET5 support
 - Update Program.cs with an email account to send out emails for stock notifications. If you use a gmail account, you might need to allow legacy applications.
 - Update Program.cs with a destination email address (your email address so you receive the notification!). If you use gmail, you can set the sending email address as priority so you get push notifications to your phones home screen.
 - Update Program.cs "Items" list with the retailer, the product url and a title for use within the notification email.
 
 If you are setting this up for the first time, i suggest you use a random in-stock item from the supported retailer of your choice to check to make sure your notifications are working and the PC Beep sound can be heard.
 
 ### Remember: no warranty, responsibility on my behalf or support is provided - use at your own risk!
