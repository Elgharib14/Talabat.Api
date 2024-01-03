using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Api.DTOS;
using Talabat.Api.Error;
using Talabat.Core.Entityes;
using Talabat.Core.Repository;

namespace Talabat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRep basketRep;
        private readonly IMapper mapper;

        public BasketController(IBasketRep basketRep , IMapper mapper)
        {
            this.basketRep = basketRep;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetCustomerBasket(string id)
        {
            var basket = await basketRep.GetBasket(id);
            return basket is null ? new CustomerBasket(id) : basket;
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var data = mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
            var createorupdateBasket = await basketRep.UpdateBasket(data);
            if(createorupdateBasket is null) 
            {
                return BadRequest(new ApiErrorRespones(404));
            }
            return Ok(createorupdateBasket);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeletBAsket(string id)
        {
            return await basketRep.DeleteBasket(id);
        }
    }
}
