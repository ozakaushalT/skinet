using API.DTOs;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace API.Controllers
{

    public class ProductsController : BaseAPIController
    {
        public IGenericRepository<Product> _prodRepo { get; }
        public IGenericRepository<ProductBrand> _brandRepo { get; }
        public IGenericRepository<ProductType> _typeRepo { get; }
        private readonly IMapper _mapper;

        //this will be disposed when controller disposed.
        public ProductsController(IGenericRepository<Product> prodRepo, IGenericRepository<ProductBrand> brandRepo, IGenericRepository<ProductType> typeRepo,
        IMapper mapper
        )
        {
            this._mapper = mapper;
            this._prodRepo = prodRepo;
            this._brandRepo = brandRepo;
            this._typeRepo = typeRepo;
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
        public async Task<ActionResult<IReadOnlyList<ProductToRunDTOs>>> GetProducts()
        {

            var spec = new ProductsWithTypesAndBrandsSpecification();
            var products = await _prodRepo.ListAsync(spec);
            return Ok(_mapper.
            Map<IReadOnlyList<Product>, IReadOnlyList<ProductToRunDTOs>>(products));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToRunDTOs>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            //So after first step our criteria and Includes are ready to go
            var product = await _prodRepo.GetEntityWithSpec(spec);
            if (product == null) return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Product, ProductToRunDTOs>(product));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _brandRepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _typeRepo.ListAllAsync());
        }
    }
}