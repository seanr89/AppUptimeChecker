
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

public class TimerService : IHostedService, IDisposable
{
    private Timer _Timer;
    private APIClient _ApiClient;
    private string _Url;
    private int _Frequency;
    private readonly HttpClient _Client;

    // public TimerService()
    // {
    //     _Url = "https://www.google.com";
    //     _Frequency = 30000;
    //     _ApiClient = new APIClient("https://siteuptimeapi.azurewebsites.net", _Url);
    //     _ApiClient.InitialiseURLToAPI();
    //     _Client = new HttpClient();
    // }

    /// <summary>
    /// Constructor with timer settings injected
    /// </summary>
    /// <param name="settings">settings class providing parameters for URL and time frequency</param>
    public TimerService(TimerSettings settings)
    {
        _Url = settings.URL;
        _Frequency = settings.Frequency;
        _ApiClient = new APIClient("https://siteuptimeapi.azurewebsites.net", _Url);
        _ApiClient.InitialiseURLToAPI();

        _Client = new HttpClient();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine($"StartAsync");
        try{
            _Timer = new Timer(async (s)=> await PingUrl(s), null, 5000, 
            _Frequency);
        }
        catch(System.OutOfMemoryException e)
        {
            Console.WriteLine($"Application Timer has had a memory issue : {e.Message}");
            Environment.Exit(0);
            //AppDomain.CurrentDomain.ProcessExit += (sender, eventArgs) => Console.WriteLine("proc exit");
        }
        catch(System.Threading.Tasks.TaskCanceledException et)
        {
            Console.WriteLine($"Application Timer has cancelled for exception : {et.Message}");
            Environment.Exit(0);
            //AppDomain.CurrentDomain.ProcessExit += (sender, eventArgs) => Console.WriteLine("proc exit");
        }      
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _Timer.Dispose();
        _Timer = null;
        return Task.CompletedTask;
    }

    /// <summary>
    /// Method handle the pinging of the url, handling of message response
    /// and the saving of the response
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    private async Task PingUrl(object state)
    {
        //Console.WriteLine($"PingUrl with current time {DateTime.Now.ToLongTimeString()}");
        var watch = Stopwatch.StartNew();
        var response = await GetResponse();
        if (response != null)
        {
            LogResponse logResponse = CreateLogResponse(response.StatusCode, (int)watch.ElapsedMilliseconds, response);
            await _ApiClient.SaveResponse(logResponse);
        }
        // Force a garbage collection to occur for this demo.
        GC.KeepAlive(_Timer);
    }

    /// <summary>
    /// ASYNC Request the response from the provided URL on startup
    /// </summary>
    /// <returns>HttpResponseMessage if return accepted</returns>
    private async Task<HttpResponseMessage> GetResponse()
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

    public void Dispose()
    {
        _Timer?.Dispose();
    }
}