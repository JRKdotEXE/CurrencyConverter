using CsvHelper;
using CsvHelper.Configuration;
using CurrencyConverter.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyConverter.Models;


namespace CurrencyConverter {
    public class CNBCurrencyConverter : ICurrencyConverter {
        public DateTime ValidityDate { get; private set; }
        public IEnumerable<CurrencyCode> SupportedCurrencies { get {
            return ExchangeRates.Keys;
        } }

        public IDictionary<CurrencyCode, decimal> ExchangeRates = new Dictionary<CurrencyCode, decimal>();

        public CNBCurrencyConverter() {
            RefreshAsync().GetAwaiter().GetResult();
        }
        public decimal Convert(CurrencyCode source, CurrencyCode destination, decimal sourceCurrencyAmount) {
            if (sourceCurrencyAmount <= 0) {
                throw new CNBException("Amount must be positive");
            }
            return ConversionRate(source, destination) * sourceCurrencyAmount;
        }
        public decimal ConversionRate(CurrencyCode source, CurrencyCode destination) {
            if (SupportedCurrencies.Contains(source) && SupportedCurrencies.Contains(destination)) {
                return ExchangeRates[source] / ExchangeRates[destination];
            }
            throw new CNBException("Currency not supported");
        }
        public bool isCurrencySupported(CurrencyCode currencyCode) {
           return SupportedCurrencies.Contains(currencyCode);
        }

        public async Task RefreshAsync() {
            try {
                using (var client = new HttpClient()) {
                    using (var result = await client.GetAsync("https://www.cnb.cz/cs/financni-trhy/devizovy-trh/kurzy-devizoveho-trhu/kurzy-devizoveho-trhu/denni_kurz.txt")) {
                        if (result.IsSuccessStatusCode) {
                            var response = await result.Content.ReadAsStringAsync();

                            ValidityDate = DateTime.ParseExact(response.Substring(0, 10), "dd.MM.yyyy", null)
                                .AddHours(14)
                                .AddMinutes(30);
                            
                            var csvConfig = new CsvConfiguration(new CultureInfo("cs-CZ")) {
                                Delimiter = "|",
                                ShouldSkipRecord = (args) => args.Row.Context.Parser.Row == 1 || args.Row.Context.Parser.Row == 2
                            };
                            using (var csvReader = new CsvReader(new StringReader(response), csvConfig)) {
                                
                                
                                foreach (var entry in csvReader.GetRecords<CNBExchangeRateEntry>()) {
                                    Console.WriteLine("{0} {1} {2}", entry.Amount, entry.CurrencyCode, entry.Rate);
                                    ExchangeRates.Add(entry.CurrencyCode, entry.Rate / entry.Amount);
                                }
                            }
                        }
                    }
                }
            } catch (Exception e) {
                throw new Exception("Unable to refresh currency rate", e);
            }
            var czkExchangeRate = new CNBExchangeRateEntry() {
                Amount = 1,
                CurrencyCode = CurrencyCode.CZK,
                Rate = 1
            };
            ExchangeRates.Add(czkExchangeRate.CurrencyCode, czkExchangeRate.Rate / czkExchangeRate.Amount);
        }
    }
}