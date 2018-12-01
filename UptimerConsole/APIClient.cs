using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

/// <summary>
/// class object to handle the Consuming/Communication of an APIClient
/// </summary>
public class APIClient
{
    private string _APIURL;
    private string _URL;
    private bool _IsActive;
    private int _URLID;
    private static HttpClient _HttpClient;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="apiurl">API Response URL</param>
    /// <param name="url"></param>
    public APIClient(string apiurl, string url)
    {
        _APIURL = apiurl;
        _URL = url;
        //Initialise these two parameters by default
        _URLID = 0;
        _IsActive = true;
        _HttpClient = new HttpClient();
        _HttpClient.Timeout = TimeSpan.FromSeconds(5.0);
        _HttpClient.DefaultRequestHeaders.Accept.Clear();
        _HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        TestClient();
    }

    /// <summary>
    /// Method to run and check if the API Client is active and available
    /// Will then set the parameter to handle if the API is running
    /// </summary>
    async void TestClient()
    {
        try
        {
            using(HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(5.0);
                client.DefaultRequestHeaders.Accept.Clear();
                
                var response = await client.GetAsync(_APIURL+"/api/Home/Available");

                if(response.IsSuccessStatusCode)
                {
                    Console.WriteLine("TestClient", "API Client is Active");
                    _IsActive = true;
                }
            }
        }
        catch(HttpRequestException hre)
        {
            Console.WriteLine($"TestClient HttpRequestException Caught {hre.Message}");
        }
        catch(Exception e)
        {
            Console.WriteLine($"Exception Caught {e.Message}");
        }
    }

    /// <summary>
    /// Method to handle the saving of Uptime Ping responses to an API
    /// </summary>
    /// <param name="response">The log response object to be sent</param>
    public async Task SaveResponse(LogResponse response)
    {
        //Console.WriteLine($"SaveResponse at {response.EventDate.ToLongTimeString()}");
        try
        {
            if(_IsActive == false)
            {
                return;
            }
                AppendURLIDTOLogResponse(response, _URLID);
                string jsonResponse = JsonConvert.SerializeObject(response);
                HttpContent _Body = new StringContent(jsonResponse);

                // and add the header to this object instance
                // optional: add a formatter option to it as well
                _Body.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                var APIResponse = await _HttpClient.PostAsync(_APIURL+"/api/Uptime/Post", _Body);
                // if(APIResponse != null)
                // {
                //     Console.WriteLine($"Handling API Response with status: {APIResponse.StatusCode}");
                // }
                // else{
                //     Console.WriteLine($"API Respone was null");
                // }
                //Console.WriteLine($"done");
        }
        catch(HttpRequestException hre)
        {
            Console.WriteLine($"SaveResponse HttpRequestException Caught {hre.Message}");
        }
        catch(Exception e)
        {
            Console.WriteLine($"SaveResponse: Exception Caught {e.Message}");
        }
        
    }

        /// <summary>
    /// Operation to request initialise and request the UniqueID of the URL
    /// </summary>
    /// <returns></returns>
    public async Task InitialiseURLToAPI()
    {
        //Console.WriteLine("Executing: APIClient:InitialiseURLToAPI");
        if(_IsActive)
        {
            try
            {
                //implement the using statement to ensure the client is closed
                using(HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(5.0);
                    client.DefaultRequestHeaders.Accept.Clear();
                    // Construct an HttpContent from a StringContent
                    HttpContent _Body = new StringContent(JsonConvert.SerializeObject(_URL));
                    // and add the header to this object instance
                    // optional: add a formatter option to it as well
                    _Body.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    
                    var APIResponse = await client.PostAsync(_APIURL+"/api/URL/ReadIDForURL", _Body);
                    if(APIResponse != null && APIResponse.IsSuccessStatusCode)
                    {
                        //request the returned content
                        var contents = await APIResponse.Content.ReadAsStringAsync();
                        ReadURLIDFromResponse(contents);
                    }
                    // else
                    // {
                    //     Console.WriteLine($"No response or unsuccessful for method call: InitialiseURLToAPI with response {APIResponse.StatusCode}");
                    // }
                }
            }
            catch(HttpRequestException hre)
            {
                Console.WriteLine($"InitialiseURLToAPI HttpRequestException Caught {hre.Message}");
            }
            catch(Exception e)
            {
                Console.WriteLine($"InitialiseURLToAPI: Exception caught {e.Message}");
            }
        }
    }

    /// <summary>
    /// Operation to parse out the URL contents to set the _URLID
    /// </summary>
    /// <param name="contents">Return parameter content from httpclient event</param>
    private void ReadURLIDFromResponse(string contents)
    {
        _URLID = Convert.ToInt32(contents);
    }

    /// <summary>
    /// Operation to append on the url ID to the LogReponse
    /// </summary>
    /// <param name="response">LogReponse object to append the URL ID onto</param>
    /// <param name="ID"></param>
    private void AppendURLIDTOLogResponse(LogResponse response, int ID)
    {
        response.URLID = ID;
    }
}
