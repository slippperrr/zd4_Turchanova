using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Calculyator_Trchanova.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrencyPageFlyout :ContentPage
    {
        public ListView ListView;

        public CurrencyPageFlyout ()
        {
            InitializeComponent();

            BindingContext = new CurrencyPageFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class CurrencyPageFlyoutViewModel :INotifyPropertyChanged
        {
            public ObservableCollection<CurrencyPageFlyoutMenuItem> MenuItems { get; set; }

            public CurrencyPageFlyoutViewModel ()
            {
                MenuItems = new ObservableCollection<CurrencyPageFlyoutMenuItem>(new[]
                {
                    new CurrencyPageFlyoutMenuItem { Id = 0, Title = "Page 1" },
                    new CurrencyPageFlyoutMenuItem { Id = 1, Title = "Page 2" },
                    new CurrencyPageFlyoutMenuItem { Id = 2, Title = "Page 3" },
                    new CurrencyPageFlyoutMenuItem { Id = 3, Title = "Page 4" },
                    new CurrencyPageFlyoutMenuItem { Id = 4, Title = "Page 5" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged ([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}