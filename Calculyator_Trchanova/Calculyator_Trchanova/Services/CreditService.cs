using System;
using Calculyator_Trchanova.Models;

namespace Calculyator_Trchanova.Services
{
    // Сервис для расчета кредита
    public class CreditService
    {
        // Метод расчета аннуитетного платежа
        public CreditResult CalculateAnnuity (decimal amount, int months, decimal interestRate)
        {
            decimal monthlyRate = interestRate / 100 / 12;
            decimal monthlyPaymentResult;

            if (monthlyRate == 0)
            {
                monthlyPaymentResult = amount / months;
                return new CreditResult
                {
                    MonthlyPayment = Math.Round(monthlyPaymentResult, 2),
                    TotalAmount = amount,
                    Overpayment = 0,
                    GracePeriodPayment = 0,
                    GracePeriodMonths = 0,
                    FullPaymentAfterGrace = 0
                };
            }

            double factorDouble = Math.Pow((double) (1 + monthlyRate), months);
            decimal factor = (decimal) factorDouble;
            monthlyPaymentResult = amount * monthlyRate * factor / (factor - 1);

            decimal totalAmount = monthlyPaymentResult * months;
            decimal overpayment = totalAmount - amount;

            return new CreditResult
            {
                MonthlyPayment = Math.Round(monthlyPaymentResult, 2),
                TotalAmount = Math.Round(totalAmount, 2),
                Overpayment = Math.Round(overpayment, 2),
                GracePeriodPayment = 0,
                GracePeriodMonths = 0,
                FullPaymentAfterGrace = 0
            };
        }

        // Метод расчета кредита с льготным периодом
        public CreditResult CalculateWithGracePeriod (decimal amount, int months, decimal interestRate, int graceMonths)
        {
            decimal monthlyRate = interestRate / 100 / 12;

            // Проверка: льготный период не может быть больше срока кредита
            if (graceMonths >= months)
            {
                graceMonths = months / 2;
            }

            int remainingMonths = months - graceMonths;

            // Расчет платежа в льготный период (только проценты)
            decimal gracePeriodPayment = amount * monthlyRate;

            // Расчет аннуитетного платежа после льготного периода
            decimal monthlyPaymentAfterGrace;

            if (monthlyRate == 0 || remainingMonths <= 0)
            {
                monthlyPaymentAfterGrace = amount / remainingMonths;
            } else
            {
                double factorDouble = Math.Pow((double) (1 + monthlyRate), remainingMonths);
                decimal factor = (decimal) factorDouble;
                monthlyPaymentAfterGrace = amount * monthlyRate * factor / (factor - 1);
            }

            // Расчет общей суммы выплат
            decimal totalGracePayments = gracePeriodPayment * graceMonths;
            decimal totalMainPayments = monthlyPaymentAfterGrace * remainingMonths;
            decimal totalAmount = totalGracePayments + totalMainPayments;
            decimal overpayment = totalAmount - amount;

            return new CreditResult
            {
                MonthlyPayment = Math.Round(monthlyPaymentAfterGrace, 2),
                TotalAmount = Math.Round(totalAmount, 2),
                Overpayment = Math.Round(overpayment, 2),
                GracePeriodPayment = Math.Round(gracePeriodPayment, 2),
                GracePeriodMonths = graceMonths,
                FullPaymentAfterGrace = Math.Round(monthlyPaymentAfterGrace, 2)
            };
        }
    }
}