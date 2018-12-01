
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UptimeAPI.DataLayer.Interfaces;
using UptimeAPI.Models;

namespace UptimeAPI.Business
{
    /// <summary>
    /// Business logic manager for handling 
    /// </summary>
    public class UptimeManager
    {
        private readonly ILogger _Logger;
        private readonly IUptimeDataStore _UptimeDataStore;
        private readonly ISiteDataStore _SiteDataStore;

        public UptimeManager(ILogger<UptimeManager> logger,
            IUptimeDataStore uptimeDataStore,
            ISiteDataStore siteDataStore)
        {
            _Logger = logger;
            _UptimeDataStore = uptimeDataStore ?? throw new ArgumentNullException(nameof(uptimeDataStore));
            _SiteDataStore = siteDataStore ?? throw new ArgumentException(nameof(siteDataStore));
        }

        /// <summary>
        /// ASYNC operation to handle the insertion of a single uptime response record
        /// into the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> InsertUptimeRecord(UptimeResponseModel model)
        {
            // if (model == null) //null check could be moved to controller
            // {
            //     throw new System.ArgumentNullException(nameof(model));
            // }
            int result = await _UptimeDataStore.InsertUptimeRecordAsync(model);
            return result;
        }

        /// <summary>
        /// Operation to request all available website details stored
        /// </summary>
        /// <returns>A collection of WebsiteDetail objects</returns>
        public async Task<IEnumerable<WebsiteDetails>> GetAllWebsites()
        {
            IEnumerable<WebsiteDetails> result = await _SiteDataStore.GetAllWebSites();
            return result;
        }

        /// <summary>
        /// ASYNC Operation to return the URL ID for a web url parameter
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<int> GetURLID(string url)
        {
            int result = await _SiteDataStore.GetURLIDForSite(url);
            return result;
        }

        /// <summary>
        /// ASYNC Insert a new web url parameter
        /// </summary>
        /// <param name="site"></param>
        /// <returns>An integer</returns>
        public async Task<int> InsertSite(string site)
        {
            int result = await _SiteDataStore.InsertWebSite(site);
            return result;
        }

        /// <summary>
        /// Operation to record all of the uptime records for a url by ID
        /// </summary>
        /// <param name="urlID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UptimeRecord>> GetAllUptimeRecordsForURLID(int urlID)
        {
            IEnumerable<UptimeRecord> results = await _UptimeDataStore.GetUptimeRecordsForURLID(urlID);
            return results;
        }

        /// <summary>
        /// Operation to record all of the uptime records for a url by address
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UptimeRecord>> GetAllUptimeRecordsForURL(string url)
        {
            IEnumerable<UptimeRecord> results = await _UptimeDataStore.GetUptimeRecordsForURL(url);
            return results;
        }

        /// <summary>
        /// Operation to check if the latest website uptime record for URL ID is OK
        /// </summary>
        /// <param name="URLID">A unique URL ID</param>
        /// <returns>A boolean variable</returns>
        public async Task<bool> IsWebsiteOK(int URLID)
        {
            bool result = false; //create return parameter and default to false

            UptimeRecord model = await _UptimeDataStore.GetLatestUptimeRecordForURLID(URLID);
            if(model != null)
            {
                if(model.StatusCode == 200)
                {
                    result = true;
                }
            }            
            return result;
        }

        #region Calculations

        /// <summary>
        /// Operation to calculate the percentage uptime for Ok requests
        /// </summary>
        /// <param name="records"></param>
        /// <returns></returns>
        public double CalculatePercentageOkUptime(IEnumerable<UptimeRecord> records)
        {
            double result = 0;
            //Ensure that there are results present
            if(records.Any())
            {
                //Search for all records where the status code is OK(200)
                var OkResults = records.Where(r => r.StatusCode == 200);

                // Calculate the percentage value
                if(OkResults.Any())
                {
                    result = (OkResults.Count() / records.Count()) * 100;
                }
            }
            return result;
        }

        #endregion
    }
}