using Xamarin.Forms;
using Calculyator_Trchanova.ViewModels;

namespace Calculyator_Trchanova.Views
{
    // Страница курсов валют
    public partial class CurrencyPage :ContentPage
    {
        // Конструктор страницы
        public CurrencyPage ()
        {
            InitializeComponent();
            BindingContext = new CurrencyViewModel(); // Установка ViewModel
        }
    }
}