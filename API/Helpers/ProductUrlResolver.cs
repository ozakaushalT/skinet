using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductToRunDTOs, string>
    {
        private readonly IConfiguration config;
        public ProductUrlResolver(IConfiguration config)
        {
            this.config = config;

        }

        public string Resolve(Product source, ProductToRunDTOs destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureURL))
            {
                return config["ApiUrl"] + source.PictureURL;
            }

            return config["ApiUrl"] + "images/products/sb-ang1.png";
        }
    }
}