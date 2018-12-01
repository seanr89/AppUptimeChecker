
using System;
using System.Runtime.CompilerServices;

namespace UptimeAPI.Utilities
{
    public static class UtilityMethods
    {
        /// <summary>
        /// Static operation to get the name of the method calling this method
        /// </summary>
        /// <param name="name"></param>
        /// <returns>The method calling name</returns>
        public static string GetCallerMemberName([CallerMemberName]string name = "")
        {
            return name;
        }

        /// <summary>
        /// Return a provide date formatted to a string for usage with SQL query parameters
        /// </summary>
        /// <param name="date">Date in question to format</param>
        /// <returns>A date string formatted to yyyy-MM-dd HH:mm:ss</returns>
        public static string GenerateSQLFormattedDate(DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }

    public static class DateTimeHelpers
    {
        public static bool Between(this DateTime dateTime, DateTime startDate, DateTime endDate)
        {
            return dateTime >= startDate && dateTime <= endDate;
        }
    }
}