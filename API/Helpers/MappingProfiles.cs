using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;


namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToRunDTOs>().ForMember(_ => _.ProductBrand, o => o.MapFrom(_ => _.ProductBrand.Name)).ForMember(_ => _.ProductType, o => o.MapFrom(_ => _.ProductType.Name)).
            ForMember(_ => _.PictureURL, o => o.MapFrom<ProductUrlResolver>());

            CreateMap<Address, AddressDTO>().ReverseMap();
            CreateMap<CustomerBasketDTO, CustomerBasket>();
            CreateMap<BasketItemDTO, BasketItem>();
            CreateMap<AddressDTO, Core.Entities.OrderAggregate.Address>();
            CreateMap<Core.Entities.OrderAggregate.Order, OrderToReturnDTO>()
            .ForMember(d => d.DeliveryMethod, _ => _.MapFrom(s => s.DeliveryMethod.ShortName))
            .ForMember(d => d.ShippingPrice, _ => _.MapFrom(s => s.DeliveryMethod.Price));
            CreateMap<Core.Entities.OrderAggregate.OrderItem, OrderItemDTO>()
            .ForMember(d => d.ProductId, _ => _.MapFrom(s => s.ItemOrdered.ProductItemId))
            .ForMember(d => d.ProductName, _ => _.MapFrom(s => s.ItemOrdered.ProductName))
            .ForMember(d => d.PictureURL, _ => _.MapFrom(s => s.ItemOrdered.PictureUrl))
            .ForMember(d => d.PictureURL, _ => _.MapFrom<OrderItemURLResolver>());
        }
    }
}