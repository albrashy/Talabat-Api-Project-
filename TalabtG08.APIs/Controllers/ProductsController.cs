using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalabatG08.Core;
using TalabatG08.Core.Entites;
using TalabatG08.Core.Repositories;
using TalabatG08.Core.Specifications;
using TalabtG08.APIs.Dtos;
using TalabtG08.APIs.Errors;
using TalabtG08.APIs.Helpers;

namespace TalabtG08.APIs.Controllers
{
                 
    public class ProductsController : ApiBaseController
    {
        
        private readonly IMapper mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IMapper mapper,IUnitOfWork unitOfWork)
        {
           
            this.mapper = mapper;
            _unitOfWork = unitOfWork;
        }
       // [Authorize]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Pagination<ProductToReturnDto>>>> GetProducts([FromQuery] ProductSpecParams productSpec )
        {
            var spec = new ProductSpecifications(productSpec);


            var products =await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);

            var mappProducts = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            var CountSpec = new ProductForFilterationForCountSpecifications(productSpec);
            var Count = await _unitOfWork.Repository<Product>().GetCountWithSpecAsync(CountSpec);

            return Ok(new Pagination<ProductToReturnDto> (productSpec.PageSize , productSpec.PageIndex,mappProducts ,Count));
        }


        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProductById(int id)
        {
            var spec = new ProductSpecifications(id);

            var product = await _unitOfWork.Repository<Product>().GetByEntityWithSpecAsync(spec);
            if (product == null) { return NotFound(new ApiErrorResponse(404)); }

            var mappProduct = mapper.Map<Product, ProductToReturnDto>(product);


            return Ok(mappProduct);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllbrandAsync()
        {
            var brands = await _unitOfWork.Repository<ProductBrand>().GetAllAsync();

            return Ok(brands);


        }

        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.Repository<ProductType>().GetAllAsync();

            return Ok(Types);


        }

    }
}
