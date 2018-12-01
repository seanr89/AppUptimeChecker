
using System;
using System.Net;
using System.Net.Http;

namespace UptimeAPI.Models
{
    /// <summary>
    /// Object view model for handling Uptime Response Messages
    /// </summary>
    public class UptimeResponseModel
    {
        /// <summary>
        /// URL ID for the URL
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
    }
}