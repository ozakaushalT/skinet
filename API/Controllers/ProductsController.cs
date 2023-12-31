using Core.Entities;
using Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext _context;
        //this will be disposed when controller disposed.
        public ProductsController(StoreContext context)
        {
            _context = context;
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
            var products = await _context.Products.ToListAsync();
            return products;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return product;
        }
    }
}