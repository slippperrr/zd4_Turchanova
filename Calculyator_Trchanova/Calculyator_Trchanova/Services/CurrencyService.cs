using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Calculyator_Trchanova.Models;
using System.Net.Http;
using System.Xml.Linq;
using System.Globalization;

namespace Calculyator_Trchanova.Services
{
    // Сервис для получения курсов валют с сайта Центробанка РФ
    public class CurrencyService
    {
        // Метод получения курсов валют на указанную дату
        public async Task<List<CurrencyRate>> GetRates (DateTime date)
        {
            var rates = new List<CurrencyRate>();

            try
            {
                // Формирование URL для запроса к API Центробанка
                string url = $"https://www.cbr.ru/scripts/XML_daily.asp?date_req={date:dd/MM/yyyy}";

                using (var client = new HttpClient())
                {
                    // Получение XML ответа от сервера
                    var response = await client.GetStringAsync(url);
                    var doc = XDocument.Parse(response);

                    // Поиск всех элементов Valute в XML
                    var currencies = doc.Descendants("Valute");

                    // Перебор всех валют для поиска USD и EUR
                    foreach (var currency in currencies)
                    {
                        string code = currency.Element("CharCode")?.Value;
                        if (code == "USD" || code == "EUR")
                        {
                            string rateStr = currency.Element("Value")?.Value;
                            // Парсинг курса валюты
                            if (decimal.TryParse(rateStr, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal rate))
                            {
                                rates.Add(new CurrencyRate
                                {
                                    Code = code,
                                    Rate = rate,
                                    Date = date
                                });
                            }
                        }
                    }
                }
            } catch
            {
                // Fallback данные при ошибке подключения к интернету
                rates.Add(new CurrencyRate { Code = "USD", Rate = 80.000m, Date = date });
                rates.Add(new CurrencyRate { Code = "EUR", Rate = 86.000m, Date = date });
            }

            return rates;
        }
    }
}