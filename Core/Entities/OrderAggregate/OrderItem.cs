using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        public OrderItem()
        {
        }

        public OrderItem(ProductItemOrdered itemOrdered, decimal price, int quantity)
        {
            this.ItemOrdered = itemOrdered;
            this.Price = price;
            this.Quantity = quantity;
        }
        public ProductItemOrdered ItemOrdered { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}