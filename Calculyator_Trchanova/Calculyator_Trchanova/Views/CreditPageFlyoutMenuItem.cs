using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculyator_Trchanova.Views
{
    public class CreditPageFlyoutMenuItem
    {
        public CreditPageFlyoutMenuItem ()
        {
            TargetType = typeof(CreditPageFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}