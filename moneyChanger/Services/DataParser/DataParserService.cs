using moneyChanger.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace moneyChanger.Services.DataParser
{
    public class DataParserService : IDataParserService
    {
        // todo we could read this value from appsettings stored online with IOptionsSnapshot feature
        const int MAX_DAYS = 90;
        private readonly HttpClient _httpClient;

        public DataParserService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        private readonly Uri _baseAddress = new Uri("https://www.ecb.europa.eu");
        const string DAILY_URL = "/stats/eurofxref/eurofxref-daily.xml";
        const string HISTORY_URL = "/stats/eurofxref/eurofxref-hist-90d.xml";

        public async Task<Dictionary<string, decimal>> GetCurrencyRateByDate(DateTime date)
        {
            // check if the date is older than 90 days allowed
            if ((DateTime.UtcNow - date).Days > MAX_DAYS)
            {
                // we could skip this having inside a database all the history of prices if needed 
                // then we could also support 90 days before prices
                throw new UnsupportedDateException($"Error: {nameof(date)} parameter must not be 90 days older");
            }

            // check if we have data because the data will be available at 14:00 CET time
            var cetTime = DateTime.UtcNow.Date.AddHours(1);
            var dataAvailability = cetTime.Date.AddHours(14);

            if (date > dataAvailability)
            {
                throw new UnsupportedDateException($"Error: {nameof(date)} parameter is not valid in the current context. Data is still not available");
            }

            string xml = string.Empty;

            // todo add caching system in order to prevent 
            // useless calls

            _httpClient.BaseAddress = _baseAddress;

            // check wether use the daily or the history URL
            var httpUrl = date > cetTime ? DAILY_URL : HISTORY_URL;
            var response = await _httpClient.GetAsync(httpUrl);
            xml = await response.Content.ReadAsStringAsync();


            if (string.IsNullOrEmpty(xml))
            {
                throw new UnavailableDataException("Error: xml response was empty");
            }


            return ParseXml(xml);
        }

        private Dictionary<string, decimal> ParseXml(string xml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml);

            var manager = new XmlNamespaceManager(doc.NameTable);
            manager.AddNamespace("gesmes", "http://www.gesmes.org/xml/2002-08-01");
            manager.AddNamespace("ecb", "http://www.ecb.int/vocabulary/2002-08-01/eurofxref");

            var cubes = doc.SelectNodes("//ecb:Cube/ecb:Cube/ecb:Cube[@currency]", manager);

            var currencyRates = new Dictionary<string, decimal>();

            foreach (XmlNode cube in cubes)
            {
                string currency = cube.Attributes["currency"].Value;
                if (decimal.TryParse(cube.Attributes["rate"].Value, out decimal rate))
                {
                    currencyRates[currency] = rate;
                }
            }

            return currencyRates;
        }
    }
}
