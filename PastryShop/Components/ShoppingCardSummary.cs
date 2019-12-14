using System;
using Microsoft.AspNetCore.Mvc;
using PastryShop.Models;
using PastryShop.ViewModels;

namespace PastryShop.Components
{
    public class ShoppingCardSummary : ViewComponent
    {
        private readonly ShoppingCard _shoppingCard;

        public ShoppingCardSummary(ShoppingCard shoppingCard)
        {
            _shoppingCard = shoppingCard;
        }

        public IViewComponentResult Invoke()
        {
            //get shopping card items
            _shoppingCard.ShoppingCardItems = _shoppingCard.GetShoppingCardItems();

            //create view model
            var shoppingCardViewModel = new ShoppingCardViewModel
            {
                ShoppingCard = _shoppingCard,
                ShoppingCardTotal = _shoppingCard.GetShoppingCardTotal()
            };

            return View(shoppingCardViewModel);
        }
    }
}
