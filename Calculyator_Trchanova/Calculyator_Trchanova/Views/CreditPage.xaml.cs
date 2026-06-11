using Xamarin.Forms;
using Calculyator_Trchanova.ViewModels;

namespace Calculyator_Trchanova.Views
{
    public partial class CreditPage :ContentPage
    {
        public CreditPage ()
        {
            InitializeComponent();
            BindingContext = new CreditViewModel();
        }
    }
}