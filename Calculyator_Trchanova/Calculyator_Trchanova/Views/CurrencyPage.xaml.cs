using Xamarin.Forms;
using Calculyator_Trchanova.ViewModels;
using System.IO;
using System.Reflection;
using Xamarin.Forms.StyleSheets;

namespace Calculyator_Trchanova.Views
{
    public partial class CurrencyPage : ContentPage
    {
        public CurrencyPage()
        {
            InitializeComponent();
            BindingContext = new CurrencyViewModel();
            LoadCssStyles();
        }

        private void LoadCssStyles()
        {
            try
            {
                var assembly = typeof(CurrencyPage).GetTypeInfo().Assembly;
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
    }
}