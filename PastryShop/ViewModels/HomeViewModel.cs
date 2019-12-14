using System;
using System.Collections.Generic;
using PastryShop.Models;

namespace PastryShop.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Pie> PiesOfTheWeek { get; set; }
    }
}
