using System;
using System.Collections.Generic;
using PastryShop.Models.Interfaces;

namespace PastryShop.Models.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ShoppingCard _shoppingCard;

        public OrderRepository(AppDbContext appDbContext, ShoppingCard shoppingCard)
        {
            _appDbContext = appDbContext;
            _shoppingCard = shoppingCard;
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.UtcNow;

            var shoppingCardItems = _shoppingCard.ShoppingCardItems;
            order.OrderTotal = _shoppingCard.GetShoppingCardTotal();

            order.OrderDetails = new List<OrderDetail>();

            //adding the order with its details
            foreach (var shoppingCardItem in shoppingCardItems)
            {
                var orderDetail = new OrderDetail
                {
                    Amount = shoppingCardItem.Amount,
                    PieId = shoppingCardItem.Pie.PieId,
                    Price = shoppingCardItem.Pie.Price
                };

                order.OrderDetails.Add(orderDetail);
            }

            _appDbContext.Orders.Add(order);

            _appDbContext.SaveChanges();
        }
    }
}
