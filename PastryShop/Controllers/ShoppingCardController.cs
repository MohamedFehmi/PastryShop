using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PastryShop.Models;
using PastryShop.Models.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PastryShop.Controllers
{
    public class ShoppingCardController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ShoppingCard _shoppingCard;

        public ShoppingCardController(IPieRepository pieRepository, ShoppingCard shoppingCard)
        {
            _pieRepository = pieRepository;
            _shoppingCard = shoppingCard;
        }

        public ViewResult Index()
        {
            var items = _shoppingCard.GetShoppingCardItems();
            _shoppingCard.ShoppingCardItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCard,
                ShoppingCartTotal = _shoppingCard.GetShoppingCartTotal()
            };

            return View(shoppingCartViewModel);
        }

        public RedirectToActionResult AddToShoppingCart(int pieId)
        {
            var selectedPie = _pieRepository.AllPies.FirstOrDefault(p => p.PieId == pieId);

            if (selectedPie != null)
            {
                _shoppingCard.AddToCart(selectedPie, 1);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int pieId)
        {
            var selectedPie = _pieRepository.AllPies.FirstOrDefault(p => p.PieId == pieId);

            if (selectedPie != null)
            {
                _shoppingCard.RemoveFromCart(selectedPie);
            }
            return RedirectToAction("Index");
        }
    }
}
