using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithFiltersForCountSpecs : BaseSpecification<Product>
    {
        public ProductsWithFiltersForCountSpecs(ProductSpecParams prodParams) : base(_ =>
        (string.IsNullOrEmpty(prodParams.Search) || _.Name.ToLower().Contains(prodParams.Search)) &&
        (!prodParams.brandId.HasValue || _.ProductBrandId == prodParams.brandId) && (!prodParams.typeId.HasValue || _.ProductTypeId == prodParams.typeId))
        {
        }
    }
}