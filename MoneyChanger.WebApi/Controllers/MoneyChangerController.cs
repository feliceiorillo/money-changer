using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using moneyChanger.Services.DataParser;
using moneyChanger.Services.MoneyChanger;
using MoneyChanger.WebApi.Models;

namespace MoneyChanger.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MoneyChangerController : ControllerBase
    {
        private readonly IDataParserService _dataParserService;
        private readonly IMoneyChangerService _moneyChangerService;

        public MoneyChangerController(IDataParserService dataParserService, IMoneyChangerService moneyChangerService)
        {
            _dataParserService = dataParserService;
            _moneyChangerService = moneyChangerService;
        }


        [HttpGet]
        public async Task<IActionResult> ChangeMoney([FromQuery]MoneyChangerDto request)
        {
            try
            {

                // retrieves the dictionary 
                var currencyRates = await _dataParserService.GetCurrencyRateByDate(request.Date);

                // load the currency rates
                _moneyChangerService.LoadCurrencyRates(currencyRates);

                // exchange the currency
                var result = _moneyChangerService.ExchangeCurrency(request.SourceCurrency, request.DestinationCurrency, request.Amount, request.Date);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }



        }
    }
}
