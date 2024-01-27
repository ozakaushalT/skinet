using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToRunDTOs>().ForMember(_ => _.ProductBrand, o => o.MapFrom(_ => _.ProductBrand.Name)).ForMember(_ => _.ProductType, o => o.MapFrom(_ => _.ProductType.Name)).
            ForMember(_ => _.PictureURL, o => o.MapFrom<ProductUrlResolver>());
        }
    }
}