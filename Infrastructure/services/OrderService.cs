using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;

namespace Infrastructure.services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository basketRepo;
        private readonly IUniteOfWork uniteOfWork;
        public OrderService(IBasketRepository basketRepo, IUniteOfWork uniteOfWork)
        {
            this.uniteOfWork = uniteOfWork;
            this.basketRepo = basketRepo;
        }

        public async Task<Order> CreateOrder(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {
            //get basket
            var basket = await basketRepo.GetBasketAsync(basketId);
            //get items 
            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var prodItem = await uniteOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var itemOrderd = new ProductItemOrdered(prodItem.Id, prodItem.Name, prodItem.PictureURL);
                var otderItem = new OrderItem(itemOrderd, prodItem.Price, item.Quantity);
                items.Add(otderItem);
            }
            //get delivery methods
            var deliveryMethod = await uniteOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);
            //calculate totals - from database
            var subTotal = items.Sum(_ => _.Price * _.Quantity);
            //create the order
            var order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subTotal);
            uniteOfWork.Repository<Order>().Add(order);
            //save to DB
            var result = await uniteOfWork.Complete();
            //save to DB
            // if error, it will remove all the changes
            if (result <= 0) return null;
            //delete basket
            await basketRepo.DeleteBasketAsync(basketId);
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethods()
        {
            return await uniteOfWork.Repository<DeliveryMethod>().ListAllAsync();
        }

        public async Task<Order> GetOrderById(int id, string buyerEmail)
        {
            var spec = new OrdersSpecification(id, buyerEmail);
            return await uniteOfWork.Repository<Order>().GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUser(string buyerEmail)
        {
            var spec = new OrdersSpecification(buyerEmail);
            return await uniteOfWork.Repository<Order>().ListAsync(spec);
        }
    }
}