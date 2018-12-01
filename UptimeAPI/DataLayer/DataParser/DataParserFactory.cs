
using Microsoft.Extensions.Logging;
using UptimeAPI.DataLayer.DataParser.Interfaces;

namespace UptimeAPI.DataLayer.DataParser
{
    /// <summary>
    /// factory class to handle the selection of a data parser class based
    /// </summary>
    public class DataParserFactory
    {
        private readonly ILogger _Logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Data logging class</param>
        public DataParserFactory(ILogger<DataParserFactory> logger)
        {
            this._Logger = logger;
        }

        /// <summary>
        /// Operation to create an DataParser object to create and read UptimeRecord
        /// </summary>
        /// <returns></returns>
        public IDataParser CreateUptimeRecordDataParser()
        {
            throw new System.NotImplementedException();
        }
    }
}