
using System.Data;
using System.Threading.Tasks;

namespace UptimeAPI.DataLayer.DataParser.Interfaces
{
    public interface IDataParser
    {
        Task<T> CreateObject<T>(IDataReader reader);
    }
}