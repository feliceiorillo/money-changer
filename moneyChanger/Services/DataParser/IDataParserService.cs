using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace moneyChanger.Services.DataParser
{
    public interface IDataParserService
    {
        /// <summary>
        /// Retrieves the currency rate from ECB 
        /// It should automatically set a cache after the very first call
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        Task<Dictionary<string, decimal>> GetCurrencyRateByDate(DateTime date);
    }
}
