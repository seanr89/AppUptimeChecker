using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using UptimeAPI.Business;
using UptimeAPI.Models;
using UptimeAPI.Utilities;

namespace UptimeAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UptimeController : Controller
    {
        private readonly ILogger _Logger;
        private readonly UptimeManager _UptimeManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        public UptimeController(ILogger<UptimeController> logger,
            UptimeManager uptimeManager)
        {
            _Logger = logger;
            _UptimeManager = uptimeManager;
        }

        /// <summary>
        /// Async Operation to insert a new Uptime Response Record
        /// </summary>
        /// <param name="model">The model to be inserted</param>
        /// <returns>An IActionResult object</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UptimeResponseModel model)
        {
            _Logger.LogInformation("Uptime:Post");
            var result = await _UptimeManager.InsertUptimeRecord(model);
            if(result != 0)
                return Ok("Yeoo");
            else
                return BadRequest();
        }

        /// <summary>
        /// Operation to request the percentage Uptime for the provided URL
        /// </summary>
        /// <param name="webURL"></param>
        /// <returns>An IActionResult with a decimal return
        /// OK(200) With percentage in body context</returns>
        [HttpPost]
        public async Task<IActionResult> CalculatePercentageUptimeForWebUrl([FromBody] string webURL)
        {
            IEnumerable<UptimeRecord> models = await _UptimeManager.GetAllUptimeRecordsForURL(webURL);
            if(models.Any())
            {   
                double Percentage = _UptimeManager.CalculatePercentageOkUptime(models);
                return Ok(Percentage);
            }
            return BadRequest($"no records found for URL: {webURL}");
        }

        /// <summary>
        /// ASYNC Operation to calculate and return the percentage uptime for the URL ID
        /// </summary>
        /// <param name="ID">Unique URL ID</param>
        /// <returns>An IActionResult with a decimal return
        /// OK(200) With percentage in body context</returns>
        [HttpPost]
        public async Task<IActionResult> CalculatePercentageUptimeForWebUrlByID(int ID)
        {
            IEnumerable<UptimeRecord> models = await _UptimeManager.GetAllUptimeRecordsForURLID(ID);
            if(models.Any())
            {
                double Percentage = _UptimeManager.CalculatePercentageOkUptime(models);
                return Ok(Percentage);
            }
            return BadRequest($"no records found for ID: {ID}");
        }

        /// <summary>
        /// Operation to query if the last known url uptime record is ok
        /// TODO - maybe check if the site was check in last 5 mins?
        /// </summary>
        /// <param name="id">Unique URL ID for a web url</param>
        /// <returns>An Ok Status with true/false in body</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> IsOkForID(int id)
        {
            bool isOk = await _UptimeManager.IsWebsiteOK(id);
            return Ok(isOk);
        }

        public async Task<IActionResult> IsWebsiteUptimeOk([FromBody] string url)
        {
            int URLID = await _UptimeManager.GetURLID(url);
            bool result = false;
            if(URLID <= 0)
            {
                return BadRequest("URL is not avaialble");
            }
            IEnumerable<UptimeRecord> allRecords = await _UptimeManager.GetAllUptimeRecordsForURLID(URLID);
            if(allRecords.Any())
            {
                var latestRecords = GetUptimeRecordsForTheLastHour(allRecords);
                if(latestRecords.Any())
                {
                    UptimeRecord latestRecord = latestRecords.OrderByDescending(x => x.ID).ToList()[0];
                    if(latestRecord.StatusCode == 200)
                    {
                        result = true;
                    }
                }
            }
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="records"></param>
        /// <returns></returns>
        bool isUptimeCheckerActiveInLastHour(IEnumerable<UptimeRecord> records)
        {
            bool result = false;

            if(records.Any())
            {
                if(GetUptimeRecordsForTheLastHour(records).Any())
                {
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="records"></param>
        /// <returns></returns>
        IEnumerable<UptimeRecord> GetUptimeRecordsForTheLastHour(IEnumerable<UptimeRecord> records)
        {
            DateTime calculatedDateTime = DateTime.Now.AddHours(-1);
 
            var truncatedResults = records.Where(x => x.EventDate.Between(calculatedDateTime, DateTime.Now));

            return truncatedResults;
        }

    }
}