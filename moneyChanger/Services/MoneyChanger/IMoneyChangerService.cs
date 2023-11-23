using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace moneyChanger.Services.MoneyChanger
{
    public interface IMoneyChangerService
    {
        /// <summary>
        /// Calculate the amount from a given source currency to a destination currency, for a given date
        /// </summary>
        /// <param name="sourceCurrency"></param>
        /// <param name="destinationCurrency"></param>
        /// <param name="amount"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        decimal ExchangeCurrency(string sourceCurrency, string destinationCurrency,decimal amount, DateTime date);
    }
}
