using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyConverter.Enums;
using CurrencyConverter.Models;
using RestSharp;

namespace CurrencyConverter
{
    public class ExchangeRateAPIConverter : ICurrencyConverter
    {
        public DateTime ValidityDate { get; private set; }
        public IEnumerable<CurrencyCode> SupportedCurrencies => ExchangeRates.Keys;

        public IDictionary<CurrencyCode, decimal> ExchangeRates = new Dictionary<CurrencyCode, decimal>();

        public ExchangeRateAPIConverter()
        {
            RefreshAsync().GetAwaiter().GetResult();
        }

        public decimal Convert(CurrencyCode source, CurrencyCode destination, decimal sourceCurrencyAmount)
        {
            if (sourceCurrencyAmount <= 0)
            {
                throw new CNBException("Amount must be positive");
            }
            return ConversionRate(source, destination) * sourceCurrencyAmount;
        }

        public decimal ConversionRate(CurrencyCode source, CurrencyCode destination)
        {
            if (SupportedCurrencies.Contains(source) && SupportedCurrencies.Contains(destination))
            {
                return ExchangeRates[source] / ExchangeRates[destination];
            }
            throw new CNBException("Currency not supported");
        }

        public bool isCurrencySupported(CurrencyCode currencyCode)
        {
            return SupportedCurrencies.Contains(currencyCode);
        }

        public async Task RefreshAsync() {
            try {
                var client = new RestClient("https://api.exchangerate-api.com/v4/latest/USD");
                var request = new RestRequest("https://api.exchangerate-api.com/v4/latest/USD", Method.Get);
                var response = await client.ExecuteAsync<ExchangeRateApiResponse>(request);

                if (response.IsSuccessful && response.Data != null) {
                    ValidityDate = DateTime.Now;
                    ExchangeRates = response.Data.Rates.ToDictionary(
                        rate => Enum.Parse<CurrencyCode>(rate.Key),
                        rate => rate.Value
                    );
                }
                else
                {
                    throw new Exception("Unable to refresh exchange rates");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Unable to refresh exchange rates", e);
            }
        }
    }

    public class ExchangeRateApiResponse
    {
        public Dictionary<string, decimal> Rates { get; set; }
    }
}