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
    public partial class CreditPageFlyout :ContentPage
    {
        public ListView ListView;

        public CreditPageFlyout ()
        {
            InitializeComponent();

            BindingContext = new CreditPageFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class CreditPageFlyoutViewModel :INotifyPropertyChanged
        {
            public ObservableCollection<CreditPageFlyoutMenuItem> MenuItems { get; set; }

            public CreditPageFlyoutViewModel ()
            {
                MenuItems = new ObservableCollection<CreditPageFlyoutMenuItem>(new[]
                {
                    new CreditPageFlyoutMenuItem { Id = 0, Title = "Page 1" },
                    new CreditPageFlyoutMenuItem { Id = 1, Title = "Page 2" },
                    new CreditPageFlyoutMenuItem { Id = 2, Title = "Page 3" },
                    new CreditPageFlyoutMenuItem { Id = 3, Title = "Page 4" },
                    new CreditPageFlyoutMenuItem { Id = 4, Title = "Page 5" },
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