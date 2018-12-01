
using System;

namespace UptimeAPI.Models
{
    public class UptimeRecord
    {
        public int ID { get; set; }

        public int StatusCode { get; set; }

        public DateTime EventDate { get; set; }

        public int Duration { get; set; }
    }
}