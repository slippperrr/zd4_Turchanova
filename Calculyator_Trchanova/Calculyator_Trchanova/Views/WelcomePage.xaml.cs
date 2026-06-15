using Xamarin.Forms;
using System.IO;
using System.Reflection;
using Xamarin.Forms.StyleSheets;

namespace Calculyator_Trchanova.Views
{
    public partial class WelcomePage : ContentPage
    {
        public WelcomePage()
        {
            InitializeComponent();
            LoadCssStyles();
        }

        private void LoadCssStyles()
        {
            try
            {
                var assembly = typeof(WelcomePage).GetTypeInfo().Assembly;
                using (var stream = assembly.GetManifestResourceStream("Calculyator_Trchanova.mystyles.css"))
                {
                    if (stream != null)
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            var css = reader.ReadToEnd();
                            this.Resources.Add(StyleSheet.FromString(css));
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка загрузки CSS: {ex.Message}");
            }
        }

        private async void OnSignInClicked(object sender, System.EventArgs e)
        {
            string surname = SurnameEntry.Text?.Trim();

            if (string.IsNullOrWhiteSpace(surname))
            {
                ErrorLabel.Text = "Пожалуйста, введите вашу фамилию";
                ErrorLabel.IsVisible = true;
                return;
            }

            ErrorLabel.IsVisible = false;
            var mainPage = new MainTabbedPage(surname);
            await Navigation.PushAsync(mainPage);
        }
    }
}