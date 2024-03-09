using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DTOs;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class OrdersController : BaseAPIController
    {
        private readonly IOrderService _orderServ;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderServ, IMapper mapper)
        {
            _orderServ = orderServ;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDTO orderData)
        {
            var email = HttpContext.User.GetEmailFromPrincipal();
            var address = _mapper.Map<AddressDTO, Core.Entities.OrderAggregate.Address>(orderData.Address);
            var order = await _orderServ.CreateOrder(email, orderData.DeliveryMethodId, orderData.BasketId, address);
            if (order == null)
            {
                return BadRequest(new ApiResponse(400, "Error creating an order"));
            }

            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrdersForUser()
        {
            var email = HttpContext.User.GetEmailFromPrincipal();
            var orders = await _orderServ.GetOrdersForUser(email);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderByIdForUser(int id)
        {
            var email = HttpContext.User.GetEmailFromPrincipal();
            var order = await _orderServ.GetOrderById(id, email);
            if (order == null) return NotFound(new ApiResponse(404));

            return Ok(order);
        }

        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            return Ok(await _orderServ.GetDeliveryMethods());
        }
    }
}