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
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ShoppingCard _shoppingCard;

        public OrderController(IOrderRepository orderRepository, ShoppingCard shoppingCard)
        {
            _orderRepository = orderRepository;
            _shoppingCard = shoppingCard;
        }

        public IActionResult Checkout()
        {
            return View();
        }
    }
}