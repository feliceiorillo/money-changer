namespace MoneyChanger.WebApi.Models
{
    public class MoneyChangerDto
    {
        public string SourceCurrency { get; set; }
        public string DestinationCurrency { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
