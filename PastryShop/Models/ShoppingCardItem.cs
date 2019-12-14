using System;
using System.ComponentModel.DataAnnotations;

namespace PastryShop.Models
{
    public class ShoppingCardItem
    {
        [Key]
        public int ItemId { get; set; }
        public Pie Pie { get; set; }
        public int Amount { get; set; }
        public string ShoppingCardId { get; set; }
    }
}
