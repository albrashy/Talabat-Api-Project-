using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TalabatG08.Core.Entites.Order_Aggregate;
using TalabatG08.Core.Services;
using TalabatG08.Core.Specifications.OrderSpec;
using TalabtG08.APIs.Dtos;
using TalabtG08.APIs.Errors;

namespace TalabtG08.APIs.Controllers
{
   
    public class OrdersController : ApiBaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper mapper;

        public OrdersController(IOrderService order, IMapper mapper)
        {
            this._orderService = order;
            this.mapper = mapper;
        }

        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]   
        [HttpPost]  //post => baseUrl/Api/Orders
        [Authorize]
        public async Task<ActionResult<Order>> CreateOrder (OrderDto orderDto)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var MappedAddress = mapper.Map<AddressDto, Address>(orderDto.shipToAddress);

            var Order =await _orderService.CreateOrderAsync(BuyerEmail, orderDto.BasketId, orderDto.deliveryMethodId, MappedAddress);

            if(Order is null) { return BadRequest(new ApiErrorResponse(400,"there is problem with your order")); }

            return Ok(Order);
        }

        //================================  GetOrdersByEmailAsync  ==============================

        [ProducesResponseType(typeof(IReadOnlyList<OrderToReturnDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersByEmailAsync()
        {
            var buyeremail = User.FindFirstValue(ClaimTypes.Email);
            var orders =await _orderService.GetOrdersForSpecificUserAsync(buyeremail);
            if(orders is null) { return NotFound(new ApiErrorResponse(404, "there is no orders for this users ")); }
            var mappedOrders = mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(orders);   

            return Ok(mappedOrders);  
           
        }

        //=======================  GetOrderByIdForSpecificationUser  =====================

        [ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForSpecificationUser(int id)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var order =await _orderService.GetOrderByIdForSpecificUserAsync(buyerEmail, id);
            if(order is null) { return NotFound(new ApiErrorResponse(404, $"there is no order id ={id} for this user")); }

            var mappedOrder = mapper.Map<Order,OrderToReturnDto>(order);    

            return Ok(mappedOrder);   


        }


        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            var DeliveryMerthods =await _orderService.GetDeliveryMethodsService();
            return Ok(DeliveryMerthods);    
        }

    }
}
