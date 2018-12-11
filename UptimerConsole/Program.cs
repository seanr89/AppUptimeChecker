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
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

class UptimerConsole
{
    private static string _Url;
    private static string _Frequency;
    private static APIClient _APIClient;
    private static Timer _Timer;
    static AutoResetEvent _autoEvent = null;
    private static HttpClient _Client;
    
    static void Main(string[] args)
    {
        //initialise parameters with defaults
        _Url = "https://www.google.com";
        _Frequency = "30000";

        if(args.Any())
        {
            _Url = args[0] ?? "https://www.google.com";
            _Frequency = args[1] ?? "30000";
        }

        //initialise the API Client with API Url included to the AzureAPI
        _APIClient = new APIClient("https://siteuptimeapi.azurewebsites.net", _Url);
        _APIClient.InitialiseURLToAPI();

        Console.WriteLine("Starting Timer in 10 secs");
        _Client = new HttpClient();
        _Client.Timeout = TimeSpan.FromSeconds(10.0);
        
        //StartTimer();
        try{
            _autoEvent = new AutoResetEvent(false);
            //ensure current timer is stopped
            _Timer = null;
            //Start a timer to ping the application URL
            _Timer = new Timer(async (s)=> await PingUrl(s), _autoEvent, 10000, Convert.ToInt32(_Frequency));
            //GC.KeepAlive(_Timer);
        }
        catch(System.OutOfMemoryException e)
        {
            Console.WriteLine($"Application Timer has had a memory issue : {e.Message}");
            Environment.Exit(0);
            AppDomain.CurrentDomain.ProcessExit += (sender, eventArgs) => Console.WriteLine("proc exit");
        }
        catch(System.Threading.Tasks.TaskCanceledException et)
        {
            Console.WriteLine($"Application Timer has cancelled for exception : {et.Message}");
            Environment.Exit(0);
            AppDomain.CurrentDomain.ProcessExit += (sender, eventArgs) => Console.WriteLine("proc exit");
        }
        Console.WriteLine($"Application Complete!");
        Console.ReadLine();
    }

    /// <summary>
    /// Method handle the pinging of the url, handling of message response
    /// and the saving of the response
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    private static async Task PingUrl(object state)
    {
        //Console.WriteLine($"PingUrl with current time {DateTime.Now.ToLongTimeString()}");
        var watch = Stopwatch.StartNew();
        var response = await GetResponse();
        if (response != null)
        {
            LogResponse logResponse = CreateLogResponse(response.StatusCode, (int)watch.ElapsedMilliseconds, response);
            await _APIClient.SaveResponse(logResponse);
            logResponse = null;
        }
        response = null;
        watch.Stop();
        watch = null;
        GC.KeepAlive(_Timer);
        // Force a garbage collection to occur for this demo.
        //GC.Collect();
    }

    /// <summary>
    /// ASYNC Request the response from the provided URL on startup
    /// </summary>
    /// <returns>HttpResponseMessage if return accepted</returns>
    private static async Task<HttpResponseMessage> GetResponse()
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _Url);
            return await _Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
        }
        catch (Exception ex)
        {
            Console.WriteLine("GetResponse : Exception caught: " + ex.Message);
            return null;
        }
    }

    static void StartTimer()
    {
        try{
            //ensure current timer is stopped
            _Timer = null;
            //Start a timer to ping the application URL
            _Timer = new Timer(async (s)=> await PingUrl(s), null, 10000, Convert.ToInt32(_Frequency));
        }
        catch(System.OutOfMemoryException e)
        {
            Console.WriteLine($"Application Timer has had a memory issue : {e.Message}");
            StartTimer();
        }
    }

    /// <summary>
    /// Method operation to handle the creation of a log response for saving to the API
    /// </summary>
    /// <param name="responseCode"></param>
    /// <param name="duration"></param>
    /// <param name="message"></param>
    /// <returns>A LogResponse object</returns>
    static LogResponse CreateLogResponse(HttpStatusCode responseCode, int duration, HttpResponseMessage message)
    {
        LogResponse result = new LogResponse();
        //result.URL = _Url;
        result.Duration = duration;
        result.ResponseCode = responseCode;
        result.EventDate = DateTime.Now;
        result.Response = message;
        return result;
    }
}
