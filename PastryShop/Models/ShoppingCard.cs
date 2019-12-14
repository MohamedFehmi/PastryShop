using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PastryShop.Models
{
    public class ShoppingCard
    {
        private readonly AppDbContext _appDbContext;

        public string ShoppingCardId { get; set; }

        public List<ShoppingCardItem> ShoppingCardItems { get; set; }

        private ShoppingCard(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public static ShoppingCard GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            var context = services.GetService<AppDbContext>();

            string CardId = session.GetString("CardId") ?? Guid.NewGuid().ToString();

            session.SetString("CardId", CardId);

            return new ShoppingCard(context) { ShoppingCardId = CardId };
        }

        public void AddToCart(Pie pie, int amount)
        {
            var shoppingCardItem =
                    _appDbContext.ShoppingCardItems.SingleOrDefault(
                        s => s.Pie.PieId == pie.PieId && s.ShoppingCardId == ShoppingCardId);

            if (shoppingCardItem == null)
            {
                shoppingCardItem = new ShoppingCardItem
                {
                    ShoppingCardId = ShoppingCardId,
                    Pie = pie,
                    Amount = 1
                };

                _appDbContext.ShoppingCardItems.Add(shoppingCardItem);
            }
            else
            {
                shoppingCardItem.Amount++;
            }
            _appDbContext.SaveChanges();
        }

        public int RemoveFromCart(Pie pie)
        {
            var shoppingCardItem =
                    _appDbContext.ShoppingCardItems.SingleOrDefault(
                        s => s.Pie.PieId == pie.PieId && s.ShoppingCardId == ShoppingCardId);

            var localAmount = 0;

            if (shoppingCardItem != null)
            {
                if (shoppingCardItem.Amount > 1)
                {
                    shoppingCardItem.Amount--;
                    localAmount = shoppingCardItem.Amount;
                }
                else
                {
                    _appDbContext.ShoppingCardItems.Remove(shoppingCardItem);
                }
            }

            _appDbContext.SaveChanges();

            return localAmount;
        }

        public List<ShoppingCardItem> GetShoppingCardItems()
        {
            return ShoppingCardItems ??
                   (ShoppingCardItems =
                       _appDbContext.ShoppingCardItems.Where(c => c.ShoppingCardId == ShoppingCardId)
                           .Include(s => s.Pie)
                           .ToList());
        }

        public void ClearCart()
        {
            var CardItems = _appDbContext
                .ShoppingCardItems
                .Where(cart => cart.ShoppingCardId == ShoppingCardId);

            _appDbContext.ShoppingCardItems.RemoveRange(CardItems);

            _appDbContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _appDbContext.ShoppingCardItems.Where(c => c.ShoppingCardId == ShoppingCardId)
                .Select(c => c.Pie.Price * c.Amount).Sum();
            return total;
        }
    }
}
