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
    
    static void Main(string[] args)
    {
        //initialise parameters with defaults
        _Url = "https://www.bbc.co.uk/";
        _Frequency = "15000";

        if(args.Any())
        {
            _Url = args[0] ?? "https://www.bbc.co.uk/";
            _Frequency = args[1] ?? "15000";
        }

        //initialise the API Client with API Url included to the AzureAPI
        _APIClient = new APIClient("https://siteuptimeapi.azurewebsites.net", _Url);
        //_APIClient = new APIClient("http://localhost:5000", _Url);
        _APIClient.InitialiseURLToAPI();

        Console.WriteLine("Starting URL Pinging");
        
        //Start a timer to ping the application URL
        _Timer = new Timer(async (s)=> await PingUrl(s), null, 3000, Convert.ToInt32(_Frequency));
        Console.ReadLine();
        Console.WriteLine($"Application Complete!");
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
        var stopwatch = Stopwatch.StartNew();
        var response = await GetResponse();
        if (response != null)
        {
            LogResponse logResponse = CreateLogResponse(response.StatusCode, (int)stopwatch.ElapsedMilliseconds, response);
            await _APIClient.SaveResponse(logResponse);
            stopwatch.Stop();
        }
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
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(5.0);
                return await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("GetResponse:Exception caught: " + ex.StackTrace);
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
    private static LogResponse CreateLogResponse(HttpStatusCode responseCode, int duration, HttpResponseMessage message)
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
