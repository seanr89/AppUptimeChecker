
using System.Collections.Generic;
using System.Threading.Tasks;
using UptimeAPI.Models;

namespace UptimeAPI.DataLayer.Interfaces
{
    public interface ISiteDataStore
    {
        /// <summary>
        /// Interface operation to handle the requesting of all website content
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<WebsiteDetails>> GetAllWebSites();

        Task<WebsiteDetails> GetWebSiteByID(int ID);

        Task<int> GetURLIDForSite(string url);

        Task<int> InsertWebSite(string website);

        Task<int> UpdateWebSite(WebsiteDetails website);

        Task<int> DeleteWebSite(WebsiteDetails website);

    }
}