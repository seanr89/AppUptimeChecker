using System;

class UptimerConsole
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World!");
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
}
