using System.Collections.Generic;
using System.Threading.Tasks;
using UptimeAPI.DataLayer.Interfaces;
using UptimeAPI.Models;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using System.Data;
using UptimeAPI.Utilities;

namespace UptimeAPI.DataLayer
{
    /// <summary>
    /// DataStore class to provide Uptime data access
    /// </summary>
    public class UptimeDataStore : IUptimeDataStore
    {
        private readonly ILogger _Logger;
        private readonly DataConnectionSettings _ConnectionSettings;

        /// <summary>
        /// DI Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="connectionSettings"></param>
        public UptimeDataStore(ILogger<UptimeDataStore> logger,
            IOptions<DataConnectionSettings> connectionSettings)
        {
            _Logger = logger;
            _ConnectionSettings = connectionSettings.Value;
        }

        /// <summary>
        /// Handle the insert of an uptime model into the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns>An integer value
        /// 1 for success</returns>
        public async Task<int> InsertUptimeRecordAsync(UptimeResponseModel model)
        {
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_ConnectionSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT_Uptime", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("INT_URLID", model.URLID);
                        cmd.Parameters.AddWithValue("INT_ResponseCode", model.ResponseCode);
                        cmd.Parameters.AddWithValue("INT_Duration", model.Duration);
                        cmd.Parameters.AddWithValue("STR_Date", UtilityMethods.GenerateSQLFormattedDate(model.EventDate));

                        SqlParameter retValue = cmd.Parameters.Add("@return", SqlDbType.Int);
                        retValue.Direction = ParameterDirection.ReturnValue;

                        await conn.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();

                        result = Convert.ToInt32(cmd.Parameters["@return"].Value);

                        return result;
                    }
                }
            }
            catch(SqlException e)
            {
                //TODO
                _Logger.LogError("", $"Exception in method: {UtilityMethods.GetCallerMemberName()} with message {e.Message}");
                return result;
            }
        }

        /// <summary>
        /// Handle the reading of all Uptime records from the database
        /// </summary>
        /// <returns>An IEnumerable of UptimeResponseModel objects</returns>
        public async Task<IEnumerable<UptimeRecord>> GetAllUptimeRecordsAsync()
        {
            List<UptimeRecord> results = null;

            try
            {
                using(SqlConnection conn = new SqlConnection(_ConnectionSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("READ_AllUptimeRecords", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await conn.OpenAsync();

                        SqlDataReader reader = await cmd.ExecuteReaderAsync();
                        if(reader.HasRows)
                        {
                            results = new List<UptimeRecord>();
                            UptimeRecord model = null;
                            while(reader.Read())
                            {
                                model = new UptimeRecord();

                                model.ID = Convert.ToInt32(reader["ID"]);
                                model.StatusCode = Convert.ToInt32(reader["ResponseCode"]);
                                model.Duration = Convert.ToInt32(reader["Duration"]);
                                model.EventDate = Convert.ToDateTime(reader["EventDate"]);

                                results.Add(model);
                            }
                        }
                    }
                }
                return results;
            }
            catch(SqlException e)
            {
                _Logger.LogError("", $"Exception in method: {UtilityMethods.GetCallerMemberName()} with message {e.Message}");
                return results;
            }
        }

        /// <summary>
        /// Operation to read in all UptimeRecords for a URL ID
        /// </summary>
        /// <param name="urlID">unique ID for a URL</param>
        /// <returns>a collection of UptimeRecord objects</returns>
        public async Task<IEnumerable<UptimeRecord>> GetUptimeRecordsForURLID(int urlID)
        {
            List<UptimeRecord> results = null;   
            try
            {
                using(SqlConnection conn = new SqlConnection(_ConnectionSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("READ_AllUptimeRecordsForUrlID", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await conn.OpenAsync();
                        cmd.Parameters.AddWithValue("INT_URLID", urlID);

                        SqlDataReader reader = await cmd.ExecuteReaderAsync();
                        if(reader.HasRows)
                        {
                            results = new List<UptimeRecord>();
                            UptimeRecord model = null;
                            while(reader.Read())
                            {
                                model = new UptimeRecord();

                                model.ID = Convert.ToInt32(reader["ID"]);
                                model.StatusCode = Convert.ToInt32(reader["ResponseCode"]);
                                model.Duration = Convert.ToInt32(reader["Duration"]);
                                model.EventDate = Convert.ToDateTime(reader["EventDate"]);

                                results.Add(model);
                            }
                        }
                    }
                }
                return results;
            }
            catch(SqlException e)
            {
                _Logger.LogError("", $"Exception in method: {UtilityMethods.GetCallerMemberName()} with message {e.Message}");
                return results;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UptimeRecord>> GetUptimeRecordsForURL(string url)
        {
            List<UptimeRecord> results = null;   
            try
            {
                using(SqlConnection conn = new SqlConnection(_ConnectionSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("READ_AllUptimeRecordsForURL", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await conn.OpenAsync();
                        cmd.Parameters.AddWithValue("STR_URL", url);

                        SqlDataReader reader = await cmd.ExecuteReaderAsync();
                        if(reader.HasRows)
                        {
                            results = new List<UptimeRecord>();
                            UptimeRecord model = null;
                            while(reader.Read())
                            {
                                model = new UptimeRecord();

                                model.ID = Convert.ToInt32(reader["ID"]);
                                model.StatusCode = Convert.ToInt32(reader["ResponseCode"]);
                                model.Duration = Convert.ToInt32(reader["Duration"]);
                                model.EventDate = Convert.ToDateTime(reader["EventDate"]);

                                results.Add(model);
                            }
                        }
                    }
                }
                return results;
            }
            catch(SqlException e)
            {
                _Logger.LogError("", $"Exception in method: {UtilityMethods.GetCallerMemberName()} with message {e.Message}");
                return results;
            }
        }

        public async Task<UptimeRecord> GetLatestUptimeRecordForURLID(int urlID)
        {
            UptimeRecord result = null;
            try
            {
                using(SqlConnection conn = new SqlConnection(_ConnectionSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("READ_LatestUptimeForURLID", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await conn.OpenAsync();
                        cmd.Parameters.AddWithValue("INT_URLID", urlID);

                        SqlDataReader reader = await cmd.ExecuteReaderAsync();
                        if(reader.HasRows)
                        {
                            result = new UptimeRecord();
                            while(reader.Read())
                            {
                                result.ID = Convert.ToInt32(reader["ID"]);
                                result.StatusCode = Convert.ToInt32(reader["ResponseCode"]);
                                result.Duration = Convert.ToInt32(reader["Duration"]);
                                result.EventDate = Convert.ToDateTime(reader["EventDate"]);

                                return result;
                            }
                        }
                    }
                }
                return result;
            }
            catch(SqlException e)
            {
                _Logger.LogError("", $"Exception in method: {UtilityMethods.GetCallerMemberName()} with message {e.Message}");
                return result;
            }
        }
    }
}