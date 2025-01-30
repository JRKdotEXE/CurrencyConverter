using CurrencyConverter.Enums;

namespace CurrencyConverter {

    public interface ICurrencyConverter {
        DateTime ValidityDate { get; }
        IEnumerable<CurrencyCode> SupportedCurrencies { get; } 
        decimal Convert(CurrencyCode source, CurrencyCode destination, decimal value);
        decimal ConversionRate(CurrencyCode source, CurrencyCode destination);
        bool isCurrencySupported(CurrencyCode currency);

    }
}