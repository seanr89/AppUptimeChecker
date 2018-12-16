using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

class UptimerConsole
{
    // private static string _Url;
    // private static string _Frequency;
    private TimerService _TimerService;
    
    public static async Task Main(string[] args)
    {
        var hostBuilder = new HostBuilder()
             // Add configuration, logging, ...
            .ConfigureServices((hostContext, services) =>
            {
                // Add your services with depedency injection.
                services.AddHostedService<TimerService>();
            });

        await hostBuilder.RunConsoleAsync();
        Console.WriteLine($"Application Complete!");
        Console.ReadLine();
    }
}
