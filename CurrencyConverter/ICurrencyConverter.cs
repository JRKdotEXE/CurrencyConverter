namespace CurrencyConverter {

    public interface ICurrencyConverter {
        DateTime ValidityDate { get; }
        IEnumerable<CurrenyCode> SupportedCurrencies { get; } 
        decimal Convert(CurrencyCode source, CurrencyCode destination, decimal value);
        decimal ConversionRate(CurrencyCode source, CurrencyCode destination);
        decimal isCurrencySupported(CurrencyCode currency);
    }
}