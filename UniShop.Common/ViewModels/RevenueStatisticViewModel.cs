using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniShop.Common.ViewModels
{
    public class RevenueStatisticViewModel
    {
        public DateTime Date { get; set; }
        public decimal Revenues { get; set; }
        public decimal Benefit { get; set; }
    }
}
