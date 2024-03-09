using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities.OrderAggregate;

namespace Core.Specifications
{
    public class OrdersSpecification : BaseSpecification<Order>
    {
        public OrdersSpecification(string email) : base(_ => _.BuyerEmail == email)
        {
            AddInclude(_ => _.OrderItems);
            AddInclude(_ => _.DeliveryMethod);
            AddOrderByDesc(_ => _.OrderDate);
        }

        public OrdersSpecification(int id, string buyerEmail)
        : base(_ => _.Id == id && _.BuyerEmail == buyerEmail)
        {
            AddInclude(_ => _.OrderItems);
            AddInclude(_ => _.DeliveryMethod);
        }
    }
}