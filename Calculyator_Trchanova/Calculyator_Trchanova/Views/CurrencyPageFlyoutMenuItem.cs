using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculyator_Trchanova.Views
{
    public class CurrencyPageFlyoutMenuItem
    {
        public CurrencyPageFlyoutMenuItem ()
        {
            TargetType = typeof(CurrencyPageFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}