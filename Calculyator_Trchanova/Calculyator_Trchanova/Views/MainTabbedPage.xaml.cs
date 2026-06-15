using Xamarin.Forms;
using System.IO;
using System.Reflection;
using Xamarin.Forms.StyleSheets;

namespace Calculyator_Trchanova.Views
{
    public partial class MainTabbedPage : TabbedPage
    {
        public MainTabbedPage(string surname)
        {
            InitializeComponent();
            this.Title = $"Добро пожаловать, {surname}!";
            LoadCssStyles();
        }

        private void LoadCssStyles()
        {
            try
            {
                var assembly = typeof(MainTabbedPage).GetTypeInfo().Assembly;
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