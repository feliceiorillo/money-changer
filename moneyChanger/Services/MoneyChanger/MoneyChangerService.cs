using moneyChanger.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace moneyChanger.Services.MoneyChanger
{
    public class MoneyChangerService : IMoneyChangerService
    {
        private Dictionary<string,decimal> _currenciesRate;
        
        public void LoadCurrencyRates(Dictionary<string, decimal> currenciesRate)
        {
            _currenciesRate = currenciesRate;
        }

        public decimal ExchangeCurrency(string sourceCurrency, string destinationCurrency, decimal amount, DateTime date)
        {
            if(_currenciesRate == null)
            {
                throw new ArgumentNullException("First call the load currency method");
            }

            if(string.IsNullOrEmpty(sourceCurrency))
            {
                throw new ArgumentNullException(nameof(sourceCurrency));
            }

            if(string.IsNullOrEmpty(destinationCurrency))
            {
                throw new ArgumentNullException(nameof(destinationCurrency));
            }

            if(amount == 0)
            {
                throw new ArgumentNullException(nameof(amount));
            }

            if(!_currenciesRate.TryGetValue(sourceCurrency, out decimal sourceRates))
            {
                throw new InvalidCurrencyException($"Currency {sourceCurrency} is not supported");
            }

            if (!_currenciesRate.TryGetValue(destinationCurrency, out decimal destinationRates))
            {
                throw new InvalidCurrencyException($"Currency {destinationCurrency} is not supported");
            }

            var euroValue = amount / sourceRates;
            return euroValue * destinationRates;

        }

    }
}
