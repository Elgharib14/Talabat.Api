using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Api.DTOS;
using Talabat.Api.Error;
using Talabat.Api.Hellper;
using Talabat.Core;
using Talabat.Core.Entityes;
using Talabat.Core.Repository;
using Talabat.Core.Specification;

namespace Talabat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        //private readonly IGenaricRepo<Product> genaricRepo;
        //private readonly IGenaricRepo<ProductBrand> genaricBrand;
        //private readonly IGenaricRepo<ProductType> genaricType;
        private readonly IMapper mapper;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            //this.genaricRepo = genaricRepo;
            //this.genaricBrand = genaricBrand;
            //this.genaricType = genaricType;
            this.mapper = mapper;
        }


        
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDTO>>> GetAllProduct([FromQuery]ProductSpecParams specParams)
        {
            var spec = new ProductWithBrandandTypeSpecification(specParams);
            var countspec = new productwithfiltrationforcont(specParams);
            var products = await unitOfWork.Repository<Product>().GetWithSpecAll(spec);
            var map = mapper.Map<IReadOnlyList<ProductDTO>>(products);
            var count = await unitOfWork.Repository<Product>().GetCountWithSpec(countspec);
            return Ok(new Pagination<ProductDTO>(specParams.PageIndex , specParams.PageSize , count , map));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var spec = new ProductWithBrandandTypeSpecification(id);
            var product = await unitOfWork.Repository<Product>().GetbyIdWithSpec(spec);
            if (product == null)
                return NotFound(new ApiErrorRespones(404));
            var map = mapper.Map<ProductDTO>(product);
            return Ok(map);
        }

        [HttpGet("GetBrands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var data = await unitOfWork.Repository<ProductBrand>().GetAll();
            if (data == null)
                return NotFound(new ApiErrorRespones(404));
            return Ok(data);    
        }

        [HttpGet("GetTypes")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var data = await unitOfWork.Repository<ProductType>().GetAll();
            if (data == null)
                return NotFound(new ApiErrorRespones(404));
            return Ok(data);
        }

    }
}
