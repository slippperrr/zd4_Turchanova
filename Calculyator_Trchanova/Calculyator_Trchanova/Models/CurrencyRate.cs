using System;

namespace Calculyator_Trchanova.Models
{
    // Модель для хранения курса валюты
    public class CurrencyRate
    {
        public string Code { get; set; }      // Код валюты (USD, EUR)
        public decimal Rate { get; set; }     // Курс валюты
        public DateTime Date { get; set; }    // Дата курса
    }
}