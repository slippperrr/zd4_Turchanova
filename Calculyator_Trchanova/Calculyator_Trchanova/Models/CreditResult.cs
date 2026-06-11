using System;

namespace Calculyator_Trchanova.Models
{
    // Модель для хранения результатов расчета кредита
    public class CreditResult
    {
        public decimal MonthlyPayment { get; set; }     // Ежемесячный платеж
        public decimal TotalAmount { get; set; }        // Общая сумма выплат
        public decimal Overpayment { get; set; }        // Переплата по кредиту
        public decimal GracePeriodPayment { get; set; } // Платеж в льготный период
        public int GracePeriodMonths { get; set; }      // Количество месяцев льготного периода
        public decimal FullPaymentAfterGrace { get; set; } // Платеж после льготного периода
    }
}