
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UptimeAPI.DataLayer.Interfaces;
using UptimeAPI.Models;
using UptimeAPI.Utilities;

namespace UptimeAPI.DataLayer
{
    public class SiteDataStore : ISiteDataStore
    {
        private readonly ILogger _Logger;
        private readonly DataConnectionSettings _ConnectionSettings;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="connectionSettings"></param>
        public SiteDataStore(ILogger<SiteDataStore> logger,
            IOptions<DataConnectionSettings> connectionSettings)
        {
            _Logger = logger;
            _ConnectionSettings = connectionSettings.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="website"></param>
        /// <returns></returns>
        public Task<int> DeleteWebSite(WebsiteDetails website)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Operation to query and return a collection of URL/Website details
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<WebsiteDetails>> GetAllWebSites()
        {
            List<WebsiteDetails> result = null;
            try
            {
                using(SqlConnection conn = new SqlConnection(_ConnectionSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("READ_URLS", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        await conn.OpenAsync();

                        SqlDataReader reader = await cmd.ExecuteReaderAsync();
                        if(reader.HasRows)
                        {
                            result = new List<WebsiteDetails>();
                            WebsiteDetails model = null;
                            while(reader.Read())
                            {
                                model = new WebsiteDetails();
                                model.ID = Convert.ToInt32(reader["id"]);
                                model.Name = reader["address"].ToString();
                                result.Add(model);
                            }
                        }
                        conn.Close();
                        return result;
                    }
                }
            }
            catch(SqlException e)
            {
                _Logger.LogError("", $"Exception in method: {UtilityMethods.GetCallerMemberName()} with message {e.Message}");
                return result;
            }
        }

        /// <summary>
        /// Data operation to query an ID for provided URl (inserting an ID if not present)
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<int> GetURLIDForSite(string url)
        {
            int result = 0;
            try
            {
                using(SqlConnection conn = new SqlConnection(_ConnectionSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("READ_URLIDByName", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await conn.OpenAsync();

                        cmd.Parameters.AddWithValue("STR_URL", url);

                        SqlDataReader reader = await cmd.ExecuteReaderAsync();
                        if(reader.HasRows)
                        {
                            while(reader.Read())
                            {
                                result = Convert.ToInt32(reader["ID"]);
                                return result;
                            }
                        }
                        return result;
                    }
                }
            }
            catch(SqlException e)
            {
                _Logger.LogError("", $"Exception in method: {UtilityMethods.GetCallerMemberName()} with message {e.Message}");
                return result;
            }
        }

        public Task<WebsiteDetails> GetWebSiteByID(int ID)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="website"></param>
        /// <returns></returns>
        public async Task<int> InsertWebSite(string website)
        {
            int result = 0;
            try
            {
                using(SqlConnection conn = new SqlConnection(_ConnectionSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT_URL", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await conn.OpenAsync();

                        SqlParameter retValue = cmd.Parameters.Add("@return", SqlDbType.Int);
                        retValue.Direction = ParameterDirection.ReturnValue;
                        cmd.Parameters.AddWithValue("STR_URL", website);
                        await cmd.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(cmd.Parameters["@return"].Value);

                        return result;
                    }
                }
            }
            catch(SqlException e)
            {
                _Logger.LogError("", $"Exception in method: {UtilityMethods.GetCallerMemberName()} with message {e.Message}");
                return result;
            }
        }

        public Task<int> UpdateWebSite(WebsiteDetails website)
        {
            throw new System.NotImplementedException();
        }
    }
}