
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UptimeAPI.Business;
using UptimeAPI.Models;
using UptimeAPI.Utilities;

namespace UptimeAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    public class URLController : Controller
    {
        private readonly ILogger _Logger;
        private readonly UptimeManager _UptimeManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="uptimeManager"></param>
        public URLController(ILogger<URLController> logger,
            UptimeManager uptimeManager)
        {
            _Logger = logger;
            _UptimeManager = uptimeManager;
        }

        /// <summary>
        /// Get all unique/distinct URLs that have uptime records
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllURLs()
        {
            _Logger.LogInformation("Urls", UtilityMethods.GetCallerMemberName());
            var result = await _UptimeManager.GetAllWebsites();
            if(result.Any())
            {
                return Ok(result);
            }
            return NotFound();
        }

        /// <summary>
        /// Handle the requesting of a web URL id with the added feature
        /// to insert a new URL if not already available
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ReadIDForURL([FromBody] string url)
        {
            if(url == null)
            {
                return BadRequest("No url present");
            }
            
            int result = await _UptimeManager.GetURLID(url);
            if(result != 0)
                return Ok(result);
            else //Not able to find and ID so attempt to insert the new URL
            {
                _Logger.LogInformation("Unable to find ID, Attempting to insert and create");
                WebsiteDetails site = new WebsiteDetails(){Name = url};
                result = await _UptimeManager.InsertSite(url);
                if(result != 0)
                {
                    return Ok(result);
                }
            }
            return BadRequest("No ID could be found or generated!");
        }

        /// <summary>
        /// ASYNC operation to handle the insertion of a new web URL
        /// </summary>
        /// <param name="websiteURL">Website details</param>
        /// <returns>An IActionResult object with the attached URL ID</returns>
        [HttpPost]
        public async Task<IActionResult> InsertURL([FromBody] string websiteURL)
        {
            int result = await _UptimeManager.InsertSite(websiteURL);
            if(result != 0)
            {
                return Ok(result);
            }
            return BadRequest("Unable to generate URL ID");
        }
    }
}