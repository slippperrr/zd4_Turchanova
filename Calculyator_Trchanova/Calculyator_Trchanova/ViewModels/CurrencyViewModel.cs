using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Calculyator_Trchanova.Services;

namespace Calculyator_Trchanova.ViewModels
{
    // ViewModel для страницы курсов валют
    public class CurrencyViewModel :BaseViewModel
    {
        private CurrencyService _currencyService; // Сервис получения курсов валют

        // Приватные поля для свойств
        private DateTime _selectedDate = DateTime.Today;
        private string _usdRate = "--";
        private string _eurRate = "--";
        private string _dateDisplay = "";
        private bool _isLoading = false;

        // Команда для загрузки курсов валют
        public ICommand LoadRatesCommand { get; }

        // Конструктор ViewModel
        public CurrencyViewModel ()
        {
            _currencyService = new CurrencyService();
            LoadRatesCommand = new Command(async () => await LoadRates());

            // Автоматическая загрузка курсов при создании страницы
            Device.BeginInvokeOnMainThread(async () => await LoadRates());
        }

        // Свойство выбранной даты
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                SetProperty(ref _selectedDate, value);
                LoadRatesCommand.Execute(null); // Автоматическая загрузка при смене даты
            }
        }

        // Свойство курса USD
        public string USDRate
        {
            get => _usdRate;
            set => SetProperty(ref _usdRate, value);
        }

        // Свойство курса EUR
        public string EURRate
        {
            get => _eurRate;
            set => SetProperty(ref _eurRate, value);
        }

        // Свойство для отображения даты
        public string DateDisplay
        {
            get => _dateDisplay;
            set => SetProperty(ref _dateDisplay, value);
        }

        // Свойство индикатора загрузки
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        // Метод загрузки курсов валют
        private async Task LoadRates ()
        {
            try
            {
                IsLoading = true; // Показать индикатор загрузки

                // Получение курсов с сервера
                var rates = await _currencyService.GetRates(_selectedDate);

                // Обновление отображаемых курсов
                foreach (var rate in rates)
                {
                    if (rate.Code == "USD")
                        USDRate = $"{rate.Rate:F3} руб.";
                    else if (rate.Code == "EUR")
                        EURRate = $"{rate.Rate:F3} руб.";
                }

                DateDisplay = $"на {_selectedDate:dd.MM.yyyy}";
            } catch (Exception ex)
            {
                // Показ ошибки пользователю
                await Application.Current.MainPage.DisplayAlert("Ошибка", $"Не удалось загрузить курсы: {ex.Message}", "OK");
            } finally
            {
                IsLoading = false; // Скрыть индикатор загрузки
            }
        }
    }
}