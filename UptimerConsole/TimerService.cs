
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
    private string _Frequency;
    private readonly HttpClient _Client;

    public TimerService()
    {
        _ApiClient = new APIClient("https://siteuptimeapi.azurewebsites.net", "https://www.google.com");
        _ApiClient.InitialiseURLToAPI();

        _Client = new HttpClient();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _Timer = new Timer(DoWork, null, 10000, 
            30000);
            
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _Timer.Dispose();
        _Timer = null;
        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="state"></param>
    private void DoWork(object state)
    {
        Console.WriteLine("Timed Background Service is working.");
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
        GC.KeepAlive(_Timer);
        // Force a garbage collection to occur for this demo.
        //GC.Collect();
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