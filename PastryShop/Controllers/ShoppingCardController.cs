using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PastryShop.Models;
using PastryShop.Models.Interfaces;
using PastryShop.ViewModels;

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

            var shoppingCardViewModel = new ShoppingCardViewModel
            {
                ShoppingCard = _shoppingCard,
                ShoppingCardTotal = _shoppingCard.GetShoppingCardTotal()
            };

            return View(shoppingCardViewModel);
        }

        public RedirectToActionResult AddToShoppingCard(int pieId)
        {
            var selectedPie = _pieRepository.AllPies.FirstOrDefault(p => p.PieId == pieId);

            if (selectedPie != null)
            {
                _shoppingCard.AddToCard(selectedPie, 1);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCard(int pieId)
        {
            var selectedPie = _pieRepository.AllPies.FirstOrDefault(p => p.PieId == pieId);

            if (selectedPie != null)
            {
                _shoppingCard.RemoveFromCard(selectedPie);
            }
            return RedirectToAction("Index");
        }
    }
}
