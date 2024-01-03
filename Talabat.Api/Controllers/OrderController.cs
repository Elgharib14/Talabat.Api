using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Security.Claims;
using Talabat.Api.DTOS;
using Talabat.Api.Error;
using Talabat.Core.Entityes.OrderAggregate;
using Talabat.Core.Services;

namespace Talabat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper mapper;

        public OrderController(IOrderService orderService , IMapper mapper)
        {
            _orderService = orderService;
            this.mapper = mapper;
        }
        [ProducesResponseType(typeof(OrderToReturnDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorRespones),StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var address = mapper.Map<AddressDto,Address>(orderDto.address);
            var order = await _orderService.CreatOrder(buyerEmail, orderDto.basketId, orderDto.DeliveryMethodId, address);

            if(order is null)
            {
                return BadRequest(new ApiErrorRespones(400)); 
            }

            return Ok(mapper.Map<OrderToReturnDto>(order));
        }


        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderService.GetOrderForUser(buyerEmail);
            return Ok(mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders));
        }

        [ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorRespones), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderForUser(int id)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = await _orderService.GetOrderById(id, buyerEmail);
            if (order is null) return NotFound();
            return Ok(mapper.Map<OrderToReturnDto>(order));
        }


        [HttpGet("GetDeliveryMethod")]
        public async Task<ActionResult<IReadOnlyList<DeleveryMethod>>> GetDeliveryMethod()
        {
            var data = await _orderService.GetDeleveryMethod();
            return Ok(data);
        }

    } 
}
