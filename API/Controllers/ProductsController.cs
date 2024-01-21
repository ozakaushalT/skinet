using Core.Entities;
using Core.Interfaces;
using Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productService;
        //this will be disposed when controller disposed.
        public ProductsController(IProductRepository productService)
        {
            _productService = productService;
        }

        //why async is important
        // if a method is set to async 
        // it is now treated as a task
        // will be like complete the task meanwhile
        // processes another requests so 
        // there is a less probability of blockage
        // when first task is done it uses the thread again
        //..


        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _productService.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return Ok(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productService.GetProductBrandsAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _productService.GetProductTypesAsync());
        }
    }
}