using Xamarin.Forms;
using Calculyator_Trchanova.Views;

namespace Calculyator_Trchanova
{
    // Главный класс приложения
    public partial class App :Application
    {
        // Конструктор приложения
        public App ()
        {
            InitializeComponent();

            // Создание вкладок для навигации
            var tabbedPage = new TabbedPage();

            // Добавление страницы кредитного калькулятора
            tabbedPage.Children.Add(new CreditPage()
            {
                Title = "Кредитный калькулятор",
                IconImageSource = "calculator.png"
            });

            // Добавление страницы курсов валют
            tabbedPage.Children.Add(new CurrencyPage()
            {
                Title = "Курсы валют",
                IconImageSource = "currency.png"
            });

            // Установка главной страницы приложения
            MainPage = tabbedPage;
        }

        // Метод, вызываемый при запуске приложения
        protected override void OnStart ()
        {
        }

        // Метод, вызываемый при сворачивании приложения
        protected override void OnSleep ()
        {
        }

        // Метод, вызываемый при восстановлении приложения
        protected override void OnResume ()
        {
        }
    }
}