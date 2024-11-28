using CurrencyConverter.Enums;

namespace CurrencyConverter {
    public class CNBCurrencyConverter : ICurrencyConverter {
        public DateTime ValidityDate => throw new NotImplementedException();
        public IEnumerable<CurrencyCode> SupportedCurrencies => throw new NotImplementedException();
        public decimal Convert(CurrencyCode source, CurrencyCode destination, decimal value) {
            return ConversionRate(source, destination) * value;
        }
        public decimal ConversionRate(CurrencyCode source, CurrencyCode destination) {
            throw new NotImplementedException();
        }
        public bool isCurrencySupported(CurrencyCode currencyCode) {
           return SupportedCurrencies.Contains(currencyCode);
        }
    }
}