namespace MoneyChanger.WebApi.Models
{
    public class MoneyChangerDto
    {
        //string sourceCurrency, string destinationCurrency,decimal amount, DateTime date
        public string SourceCurrency { get; set; }
        public string DestinationCurrency { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
