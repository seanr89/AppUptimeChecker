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
    private static string _Url;
    private static int _Frequency;
    private TimerService _TimerService;
    
    public static async Task Main(string[] args)
    {
        //initialise parameters with defaults
        _Url = "https://www.google.com";
        _Frequency = 30000;

        if(args.Any())
        {
            int freq = 30000;
            _Url = args[0] ?? "https://www.google.com";
            
            if(int.TryParse(args[1], out freq))
            {
                _Frequency = freq;
            }
        }

        //TODO - add/inject parameters into timer service!
        var hostBuilder = new HostBuilder()
             // Add configuration, logging, ...
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<TimerSettings>(new TimerSettings(_Url, _Frequency));
                // Add your services with depedency injection.
                services.AddHostedService<TimerService>();
            });

        await hostBuilder.RunConsoleAsync();
        Console.WriteLine($"Application Complete!");
        Console.ReadLine();
    }
}
