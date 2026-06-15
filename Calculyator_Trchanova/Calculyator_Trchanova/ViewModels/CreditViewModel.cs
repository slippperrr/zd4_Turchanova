using Calculyator_Trchanova.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Calculyator_Trchanova.ViewModels
{
    public class CreditViewModel :BaseViewModel
    {
        private CreditService _creditService;

        private decimal _amount = 100000;
        private int _months = 12;
        private decimal _interestRate = 10;
        private string _selectedPaymentType;
        private int _graceMonths = 3;
        private bool _showGracePeriodSettings;
        private string _monthlyPaymentDisplay = "--";
        private string _totalAmountDisplay = "--";
        private string _overpaymentDisplay = "--";
        private string _gracePeriodInfo = "--";
        private string _infoMessage = "Выберите тип платежа";

        public ObservableCollection<string> PaymentTypes { get; set; }

        public CreditViewModel()
        {
            _creditService = new CreditService();
            PaymentTypes = new ObservableCollection<string>
            {
                "Аннуитетный",
                "Дифференцированный",
                "Льготный период"
            };
            SelectedPaymentType = "Аннуитетный";
            ShowGracePeriodSettings = false;

            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Amount) ||
                e.PropertyName == nameof(Months) ||
                e.PropertyName == nameof(InterestRate) ||
                e.PropertyName == nameof(SelectedPaymentType) ||
                e.PropertyName == nameof(GraceMonths))
            {
                CalculateCredit();
            }

            // Обновляем ShowStandardPaymentLabel при изменении ShowGracePeriodSettings
            if (e.PropertyName == nameof(ShowGracePeriodSettings))
            {
                OnPropertyChanged(nameof(ShowStandardPaymentLabel));
            }
        }

        // НОВОЕ СВОЙСТВО: обратное от ShowGracePeriodSettings
        public bool ShowStandardPaymentLabel => !ShowGracePeriodSettings;

        public decimal Amount
        {
            get => _amount;
            set => SetProperty(ref _amount, value);
        }

        public int Months
        {
            get => _months;
            set => SetProperty(ref _months, value);
        }

        public decimal InterestRate
        {
            get => _interestRate;
            set
            {
                SetProperty(ref _interestRate, value);
                OnPropertyChanged(nameof(InterestRateText));
            }
        }

        public string InterestRateText => $"{InterestRate:F1}%";

        public string SelectedPaymentType
        {
            get => _selectedPaymentType;
            set
            {
                SetProperty(ref _selectedPaymentType, value);
                ShowGracePeriodSettings = (value == "Льготный период");
                OnPropertyChanged(nameof(ShowGracePeriodSettings));
            }
        }

        public int GraceMonths
        {
            get => _graceMonths;
            set
            {
                int newValue = Math.Max(1, Math.Min(value, _months - 1));
                if (newValue != _graceMonths && _months > 1)
                {
                    SetProperty(ref _graceMonths, newValue);
                    OnPropertyChanged(nameof(GraceMonthsText));
                }
            }
        }

        public string GraceMonthsText => $"{GraceMonths} мес.";

        public bool ShowGracePeriodSettings
        {
            get => _showGracePeriodSettings;
            set => SetProperty(ref _showGracePeriodSettings, value);
        }

        public string MonthlyPaymentDisplay
        {
            get => _monthlyPaymentDisplay;
            set => SetProperty(ref _monthlyPaymentDisplay, value);
        }

        public string TotalAmountDisplay
        {
            get => _totalAmountDisplay;
            set => SetProperty(ref _totalAmountDisplay, value);
        }

        public string OverpaymentDisplay
        {
            get => _overpaymentDisplay;
            set => SetProperty(ref _overpaymentDisplay, value);
        }

        public string GracePeriodInfo
        {
            get => _gracePeriodInfo;
            set => SetProperty(ref _gracePeriodInfo, value);
        }

        public string InfoMessage
        {
            get => _infoMessage;
            set => SetProperty(ref _infoMessage, value);
        }

        private void CalculateCredit()
        {
            try
            {
                if (_amount <= 0 || _months <= 0)
                {
                    ClearResults();
                    return;
                }

                if (SelectedPaymentType == "Льготный период")
                {
                    if (_graceMonths <= 0 || _graceMonths >= _months)
                    {
                        GracePeriodInfo = "Льготный период должен быть от 1 до " + (_months - 1) + " месяцев";
                        MonthlyPaymentDisplay = "--";
                        TotalAmountDisplay = "--";
                        OverpaymentDisplay = "--";
                        InfoMessage = "Настройте льготный период";
                        return;
                    }

                    var result = _creditService.CalculateWithGracePeriod(_amount, _months, _interestRate, _graceMonths);
                    MonthlyPaymentDisplay = $"{result.MonthlyPayment:N2} руб.";
                    TotalAmountDisplay = $"{result.TotalAmount:N2} руб.";
                    OverpaymentDisplay = $"{result.Overpayment:N2} руб.";
                    GracePeriodInfo = $"Льготный период: {result.GracePeriodMonths} мес. | Платеж в льготный период: {result.GracePeriodPayment:N2} руб. | После льготного периода: {result.FullPaymentAfterGrace:N2} руб.";
                    InfoMessage = "В льготный период платятся только проценты, тело кредита не уменьшается";
                }
                else if (SelectedPaymentType == "Аннуитетный")
                {
                    var result = _creditService.CalculateAnnuity(_amount, _months, _interestRate);
                    MonthlyPaymentDisplay = $"{result.MonthlyPayment:N2} руб.";
                    TotalAmountDisplay = $"{result.TotalAmount:N2} руб.";
                    OverpaymentDisplay = $"{result.Overpayment:N2} руб.";
                    GracePeriodInfo = "--";
                    InfoMessage = "Аннуитетный платеж - равными долями весь срок";
                }
                else if (SelectedPaymentType == "Дифференцированный")
                {
                    MonthlyPaymentDisplay = "--";
                    InfoMessage = "Дифференцированный платеж - сумма платежа уменьшается каждый месяц";

                    decimal monthlyPrincipal = _amount / _months;
                    decimal totalInterest = 0;
                    decimal firstPayment = 0;
                    decimal lastPayment = 0;

                    for (int i = 1; i <= _months; i++)
                    {
                        decimal remaining = _amount - monthlyPrincipal * (i - 1);
                        decimal interest = remaining * (_interestRate / 100 / 12);
                        totalInterest += interest;

                        if (i == 1)
                            firstPayment = monthlyPrincipal + interest;
                        if (i == _months)
                            lastPayment = monthlyPrincipal + interest;
                    }

                    decimal totalAmount = _amount + totalInterest;
                    decimal overpayment = totalInterest;

                    TotalAmountDisplay = $"{totalAmount:N2} руб.";
                    OverpaymentDisplay = $"{overpayment:N2} руб.";
                    GracePeriodInfo = $"Первый платеж: {firstPayment:N2} руб. | Последний платеж: {lastPayment:N2} руб.";
                }
            }
            catch
            {
                ClearResults();
            }
        }

        private void ClearResults()
        {
            MonthlyPaymentDisplay = "--";
            TotalAmountDisplay = "--";
            OverpaymentDisplay = "--";
            GracePeriodInfo = "--";
        }
    }
}