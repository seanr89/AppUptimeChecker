
using System;
using System.Net;
using System.Net.Http;
/// <summary>
/// Method to handle the storage of the the URL response messages
/// </summary>
public class LogResponse
{
    /// <summary>
    /// URL event to be called
    /// </summary>
    /// <returns></returns>
    //public string URL { get; set; }

    /// <summary>
    /// The ID returned for the URL on request to the API
    /// </summary>
    /// <returns></returns>
    public int URLID { get; set; }

    /// <summary>
    /// The returned client Response Code
    /// </summary>
    /// <returns></returns>
    public HttpStatusCode ResponseCode { get; set; }

    /// <summary>
    /// The duration of the HTTP Client request
    /// </summary>
    /// <returns></returns>
    public int Duration { get; set; }

    /// <summary>
    /// The Date of the event
    /// TODO - work out the format
    /// </summary>
    /// <returns></returns>
    public DateTime EventDate { get; set; }

    /// <summary>
    /// The HTTP Response object
    /// </summary>
    /// <returns></returns>
    public HttpResponseMessage Response { get; set; }
}