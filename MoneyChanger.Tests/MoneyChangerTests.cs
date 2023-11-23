using moneyChanger.Exceptions;
using moneyChanger.Services.MoneyChanger;

namespace MoneyChanger.Tests
{
    public class MoneyChangerTests
    {
        [Fact]
        public void ExchangeCurrency_ValidCurrencies_ReturnsCorrectAmount()
        {
            // Arrange
            var rates = new Dictionary<string, decimal>
            {
                { "USD", 1.2m },
                { "CHF", 0.96m }
            };
            var service = new MoneyChangerService();
            service.LoadCurrencyRates(rates);

            // Act
            var result = service.ExchangeCurrency("USD", "CHF", 120m, DateTime.UtcNow);

            // Assert
            Assert.Equal(96m, result);
        }

        [Fact]
        public void ExchangeCurrency_InvalidSourceCurrency_ThrowsException()
        {
            // Arrange
            var rates = new Dictionary<string, decimal> { { "EUR", 1.0m } };
            var service = new MoneyChangerService();
            service.LoadCurrencyRates(rates);

            // Act and Assert
            Assert.Throws<InvalidCurrencyException>(() => service.ExchangeCurrency("USD", "EUR", 100m, DateTime.UtcNow));
        }
    }
}