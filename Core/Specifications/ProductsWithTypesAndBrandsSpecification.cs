using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams prodParams)
         : base(_ => (string.IsNullOrEmpty(prodParams.Search) || _.Name.ToLower().Contains(prodParams.Search)) &&
         (!prodParams.brandId.HasValue || _.ProductBrandId == prodParams.brandId) && (!prodParams.typeId.HasValue || _.ProductTypeId == prodParams.typeId))
        {
            AddInclude(_ => _.ProductType);
            AddInclude(_ => _.ProductBrand);
            AddOrderBy(_ => _.Name);
            ApplyPaging((prodParams.PageIndex - 1) * prodParams.PageSize, prodParams.PageSize);
            if (!string.IsNullOrEmpty(prodParams.sortDirection))
            {
                switch (prodParams.sortDirection)
                {
                    case "priceAsc":
                        AddOrderBy(_ => _.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(_ => _.Price);
                        break;
                    case "nameAsc":
                        AddOrderBy(_ => _.Name);
                        break;
                    case "nameDesc":
                        AddOrderByDesc(_ => _.Name);
                        break;
                    default:
                        AddOrderBy(_ => _.Name);
                        break;
                }
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(_ => _.Id == id)
        {
            AddInclude(_ => _.ProductType);
            AddInclude(_ => _.ProductBrand);
        }
    }
}