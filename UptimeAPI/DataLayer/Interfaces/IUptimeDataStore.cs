
using System.Collections.Generic;
using System.Threading.Tasks;
using UptimeAPI.Models;

namespace UptimeAPI.DataLayer.Interfaces
{
    /// <summary>
    /// A new interface to handle requests for CRUD operations on UptimeRecords
    /// </summary>
    public interface IUptimeDataStore
    {
        /// <summary>
        /// Operation to provide the controls to insert a single uptime response model
        /// </summary>
        /// <param name="model"></param>
        /// <returns>An integer value to denote the result</returns>
        Task<int> InsertUptimeRecordAsync(UptimeResponseModel model);

        /// <summary>
        /// Operation to get all UptimeRecords stored
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<UptimeRecord>> GetAllUptimeRecordsAsync();


        /// <summary>
        /// Operation to read all UptimeRecords for a specific URL ID
        /// </summary>
        /// <param name="urlID"></param>
        /// <returns></returns>
        Task<IEnumerable<UptimeRecord>> GetUptimeRecordsForURLID(int urlID);

        /// <summary>
        /// Operation to read all of the Uptime records for a specific URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<IEnumerable<UptimeRecord>> GetUptimeRecordsForURL(string url);


        /// <summary>
        /// Operation to read the latest UptimeRecord for a url with a specific ID
        /// </summary>
        /// <param name="urlID"></param>
        /// <returns></returns>
        Task<UptimeRecord> GetLatestUptimeRecordForURLID(int urlID);
    }
}