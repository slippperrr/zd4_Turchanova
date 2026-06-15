using Xamarin.Forms;
using Calculyator_Trchanova.Views;

namespace Calculyator_Trchanova
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new WelcomePage())
            {
                BarBackgroundColor = Color.FromHex("#4CAF50"),
                BarTextColor = Color.White
            };
        }

        protected override void OnStart() { }
        protected override void OnSleep() { }
        protected override void OnResume() { }
    }
}