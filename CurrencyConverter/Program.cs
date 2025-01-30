using System;
using System.Collections.Generic;
using CurrencyConverter.Enums;
namespace CurrencyConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            var apiConverter = new ExchangeRateAPIConverter();
            var cnbConverter = new CNBCurrencyConverter();
            
            Console.WriteLine("Which converter would you like to use? (API/CNB)");
            string converter = Console.ReadLine();

            if (converter == "API")
            {
                Console.WriteLine(apiConverter.Convert(CurrencyCode.EUR, CurrencyCode.CZK, 1));
                Console.WriteLine(apiConverter.Convert(CurrencyCode.HUF, CurrencyCode.CZK, 100)); 
                Console.WriteLine(apiConverter.Convert(CurrencyCode.USD, CurrencyCode.GBP, 1));
                Console.WriteLine(apiConverter.Convert(CurrencyCode.EUR, CurrencyCode.USD, 1)); 
                Console.WriteLine(apiConverter.Convert(CurrencyCode.RUB, CurrencyCode.CZK, 100)); 
                Console.WriteLine(apiConverter.Convert(CurrencyCode.CZK, CurrencyCode.USD, 1)); 
            }
            else if (converter == "CNB")
            {
                Console.WriteLine(cnbConverter.Convert(CurrencyCode.EUR, CurrencyCode.CZK, 1));
                Console.WriteLine(cnbConverter.Convert(CurrencyCode.HUF, CurrencyCode.CZK, 100)); 
                Console.WriteLine(cnbConverter.Convert(CurrencyCode.USD, CurrencyCode.GBP, 1)); 
                Console.WriteLine(cnbConverter.Convert(CurrencyCode.EUR, CurrencyCode.USD, 1)); 
                Console.WriteLine(cnbConverter.Convert(CurrencyCode.RUB, CurrencyCode.CZK, 100)); 
            }
            else
            {
                Console.WriteLine("Invalid converter");
            }
            
            // Console.WriteLine(cnbConverter.Convert(CurrencyCode.EUR, CurrencyCode.CZK, 1));
            // Console.WriteLine(cnbConverter.Convert(CurrencyCode.HUF, CurrencyCode.CZK, 100)); 
            // Console.WriteLine(cnbConverter.Convert(CurrencyCode.USD, CurrencyCode.GBP, 1)); 
            // Console.WriteLine(cnbConverter.Convert(CurrencyCode.EUR, CurrencyCode.USD, 1)); 
            // Console.WriteLine(cnbConverter.Convert(CurrencyCode.RUB, CurrencyCode.CZK, 100)); 
        }
    }
}