using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;
        private int _pageSize = 6;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? 50 : value;
        }
        public int? brandId { get; set; }
        public int? typeId { get; set; }
        public string sortDirection { get; set; }
        public string _search;
        public string Search
        {
            get
            {
                return _search;
            }
            set
            {
                _search = !string.IsNullOrEmpty(value.ToLower()) ? value : "";
            }
        }
    }
}