using CurrencyConverter.Enums;
using CsvHelper.Configuration.Attributes;

namespace CurrencyConverter.Models
{
    public class CNBExchangeRateEntry
    {
        [Index(2)]
        public int Amount { get; set; }
        
        [Index(3)]
        public CurrencyCode CurrencyCode { get; set; }
        
        [Index(4)]
        public decimal Rate { get; set; }
    }
}