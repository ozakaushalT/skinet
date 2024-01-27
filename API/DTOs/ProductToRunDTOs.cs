using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace API.DTOs
{
    public class ProductToRunDTOs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureURL { get; set; } = "images/products/sb-ang1.png";
        public string ProductType { get; set; }

        public string ProductBrand { get; set; }
    }
}