using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using AutoMapper;
using Core.Entities.OrderAggregate;

namespace API.Helpers
{
    public class OrderItemURLResolver : IValueResolver<OrderItem, OrderItemDTO, string>
    {
        private readonly IConfiguration config;
        public OrderItemURLResolver(IConfiguration config)
        {
            this.config = config;

        }

        public string Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ItemOrdered.PictureUrl))
            {
                return config["ApiUrl"] + source.ItemOrdered.PictureUrl;
            }

            return config["ApiUrl"] + "images/products/sb-ang1.png";
        }
    }
}