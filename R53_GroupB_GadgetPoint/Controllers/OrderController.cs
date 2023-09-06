using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using R53_GroupB_GadgetPoint.DAL.Interface;
using R53_GroupB_GadgetPoint.DTOs;
using R53_GroupB_GadgetPoint.Models;
using System.Security.Claims;

namespace R53_GroupB_GadgetPoint.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderservice, IMapper mapper)
        {
            this._orderService = orderservice;
            this._mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x=>x.Type==ClaimTypes.Email)?.Value;

            var address = _mapper.Map<AddressDTO, ShippingAddress>(orderDto.ShipToAddress);

            var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);
            if (order==null)
            {
                return BadRequest();
            }
            return Ok(order);
        }

    }
}
